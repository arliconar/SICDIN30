import { useState, useEffect, useRef } from 'react';
import {
  Wrench,
  Bike,
  Search,
  DollarSign,
  Phone,
  User,
  Calendar,
  X,
  Check,
  Trash2,
  Layers,
  Banknote,
  CreditCard,
  ArrowLeftRight,
  Plus,
  MapPin
} from 'lucide-react';
import { api, type Articulo, type Cliente } from './api';

interface NuevaReparacionViewProps {
  onOrdenGuardada: (folio: string) => void;
  onCerrar: () => void;
}

interface ItemBici {
  id: string; // ID temporal único para la interfaz
  Clave: string;
  Descripcion: string;
  Cantidad: number;
  Precio: number;
  Trabajo: number; // 0 = Refacción, 1 = Mano de obra
  Paquete?: string;
}

interface BicicletaEnOrden {
  biciId: number;
  Marca: string;
  Color: string;
  Rodada: string;
  Nota: string;
  Fhentrega: string;
  articulos: ItemBici[];
}

export default function NuevaReparacionView({ onOrdenGuardada, onCerrar }: NuevaReparacionViewProps) {
  // Datos del Cliente
  const [clienteNombre, setClienteNombre] = useState('');
  const [clienteTelcel, setClienteTelcel] = useState('');
  const [clienteDireccion, setClienteDireccion] = useState('');
  const [clienteRFC, setClienteRFC] = useState('');
  const [clienteMail, setClienteMail] = useState('');

  // Selector / Autocompletado de Clientes existentes
  const [clientesSugeridos, setClientesSugeridos] = useState<Cliente[]>([]);
  const [mostrarSugerenciasClnt, setMostrarSugerenciasClnt] = useState(false);

  // Fecha prometida de entrega general (por defecto para mañana a las 5 PM)
  const [fechaEntregaGeneral, setFechaEntregaGeneral] = useState(() => {
    const manana = new Date();
    manana.setDate(manana.getDate() + 1);
    manana.setHours(17, 0, 0, 0);
    return manana.toISOString().slice(0, 16);
  });

  // Bicicletas en la orden (cada una con sus propios artículos/ventas)
  const [bicicletas, setBicicletas] = useState<BicicletaEnOrden[]>([
    {
      biciId: 1,
      Marca: '',
      Color: '',
      Rodada: '26',
      Nota: '',
      Fhentrega: '',
      articulos: []
    }
  ]);
  const [biciActivaIdx, setBiciActivaIdx] = useState(0);

  // Buscador de refacciones / mano de obra
  const [queryArticulo, setQueryArticulo] = useState('');
  const [sugerenciasArt, setSugerenciasArt] = useState<Articulo[]>([]);

  // Anticipo inicial combinado
  const [anticipoEfectivo, setAnticipoEfectivo] = useState('');
  const [anticipoTarjeta, setAnticipoTarjeta] = useState('');
  const [anticipoTransfer, setAnticipoTransfer] = useState('');
  const [guardando, setGuardando] = useState(false);

  const searchArtInputRef = useRef<HTMLInputElement>(null);

  // Búsqueda en vivo de clientes para autocompletado rápido
  useEffect(() => {
    if (!clienteNombre.trim() || clienteNombre.length < 2) {
      setClientesSugeridos([]);
      return;
    }
    const timer = setTimeout(async () => {
      try {
        const res = await api.buscarClientes(clienteNombre);
        setClientesSugeridos(res.slice(0, 5));
        setMostrarSugerenciasClnt(res.length > 0);
      } catch (err) {
        console.error(err);
      }
    }, 150);
    return () => clearTimeout(timer);
  }, [clienteNombre]);

  // Búsqueda en vivo de artículos / refacciones
  useEffect(() => {
    if (!queryArticulo.trim()) {
      setSugerenciasArt([]);
      return;
    }
    const timer = setTimeout(async () => {
      try {
        const res = await api.buscarArticulos(queryArticulo);
        setSugerenciasArt(res.slice(0, 10));
      } catch (err) {
        console.error(err);
      }
    }, 80);
    return () => clearTimeout(timer);
  }, [queryArticulo]);

  const seleccionarClienteExistente = (clnt: Cliente) => {
    setClienteNombre(clnt.Nombre);
    setClienteTelcel(clnt.Telcel || '');
    setClienteDireccion(clnt.Direccion || '');
    setClienteRFC(clnt.RFC || '');
    setClienteMail(clnt.mail || '');
    setMostrarSugerenciasClnt(false);
  };

  // Gestión de Bicicletas
  const agregarBicicleta = () => {
    const nuevoId = bicicletas.length + 1;
    setBicicletas(prev => [
      ...prev,
      {
        biciId: nuevoId,
        Marca: '',
        Color: '',
        Rodada: '26',
        Nota: '',
        Fhentrega: '',
        articulos: []
      }
    ]);
    setBiciActivaIdx(bicicletas.length);
  };

  const eliminarBicicleta = (index: number) => {
    if (bicicletas.length <= 1) return;
    setBicicletas(prev => prev.filter((_, i) => i !== index));
    setBiciActivaIdx(Math.max(0, index - 1));
  };

  const actualizarBiciActiva = (campos: Partial<BicicletaEnOrden>) => {
    setBicicletas(prev =>
      prev.map((b, idx) => (idx === biciActivaIdx ? { ...b, ...campos } : b))
    );
  };

  // Gestión de artículos / refacciones para la bicicleta activa
  const agregarArticuloABici = (art: Articulo) => {
    setBicicletas(prev =>
      prev.map((b, idx) => {
        if (idx !== biciActivaIdx) return b;
        const existeIdx = b.articulos.findIndex(a => a.Clave === art.Clave);
        if (existeIdx >= 0) {
          const nuevosArts = [...b.articulos];
          nuevosArts[existeIdx].Cantidad += 1;
          return { ...b, articulos: nuevosArts };
        }
        return {
          ...b,
          articulos: [
            ...b.articulos,
            {
              id: `${art.Clave}_${Date.now()}`,
              Clave: art.Clave,
              Descripcion: art.Descripcion,
              Cantidad: 1,
              Precio: art.Menudeo,
              Trabajo: art.Tipo === 'Servicios' || art.Clave.startsWith('TR') ? 1 : 0,
              Paquete: art.Paquete || 'PZA'
            }
          ]
        };
      })
    );
    setQueryArticulo('');
    setSugerenciasArt([]);
    searchArtInputRef.current?.focus();
  };

  const agregarServicioManoObra = (descripcion: string, precio: number) => {
    const claveServicio = `TR_${Date.now().toString().slice(-4)}`;
    setBicicletas(prev =>
      prev.map((b, idx) => {
        if (idx !== biciActivaIdx) return b;
        return {
          ...b,
          articulos: [
            ...b.articulos,
            {
              id: claveServicio,
              Clave: claveServicio,
              Descripcion: descripcion,
              Cantidad: 1,
              Precio: precio,
              Trabajo: 1,
              Paquete: 'MO'
            }
          ]
        };
      })
    );
  };

  const modificarCantidadItem = (itemId: string, delta: number) => {
    setBicicletas(prev =>
      prev.map((b, idx) => {
        if (idx !== biciActivaIdx) return b;
        return {
          ...b,
          articulos: b.articulos
            .map(a => (a.id === itemId ? { ...a, Cantidad: a.Cantidad + delta } : a))
            .filter(a => a.Cantidad > 0)
        };
      })
    );
  };

  const eliminarItemDeBici = (itemId: string) => {
    setBicicletas(prev =>
      prev.map((b, idx) => {
        if (idx !== biciActivaIdx) return b;
        return {
          ...b,
          articulos: b.articulos.filter(a => a.id !== itemId)
        };
      })
    );
  };

  // Cálculos consolidados de la Orden
  const calcularSubtotalBici = (b: BicicletaEnOrden) =>
    b.articulos.reduce((sum, it) => sum + (it.Cantidad * it.Precio), 0);

  const totalOrdenCompleta = bicicletas.reduce(
    (sum, b) => sum + calcularSubtotalBici(b),
    0
  );

  const numAnticipoEf = parseFloat(anticipoEfectivo) || 0;
  const numAnticipoTar = parseFloat(anticipoTarjeta) || 0;
  const numAnticipoTra = parseFloat(anticipoTransfer) || 0;
  const totalAnticipo = numAnticipoEf + numAnticipoTar + numAnticipoTra;
  const saldoPendiente = Math.max(0, totalOrdenCompleta - totalAnticipo);

  const asignarAnticipoExacto = (porcentaje: number) => {
    const monto = (totalOrdenCompleta * porcentaje) / 100;
    setAnticipoEfectivo(monto > 0 ? monto.toFixed(2) : '');
    setAnticipoTarjeta('');
    setAnticipoTransfer('');
  };

  const guardarOrden = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!clienteNombre.trim()) {
      alert('Por favor ingresa el nombre del cliente.');
      return;
    }

    // Validar que cada bicicleta tenga al menos marca
    for (let i = 0; i < bicicletas.length; i++) {
      if (!bicicletas[i].Marca.trim()) {
        alert(`La Bicicleta #${i + 1} requiere al menos una Marca o descripción.`);
        setBiciActivaIdx(i);
        return;
      }
    }

    setGuardando(true);
    try {
      // Aplanar todos los artículos asociándolos a su biciId correspondiente
      const articulosAplanados = bicicletas.flatMap(b =>
        b.articulos.map(a => ({
          biciId: b.biciId,
          Clave: a.Clave,
          Descripcion: a.Descripcion,
          Cantidad: a.Cantidad,
          Precio: a.Precio,
          Trabajo: a.Trabajo,
          Paquete: a.Paquete
        }))
      );

      const payload = {
        clienteNombre: clienteNombre.trim(),
        clienteTelcel: clienteTelcel.trim(),
        clienteDireccion: clienteDireccion.trim(),
        clienteRFC: clienteRFC.trim(),
        clienteMail: clienteMail.trim(),
        fechaEntregaPrometida: fechaEntregaGeneral ? fechaEntregaGeneral.replace('T', ' ') + ':00' : undefined,
        bicicletas: bicicletas.map(b => ({
          Id: b.biciId,
          Marca: b.Marca.trim(),
          Color: b.Color.trim(),
          Rodada: b.Rodada,
          Nota: b.Nota.trim(),
          Fhentrega: b.Fhentrega ? b.Fhentrega.replace('T', ' ') + ':00' : (fechaEntregaGeneral ? fechaEntregaGeneral.replace('T', ' ') + ':00' : undefined)
        })),
        articulos: articulosAplanados,
        anticipoEfectivo: numAnticipoEf,
        anticipoTarjeta: numAnticipoTar,
        anticipoTransfer: numAnticipoTra
      };

      const res = await api.crearReparacion(payload);
      if (res.success) {
        alert(
          `¡Orden de servicio generada con éxito!\n\n` +
          `Folio: ${res.folio}\n` +
          `Cliente: ${clienteNombre}\n` +
          `Bicicletas registradas: ${bicicletas.length}\n` +
          `Total orden: $${res.total.toFixed(2)}\n` +
          `Anticipo entregado: $${res.anticipo.toFixed(2)}\n` +
          `Saldo pendiente por liquidar: $${res.resta.toFixed(2)}`
        );
        onOrdenGuardada(res.folio);
      }
    } catch (err: any) {
      alert(`Error al guardar la orden: ${err.message}`);
    } finally {
      setGuardando(false);
    }
  };

  const biciActiva = bicicletas[biciActivaIdx] || bicicletas[0];

  return (
    <div className="flex-1 flex flex-col h-full overflow-hidden bg-slate-950 text-slate-100 select-none">
      {/* Cabecera de la Nueva Orden */}
      <div className="h-14 px-6 border-b border-slate-800 bg-slate-900/60 flex items-center justify-between shrink-0">
        <div className="flex items-center gap-3">
          <div className="p-2 rounded-lg bg-amber-600/20 text-amber-400 border border-amber-500/30">
            <Wrench className="w-5 h-5" />
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h1 className="text-base font-bold text-white tracking-wide">Nueva Orden de Servicio</h1>
              <span className="bg-blue-950 text-blue-300 border border-blue-800 px-2 py-0.5 rounded text-[10px] font-bold">
                TALLER
              </span>
            </div>
            <div className="text-[11px] text-slate-400">
              Registra una orden para una o múltiples bicicletas con sus refacciones y mano de obra
            </div>
          </div>
        </div>

        <div className="flex items-center gap-2">
          <button
            onClick={onCerrar}
            className="flex items-center gap-1.5 text-xs text-slate-400 hover:text-white px-3 py-1.5 rounded-lg hover:bg-slate-800 transition cursor-pointer"
          >
            <X className="w-4 h-4" />
            <span>Cancelar / Cerrar pestaña</span>
          </button>
        </div>
      </div>

      {/* Contenido Principal con Scroll */}
      <form onSubmit={guardarOrden} className="flex-1 overflow-y-auto p-6 space-y-5">
        {/* 1. Datos del Cliente y Entrega */}
        <div className="bg-slate-900/80 rounded-2xl p-5 border border-slate-800 space-y-4 shadow-sm">
          <div className="flex items-center justify-between border-b border-slate-800/80 pb-3">
            <div className="flex items-center gap-2 text-sm font-bold text-slate-200">
              <User className="w-4 h-4 text-blue-400" />
              <span>1. Datos del Cliente y Fecha de Entrega</span>
            </div>
            <span className="text-xs text-slate-500 font-normal">
              Escribe el nombre o selecciónalo del directorio
            </span>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            {/* Nombre del Cliente con Autocompletado */}
            <div className="relative">
              <label className="block text-xs font-semibold text-slate-300 mb-1">Nombre del Cliente *</label>
              <input
                type="text"
                required
                value={clienteNombre}
                onChange={e => setClienteNombre(e.target.value)}
                placeholder="Escribe el nombre del cliente..."
                className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3.5 py-2 text-sm text-white focus:outline-none"
              />

              {mostrarSugerenciasClnt && clientesSugeridos.length > 0 && (
                <div className="absolute top-16 left-0 right-0 bg-slate-900 border border-slate-700 rounded-xl shadow-2xl z-30 divide-y divide-slate-800 overflow-hidden">
                  <div className="px-3 py-1.5 bg-slate-950 text-[10px] text-slate-400 uppercase font-bold">
                    Clientes Registrados
                  </div>
                  {clientesSugeridos.map(c => (
                    <div
                      key={c.Id}
                      onClick={() => seleccionarClienteExistente(c)}
                      className="p-2.5 hover:bg-slate-800/80 cursor-pointer flex justify-between items-center text-xs transition"
                    >
                      <div>
                        <div className="font-semibold text-slate-200">{c.Nombre}</div>
                        {c.Telcel && <div className="text-[11px] text-slate-400">Tel: {c.Telcel}</div>}
                      </div>
                      <span className="text-[10px] bg-slate-800 text-slate-400 px-1.5 py-0.5 rounded font-mono">
                        #{c.Id}
                      </span>
                    </div>
                  ))}
                </div>
              )}
            </div>

            {/* Teléfono Celular */}
            <div>
              <label className="block text-xs font-semibold text-slate-300 mb-1">Teléfono Celular / WhatsApp</label>
              <div className="relative">
                <Phone className="absolute left-3 top-2.5 text-slate-500 w-4 h-4" />
                <input
                  type="text"
                  value={clienteTelcel}
                  onChange={e => setClienteTelcel(e.target.value)}
                  placeholder="771..."
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg pl-9 pr-3.5 py-2 text-sm text-white focus:outline-none font-mono"
                />
              </div>
            </div>

            {/* Fecha y Hora de Entrega Prometida General */}
            <div>
              <label className="block text-xs font-semibold text-slate-300 mb-1">Promesa de Entrega General</label>
              <div className="relative">
                <Calendar className="absolute left-3 top-2.5 text-slate-500 w-4 h-4" />
                <input
                  type="datetime-local"
                  value={fechaEntregaGeneral}
                  onChange={e => setFechaEntregaGeneral(e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg pl-9 pr-3.5 py-2 text-xs text-white focus:outline-none"
                />
              </div>
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4 pt-1">
            <div>
              <label className="block text-xs font-semibold text-slate-400 mb-1">Dirección (Opcional)</label>
              <div className="relative">
                <MapPin className="absolute left-3 top-2.5 text-slate-600 w-4 h-4" />
                <input
                  type="text"
                  value={clienteDireccion}
                  onChange={e => setClienteDireccion(e.target.value)}
                  placeholder="Calle, colonia..."
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg pl-9 pr-3.5 py-2 text-xs text-white focus:outline-none"
                />
              </div>
            </div>

            <div className="grid grid-cols-2 gap-3">
              <div>
                <label className="block text-xs font-semibold text-slate-400 mb-1">RFC</label>
                <input
                  type="text"
                  value={clienteRFC}
                  onChange={e => setClienteRFC(e.target.value)}
                  placeholder="XAXX010101000"
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-xs text-white uppercase font-mono focus:outline-none"
                />
              </div>
              <div>
                <label className="block text-xs font-semibold text-slate-400 mb-1">Correo Electrónico</label>
                <input
                  type="email"
                  value={clienteMail}
                  onChange={e => setClienteMail(e.target.value)}
                  placeholder="correo@ejemplo.com"
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-xs text-white focus:outline-none"
                />
              </div>
            </div>
          </div>
        </div>

        {/* 2. Sección de Bicicletas: Cada Bici tiene su espacio y sus refacciones */}
        <div className="bg-slate-900/80 rounded-2xl p-5 border border-slate-800 space-y-4 shadow-sm">
          <div className="flex flex-wrap items-center justify-between gap-3 border-b border-slate-800/80 pb-3">
            <div className="flex items-center gap-2 text-sm font-bold text-slate-200">
              <Bike className="w-4 h-4 text-emerald-400" />
              <span>2. Bicicletas en esta Orden ({bicicletas.length})</span>
            </div>

            {/* Pestañas de las Bicicletas */}
            <div className="flex items-center gap-2 overflow-x-auto">
              {bicicletas.map((bici, idx) => {
                const sub = calcularSubtotalBici(bici);
                const isActiva = idx === biciActivaIdx;
                return (
                  <button
                    key={bici.biciId}
                    type="button"
                    onClick={() => setBiciActivaIdx(idx)}
                    className={`px-3 py-1.5 rounded-lg text-xs font-semibold flex items-center gap-2 transition cursor-pointer border ${
                      isActiva
                        ? 'bg-emerald-600 text-white border-emerald-500 shadow-md'
                        : 'bg-slate-800 text-slate-300 border-slate-700 hover:bg-slate-750'
                    }`}
                  >
                    <Bike className="w-3.5 h-3.5" />
                    <span>
                      Bici #{bici.biciId} {bici.Marca ? `(${bici.Marca})` : ''}
                    </span>
                    <span className="font-mono text-[11px] opacity-80">
                      ${sub.toFixed(2)}
                    </span>
                  </button>
                );
              })}

              <button
                type="button"
                onClick={agregarBicicleta}
                className="px-3 py-1.5 rounded-lg text-xs font-bold bg-blue-600/20 hover:bg-blue-600/30 text-blue-300 border border-blue-500/40 flex items-center gap-1.5 transition cursor-pointer"
                title="Agregar otra bicicleta a esta misma orden"
              >
                <Plus className="w-3.5 h-3.5" />
                <span>+ Agregar otra Bici</span>
              </button>
            </div>
          </div>

          {/* Ficha y Ventas de la Bicicleta Activa */}
          <div className="bg-slate-950/70 rounded-xl p-4 border border-slate-800/90 space-y-4">
            <div className="flex justify-between items-center pb-2 border-b border-slate-800/60">
              <div className="flex items-center gap-2">
                <span className="text-xs font-bold text-emerald-400">
                  EDITANDO BICICLETA #{biciActiva.biciId}
                </span>
                <span className="text-xs text-slate-500">|</span>
                <span className="text-xs text-slate-400">
                  Subtotal piezas y mano de obra: <strong className="text-white font-mono">${calcularSubtotalBici(biciActiva).toFixed(2)}</strong>
                </span>
              </div>

              {bicicletas.length > 1 && (
                <button
                  type="button"
                  onClick={() => eliminarBicicleta(biciActivaIdx)}
                  className="text-xs text-rose-400 hover:text-rose-300 hover:bg-rose-950/40 px-2 py-1 rounded transition flex items-center gap-1 cursor-pointer"
                >
                  <Trash2 className="w-3.5 h-3.5" />
                  <span>Eliminar esta bicicleta</span>
                </button>
              )}
            </div>

            {/* Datos específicos de esta bicicleta */}
            <div className="grid grid-cols-1 md:grid-cols-4 gap-3">
              <div>
                <label className="block text-[11px] text-slate-300 font-semibold mb-1">Marca / Modelo *</label>
                <input
                  type="text"
                  required
                  value={biciActiva.Marca}
                  onChange={e => actualizarBiciActiva({ Marca: e.target.value })}
                  placeholder="Ej: Benotto, Trek, Alubike..."
                  className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-1.5 text-xs text-white focus:outline-none focus:border-emerald-500 font-semibold"
                />
              </div>

              <div>
                <label className="block text-[11px] text-slate-300 font-semibold mb-1">Color</label>
                <input
                  type="text"
                  value={biciActiva.Color}
                  onChange={e => actualizarBiciActiva({ Color: e.target.value })}
                  placeholder="Ej: Rojo / Negro"
                  className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-1.5 text-xs text-white focus:outline-none focus:border-emerald-500"
                />
              </div>

              <div>
                <label className="block text-[11px] text-slate-300 font-semibold mb-1">Rodada</label>
                <select
                  value={biciActiva.Rodada}
                  onChange={e => actualizarBiciActiva({ Rodada: e.target.value })}
                  className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-1.5 text-xs text-white focus:outline-none focus:border-emerald-500 font-mono"
                >
                  {['12', '16', '20', '24', '26', '27.5', '29', '700c'].map(r => (
                    <option key={r} value={r}>
                      R{r}
                    </option>
                  ))}
                </select>
              </div>

              <div>
                <label className="block text-[11px] text-slate-300 font-semibold mb-1">Falla / Solicitud</label>
                <input
                  type="text"
                  value={biciActiva.Nota}
                  onChange={e => actualizarBiciActiva({ Nota: e.target.value })}
                  placeholder="Ej: Cadena rota, ajustar frenos..."
                  className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-1.5 text-xs text-white focus:outline-none focus:border-emerald-500"
                />
              </div>
            </div>

            {/* Refacciones y Mano de Obra para ESTA bicicleta */}
            <div className="pt-2 border-t border-slate-800/80 space-y-3">
              <div className="flex flex-wrap items-center justify-between gap-2">
                <div className="text-xs font-bold text-slate-300 flex items-center gap-1.5">
                  <Layers className="w-3.5 h-3.5 text-purple-400" />
                  <span>Refacciones y Trabajos para Bici #{biciActiva.biciId}</span>
                </div>

                {/* Atajos de Mano de Obra común */}
                <div className="flex flex-wrap gap-1.5">
                  <button
                    type="button"
                    onClick={() => agregarServicioManoObra('Ajuste General de Frenos y Cambios', 120)}
                    className="bg-slate-800 hover:bg-slate-700 text-slate-200 px-2 py-1 rounded text-[11px] transition cursor-pointer border border-slate-700"
                  >
                    + Ajuste General ($120)
                  </button>
                  <button
                    type="button"
                    onClick={() => agregarServicioManoObra('Parchado de Llanta', 30)}
                    className="bg-slate-800 hover:bg-slate-700 text-slate-200 px-2 py-1 rounded text-[11px] transition cursor-pointer border border-slate-700"
                  >
                    + Parche ($30)
                  </button>
                  <button
                    type="button"
                    onClick={() => agregarServicioManoObra('Servicio de Mantenimiento Completo', 250)}
                    className="bg-slate-800 hover:bg-slate-700 text-slate-200 px-2 py-1 rounded text-[11px] transition cursor-pointer border border-slate-700"
                  >
                    + Servicio Completo ($250)
                  </button>
                  <button
                    type="button"
                    onClick={() => agregarServicioManoObra('Centrado de Rin', 70)}
                    className="bg-slate-800 hover:bg-slate-700 text-slate-200 px-2 py-1 rounded text-[11px] transition cursor-pointer border border-slate-700"
                  >
                    + Centrado Rin ($70)
                  </button>
                </div>
              </div>

              {/* Buscador de refacciones de inventario */}
              <div className="relative">
                <Search className="absolute left-3 top-2.5 text-slate-400 w-4 h-4" />
                <input
                  ref={searchArtInputRef}
                  type="text"
                  value={queryArticulo}
                  onChange={e => setQueryArticulo(e.target.value)}
                  placeholder="Buscar refacción en inventario para esta bici (por nombre, código de barras o clave)..."
                  className="w-full bg-slate-900 border border-slate-700 focus:border-purple-500 rounded-lg pl-9 pr-3.5 py-2 text-xs text-white focus:outline-none"
                />

                {sugerenciasArt.length > 0 && (
                  <div className="absolute top-10 left-0 right-0 bg-slate-900 border border-slate-700 rounded-xl shadow-2xl z-20 max-h-52 overflow-y-auto divide-y divide-slate-800">
                    {sugerenciasArt.map(art => (
                      <div
                        key={art.Clave}
                        onClick={() => agregarArticuloABici(art)}
                        className="p-2.5 hover:bg-slate-800 cursor-pointer flex justify-between items-center text-xs transition"
                      >
                        <div>
                          <span className="font-semibold text-slate-200">{art.Descripcion}</span>
                          <span className="text-[10px] text-slate-500 ml-2 font-mono">Clave: {art.Clave}</span>
                          <span className="text-[10px] text-emerald-400/80 ml-2">Stock: {art.Existencias}</span>
                        </div>
                        <span className="font-bold text-emerald-400 font-mono">${art.Menudeo.toFixed(2)}</span>
                      </div>
                    ))}
                  </div>
                )}
              </div>

              {/* Tabla de Artículos de ESTA bicicleta */}
              <div className="bg-slate-900 rounded-xl overflow-hidden border border-slate-800">
                <table className="w-full text-left text-xs">
                  <thead className="bg-slate-950 text-slate-400 uppercase text-[10px]">
                    <tr>
                      <th className="p-2.5 w-16 text-center">Cant</th>
                      <th className="p-2.5">Descripción / Concepto</th>
                      <th className="p-2.5">Tipo</th>
                      <th className="p-2.5 text-right">Precio Unit.</th>
                      <th className="p-2.5 text-right">Importe</th>
                      <th className="p-2.5 w-10 text-center"></th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-800/60 font-mono">
                    {biciActiva.articulos.map(it => (
                      <tr key={it.id} className="hover:bg-slate-800/40">
                        <td className="p-2.5 text-center">
                          <div className="flex items-center justify-center gap-1">
                            <button
                              type="button"
                              onClick={() => modificarCantidadItem(it.id, -1)}
                              className="px-1 bg-slate-800 hover:bg-slate-700 rounded text-slate-300"
                            >
                              -
                            </button>
                            <span className="font-bold text-white px-1">{it.Cantidad}</span>
                            <button
                              type="button"
                              onClick={() => modificarCantidadItem(it.id, 1)}
                              className="px-1 bg-slate-800 hover:bg-slate-700 rounded text-slate-300"
                            >
                              +
                            </button>
                          </div>
                        </td>
                        <td className="p-2.5 font-sans">
                          <span className="font-semibold text-slate-200">{it.Descripcion}</span>
                          <span className="text-[10px] text-slate-500 ml-2 font-mono">{it.Clave}</span>
                        </td>
                        <td className="p-2.5">
                          {it.Trabajo === 1 ? (
                            <span className="bg-purple-950 text-purple-300 border border-purple-800 px-1.5 py-0.2 rounded text-[10px] font-bold">
                              Mano de Obra
                            </span>
                          ) : (
                            <span className="bg-slate-800 text-slate-300 px-1.5 py-0.2 rounded text-[10px]">
                              Refacción
                            </span>
                          )}
                        </td>
                        <td className="p-2.5 text-right text-slate-300">${it.Precio.toFixed(2)}</td>
                        <td className="p-2.5 text-right font-bold text-emerald-400">
                          ${(it.Cantidad * it.Precio).toFixed(2)}
                        </td>
                        <td className="p-2.5 text-center">
                          <button
                            type="button"
                            onClick={() => eliminarItemDeBici(it.id)}
                            className="text-rose-400 hover:text-rose-300 cursor-pointer"
                          >
                            <Trash2 className="w-3.5 h-3.5" />
                          </button>
                        </td>
                      </tr>
                    ))}

                    {biciActiva.articulos.length === 0 && (
                      <tr>
                        <td colSpan={6} className="text-center py-6 text-slate-500 italic text-[11px] font-sans">
                          No se han agregado refacciones ni mano de obra a esta bicicleta.
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>

        {/* 3. Liquidación Consolidada y Anticipos */}
        <div className="bg-slate-900/80 rounded-2xl p-5 border border-slate-800 space-y-4 shadow-sm">
          <div className="flex items-center gap-2 text-sm font-bold text-slate-200 border-b border-slate-800/80 pb-3">
            <DollarSign className="w-4 h-4 text-amber-400" />
            <span>3. Resumen de la Orden y Anticipo Inicial</span>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {/* Desglose por Bicicleta */}
            <div className="space-y-2">
              <div className="text-xs font-semibold text-slate-400">Desglose por Bicicleta:</div>
              <div className="space-y-1.5 bg-slate-950 p-3 rounded-xl border border-slate-800">
                {bicicletas.map(b => (
                  <div key={b.biciId} className="flex justify-between items-center text-xs">
                    <span className="text-slate-300">
                      Bici #{b.biciId} ({b.Marca || 'Sin marca'} {b.Color})
                    </span>
                    <span className="font-mono font-bold text-slate-200">
                      ${calcularSubtotalBici(b).toFixed(2)}
                    </span>
                  </div>
                ))}

                <div className="pt-2 border-t border-slate-800 flex justify-between items-center text-sm font-bold">
                  <span className="text-white">TOTAL DE LA ORDEN:</span>
                  <span className="text-xl font-mono text-emerald-400">
                    ${totalOrdenCompleta.toFixed(2)}
                  </span>
                </div>
              </div>

              {/* Botones rápidos de anticipo */}
              <div className="flex gap-2 pt-1">
                <button
                  type="button"
                  onClick={() => asignarAnticipoExacto(0)}
                  className="flex-1 py-1 px-2 rounded bg-slate-800 hover:bg-slate-700 text-[11px] text-slate-300 cursor-pointer"
                >
                  Sin anticipo
                </button>
                <button
                  type="button"
                  onClick={() => asignarAnticipoExacto(50)}
                  className="flex-1 py-1 px-2 rounded bg-slate-800 hover:bg-slate-700 text-[11px] text-slate-300 cursor-pointer"
                >
                  Anticipo 50%
                </button>
                <button
                  type="button"
                  onClick={() => asignarAnticipoExacto(100)}
                  className="flex-1 py-1 px-2 rounded bg-slate-800 hover:bg-slate-700 text-[11px] text-slate-300 cursor-pointer"
                >
                  100% Pagado
                </button>
              </div>
            </div>

            {/* Campos de Anticipo Dividido */}
            <div className="space-y-3 bg-slate-950 p-4 rounded-xl border border-slate-800">
              <div className="text-xs font-semibold text-slate-300">Métodos de Anticipo:</div>

              <div className="grid grid-cols-3 gap-2">
                <div>
                  <label className="block text-[10px] text-slate-400 mb-1 flex items-center gap-1">
                    <Banknote className="w-3 h-3 text-emerald-400" /> Efectivo ($)
                  </label>
                  <input
                    type="number"
                    step="any"
                    min="0"
                    value={anticipoEfectivo}
                    onChange={e => setAnticipoEfectivo(e.target.value)}
                    placeholder="0.00"
                    className="w-full bg-slate-900 border border-slate-700 rounded px-2.5 py-1.5 text-xs text-white font-mono focus:outline-none focus:border-emerald-500"
                  />
                </div>

                <div>
                  <label className="block text-[10px] text-slate-400 mb-1 flex items-center gap-1">
                    <CreditCard className="w-3 h-3 text-blue-400" /> Tarjeta ($)
                  </label>
                  <input
                    type="number"
                    step="any"
                    min="0"
                    value={anticipoTarjeta}
                    onChange={e => setAnticipoTarjeta(e.target.value)}
                    placeholder="0.00"
                    className="w-full bg-slate-900 border border-slate-700 rounded px-2.5 py-1.5 text-xs text-white font-mono focus:outline-none focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-[10px] text-slate-400 mb-1 flex items-center gap-1">
                    <ArrowLeftRight className="w-3 h-3 text-purple-400" /> Transfer ($)
                  </label>
                  <input
                    type="number"
                    step="any"
                    min="0"
                    value={anticipoTransfer}
                    onChange={e => setAnticipoTransfer(e.target.value)}
                    placeholder="0.00"
                    className="w-full bg-slate-900 border border-slate-700 rounded px-2.5 py-1.5 text-xs text-white font-mono focus:outline-none focus:border-purple-500"
                  />
                </div>
              </div>

              <div className="pt-2 border-t border-slate-800 space-y-1 text-xs">
                <div className="flex justify-between items-center text-slate-400">
                  <span>Anticipo entregado:</span>
                  <span className="font-mono font-bold text-emerald-400">${totalAnticipo.toFixed(2)}</span>
                </div>
                <div className="flex justify-between items-center text-slate-300 font-bold">
                  <span>Saldo a liquidar al entregar:</span>
                  <span className={`font-mono text-base ${saldoPendiente > 0 ? 'text-amber-400' : 'text-emerald-400'}`}>
                    ${saldoPendiente.toFixed(2)}
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Botón de Enviar */}
        <div className="flex items-center justify-end gap-3 pt-2 pb-6">
          <button
            type="button"
            onClick={onCerrar}
            className="px-5 py-2.5 rounded-xl bg-slate-800 hover:bg-slate-700 text-xs font-semibold text-slate-300 cursor-pointer"
          >
            Cancelar
          </button>
          <button
            type="submit"
            disabled={guardando}
            className="px-6 py-2.5 rounded-xl bg-blue-600 hover:bg-blue-500 disabled:opacity-50 text-sm font-bold text-white shadow-xl shadow-blue-900/40 cursor-pointer flex items-center gap-2"
          >
            <Check className="w-4 h-4" />
            <span>{guardando ? 'Generando Orden...' : 'Crear y Guardar Orden de Taller'}</span>
          </button>
        </div>
      </form>
    </div>
  );
}
