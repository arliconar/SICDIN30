import { useState, useEffect, useRef } from 'react';
import { ShoppingCart, Search, Trash2, DollarSign, Plus, Minus, X, User, Users, Tag, Check, ArrowRight, Banknote, CreditCard, ArrowLeftRight, AlertTriangle, Package, Wrench, Infinity } from 'lucide-react';
import { api, type Articulo, type ItemVenta, type Cliente } from './api';
import NotaVentaModal, { type NotaVentaData } from './NotaVentaModal';

interface VentaViewProps {
  id: string;
  tabTitle: string;
  usuarioActualNombre?: string;
  onClose: () => void;
}

export default function VentaView({ tabTitle, usuarioActualNombre = 'ADMIN', onClose }: VentaViewProps) {
  const [query, setQuery] = useState('');
  const [sugerencias, setSugerencias] = useState<Articulo[]>([]);
  const [carrito, setCarrito] = useState<ItemVenta[]>([]);
  const [modalCobro, setModalCobro] = useState(false);
  const [modalClientes, setModalClientes] = useState(false);
  const [pagoEfectivo, setPagoEfectivo] = useState<string>('');
  const [pagoTarjeta, setPagoTarjeta] = useState<string>('');
  const [pagoTransfer, setPagoTransfer] = useState<string>('');
  const [notaVentaFinalizada, setNotaVentaFinalizada] = useState<NotaVentaData | null>(null);
  const [advertenciaStock, setAdvertenciaStock] = useState<string | null>(null);

  // Filtros de Tipo y Existencias
  const [filtroTipo, setFiltroTipo] = useState<'todos' | 'articulos' | 'servicios' | 'infinitos'>('todos');
  const [soloConExistencias, setSoloConExistencias] = useState<boolean>(false);
  const [mostrarTicketMobile, setMostrarTicketMobile] = useState<boolean>(false);
  
  // Cliente seleccionado (por defecto Mostrador)
  const [cliente, setCliente] = useState<Cliente | null>(null);
  const [queryCliente, setQueryCliente] = useState('');
  const [clientesEncontrados, setClientesEncontrados] = useState<Cliente[]>([]);
  const [cargandoClientes, setCargandoClientes] = useState(false);

  const searchInputRef = useRef<HTMLInputElement>(null);
  const clienteInputRef = useRef<HTMLInputElement>(null);

  // Cargar cliente default (Mostrador / Id 1) y catálogo inicial
  useEffect(() => {
    searchInputRef.current?.focus();
    api.buscarArticulos('').then(res => setSugerencias(res)).catch(() => {});
    
    // Cargar cliente inicial Mostrador
    api.buscarClientes('').then(clnts => {
      const mostrador = clnts.find(c => c.Id === 1 || c.Nombre.toLowerCase().includes('mostrador')) || clnts[0];
      if (mostrador) setCliente(mostrador);
      setClientesEncontrados(clnts);
    }).catch(() => {});
  }, []);

  // Búsqueda reactiva de productos
  useEffect(() => {
    const timer = setTimeout(async () => {
      try {
        const res = await api.buscarArticulos(query);
        setSugerencias(res);
      } catch {
        setSugerencias([]);
      }
    }, 60);
    return () => clearTimeout(timer);
  }, [query]);

  // Búsqueda reactiva de clientes en el modal
  useEffect(() => {
    if (!modalClientes) return;
    setCargandoClientes(true);
    const timer = setTimeout(async () => {
      try {
        const res = await api.buscarClientes(queryCliente);
        setClientesEncontrados(res);
      } catch {
        setClientesEncontrados([]);
      } finally {
        setCargandoClientes(false);
      }
    }, 150);
    return () => clearTimeout(timer);
  }, [queryCliente, modalClientes]);

  // Manejo de atajos de teclado globales en la vista de venta
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === 'F2') {
        e.preventDefault();
        searchInputRef.current?.focus();
      } else if (e.key === 'F3') {
        e.preventDefault();
        setModalClientes(true);
        setTimeout(() => clienteInputRef.current?.focus(), 50);
      } else if (e.key === 'F12') {
        e.preventDefault();
        if (carrito.length > 0) abrirModalCobro();
      } else if (e.key === 'Escape') {
        if (modalCobro) setModalCobro(false);
        if (modalClientes) setModalClientes(false);
      }
    };
    window.addEventListener('keydown', handleKeyDown);
    return () => window.removeEventListener('keydown', handleKeyDown);
  }, [carrito.length, modalCobro, modalClientes]);

  // Cálculo de precio según el cliente y la cantidad:
  // - Si el cliente es Mayoreo:
  //    * Si tiene PME > 0 y Especial > 0, y cantidad >= PME -> Precio Especial (PME)
  //    * Si no alcanza PME -> Precio Mayoreo
  // - Si el cliente NO es mayoreo (Menudeo):
  //    * Precio Menudeo siempre
  const calcularPrecioItem = (
    art: { Menudeo: number; Mayoreo: number; PME: number; Especial: number },
    clnt: Cliente | null,
    cantidad: number
  ): { precio: number; tipoPrecio: 'Menudeo' | 'Mayoreo' | 'PME Especial' } => {
    const esMayoreo = Boolean(clnt && clnt.Mayoreo === 1);

    if (esMayoreo) {
      const pme = Number(art.PME) || 0;
      const esp = Number(art.Especial) || 0;
      if (pme > 0 && esp > 0 && cantidad >= pme) {
        return { precio: esp, tipoPrecio: 'PME Especial' };
      }
      const precioMayoreo = (Number(art.Mayoreo) || 0) > 0 ? Number(art.Mayoreo) : Number(art.Menudeo);
      return { precio: precioMayoreo, tipoPrecio: 'Mayoreo' };
    }

    return { precio: Number(art.Menudeo) || 0, tipoPrecio: 'Menudeo' };
  };

  const seleccionarCliente = (nuevoCliente: Cliente) => {
    setCliente(nuevoCliente);
    setModalClientes(false);

    // Recalcular inmediatamente todos los productos en el carrito con el nuevo cliente
    setCarrito(prev =>
      prev.map(it => {
        const { precio, tipoPrecio } = calcularPrecioItem(
          {
            Menudeo: it.Menudeo ?? it.Precio,
            Mayoreo: it.Mayoreo ?? it.Precio,
            PME: it.PME ?? 0,
            Especial: it.Especial ?? 0
          },
          nuevoCliente,
          it.Cantidad
        );
        return {
          ...it,
          Precio: precio,
          Total: it.Cantidad * precio,
          tipoPrecio
        };
      })
    );

    searchInputRef.current?.focus();
  };

  const esSinControlStock = (tipo?: string) => {
    const t = (tipo || '').toLowerCase().trim();
    return t === 'servicio' || t === 'infinito' || t === 'trabajo';
  };

  const agregarAlCarrito = (art: Articulo) => {
    const sinStock = esSinControlStock(art.Tipo);
    const stockDisponible = Number(art.Existencias) || 0;

    // Validación preventiva de existencias
    if (!sinStock) {
      if (stockDisponible <= 0) {
        setAdvertenciaStock(`El artículo "${art.Descripcion}" está AGOTADO (0 existencias). No se puede agregar.`);
        return;
      }
      const itemExistente = carrito.find(it => it.Clave === art.Clave);
      const cantidadEnCarrito = itemExistente ? itemExistente.Cantidad : 0;
      if (cantidadEnCarrito + 1 > stockDisponible) {
        setAdvertenciaStock(`Existencias insuficientes para "${art.Descripcion}". Solo hay ${stockDisponible} disponible(s) en tienda.`);
        return;
      }
    }

    setCarrito(prev => {
      const idx = prev.findIndex(it => it.Clave === art.Clave);
      if (idx >= 0) {
        const itemActual = prev[idx];
        const nuevaCant = itemActual.Cantidad + 1;
        const { precio, tipoPrecio } = calcularPrecioItem(
          {
            Menudeo: itemActual.Menudeo ?? art.Menudeo,
            Mayoreo: itemActual.Mayoreo ?? art.Mayoreo,
            PME: itemActual.PME ?? art.PME,
            Especial: itemActual.Especial ?? art.Especial
          },
          cliente,
          nuevaCant
        );
        const copy = [...prev];
        copy[idx] = {
          ...copy[idx],
          Cantidad: nuevaCant,
          Precio: precio,
          Total: nuevaCant * precio,
          tipoPrecio,
          Existencias: art.Existencias,
          Tipo: art.Tipo
        };
        return copy;
      }

      const { precio, tipoPrecio } = calcularPrecioItem(art, cliente, 1);
      return [
        ...prev,
        {
          Clave: art.Clave,
          Descripcion: art.Descripcion,
          Cantidad: 1,
          Precio: precio,
          Total: precio,
          Menudeo: art.Menudeo,
          Mayoreo: art.Mayoreo,
          PME: art.PME,
          Especial: art.Especial,
          tipoPrecio,
          Existencias: art.Existencias,
          Tipo: art.Tipo
        }
      ];
    });
    setQuery('');
    searchInputRef.current?.focus();
  };

  const modificarCantidad = (clave: string, delta: number) => {
    if (delta > 0) {
      const it = carrito.find(x => x.Clave === clave);
      if (it && !esSinControlStock(it.Tipo)) {
        const stockActual = Number(it.Existencias) || 0;
        if (it.Cantidad + delta > stockActual) {
          setAdvertenciaStock(`Stock insuficiente para "${it.Descripcion}". Disponibles en tienda: ${stockActual}.`);
          return;
        }
      }
    }

    setCarrito(prev =>
      prev
        .map(it => {
          if (it.Clave === clave) {
            const nuevaCant = it.Cantidad + delta;
            if (nuevaCant <= 0) return null as any;
            const { precio, tipoPrecio } = calcularPrecioItem(
              {
                Menudeo: it.Menudeo ?? it.Precio,
                Mayoreo: it.Mayoreo ?? it.Precio,
                PME: it.PME ?? 0,
                Especial: it.Especial ?? 0
              },
              cliente,
              nuevaCant
            );
            return {
              ...it,
              Cantidad: nuevaCant,
              Precio: precio,
              Total: nuevaCant * precio,
              tipoPrecio
            };
          }
          return it;
        })
        .filter(Boolean)
    );
  };

  const itemsConErrorStock = carrito.filter(it => {
    if (esSinControlStock(it.Tipo)) return false;
    const stock = Number(it.Existencias) || 0;
    return stock <= 0 || it.Cantidad > stock;
  });
  const hayErrorStock = itemsConErrorStock.length > 0;

  const subtotal = carrito.reduce((sum, it) => sum + it.Total, 0);
  const adeudoCliente = Number(cliente?.Adeudo) || 0;
  const total = subtotal + adeudoCliente;

  const numEfectivo = parseFloat(pagoEfectivo) || 0;
  const numTarjeta = parseFloat(pagoTarjeta) || 0;
  const numTransfer = parseFloat(pagoTransfer) || 0;
  const totalPagado = numEfectivo + numTarjeta + numTransfer;
  const restante = Math.max(0, total - totalPagado);
  const cambio = Math.max(0, totalPagado - total);

  const abrirModalCobro = () => {
    if (hayErrorStock) {
      const itErr = itemsConErrorStock[0];
      setAdvertenciaStock(`No se puede cobrar: el artículo "${itErr.Descripcion}" supera las existencias disponibles (${itErr.Existencias || 0} disponibles, ${itErr.Cantidad} solicitados).`);
      return;
    }
    setMostrarTicketMobile(false);
    // Al abrir el modal, pre-llenar con Efectivo el total exacto por defecto
    setPagoEfectivo(total > 0 ? total.toString() : '');
    setPagoTarjeta('');
    setPagoTransfer('');
    setModalCobro(true);
  };

  const pagarTodoCon = (metodo: 'efectivo' | 'tarjeta' | 'transferencia') => {
    if (metodo === 'efectivo') {
      setPagoEfectivo(total > 0 ? total.toString() : '');
      setPagoTarjeta('');
      setPagoTransfer('');
    } else if (metodo === 'tarjeta') {
      setPagoEfectivo('');
      setPagoTarjeta(total > 0 ? total.toString() : '');
      setPagoTransfer('');
    } else {
      setPagoEfectivo('');
      setPagoTarjeta('');
      setPagoTransfer(total > 0 ? total.toString() : '');
    }
  };

  const completarRestante = (metodo: 'efectivo' | 'tarjeta' | 'transferencia') => {
    if (metodo === 'efectivo') {
      const faltante = Math.max(0, total - (numTarjeta + numTransfer));
      setPagoEfectivo(faltante > 0 ? faltante.toFixed(2) : '');
    } else if (metodo === 'tarjeta') {
      const faltante = Math.max(0, total - (numEfectivo + numTransfer));
      setPagoTarjeta(faltante > 0 ? faltante.toFixed(2) : '');
    } else {
      const faltante = Math.max(0, total - (numEfectivo + numTarjeta));
      setPagoTransfer(faltante > 0 ? faltante.toFixed(2) : '');
    }
  };

  const procesarCobro = async () => {
    if (carrito.length === 0) return;
    if (hayErrorStock) {
      const itErr = itemsConErrorStock[0];
      setAdvertenciaStock(`Existencias insuficientes para "${itErr.Descripcion}". No es posible continuar con la venta.`);
      return;
    }
    try {
      const res = await api.crearVenta({
        clienteId: cliente?.Id || 1,
        pagoEfectivo: numEfectivo,
        pagoTarjeta: numTarjeta,
        pagoTransfer: numTransfer,
        descuento: 0,
        items: carrito
      });

      if (res.success) {
        // Preparar datos para imprimir la Nota de Venta (Media Hoja o Ticket)
        const folioReal = res.folio || res.Folio || ('V' + new Date().toISOString().slice(0, 10).replace(/-/g, '') + '0001');
        const datosNota: NotaVentaData = {
          folio: folioReal,
          fecha: new Date().toISOString().slice(0, 10),
          hora: new Date().toLocaleTimeString('es-MX', { hour: '2-digit', minute: '2-digit' }),
          usuario: usuarioActualNombre,
          cliente: {
            Nombre: cliente ? cliente.Nombre : 'Mostrador',
            Direccion: cliente?.Direccion || '',
            Telcel: cliente?.Telcel || '',
            Telcasa: cliente?.Telcasa || '',
            RFC: cliente?.RFC || ''
          },
          items: carrito.map(it => ({
            Clave: it.Clave,
            Descripcion: it.Descripcion,
            Cantidad: it.Cantidad,
            Precio: it.Precio,
            Importe: it.Total,
            Paquete: it.Paquete
          })),
          subtotal: res.subtotal,
          adeudoPrevio: res.adeudoPrevio,
          total: res.total,
          pagoEfectivo: numEfectivo,
          pagoTarjeta: numTarjeta,
          pagoTransfer: numTransfer,
          cambio: res.cambio,
          nuevoAdeudo: res.nuevoAdeudo
        };

        // Si el cliente liquidó o modificó su adeudo, actualizar en caliente
        if (cliente && typeof res.nuevoAdeudo === 'number') {
          setCliente(prev => prev ? { ...prev, Adeudo: res.nuevoAdeudo } : null);
        }

        setCarrito([]);
        setModalCobro(false);
        setPagoEfectivo('');
        setPagoTarjeta('');
        setPagoTransfer('');
        setNotaVentaFinalizada(datosNota);

        // Notificar a toda la aplicación para actualizar la libreta de ventas
        window.dispatchEvent(new CustomEvent('sicdin_venta_registrada', { detail: res }));
      }
    } catch (err: any) {
      setAdvertenciaStock(err.message || 'Error al cobrar');
    }
  };

  const esClienteMayoreo = Boolean(cliente && cliente.Mayoreo === 1);

  const sugerenciasFiltradas = sugerencias.filter(art => {
    const cleanTipo = (art.Tipo || '').trim().toLowerCase();
    const esServicio = cleanTipo === 'servicio' || cleanTipo === 'trabajo';
    const esInfinito = cleanTipo === 'infinito';
    const esArticulo = !esServicio && !esInfinito;
    const stock = Number(art.Existencias) || 0;

    if (filtroTipo === 'articulos' && !esArticulo) return false;
    if (filtroTipo === 'servicios' && !esServicio) return false;
    if (filtroTipo === 'infinitos' && !esInfinito) return false;

    if (soloConExistencias) {
      if (esServicio || esInfinito) return true;
      return stock > 0;
    }

    return true;
  });

  return (
    <div className="flex-1 flex h-full overflow-hidden">
      {/* Columna Izquierda: Búsqueda, Filtro Cliente y Resultados */}
      <div className="flex-1 flex flex-col border-r border-slate-800">
        {/* Cabecera interna de la venta y selector de cliente */}
        <div className="h-14 px-4 border-b border-slate-800 bg-slate-900/40 flex items-center justify-between text-xs text-slate-400 gap-4">
          <div className="flex items-center gap-2">
            <span className="font-semibold text-slate-200">{tabTitle}</span>
            <span className="bg-slate-800 px-2 py-0.5 rounded text-[11px] text-emerald-400 font-mono">EN VENTA</span>
          </div>

          {/* Selector de Cliente */}
          <div className="flex items-center gap-2">
            <button
              onClick={() => {
                setModalClientes(true);
                setTimeout(() => clienteInputRef.current?.focus(), 50);
              }}
              className="flex items-center gap-2 bg-slate-800/80 hover:bg-slate-800 px-3 py-1.5 rounded-lg border border-slate-700/80 hover:border-blue-500/50 transition cursor-pointer"
              title="Cambiar cliente (F3)"
            >
              <User className="w-3.5 h-3.5 text-blue-400" />
              <span className="font-semibold text-slate-200 text-xs">
                {cliente ? `${cliente.Id} - ${cliente.Nombre}` : 'Seleccionar Cliente (F3)'}
              </span>

              {esClienteMayoreo ? (
                <span className="bg-purple-950 text-purple-300 border border-purple-800/80 px-2 py-0.5 rounded text-[10px] font-bold tracking-wide">
                  MAYOREO
                </span>
              ) : (
                <span className="bg-slate-700/60 text-slate-300 px-2 py-0.5 rounded text-[10px] font-medium">
                  MENUDEO
                </span>
              )}

              {(cliente?.Adeudo ?? 0) > 0 && (
                <span className="bg-amber-950 text-amber-400 border border-amber-800/80 px-1.5 py-0.5 rounded text-[10px] font-mono">
                  Adeudo: ${(cliente?.Adeudo || 0).toFixed(2)}
                </span>
              )}
            </button>
          </div>

          <button
            onClick={onClose}
            title="Cerrar esta pestaña de venta"
            className="flex items-center gap-1 hover:text-rose-400 hover:bg-slate-800/80 px-2 py-1 rounded transition cursor-pointer"
          >
            <X className="w-3.5 h-3.5" />
            <span>Cerrar pestaña</span>
          </button>
        </div>

        {/* Buscador / Lector Código de Barras */}
        <div className="p-4 bg-slate-900/30 space-y-3">
          <div className="relative">
            <Search className="absolute left-3.5 top-3.5 text-slate-400 w-5 h-5" />
            <input
              ref={searchInputRef}
              type="text"
              value={query}
              onChange={e => setQuery(e.target.value)}
              placeholder="Escanear código de barras o escribir producto... (F2 búsqueda, F3 cliente)"
              className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-lg pl-11 pr-4 py-3 text-base text-white focus:outline-none placeholder-slate-500 shadow-inner"
            />
          </div>

          {/* Filtros de Tipo y Existencias */}
          <div className="flex flex-wrap items-center gap-1.5 text-xs">
            <button
              type="button"
              onClick={() => setFiltroTipo('todos')}
              className={`px-2.5 py-1 rounded-lg font-semibold transition cursor-pointer ${
                filtroTipo === 'todos'
                  ? 'bg-blue-600 text-white shadow-sm'
                  : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-slate-200 border border-slate-800'
              }`}
            >
              Todos
            </button>
            <button
              type="button"
              onClick={() => setFiltroTipo('articulos')}
              className={`px-2.5 py-1 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1 ${
                filtroTipo === 'articulos'
                  ? 'bg-blue-600 text-white shadow-sm'
                  : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-slate-200 border border-slate-800'
              }`}
            >
              <Package className="w-3 h-3" />
              <span>Artículos</span>
            </button>
            <button
              type="button"
              onClick={() => setFiltroTipo('servicios')}
              className={`px-2.5 py-1 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1 ${
                filtroTipo === 'servicios'
                  ? 'bg-purple-600 text-white shadow-sm'
                  : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-purple-300 border border-slate-800'
              }`}
            >
              <Wrench className="w-3 h-3" />
              <span>Servicios</span>
            </button>
            <button
              type="button"
              onClick={() => setFiltroTipo('infinitos')}
              className={`px-2.5 py-1 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1 ${
                filtroTipo === 'infinitos'
                  ? 'bg-emerald-600 text-white shadow-sm'
                  : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-emerald-300 border border-slate-800'
              }`}
            >
              <Infinity className="w-3 h-3" />
              <span>Infinitos</span>
            </button>

            <div className="h-4 w-[1px] bg-slate-800 mx-1" />

            <button
              type="button"
              onClick={() => setSoloConExistencias(prev => !prev)}
              className={`px-2.5 py-1 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1 border ${
                soloConExistencias
                  ? 'bg-emerald-600 text-white border-emerald-500 shadow-sm'
                  : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-emerald-300 border-slate-800'
              }`}
              title="Mostrar solo productos con existencias disponibles (>0)"
            >
              <Check className="w-3 h-3" />
              <span>Con existencias</span>
            </button>

            <div className="ml-auto text-[11px] text-slate-500 font-mono">
              {sugerenciasFiltradas.length} encontrados
            </div>
          </div>
        </div>

        {/* Lista de Sugerencias / Resultados */}
        <div className="flex-1 overflow-y-auto p-4 space-y-2">
          {sugerenciasFiltradas.map(art => {
            const tienePME = (art.PME || 0) > 0 && (art.Especial || 0) > 0;
            return (
              <div
                key={art.Clave}
                onClick={() => agregarAlCarrito(art)}
                className="flex items-center justify-between p-3.5 bg-slate-900 hover:bg-slate-800/80 border border-slate-800/80 hover:border-blue-500/50 rounded-lg cursor-pointer transition"
              >
                <div>
                  <div className="font-semibold text-slate-200">{art.Descripcion}</div>
                  <div className="text-xs text-slate-400 font-mono flex items-center gap-3 mt-1">
                    <span>Clave: {art.Clave}</span>
                    {art.cbarra && <span>Barra: {art.cbarra}</span>}
                    {(() => {
                      const t = (art.Tipo || '').toLowerCase().trim();
                      if (t === 'servicio' || t === 'trabajo') {
                        return (
                          <span className="px-1.5 py-0.5 rounded text-[11px] bg-purple-950/80 text-purple-300 border border-purple-800/80 font-bold">
                            🔧 Servicio
                          </span>
                        );
                      }
                      if (t === 'infinito') {
                        return (
                          <span className="px-1.5 py-0.5 rounded text-[11px] bg-emerald-950/80 text-emerald-300 border border-emerald-800/80 font-bold">
                            ♾️ Infinito
                          </span>
                        );
                      }
                      return (
                        <span className={`px-1.5 py-0.5 rounded text-[11px] ${art.Existencias > 0 ? 'bg-emerald-950 text-emerald-400 border border-emerald-800/60' : 'bg-rose-950 text-rose-400'}`}>
                          Stock: {art.Existencias} {art.Paquete || 'PZA'}
                        </span>
                      );
                    })()}
                  </div>
                </div>

                <div className="text-right flex flex-col items-end">
                  {esClienteMayoreo ? (
                    <>
                      <div className="text-lg font-bold text-emerald-400 font-mono">
                        ${(art.Mayoreo > 0 ? art.Mayoreo : art.Menudeo).toFixed(2)}
                        <span className="text-xs font-normal text-purple-400 ml-1.5">Mayoreo</span>
                      </div>
                      <div className="text-xs text-slate-400">Menudeo: ${art.Menudeo.toFixed(2)}</div>
                      {tienePME && (
                        <div className="text-[11px] font-semibold text-amber-400 bg-amber-950/60 border border-amber-800/60 px-1.5 py-0.5 rounded mt-0.5">
                          PME: ≥ {art.PME} pz a ${art.Especial.toFixed(2)}
                        </div>
                      )}
                    </>
                  ) : (
                    <>
                      <div className="text-lg font-bold text-emerald-400 font-mono">
                        ${art.Menudeo.toFixed(2)}
                        <span className="text-xs font-normal text-slate-400 ml-1.5">Menudeo</span>
                      </div>
                      <div className="text-xs text-slate-400">Mayoreo: ${art.Mayoreo.toFixed(2)}</div>
                      {tienePME && (
                        <div className="text-[10px] text-slate-500">
                          (PME mayoreo: ≥ {art.PME} pz a ${art.Especial.toFixed(2)})
                        </div>
                      )}
                    </>
                  )}
                </div>
              </div>
            );
          })}

          {sugerenciasFiltradas.length === 0 && (
            <div className="text-center py-16 text-slate-600 text-sm flex flex-col items-center gap-2">
              <ShoppingCart className="w-8 h-8 opacity-40" />
              <span>No se encontraron artículos con los filtros seleccionados</span>
            </div>
          )}
        </div>

        {/* Barra Flotante Inferior en Celular (cuando hay artículos en el carrito) */}
        {carrito.length > 0 && (
          <div className="lg:hidden p-3 bg-slate-900/95 backdrop-blur border-t border-slate-800 flex items-center justify-between gap-3 shrink-0 shadow-2xl z-20">
            <div
              onClick={() => setMostrarTicketMobile(true)}
              className="flex items-center gap-2.5 cursor-pointer"
            >
              <div className="relative p-2 bg-blue-600/20 text-blue-400 rounded-xl border border-blue-500/30">
                <ShoppingCart className="w-5 h-5" />
                <span className="absolute -top-1 -right-1 bg-blue-600 text-white text-[10px] font-bold w-4 h-4 rounded-full flex items-center justify-center">
                  {carrito.length}
                </span>
              </div>
              <div>
                <div className="text-[11px] text-slate-400 font-medium">Ticket actual</div>
                <div className="text-base font-bold font-mono text-emerald-400">${total.toFixed(2)}</div>
              </div>
            </div>

            <div className="flex items-center gap-2">
              <button
                type="button"
                onClick={() => setMostrarTicketMobile(true)}
                className="px-3 py-2 bg-slate-800 hover:bg-slate-700 text-slate-200 rounded-xl text-xs font-semibold border border-slate-700 cursor-pointer"
              >
                Ver Ticket
              </button>
              <button
                type="button"
                onClick={abrirModalCobro}
                disabled={hayErrorStock}
                className="px-4 py-2 bg-emerald-600 hover:bg-emerald-500 disabled:opacity-50 text-white rounded-xl text-xs font-bold shadow-lg shadow-emerald-950 flex items-center gap-1.5 cursor-pointer"
              >
                <DollarSign className="w-4 h-4" />
                <span>Cobrar</span>
              </button>
            </div>
          </div>
        )}
      </div>

      {/* Columna Derecha: Ticket / Carrito (Responsivo: Panel lateral en Desktop, Pantalla Completa deslizable en Móvil) */}
      <div
        className={`
          ${mostrarTicketMobile ? 'fixed inset-0 z-50 flex flex-col bg-slate-950 animate-in slide-in-from-bottom-5 duration-200' : 'hidden'}
          lg:flex lg:static lg:inset-auto lg:w-96 lg:flex-col lg:bg-slate-900/50 lg:border-l lg:border-slate-800
        `}
      >
        <div className="p-3.5 border-b border-slate-800 flex items-center justify-between bg-slate-900/90">
          <div className="font-bold flex items-center gap-2 text-sm text-slate-200">
            <ShoppingCart className="w-4 h-4 text-blue-400" />
            <span>Ticket de Venta</span>
          </div>
          <div className="flex items-center gap-2">
            {esClienteMayoreo && (
              <span className="text-[10px] bg-purple-950 text-purple-300 border border-purple-800 px-1.5 py-0.5 rounded font-bold">
                MAYOREO
              </span>
            )}
            <span className="text-xs bg-slate-800 px-2 py-0.5 rounded text-slate-400 font-mono">
              {carrito.length} items
            </span>
            {/* Botón para volver/cerrar ticket en celular */}
            <button
              type="button"
              onClick={() => setMostrarTicketMobile(false)}
              className="lg:hidden p-1.5 rounded-lg bg-slate-800 text-slate-300 hover:text-white cursor-pointer ml-1"
              title="Volver al catálogo"
            >
              <X className="w-4 h-4" />
            </button>
          </div>
        </div>

        {/* Items del Carrito */}
        <div className="flex-1 overflow-y-auto p-4 space-y-2">
          {carrito.map(it => {
            const esExento = esSinControlStock(it.Tipo);
            const stock = Number(it.Existencias) || 0;
            const tieneError = !esExento && (stock <= 0 || it.Cantidad > stock);

            return (
              <div
                key={it.Clave}
                className={`p-3 bg-slate-900 border rounded-lg flex flex-col gap-2 transition ${
                  tieneError ? 'border-rose-500/80 bg-rose-950/20' : 'border-slate-800'
                }`}
              >
                <div className="flex justify-between items-start">
                  <span className="text-sm font-medium text-slate-200 line-clamp-1">{it.Descripcion}</span>
                  <span className="text-sm font-bold text-emerald-400 font-mono">${it.Total.toFixed(2)}</span>
                </div>

                {tieneError && (
                  <div className="text-[11px] text-rose-300 bg-rose-950/80 border border-rose-800/80 rounded px-2 py-1 flex items-center gap-1.5 font-semibold">
                    <AlertTriangle className="w-3.5 h-3.5 text-rose-400 shrink-0" />
                    <span>
                      {stock <= 0
                        ? 'Producto agotado (0 existencias)'
                        : `Solicitas ${it.Cantidad} pero solo hay ${stock} en stock`}
                    </span>
                  </div>
                )}

                <div className="flex justify-between items-center text-xs">
                  <div className="flex items-center gap-1.5">
                    <span className="text-slate-300 font-mono">${it.Precio.toFixed(2)} c/u</span>

                    {/* Badge de tipo de precio aplicado */}
                    {it.tipoPrecio === 'PME Especial' ? (
                      <span className="bg-amber-950 text-amber-300 border border-amber-800/80 px-1.5 py-0.5 rounded text-[10px] font-bold flex items-center gap-1">
                        <Tag className="w-2.5 h-2.5" /> PME (≥{it.PME})
                      </span>
                    ) : it.tipoPrecio === 'Mayoreo' ? (
                      <span className="bg-purple-950 text-purple-300 border border-purple-800/80 px-1.5 py-0.5 rounded text-[10px] font-semibold">
                        Mayoreo
                      </span>
                    ) : (
                      <span className="bg-slate-800 text-slate-400 px-1.5 py-0.5 rounded text-[10px]">
                        Menudeo
                      </span>
                    )}
                  </div>

                  <div className="flex items-center gap-2">
                    <button
                      onClick={() => modificarCantidad(it.Clave, -1)}
                      className="p-1 bg-slate-800 hover:bg-slate-700 rounded text-slate-300 cursor-pointer"
                      title="Disminuir cantidad"
                    >
                      <Minus className="w-3 h-3" />
                    </button>
                    <span className="font-bold font-mono px-1 text-white">{it.Cantidad}</span>
                    <button
                      onClick={() => modificarCantidad(it.Clave, 1)}
                      className="p-1 bg-slate-800 hover:bg-slate-700 rounded text-slate-300 cursor-pointer"
                      title="Aumentar cantidad"
                    >
                      <Plus className="w-3 h-3" />
                    </button>
                    <button
                      onClick={() => modificarCantidad(it.Clave, -it.Cantidad)}
                      className="p-1 text-rose-400 hover:bg-rose-950/40 rounded ml-1 cursor-pointer"
                      title="Quitar de la venta"
                    >
                      <Trash2 className="w-3.5 h-3.5" />
                    </button>
                  </div>
                </div>
              </div>
            );
          })}

          {carrito.length === 0 && (
            <div className="text-center py-20 text-slate-600 text-sm">
              Carrito vacío
            </div>
          )}
        </div>

        {/* Resumen y Botón de Cobro: Subtotal, Adeudo y Total */}
        <div className="p-4 bg-slate-900 border-t border-slate-800 space-y-2.5">
          {/* Subtotal (Venta actual) */}
          <div className="flex justify-between items-center text-sm">
            <span className="text-slate-400">Subtotal (venta actual):</span>
            <span className="font-mono font-semibold text-slate-200">${subtotal.toFixed(2)}</span>
          </div>

          {/* Adeudo anterior */}
          <div className="flex justify-between items-center text-sm">
            <span className="text-slate-400 flex items-center gap-1.5">
              <span>Adeudo:</span>
              {adeudoCliente > 0 && (
                <span className="bg-amber-950 text-amber-400 border border-amber-800/80 text-[10px] px-1.5 py-0.5 rounded font-semibold">
                  Pendiente
                </span>
              )}
            </span>
            <span className={`font-mono font-semibold ${adeudoCliente > 0 ? 'text-amber-400 font-bold' : 'text-slate-400'}`}>
              ${adeudoCliente.toFixed(2)}
            </span>
          </div>

          {/* Total (Subtotal + Adeudo) */}
          <div className="pt-2 border-t border-slate-800 flex justify-between items-center">
            <span className="text-sm font-bold text-white">Total (Subtotal + Adeudo):</span>
            <span className="text-2xl text-emerald-400 font-mono font-bold">${total.toFixed(2)}</span>
          </div>

          {hayErrorStock && (
            <div className="p-2.5 bg-rose-950/60 border border-rose-800/80 rounded-lg text-rose-300 text-xs flex items-center gap-2 font-medium">
              <AlertTriangle className="w-4 h-4 text-rose-400 shrink-0" />
              <span>Hay productos que superan las existencias disponibles. Ajusta el ticket para cobrar.</span>
            </div>
          )}

          <button
            onClick={abrirModalCobro}
            disabled={carrito.length === 0 || hayErrorStock}
            className={`w-full font-bold py-3.5 rounded-lg flex items-center justify-center gap-2 transition text-base mt-2 shadow-lg ${
              carrito.length === 0 || hayErrorStock
                ? 'bg-slate-800 text-slate-500 cursor-not-allowed opacity-60 border border-slate-700'
                : 'bg-blue-600 hover:bg-blue-500 text-white cursor-pointer shadow-blue-900/30'
            }`}
          >
            <DollarSign className="w-5 h-5" />
            {hayErrorStock ? 'Existencias Insuficientes' : 'Cobrar (F12)'}
          </button>
        </div>
      </div>

      {/* Modal de Selección de Cliente (F3) */}
      {modalClientes && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm flex items-center justify-center p-0 sm:p-4 z-50">
          <div className="bg-slate-900 border-0 sm:border border-slate-800 rounded-none sm:rounded-xl max-w-lg w-full p-4 sm:p-6 shadow-2xl flex flex-col h-full sm:h-auto max-h-full sm:max-h-[85vh]">
            <div className="flex justify-between items-center mb-4">
              <h3 className="text-lg font-bold flex items-center gap-2 text-white">
                <Users className="w-5 h-5 text-blue-400" />
                Seleccionar Cliente
              </h3>
              <button
                onClick={() => setModalClientes(false)}
                className="text-slate-400 hover:text-white p-1 rounded-lg hover:bg-slate-800 transition cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {/* Buscador de Clientes */}
            <div className="relative mb-3">
              <Search className="absolute left-3.5 top-3 text-slate-400 w-4 h-4" />
              <input
                ref={clienteInputRef}
                type="text"
                value={queryCliente}
                onChange={e => setQueryCliente(e.target.value)}
                placeholder="Buscar por Nombre, ID, Teléfono o RFC..."
                className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg pl-10 pr-4 py-2.5 text-sm text-white focus:outline-none"
              />
            </div>

            {/* Botón rápido Mostrador */}
            <div className="mb-3">
              <button
                onClick={() => {
                  const mostrador = clientesEncontrados.find(c => c.Id === 1 || c.Nombre.toLowerCase().includes('mostrador')) || {
                    Id: 1,
                    Nombre: 'Mostrador',
                    Mayoreo: 0
                  };
                  seleccionarCliente(mostrador);
                }}
                className="w-full py-2 px-3 bg-slate-800/80 hover:bg-slate-800 border border-slate-700 rounded-lg text-xs font-semibold text-slate-200 flex items-center justify-between transition cursor-pointer"
              >
                <span className="flex items-center gap-2">
                  <User className="w-3.5 h-3.5 text-emerald-400" />
                  Mostrador (Público General - Menudeo)
                </span>
                <span className="text-slate-400 text-[11px]">ID: 1</span>
              </button>
            </div>

            {/* Lista de Clientes */}
            <div className="flex-1 overflow-y-auto space-y-2 pr-1">
              {clientesEncontrados.map(clnt => {
                const isSelected = cliente?.Id === clnt.Id;
                const esMay = clnt.Mayoreo === 1;

                return (
                  <div
                    key={clnt.Id}
                    onClick={() => seleccionarCliente(clnt)}
                    className={`p-3 rounded-lg border transition cursor-pointer flex items-center justify-between ${
                      isSelected
                        ? 'bg-blue-950/40 border-blue-600/80'
                        : 'bg-slate-950/60 border-slate-800 hover:border-slate-700 hover:bg-slate-800/40'
                    }`}
                  >
                    <div>
                      <div className="flex items-center gap-2">
                        <span className="font-semibold text-slate-200 text-sm">{clnt.Nombre}</span>
                        <span className="text-xs text-slate-500 font-mono">#{clnt.Id}</span>
                        {esMay ? (
                          <span className="bg-purple-950 text-purple-300 border border-purple-800/80 px-1.5 py-0.5 rounded text-[10px] font-bold">
                            MAYOREO
                          </span>
                        ) : (
                          <span className="bg-slate-800 text-slate-400 px-1.5 py-0.5 rounded text-[10px]">
                            Menudeo
                          </span>
                        )}
                      </div>

                      <div className="text-xs text-slate-400 mt-1 flex items-center gap-3">
                        {clnt.Telcel && <span>Tel: {clnt.Telcel}</span>}
                        {clnt.Direccion && <span className="line-clamp-1">{clnt.Direccion}</span>}
                        {(clnt.Adeudo ?? 0) > 0 && (
                          <span className="text-amber-400 font-semibold font-mono">
                            Adeudo: ${(clnt.Adeudo || 0).toFixed(2)}
                          </span>
                        )}
                      </div>
                    </div>

                    <div className="text-right">
                      {isSelected ? (
                        <span className="flex items-center gap-1 text-blue-400 text-xs font-semibold">
                          <Check className="w-4 h-4" /> Seleccionado
                        </span>
                      ) : (
                        <ArrowRight className="w-4 h-4 text-slate-600" />
                      )}
                    </div>
                  </div>
                );
              })}

              {!cargandoClientes && clientesEncontrados.length === 0 && (
                <div className="text-center py-10 text-slate-500 text-xs">
                  No se encontraron clientes con "{queryCliente}"
                </div>
              )}
            </div>
          </div>
        </div>
      )}

      {/* Modal de Cobro (F12) */}
      {modalCobro && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm flex items-center justify-center p-0 sm:p-4 z-50 overflow-y-auto">
          <div className="bg-slate-900 border-0 sm:border border-slate-800 rounded-none sm:rounded-2xl max-w-md w-full shadow-2xl flex flex-col overflow-hidden h-full sm:h-auto max-h-full sm:max-h-[88vh]">
            {/* Cabecera Fija */}
            <div className="p-4 sm:px-5 border-b border-slate-800 flex justify-between items-center shrink-0 bg-slate-900">
              <h3 className="text-lg font-bold flex items-center gap-2 text-white">
                <DollarSign className="w-5 h-5 text-emerald-400" />
                Finalizar Venta
              </h3>
              <button
                type="button"
                onClick={() => setModalCobro(false)}
                className="text-slate-400 hover:text-white p-1 rounded-lg hover:bg-slate-800 transition cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {/* Cuerpo con Scroll para Zoom */}
            <div className="p-4 sm:px-5 space-y-4 overflow-y-auto flex-1 text-sm">
              <div className="p-3 bg-slate-950 rounded-lg flex justify-between items-center text-xs text-slate-400 border border-slate-800">
                <span>Cliente:</span>
                <span className="font-semibold text-slate-200">
                  {cliente ? `${cliente.Nombre} (${cliente.Mayoreo === 1 ? 'Mayoreo' : 'Menudeo'})` : 'Mostrador'}
                </span>
              </div>

              {/* Desglose: Subtotal, Adeudo y Total */}
              <div className="p-3 bg-slate-950 rounded-lg space-y-2 border border-slate-800">
                <div className="flex justify-between items-center text-sm">
                  <span className="text-slate-400">Subtotal (venta actual):</span>
                  <span className="font-mono font-semibold text-slate-200">${subtotal.toFixed(2)}</span>
                </div>

                <div className="flex justify-between items-center text-sm">
                  <span className="text-slate-400 flex items-center gap-1.5">
                    <span>Adeudo:</span>
                    {adeudoCliente > 0 && (
                      <span className="bg-amber-950 text-amber-400 border border-amber-800/80 text-[10px] px-1.5 py-0.2 rounded font-semibold">
                        Pendiente
                      </span>
                    )}
                  </span>
                  <span className={`font-mono font-semibold ${adeudoCliente > 0 ? 'text-amber-400 font-bold' : 'text-slate-400'}`}>
                    ${adeudoCliente.toFixed(2)}
                  </span>
                </div>

                <div className="pt-2 border-t border-slate-800 flex justify-between items-center">
                  <span className="text-sm font-bold text-white">Total (Subtotal + Adeudo):</span>
                  <span className="text-2xl font-bold font-mono text-emerald-400">${total.toFixed(2)}</span>
                </div>
              </div>

              {/* Botones de Pago Rápido (100% de una sola vía) */}
              <div className="grid grid-cols-3 gap-2">
                <button
                  type="button"
                  onClick={() => pagarTodoCon('efectivo')}
                  className={`py-2 px-2.5 rounded-lg border text-xs font-semibold flex items-center justify-center gap-1.5 transition cursor-pointer ${
                    numEfectivo >= total && numTarjeta === 0 && numTransfer === 0
                      ? 'bg-emerald-600 text-white border-emerald-500 shadow-sm'
                      : 'bg-slate-800/80 hover:bg-slate-800 text-slate-300 border-slate-700'
                  }`}
                >
                  <Banknote className="w-3.5 h-3.5 text-emerald-400" />
                  <span>Efectivo 100%</span>
                </button>

                <button
                  type="button"
                  onClick={() => pagarTodoCon('tarjeta')}
                  className={`py-2 px-2.5 rounded-lg border text-xs font-semibold flex items-center justify-center gap-1.5 transition cursor-pointer ${
                    numTarjeta === total && numEfectivo === 0 && numTransfer === 0
                      ? 'bg-blue-600 text-white border-blue-500 shadow-sm'
                      : 'bg-slate-800/80 hover:bg-slate-800 text-slate-300 border-slate-700'
                  }`}
                >
                  <CreditCard className="w-3.5 h-3.5 text-blue-400" />
                  <span>Tarjeta 100%</span>
                </button>

                <button
                  type="button"
                  onClick={() => pagarTodoCon('transferencia')}
                  className={`py-2 px-2.5 rounded-lg border text-xs font-semibold flex items-center justify-center gap-1.5 transition cursor-pointer ${
                    numTransfer === total && numEfectivo === 0 && numTarjeta === 0
                      ? 'bg-purple-600 text-white border-purple-500 shadow-sm'
                      : 'bg-slate-800/80 hover:bg-slate-800 text-slate-300 border-slate-700'
                  }`}
                >
                  <ArrowLeftRight className="w-3.5 h-3.5 text-purple-400" />
                  <span>Transfer 100%</span>
                </button>
              </div>

              {/* Campos divididos de Pago */}
              <div className="space-y-3 pt-1">
                {/* 1. Efectivo */}
                <div className="bg-slate-950/70 p-2.5 rounded-lg border border-slate-800 flex items-center justify-between gap-3">
                  <div className="flex items-center gap-2 min-w-[110px]">
                    <div className="p-1.5 rounded-md bg-emerald-950 text-emerald-400 border border-emerald-800/60">
                      <Banknote className="w-4 h-4" />
                    </div>
                    <span className="text-xs font-semibold text-slate-200">Efectivo ($)</span>
                  </div>

                  <div className="flex items-center gap-2 flex-1 justify-end">
                    <input
                      type="number"
                      autoFocus
                      step="any"
                      min="0"
                      inputMode="decimal"
                      value={pagoEfectivo}
                      onChange={e => setPagoEfectivo(e.target.value)}
                      placeholder="0.00"
                      className="w-32 bg-slate-900 border border-slate-700 focus:border-emerald-500 rounded px-3 py-1.5 text-right font-mono text-base text-white focus:outline-none"
                    />
                    <button
                      type="button"
                      onClick={() => completarRestante('efectivo')}
                      title="Completar el saldo restante con Efectivo"
                      className="px-2 py-1.5 text-[11px] bg-slate-800 hover:bg-slate-700 text-slate-300 hover:text-white rounded border border-slate-700 transition cursor-pointer shrink-0"
                    >
                      Resto
                    </button>
                  </div>
                </div>

                {/* 2. Tarjeta */}
                <div className="bg-slate-950/70 p-2.5 rounded-lg border border-slate-800 flex items-center justify-between gap-3">
                  <div className="flex items-center gap-2 min-w-[110px]">
                    <div className="p-1.5 rounded-md bg-blue-950 text-blue-400 border border-blue-800/60">
                      <CreditCard className="w-4 h-4" />
                    </div>
                    <span className="text-xs font-semibold text-slate-200">Tarjeta ($)</span>
                  </div>

                  <div className="flex items-center gap-2 flex-1 justify-end">
                    <input
                      type="number"
                      step="any"
                      min="0"
                      inputMode="decimal"
                      value={pagoTarjeta}
                      onChange={e => setPagoTarjeta(e.target.value)}
                      placeholder="0.00"
                      className="w-32 bg-slate-900 border border-slate-700 focus:border-blue-500 rounded px-3 py-1.5 text-right font-mono text-base text-white focus:outline-none"
                    />
                    <button
                      type="button"
                      onClick={() => completarRestante('tarjeta')}
                      title="Completar el saldo restante con Tarjeta"
                      className="px-2 py-1.5 text-[11px] bg-slate-800 hover:bg-slate-700 text-slate-300 hover:text-white rounded border border-slate-700 transition cursor-pointer shrink-0"
                    >
                      Resto
                    </button>
                  </div>
                </div>

                {/* 3. Transferencia */}
                <div className="bg-slate-950/70 p-2.5 rounded-lg border border-slate-800 flex items-center justify-between gap-3">
                  <div className="flex items-center gap-2 min-w-[110px]">
                    <div className="p-1.5 rounded-md bg-purple-950 text-purple-400 border border-purple-800/60">
                      <ArrowLeftRight className="w-4 h-4" />
                    </div>
                    <span className="text-xs font-semibold text-slate-200">Transfer ($)</span>
                  </div>

                  <div className="flex items-center gap-2 flex-1 justify-end">
                    <input
                      type="number"
                      step="any"
                      min="0"
                      inputMode="decimal"
                      value={pagoTransfer}
                      onChange={e => setPagoTransfer(e.target.value)}
                      placeholder="0.00"
                      className="w-32 bg-slate-900 border border-slate-700 focus:border-purple-500 rounded px-3 py-1.5 text-right font-mono text-base text-white focus:outline-none"
                    />
                    <button
                      type="button"
                      onClick={() => completarRestante('transferencia')}
                      title="Completar el saldo restante con Transferencia"
                      className="px-2 py-1.5 text-[11px] bg-slate-800 hover:bg-slate-700 text-slate-300 hover:text-white rounded border border-slate-700 transition cursor-pointer shrink-0"
                    >
                      Resto
                    </button>
                  </div>
                </div>
              </div>

              {/* Resumen del Cobro: Pagado vs Restante o Cambio */}
              <div className="p-3 bg-slate-950 rounded-lg space-y-1.5 border border-slate-800 text-xs">
                <div className="flex justify-between items-center text-slate-400">
                  <span>Total entregado (suma):</span>
                  <span className="font-mono font-bold text-slate-200 text-sm">${totalPagado.toFixed(2)}</span>
                </div>

                {/* Indicador de Faltante / Adeudo si falta dinero */}
                {restante > 0 && (
                  <div className="pt-2 border-t border-slate-800/80 space-y-1">
                    <div className="flex justify-between items-center text-rose-400 font-semibold">
                      <span>Monto faltante por cubrir:</span>
                      <span className="font-mono font-bold text-sm">${restante.toFixed(2)}</span>
                    </div>

                    {cliente && cliente.Id > 1 && (cliente.Credito ?? 0) > 0 ? (
                      <div className="bg-amber-950/40 border border-amber-800/60 p-2 rounded text-[11px] text-amber-300 space-y-0.5">
                        <div className="flex justify-between font-semibold">
                          <span>✓ Se sumará al Adeudo del cliente:</span>
                          <span className="font-mono text-amber-200">+${restante.toFixed(2)}</span>
                        </div>
                        <div className="flex justify-between text-slate-400 text-[10px]">
                          <span>Nuevo saldo deudor total:</span>
                          <span className="font-mono text-amber-300 font-semibold">
                            ${(adeudoCliente + restante).toFixed(2)} (Límite: ${(cliente.Credito || 0).toFixed(2)})
                          </span>
                        </div>
                        {(adeudoCliente + restante) > (cliente.Credito || 0) && (cliente.Credito || 0) > 0 && (
                          <div className="text-[10px] text-rose-400 font-bold pt-0.5">
                            ⚠️ El nuevo saldo superará el límite de crédito autorizado.
                          </div>
                        )}
                      </div>
                    ) : cliente && cliente.Id === 1 ? (
                      <div className="bg-rose-950/40 border border-rose-800/60 p-2 rounded text-[11px] text-rose-300 font-semibold">
                        ⚠️ Mostrador / Público General: No se permiten ventas a crédito. Se debe liquidar el total.
                      </div>
                    ) : (
                      <div className="bg-amber-950/40 border border-amber-800/60 p-2 rounded text-[11px] text-amber-300 font-semibold">
                        ⚠️ Este cliente no tiene límite de crédito asignado. Se cargará a su adeudo al confirmar.
                      </div>
                    )}
                  </div>
                )}

                {cambio > 0 && (
                  <div className="flex justify-between items-center text-emerald-400 pt-1 border-t border-slate-800/60">
                    <span className="font-semibold">Cambio a entregar:</span>
                    <span className="font-mono font-bold text-base">${cambio.toFixed(2)}</span>
                  </div>
                )}

                {totalPagado === total && (
                  <div className="flex justify-between items-center text-emerald-400 pt-1 border-t border-slate-800/60 font-semibold">
                    <span>Monto exacto cubierto</span>
                    <span>✓ Sin cambio</span>
                  </div>
                )}
              </div>
            </div>

            {/* Botones de Acción Fijos */}
            <div className="flex gap-3 p-3.5 sm:px-5 border-t border-slate-800 bg-slate-900/95 shrink-0">
              <button
                type="button"
                onClick={() => setModalCobro(false)}
                className="flex-1 bg-slate-800 hover:bg-slate-700 py-2.5 rounded-lg text-sm text-slate-300 cursor-pointer"
              >
                Cancelar (Esc)
              </button>
              <button
                type="button"
                disabled={restante > 0 && cliente?.Id === 1}
                onClick={procesarCobro}
                className={`flex-1 py-2.5 rounded-lg text-sm font-bold text-white transition flex items-center justify-center gap-1.5 shadow-lg ${
                  restante > 0 && cliente?.Id === 1
                    ? 'bg-slate-800 text-slate-500 cursor-not-allowed opacity-50'
                    : restante > 0
                    ? 'bg-amber-600 hover:bg-amber-500 shadow-amber-950 cursor-pointer'
                    : 'bg-emerald-600 hover:bg-emerald-500 shadow-emerald-950 cursor-pointer'
                }`}
              >
                <Check className="w-4 h-4" />
                {restante > 0 ? 'Confirmar con Adeudo' : 'Confirmar Cobro'}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal de Impresión de Nota de Venta (Media Hoja / Ticket) */}
      {notaVentaFinalizada && (
        <NotaVentaModal
          datos={notaVentaFinalizada}
          onCerrar={() => {
            setNotaVentaFinalizada(null);
            onClose();
          }}
        />
      )}

      {/* Modal / Diálogo de Advertencia de Stock Agotado / Insuficiente */}
      {advertenciaStock && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm flex items-center justify-center p-4 z-[9999] animate-in fade-in duration-150">
          <div className="bg-slate-900 border-2 border-rose-500/80 rounded-2xl max-w-md w-full p-6 shadow-2xl text-center space-y-4 animate-in zoom-in-95 duration-200">
            <div className="w-14 h-14 bg-rose-500/20 text-rose-400 rounded-full flex items-center justify-center mx-auto border border-rose-500/40">
              <AlertTriangle className="w-7 h-7 stroke-[2.5]" />
            </div>
            <div>
              <h3 className="text-lg font-bold text-white">Existencias Insuficientes</h3>
              <p className="text-sm text-slate-300 mt-2 leading-relaxed">
                {advertenciaStock}
              </p>
            </div>
            <div className="pt-2">
              <button
                type="button"
                onClick={() => setAdvertenciaStock(null)}
                className="w-full bg-rose-600 hover:bg-rose-500 text-white font-bold py-2.5 rounded-xl text-sm transition cursor-pointer shadow-lg shadow-rose-950"
              >
                Entendido
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
