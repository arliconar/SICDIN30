import { Request, Response } from 'express';
import { whatsAppService } from '../services/WhatsAppService';

export class WhatsAppController {
  static getStatus = (req: Request, res: Response) => {
    try {
      const status = whatsAppService.getStatus();
      res.json(status);
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  };

  static enviarComprobante = async (req: Request, res: Response) => {
    try {
      const { telefono, imagenBase64, mensaje, folio } = req.body;

      if (!telefono || !imagenBase64) {
        return res.status(400).json({
          error: 'Faltan parámetros requeridos: telefono e imagenBase64 son obligatorios.'
        });
      }

      const resultado = await whatsAppService.enviarImagen({
        telefono,
        imagenBase64,
        mensaje,
        folio
      });

      res.json(resultado);
    } catch (err: any) {
      console.error('[WhatsAppController] Error enviando comprobante:', err.message);
      res.status(500).json({ error: err.message || 'Error al enviar comprobante por WhatsApp.' });
    }
  };

  static logout = async (req: Request, res: Response) => {
    try {
      await whatsAppService.logout();
      res.json({ success: true, message: 'Sesión de WhatsApp cerrada correctamente.' });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  };

  static restart = async (req: Request, res: Response) => {
    try {
      await whatsAppService.init();
      res.json({ success: true, message: 'Servicio de WhatsApp reiniciado.' });
    } catch (err: any) {
      res.status(500).json({ error: err.message });
    }
  };
}
