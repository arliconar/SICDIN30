import { Request, Response } from 'express';
import db from '../db';
import { syncEngine } from '../sync/SyncEngine';

export const ArticulosController = {
  // Búsqueda ágil: código de barras exacto, clave y descripción
  buscar: (req: Request, res: Response) => {
    try {
      const q = (req.query.q as string || '').trim();
      if (!q) {
        const rows = db.prepare(`
          SELECT Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, cbarra, Paquete 
          FROM art_articulos 
          LIMIT 50
        `).all();
        return res.json(rows);
      }

      // 1. PRIORIDAD 1: Si es un número/código de barras, buscar coincidencia EXACTA en cbarra
      // Los códigos de barras son secuencias numéricas (EAN-13, UPC, etc.)
      const esCodigoNumerico = /^\d+$/.test(q);
      if (esCodigoNumerico) {
        const porBarraExacta = db.prepare(`
          SELECT Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, cbarra, Paquete 
          FROM art_articulos 
          WHERE cbarra = ?
        `).get(q);

        if (porBarraExacta) {
          return res.json([porBarraExacta]);
        }
      }

      // 2. PRIORIDAD 2: Coincidencia EXACTA por Clave
      const porClaveExacta = db.prepare(`
        SELECT Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, cbarra, Paquete 
        FROM art_articulos 
        WHERE Clave = ?
      `).get(q);

      if (porClaveExacta) {
        return res.json([porClaveExacta]);
      }

      // 3. PRIORIDAD 3: Búsqueda flexible por prefijo de Clave o por palabras en la Descripción
      // Ej: si escribe "AR", busca claves que inicien con "AR%" o descripciones con esa palabra
      const words = q.split(' ').filter(Boolean);
      const descConditions = words.map(() => 'Descripcion LIKE ?').join(' AND ');
      const descParams = words.map(w => `%${w}%`);

      const sql = `
        SELECT Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, cbarra, Paquete 
        FROM art_articulos 
        WHERE (Clave LIKE ?) OR (${descConditions})
        ORDER BY 
          CASE WHEN Clave LIKE ? THEN 0 ELSE 1 END,
          Descripcion ASC
        LIMIT 50
      `;

      const params = [`${q}%`, ...descParams, `${q}%`];
      const results = db.prepare(sql).all(...params);
      res.json(results);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  obtenerPorClave: (req: Request, res: Response) => {
    try {
      const { clave } = req.params;
      const art = db.prepare('SELECT * FROM art_articulos WHERE Clave = ?').get(clave);
      if (!art) return res.status(404).json({ error: 'Artículo no encontrado' });
      res.json(art);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  guardar: (req: Request, res: Response) => {
    try {
      const data = req.body;
      const { Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, cbarra, Tipo } = data;

      if (!Clave || !Descripcion) {
        return res.status(400).json({ error: 'Clave y Descripción son requeridos' });
      }

      const existing = db.prepare('SELECT Clave FROM art_articulos WHERE Clave = ?').get(Clave);
      const isNew = !existing;

      if (isNew) {
        db.prepare(`
          INSERT INTO art_articulos (Clave, Descripcion, Menudeo, Mayoreo, PME, Especial, Existencias, cbarra, Tipo, Fum)
          VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, datetime('now', 'localtime'))
        `).run(Clave, Descripcion, Menudeo || 0, Mayoreo || 0, PME || 0, Especial || 0, Existencias || 0, cbarra || '', Tipo || 'Artículos');

        syncEngine.emitDelta('art_articulos', 'INSERT', 'Clave', Clave, data);
      } else {
        db.prepare(`
          UPDATE art_articulos 
          SET Descripcion = ?, Menudeo = ?, Mayoreo = ?, PME = ?, Especial = ?, Existencias = ?, cbarra = ?, Tipo = ?, Fum = datetime('now', 'localtime')
          WHERE Clave = ?
        `).run(Descripcion, Menudeo || 0, Mayoreo || 0, PME || 0, Especial || 0, Existencias || 0, cbarra || '', Tipo || 'Artículos', Clave);

        syncEngine.emitDelta('art_articulos', 'UPDATE', 'Clave', Clave, data);
      }

      res.json({ success: true, clave: Clave });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  modificarExistencias: (req: Request, res: Response) => {
    try {
      const { clave } = req.params;
      const { cambio, tipo, motivo } = req.body as {
        cambio: number; // cantidad a sumar o restar, o valor absoluto
        tipo: 'AJUSTE' | 'ABSOLUTO'; // AJUSTE: suma/resta; ABSOLUTO: establece el número directamente
        motivo?: string;
      };

      const art = db.prepare('SELECT * FROM art_articulos WHERE Clave = ?').get(clave) as any;
      if (!art) {
        return res.status(404).json({ error: 'Artículo no encontrado' });
      }

      let nuevaExistencia = art.Existencias || 0;
      if (tipo === 'ABSOLUTO') {
        nuevaExistencia = Number(cambio) || 0;
      } else {
        nuevaExistencia = (art.Existencias || 0) + (Number(cambio) || 0);
      }

      if (nuevaExistencia < 0) {
        nuevaExistencia = 0;
      }

      db.prepare(`
        UPDATE art_articulos 
        SET Existencias = ?, Fum = datetime('now', 'localtime')
        WHERE Clave = ?
      `).run(nuevaExistencia, clave);

      // Emitir delta de sincronización para réplica en las demás tiendas
      syncEngine.emitDelta('art_articulos', 'UPDATE', 'Clave', clave, {
        Clave: clave,
        Existencias: nuevaExistencia
      });

      res.json({
        success: true,
        clave,
        existenciaAnterior: art.Existencias || 0,
        existencias: nuevaExistencia
      });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  }
};
