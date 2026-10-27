import express from 'express';
import cors from 'cors';
import dotenv from 'dotenv';
import path from 'path';
import fs from 'fs';
import apiRoutes from './routes/api';
import { whatsAppService } from './services/WhatsAppService';

dotenv.config();

const app = express();
const PORT = process.env.PORT || 4000;

// Asegurar carpeta local de uploads para fotos de usuarios
const uploadsDir = path.resolve(__dirname, '../uploads/usuarios');
if (!fs.existsSync(uploadsDir)) {
  fs.mkdirSync(uploadsDir, { recursive: true });
}

app.use(cors());
app.use(express.json({ limit: '25mb' }));
app.use(express.urlencoded({ extended: true, limit: '25mb' }));

// Servir archivos estáticos de uploads
app.use('/uploads', express.static(path.resolve(__dirname, '../uploads')));

// API REST Endpoints
app.use('/api', apiRoutes);

// Healthcheck
app.get('/health', (req, res) => {
  res.json({ status: 'ok', name: 'SICDIN-Modern Backend', timestamp: new Date().toISOString() });
});

app.listen(Number(PORT), '0.0.0.0', () => {
  console.log(`[SICDIN-Modern] Servidor backend escuchando en http://0.0.0.0:${PORT}`);
  // Iniciar conexión de WhatsApp en segundo plano
  whatsAppService.init().catch(err => {
    console.error('[SICDIN-Modern] Error iniciando servicio de WhatsApp:', err);
  });
});
