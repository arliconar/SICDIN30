import { useState, useEffect } from 'react';
import {
  Receipt,
  ChevronLeft,
  ChevronRight,
  RefreshCw,
  Search,
  Zap,
  Banknote,
  CreditCard,
  ArrowLeftRight,
  User,
  Clock,
  Package,
  Printer
} from 'lucide-react';
import { api, type ResumenVentasDia, type MovimientoDia } from './api';
import WinFormsDatePicker from './WinFormsDatePicker';
import NotaVentaModal, { type NotaVentaData } from './NotaVentaModal';

interface LibretaVentasViewProps {
  activa?: boolean;
}

const getFechaLocalHoy = () => new Date().toLocaleDateString('en-CA');

export default function LibretaVentasView({ activa = true }: LibretaVentasViewProps) {
  const [fecha, setFecha] = useState<string>(getFechaLocalHoy);
  const [datos, setDatos] = useState<ResumenVentasDia | null>(null);
  const [cargando, setCargando] = useState<boolean>(false);
  const [busqueda, setBusqueda] = useState<string>('');
  const [filtroTipo, setFiltroTipo] = useState<'TODOS' | 'RAPIDAS' | 'NOTAS'>('TODOS');
  const [notaSeleccionada, setNotaSeleccionada] = useState<NotaVentaData | null>(null);
  const [cargandoRowId, setCargandoRowId] = useState<number | null>(null);

  // Cargar ventas al montar, cuando cambia la fecha o cuando la pestaña pasa a activa
  useEffect(() => {
    if (activa) {
      cargarVentas(fecha);
    }
  }, [fecha, activa]);

  // Recarga reactiva en tiempo real al registrar ventas en cualquier parte del sistema o cambiar foco
  useEffect(() => {
    const handleVenta = () => {
      cargarVentas(fecha);
    };

    window.addEventListener('sicdin_venta_registrada', handleVenta);
    window.addEventListener('focus', handleVenta);

    // Auto-actualización periódica de caja cada 12 segundos cuando la pestaña está activa
    const interval = activa
      ? setInterval(() => {
          cargarVentas(fecha);
        }, 12000)
      : null;

    return () => {
      window.removeEventListener('sicdin_venta_registrada', handleVenta);
      window.removeEventListener('focus', handleVenta);
      if (interval) clearInterval(interval);
    };
  }, [fecha, activa]);

  const cargarVentas = async (fechaConsulta: string) => {
    setCargando(true);
    try {
      const res = await api.obtenerVentasDia(fechaConsulta);
      setDatos(res);
    } catch (err) {
      console.error('Error al cargar ventas del día:', err);
    } finally {
      setCargando(false);
    }
  };

  const cambiarDia = (offset: number) => {
    const d = new Date(fecha + 'T12:00:00');
    d.setDate(d.getDate() + offset);
    setFecha(d.toLocaleDateString('en-CA'));
  };

  const irAHoy = () => {
    setFecha(getFechaLocalHoy());
  };

  const reimprimirMovimiento = async (m: MovimientoDia) => {
    setCargandoRowId(m.rowid);
    try {
      if (m.nota === 1 && m.Clave) {
        // Buscar el detalle completo de la nota de venta por folio
        const det = await api.obtenerDetalleVenta(m.Clave);
        if (det && det.venta) {
          const v = det.venta;
          const cl = det.cliente || {};
          const arts = (det.articulos || []).map(a => ({
            Clave: a.Clave || '',
            Descripcion: a.Descripcion || '',
            Cantidad: Number(a.Cantidad) || 1,
            Precio: Number(a.Cantidad) > 0 ? (Number(a.Importe) / Number(a.Cantidad)) : Number(a.Importe),
            Importe: Number(a.Importe) || 0,
            Paquete: a.Paquete || ''
          }));

          const sub = Number(v.Total || m.Total) + Number(v.Descuento || m.Descuento || 0);
          const efe = Number(v.Pagoefe || m.Pagoefe || 0);
          const tar = Number(v.Pagotar || m.Pagotar || 0);
          const tra = Number(v.Pagotrans || m.Pagotrans || 0);
          const tot = Number(v.Total || m.Total || 0);
          const cmb = Math.max(0, (efe + tar + tra) - tot);

          const folioReal = det.folio || det.venta?.Folio || m.Clave || `V-${m.rowid}`;

          setNotaSeleccionada({
            folio: folioReal,
            fecha: v.Fecha || m.fecha,
            hora: m.horast || m.hora || '',
            usuario: v.Usuario || m.usuario || 'ADMIN',
            cliente: {
              Nombre: cl.Nombre || m.Cliente || 'Mostrador',
              Direccion: cl.Direccion || '',
              Telcel: cl.telcel || '',
              Telcasa: cl.telcasa || '',
              RFC: cl.RFC || '',
              mail: cl.mail || ''
            },
            items: arts.length > 0 ? arts : [
              {
                Clave: m.Clave || '',
                Descripcion: m.Descripcion || 'Nota de venta',
                Cantidad: m.cantidad || 1,
                Precio: (Number(m.Total) || 0) / (m.cantidad || 1),
                Importe: Number(m.Total) || 0,
                Paquete: ''
              }
            ],
            subtotal: sub,
            descuento: Number(v.Descuento || m.Descuento || 0),
            adeudoPrevio: Number(v.adeudo || 0),
            total: tot,
            pagoEfectivo: efe,
            pagoTarjeta: tar,
            pagoTransfer: tra,
            cambio: cmb,
            nuevoAdeudo: Number(v.Resta || 0)
          });
          return;
        }
      }

      // Si es venta rápida o no se encontró folio formal, generar nota directa
      const tot = Number(m.Total) || Number(m.Pagado) || 0;
      const desc = Number(m.Descuento || 0);
      const efe = Number(m.Pagoefe || 0);
      const tar = Number(m.Pagotar || 0);
      const tra = Number(m.Pagotrans || 0);

      setNotaSeleccionada({
        folio: m.Clave || `VR-${m.rowid}`,
        fecha: m.fecha,
        hora: m.horast || m.hora || '',
        usuario: m.usuario || 'ADMIN',
        cliente: {
          Nombre: m.Cliente || 'Venta Rápida de Mostrador'
        },
        items: [
          {
            Clave: m.Clave || 'RAPIDA',
            Descripcion: m.Descripcion || 'Venta de Mostrador',
            Cantidad: m.cantidad || 1,
            Precio: tot / (m.cantidad || 1),
            Importe: tot,
            Paquete: ''
          }
        ],
        subtotal: tot + desc,
        descuento: desc,
        adeudoPrevio: 0,
        total: tot,
        pagoEfectivo: efe,
        pagoTarjeta: tar,
        pagoTransfer: tra,
        cambio: Math.max(0, (efe + tar + tra) - tot),
        nuevoAdeudo: 0
      });
    } catch (err) {
      console.error('Error al preparar impresión:', err);
      // Fallback básico
      const tot = Number(m.Total) || Number(m.Pagado) || 0;
      setNotaSeleccionada({
        folio: m.Clave || `MOV-${m.rowid}`,
        fecha: m.fecha,
        hora: m.horast || m.hora || '',
        usuario: m.usuario || 'ADMIN',
        cliente: { Nombre: m.Cliente || 'Mostrador' },
        items: [
          {
            Clave: m.Clave || '',
            Descripcion: m.Descripcion || 'Venta',
            Cantidad: m.cantidad || 1,
            Precio: tot / (m.cantidad || 1),
            Importe: tot
          }
        ],
        subtotal: tot,
        total: tot,
        pagoEfectivo: Number(m.Pagoefe || 0),
        pagoTarjeta: Number(m.Pagotar || 0),
        pagoTransfer: Number(m.Pagotrans || 0)
      });
    } finally {
      setCargandoRowId(null);
    }
  };

  // Filtrado reactivo de movimientos
  const movimientosFiltrados = (datos?.movimientos || []).filter(m => {
    // Filtro por tipo
    if (filtroTipo === 'RAPIDAS' && m.nota !== 0) return false;
    if (filtroTipo === 'NOTAS' && m.nota !== 1) return false;

    // Filtro por texto de búsqueda
    if (busqueda.trim()) {
      const q = busqueda.toLowerCase();
      const matchClave = (m.Clave || '').toLowerCase().includes(q);
      const matchDesc = (m.Descripcion || '').toLowerCase().includes(q);
      const matchClnt = (m.Cliente || '').toLowerCase().includes(q);
      const matchUsu = (m.usuario || '').toLowerCase().includes(q);
      if (!matchClave && !matchDesc && !matchClnt && !matchUsu) {
        return false;
      }
    }

    return true;
  });

  const conteoRapidas = (datos?.movimientos || []).filter(m => m.nota === 0).length;
  const conteoNotas = (datos?.movimientos || []).filter(m => m.nota === 1).length;

  return (
    <div className="flex-1 flex flex-col h-full bg-slate-950 text-slate-100 overflow-hidden select-none">
      {/* 1. Barra Superior con Navegación de Fecha y Totales */}
      <div className="p-4 bg-slate-900/70 border-b border-slate-800 flex flex-wrap items-center justify-between gap-4">
        {/* Título de la pestaña */}
        <div className="flex items-center gap-3">
          <div className="p-2.5 bg-blue-500/10 text-blue-400 rounded-xl border border-blue-500/20">
            <Receipt className="w-5 h-5" />
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h1 className="text-lg font-bold text-white tracking-wide">Libreta de Ventas</h1>
              <span className="bg-blue-950 text-blue-300 border border-blue-800/80 px-2 py-0.5 rounded text-[10px] font-bold">
                DIARIO DE CAJA
              </span>
            </div>
            <p className="text-xs text-slate-400">Control de ventas del día, ventas rápidas y notas de venta</p>
          </div>
        </div>

        {/* Controles de Navegación de Fecha con Desplegable Estilo Windows Forms */}
        <div className="flex items-center gap-2">
          <button
            onClick={() => cambiarDia(-1)}
            title="Día anterior"
            className="p-2 bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-white border border-slate-800 rounded-xl transition cursor-pointer"
          >
            <ChevronLeft className="w-4 h-4" />
          </button>

          <WinFormsDatePicker
            value={fecha}
            onChange={setFecha}
          />

          <button
            onClick={() => cambiarDia(1)}
            title="Día siguiente"
            className="p-2 bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-white border border-slate-800 rounded-xl transition cursor-pointer"
          >
            <ChevronRight className="w-4 h-4" />
          </button>

          <button
            onClick={irAHoy}
            className={`px-3 py-2 rounded-xl text-xs font-bold border transition cursor-pointer ${
              fecha === new Date().toISOString().slice(0, 10)
                ? 'bg-blue-600 text-white border-blue-500 shadow-sm'
                : 'bg-slate-900 hover:bg-slate-800 text-slate-300 border-slate-800'
            }`}
          >
            Hoy
          </button>

          <button
            onClick={() => cargarVentas(fecha)}
            disabled={cargando}
            title="Refrescar ventas de este día"
            className="p-2 bg-slate-900 hover:bg-slate-800 text-slate-300 hover:text-white border border-slate-800 rounded-xl transition cursor-pointer"
          >
            <RefreshCw className={`w-4 h-4 ${cargando ? 'animate-spin text-blue-400' : ''}`} />
          </button>
        </div>
      </div>

      {/* 2. Tarjetas KPI de Resumen del Día */}
      <div className="p-4 bg-slate-900/30 border-b border-slate-800/80">
        <div className="grid grid-cols-2 sm:grid-cols-5 gap-3">
          {/* Total del Día */}
          <div className="bg-slate-900/90 border border-slate-800 p-3.5 rounded-xl shadow-sm">
            <div className="text-[10px] uppercase font-bold text-slate-400 tracking-wider">
              Total del Día
            </div>
            <div className="text-2xl font-bold font-mono text-emerald-400 mt-0.5">
              ${(datos?.totalVentas || 0).toFixed(2)}
            </div>
            <div className="text-[10px] text-slate-500 font-mono mt-1">
              Ingresos netos registrados
            </div>
          </div>

          {/* Efectivo */}
          <div className="bg-slate-900/90 border border-slate-800 p-3.5 rounded-xl shadow-sm">
            <div className="text-[10px] uppercase font-bold text-emerald-400 tracking-wider flex items-center gap-1">
              <Banknote className="w-3.5 h-3.5" />
              <span>Efectivo</span>
            </div>
            <div className="text-xl font-bold font-mono text-white mt-0.5">
              ${(datos?.totalEfectivo || 0).toFixed(2)}
            </div>
            <div className="text-[10px] text-slate-500 font-mono mt-1">
              Dinero en caja física
            </div>
          </div>

          {/* Tarjeta */}
          <div className="bg-slate-900/90 border border-slate-800 p-3.5 rounded-xl shadow-sm">
            <div className="text-[10px] uppercase font-bold text-blue-400 tracking-wider flex items-center gap-1">
              <CreditCard className="w-3.5 h-3.5" />
              <span>Tarjeta</span>
            </div>
            <div className="text-xl font-bold font-mono text-blue-300 mt-0.5">
              ${(datos?.totalTarjeta || 0).toFixed(2)}
            </div>
            <div className="text-[10px] text-slate-500 font-mono mt-1">
              Terminal bancaria
            </div>
          </div>

          {/* Transferencia */}
          <div className="bg-slate-900/90 border border-slate-800 p-3.5 rounded-xl shadow-sm">
            <div className="text-[10px] uppercase font-bold text-purple-400 tracking-wider flex items-center gap-1">
              <ArrowLeftRight className="w-3.5 h-3.5" />
              <span>Transferencia</span>
            </div>
            <div className="text-xl font-bold font-mono text-purple-300 mt-0.5">
              ${(datos?.totalTransfer || 0).toFixed(2)}
            </div>
            <div className="text-[10px] text-slate-500 font-mono mt-1">
              SPEI / Depósitos
            </div>
          </div>

          {/* Movimientos */}
          <div className="bg-slate-900/90 border border-slate-800 p-3.5 rounded-xl shadow-sm">
            <div className="text-[10px] uppercase font-bold text-amber-400 tracking-wider">
              Movimientos
            </div>
            <div className="text-xl font-bold font-mono text-amber-300 mt-0.5">
              {datos?.conteo || 0}
            </div>
            <div className="text-[10px] text-slate-500 font-mono mt-1">
              {conteoRapidas} rápidas • {conteoNotas} notas
            </div>
          </div>
        </div>
      </div>

      {/* 3. Barra de Búsqueda y Filtros */}
      <div className="p-3.5 bg-slate-900/20 border-b border-slate-800 flex flex-wrap items-center justify-between gap-3">
        {/* Buscador */}
        <div className="relative flex-1 max-w-md">
          <Search className="absolute left-3.5 top-2.5 text-slate-400 w-4 h-4" />
          <input
            type="text"
            value={busqueda}
            onChange={e => setBusqueda(e.target.value)}
            placeholder="Buscar por producto, clave, cliente o usuario..."
            className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-lg pl-10 pr-3 py-1.5 text-xs text-white placeholder-slate-500 focus:outline-none"
          />
        </div>

        {/* Chips de Filtro */}
        <div className="flex items-center gap-1.5 text-xs">
          <button
            onClick={() => setFiltroTipo('TODOS')}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer ${
              filtroTipo === 'TODOS'
                ? 'bg-blue-600 text-white shadow-sm'
                : 'bg-slate-900 text-slate-400 hover:text-white border border-slate-800'
            }`}
          >
            Todos ({datos?.movimientos.length || 0})
          </button>

          <button
            onClick={() => setFiltroTipo('RAPIDAS')}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1.5 ${
              filtroTipo === 'RAPIDAS'
                ? 'bg-emerald-600 text-white shadow-sm'
                : 'bg-slate-900 text-slate-400 hover:text-white border border-slate-800'
            }`}
          >
            <Zap className="w-3.5 h-3.5 text-amber-400 fill-amber-400" />
            <span>Ventas Rápidas ({conteoRapidas})</span>
          </button>

          <button
            onClick={() => setFiltroTipo('NOTAS')}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1.5 ${
              filtroTipo === 'NOTAS'
                ? 'bg-blue-600 text-white shadow-sm'
                : 'bg-slate-900 text-slate-400 hover:text-white border border-slate-800'
            }`}
          >
            <Receipt className="w-3.5 h-3.5 text-blue-400" />
            <span>Notas de Venta ({conteoNotas})</span>
          </button>
        </div>
      </div>

      {/* 4. Lista / Tabla de Movimientos de la Libreta */}
      <div className="flex-1 overflow-y-auto p-4 divide-y divide-slate-800/80">
        {cargando ? (
          <div className="py-24 text-center text-slate-400 text-sm flex flex-col items-center gap-3">
            <RefreshCw className="w-8 h-8 animate-spin text-blue-400" />
            <span>Cargando movimientos de la libreta...</span>
          </div>
        ) : movimientosFiltrados.length > 0 ? (
          movimientosFiltrados.map((m: MovimientoDia) => (
            <div
              key={m.rowid}
              className="py-3 px-3 hover:bg-slate-900/80 rounded-xl transition flex flex-col sm:flex-row sm:items-center justify-between gap-3"
            >
              {/* Información izquierda: Hora, Badge, Clave, Descripción y Cliente */}
              <div className="flex items-center gap-3.5 min-w-0">
                {/* Hora */}
                <div className="flex items-center gap-1.5 text-slate-400 font-mono text-xs shrink-0 w-24">
                  <Clock className="w-3.5 h-3.5 text-slate-500" />
                  <span>{m.horast || m.hora || '--:--'}</span>
                </div>

                {/* Badge de Tipo */}
                {m.nota === 0 ? (
                  <span className="bg-emerald-950 text-emerald-300 border border-emerald-800/80 px-2 py-0.5 rounded text-[10px] font-bold flex items-center gap-1 shrink-0">
                    <Zap className="w-3 h-3 text-amber-400 fill-amber-400" />
                    V. RÁPIDA
                  </span>
                ) : (
                  <span className="bg-blue-950 text-blue-300 border border-blue-800/80 px-2 py-0.5 rounded text-[10px] font-bold flex items-center gap-1 shrink-0">
                    <Receipt className="w-3 h-3 text-blue-400" />
                    NOTA
                  </span>
                )}

                {/* Detalle del Producto o Nota */}
                <div className="min-w-0">
                  <div className="font-semibold text-slate-100 text-sm flex items-center gap-2 truncate">
                    <span>{m.Descripcion}</span>
                    {m.cantidad > 1 && (
                      <span className="text-amber-400 font-mono font-bold text-xs bg-amber-950/60 px-1.5 py-0.2 rounded border border-amber-800/60">
                        {m.cantidad} pzas
                      </span>
                    )}
                  </div>

                  <div className="text-xs text-slate-400 font-mono flex flex-wrap items-center gap-2.5 mt-0.5">
                    {m.Clave && (
                      <span className="text-blue-300 font-semibold bg-slate-900 px-1.5 py-0.2 rounded border border-slate-800">
                        Clave: {m.Clave}
                      </span>
                    )}
                    <span className="flex items-center gap-1 text-slate-300">
                      <User className="w-3 h-3 text-slate-500" />
                      {m.Cliente || 'Mostrador'}
                    </span>
                    <span>•</span>
                    <span className="text-slate-500">Atendió: {m.usuario || 'ADMIN'}</span>
                  </div>
                </div>
              </div>

              {/* Información derecha: Desglose de pago y Total */}
              <div className="flex items-center gap-5 justify-end shrink-0 pl-4 sm:pl-0">
                {/* Desglose de métodos de pago */}
                <div className="text-right text-xs font-mono space-y-0.5">
                  {Number(m.Pagoefe) > 0 && (
                    <div className="text-slate-300 flex items-center justify-end gap-1.5">
                      <span className="text-[10px] text-slate-500 uppercase font-semibold">Efec:</span>
                      <strong className="text-emerald-400">${Number(m.Pagoefe).toFixed(2)}</strong>
                    </div>
                  )}
                  {Number(m.Pagotar) > 0 && (
                    <div className="text-slate-300 flex items-center justify-end gap-1.5">
                      <span className="text-[10px] text-slate-500 uppercase font-semibold">Tarj:</span>
                      <strong className="text-blue-400">${Number(m.Pagotar).toFixed(2)}</strong>
                    </div>
                  )}
                  {Number(m.Pagotrans) > 0 && (
                    <div className="text-slate-300 flex items-center justify-end gap-1.5">
                      <span className="text-[10px] text-slate-500 uppercase font-semibold">Trans:</span>
                      <strong className="text-purple-400">${Number(m.Pagotrans).toFixed(2)}</strong>
                    </div>
                  )}
                </div>

                {/* Importe Total de la Transacción */}
                <div className="text-right min-w-[90px]">
                  <div className="text-base font-bold font-mono text-emerald-400">
                    ${(Number(m.Total) || Number(m.Pagado) || 0).toFixed(2)}
                  </div>
                  {m.cancel === 1 && (
                    <span className="text-[10px] text-rose-400 font-bold uppercase">Cancelado</span>
                  )}
                </div>

                {/* Botón Reimprimir Comprobante / Nota */}
                <button
                  onClick={() => reimprimirMovimiento(m)}
                  disabled={cargandoRowId === m.rowid}
                  title="Reimprimir Nota / Ticket de Venta"
                  className="p-2 bg-slate-800 hover:bg-blue-600 hover:text-white text-slate-300 rounded-xl border border-slate-700/80 transition cursor-pointer flex items-center justify-center shrink-0 disabled:opacity-50"
                >
                  <Printer className={`w-4 h-4 ${cargandoRowId === m.rowid ? 'animate-pulse text-amber-400' : 'text-blue-400 hover:text-white'}`} />
                </button>
              </div>
            </div>
          ))
        ) : (
          <div className="py-24 text-center text-slate-500 text-sm flex flex-col items-center gap-2">
            <Package className="w-10 h-10 opacity-30 text-slate-400" />
            <span>No se encontraron movimientos de ventas en la fecha {fecha}.</span>
          </div>
        )}
      </div>

      {/* Modal de Impresión de Nota / Ticket */}
      {notaSeleccionada && (
        <NotaVentaModal
          datos={notaSeleccionada}
          onCerrar={() => setNotaSeleccionada(null)}
        />
      )}
    </div>
  );
}
