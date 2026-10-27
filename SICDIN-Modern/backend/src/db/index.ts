import Database from 'better-sqlite3';
import path from 'path';

const dbPath = process.env.DB_PATH || path.resolve(__dirname, '../../sicdin.db');
export const db = new Database(dbPath);

// Habilitar modo WAL (Write-Ahead Logging) para mejor rendimiento concurrente
db.pragma('journal_mode = WAL');
db.pragma('foreign_keys = ON');

export default db;
