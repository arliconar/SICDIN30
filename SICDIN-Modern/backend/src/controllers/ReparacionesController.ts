import { Request, Response } from 'express';
import db from '../db';
import { syncEngine } from '../sync/SyncEngine';

interface ItemReparacion {
  biciId: number;
  Clave: string;
  Descripcion: string;
  Cantidad: number;
  Precio: number;
  Trabajo: number; // 0 = Refacción, 1 = Mano de obra
  Paquete?: string;
  Realizo?: number;
}

interface BiciReparacion {
  Id: number;
  Marca: string;
  Color: string;
  Rodada: string;
  Nota?: string;
  Fhentrega?: string;
}

export const ReparacionesController = {
  // 1. Listar reparaciones con filtros de estado y búsqueda
  listar: (req: Request, res: Response) => {
    try {
      const q = (req.query.q as string || '').trim();
      const filtro = (req.query.filtro as string || 'ACTIVAS').toUpperCase(); // ACTIVAS, LISTAS, ENTREGADAS, TODAS

      let whereClause = '1=1';
      const params: any[] = [];

      if (filtro === 'ACTIVAS') {
        // En taller: no entregadas y no canceladas
        whereClause += ' AND n.bentregada = 0 AND (n.Fcan = 0 OR n.Fcan IS NULL)';
      } else if (filtro === 'LISTAS') {
        // Finalizadas por el mecánico pero aún no entregadas al cliente
        whereClause += ' AND n.bentregada = 0 AND n.Finalizadaall = 1 AND (n.Fcan = 0 OR n.Fcan IS NULL)';
      } else if (filtro === 'ENTREGADAS') {
        whereClause += ' AND n.bentregada = 1';
      }

      if (q) {
        whereClause += ` AND (n.Folio LIKE ? OR n.Cliente LIKE ? OR n.telcel LIKE ? OR EXISTS (
          SELECT 1 FROM rep_bici b WHERE b.Folio = n.Folio AND (b.Marca LIKE ? OR b.Color LIKE ?)
        ))`;
        params.push(`%${q}%`, `%${q}%`, `%${q}%`, `%${q}%`, `%${q}%`);
      }

      const sql = `
        SELECT 
          n.Folio, n.Cliente, n.Fecha, n.Hora, n.Total, n.Usuario, n.telcel, n.telcasa, 
          n.Direccion, n.Fcreacion, n.Fechae, n.Finalizadaall, n.bentregada, n.Fcan,
          COALESCE((SELECT SUM(COALESCE(Efectivo, 0) + COALESCE(Tarjeta, 0) + COALESCE(Transferencia, 0)) FROM rep_pagos p WHERE p.Folio = n.Folio), 0) AS TotalPagado
        FROM rep_nota n
        WHERE ${whereClause}
        ORDER BY n.Fcreacion DESC
        LIMIT 100
      `;

      const notas = db.prepare(sql).all(...params) as any[];

      // Cargar bicicletas asociadas a cada nota
      const folios = notas.map(n => n.Folio);
      let bicisPorFolio: Record<string, any[]> = {};

      if (folios.length > 0) {
        const placeholders = folios.map(() => '?').join(',');
        const bicis = db.prepare(`
          SELECT Folio, Id, Marca, Color, Rodada, Nota, Fhentrega, bfinalizada, bentregada, bcancelada, Finalizada, Entregada, Realizo, Entrego
          FROM rep_bici
          WHERE Folio IN (${placeholders})
          ORDER BY Id ASC
        `).all(...folios) as any[];

        for (const b of bicis) {
          if (!bicisPorFolio[b.Folio]) bicisPorFolio[b.Folio] = [];
          bicisPorFolio[b.Folio].push(b);
        }
      }

      const resultado = notas.map(n => {
        const bicis = bicisPorFolio[n.Folio] || [];
        const total = Number(n.Total) || 0;
        const pagado = Number(n.TotalPagado) || 0;
        return {
          ...n,
          Resta: Math.max(0, total - pagado),
          bicicletas: bicis
        };
      });

      res.json(resultado);
    } catch (err: any) {
      console.error('[REPARACIONES LISTAR ERROR]', err);
      res.status(500).json({ error: err.message });
    }
  },

  // 2. Obtener detalle completo de una orden por Folio
  obtenerDetalle: (req: Request, res: Response) => {
    try {
      const { folio } = req.params;
      const nota = db.prepare('SELECT * FROM rep_nota WHERE Folio = ?').get(folio);
      if (!nota) return res.status(404).json({ error: 'Orden de reparación no encontrada' });

      const bicicletas = db.prepare('SELECT * FROM rep_bici WHERE Folio = ? ORDER BY Id ASC').all(folio);
      const articulos = db.prepare('SELECT * FROM rep_art WHERE Folio = ? ORDER BY Id ASC, Clave ASC').all(folio);
      const pagos = db.prepare('SELECT * FROM rep_pagos WHERE Folio = ? ORDER BY Fecha ASC').all(folio);
      const modificaciones = db.prepare('SELECT * FROM rep_mod WHERE Folio = ? ORDER BY Fecha DESC').all(folio);

      const totalPagado = pagos.reduce((sum: number, p: any) => sum + (Number(p.Efectivo || 0) + Number(p.Tarjeta || 0) + Number(p.Transferencia || 0)), 0);

      res.json({
        nota,
        bicicletas,
        articulos,
        pagos,
        modificaciones,
        totalPagado,
        resta: Math.max(0, (Number((nota as any).Total) || 0) - totalPagado)
      });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  // 3. Crear nueva orden de reparación
  crear: (req: Request, res: Response) => {
    try {
      const {
        clienteNombre,
        clienteTelcel = '',
        clienteDireccion = '',
        clienteRFC = '',
        clienteMail = '',
        usuario = 'ADMIN',
        fechaEntregaPrometida,
        bicicletas = [],
        articulos = [],
        anticipoEfectivo = 0,
        anticipoTarjeta = 0,
        anticipoTransfer = 0
      } = req.body as {
        clienteNombre: string;
        clienteTelcel: string;
        clienteDireccion: string;
        clienteRFC: string;
        clienteMail: string;
        usuario: string;
        fechaEntregaPrometida?: string;
        bicicletas: BiciReparacion[];
        articulos: ItemReparacion[];
        anticipoEfectivo: number;
        anticipoTarjeta: number;
        anticipoTransfer: number;
      };

      if (!clienteNombre || !clienteNombre.trim()) {
        return res.status(400).json({ error: 'El nombre del cliente es obligatorio' });
      }

      if (!bicicletas || bicicletas.length === 0) {
        return res.status(400).json({ error: 'Debes registrar al menos una bicicleta' });
      }

      // Calcular total sumando artículos y mano de obra
      const totalOrden = articulos.reduce((sum, it) => sum + (it.Cantidad * it.Precio), 0);
      const totalAnticipo = anticipoEfectivo + anticipoTarjeta + anticipoTransfer;

      // Generar consecutivo de folio (CR + YYMMDD + 001)
      const fecha = new Date();
      const yy = fecha.getFullYear().toString().slice(-2);
      const mm = (fecha.getMonth() + 1).toString().padStart(2, '0');
      const dd = fecha.getDate().toString().padStart(2, '0');
      const prefijo = `CR${yy}${mm}${dd}`;

      const ultimoFolioRow = db.prepare(`SELECT Folio FROM rep_nota WHERE Folio LIKE ? ORDER BY Folio DESC LIMIT 1`).get(`${prefijo}%`) as { Folio?: string } | undefined;
      let consecutivo = 1;
      if (ultimoFolioRow?.Folio) {
        const lastNum = parseInt(ultimoFolioRow.Folio.slice(-3), 10);
        if (!isNaN(lastNum)) consecutivo = lastNum + 1;
      }
      const folio = `${prefijo}${consecutivo.toString().padStart(3, '0')}`;

      const fcreacion = new Date().toISOString().replace('T', ' ').slice(0, 19);
      const fechaCorta = `${dd}/${mm}/${fecha.getFullYear()}`;
      const horaActual = new Date().toLocaleTimeString('es-MX', { hour: '2-digit', minute: '2-digit' });

      const tx = db.transaction(() => {
        // 1. Guardar cabecera de la nota
        db.prepare(`
          INSERT INTO rep_nota (
            Folio, Cliente, Fecha, Total, Fcan, Hora, Direccion, RFC, telcasa, telcel, mail, 
            Fcreacion, Usuario, Finalizadaall, bentregada, Fechae, Almacenada
          ) VALUES (?, ?, ?, ?, 0, ?, ?, ?, '', ?, ?, ?, ?, 0, 0, ?, 0)
        `).run(
          folio,
          clienteNombre.trim(),
          fechaCorta,
          totalOrden,
          horaActual,
          clienteDireccion.trim(),
          clienteRFC.trim(),
          clienteTelcel.trim(),
          clienteMail.trim(),
          fcreacion,
          usuario,
          fechaEntregaPrometida || null
        );

        // 2. Guardar cada bicicleta
        bicicletas.forEach((bici, idx) => {
          const biciId = bici.Id || (idx + 1);
          db.prepare(`
            INSERT INTO rep_bici (
              Folio, Id, Marca, Color, Rodada, Nota, bentregada, bfinalizada, bcancelada, Fhentrega, Almacenada
            ) VALUES (?, ?, ?, ?, ?, ?, 0, 0, 0, ?, 0)
          `).run(
            folio,
            biciId,
            bici.Marca || 'Genérica',
            bici.Color || '',
            bici.Rodada || '26',
            bici.Nota || '',
            bici.Fhentrega || fechaEntregaPrometida || null
          );
        });

        // 3. Guardar refacciones y mano de obra
        for (const art of articulos) {
          const biciId = art.biciId || 1;
          const precioFormatted = `1.-m:${art.Precio}`;

          db.prepare(`
            INSERT INTO rep_art (
              Folio, Id, Cantidad, Clave, Tp, cancel, colocado, Trabajo, Paquete, Realizo, Fecha, Descripcion, Hora
            ) VALUES (?, ?, ?, ?, ?, 0, 0, ?, ?, 0, ?, ?, ?)
          `).run(
            folio,
            biciId,
            art.Cantidad,
            art.Clave,
            precioFormatted,
            art.Trabajo ? 1 : 0,
            art.Paquete || (art.Trabajo ? 'MO' : 'PZA'),
            fechaCorta,
            art.Descripcion,
            horaActual
          );

          // Si es refacción de inventario físico, descontar del stock
          if (art.Trabajo === 0 && art.Clave) {
            db.prepare(`
              UPDATE art_articulos 
              SET Existencias = Existencias - ? 
              WHERE Clave = ?
            `).run(art.Cantidad, art.Clave);
          }
        }

        // 4. Registrar anticipo si hubo
        if (totalAnticipo > 0) {
          db.prepare(`
            INSERT INTO rep_pagos (Folio, Fecha, Usuario, Cantidad, Id, Efectivo, Transferencia, Tarjeta, Apartado)
            VALUES (?, ?, ?, ?, 1, ?, ?, ?, 0)
          `).run(
            folio,
            fcreacion,
            usuario,
            totalAnticipo,
            anticipoEfectivo,
            anticipoTransfer,
            anticipoTarjeta
          );
        }
      });

      tx();

      syncEngine.emitDelta('rep_nota', 'INSERT', 'Folio', folio, { Folio: folio, Cliente: clienteNombre, Total: totalOrden });

      res.status(201).json({
        success: true,
        folio,
        total: totalOrden,
        anticipo: totalAnticipo,
        resta: Math.max(0, totalOrden - totalAnticipo)
      });
    } catch (err: any) {
      console.error('[CREAR REPARACION ERROR]', err);
      res.status(500).json({ error: err.message });
    }
  },

  // 4. Cambiar estado de una bicicleta (Finalizar, Entregar, Cancelar)
  cambiarEstado: (req: Request, res: Response) => {
    try {
      const { folio, biciId } = req.params;
      const { accion, mecanico, usuario = 'ADMIN', motivoCancel } = req.body as {
        accion: 'FINALIZAR' | 'ENTREGAR' | 'CANCELAR';
        mecanico?: string;
        usuario?: string;
        motivoCancel?: string;
      };

      const now = new Date().toISOString().replace('T', ' ').slice(0, 19);

      if (accion === 'FINALIZAR') {
        db.prepare(`
          UPDATE rep_bici 
          SET bfinalizada = 1, Finalizada = ?, Realizo = ?
          WHERE Folio = ? AND Id = ?
        `).run(now, mecanico || usuario, folio, biciId);

        // Verificar si todas las bicis de esta orden están listas
        const pendientes = db.prepare(`SELECT count(*) as c FROM rep_bici WHERE Folio = ? AND bfinalizada = 0`).get(folio) as { c: number };
        if (pendientes.c === 0) {
          db.prepare(`UPDATE rep_nota SET Finalizadaall = 1 WHERE Folio = ?`).run(folio);
        }
      } else if (accion === 'ENTREGAR') {
        db.prepare(`
          UPDATE rep_bici 
          SET bentregada = 1, Entregada = ?, Entrego = ?
          WHERE Folio = ? AND Id = ?
        `).run(now, usuario, folio, biciId);

        // Verificar si todas están entregadas
        const pendientesEntrega = db.prepare(`SELECT count(*) as c FROM rep_bici WHERE Folio = ? AND bentregada = 0`).get(folio) as { c: number };
        if (pendientesEntrega.c === 0) {
          db.prepare(`UPDATE rep_nota SET bentregada = 1, Fechae = ? WHERE Folio = ?`).run(now, folio);
        }
      } else if (accion === 'CANCELAR') {
        db.prepare(`
          UPDATE rep_bici 
          SET bcancelada = 1, Cancelada = ?, Rcancel = ?
          WHERE Folio = ? AND Id = ?
        `).run(now, motivoCancel || 'Cancelado por usuario', folio, biciId);
      }

      res.json({ success: true, message: `Estado actualizado a ${accion}` });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  // 5. Registrar abono / liquidación de orden de taller
  registrarPago: (req: Request, res: Response) => {
    try {
      const { folio } = req.params;
      const {
        efectivo = 0,
        tarjeta = 0,
        transferencia = 0,
        usuario = 'ADMIN',
        entregarAlLiquidar = false
      } = req.body;

      const totalPagado = Number(efectivo || 0) + Number(tarjeta || 0) + Number(transferencia || 0);
      if (totalPagado <= 0) {
        return res.status(400).json({ error: 'El monto del pago debe ser mayor a 0' });
      }

      const fcreacion = new Date().toISOString().replace('T', ' ').slice(0, 19);

      db.prepare(`
        INSERT INTO rep_pagos (Folio, Fecha, Usuario, Cantidad, Id, Efectivo, Transferencia, Tarjeta, Apartado)
        VALUES (?, ?, ?, ?, 1, ?, ?, ?, 0)
      `).run(folio, fcreacion, usuario, totalPagado, efectivo, transferencia, tarjeta);

      if (entregarAlLiquidar) {
        db.prepare(`
          UPDATE rep_bici 
          SET bentregada = 1, Entregada = ?, Entrego = ? 
          WHERE Folio = ? AND bentregada = 0
        `).run(fcreacion, usuario, folio);

        db.prepare(`
          UPDATE rep_nota 
          SET bentregada = 1, Fechae = ? 
          WHERE Folio = ?
        `).run(fcreacion, folio);
      }

      res.json({ success: true, message: 'Pago registrado con éxito', pago: totalPagado });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  // 6. Marcar refacción como colocada / no colocada (checklist de taller)
  toggleColocado: (req: Request, res: Response) => {
    try {
      const { folio, clave } = req.params;
      const { colocado } = req.body as { colocado: boolean };

      db.prepare(`
        UPDATE rep_art 
        SET colocado = ? 
        WHERE Folio = ? AND Clave = ?
      `).run(colocado ? 1 : 0, folio, clave);

      res.json({ success: true, colocado: colocado ? 1 : 0 });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  // 7. Lista de trabajadores mecánicos del taller
  listarTrabajadores: (req: Request, res: Response) => {
    try {
      const rows = db.prepare(`
        SELECT Id, Nombre, Telefono, telcel, Activo 
        FROM trab_trabajador 
        WHERE Activo = 1 
        ORDER BY Nombre ASC
      `).all();
      res.json(rows);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  }
};
