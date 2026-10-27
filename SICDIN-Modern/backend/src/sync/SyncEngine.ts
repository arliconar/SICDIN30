import fs from 'fs';
import path from 'path';
import db from '../db';

export interface DeltaOperation {
  table: string;
  operation: 'INSERT' | 'UPDATE' | 'DELETE';
  keyField: string;
  keyValue: string | number;
  data: Record<string, any>;
  timestamp: string;
  originStore: string;
  id: string; // UUID / timestamp hash
}

export class SyncEngine {
  private baseCloudDir: string;
  private myStoreApodo: string;
  private localOutbox: string;

  constructor() {
    this.myStoreApodo = (process.env.STORE_APODO || 'matriz').trim().toLowerCase();
    this.baseCloudDir = process.env.CLOUD_PATH || 'C:\\sc3\\sync_deltas';
    this.localOutbox = process.env.OUTBOX_PATH || 'C:\\sc3\\sync_outbox';
    this.ensureDirs();
    this.initSyncTable();
  }

  private ensureDirs() {
    try {
      if (!fs.existsSync(this.localOutbox)) fs.mkdirSync(this.localOutbox, { recursive: true });
      if (!fs.existsSync(this.baseCloudDir)) fs.mkdirSync(this.baseCloudDir, { recursive: true });
      const inboxes = path.join(this.baseCloudDir, 'inboxes');
      if (!fs.existsSync(inboxes)) fs.mkdirSync(inboxes, { recursive: true });
      const myInbox = path.join(inboxes, this.myStoreApodo);
      if (!fs.existsSync(myInbox)) fs.mkdirSync(myInbox, { recursive: true });
    } catch (err) {
      console.error('[SYNC] Error creando directorios de sincronización:', err);
    }
  }

  private initSyncTable() {
    db.exec(`
      CREATE TABLE IF NOT EXISTS sys_processed_deltas (
        delta_file TEXT PRIMARY KEY,
        processed_at TEXT NOT NULL
      );
    `);
  }

  /**
   * Genera un delta JSON cuando ocurre un cambio y lo distribuye a los buzones (inboxes) de otras tiendas.
   */
  public emitDelta(table: string, operation: 'INSERT' | 'UPDATE' | 'DELETE', keyField: string, keyValue: any, data: Record<string, any>) {
    const timestamp = new Date().toISOString().replace(/[-:T.]/g, '').slice(0, 14);
    const rand = Math.floor(Math.random() * 1000).toString().padStart(3, '0');
    const fileName = `delta_${this.myStoreApodo}_${table}_${timestamp}_${rand}.json`;

    const delta: DeltaOperation = {
      table,
      operation,
      keyField,
      keyValue,
      data,
      timestamp: new Date().toISOString(),
      originStore: this.myStoreApodo,
      id: fileName
    };

    const deltaContent = JSON.stringify(delta, null, 2);

    // 1. Guardar en outbox local
    const outboxFile = path.join(this.localOutbox, fileName);
    fs.writeFileSync(outboxFile, deltaContent, 'utf-8');

    // 2. Distribuir a los inboxes de las demás tiendas conocidas
    const destinationStores = this.getDestinationStores();
    for (const targetStore of destinationStores) {
      try {
        const targetInbox = path.join(this.baseCloudDir, 'inboxes', targetStore);
        if (!fs.existsSync(targetInbox)) fs.mkdirSync(targetInbox, { recursive: true });
        fs.writeFileSync(path.join(targetInbox, fileName), deltaContent, 'utf-8');
      } catch (err) {
        console.error(`[SYNC] Error enviando delta a buzón ${targetStore}:`, err);
      }
    }

    return fileName;
  }

  /**
   * Procesa los deltas acumulados en el buzón de la tienda actual.
   */
  public processMyInbox(): { processed: number; errors: number } {
    const myInbox = path.join(this.baseCloudDir, 'inboxes', this.myStoreApodo);
    if (!fs.existsSync(myInbox)) return { processed: 0, errors: 0 };

    const files = fs.readdirSync(myInbox).filter(f => f.endsWith('.json')).sort();
    let processed = 0;
    let errors = 0;

    for (const file of files) {
      const isDone = db.prepare('SELECT 1 FROM sys_processed_deltas WHERE delta_file = ?').get(file);
      if (isDone) continue;

      const filePath = path.join(myInbox, file);
      try {
        const content = fs.readFileSync(filePath, 'utf-8');
        const delta: DeltaOperation = JSON.parse(content);

        this.applyDelta(delta);

        db.prepare('INSERT INTO sys_processed_deltas (delta_file, processed_at) VALUES (?, ?)').run(
          file,
          new Date().toISOString()
        );
        processed++;
      } catch (err) {
        console.error(`[SYNC] Error procesando delta ${file}:`, err);
        errors++;
      }
    }

    return { processed, errors };
  }

  private applyDelta(delta: DeltaOperation) {
    const { table, operation, keyField, keyValue, data } = delta;

    if (operation === 'DELETE') {
      db.prepare(`DELETE FROM ${table} WHERE ${keyField} = ?`).run(keyValue);
      return;
    }

    const columns = Object.keys(data);
    if (columns.length === 0) return;

    if (operation === 'INSERT') {
      const placeholders = columns.map(() => '?').join(', ');
      const values = columns.map(c => data[c]);
      db.prepare(`INSERT OR REPLACE INTO ${table} (${columns.join(', ')}) VALUES (${placeholders})`).run(...values);
    } else if (operation === 'UPDATE') {
      const setClause = columns.map(c => `${c} = ?`).join(', ');
      const values = [...columns.map(c => data[c]), keyValue];
      db.prepare(`UPDATE ${table} SET ${setClause} WHERE ${keyField} = ?`).run(...values);
    }
  }

  public getDestinationStores(): string[] {
    try {
      const rows = db.prepare('SELECT Apodo FROM sys_lugares WHERE Apodo IS NOT NULL').all() as { Apodo: string }[];
      return rows
        .map(r => r.Apodo?.trim().toLowerCase())
        .filter(ap => ap && ap !== this.myStoreApodo);
    } catch {
      return [];
    }
  }
}

export const syncEngine = new SyncEngine();
