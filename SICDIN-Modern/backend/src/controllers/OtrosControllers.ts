import { Request, Response } from 'express';
import db from '../db';
import { syncEngine } from '../sync/SyncEngine';

export const ClientesController = {
  listar: (req: Request, res: Response) => {
    try {
      const q = (req.query.q as string || '').trim();
      let rows;
      if (q) {
        rows = db.prepare(`
          SELECT Id, Nombre, Direccion, Adeudo, Telcel, Telcasa, mail, RFC, Mayoreo, Especial, Credito, ultcompra 
          FROM clnt_client 
          WHERE Nombre LIKE ? OR Id = ? OR Telcel LIKE ? OR RFC LIKE ?
          ORDER BY Nombre ASC LIMIT 100
        `).all(`%${q}%`, q, `%${q}%`, `%${q}%`);
      } else {
        rows = db.prepare(`
          SELECT Id, Nombre, Direccion, Adeudo, Telcel, Telcasa, mail, RFC, Mayoreo, Especial, Credito, ultcompra 
          FROM clnt_client 
          ORDER BY Nombre ASC LIMIT 200
        `).all();
      }
      res.json(rows);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  obtenerDetalle: (req: Request, res: Response) => {
    try {
      const { id } = req.params;
      const cliente = db.prepare('SELECT * FROM clnt_client WHERE Id = ?').get(id);
      if (!cliente) return res.status(404).json({ error: 'Cliente no encontrado' });

      // Movimientos de adeudo / abonos
      const movimientos = db.prepare('SELECT * FROM clnt_movimientos WHERE Id = ? ORDER BY Fecha DESC, Hora DESC LIMIT 50').all(id);

      // Historial de compras / ventas realizadas a este cliente
      const ventas = db.prepare(`
        SELECT Folio, Total, Fecha, Hora, Pagoef, Pagotcr, Pagotran, Resta, Usuario
        FROM vent_venta 
        WHERE Cliente = ? OR Cliente LIKE ? 
        ORDER BY Fecha DESC, Hora DESC LIMIT 50
      `).all(id, `${id}:%`);

      res.json({ cliente, movimientos, ventas });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  crear: (req: Request, res: Response) => {
    try {
      const {
        Nombre,
        Direccion = '',
        Mayoreo = 0,
        Credito = 0,
        Adeudo = 0,
        Especial = 0,
        RFC = '',
        Telcasa = '',
        Telcel = '',
        mail = ''
      } = req.body;

      if (!Nombre || !Nombre.trim()) {
        return res.status(400).json({ error: 'El nombre del cliente es obligatorio' });
      }

      // Obtener el siguiente ID consecutivo
      const maxIdRow = db.prepare('SELECT MAX(Id) as maxId FROM clnt_client').get() as { maxId?: number };
      const nuevoId = (maxIdRow?.maxId || 0) + 1;

      db.prepare(`
        INSERT INTO clnt_client (Id, Nombre, Direccion, Mayoreo, Credito, Adeudo, Especial, RFC, Telcasa, Telcel, mail, ultmod)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, datetime('now', 'localtime'))
      `).run(
        nuevoId,
        Nombre.trim(),
        Direccion.trim(),
        Mayoreo ? 1 : 0,
        Number(Credito) || 0,
        Number(Adeudo) || 0,
        Especial ? 1 : 0,
        RFC.trim(),
        Telcasa.trim(),
        Telcel.trim(),
        mail.trim()
      );

      const clienteCreado = db.prepare('SELECT * FROM clnt_client WHERE Id = ?').get(nuevoId) as Record<string, any>;

      syncEngine.emitDelta('clnt_client', 'INSERT', 'Id', nuevoId, clienteCreado);

      res.status(201).json({ success: true, cliente: clienteCreado });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  actualizar: (req: Request, res: Response) => {
    try {
      const { id } = req.params;
      const {
        Nombre,
        Direccion = '',
        Mayoreo = 0,
        Credito = 0,
        Adeudo = 0,
        Especial = 0,
        RFC = '',
        Telcasa = '',
        Telcel = '',
        mail = ''
      } = req.body;

      if (!Nombre || !Nombre.trim()) {
        return res.status(400).json({ error: 'El nombre del cliente es obligatorio' });
      }

      const existing = db.prepare('SELECT Id FROM clnt_client WHERE Id = ?').get(id);
      if (!existing) return res.status(404).json({ error: 'Cliente no encontrado' });

      db.prepare(`
        UPDATE clnt_client 
        SET Nombre = ?, Direccion = ?, Mayoreo = ?, Credito = ?, Adeudo = ?, Especial = ?, RFC = ?, Telcasa = ?, Telcel = ?, mail = ?, ultmod = datetime('now', 'localtime')
        WHERE Id = ?
      `).run(
        Nombre.trim(),
        Direccion.trim(),
        Mayoreo ? 1 : 0,
        Number(Credito) || 0,
        Number(Adeudo) || 0,
        Especial ? 1 : 0,
        RFC.trim(),
        Telcasa.trim(),
        Telcel.trim(),
        mail.trim(),
        id
      );

      const clienteActualizado = db.prepare('SELECT * FROM clnt_client WHERE Id = ?').get(id) as Record<string, any>;

      syncEngine.emitDelta('clnt_client', 'UPDATE', 'Id', id, clienteActualizado);

      res.json({ success: true, cliente: clienteActualizado });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  eliminar: (req: Request, res: Response) => {
    try {
      const { id } = req.params;

      if (Number(id) === 1) {
        return res.status(400).json({ error: 'No es posible eliminar el cliente de Mostrador (Id 1)' });
      }

      const existing = db.prepare('SELECT Id, Nombre, Adeudo FROM clnt_client WHERE Id = ?').get(id) as { Id: number; Nombre: string; Adeudo: number } | undefined;
      if (!existing) return res.status(404).json({ error: 'Cliente no encontrado' });

      if (existing.Adeudo > 0) {
        return res.status(400).json({ error: `El cliente ${existing.Nombre} tiene un saldo deudor pendiente de $${existing.Adeudo.toFixed(2)}. Debe saldarse antes de eliminar.` });
      }

      db.prepare('DELETE FROM clnt_client WHERE Id = ?').run(id);

      syncEngine.emitDelta('clnt_client', 'DELETE', 'Id', id, { Id: id });

      res.json({ success: true, message: `Cliente ${existing.Nombre} eliminado con éxito` });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  }
};

export const ReparacionesController = {
  listarActivas: (req: Request, res: Response) => {
    try {
      const rows = db.prepare(`
        SELECT Folio, Id, Marca, Color, Rodada, Nota, Entregada, Finalizada 
        FROM rep_bici 
        WHERE Entregada = 0 AND Cancelada = 0 
        ORDER BY Folio DESC LIMIT 50
      `).all();
      res.json(rows);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  }
};
