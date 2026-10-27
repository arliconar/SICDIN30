import { Request, Response } from 'express';
import db from '../db';
import { syncEngine } from '../sync/SyncEngine';

interface ItemVenta {
  Clave: string;
  Descripcion: string;
  Cantidad: number;
  Precio: number;
  Total: number;
  Paquete?: string;
}

export const VentasController = {
  crearVenta: (req: Request, res: Response) => {
    try {
      const {
        clienteId = 0,
        usuario = 'ADMIN',
        pagoEfectivo = 0,
        pagoTarjeta = 0,
        pagoTransfer = 0,
        descuento = 0,
        items
      } = req.body as {
        clienteId: number;
        usuario: string;
        pagoEfectivo: number;
        pagoTarjeta: number;
        pagoTransfer: number;
        descuento: number;
        items: ItemVenta[];
      };

      if (!items || items.length === 0) {
        return res.status(400).json({ error: 'La venta debe contener al menos un producto' });
      }

      const total = items.reduce((sum, it) => sum + (it.Cantidad * it.Precio), 0) - descuento;
      const totalPagado = pagoEfectivo + pagoTarjeta + pagoTransfer;
      const resta = Math.max(0, total - totalPagado);

      // Obtener adeudo previo del cliente si aplica
      let adeudoPrevio = 0;
      if (clienteId > 0) {
        const clienteRow = db.prepare('SELECT Adeudo FROM clnt_client WHERE Id = ?').get(clienteId) as { Adeudo?: number } | undefined;
        adeudoPrevio = Number(clienteRow?.Adeudo) || 0;
      }
      const totalConAdeudo = total + adeudoPrevio;
      const cambioFinal = Math.max(0, totalPagado - totalConAdeudo);

      // Generar nuevo folio
      const fechaHoy = new Date().toISOString().slice(0, 10).replace(/-/g, '');
      const lastFolioRow = db.prepare(`SELECT Folio FROM vent_venta WHERE Folio LIKE ? ORDER BY Folio DESC LIMIT 1`).get(`V${fechaHoy}%`) as { Folio?: string } | undefined;
      let consecutivo = 1;
      if (lastFolioRow?.Folio) {
        const lastNum = parseInt(lastFolioRow.Folio.slice(-4), 10);
        if (!isNaN(lastNum)) consecutivo = lastNum + 1;
      }
      const folio = `V${fechaHoy}${consecutivo.toString().padStart(4, '0')}`;

      let nuevoAdeudo = adeudoPrevio;

      // 0. Validar existencias antes de proceder con la venta
      for (const it of items) {
        const artRow = db.prepare('SELECT Descripcion, Tipo, Existencias FROM art_articulos WHERE Clave = ?').get(it.Clave) as { Descripcion?: string; Tipo?: string; Existencias?: number } | undefined;
        if (artRow) {
          const tipoNormalizado = (artRow.Tipo || (it as any).Tipo || '').toLowerCase().trim();
          const esSinControlStock = tipoNormalizado === 'servicio' || tipoNormalizado === 'infinito' || tipoNormalizado === 'trabajo';
          if (!esSinControlStock) {
            const stockActual = Number(artRow.Existencias) || 0;
            if (stockActual <= 0) {
              return res.status(400).json({
                error: `El artículo "${artRow.Descripcion || it.Clave}" está agotado (0 existencias). No es posible continuar con la venta.`
              });
            }
            if (it.Cantidad > stockActual) {
              return res.status(400).json({
                error: `Existencias insuficientes para "${artRow.Descripcion || it.Clave}". Disponibles: ${stockActual}, Solicitados: ${it.Cantidad}.`
              });
            }
          }
        }
      }

      // Iniciar transacción de cobro
      const tx = db.transaction(() => {
        // 1. Guardar cabecera de venta
        db.prepare(`
          INSERT INTO vent_venta (Folio, Cliente, Total, Fecha, Hora, Usuario, Pagoef, Resta, Pagotcr, Pagotran, Descuento, Adeudoold, Cancel)
          VALUES (?, ?, ?, date('now', 'localtime'), time('now', 'localtime'), ?, ?, ?, ?, ?, ?, ?, 0)
        `).run(folio, clienteId, total, usuario, pagoEfectivo, resta, pagoTarjeta, pagoTransfer, descuento, adeudoPrevio);

        // Guardar detalle del cliente en vent_clientes
        if (clienteId > 0) {
          const clnt = db.prepare('SELECT Nombre, Direccion, Telcel, Telcasa, RFC, mail FROM clnt_client WHERE Id = ?').get(clienteId) as any;
          if (clnt) {
            db.prepare(`
              INSERT INTO vent_clientes (Folio, Nombre, Direccion, telcel, telcasa, RFC, mail)
              VALUES (?, ?, ?, ?, ?, ?, ?)
            `).run(folio, clnt.Nombre || '', clnt.Direccion || '', clnt.Telcel || '', clnt.Telcasa || '', clnt.RFC || '', clnt.mail || '');
          }
        }

        // 2. Guardar líneas de venta (vent_articulos) y descontar existencias
        for (const it of items) {
          // Consultar el tipo de artículo en la base de datos
          const artRow = db.prepare('SELECT Tipo, Existencias FROM art_articulos WHERE Clave = ?').get(it.Clave) as { Tipo?: string; Existencias?: number } | undefined;
          const tipoNormalizado = (artRow?.Tipo || (it as any).Tipo || '').toLowerCase().trim();

          // Artículos de tipo "Servicio", "Infinito" o "Trabajo" NO reducen existencias
          const esSinControlStock = tipoNormalizado === 'servicio' || tipoNormalizado === 'infinito' || tipoNormalizado === 'trabajo';

          db.prepare(`
            INSERT INTO vent_articulos (Folio, Clave, Descripcion, Tp, Cantidad, Importe, Paquete)
            VALUES (?, ?, ?, ?, ?, ?, ?)
          `).run(folio, it.Clave, it.Descripcion, esSinControlStock ? (artRow?.Tipo || 'Servicio') : 'Articulo', it.Cantidad, it.Cantidad * it.Precio, it.Paquete || '');

          // Descontar inventario únicamente si es artículo físico inventariable (nunca permitir negativos, reset a 0)
          if (!esSinControlStock) {
            db.prepare(`
              UPDATE art_articulos 
              SET Existencias = MAX(0, MAX(0, Existencias) - ?) 
              WHERE Clave = ?
            `).run(it.Cantidad, it.Clave);
          }
        }

        // Registrar nota en el libro diario de ventas (vent_lv con nota = 1)
        let nombreCliente = '1:Mostrador';
        if (clienteId > 0) {
          const clnt = db.prepare('SELECT Nombre FROM clnt_client WHERE Id = ?').get(clienteId) as any;
          if (clnt?.Nombre) nombreCliente = clnt.Nombre;
        }
        db.prepare(`
          INSERT INTO vent_lv (
            Clave, Descripcion, Total, usuario, fecha, hora, horast, cantidad, Cliente, Pagoefe, Pagotrans, Pagotar, Descuento, Pagado, cancel, nota
          ) VALUES (
            ?, ?, ?, ?, datetime('now', 'localtime'), time('now', 'localtime'), time('now', 'localtime'), 1, ?, ?, ?, ?, ?, ?, 0, 1
          )
        `).run(
          folio,
          `Nota de venta a ${nombreCliente}`,
          total,
          usuario,
          nombreCliente,
          pagoEfectivo,
          pagoTransfer,
          pagoTarjeta,
          descuento,
          totalPagado
        );

        // 3. Liquidación o actualización de Adeudo del cliente
        if (clienteId > 0) {
          if (totalPagado >= total) {
            // Cubrió la venta actual; el excedente abona al adeudo anterior
            const sobranteParaAdeudo = totalPagado - total;
            const abonoAdeudo = Math.min(adeudoPrevio, sobranteParaAdeudo);

            nuevoAdeudo = Math.max(0, adeudoPrevio - abonoAdeudo);
            db.prepare(`
              UPDATE clnt_client 
              SET Adeudo = ?, ultcompra = date('now', 'localtime')
              WHERE Id = ?
            `).run(nuevoAdeudo, clienteId);

            if (abonoAdeudo > 0) {
              db.prepare(`
                INSERT INTO clnt_movimientos (Id, Detalle, Usuario, Fecha, Hora, Pago, Total, Adeudo)
                VALUES (?, ?, ?, date('now', 'localtime'), time('now', 'localtime'), ?, ?, ?)
              `).run(clienteId, `Abono adeudo venta ${folio}`, usuario, abonoAdeudo, 0, nuevoAdeudo);
            }
          } else {
            // No cubrió la venta completa: la resta se suma al adeudo existente
            nuevoAdeudo = adeudoPrevio + resta;
            db.prepare(`
              UPDATE clnt_client 
              SET Adeudo = ?, ultcompra = date('now', 'localtime')
              WHERE Id = ?
            `).run(nuevoAdeudo, clienteId);

            db.prepare(`
              INSERT INTO clnt_movimientos (Id, Detalle, Usuario, Fecha, Hora, Pago, Total, Adeudo)
              VALUES (?, ?, ?, date('now', 'localtime'), time('now', 'localtime'), ?, ?, ?)
            `).run(clienteId, `Venta crédito ${folio}`, usuario, totalPagado, total, nuevoAdeudo);
          }
        }
      });

      tx();

      // Emitir delta de sincronización para réplica
      syncEngine.emitDelta('vent_venta', 'INSERT', 'Folio', folio, {
        Folio: folio,
        Cliente: clienteId,
        Total: total,
        Usuario: usuario
      });

      res.status(201).json({
        success: true,
        folio,
        subtotal: total,
        adeudoPrevio,
        total: totalConAdeudo,
        resta,
        nuevoAdeudo,
        cambio: cambioFinal
      });
    } catch (err: any) {
      console.error('[VENTAS ERROR]', err);
      res.status(500).json({ error: err.message });
    }
  },

  ultimasVentas: (req: Request, res: Response) => {
    try {
      const rows = db.prepare(`
        SELECT Folio, Cliente, Total, Fecha, Hora, Usuario, Pagoef, Pagotcr, Resta 
        FROM vent_venta 
        ORDER BY Fecha DESC, Hora DESC 
        LIMIT 50
      `).all();
      res.json(rows);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  crearVentaRapida: (req: Request, res: Response) => {
    try {
      const {
        clave,
        cantidad = 1,
        precio,
        pagoEfectivo = 0,
        pagoTarjeta = 0,
        pagoTransfer = 0,
        cliente = '1:Mostrador',
        usuario = 'ADMIN'
      } = req.body as {
        clave: string;
        cantidad: number;
        precio?: number;
        pagoEfectivo?: number;
        pagoTarjeta?: number;
        pagoTransfer?: number;
        cliente?: string;
        usuario?: string;
      };

      if (!clave) {
        return res.status(400).json({ error: 'Se requiere la clave del artículo' });
      }

      if (cantidad <= 0) {
        return res.status(400).json({ error: 'La cantidad debe ser mayor a 0' });
      }

      const art = db.prepare('SELECT Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, Tipo FROM art_articulos WHERE Clave = ?').get(clave) as any;
      if (!art) {
        return res.status(404).json({ error: `Artículo con clave ${clave} no encontrado` });
      }

      const precioUnitario = typeof precio === 'number' && precio >= 0 ? precio : (art.Menudeo || 0);
      const total = Math.round(cantidad * precioUnitario * 100) / 100;

      let pe = Number(pagoEfectivo) || 0;
      let pt = Number(pagoTarjeta) || 0;
      let ptr = Number(pagoTransfer) || 0;
      const totalPagado = pe + pt + ptr;

      // Si no especificaron desglose pero confirman la venta, por defecto se asume efectivo exacto
      if (totalPagado === 0) {
        pe = total;
      }

      let cambio = 0;
      if (totalPagado > total && pe > 0) {
        const excedente = totalPagado - total;
        cambio = Math.min(pe, excedente);
        pe = Math.max(0, pe - cambio); // Monto que realmente ingresa en caja
      }

      // Validar existencias para venta rápida
      const tipoNormalizado = (art.Tipo || '').toLowerCase().trim();
      const esSinControlStock = tipoNormalizado === 'servicio' || tipoNormalizado === 'infinito' || tipoNormalizado === 'trabajo';

      if (!esSinControlStock) {
        const stockActual = Number(art.Existencias) || 0;
        if (stockActual <= 0) {
          return res.status(400).json({
            error: `El artículo "${art.Descripcion || clave}" está agotado (0 existencias). No es posible continuar con la venta.`
          });
        }
        if (cantidad > stockActual) {
          return res.status(400).json({
            error: `Existencias insuficientes para "${art.Descripcion || clave}". Disponibles: ${stockActual}, Solicitados: ${cantidad}.`
          });
        }
      }

      let nuevaExistencia = art.Existencias;

      const tx = db.transaction(() => {
        // 1. Guardar directo en vent_lv SIN generar nota (nota = 0)
        db.prepare(`
          INSERT INTO vent_lv (
            Clave, Descripcion, Total, usuario, fecha, hora, horast, cantidad, Cliente, Pagoefe, Pagotrans, Pagotar, Descuento, Pagado, cancel, nota
          ) VALUES (
            ?, ?, ?, ?, datetime('now', 'localtime'), time('now', 'localtime'), time('now', 'localtime'), ?, ?, ?, ?, ?, 0, ?, 0, 0
          )
        `).run(
          art.Clave,
          art.Descripcion,
          total,
          usuario,
          cantidad,
          cliente,
          pe,
          ptr,
          pt,
          total
        );

        // 2. Descontar existencias únicamente si es un artículo físico inventariable
        const tipoNormalizado = (art.Tipo || '').toLowerCase().trim();
        const esSinControlStock = tipoNormalizado === 'servicio' || tipoNormalizado === 'infinito' || tipoNormalizado === 'trabajo';

        if (!esSinControlStock) {
          db.prepare(`
            UPDATE art_articulos 
            SET Existencias = MAX(0, MAX(0, Existencias) - ?) 
            WHERE Clave = ?
          `).run(cantidad, art.Clave);

          nuevaExistencia = Math.max(0, Math.max(0, Number(art.Existencias) || 0) - cantidad);
        } else {
          nuevaExistencia = Math.max(0, Number(art.Existencias) || 0);
        }
      });

      tx();

      // Emitir sincronización
      syncEngine.emitDelta('vent_lv', 'INSERT', 'Clave', art.Clave, {
        Clave: art.Clave,
        Descripcion: art.Descripcion,
        Total: total,
        Cantidad: cantidad,
        Fecha: new Date().toISOString()
      });

      syncEngine.emitDelta('art_articulos', 'UPDATE', 'Clave', art.Clave, {
        Clave: art.Clave,
        Existencias: nuevaExistencia
      });

      res.status(201).json({
        success: true,
        message: 'Venta rápida completada con éxito',
        clave: art.Clave,
        descripcion: art.Descripcion,
        cantidad,
        precioUnitario,
        total,
        pagoEfectivo: pe,
        pagoTarjeta: pt,
        pagoTransfer: ptr,
        cambio,
        existenciasRestantes: nuevaExistencia
      });
    } catch (err: any) {
      console.error('[VENTA RAPIDA ERROR]', err);
      res.status(500).json({ error: err.message });
    }
  },

  ventasDelDia: (req: Request, res: Response) => {
    try {
      const fecha = (req.query.fecha as string) || new Date().toLocaleDateString('en-CA');
      const rows = db.prepare(`
        SELECT rowid, Clave, Descripcion, Total, usuario, fecha, hora, horast, cantidad, Cliente, Pagoefe, Pagotrans, Pagotar, Descuento, Pagado, cancel, nota
        FROM vent_lv
        WHERE substr(fecha, 1, 10) = ? OR fecha LIKE ?
        ORDER BY rowid DESC
      `).all(fecha, `${fecha}%`) as any[];

      const totalVentas = rows.reduce((s, r) => s + (r.cancel === 0 ? Number(r.Total || r.Pagado || 0) : 0), 0);
      const totalEfectivo = rows.reduce((s, r) => s + (r.cancel === 0 ? Number(r.Pagoefe || 0) : 0), 0);
      const totalTarjeta = rows.reduce((s, r) => s + (r.cancel === 0 ? Number(r.Pagotar || 0) : 0), 0);
      const totalTransfer = rows.reduce((s, r) => s + (r.cancel === 0 ? Number(r.Pagotrans || 0) : 0), 0);

      res.json({
        fecha,
        totalVentas,
        totalEfectivo,
        totalTarjeta,
        totalTransfer,
        conteo: rows.length,
        movimientos: rows
      });
    } catch (err: any) {
      console.error('[VENTAS DEL DIA ERROR]', err);
      res.status(500).json({ error: err.message });
    }
  },

  obtenerDetalleVenta: (req: Request, res: Response) => {
    try {
      const { folio } = req.params;
      const venta = db.prepare('SELECT * FROM vent_venta WHERE Folio = ?').get(folio) as any;
      if (!venta) return res.status(404).json({ error: 'Venta no encontrada' });

      const cliente = db.prepare('SELECT * FROM vent_clientes WHERE Folio = ?').get(folio) as any;
      const articulos = db.prepare('SELECT * FROM vent_articulos WHERE Folio = ?').all(folio) as any[];

      res.json({
        folio: folio || venta.Folio,
        venta,
        cliente: cliente || { Nombre: 'Mostrador' },
        articulos: articulos.map(a => ({
          ...a,
          Precio: a.Cantidad > 0 ? (a.Importe / a.Cantidad) : a.Importe
        }))
      });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  }
};
