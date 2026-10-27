# SICDIN Modern 🚀

Punto de Venta (POS) Modernizado, Multiplataforma (Windows, Linux, macOS) y preparado para aplicaciones móviles Android e iOS.

---

## 📁 Estructura del Proyecto

* **`backend/`**: REST API en Node.js + TypeScript con base de datos **SQLite** embebida (`sicdin.db`) y **Motor de Sincronización Nube por Deltas e Inboxes**.
* **`frontend/`**: WebApp TPV desarrollada en **React + Vite + Tailwind CSS** con soporte para escáneres de código de barras, teclado rápido y cobro.
* **`migration/`**: Extractor compilado en .NET 8 que migró automáticamente todas las bases Access `.dat` legadas a SQLite.
* **`docs/`**: Especificación de la REST API lista para conectar las apps móviles Android / iOS.

---

## ⚡ Cómo Ejecutar

### 1. Iniciar el Backend (REST API + SQLite):
```bash
cd SICDIN-Modern/backend
npx tsx src/index.ts
```
> El servidor arrancará en `http://localhost:4000`

### 2. Iniciar el Frontend (Punto de Venta Web):
```bash
cd SICDIN-Modern/frontend
npm run dev
```
> La app abrirá en `http://localhost:3000`

---

## 🔄 Sistema de Sincronización Nube (Deltas e Inboxes)

El backend replica exactamente la lógica de `nube.vb`:
* **Outbox**: `C:\sc3\sync_outbox` (almacena deltas locales generados al vender o editar artículos).
* **Buzones (Inboxes)**: `C:\sc3\sync_deltas\inboxes\<apodo_tienda>` (distribuye los cambios a las demás sucursales).
* **Control de Procesados**: Tabla SQLite `sys_processed_deltas` para evitar duplicidad de transacciones.
