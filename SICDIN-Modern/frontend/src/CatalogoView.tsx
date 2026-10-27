import { useState, useEffect, useRef } from 'react';
import {
  Search,
  Package,
  Layers,
  MoreVertical,
  Edit3,
  PlusCircle,
  X,
  Plus,
  Minus,
  Check,
  Zap,
  CreditCard,
  ArrowLeftRight,
  Banknote,
  Wrench,
  Infinity,
  AlertTriangle
} from 'lucide-react';
import { api, type Articulo } from './api';

interface CatalogoViewProps {
  esAdmin: boolean;
  onEditarArticulo: (art: Articulo) => void;
}

export default function CatalogoView({ esAdmin, onEditarArticulo }: CatalogoViewProps) {
  const [query, setQuery] = useState('');
  const [articulos, setArticulos] = useState<Articulo[]>([]);
  const [sinCoincidencia, setSinCoincidencia] = useState(false);
  const [menuActivoClave, setMenuActivoClave] = useState<string | null>(null);

  // Estado para la ventana flotante de modificar existencias
  const [articuloAjuste, setArticuloAjuste] = useState<Articulo | null>(null);
  const [cantidadAjuste, setCantidadAjuste] = useState<number>(1);
  const [operacionAjuste, setOperacionAjuste] = useState<'SUMAR' | 'RESTAR' | 'FIJAR'>('SUMAR');
  const [guardandoAjuste, setGuardandoAjuste] = useState(false);

  // Estado para Venta Rápida (DISPONIBLE PARA TODOS)
  const [articuloVR, setArticuloVR] = useState<Articulo | null>(null);
  const [cantidadVR, setCantidadVR] = useState<number>(1);
  const [tipoPrecioVR, setTipoPrecioVR] = useState<'Menudeo' | 'Mayoreo' | 'Especial'>('Menudeo');
  const [precioVR, setPrecioVR] = useState<number>(0);
  const [pagoEfectivoVR, setPagoEfectivoVR] = useState<string>('');
  const [pagoTarjetaVR, setPagoTarjetaVR] = useState<string>('');
  const [pagoTransferVR, setPagoTransferVR] = useState<string>('');
  const [procesandoVR, setProcesandoVR] = useState<boolean>(false);
  const [exitoVR, setExitoVR] = useState<{ total: number; cambio: number; clave: string; desc: string } | null>(null);

  // Filtros de Tipo y Existencias
  const [filtroTipo, setFiltroTipo] = useState<'todos' | 'articulos' | 'servicios' | 'infinitos'>('todos');
  const [soloConExistencias, setSoloConExistencias] = useState<boolean>(false);

  const searchInputRef = useRef<HTMLInputElement>(null);
  const ajusteInputRef = useRef<HTMLInputElement>(null);
  const vrCantidadInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    searchInputRef.current?.focus();
    cargarInicial();
  }, []);

  const cargarInicial = async () => {
    try {
      const res = await api.buscarArticulos('');
      setArticulos(res);
      setSinCoincidencia(false);
    } catch (err) {
      console.error(err);
    }
  };

  // Filtrado / Búsqueda reactiva desde el primer caracter
  useEffect(() => {
    const timer = setTimeout(async () => {
      try {
        const res = await api.buscarArticulos(query);
        if (res && res.length > 0) {
          setArticulos(res);
          setSinCoincidencia(false);
        } else {
          setSinCoincidencia(true);
        }
      } catch (err) {
        console.error(err);
      }
    }, 60);
    return () => clearTimeout(timer);
  }, [query]);

  // Cerrar menú contextual al hacer clic afuera
  useEffect(() => {
    const handleClickOutside = () => setMenuActivoClave(null);
    window.addEventListener('click', handleClickOutside);
    return () => window.removeEventListener('click', handleClickOutside);
  }, []);

  useEffect(() => {
    if (articuloAjuste) {
      ajusteInputRef.current?.focus();
    }
  }, [articuloAjuste]);

  const abrirModalAjuste = (art: Articulo) => {
    setArticuloAjuste(art);
    setCantidadAjuste(1);
    setOperacionAjuste('SUMAR');
    setMenuActivoClave(null);
  };

  const aplicarAjusteExistencias = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!articuloAjuste) return;

    setGuardandoAjuste(true);
    try {
      let cambio = cantidadAjuste;
      let tipo: 'AJUSTE' | 'ABSOLUTO' = 'AJUSTE';

      if (operacionAjuste === 'RESTAR') {
        cambio = -Math.abs(cantidadAjuste);
      } else if (operacionAjuste === 'FIJAR') {
        tipo = 'ABSOLUTO';
      }

      const res = await api.modificarExistencias(articuloAjuste.Clave, cambio, tipo);
      if (res.success) {
        // Actualizar la lista en memoria de inmediato
        setArticulos(prev =>
          prev.map(a =>
            a.Clave === articuloAjuste.Clave
              ? { ...a, Existencias: res.existencias }
              : a
          )
        );
        setArticuloAjuste(null);
      }
    } catch (err: any) {
      alert(`Error al ajustar existencias: ${err.message}`);
    } finally {
      setGuardandoAjuste(false);
    }
  };

  // Calcular la previsualización del nuevo stock
  const calcularStockResultante = () => {
    if (!articuloAjuste) return 0;
    const actual = articuloAjuste.Existencias || 0;
    const val = Number(cantidadAjuste) || 0;
    if (operacionAjuste === 'SUMAR') return actual + val;
    if (operacionAjuste === 'RESTAR') return Math.max(0, actual - val);
    if (operacionAjuste === 'FIJAR') return Math.max(0, val);
    return actual;
  };

  // --- VENTA RÁPIDA (Disponible para Todos) ---
  const abrirModalVR = (art: Articulo) => {
    setArticuloVR(art);
    setCantidadVR(1);
    setTipoPrecioVR('Menudeo');
    const p = art.Menudeo || 0;
    setPrecioVR(p);
    setPagoEfectivoVR(p > 0 ? p.toString() : '');
    setPagoTarjetaVR('');
    setPagoTransferVR('');
    setExitoVR(null);
    setTimeout(() => vrCantidadInputRef.current?.select(), 50);
  };

  const seleccionarTipoPrecioVR = (tipo: 'Menudeo' | 'Mayoreo' | 'Especial') => {
    if (!articuloVR) return;
    setTipoPrecioVR(tipo);
    const p = articuloVR[tipo] || 0;
    setPrecioVR(p);
    const total = Math.round(cantidadVR * p * 100) / 100;
    const numTar = parseFloat(pagoTarjetaVR) || 0;
    const numTra = parseFloat(pagoTransferVR) || 0;
    if (numTar === 0 && numTra === 0) {
      setPagoEfectivoVR(total > 0 ? total.toString() : '');
    }
  };

  const cambiarCantidadVR = (nuevaCantidad: number) => {
    const cant = Math.max(1, nuevaCantidad);
    setCantidadVR(cant);
    const total = Math.round(cant * precioVR * 100) / 100;
    const numTar = parseFloat(pagoTarjetaVR) || 0;
    const numTra = parseFloat(pagoTransferVR) || 0;
    if (numTar === 0 && numTra === 0) {
      setPagoEfectivoVR(total > 0 ? total.toString() : '');
    }
  };

  const totalCalculadoVR = Math.round(cantidadVR * precioVR * 100) / 100;
  const numEfVR = parseFloat(pagoEfectivoVR) || 0;
  const numTarVR = parseFloat(pagoTarjetaVR) || 0;
  const numTraVR = parseFloat(pagoTransferVR) || 0;
  const totalPagadoVR = Math.round((numEfVR + numTarVR + numTraVR) * 100) / 100;
  const faltaCubrirVR = Math.max(0, Math.round((totalCalculadoVR - totalPagadoVR) * 100) / 100);
  const cambioCalculadoVR = numEfVR > 0 ? Math.max(0, Math.round((totalPagadoVR - totalCalculadoVR) * 100) / 100) : 0;

  const esSinControlStockVR = (tipo?: string) => {
    const t = (tipo || '').toLowerCase().trim();
    return t === 'servicio' || t === 'infinito' || t === 'trabajo';
  };

  const tipoEsExentoVR = articuloVR ? esSinControlStockVR(articuloVR.Tipo) : false;
  const stockDisponibleVR = articuloVR ? (Number(articuloVR.Existencias) || 0) : 0;
  const stockInsuficienteVR = Boolean(
    articuloVR &&
    !tipoEsExentoVR &&
    (stockDisponibleVR <= 0 || cantidadVR > stockDisponibleVR)
  );

  const pagarTodoConVR = (tipo: 'efectivo' | 'tarjeta' | 'transferencia') => {
    if (tipo === 'efectivo') {
      setPagoEfectivoVR(totalCalculadoVR > 0 ? totalCalculadoVR.toString() : '');
      setPagoTarjetaVR('');
      setPagoTransferVR('');
    } else if (tipo === 'tarjeta') {
      setPagoEfectivoVR('');
      setPagoTarjetaVR(totalCalculadoVR > 0 ? totalCalculadoVR.toString() : '');
      setPagoTransferVR('');
    } else if (tipo === 'transferencia') {
      setPagoEfectivoVR('');
      setPagoTarjetaVR('');
      setPagoTransferVR(totalCalculadoVR > 0 ? totalCalculadoVR.toString() : '');
    }
  };

  const completarRestoVR = (tipo: 'efectivo' | 'tarjeta' | 'transferencia') => {
    if (tipo === 'efectivo') {
      const resto = Math.max(0, totalCalculadoVR - numTarVR - numTraVR);
      setPagoEfectivoVR(resto > 0 ? resto.toFixed(2) : '');
    } else if (tipo === 'tarjeta') {
      const resto = Math.max(0, totalCalculadoVR - numEfVR - numTraVR);
      setPagoTarjetaVR(resto > 0 ? resto.toFixed(2) : '');
    } else if (tipo === 'transferencia') {
      const resto = Math.max(0, totalCalculadoVR - numEfVR - numTarVR);
      setPagoTransferVR(resto > 0 ? resto.toFixed(2) : '');
    }
  };

  const confirmarVentaRapida = async (e?: React.FormEvent) => {
    if (e) e.preventDefault();
    if (!articuloVR) return;

    if (stockInsuficienteVR) {
      alert(
        stockDisponibleVR <= 0
          ? `El artículo "${articuloVR.Descripcion}" está AGOTADO (0 existencias). No se puede realizar la venta.`
          : `Existencias insuficientes para "${articuloVR.Descripcion}". Disponibles: ${stockDisponibleVR}, Solicitados: ${cantidadVR}.`
      );
      return;
    }

    if (totalPagadoVR < totalCalculadoVR) {
      alert(`El pago ingresado ($${totalPagadoVR.toFixed(2)}) no cubre el total de la venta ($${totalCalculadoVR.toFixed(2)}). Faltan $${faltaCubrirVR.toFixed(2)}.`);
      return;
    }

    setProcesandoVR(true);
    try {
      const res = await api.crearVentaRapida({
        clave: articuloVR.Clave,
        cantidad: cantidadVR,
        precio: precioVR,
        pagoEfectivo: numEfVR,
        pagoTarjeta: numTarVR,
        pagoTransfer: numTraVR,
        cliente: '1:Mostrador',
        usuario: 'ADMIN'
      });

      if (res.success) {
        // Actualizar existencias en la lista en memoria inmediatamente
        setArticulos(prev =>
          prev.map(a =>
            a.Clave === articuloVR.Clave
              ? { ...a, Existencias: res.existenciasRestantes }
              : a
          )
        );

        setExitoVR({
          total: res.total,
          cambio: res.cambio,
          clave: res.clave,
          desc: res.descripcion
        });

        // Notificar a toda la aplicación para actualizar la libreta de ventas al instante
        window.dispatchEvent(new CustomEvent('sicdin_venta_registrada', { detail: res }));

        // Cerrar modal automáticamente después de mostrar el cambio
        setTimeout(() => {
          setArticuloVR(null);
          setExitoVR(null);
        }, 1800);
      }
    } catch (err: any) {
      alert(`Error en venta rápida: ${err.message}`);
    } finally {
      setProcesandoVR(false);
    }
  };

  const articulosFiltrados = articulos.filter(art => {
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
    <div className="relative flex-1 flex flex-col h-full bg-slate-950 text-slate-100 overflow-hidden">
      {/* Cabecera */}
      <div className="p-4 bg-slate-900/60 border-b border-slate-800 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <div className="p-2 bg-blue-500/10 text-blue-400 rounded-lg border border-blue-500/20">
            <Layers className="w-5 h-5" />
          </div>
          <div>
            <h2 className="text-lg font-bold text-white tracking-wide">Catálogo de Artículos</h2>
            <p className="text-xs text-slate-400">Consulta de precios, existencias y detalles (solo consulta)</p>
          </div>
        </div>
        <div className="flex items-center gap-2.5">
          {sinCoincidencia && (
            <span className="text-xs bg-amber-950/80 text-amber-300 border border-amber-800/80 px-2.5 py-1 rounded-full font-medium">
              Sin coincidencias nuevas (mostrando últimos resultados)
            </span>
          )}
          <div className="text-xs bg-slate-800/80 px-3 py-1.5 rounded-full border border-slate-700/80 font-mono text-slate-300">
            {articulosFiltrados.length} artículos mostrados
          </div>
        </div>
      </div>

      {/* Buscador y Filtros */}
      <div className="p-4 bg-slate-900/30 border-b border-slate-800/60 flex flex-col lg:flex-row lg:items-center justify-between gap-3">
        <div className="relative flex-1 max-w-2xl">
          <Search className="absolute left-3.5 top-3.5 text-slate-400 w-5 h-5" />
          <input
            ref={searchInputRef}
            type="text"
            value={query}
            onChange={e => setQuery(e.target.value)}
            placeholder="Buscar por descripción, clave o código de barras exacto..."
            className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-xl pl-11 pr-4 py-2.5 text-sm text-white focus:outline-none placeholder-slate-500 shadow-inner transition"
          />
        </div>

        {/* Botones de Filtro: Artículos, Servicios, Infinitos, Con Existencias */}
        <div className="flex flex-wrap items-center gap-1.5 text-xs">
          <button
            type="button"
            onClick={() => setFiltroTipo('todos')}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer ${
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
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1.5 ${
              filtroTipo === 'articulos'
                ? 'bg-blue-600 text-white shadow-sm'
                : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-slate-200 border border-slate-800'
            }`}
          >
            <Package className="w-3.5 h-3.5" />
            <span>Artículos</span>
          </button>
          <button
            type="button"
            onClick={() => setFiltroTipo('servicios')}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1.5 ${
              filtroTipo === 'servicios'
                ? 'bg-purple-600 text-white shadow-sm'
                : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-purple-300 border border-slate-800'
            }`}
          >
            <Wrench className="w-3.5 h-3.5" />
            <span>Servicios</span>
          </button>
          <button
            type="button"
            onClick={() => setFiltroTipo('infinitos')}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1.5 ${
              filtroTipo === 'infinitos'
                ? 'bg-emerald-600 text-white shadow-sm'
                : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-emerald-300 border border-slate-800'
            }`}
          >
            <Infinity className="w-3.5 h-3.5" />
            <span>Infinitos</span>
          </button>

          <div className="h-5 w-[1px] bg-slate-800 mx-1 hidden sm:block" />

          <button
            type="button"
            onClick={() => setSoloConExistencias(prev => !prev)}
            className={`px-3 py-1.5 rounded-lg font-semibold transition cursor-pointer flex items-center gap-1.5 border ${
              soloConExistencias
                ? 'bg-emerald-600 text-white border-emerald-500 shadow-sm'
                : 'bg-slate-900 hover:bg-slate-800 text-slate-400 hover:text-emerald-300 border-slate-800'
            }`}
            title="Mostrar solo productos con existencias disponibles (>0)"
          >
            <Check className="w-3.5 h-3.5" />
            <span>Con existencias</span>
          </button>
        </div>
      </div>

      {/* Contenido / Tabla de Artículos */}
      <div className="flex-1 overflow-y-auto p-4 space-y-2">
        {articulosFiltrados.map(art => {
          const cleanTipo = (art.Tipo || '').trim().toLowerCase();
          const esServicio = cleanTipo === 'servicio' || cleanTipo === 'trabajo';
          const esInfinito = cleanTipo === 'infinito';

          return (
            <div
              key={art.Clave}
              className="relative p-4 bg-slate-900/90 hover:bg-slate-800/90 border border-slate-800/90 hover:border-slate-700 rounded-xl transition shadow-sm flex flex-col md:flex-row md:items-center justify-between gap-4"
            >
              {/* Información del Artículo */}
              <div className="flex-1">
                <div className="flex flex-wrap items-center gap-2">
                  <span className="font-bold text-slate-100 text-base">{art.Descripcion}</span>
                  {esServicio ? (
                    <span className="text-[10px] font-bold bg-purple-950/80 text-purple-300 border border-purple-800/80 px-2 py-0.5 rounded flex items-center gap-1">
                      <Wrench className="w-3 h-3 text-purple-400" /> SERVICIO
                    </span>
                  ) : esInfinito ? (
                    <span className="text-[10px] font-bold bg-emerald-950/80 text-emerald-300 border border-emerald-800/80 px-2 py-0.5 rounded flex items-center gap-1">
                      <Infinity className="w-3 h-3 text-emerald-400" /> INFINITO
                    </span>
                  ) : (
                    <span className="text-[10px] font-bold bg-slate-800 text-slate-300 border border-slate-700 px-2 py-0.5 rounded flex items-center gap-1">
                      <Package className="w-3 h-3 text-blue-400" /> ARTÍCULO
                    </span>
                  )}
                </div>
                <div className="text-xs text-slate-400 font-mono flex flex-wrap items-center gap-3 mt-1.5">
                  <span className="bg-slate-800 px-2 py-0.5 rounded text-blue-300 font-semibold border border-slate-700">
                    Clave: {art.Clave}
                  </span>
                  {art.cbarra && (
                    <span className="bg-slate-800/80 px-2 py-0.5 rounded text-slate-300 border border-slate-700">
                      C. Barra: {art.cbarra}
                    </span>
                  )}
                  {art.Paquete && (
                    <span className="text-slate-400">
                      Unidad: {art.Paquete}
                    </span>
                  )}
                </div>
              </div>

              {/* Existencias y Precios */}
              <div className="flex flex-wrap items-center gap-3 sm:gap-4 justify-end">
                {/* Existencias */}
                <div className="text-center px-3 py-1.5 rounded-lg bg-slate-950/80 border border-slate-800 min-w-[85px]">
                  <div className="text-[10px] uppercase font-bold text-slate-400 tracking-wider">Existencias</div>
                  {esServicio ? (
                    <div className="text-xs font-bold font-mono text-purple-300 mt-0.5 flex items-center justify-center gap-1">
                      <Wrench className="w-3.5 h-3.5 text-purple-400" />
                      <span>Servicio</span>
                    </div>
                  ) : esInfinito ? (
                    <div className="text-base font-bold font-mono text-emerald-400 mt-0.5 flex items-center justify-center gap-1">
                      <Infinity className="w-4 h-4 text-emerald-400" />
                    </div>
                  ) : (
                    <div className={`text-base font-bold font-mono ${art.Existencias > 0 ? 'text-emerald-400' : 'text-rose-400'}`}>
                      {art.Existencias}
                    </div>
                  )}
                </div>

              {/* Menudeo */}
              <div className="text-center px-3 py-1.5 rounded-lg bg-slate-950/80 border border-slate-800">
                <div className="text-[10px] uppercase font-bold text-blue-400 tracking-wider">Menudeo</div>
                <div className="text-base font-bold font-mono text-emerald-400">
                  ${(art.Menudeo || 0).toFixed(2)}
                </div>
              </div>

              {/* Mayoreo */}
              <div className="text-center px-3 py-1.5 rounded-lg bg-slate-950/80 border border-slate-800">
                <div className="text-[10px] uppercase font-bold text-slate-400 tracking-wider">Mayoreo</div>
                <div className="text-base font-semibold font-mono text-slate-200">
                  ${(art.Mayoreo || 0).toFixed(2)}
                </div>
              </div>

              {/* Especial */}
              <div className="text-center px-3 py-1.5 rounded-lg bg-slate-950/80 border border-slate-800">
                <div className="text-[10px] uppercase font-bold text-slate-400 tracking-wider">Especial</div>
                <div className="text-base font-semibold font-mono text-purple-300">
                  ${(art.Especial || 0).toFixed(2)}
                </div>
              </div>

              {/* Cantidad PME */}
              <div className="text-center px-3 py-1.5 rounded-lg bg-slate-950/80 border border-slate-800" title="Piezas mínimas para precio especial">
                <div className="text-[10px] uppercase font-bold text-slate-400 tracking-wider">Cant. PME</div>
                <div className="text-base font-semibold font-mono text-amber-300">
                  {(art.PME || 0) > 0 ? `≥ ${art.PME} pz` : '—'}
                </div>
              </div>

              {/* Botón de Venta Rápida (DISPONIBLE PARA TODOS: Admin y Usuario) */}
              <button
                onClick={() => abrirModalVR(art)}
                className="flex items-center gap-1.5 px-3 py-2 rounded-lg bg-emerald-600/20 hover:bg-emerald-600 text-emerald-400 hover:text-white border border-emerald-500/30 text-xs font-bold transition cursor-pointer shadow-sm group/vr shrink-0"
                title="Venta rápida directa a caja (sin generar nota)"
              >
                <Zap className="w-3.5 h-3.5 text-amber-400 group-hover/vr:text-white fill-amber-400 group-hover/vr:fill-white transition" />
                <span>Venta Rápida</span>
              </button>

              {/* Menú Secundario desplegable por Artículo (Para Administrador) */}
              {esAdmin && (
                <div className="relative">
                  <button
                    onClick={e => {
                      e.stopPropagation();
                      setMenuActivoClave(prev => (prev === art.Clave ? null : art.Clave));
                    }}
                    title="Opciones de Artículo"
                    className="p-2 rounded-lg bg-slate-800/80 hover:bg-slate-700 text-slate-300 border border-slate-700 hover:text-white transition cursor-pointer"
                  >
                    <MoreVertical className="w-4 h-4" />
                  </button>

                  {/* Menú desplegable */}
                  {menuActivoClave === art.Clave && (
                    <div
                      onClick={e => e.stopPropagation()}
                      className="absolute right-0 top-full mt-1.5 w-52 bg-slate-900 border border-slate-700/80 rounded-xl shadow-2xl py-1.5 z-30 backdrop-blur"
                    >
                      {/* Opción 1: Modificar solo existencias */}
                      <button
                        onClick={() => abrirModalAjuste(art)}
                        className="w-full flex items-center gap-2.5 px-3 py-2 text-xs font-semibold text-blue-300 hover:bg-slate-800 hover:text-blue-200 transition text-left cursor-pointer border-b border-slate-800"
                      >
                        <PlusCircle className="w-4 h-4 text-blue-400" />
                        <span>Modificar Existencias</span>
                      </button>

                      {/* Opción 2: Editar Artículo completo */}
                      <button
                        onClick={() => {
                          setMenuActivoClave(null);
                          onEditarArticulo(art);
                        }}
                        className="w-full flex items-center gap-2.5 px-3 py-2 text-xs font-semibold text-amber-300 hover:bg-slate-800 hover:text-amber-200 transition text-left cursor-pointer"
                      >
                        <Edit3 className="w-4 h-4 text-amber-400" />
                        <span>Editar Artículo Completo</span>
                      </button>
                    </div>
                  )}
                </div>
              )}
            </div>
          </div>
        );
      })}

        {articulosFiltrados.length === 0 && (
          <div className="text-center py-24 text-slate-500 text-sm flex flex-col items-center gap-2">
            <Package className="w-8 h-8 opacity-40" />
            <span>No se encontraron artículos con los filtros seleccionados.</span>
          </div>
        )}
      </div>

      {/* 
        VENTANA / MODAL INTERNO DENTRO DE LA MISMA PESTAÑA PARA MODIFICAR EXISTENCIAS
        Solo accesible para el Administrador
      */}
      {articuloAjuste && esAdmin && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm z-50 flex items-center justify-center p-0 sm:p-4">
          <div className="bg-slate-900 border-0 sm:border border-slate-700 rounded-none sm:rounded-2xl max-w-md w-full p-5 sm:p-6 shadow-2xl space-y-5 h-full sm:h-auto max-h-full sm:max-h-[88vh] overflow-y-auto animate-in fade-in zoom-in-95 duration-150">
            {/* Cabecera Modal */}
            <div className="flex items-center justify-between border-b border-slate-800 pb-3">
              <div className="flex items-center gap-2.5">
                <div className="p-2 bg-blue-500/10 text-blue-400 rounded-lg border border-blue-500/20">
                  <PlusCircle className="w-5 h-5" />
                </div>
                <div>
                  <h3 className="text-base font-bold text-white">Modificar Existencias</h3>
                  <div className="text-xs text-slate-400 font-mono">Clave: {articuloAjuste.Clave}</div>
                </div>
              </div>
              <button
                onClick={() => setArticuloAjuste(null)}
                className="p-1 rounded-lg hover:bg-slate-800 text-slate-400 hover:text-white transition cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {/* Descripción del Artículo */}
            <div className="bg-slate-950 p-3 rounded-xl border border-slate-800">
              <div className="text-xs text-slate-400 uppercase font-semibold">Producto:</div>
              <div className="text-sm font-bold text-slate-200 mt-0.5">{articuloAjuste.Descripcion}</div>
              <div className="text-xs text-slate-400 font-mono mt-1">
                Stock actual: <span className="font-bold text-white">{articuloAjuste.Existencias}</span> {articuloAjuste.Paquete || 'PZA'}
              </div>
            </div>

            <form onSubmit={aplicarAjusteExistencias} className="space-y-4">
              {/* Selector de Acción */}
              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-2">
                  Tipo de Modificación:
                </label>
                <div className="grid grid-cols-3 gap-2">
                  <button
                    type="button"
                    onClick={() => setOperacionAjuste('SUMAR')}
                    className={`flex items-center justify-center gap-1.5 py-2 px-3 rounded-lg text-xs font-bold border transition cursor-pointer ${
                      operacionAjuste === 'SUMAR'
                        ? 'bg-emerald-600 text-white border-emerald-500 shadow-md shadow-emerald-950'
                        : 'bg-slate-800/80 text-slate-300 border-slate-700 hover:bg-slate-800'
                    }`}
                  >
                    <Plus className="w-3.5 h-3.5" />
                    <span>Agregar (+)</span>
                  </button>

                  <button
                    type="button"
                    onClick={() => setOperacionAjuste('RESTAR')}
                    className={`flex items-center justify-center gap-1.5 py-2 px-3 rounded-lg text-xs font-bold border transition cursor-pointer ${
                      operacionAjuste === 'RESTAR'
                        ? 'bg-rose-600 text-white border-rose-500 shadow-md shadow-rose-950'
                        : 'bg-slate-800/80 text-slate-300 border-slate-700 hover:bg-slate-800'
                    }`}
                  >
                    <Minus className="w-3.5 h-3.5" />
                    <span>Quitar (-)</span>
                  </button>

                  <button
                    type="button"
                    onClick={() => setOperacionAjuste('FIJAR')}
                    className={`flex items-center justify-center gap-1.5 py-2 px-3 rounded-lg text-xs font-bold border transition cursor-pointer ${
                      operacionAjuste === 'FIJAR'
                        ? 'bg-blue-600 text-white border-blue-500 shadow-md shadow-blue-950'
                        : 'bg-slate-800/80 text-slate-300 border-slate-700 hover:bg-slate-800'
                    }`}
                  >
                    <Edit3 className="w-3.5 h-3.5" />
                    <span>Fijar Valor</span>
                  </button>
                </div>
              </div>

              {/* Cantidad */}
              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-1.5">
                  {operacionAjuste === 'SUMAR' && 'Cantidad a agregar al inventario:'}
                  {operacionAjuste === 'RESTAR' && 'Cantidad a descontar del inventario:'}
                  {operacionAjuste === 'FIJAR' && 'Nuevo valor total exacto de existencias:'}
                </label>
                <div className="flex items-center gap-2">
                  <input
                    ref={ajusteInputRef}
                    type="number"
                    min="1"
                    required
                    value={cantidadAjuste}
                    onChange={e => setCantidadAjuste(Math.max(0, parseInt(e.target.value) || 0))}
                    className="flex-1 bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-xl px-4 py-2.5 text-lg font-mono font-bold text-white focus:outline-none"
                  />
                  <div className="text-xs text-slate-400 font-mono px-2">
                    {articuloAjuste.Paquete || 'PZA'}
                  </div>
                </div>
              </div>

              {/* Previsualización del Resultado */}
              <div className="p-3 bg-slate-950/80 rounded-xl border border-slate-800 flex justify-between items-center text-xs">
                <span className="text-slate-400">Existencias resultantes:</span>
                <span className="text-base font-bold font-mono text-emerald-400">
                  {calcularStockResultante()} {articuloAjuste.Paquete || 'PZA'}
                </span>
              </div>

              {/* Botones */}
              <div className="flex gap-2.5 pt-2">
                <button
                  type="button"
                  onClick={() => setArticuloAjuste(null)}
                  className="flex-1 bg-slate-800 hover:bg-slate-700 py-2.5 rounded-xl text-xs font-semibold text-slate-300 transition cursor-pointer"
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  disabled={guardandoAjuste}
                  className="flex-1 flex items-center justify-center gap-2 bg-blue-600 hover:bg-blue-500 text-white py-2.5 rounded-xl text-xs font-bold shadow-lg shadow-blue-950 transition cursor-pointer disabled:opacity-50"
                >
                  <Check className="w-4 h-4" />
                  <span>{guardandoAjuste ? 'Guardando...' : 'Aplicar Ajuste'}</span>
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
      {/* 
        =======================================================
        MODAL DE VENTA RÁPIDA (DISPONIBLE PARA TODOS LOS USUARIOS)
        No genera nota de venta, descuenta inventario y registra en vent_lv
        =======================================================
      */}
      {articuloVR && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm z-50 flex items-center justify-center p-0 sm:p-4 overflow-y-auto">
          <div className="bg-slate-900 border-0 sm:border border-slate-700/80 rounded-none sm:rounded-2xl max-w-lg w-full shadow-2xl flex flex-col overflow-hidden h-full sm:h-auto max-h-full sm:max-h-[88vh] animate-in fade-in zoom-in-95 duration-150">
            {/* Cabecera (Fija en la parte superior) */}
            <div className="flex items-center justify-between border-b border-slate-800 p-4 sm:px-5 shrink-0 bg-slate-900">
              <div className="flex items-center gap-2.5">
                <div className="p-2 bg-emerald-500/10 text-emerald-400 rounded-lg border border-emerald-500/20">
                  <Zap className="w-5 h-5 text-amber-400 fill-amber-400" />
                </div>
                <div>
                  <div className="flex items-center gap-2">
                    <h3 className="text-base font-bold text-white">Venta Rápida Directa</h3>
                    <span className="bg-emerald-950 text-emerald-300 border border-emerald-800/80 px-2 py-0.5 rounded text-[10px] font-bold">
                      SIN NOTA
                    </span>
                  </div>
                  <p className="text-[11px] text-slate-400">Descuenta inventario y se registra directo en Ventas del Día</p>
                </div>
              </div>
              <button
                onClick={() => {
                  setArticuloVR(null);
                  setExitoVR(null);
                }}
                className="p-1 rounded-lg hover:bg-slate-800 text-slate-400 hover:text-white transition cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {/* Mensaje de éxito al completar */}
            {exitoVR ? (
              <div className="p-6 text-center space-y-4 bg-emerald-950/40 border border-emerald-800/60 rounded-xl m-4 overflow-y-auto">
                <div className="w-12 h-12 bg-emerald-500/20 text-emerald-400 rounded-full flex items-center justify-center mx-auto border border-emerald-500/30">
                  <Check className="w-6 h-6 stroke-[3]" />
                </div>
                <div>
                  <h4 className="text-base font-bold text-white">¡Venta Rápida Registrada!</h4>
                  <p className="text-xs text-slate-300 mt-1">{exitoVR.desc}</p>
                </div>
                <div className="flex justify-center items-center gap-6 pt-1 font-mono">
                  <div>
                    <div className="text-[10px] uppercase text-slate-400 font-bold">Total Cobrado</div>
                    <div className="text-lg font-bold text-emerald-400">${exitoVR.total.toFixed(2)}</div>
                  </div>
                  {exitoVR.cambio > 0 && (
                    <div className="border-l border-emerald-800/60 pl-6">
                      <div className="text-[10px] uppercase text-amber-300 font-bold">Cambio a Entregar</div>
                      <div className="text-xl font-bold text-amber-400">${exitoVR.cambio.toFixed(2)}</div>
                    </div>
                  )}
                </div>
                <button
                  type="button"
                  onClick={() => {
                    setArticuloVR(null);
                    setExitoVR(null);
                  }}
                  className="w-full bg-emerald-600 hover:bg-emerald-500 text-white py-2 rounded-xl text-xs font-bold transition cursor-pointer mt-2"
                >
                  Listo / Cerrar
                </button>
              </div>
            ) : (
              <form onSubmit={confirmarVentaRapida} className="flex flex-col flex-1 min-h-0 overflow-hidden">
                {/* Cuerpo del formulario con Scroll dinámico adaptable al Zoom */}
                <div className="p-4 sm:px-5 space-y-3.5 overflow-y-auto flex-1 text-sm">
                {/* Detalle del Producto */}
                <div className="bg-slate-950 p-3 rounded-xl border border-slate-800 flex justify-between items-center">
                  <div>
                    <div className="text-sm font-bold text-slate-100">{articuloVR.Descripcion}</div>
                    <div className="text-xs text-slate-400 font-mono mt-0.5 flex items-center gap-2">
                      <span className="text-blue-400">Clave: {articuloVR.Clave}</span>
                      <span>•</span>
                      <span>
                        Stock:{' '}
                        {(articuloVR.Tipo || '').toLowerCase() === 'servicio' || (articuloVR.Tipo || '').toLowerCase() === 'trabajo' ? (
                          <strong className="text-purple-400">Servicio (No reduce existencias)</strong>
                        ) : (articuloVR.Tipo || '').toLowerCase() === 'infinito' ? (
                          <strong className="text-emerald-400">Infinito (No reduce existencias)</strong>
                        ) : (
                          <>
                            <strong className={articuloVR.Existencias > 0 ? 'text-emerald-400' : 'text-rose-400'}>
                              {articuloVR.Existencias}
                            </strong>{' '}
                            {articuloVR.Paquete || 'PZA'}
                          </>
                        )}
                      </span>
                    </div>
                  </div>
                </div>

                {/* Banner de Advertencia por Existencias Insuficientes */}
                {stockInsuficienteVR && (
                  <div className="p-3 bg-rose-950/70 border border-rose-800/80 rounded-xl flex items-center gap-2.5 text-rose-300 text-xs">
                    <AlertTriangle className="w-5 h-5 text-rose-400 shrink-0" />
                    <div>
                      <strong className="block font-bold text-white">Existencias insuficientes</strong>
                      <span>
                        {stockDisponibleVR <= 0
                          ? `Este artículo se encuentra agotado (${stockDisponibleVR} disponibles en inventario).`
                          : `Solo hay ${stockDisponibleVR} en inventario y solicitas vender ${cantidadVR}.`}
                      </span>
                    </div>
                  </div>
                )}

                {/* Selector de Precio */}
                <div>
                  <label className="block text-[11px] font-semibold text-slate-300 mb-1.5">
                    Precio a aplicar:
                  </label>
                  <div className="grid grid-cols-3 gap-1.5">
                    {(['Menudeo', 'Mayoreo', 'Especial'] as const).map(tipo => {
                      const valor = articuloVR[tipo] || 0;
                      const seleccionado = tipoPrecioVR === tipo;
                      return (
                        <button
                          key={tipo}
                          type="button"
                          onClick={() => seleccionarTipoPrecioVR(tipo)}
                          className={`p-2 rounded-lg text-center transition cursor-pointer border ${
                            seleccionado
                              ? 'bg-blue-600 text-white border-blue-500 shadow-md shadow-blue-950'
                              : 'bg-slate-950 text-slate-300 border-slate-800 hover:bg-slate-800'
                          }`}
                        >
                          <div className="text-[10px] font-semibold uppercase">
                            {tipo === 'Especial' ? 'Especial / PME' : tipo}
                          </div>
                          <div className="text-xs font-bold font-mono mt-0.5">${valor.toFixed(2)}</div>
                          {tipo === 'Especial' && (articuloVR.PME || 0) > 0 && (
                            <div className="text-[9px] text-amber-400 font-normal mt-0.5">
                              (≥ {articuloVR.PME} pz)
                            </div>
                          )}
                        </button>
                      );
                    })}
                  </div>
                </div>

                {/* Cantidad y Total */}
                <div className="grid grid-cols-2 gap-3 items-center">
                  <div>
                    <label className="block text-[11px] font-semibold text-slate-300 mb-1.5">
                      Cantidad a vender:
                    </label>
                    <div className="flex items-center gap-1.5">
                      <button
                        type="button"
                        onClick={() => cambiarCantidadVR(cantidadVR - 1)}
                        className="p-2 bg-slate-800 hover:bg-slate-700 text-slate-200 rounded-lg cursor-pointer"
                      >
                        <Minus className="w-4 h-4" />
                      </button>
                      <input
                        ref={vrCantidadInputRef}
                        type="number"
                        min="1"
                        inputMode="numeric"
                        required
                        value={cantidadVR}
                        onChange={e => cambiarCantidadVR(parseInt(e.target.value) || 1)}
                        className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg py-1.5 text-center text-base font-mono font-bold text-white focus:outline-none"
                      />
                      <button
                        type="button"
                        onClick={() => cambiarCantidadVR(cantidadVR + 1)}
                        className="p-2 bg-slate-800 hover:bg-slate-700 text-slate-200 rounded-lg cursor-pointer"
                      >
                        <Plus className="w-4 h-4" />
                      </button>
                    </div>
                  </div>

                  <div className="bg-slate-950 p-2.5 rounded-xl border border-slate-800 text-right">
                    <div className="text-[10px] uppercase font-bold text-slate-400">Total a Cobrar</div>
                    <div className="text-2xl font-bold font-mono text-emerald-400">
                      ${totalCalculadoVR.toFixed(2)}
                    </div>
                  </div>
                </div>

                {/* Métodos de Pago Divididos (Efectivo, Tarjeta, Transferencia) */}
                <div className="space-y-3 pt-1 border-t border-slate-800">
                  <div className="flex justify-between items-center">
                    <label className="text-[11px] font-semibold text-slate-300">
                      Forma de Pago (puede dividirse en varios):
                    </label>

                    {/* Atajos de pago 100% rápido */}
                    <div className="flex gap-1">
                      <button
                        type="button"
                        onClick={() => pagarTodoConVR('efectivo')}
                        className={`text-[10px] px-2 py-0.5 rounded border transition cursor-pointer flex items-center gap-1 ${
                          numEfVR === totalCalculadoVR && numTarVR === 0 && numTraVR === 0
                            ? 'bg-emerald-600 text-white border-emerald-500 font-bold'
                            : 'bg-slate-800 hover:bg-slate-700 text-slate-300 border-slate-700'
                        }`}
                      >
                        <Banknote className="w-3 h-3 text-emerald-400" />
                        <span>Efec 100%</span>
                      </button>
                      <button
                        type="button"
                        onClick={() => pagarTodoConVR('tarjeta')}
                        className={`text-[10px] px-2 py-0.5 rounded border transition cursor-pointer flex items-center gap-1 ${
                          numTarVR === totalCalculadoVR && numEfVR === 0 && numTraVR === 0
                            ? 'bg-blue-600 text-white border-blue-500 font-bold'
                            : 'bg-slate-800 hover:bg-slate-700 text-slate-300 border-slate-700'
                        }`}
                      >
                        <CreditCard className="w-3 h-3 text-blue-400" />
                        <span>Tarj 100%</span>
                      </button>
                      <button
                        type="button"
                        onClick={() => pagarTodoConVR('transferencia')}
                        className={`text-[10px] px-2 py-0.5 rounded border transition cursor-pointer flex items-center gap-1 ${
                          numTraVR === totalCalculadoVR && numEfVR === 0 && numTarVR === 0
                            ? 'bg-purple-600 text-white border-purple-500 font-bold'
                            : 'bg-slate-800 hover:bg-slate-700 text-slate-300 border-slate-700'
                        }`}
                      >
                        <ArrowLeftRight className="w-3 h-3 text-purple-400" />
                        <span>Trans 100%</span>
                      </button>
                    </div>
                  </div>

                  {/* 1. Efectivo */}
                  <div className="bg-slate-950 p-2.5 rounded-xl border border-slate-800 space-y-1.5">
                    <div className="flex justify-between items-center">
                      <div className="flex items-center gap-1.5 text-xs font-semibold text-slate-200">
                        <Banknote className="w-3.5 h-3.5 text-emerald-400" />
                        <span>Efectivo ($)</span>
                      </div>
                      <div className="flex items-center gap-1">
                        <button
                          type="button"
                          onClick={() => setPagoEfectivoVR(totalCalculadoVR.toString())}
                          className="text-[10px] bg-slate-800 hover:bg-slate-700 text-slate-300 px-1.5 py-0.5 rounded cursor-pointer"
                        >
                          Exacto
                        </button>
                        <button
                          type="button"
                          onClick={() => setPagoEfectivoVR((Math.ceil(totalCalculadoVR / 50) * 50 || 50).toString())}
                          className="text-[10px] bg-slate-800 hover:bg-slate-700 text-slate-300 px-1.5 py-0.5 rounded cursor-pointer"
                        >
                          +$50
                        </button>
                        <button
                          type="button"
                          onClick={() => setPagoEfectivoVR((Math.ceil(totalCalculadoVR / 100) * 100 || 100).toString())}
                          className="text-[10px] bg-slate-800 hover:bg-slate-700 text-slate-300 px-1.5 py-0.5 rounded cursor-pointer"
                        >
                          +$100
                        </button>
                        <button
                          type="button"
                          onClick={() => completarRestoVR('efectivo')}
                          className="text-[10px] bg-emerald-950 hover:bg-emerald-900 text-emerald-300 border border-emerald-800/60 px-2 py-0.5 rounded cursor-pointer font-bold"
                          title="Llenar lo que falta con efectivo"
                        >
                          Resto
                        </button>
                      </div>
                    </div>

                    <input
                      type="number"
                      step="any"
                      min="0"
                      inputMode="decimal"
                      value={pagoEfectivoVR}
                      onChange={e => setPagoEfectivoVR(e.target.value)}
                      placeholder="0.00"
                      className="w-full bg-slate-900 border border-slate-700 focus:border-emerald-500 rounded-lg px-3 py-1.5 text-sm font-mono font-bold text-white focus:outline-none"
                    />
                  </div>

                  {/* 2. Tarjeta y 3. Transferencia (en 2 columnas) */}
                  <div className="grid grid-cols-2 gap-2">
                    {/* Tarjeta */}
                    <div className="bg-slate-950 p-2.5 rounded-xl border border-slate-800 space-y-1.5">
                      <div className="flex justify-between items-center">
                        <div className="flex items-center gap-1.5 text-xs font-semibold text-slate-200">
                          <CreditCard className="w-3.5 h-3.5 text-blue-400" />
                          <span>Tarjeta ($)</span>
                        </div>
                        <button
                          type="button"
                          onClick={() => completarRestoVR('tarjeta')}
                          className="text-[10px] bg-blue-950 hover:bg-blue-900 text-blue-300 border border-blue-800/60 px-2 py-0.5 rounded cursor-pointer font-bold"
                          title="Llenar lo que falta con tarjeta"
                        >
                          Resto
                        </button>
                      </div>
                      <input
                        type="number"
                        step="any"
                        min="0"
                        inputMode="decimal"
                        value={pagoTarjetaVR}
                        onChange={e => setPagoTarjetaVR(e.target.value)}
                        placeholder="0.00"
                        className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-1.5 text-sm font-mono font-bold text-white focus:outline-none"
                      />
                    </div>

                    {/* Transferencia */}
                    <div className="bg-slate-950 p-2.5 rounded-xl border border-slate-800 space-y-1.5">
                      <div className="flex justify-between items-center">
                        <div className="flex items-center gap-1.5 text-xs font-semibold text-slate-200">
                          <ArrowLeftRight className="w-3.5 h-3.5 text-purple-400" />
                          <span>Transfer ($)</span>
                        </div>
                        <button
                          type="button"
                          onClick={() => completarRestoVR('transferencia')}
                          className="text-[10px] bg-purple-950 hover:bg-purple-900 text-purple-300 border border-purple-800/60 px-2 py-0.5 rounded cursor-pointer font-bold"
                          title="Llenar lo que falta con transferencia"
                        >
                          Resto
                        </button>
                      </div>
                      <input
                        type="number"
                        step="any"
                        min="0"
                        inputMode="decimal"
                        value={pagoTransferVR}
                        onChange={e => setPagoTransferVR(e.target.value)}
                        placeholder="0.00"
                        className="w-full bg-slate-900 border border-slate-700 focus:border-purple-500 rounded-lg px-3 py-1.5 text-sm font-mono font-bold text-white focus:outline-none"
                      />
                    </div>
                  </div>

                  {/* Resumen del Balance de Pago y Cambio */}
                  <div className="p-2.5 bg-slate-950/90 rounded-xl border border-slate-800 flex justify-between items-center text-xs font-mono">
                    <div>
                      <span className="text-slate-400">Total Ingresado: </span>
                      <strong className={`font-bold ${totalPagadoVR >= totalCalculadoVR ? 'text-white' : 'text-amber-400'}`}>
                        ${totalPagadoVR.toFixed(2)}
                      </strong>
                    </div>

                    {faltaCubrirVR > 0 ? (
                      <div className="text-rose-400 font-bold animate-pulse">
                        Falta: ${faltaCubrirVR.toFixed(2)}
                      </div>
                    ) : cambioCalculadoVR > 0 ? (
                      <div className="text-amber-400 font-bold text-sm">
                        Cambio: ${cambioCalculadoVR.toFixed(2)}
                      </div>
                    ) : (
                      <div className="text-emerald-400 font-bold">
                        Total Cubierto ✓
                      </div>
                    )}
                  </div>
                </div>
                </div>

                {/* Botones de Acción (Fijos en la base del modal, siempre accesibles) */}
                <div className="flex gap-2.5 p-3.5 sm:px-5 border-t border-slate-800 bg-slate-900/95 shrink-0">
                  <button
                    type="button"
                    onClick={() => setArticuloVR(null)}
                    className="flex-1 bg-slate-800 hover:bg-slate-700 py-2.5 rounded-xl text-xs font-semibold text-slate-300 transition cursor-pointer"
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    disabled={procesandoVR || faltaCubrirVR > 0 || stockInsuficienteVR}
                    className="flex-1 flex items-center justify-center gap-2 bg-emerald-600 hover:bg-emerald-500 text-white py-2.5 rounded-xl text-xs font-bold shadow-lg shadow-emerald-950 transition cursor-pointer disabled:opacity-40 disabled:cursor-not-allowed"
                  >
                    <Zap className="w-4 h-4 fill-white text-white" />
                    <span>
                      {procesandoVR
                        ? 'Cobrando...'
                        : stockInsuficienteVR
                        ? 'Existencias Insuficientes'
                        : faltaCubrirVR > 0
                        ? `Faltan $${faltaCubrirVR.toFixed(2)}`
                        : `Cobrar Venta Rápida: $${totalCalculadoVR.toFixed(2)}`}
                    </span>
                  </button>
                </div>
              </form>
            )}
          </div>
        </div>
      )}
    </div>
  );
}
