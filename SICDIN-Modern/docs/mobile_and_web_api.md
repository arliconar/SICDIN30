# Especificación de API REST - SICDIN Moderno (Para Frontend Web, Android & iOS)

Servidor Base: `http://<IP_SERVIDOR>:4000/api`

---

## 1. Catálogo e Inventario

### `GET /api/articulos`
Búsqueda de artículos para escáner o texto.
* **Query Params**:
  * `q` (string): Código de barras, clave directa o palabras de la descripción.
* **Respuesta (200)**:
```json
[
  {
    "Clave": "EK8PTD",
    "Descripcion": "ESPROK 8 VEL 13/28 INDEXADO TRI DIAM",
    "Menudeo": 140,
    "Mayoreo": 95,
    "PME": 0,
    "Especial": 0,
    "Existencias": 1,
    "cbarra": "750123456789",
    "Paquete": "PZA"
  }
]
```

### `GET /api/articulos/:clave`
Obtiene los detalles completos de un artículo por su clave.

### `POST /api/articulos`
Crea o actualiza un artículo y emite un delta de sincronización.
```json
{
  "Clave": "EK8PTD",
  "Descripcion": "ESPROK 8 VEL 13/28 INDEXADO TRI DIAM",
  "Menudeo": 145,
  "Mayoreo": 100,
  "Existencias": 10,
  "cbarra": "750123456789",
  "Tipo": "Artículos"
}
```

---

## 2. Ventas y Cobro (TPV)

### `POST /api/ventas`
Registra una venta, descuenta inventario y emite delta a la nube.
```json
{
  "clienteId": 1,
  "usuario": "CAJERO1",
  "pagoEfectivo": 200,
  "pagoTarjeta": 0,
  "pagoTransfer": 0,
  "descuento": 0,
  "items": [
    {
      "Clave": "EK8PTD",
      "Descripcion": "ESPROK 8 VEL 13/28 INDEXADO TRI DIAM",
      "Cantidad": 1,
      "Precio": 140,
      "Total": 140
    }
  ]
}
```
* **Respuesta (201)**:
```json
{
  "success": true,
  "folio": "V202610240001",
  "total": 140,
  "resta": 0,
  "cambio": 60
}
```

### `GET /api/ventas`
Retorna el historial de las últimas 50 ventas realizadas en la sucursal.

---

## 3. Clientes y Cartera

### `GET /api/clientes`
Listado y búsqueda de clientes.
* **Query Params**: `q` (nombre o id).

### `GET /api/clientes/:id`
Detalle del cliente y sus movimientos históricos (abonos, cargos, adeudo actual).

---

## 4. Taller y Reparaciones

### `GET /api/reparaciones`
Lista todas las órdenes de trabajo y bicicletas activas en taller (`Entregada = 0`).

---

## 5. Motor de Sincronización (Nube / Deltas)

### `POST /api/sync/pull`
Procesa y aplica todos los deltas JSON pendientes en el buzón `inboxes/<apodo_tienda>`.

### `GET /api/sync/destinos`
Lista las sucursales y destinos conocidos configurados en la red.
