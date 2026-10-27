import path from 'path';
import fs from 'fs';
import pino from 'pino';
import QRCode from 'qrcode';
import NodeCache from 'node-cache';

const logFile = path.join(__dirname, '../../data/whatsapp.log');
const msgRetryCounterCache = new NodeCache();
function logWhatsApp(...args: any[]) {
  const line = `[${new Date().toISOString()}] ${args.map(a => typeof a === 'object' ? JSON.stringify(a) : a).join(' ')}\n`;
  try {
    fs.appendFileSync(logFile, line);
  } catch {}
  console.log(...args);
}

type WASocket = any;
type ConnectionState = any;

export type WhatsAppStatus = 'DISCONNECTED' | 'CONNECTING' | 'QR_READY' | 'CONNECTED';

export interface WhatsAppStatusInfo {
  status: WhatsAppStatus;
  qrCode: string | null;
  phoneNumber: string | null;
  name: string | null;
  ultimoError: string | null;
}

class WhatsAppService {
  private sock: WASocket | null = null;
  private status: WhatsAppStatus = 'DISCONNECTED';
  private qrCodeDataUrl: string | null = null;
  private phoneNumber: string | null = null;
  private userName: string | null = null;
  private ultimoError: string | null = null;
  private reconnectAttempts = 0;
  private authDir: string;
  private isInitializing = false;
  private messageStore = new Map<string, any>();

  constructor() {
    this.authDir = path.join(__dirname, '../../data/whatsapp_session');
    if (!fs.existsSync(this.authDir)) {
      fs.mkdirSync(this.authDir, { recursive: true });
    }
  }

  public async init(): Promise<void> {
    if (this.isInitializing) return;
    this.isInitializing = true;
    this.status = 'CONNECTING';
    this.ultimoError = null;

    if (this.sock) {
      try {
        this.sock.ev.removeAllListeners();
        this.sock.end(undefined);
      } catch {}
      this.sock = null;
    }

    try {
      // Importación dinámica de Baileys para compatibilidad ESM en CJS/Node16
      const { default: makeWASocket, useMultiFileAuthState, DisconnectReason, makeCacheableSignalKeyStore } = await import('@whiskeysockets/baileys');

      const { state, saveCreds } = await useMultiFileAuthState(this.authDir);

      const pinoLogger = pino({ level: 'silent' });

      this.sock = makeWASocket({
        auth: {
          creds: state.creds,
          keys: makeCacheableSignalKeyStore(state.keys, pinoLogger),
        },
        logger: pinoLogger,
        printQRInTerminal: false,
        browser: ['SICDIN 3.0', 'Desktop', '3.0.0'],
        msgRetryCounterCache,
        syncFullHistory: false,
        generateHighQualityLinkPreview: true,
        getMessage: async (key: any) => {
          logWhatsApp(`[WhatsApp] getMessage retry solicitado para key:`, key);
          if (key?.id && this.messageStore.has(key.id)) {
            return this.messageStore.get(key.id);
          }
          return undefined;
        }
      });

      this.sock.ev.on('creds.update', saveCreds);

      this.sock.ev.on('messages.upsert', async (m: any) => {
        if (m.messages) {
          for (const msg of m.messages) {
            if (msg.key?.id && msg.message) {
              this.messageStore.set(msg.key.id, msg.message);
            }
          }
        }
      });

      this.sock.ev.on('connection.update', async (update: Partial<ConnectionState>) => {
        const { connection, lastDisconnect, qr } = update;

        if (qr) {
          try {
            this.qrCodeDataUrl = await QRCode.toDataURL(qr, { width: 300, margin: 2 });
            this.status = 'QR_READY';
            logWhatsApp('[WhatsApp] Nuevo código QR listo para escanear');
          } catch (e: any) {
            logWhatsApp('[WhatsApp] Error generando DataURL de QR:', e);
          }
        }

        if (connection === 'open') {
          this.status = 'CONNECTED';
          this.qrCodeDataUrl = null;
          this.reconnectAttempts = 0;

          const id = this.sock?.user?.id || '';
          this.phoneNumber = id.split(':')[0] || id.split('@')[0] || null;
          this.userName = this.sock?.user?.name || 'Casa Licona';
          logWhatsApp(`[WhatsApp] ¡Sesión conectada con éxito! Tel: ${this.phoneNumber} (${this.userName})`);
        }

        if (connection === 'close') {
          const statusCode = (lastDisconnect?.error as any)?.output?.statusCode;
          const isReplaced = statusCode === DisconnectReason.connectionReplaced;
          const isLoggedOut = statusCode === DisconnectReason.loggedOut;
          const shouldReconnect = !isLoggedOut && !isReplaced;
          this.ultimoError = (lastDisconnect?.error as any)?.message || `Desconectado (código ${statusCode})`;

          logWhatsApp(`[WhatsApp] Conexión cerrada. Razón: ${statusCode}. Reconectar: ${shouldReconnect}`);

          if (this.sock) {
            try {
              this.sock.ev.removeAllListeners();
            } catch {}
            this.sock = null;
          }

          if (shouldReconnect) {
            this.status = 'CONNECTING';
            this.reconnectAttempts++;
            const delay = Math.min(10000, 2000 * this.reconnectAttempts);
            setTimeout(() => {
              this.isInitializing = false;
              this.init();
            }, delay);
          } else {
            this.status = 'DISCONNECTED';
            this.phoneNumber = null;
            this.userName = null;
            this.qrCodeDataUrl = null;
            this.isInitializing = false;
            if (isLoggedOut) {
              // Si la sesión se cerró por logout, limpiar y regenerar QR
              this.limpiarSesion();
              this.init();
            }
          }
        }
      });
    } catch (err: any) {
      logWhatsApp('[WhatsApp] Error al inicializar socket:', err);
      this.status = 'DISCONNECTED';
      this.ultimoError = err.message || 'Error desconocido al inicializar';
    } finally {
      this.isInitializing = false;
    }
  }

  public getStatus(): WhatsAppStatusInfo {
    return {
      status: this.status,
      qrCode: this.qrCodeDataUrl,
      phoneNumber: this.phoneNumber,
      name: this.userName,
      ultimoError: this.ultimoError
    };
  }

  public async enviarImagen(params: {
    telefono: string;
    imagenBase64: string;
    mensaje?: string;
    folio?: string;
  }): Promise<{ success: boolean; jid: string; messageId?: string }> {
    if (this.status !== 'CONNECTED' || !this.sock) {
      throw new Error('El servicio de WhatsApp de SICDIN no está conectado. Por favor verifique o escanee el código QR.');
    }

    // Normalizar número telefónico
    let clean = params.telefono.replace(/\D/g, '');
    if (!clean) {
      throw new Error('El número de teléfono es inválido o está vacío.');
    }

    // Candidatos a JID: en México WhatsApp migró de 521 a 52, por lo que 52 va primero
    let jidCandidates: string[] = [];
    if (clean.length === 10) {
      // 10 dígitos (ej. 7711216098): probar 52 primero y luego 521
      jidCandidates = [`52${clean}@s.whatsapp.net`, `521${clean}@s.whatsapp.net`];
    } else if (clean.startsWith('52') && clean.length === 13 && clean[2] === '1') {
      // 521 + 10 dígitos: probar sin el 1 primero
      const sinUno = '52' + clean.slice(3);
      jidCandidates = [`${sinUno}@s.whatsapp.net`, `${clean}@s.whatsapp.net`];
    } else if (clean.startsWith('52') && clean.length === 12) {
      jidCandidates = [`${clean}@s.whatsapp.net`, `521${clean.slice(2)}@s.whatsapp.net`];
    } else {
      jidCandidates = [`${clean}@s.whatsapp.net`];
    }

    // Si coincide con el teléfono de la sesión propia (auto-envío de prueba)
    if (this.phoneNumber && (clean === this.phoneNumber || clean.endsWith(this.phoneNumber.slice(-10)))) {
      const myClean = this.phoneNumber.replace(/\D/g, '');
      jidCandidates = [`${myClean}@s.whatsapp.net`, ...jidCandidates];
    }

    let targetJid = jidCandidates[0];
    try {
      // Intentar validar y obtener el JID canónico registrado en WhatsApp
      for (const candidate of jidCandidates) {
        const results = await this.sock.onWhatsApp(candidate);
        if (results && results.length > 0 && results[0]?.exists) {
          targetJid = results[0].jid;
          break;
        }
      }
    } catch (e: any) {
      console.warn('[WhatsApp] onWhatsApp check falló, usando candidato principal:', targetJid, e.message);
      targetJid = jidCandidates[0];
    }

    // Convertir imagen base64 a Buffer
    let pureBase64 = params.imagenBase64;
    if (pureBase64.includes(',')) {
      pureBase64 = pureBase64.split(',')[1];
    }
    const buffer = Buffer.from(pureBase64, 'base64');

    // Detectar mimetype real para evitar rechazo o fallo en cliente de WhatsApp
    const isPng = buffer.length > 4 && buffer[0] === 0x89 && buffer[1] === 0x50 && buffer[2] === 0x4e && buffer[3] === 0x47;
    const mimetype = isPng ? 'image/png' : 'image/jpeg';

    logWhatsApp(`[WhatsApp] Enviando comprobante (${mimetype}, ${(buffer.length / 1024).toFixed(1)} KB) a ${targetJid}`);

    // Mensaje / Leyenda
    const folioLimpio = params.folio && params.folio !== 'undefined' ? params.folio.trim() : '';
    let caption = params.mensaje;
    if (!caption) {
      caption = `🚲 *Casa Licona - Bicicletas e Innovaciones*\n\nLe hacemos llegar su comprobante de venta digital${
        folioLimpio ? ` (*Folio: ${folioLimpio}*)` : ''
      }.\n\n¡Agradecemos su compra y preferencia!`;
    } else {
      // Limpieza de seguridad por si venía la palabra literal "undefined"
      caption = caption
        .replace(/Folio:\s*\*undefined\*/gi, folioLimpio ? `Folio: *${folioLimpio}*` : '')
        .replace(/\*undefined\*/gi, folioLimpio ? `*${folioLimpio}*` : '')
        .replace(/undefined/gi, folioLimpio || '')
        .trim();
    }

    // Enviar por Baileys con mimetype explícito
    const result = await this.sock.sendMessage(targetJid, {
      image: buffer,
      mimetype: mimetype,
      caption: caption
    });

    // Guardar el mensaje en el store para responder a solicitudes de retry/descifrado de WhatsApp
    if (result?.key?.id && result?.message) {
      this.messageStore.set(result.key.id, result.message);
      logWhatsApp(`[WhatsApp] Guardado en messageStore para key.id: ${result.key.id}, keys en mensaje: ${Object.keys(result.message).join(', ')}`);
      if (this.messageStore.size > 300) {
        const first = this.messageStore.keys().next().value;
        if (first) this.messageStore.delete(first);
      }
    }

    logWhatsApp(`[WhatsApp] ¡Comprobante enviado exitosamente! MessageId: ${result?.key?.id}`);

    return {
      success: true,
      jid: targetJid,
      messageId: result?.key?.id || undefined
    };
  }

  public async logout(): Promise<void> {
    try {
      if (this.sock) {
        await this.sock.logout();
      }
    } catch (e) {
      console.warn('[WhatsApp] Error al cerrar socket en logout:', e);
    }
    this.limpiarSesion();
    this.status = 'DISCONNECTED';
    this.phoneNumber = null;
    this.userName = null;
    this.qrCodeDataUrl = null;
    this.isInitializing = false;
    // Volver a inicializar para dar de inmediato nuevo QR
    setTimeout(() => this.init(), 1000);
  }

  private limpiarSesion(): void {
    try {
      if (fs.existsSync(this.authDir)) {
        fs.rmSync(this.authDir, { recursive: true, force: true });
        fs.mkdirSync(this.authDir, { recursive: true });
      }
    } catch (err) {
      console.error('[WhatsApp] Error al limpiar directorio de sesión:', err);
    }
  }
}

export const whatsAppService = new WhatsAppService();
