import { useState, useEffect, useRef } from 'react';
import {
  Wrench,
  Bike,
  Search,
  PlusCircle,
  Clock,
  CheckCircle2,
  DollarSign,
  Phone,
  User,
  Calendar,
  X,
  Check,
  Layers
} from 'lucide-react';
import {
  api,
  type OrdenReparacion,
  type BiciOrden,
  type ArticuloReparacion,
  type PagoReparacion,
  type Trabajador
} from './api';

interface ReparacionesViewProps {
  esAdmin: boolean;
  onNuevaOrden?: () => void;
}

export default function ReparacionesView({ esAdmin: _esAdmin, onNuevaOrden }: ReparacionesViewProps) {
  const [reparaciones, setReparaciones] = useState<OrdenReparacion[]>([]);
  const [filtro, setFiltro] = useState<'ACTIVAS' | 'LISTAS' | 'ENTREGADAS' | 'TODAS'>('ACTIVAS');
  const [query, setQuery] = useState('');
  const [cargando, setCargando] = useState(false);

  // Trabajadores mecánicos
  const [trabajadores, setTrabajadores] = useState<Trabajador[]>([]);

  // Modal Detalle / Hoja de Taller
  const [modalDetalle, setModalDetalle] = useState(false);
  const [ordenDetalle, setOrdenDetalle] = useState<{
    nota: OrdenReparacion;
    bicicletas: BiciOrden[];
    articulos: ArticuloReparacion[];
    pagos: PagoReparacion[];
    totalPagado: number;
    resta: number;
  } | null>(null);

  // Modal de Pago / Liquidación rápida
  const [modalCobro, setModalCobro] = useState(false);
  const [folioCobro, setFolioCobro] = useState<string | null>(null);
  const [restaCobro, setRestaCobro] = useState(0);
  const [pagoEfectivo, setPagoEfectivo] = useState('');
  const [pagoTarjeta, setPagoTarjeta] = useState('');
  const [pagoTransfer, setPagoTransfer] = useState('');
  const [entregarAlCobrar, setEntregarAlCobrar] = useState(true);

  // Mecánico asignado al finalizar
  const [mecanicoSeleccionado, setMecanicoSeleccionado] = useState('');

  const searchInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    cargarReparaciones();
    api.listarTrabajadores().then(setTrabajadores).catch(console.error);
  }, [filtro]);

  const cargarReparaciones = async () => {
    setCargando(true);
    try {
      const data = await api.listarReparaciones(filtro, query);
      setReparaciones(data);
    } catch (err) {
      console.error('Error cargando reparaciones:', err);
    } finally {
      setCargando(false);
    }
  };

  // Búsqueda en vivo reactiva
  useEffect(() => {
    const timer = setTimeout(async () => {
      try {
        const data = await api.listarReparaciones(filtro, query);
        setReparaciones(data);
      } catch (err) {
        console.error(err);
      }
    }, 150);
    return () => clearTimeout(timer);
  }, [query]);

  const abrirDetalle = async (folio: string) => {
    try {
      const data = await api.obtenerReparacion(folio);
      setOrdenDetalle(data);
      setModalDetalle(true);
      if (trabajadores.length > 0 && !mecanicoSeleccionado) {
        setMecanicoSeleccionado(trabajadores[0].Nombre);
      }
    } catch (err: any) {
      alert(`Error al abrir detalle: ${err.message}`);
    }
  };

  const toggleColocado = async (clave: string, actual: number) => {
    if (!ordenDetalle) return;
    const nuevoEstado = actual === 0;
    try {
      await api.toggleArticuloColocado(ordenDetalle.nota.Folio, clave, nuevoEstado);
      setOrdenDetalle({
        ...ordenDetalle,
        articulos: ordenDetalle.articulos.map(a =>
          a.Clave === clave ? { ...a, colocado: nuevoEstado ? 1 : 0 } : a
        )
      });
    } catch (err: any) {
      alert(`Error al actualizar estado: ${err.message}`);
    }
  };

  const marcarFinalizada = async (folio: string, biciId: number) => {
    const mecanico = mecanicoSeleccionado || (trabajadores[0]?.Nombre || 'Mecánico');
    try {
      const res = await api.cambiarEstadoBici(folio, biciId, {
        accion: 'FINALIZAR',
        mecanico
      });
      if (res.success) {
        alert('¡Bicicleta marcada como lista para entrega!');
        await cargarReparaciones();
        if (modalDetalle && ordenDetalle?.nota.Folio === folio) {
          abrirDetalle(folio);
        }
      }
    } catch (err: any) {
      alert(`Error: ${err.message}`);
    }
  };

  const abrirCobroModal = (folio: string, resta: number) => {
    setFolioCobro(folio);
    setRestaCobro(resta);
    setPagoEfectivo(resta > 0 ? resta.toString() : '');
    setPagoTarjeta('');
    setPagoTransfer('');
    setEntregarAlCobrar(true);
    setModalCobro(true);
  };

  const procesarPagoOrden = async () => {
    if (!folioCobro) return;
    const ef = parseFloat(pagoEfectivo) || 0;
    const tar = parseFloat(pagoTarjeta) || 0;
    const tra = parseFloat(pagoTransfer) || 0;
    const totalEntregado = ef + tar + tra;

    if (totalEntregado <= 0 && restaCobro > 0) {
      alert('Ingresa el monto del pago.');
      return;
    }

    try {
      const res = await api.registrarPagoReparacion(folioCobro, {
        efectivo: ef,
        tarjeta: tar,
        transferencia: tra,
        usuario: 'ADMIN',
        entregarAlLiquidar: entregarAlCobrar
      });

      if (res.success) {
        alert(entregarAlCobrar ? '¡Bicicleta liquidada y entregada al cliente!' : '¡Abono registrado con éxito!');
        setModalCobro(false);
        await cargarReparaciones();
        if (modalDetalle && ordenDetalle?.nota.Folio === folioCobro) {
          abrirDetalle(folioCobro);
        }
      }
    } catch (err: any) {
      alert(`Error: ${err.message}`);
    }
  };

  // Contadores rápidos para los KPIs
  const enTallerCount = reparaciones.filter(r => r.bentregada === 0 && r.Finalizadaall === 0).length;
  const listasCount = reparaciones.filter(r => r.bentregada === 0 && r.Finalizadaall === 1).length;
  const entregadasCount = reparaciones.filter(r => r.bentregada === 1).length;

  return (
    <div className="flex-1 flex flex-col h-full overflow-hidden bg-slate-950 text-slate-100 select-none">
      {/* 1. Barra Superior / Dashboard de Taller */}
      <div className="p-4 border-b border-slate-800 bg-slate-900/60 flex flex-wrap items-center justify-between gap-4">
        <div className="flex items-center gap-3">
          <div className="p-2 rounded-xl bg-amber-600/20 text-amber-400 border border-amber-500/30">
            <Wrench className="w-6 h-6" />
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h1 className="text-lg font-bold text-white tracking-wide">Taller de Reparaciones</h1>
              <span className="bg-amber-950 text-amber-300 border border-amber-800/80 px-2 py-0.5 rounded text-[10px] font-bold">
                BICICLETAS
              </span>
            </div>
            <p className="text-xs text-slate-400">Control de órdenes de servicio, refacciones, mecánicos y entregas</p>
          </div>
        </div>

        {/* Indicadores KPI */}
        <div className="flex items-center gap-3">
          <div className="bg-slate-900 border border-slate-800 px-3.5 py-1.5 rounded-lg flex items-center gap-2.5">
            <Clock className="w-4 h-4 text-amber-400" />
            <div>
              <div className="text-[10px] text-slate-400 uppercase font-semibold">En Taller</div>
              <div className="text-sm font-bold font-mono text-amber-400">{enTallerCount}</div>
            </div>
          </div>

          <div className="bg-slate-900 border border-slate-800 px-3.5 py-1.5 rounded-lg flex items-center gap-2.5">
            <CheckCircle2 className="w-4 h-4 text-emerald-400" />
            <div>
              <div className="text-[10px] text-slate-400 uppercase font-semibold">Listas para Entrega</div>
              <div className="text-sm font-bold font-mono text-emerald-400">{listasCount}</div>
            </div>
          </div>

          <button
            onClick={onNuevaOrden}
            className="bg-blue-600 hover:bg-blue-500 text-white font-bold text-xs px-4 py-2.5 rounded-lg flex items-center gap-2 transition cursor-pointer shadow-lg shadow-blue-900/40"
          >
            <PlusCircle className="w-4 h-4" />
            <span>Nueva Orden de Servicio</span>
          </button>
        </div>
      </div>

      {/* 2. Filtros y Buscador */}
      <div className="p-3.5 border-b border-slate-800 bg-slate-900/30 flex items-center justify-between gap-4">
        {/* Buscador */}
        <div className="relative flex-1 max-w-md">
          <Search className="absolute left-3.5 top-2.5 text-slate-400 w-4 h-4" />
          <input
            ref={searchInputRef}
            type="text"
            value={query}
            onChange={e => setQuery(e.target.value)}
            placeholder="Buscar por Folio, Cliente, Teléfono o Marca..."
            className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-lg pl-10 pr-3 py-2 text-xs text-white placeholder-slate-500 focus:outline-none"
          />
        </div>

        {/* Chips de Estado */}
        <div className="flex items-center gap-1.5 text-xs">
          <button
            onClick={() => setFiltro('ACTIVAS')}
            className={`px-3 py-1.5 rounded-lg transition cursor-pointer font-semibold flex items-center gap-1.5 ${
              filtro === 'ACTIVAS'
                ? 'bg-amber-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            <Clock className="w-3.5 h-3.5" />
            <span>En Taller ({enTallerCount})</span>
          </button>

          <button
            onClick={() => setFiltro('LISTAS')}
            className={`px-3 py-1.5 rounded-lg transition cursor-pointer font-semibold flex items-center gap-1.5 ${
              filtro === 'LISTAS'
                ? 'bg-emerald-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            <CheckCircle2 className="w-3.5 h-3.5" />
            <span>Listas para Entrega ({listasCount})</span>
          </button>

          <button
            onClick={() => setFiltro('ENTREGADAS')}
            className={`px-3 py-1.5 rounded-lg transition cursor-pointer font-semibold ${
              filtro === 'ENTREGADAS'
                ? 'bg-blue-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            Entregadas ({entregadasCount})
          </button>

          <button
            onClick={() => setFiltro('TODAS')}
            className={`px-3 py-1.5 rounded-lg transition cursor-pointer font-semibold ${
              filtro === 'TODAS'
                ? 'bg-slate-700 text-white'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            Todas
          </button>
        </div>
      </div>

      {/* 3. Cuadrícula de Órdenes de Reparación */}
      <div className="flex-1 overflow-y-auto p-4">
        {cargando ? (
          <div className="text-center py-20 text-slate-500 text-xs">Cargando órdenes del taller...</div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-3.5">
            {reparaciones.map(rep => {
              const estaLista = rep.Finalizadaall === 1 && rep.bentregada === 0;
              const estaEntregada = rep.bentregada === 1;
              const tieneSaldoPendiente = rep.Resta > 0;
              const biciPrincipal = rep.bicicletas[0];

              return (
                <div
                  key={rep.Folio}
                  className={`bg-slate-900 border rounded-xl p-4 flex flex-col justify-between transition hover:border-slate-600 shadow-md ${
                    estaLista
                      ? 'border-emerald-600/60 bg-emerald-950/10'
                      : estaEntregada
                      ? 'border-slate-800 opacity-75'
                      : 'border-slate-800'
                  }`}
                >
                  <div className="space-y-3">
                    {/* Encabezado: Folio y Estado */}
                    <div className="flex justify-between items-start">
                      <div>
                        <span className="font-mono font-bold text-blue-400 text-sm tracking-wide">
                          {rep.Folio}
                        </span>
                        <div className="text-xs text-slate-400 mt-0.5 flex items-center gap-1.5">
                          <Calendar className="w-3 h-3 text-slate-500" />
                          <span>{rep.Fecha} {rep.Hora}</span>
                        </div>
                      </div>

                      {estaEntregada ? (
                        <span className="bg-slate-800 text-slate-300 border border-slate-700 px-2 py-0.5 rounded text-[10px] font-bold">
                          ENTREGADA
                        </span>
                      ) : estaLista ? (
                        <span className="bg-emerald-950 text-emerald-300 border border-emerald-800 px-2 py-0.5 rounded text-[10px] font-bold flex items-center gap-1">
                          <CheckCircle2 className="w-3 h-3" /> LISTA
                        </span>
                      ) : (
                        <span className="bg-amber-950 text-amber-300 border border-amber-800 px-2 py-0.5 rounded text-[10px] font-bold flex items-center gap-1">
                          <Clock className="w-3 h-3" /> EN TALLER
                        </span>
                      )}
                    </div>

                    {/* Cliente */}
                    <div className="bg-slate-950/60 p-2.5 rounded-lg border border-slate-800/80">
                      <div className="flex items-center gap-1.5 font-semibold text-slate-200 text-xs">
                        <User className="w-3.5 h-3.5 text-blue-400" />
                        <span className="truncate">{rep.Cliente}</span>
                      </div>
                      {rep.telcel && (
                        <div className="flex items-center gap-1.5 text-[11px] text-slate-400 mt-1">
                          <Phone className="w-3 h-3 text-slate-500" />
                          <span>{rep.telcel}</span>
                        </div>
                      )}
                    </div>

                    {/* Datos de la Bicicleta */}
                    {biciPrincipal && (
                      <div className="text-xs space-y-1">
                        <div className="flex items-center gap-2 text-slate-200 font-semibold">
                          <Bike className="w-4 h-4 text-emerald-400" />
                          <span>{biciPrincipal.Marca}</span>
                          <span className="text-slate-400 font-normal">({biciPrincipal.Color || 'S/C'}, R{biciPrincipal.Rodada})</span>
                        </div>

                        {biciPrincipal.Nota && (
                          <div className="text-[11px] text-slate-400 italic bg-slate-950/40 p-1.5 rounded line-clamp-2">
                            "{biciPrincipal.Nota}"
                          </div>
                        )}

                        {biciPrincipal.Fhentrega && (
                          <div className="text-[11px] text-amber-300/90 flex items-center gap-1 pt-0.5">
                            <Clock className="w-3 h-3 text-amber-400" />
                            <span>Entrega: {biciPrincipal.Fhentrega}</span>
                          </div>
                        )}
                      </div>
                    )}

                    {/* Resumen Financiero */}
                    <div className="pt-2 border-t border-slate-800/80 flex justify-between items-center text-xs">
                      <div>
                        <span className="text-slate-400">Total: </span>
                        <span className="font-bold text-white font-mono">${rep.Total.toFixed(2)}</span>
                      </div>

                      <div>
                        {tieneSaldoPendiente ? (
                          <span className="text-amber-400 font-bold font-mono">
                            Resta: ${rep.Resta.toFixed(2)}
                          </span>
                        ) : (
                          <span className="text-emerald-400 font-bold">Liquidada ✓</span>
                        )}
                      </div>
                    </div>
                  </div>

                  {/* Botones de Acción */}
                  <div className="flex items-center gap-2 pt-3 mt-3 border-t border-slate-800/60">
                    <button
                      onClick={() => abrirDetalle(rep.Folio)}
                      className="flex-1 bg-slate-800 hover:bg-slate-700 text-slate-200 text-xs py-2 rounded-lg font-semibold border border-slate-700 flex items-center justify-center gap-1.5 transition cursor-pointer"
                    >
                      <Wrench className="w-3.5 h-3.5 text-blue-400" />
                      <span>Hoja de Taller</span>
                    </button>

                    {!estaEntregada && (
                      <button
                        onClick={() => abrirCobroModal(rep.Folio, rep.Resta)}
                        className={`px-3 py-2 rounded-lg text-xs font-bold transition cursor-pointer flex items-center gap-1 ${
                          estaLista
                            ? 'bg-emerald-600 hover:bg-emerald-500 text-white shadow-md shadow-emerald-950'
                            : 'bg-blue-600 hover:bg-blue-500 text-white'
                        }`}
                      >
                        <DollarSign className="w-3.5 h-3.5" />
                        <span>{tieneSaldoPendiente ? 'Cobrar / Entregar' : 'Entregar'}</span>
                      </button>
                    )}
                  </div>
                </div>
              );
            })}
          </div>
        )}

        {!cargando && reparaciones.length === 0 && (
          <div className="text-center py-20 text-slate-500 text-xs flex flex-col items-center gap-2">
            <Bike className="w-10 h-10 opacity-30" />
            <span>No hay órdenes de reparación con el filtro seleccionado</span>
          </div>
        )}
      </div>

      {/* 4. Modal: Hoja de Taller / Detalle de la Orden */}
      {modalDetalle && ordenDetalle && (
        <div className="fixed inset-0 bg-black/75 backdrop-blur-sm flex items-center justify-center p-4 z-50">
          <div className="bg-slate-900 border border-slate-800 rounded-2xl max-w-3xl w-full p-6 shadow-2xl flex flex-col max-h-[90vh]">
            <div className="flex justify-between items-start pb-4 border-b border-slate-800">
              <div>
                <div className="flex items-center gap-3">
                  <span className="font-mono font-bold text-xl text-blue-400">{ordenDetalle.nota.Folio}</span>
                  <span className="bg-slate-800 px-2.5 py-0.5 rounded text-xs text-slate-300 font-semibold">
                    Cliente: {ordenDetalle.nota.Cliente}
                  </span>
                  {ordenDetalle.nota.telcel && (
                    <span className="text-xs text-slate-400 flex items-center gap-1 font-mono">
                      <Phone className="w-3 h-3 text-slate-500" />
                      {ordenDetalle.nota.telcel}
                    </span>
                  )}
                </div>
                <div className="text-xs text-slate-500 mt-1">
                  Recibida el {ordenDetalle.nota.Fecha} a las {ordenDetalle.nota.Hora} por {ordenDetalle.nota.Usuario}
                </div>
              </div>

              <button
                onClick={() => setModalDetalle(false)}
                className="text-slate-400 hover:text-white p-1 rounded-lg hover:bg-slate-800 transition cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            <div className="flex-1 overflow-y-auto space-y-4 py-4 pr-1">
              {/* Bicicletas en la orden */}
              <div className="space-y-2">
                <h3 className="text-xs font-bold text-slate-400 uppercase tracking-wider flex items-center gap-1.5">
                  <Bike className="w-4 h-4 text-emerald-400" />
                  Bicicletas Registradas
                </h3>

                {ordenDetalle.bicicletas.map(b => (
                  <div
                    key={b.Id}
                    className="p-3.5 bg-slate-950 rounded-xl border border-slate-800 flex justify-between items-center"
                  >
                    <div>
                      <div className="flex items-center gap-2">
                        <span className="font-bold text-slate-200 text-sm">{b.Marca}</span>
                        <span className="text-xs text-slate-400">Color: {b.Color || 'S/C'}, Rodada {b.Rodada}</span>
                      </div>
                      {b.Nota && (
                        <div className="text-xs text-slate-400 italic mt-1">
                          Trabajo solicitado: "{b.Nota}"
                        </div>
                      )}
                      {b.Fhentrega && (
                        <div className="text-xs text-amber-400 mt-1">
                          Promesa de entrega: {b.Fhentrega}
                        </div>
                      )}
                    </div>

                    <div className="flex items-center gap-2">
                      {b.bentregada === 1 ? (
                        <span className="bg-slate-800 text-slate-300 px-2.5 py-1 rounded text-xs font-bold">
                          Entregada por {b.Entrego}
                        </span>
                      ) : b.bfinalizada === 1 ? (
                        <span className="bg-emerald-950 text-emerald-300 border border-emerald-800 px-2.5 py-1 rounded text-xs font-bold flex items-center gap-1">
                          <CheckCircle2 className="w-3.5 h-3.5" /> Lista ({b.Realizo})
                        </span>
                      ) : (
                        <div className="flex items-center gap-2">
                          <select
                            value={mecanicoSeleccionado}
                            onChange={e => setMecanicoSeleccionado(e.target.value)}
                            className="bg-slate-900 border border-slate-700 rounded px-2 py-1 text-xs text-slate-200"
                          >
                            {trabajadores.map(t => (
                              <option key={t.Id} value={t.Nombre}>
                                {t.Nombre}
                              </option>
                            ))}
                          </select>
                          <button
                            onClick={() => marcarFinalizada(ordenDetalle.nota.Folio, b.Id)}
                            className="bg-emerald-600 hover:bg-emerald-500 text-white text-xs font-bold px-3 py-1.5 rounded-lg transition cursor-pointer flex items-center gap-1"
                          >
                            <Check className="w-3.5 h-3.5" /> Marcar Lista
                          </button>
                        </div>
                      )}
                    </div>
                  </div>
                ))}
              </div>

              {/* Checklist de Refacciones y Mano de Obra */}
              <div className="space-y-2">
                <h3 className="text-xs font-bold text-slate-400 uppercase tracking-wider flex items-center gap-1.5">
                  <Layers className="w-4 h-4 text-blue-400" />
                  Checklist de Refacciones y Mano de Obra
                </h3>

                <table className="w-full text-left text-xs bg-slate-950 rounded-xl overflow-hidden border border-slate-800">
                  <thead className="bg-slate-900 text-slate-400 uppercase text-[10px]">
                    <tr>
                      <th className="p-3 w-16 text-center">Instalado</th>
                      <th className="p-3">Cant</th>
                      <th className="p-3">Clave / Descripción</th>
                      <th className="p-3">Tipo</th>
                      <th className="p-3 text-right">Precio</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-800/60 font-mono">
                    {ordenDetalle.articulos.map((art, idx) => {
                      const esTrabajo = art.Trabajo === 1;
                      const colocado = art.colocado === 1;

                      return (
                        <tr
                          key={idx}
                          onClick={() => toggleColocado(art.Clave, art.colocado)}
                          className={`hover:bg-slate-900/60 transition cursor-pointer ${
                            colocado ? 'bg-emerald-950/20' : ''
                          }`}
                        >
                          <td className="p-3 text-center">
                            <input
                              type="checkbox"
                              checked={colocado}
                              onChange={() => {}} // Manejado por onClick de fila
                              className="w-4 h-4 rounded bg-slate-900 border-slate-700 text-emerald-600 cursor-pointer"
                            />
                          </td>
                          <td className="p-3 font-bold text-white">{art.Cantidad}</td>
                          <td className="p-3 font-sans">
                            <div className="font-semibold text-slate-200">{art.Descripcion}</div>
                            <div className="text-[11px] text-slate-500 font-mono">{art.Clave}</div>
                          </td>
                          <td className="p-3">
                            {esTrabajo ? (
                              <span className="bg-purple-950 text-purple-300 border border-purple-800/80 px-2 py-0.5 rounded text-[10px] font-bold">
                                MANO DE OBRA
                              </span>
                            ) : (
                              <span className="bg-slate-800 text-slate-300 px-2 py-0.5 rounded text-[10px]">
                                REFACCIÓN
                              </span>
                            )}
                          </td>
                          <td className="p-3 text-right font-bold text-emerald-400">
                            ${(parseFloat(art.Tp.split(':')[1]) || 0).toFixed(2)}
                          </td>
                        </tr>
                      );
                    })}
                  </tbody>
                </table>
              </div>

              {/* Historial de Pagos y Anticipos */}
              <div className="space-y-2">
                <h3 className="text-xs font-bold text-slate-400 uppercase tracking-wider flex items-center gap-1.5">
                  <DollarSign className="w-4 h-4 text-amber-400" />
                  Pagos y Anticipos Recibidos
                </h3>

                <div className="bg-slate-950 p-3 rounded-xl border border-slate-800 space-y-2 text-xs">
                  {ordenDetalle.pagos.map((p, idx) => (
                    <div key={idx} className="flex justify-between items-center text-slate-300 font-mono">
                      <span>{p.Fecha} ({p.Usuario}):</span>
                      <span className="font-bold text-emerald-400">
                        ${(Number(p.Efectivo || 0) + Number(p.Tarjeta || 0) + Number(p.Transferencia || 0)).toFixed(2)}
                      </span>
                    </div>
                  ))}

                  {ordenDetalle.pagos.length === 0 && (
                    <div className="text-slate-500 italic text-[11px]">Sin anticipos registrados</div>
                  )}

                  <div className="pt-2 border-t border-slate-800 flex justify-between items-center text-sm font-bold">
                    <span className="text-slate-300">Total Orden: ${ordenDetalle.nota.Total.toFixed(2)}</span>
                    <span className={`font-mono ${ordenDetalle.resta > 0 ? 'text-amber-400' : 'text-emerald-400'}`}>
                      {ordenDetalle.resta > 0 ? `Saldo Pendiente: $${ordenDetalle.resta.toFixed(2)}` : 'Totalmente Liquidado ✓'}
                    </span>
                  </div>
                </div>
              </div>
            </div>

            {/* Pie del Modal */}
            <div className="pt-4 border-t border-slate-800 flex justify-end gap-3">
              <button
                onClick={() => setModalDetalle(false)}
                className="bg-slate-800 hover:bg-slate-700 text-slate-300 px-4 py-2 rounded-lg text-xs font-semibold cursor-pointer"
              >
                Cerrar
              </button>

              {ordenDetalle.nota.bentregada === 0 && (
                <button
                  onClick={() => abrirCobroModal(ordenDetalle.nota.Folio, ordenDetalle.resta)}
                  className="bg-emerald-600 hover:bg-emerald-500 text-white font-bold px-4 py-2 rounded-lg text-xs cursor-pointer shadow-lg shadow-emerald-950 flex items-center gap-1.5"
                >
                  <DollarSign className="w-4 h-4" />
                  <span>Cobrar / Entregar Bicicleta</span>
                </button>
              )}
            </div>
          </div>
        </div>
      )}

      {/* Modal: Cobrar / Liquidar y Entregar */}
      {modalCobro && folioCobro && (
        <div className="fixed inset-0 bg-black/75 backdrop-blur-sm flex items-center justify-center p-4 z-50">
          <div className="bg-slate-900 border border-slate-800 rounded-2xl max-w-md w-full p-6 shadow-2xl">
            <h3 className="text-lg font-bold flex items-center gap-2 text-white mb-4">
              <DollarSign className="w-5 h-5 text-emerald-400" />
              Cobrar y Liquidar Taller ({folioCobro})
            </h3>

            <div className="space-y-4">
              <div className="p-3 bg-slate-950 rounded-lg flex justify-between items-center">
                <span className="text-xs text-slate-400">Saldo Pendiente:</span>
                <span className="text-xl font-bold font-mono text-amber-400">${restaCobro.toFixed(2)}</span>
              </div>

              <div className="space-y-2.5">
                <div>
                  <label className="block text-[11px] text-slate-400 mb-1">Efectivo ($)</label>
                  <input
                    type="number"
                    step="any"
                    min="0"
                    value={pagoEfectivo}
                    onChange={e => setPagoEfectivo(e.target.value)}
                    placeholder="0.00"
                    className="w-full bg-slate-950 border border-slate-700 rounded px-3 py-1.5 text-sm text-white font-mono focus:outline-none focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-[11px] text-slate-400 mb-1">Tarjeta ($)</label>
                  <input
                    type="number"
                    step="any"
                    min="0"
                    value={pagoTarjeta}
                    onChange={e => setPagoTarjeta(e.target.value)}
                    placeholder="0.00"
                    className="w-full bg-slate-950 border border-slate-700 rounded px-3 py-1.5 text-sm text-white font-mono focus:outline-none focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-[11px] text-slate-400 mb-1">Transferencia ($)</label>
                  <input
                    type="number"
                    step="any"
                    min="0"
                    value={pagoTransfer}
                    onChange={e => setPagoTransfer(e.target.value)}
                    placeholder="0.00"
                    className="w-full bg-slate-950 border border-slate-700 rounded px-3 py-1.5 text-sm text-white font-mono focus:outline-none focus:border-blue-500"
                  />
                </div>
              </div>

              <label className="flex items-center gap-2 cursor-pointer text-xs text-slate-300 pt-1">
                <input
                  type="checkbox"
                  checked={entregarAlCobrar}
                  onChange={e => setEntregarAlCobrar(e.target.checked)}
                  className="rounded bg-slate-800 border-slate-700 text-emerald-600 focus:ring-0 w-4 h-4 cursor-pointer"
                />
                <span>Marcar bicicleta como ENTREGADA al cliente inmediatamente</span>
              </label>

              <div className="flex gap-3 pt-2">
                <button
                  type="button"
                  onClick={() => setModalCobro(false)}
                  className="flex-1 bg-slate-800 hover:bg-slate-700 py-2.5 rounded-lg text-xs font-semibold text-slate-300 cursor-pointer"
                >
                  Cancelar
                </button>
                <button
                  type="button"
                  onClick={procesarPagoOrden}
                  className="flex-1 bg-emerald-600 hover:bg-emerald-500 py-2.5 rounded-lg text-xs font-bold text-white shadow-lg shadow-emerald-950 cursor-pointer flex items-center justify-center gap-1.5"
                >
                  <Check className="w-4 h-4" />
                  <span>Confirmar Cobro</span>
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
