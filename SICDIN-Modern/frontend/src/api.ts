const API_BASE = import.meta.env.VITE_API_BASE || `http://${window.location.hostname}:4000/api`;

export interface Articulo {
  Clave: string;
  Descripcion: string;
  Menudeo: number;
  Mayoreo: number;
  PME: number;
  Especial: number;
  Existencias: number;
  cbarra: string;
  Paquete?: string;
  Tipo?: string;
}

export interface Cliente {
  Id: number;
  Nombre: string;
  Direccion?: string;
  Mayoreo: number;
  Especial?: number;
  Credito?: number;
  Adeudo?: number;
  Telcel?: string;
  Telcasa?: string;
  mail?: string;
  RFC?: string;
  ultcompra?: string;
}

export interface ItemVenta {
  Clave: string;
  Descripcion: string;
  Cantidad: number;
  Precio: number;
  Total: number;
  Menudeo?: number;
  Mayoreo?: number;
  PME?: number;
  Especial?: number;
  tipoPrecio?: 'Menudeo' | 'Mayoreo' | 'PME Especial';
  Paquete?: string;
  Existencias?: number;
  Tipo?: string;
}

export interface BiciOrden {
  Folio: string;
  Id: number;
  Marca: string;
  Color: string;
  Rodada: string;
  Nota?: string;
  Fhentrega?: string;
  bfinalizada: number;
  bentregada: number;
  bcancelada: number;
  Finalizada?: string;
  Entregada?: string;
  Realizo?: string;
  Entrego?: string;
}

export interface OrdenReparacion {
  Folio: string;
  Cliente: string;
  Fecha: string;
  Hora: string;
  Total: number;
  TotalPagado: number;
  Resta: number;
  Usuario: string;
  telcel?: string;
  telcasa?: string;
  Direccion?: string;
  Fcreacion?: string;
  Fechae?: string;
  Finalizadaall: number;
  bentregada: number;
  Fcan: number;
  bicicletas: BiciOrden[];
}

export interface ArticuloReparacion {
  Folio: string;
  Id: number;
  Cantidad: number;
  Clave: string;
  Descripcion: string;
  Tp: string;
  cancel: number;
  colocado: number;
  Trabajo: number;
  Paquete?: string;
  Realizo?: number;
  Fecha?: string;
  Hora?: string;
}

export interface MovimientoDia {
  rowid: number;
  Clave: string;
  Descripcion: string;
  Total: number;
  usuario: string;
  fecha: string;
  hora: string;
  horast: string;
  cantidad: number;
  Cliente: string;
  Pagoefe: number;
  Pagotrans: number;
  Pagotar: number;
  Descuento: number;
  Pagado: number;
  cancel: number;
  nota: number; // 0 = Venta rápida (sin nota), 1 = Nota de venta
}

export interface ResumenVentasDia {
  fecha: string;
  totalVentas: number;
  totalEfectivo: number;
  totalTarjeta: number;
  totalTransfer: number;
  conteo: number;
  movimientos: MovimientoDia[];
}

export interface PagoReparacion {
  Folio: string;
  Fecha: string;
  Usuario: string;
  Cantidad: number;
  Id: number;
  Efectivo: number;
  Transferencia: number;
  Tarjeta: number;
}

export interface Trabajador {
  Id: number;
  Nombre: string;
  Telefono?: string;
  telcel?: string;
  Activo: number;
}

export interface Usuario {
  Id: number;
  Nombre: string;
  Pass: string;
  Fu?: string;
  Admin: number;
  telefono?: string;
  activo: number;
  Foto?: string;
}

export interface WhatsAppStatus {
  status: 'DISCONNECTED' | 'CONNECTING' | 'QR_READY' | 'CONNECTED';
  qrCode: string | null;
  phoneNumber: string | null;
  name: string | null;
  ultimoError: string | null;
}

export const api = {
  buscarArticulos: async (query: string): Promise<Articulo[]> => {
    const res = await fetch(`${API_BASE}/articulos?q=${encodeURIComponent(query)}`);
    return res.json();
  },

  buscarClientes: async (query: string = ''): Promise<Cliente[]> => {
    const res = await fetch(`${API_BASE}/clientes?q=${encodeURIComponent(query)}`);
    return res.json();
  },

  obtenerClienteDetalle: async (id: number): Promise<{ cliente: Cliente; movimientos: any[]; ventas: any[] }> => {
    const res = await fetch(`${API_BASE}/clientes/${id}`);
    return res.json();
  },

  crearCliente: async (cliente: Partial<Cliente>): Promise<{ success: boolean; cliente: Cliente }> => {
    const res = await fetch(`${API_BASE}/clientes`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(cliente)
    });
    return res.json();
  },

  actualizarCliente: async (id: number, cliente: Partial<Cliente>): Promise<{ success: boolean; cliente: Cliente }> => {
    const res = await fetch(`${API_BASE}/clientes/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(cliente)
    });
    return res.json();
  },

  eliminarCliente: async (id: number): Promise<{ success: boolean; message?: string; error?: string }> => {
    const res = await fetch(`${API_BASE}/clientes/${id}`, {
      method: 'DELETE'
    });
    return res.json();
  },

  crearVenta: async (data: {
    clienteId?: number;
    usuario?: string;
    pagoEfectivo: number;
    pagoTarjeta: number;
    pagoTransfer: number;
    descuento: number;
    items: ItemVenta[];
  }) => {
    const res = await fetch(`${API_BASE}/ventas`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    if (!res.ok) {
      const err = await res.json().catch(() => ({}));
      throw new Error(err.error || 'Error al procesar la venta');
    }
    return res.json();
  },

  crearVentaRapida: async (data: {
    clave: string;
    cantidad: number;
    precio?: number;
    pagoEfectivo?: number;
    pagoTarjeta?: number;
    pagoTransfer?: number;
    cliente?: string;
    usuario?: string;
  }): Promise<{
    success: boolean;
    message: string;
    clave: string;
    descripcion: string;
    cantidad: number;
    precioUnitario: number;
    total: number;
    pagoEfectivo: number;
    pagoTarjeta: number;
    pagoTransfer: number;
    cambio: number;
    existenciasRestantes: number;
  }> => {
    const res = await fetch(`${API_BASE}/ventas/rapida`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    if (!res.ok) {
      const err = await res.json().catch(() => ({}));
      throw new Error(err.error || 'Error al procesar la venta rápida');
    }
    return res.json();
  },

  obtenerVentasDia: async (fecha?: string): Promise<ResumenVentasDia> => {
    const url = fecha ? `${API_BASE}/ventas/dia?fecha=${encodeURIComponent(fecha)}` : `${API_BASE}/ventas/dia`;
    const res = await fetch(url);
    return res.json();
  },

  obtenerDetalleVenta: async (folio: string): Promise<{
    folio: string;
    venta: any;
    cliente: any;
    articulos: Array<{ Clave: string; Cantidad: number; Descripcion: string; Precio: number; Importe: number; Paquete?: string }>;
  }> => {
    const res = await fetch(`${API_BASE}/ventas/${encodeURIComponent(folio)}`);
    return res.json();
  },

  obtenerArticulo: async (clave: string): Promise<Articulo> => {
    const res = await fetch(`${API_BASE}/articulos/${encodeURIComponent(clave)}`);
    return res.json();
  },

  guardarArticulo: async (articulo: Partial<Articulo>): Promise<{ success: boolean; clave: string }> => {
    const res = await fetch(`${API_BASE}/articulos`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(articulo)
    });
    return res.json();
  },

  modificarExistencias: async (clave: string, cambio: number, tipo: 'AJUSTE' | 'ABSOLUTO' = 'AJUSTE'): Promise<{ success: boolean; existencias: number }> => {
    const res = await fetch(`${API_BASE}/articulos/${encodeURIComponent(clave)}/existencias`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ cambio, tipo })
    });
    return res.json();
  },

  syncPull: async () => {
    const res = await fetch(`${API_BASE}/sync/pull`, { method: 'POST' });
    return res.json();
  },

  // Taller y Reparaciones
  listarReparaciones: async (filtro: string = 'ACTIVAS', q: string = ''): Promise<OrdenReparacion[]> => {
    const res = await fetch(`${API_BASE}/reparaciones?filtro=${encodeURIComponent(filtro)}&q=${encodeURIComponent(q)}`);
    return res.json();
  },

  obtenerReparacion: async (folio: string): Promise<{
    nota: OrdenReparacion;
    bicicletas: BiciOrden[];
    articulos: ArticuloReparacion[];
    pagos: PagoReparacion[];
    totalPagado: number;
    resta: number;
  }> => {
    const res = await fetch(`${API_BASE}/reparaciones/${encodeURIComponent(folio)}`);
    return res.json();
  },

  crearReparacion: async (data: any): Promise<{ success: boolean; folio: string; total: number; anticipo: number; resta: number }> => {
    const res = await fetch(`${API_BASE}/reparaciones`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  cambiarEstadoBici: async (folio: string, biciId: number, data: { accion: 'FINALIZAR' | 'ENTREGAR' | 'CANCELAR'; mecanico?: string; usuario?: string; motivoCancel?: string }): Promise<{ success: boolean; message: string }> => {
    const res = await fetch(`${API_BASE}/reparaciones/${encodeURIComponent(folio)}/bicicletas/${biciId}/estado`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  registrarPagoReparacion: async (folio: string, data: { efectivo: number; tarjeta: number; transferencia: number; usuario?: string; entregarAlLiquidar?: boolean }): Promise<{ success: boolean; message: string; pago: number }> => {
    const res = await fetch(`${API_BASE}/reparaciones/${encodeURIComponent(folio)}/pagos`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  toggleArticuloColocado: async (folio: string, clave: string, colocado: boolean): Promise<{ success: boolean; colocado: number }> => {
    const res = await fetch(`${API_BASE}/reparaciones/${encodeURIComponent(folio)}/articulos/${encodeURIComponent(clave)}/colocado`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ colocado })
    });
    return res.json();
  },

  listarTrabajadores: async (): Promise<Trabajador[]> => {
    const res = await fetch(`${API_BASE}/trabajadores`);
    return res.json();
  },

  // Control y Autenticación de Usuarios
  loginUsuario: async (idOUsername: number | string, pass: string): Promise<{ success: boolean; usuario: Usuario; message: string }> => {
    const payload = typeof idOUsername === 'number' ? { id: idOUsername, pass } : { nombre: idOUsername, pass };
    const res = await fetch(`${API_BASE}/usuarios/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    });
    const data = await res.json();
    if (!res.ok) {
      throw new Error(data.error || 'Error al iniciar sesión');
    }
    return data;
  },

  listarUsuarios: async (q?: string): Promise<Usuario[]> => {
    const url = q ? `${API_BASE}/usuarios?q=${encodeURIComponent(q)}` : `${API_BASE}/usuarios`;
    const res = await fetch(url);
    if (!res.ok) {
      const err = await res.json().catch(() => ({}));
      throw new Error(err.error || 'Error al listar usuarios');
    }
    const data = await res.json();
    return Array.isArray(data) ? data : [];
  },

  obtenerUsuario: async (id: number): Promise<Usuario> => {
    const res = await fetch(`${API_BASE}/usuarios/${id}`);
    return res.json();
  },

  crearUsuario: async (data: Partial<Usuario>): Promise<{ success: boolean; usuario: Usuario }> => {
    const res = await fetch(`${API_BASE}/usuarios`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al crear usuario');
    }
    return res.json();
  },

  actualizarUsuario: async (id: number, data: Partial<Usuario>): Promise<{ success: boolean; usuario: Usuario }> => {
    const res = await fetch(`${API_BASE}/usuarios/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al actualizar usuario');
    }
    return res.json();
  },

  eliminarUsuario: async (id: number): Promise<{ success: boolean; message: string }> => {
    const res = await fetch(`${API_BASE}/usuarios/${id}`, {
      method: 'DELETE'
    });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al eliminar usuario');
    }
    return res.json();
  },

  subirFotoUsuario: async (imagenBase64: string, userId?: number): Promise<{ success: boolean; url: string; rutaLocal: string; nombreArchivo: string }> => {
    const res = await fetch(`${API_BASE}/usuarios/upload-foto`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ imagenBase64, userId })
    });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al subir foto de usuario');
    }
    return res.json();
  },

  resolverAvatarUrl: (foto?: string): string | null => {
    if (!foto) return null;
    const f = foto.trim();
    if (f.startsWith('data:') || f.startsWith('http://') || f.startsWith('https://')) {
      return f;
    }
    if (f.startsWith('/uploads/')) {
      const baseUrl = API_BASE.replace(/\/api$/, '');
      return `${baseUrl}${f}`;
    }
    if (f.includes('\\') || f.includes('/')) {
      return `${API_BASE}/usuarios/foto?path=${encodeURIComponent(f)}`;
    }
    return null;
  },

  // WhatsApp del Negocio (Baileys)
  obtenerEstadoWhatsApp: async (): Promise<WhatsAppStatus> => {
    const res = await fetch(`${API_BASE}/whatsapp/status`);
    if (!res.ok) {
      throw new Error('Error al obtener estado de WhatsApp');
    }
    return res.json();
  },

  enviarTicketWhatsApp: async (data: {
    telefono: string;
    imagenBase64: string;
    mensaje?: string;
    folio?: string;
  }): Promise<{ success: boolean; jid: string; messageId?: string }> => {
    const res = await fetch(`${API_BASE}/whatsapp/send`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al enviar ticket por WhatsApp');
    }
    return res.json();
  },

  desconectarWhatsApp: async (): Promise<{ success: boolean; message: string }> => {
    const res = await fetch(`${API_BASE}/whatsapp/logout`, { method: 'POST' });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al desconectar WhatsApp');
    }
    return res.json();
  },

  reiniciarWhatsApp: async (): Promise<{ success: boolean; message: string }> => {
    const res = await fetch(`${API_BASE}/whatsapp/restart`, { method: 'POST' });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.error || 'Error al reiniciar servicio de WhatsApp');
    }
    return res.json();
  }
};
