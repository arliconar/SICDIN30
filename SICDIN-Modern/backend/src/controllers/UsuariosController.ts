import { Request, Response } from 'express';
import path from 'path';
import fs from 'fs';
import db from '../db';
import { syncEngine } from '../sync/SyncEngine';

export const UsuariosController = {
  login: (req: Request, res: Response) => {
    try {
      const { id, nombre, pass } = req.body;
      if ((!id && !nombre) || pass === undefined) {
        return res.status(400).json({ error: 'Usuario y contraseña requeridos' });
      }

      let user: any;
      if (id) {
        user = db.prepare('SELECT Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto FROM usu_usuario WHERE Id = ?').get(id);
      } else {
        user = db.prepare('SELECT Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto FROM usu_usuario WHERE LOWER(Nombre) = LOWER(?)').get(nombre.trim());
      }

      if (!user) {
        return res.status(404).json({ error: 'Usuario no encontrado' });
      }

      if (user.activo !== 1) {
        return res.status(403).json({ error: 'Este usuario se encuentra inhabilitado' });
      }

      if (String(user.Pass).trim() !== String(pass).trim()) {
        return res.status(401).json({ error: 'Contraseña incorrecta' });
      }

      // Actualizar último acceso (Fu)
      db.prepare("UPDATE usu_usuario SET Fu = datetime('now', 'localtime') WHERE Id = ?").run(user.Id);
      const usuarioActualizado = db.prepare('SELECT Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto FROM usu_usuario WHERE Id = ?').get(user.Id);

      res.json({
        success: true,
        usuario: usuarioActualizado,
        message: `Bienvenido ${user.Nombre}`
      });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  listar: (req: Request, res: Response) => {
    try {
      const q = (req.query.q as string || '').trim();
      let rows;
      if (q) {
        rows = db.prepare(`
          SELECT Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto
          FROM usu_usuario
          WHERE Nombre LIKE ? OR Id = ? OR telefono LIKE ?
          ORDER BY Id ASC
        `).all(`%${q}%`, q, `%${q}%`);
      } else {
        rows = db.prepare(`
          SELECT Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto
          FROM usu_usuario
          ORDER BY Id ASC
        `).all();
      }
      res.json(rows);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  obtener: (req: Request, res: Response) => {
    try {
      const { id } = req.params;
      const user = db.prepare('SELECT Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto FROM usu_usuario WHERE Id = ?').get(id);
      if (!user) return res.status(404).json({ error: 'Usuario no encontrado' });
      res.json(user);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  crear: (req: Request, res: Response) => {
    try {
      const { Nombre, Pass, Admin = 0, telefono = '', activo = 1, Foto = 'Hombre' } = req.body;

      if (!Nombre || !Nombre.trim()) {
        return res.status(400).json({ error: 'El nombre de usuario es obligatorio' });
      }
      if (!Pass || !Pass.trim()) {
        return res.status(400).json({ error: 'La contraseña es obligatoria' });
      }

      // Validar si ya existe el nombre
      const existente = db.prepare('SELECT Id FROM usu_usuario WHERE LOWER(Nombre) = LOWER(?)').get(Nombre.trim());
      if (existente) {
        return res.status(400).json({ error: 'Ya existe un usuario con este nombre' });
      }

      // Obtener el siguiente Id consecutivo
      const maxIdRow = db.prepare('SELECT MAX(Id) as maxId FROM usu_usuario').get() as { maxId?: number };
      const nuevoId = (maxIdRow?.maxId || 0) + 1;

      db.prepare(`
        INSERT INTO usu_usuario (Id, Nombre, Pass, Fu, Admin, telefono, activo, Foto)
        VALUES (?, ?, ?, datetime('now', 'localtime'), ?, ?, ?, ?)
      `).run(
        nuevoId,
        Nombre.trim(),
        Pass.trim(),
        Admin ? 1 : 0,
        telefono ? telefono.trim() : '',
        activo ? 1 : 0,
        Foto ? Foto.trim() : 'Hombre'
      );

      const usuarioCreado = db.prepare('SELECT * FROM usu_usuario WHERE Id = ?').get(nuevoId) as Record<string, any>;
      syncEngine.emitDelta('usu_usuario', 'INSERT', 'Id', nuevoId, usuarioCreado);

      res.status(201).json({ success: true, usuario: usuarioCreado });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  actualizar: (req: Request, res: Response) => {
    try {
      const { id } = req.params;
      const { Nombre, Pass, Admin, telefono, activo, Foto } = req.body;

      const user = db.prepare('SELECT * FROM usu_usuario WHERE Id = ?').get(id) as Record<string, any>;
      if (!user) return res.status(404).json({ error: 'Usuario no encontrado' });

      // Validar nombre único si se cambió
      if (Nombre && Nombre.trim().toLowerCase() !== user.Nombre.toLowerCase()) {
        const existente = db.prepare('SELECT Id FROM usu_usuario WHERE LOWER(Nombre) = LOWER(?) AND Id != ?').get(Nombre.trim(), id);
        if (existente) {
          return res.status(400).json({ error: 'Ya existe otro usuario con este nombre' });
        }
      }

      const nuevoNombre = Nombre !== undefined ? Nombre.trim() : user.Nombre;
      const nuevoPass = Pass !== undefined && Pass.trim() !== '' ? Pass.trim() : user.Pass;
      const nuevoAdmin = Admin !== undefined ? (Admin ? 1 : 0) : user.Admin;
      const nuevoTel = telefono !== undefined ? telefono.trim() : (user.telefono || '');
      const nuevoActivo = activo !== undefined ? (activo ? 1 : 0) : user.activo;
      const nuevaFoto = Foto !== undefined ? Foto.trim() : (user.Foto || 'Hombre');

      db.prepare(`
        UPDATE usu_usuario
        SET Nombre = ?, Pass = ?, Admin = ?, telefono = ?, activo = ?, Foto = ?, Fu = datetime('now', 'localtime')
        WHERE Id = ?
      `).run(
        nuevoNombre,
        nuevoPass,
        nuevoAdmin,
        nuevoTel,
        nuevoActivo,
        nuevaFoto,
        id
      );

      const usuarioActualizado = db.prepare('SELECT * FROM usu_usuario WHERE Id = ?').get(id) as Record<string, any>;
      syncEngine.emitDelta('usu_usuario', 'UPDATE', 'Id', id, usuarioActualizado);

      res.json({ success: true, usuario: usuarioActualizado });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  eliminar: (req: Request, res: Response) => {
    try {
      const { id } = req.params;
      const user = db.prepare('SELECT * FROM usu_usuario WHERE Id = ?').get(id) as Record<string, any>;
      if (!user) return res.status(404).json({ error: 'Usuario no encontrado' });

      // Si es el único admin, impedir eliminarlo
      if (user.Admin === 1) {
        const totalAdmins = db.prepare('SELECT COUNT(*) as count FROM usu_usuario WHERE Admin = 1').get() as { count: number };
        if (totalAdmins.count <= 1) {
          return res.status(400).json({ error: 'No se puede eliminar el único usuario Administrador del sistema' });
        }
      }

      db.prepare('DELETE FROM usu_usuario WHERE Id = ?').run(id);
      syncEngine.emitDelta('usu_usuario', 'DELETE', 'Id', id, { Id: id });

      res.json({ success: true, message: `Usuario ${user.Nombre} eliminado con éxito` });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  subirFoto: (req: Request, res: Response) => {
    try {
      const { imagenBase64, userId } = req.body;
      if (!imagenBase64) {
        return res.status(400).json({ error: 'No se envió ninguna imagen' });
      }

      // Extraer datos base64 y tipo de mime
      const matches = imagenBase64.match(/^data:([A-Za-z-+\/]+);base64,(.+)$/);
      let buffer: Buffer;
      let ext = 'png';

      if (matches && matches.length === 3) {
        const mime = matches[1];
        if (mime.includes('jpeg') || mime.includes('jpg')) ext = 'jpg';
        else if (mime.includes('webp')) ext = 'webp';
        else if (mime.includes('gif')) ext = 'gif';
        buffer = Buffer.from(matches[2], 'base64');
      } else {
        buffer = Buffer.from(imagenBase64, 'base64');
      }

      const uploadsDir = path.resolve(__dirname, '../../uploads/usuarios');
      if (!fs.existsSync(uploadsDir)) {
        fs.mkdirSync(uploadsDir, { recursive: true });
      }

      const safeName = (userId ? `user_${userId}` : `avatar_${Date.now()}`) + `.${ext}`;
      const destPath = path.join(uploadsDir, safeName);
      fs.writeFileSync(destPath, buffer);

      const urlRelativa = `/uploads/usuarios/${safeName}`;

      // Si se proporcionó userId, actualizar directamente en BD
      if (userId) {
        db.prepare("UPDATE usu_usuario SET Foto = ?, Fu = datetime('now', 'localtime') WHERE Id = ?").run(urlRelativa, userId);
      }

      res.json({
        success: true,
        url: urlRelativa,
        rutaLocal: destPath,
        nombreArchivo: safeName
      });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  },

  obtenerFotoArchivo: (req: Request, res: Response) => {
    try {
      const ruta = (req.query.path as string || '').trim();
      if (!ruta) return res.status(400).json({ error: 'Ruta no especificada' });

      // Si es una ruta absoluta en Windows (ej. C:\sc3\angel.png)
      if (fs.existsSync(ruta)) {
        return res.sendFile(path.resolve(ruta));
      }

      // Si es una ruta relativa de uploads (/uploads/usuarios/...)
      const cleanRel = ruta.replace(/^\//, '');
      const relativeUploadPath = path.resolve(__dirname, '../../', cleanRel);
      if (fs.existsSync(relativeUploadPath)) {
        return res.sendFile(relativeUploadPath);
      }

      // Si existe en SICDIN30/Resources
      const resourcePath = path.resolve(__dirname, '../../../SICDIN30/Resources', ruta);
      if (fs.existsSync(resourcePath)) {
        return res.sendFile(resourcePath);
      }

      res.status(404).json({ error: 'Imagen no encontrada' });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  }
};
