import { useState, useEffect, useRef } from 'react';
import {
  Users,
  Search,
  PlusCircle,
  Edit3,
  Trash2,
  X,
  Check,
  Calendar,
  Phone,
  Mail,
  MapPin,
  FileText,
  Receipt,
  CreditCard
} from 'lucide-react';
import { api, type Cliente } from './api';

interface ClientesViewProps {
  esAdmin: boolean;
}

interface MovimientoCliente {
  Id: number;
  Detalle: string;
  Usuario: string;
  Fecha: string;
  Hora: string;
  Pago: number;
  Total: number;
  Adeudo: number;
}

interface VentaCliente {
  Folio: string;
  Total: number;
  Fecha: string;
  Hora: string;
  Pagoef: number;
  Pagotcr: number;
  Pagotran: number;
  Resta: number;
  Usuario: string;
}

export default function ClientesView({ esAdmin }: ClientesViewProps) {
  const [clientes, setClientes] = useState<Cliente[]>([]);
  const [query, setQuery] = useState('');
  const [filtro, setFiltro] = useState<'TODOS' | 'ADEUDO' | 'MAYOREO'>('TODOS');
  const [clienteSeleccionado, setClienteSeleccionado] = useState<Cliente | null>(null);

  // Detalle del cliente seleccionado
  const [movimientos, setMovimientos] = useState<MovimientoCliente[]>([]);
  const [ventas, setVentas] = useState<VentaCliente[]>([]);
  const [cargandoDetalle, setCargandoDetalle] = useState(false);
  const [tabHistorial, setTabHistorial] = useState<'VENTAS' | 'MOVIMIENTOS'>('VENTAS');

  // Modal Crear / Editar
  const [modalFormulario, setModalFormulario] = useState(false);
  const [modoEdicion, setModoEdicion] = useState(false);
  const [guardando, setGuardando] = useState(false);
  const [formData, setFormData] = useState<Partial<Cliente>>({
    Nombre: '',
    Direccion: '',
    Telcel: '',
    Telcasa: '',
    RFC: '',
    mail: '',
    Mayoreo: 0,
    Especial: 0,
    Credito: 0,
    Adeudo: 0
  });

  const searchInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    cargarClientes();
  }, []);

  const cargarClientes = async () => {
    try {
      const data = await api.buscarClientes('');
      setClientes(data);
      if (data.length > 0 && !clienteSeleccionado) {
        seleccionarCliente(data[0]);
      }
    } catch (err) {
      console.error('Error al cargar clientes:', err);
    }
  };

  // Búsqueda reactiva
  useEffect(() => {
    const timer = setTimeout(async () => {
      try {
        const res = await api.buscarClientes(query);
        setClientes(res);
      } catch (err) {
        console.error('Error buscando clientes:', err);
      }
    }, 120);
    return () => clearTimeout(timer);
  }, [query]);

  const seleccionarCliente = async (clnt: Cliente) => {
    setClienteSeleccionado(clnt);
    setCargandoDetalle(true);
    try {
      const data = await api.obtenerClienteDetalle(clnt.Id);
      if (data.cliente) setClienteSeleccionado(data.cliente);
      setMovimientos(data.movimientos || []);
      setVentas(data.ventas || []);
    } catch (err) {
      console.error('Error al cargar detalle:', err);
    } finally {
      setCargandoDetalle(false);
    }
  };

  const abrirNuevoCliente = () => {
    setFormData({
      Nombre: '',
      Direccion: '',
      Telcel: '',
      Telcasa: '',
      RFC: '',
      mail: '',
      Mayoreo: 0,
      Especial: 0,
      Credito: 0,
      Adeudo: 0
    });
    setModoEdicion(false);
    setModalFormulario(true);
  };

  const abrirEditarCliente = (clnt: Cliente) => {
    setFormData({
      Id: clnt.Id,
      Nombre: clnt.Nombre || '',
      Direccion: clnt.Direccion || '',
      Telcel: clnt.Telcel || '',
      Telcasa: clnt.Telcasa || '',
      RFC: clnt.RFC || '',
      mail: clnt.mail || '',
      Mayoreo: clnt.Mayoreo || 0,
      Especial: clnt.Especial || 0,
      Credito: clnt.Credito || 0,
      Adeudo: clnt.Adeudo || 0
    });
    setModoEdicion(true);
    setModalFormulario(true);
  };

  const guardarCliente = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formData.Nombre?.trim()) {
      alert('El nombre del cliente es obligatorio');
      return;
    }

    setGuardando(true);
    try {
      if (modoEdicion && formData.Id) {
        const res = await api.actualizarCliente(formData.Id, formData);
        if (res.success) {
          alert('Cliente actualizado con éxito');
          setModalFormulario(false);
          await cargarClientes();
          seleccionarCliente(res.cliente);
        }
      } else {
        const res = await api.crearCliente(formData);
        if (res.success) {
          alert('Cliente registrado con éxito');
          setModalFormulario(false);
          await cargarClientes();
          seleccionarCliente(res.cliente);
        }
      }
    } catch (err: any) {
      alert(`Error al guardar: ${err.message}`);
    } finally {
      setGuardando(false);
    }
  };

  const eliminarCliente = async (clnt: Cliente) => {
    if (!esAdmin) return;
    if (clnt.Id === 1) {
      alert('No se puede eliminar el cliente de Mostrador.');
      return;
    }
    if ((clnt.Adeudo || 0) > 0) {
      alert(`No es posible eliminar a ${clnt.Nombre} porque tiene un adeudo pendiente de $${clnt.Adeudo?.toFixed(2)}.`);
      return;
    }

    if (!confirm(`¿Estás seguro de que deseas eliminar permanentemente a "${clnt.Nombre}"?`)) {
      return;
    }

    try {
      const res = await api.eliminarCliente(clnt.Id);
      if (res.success) {
        alert(res.message || 'Cliente eliminado con éxito');
        await cargarClientes();
        setClienteSeleccionado(null);
      } else {
        alert(res.error || 'No se pudo eliminar el cliente');
      }
    } catch (err: any) {
      alert(`Error al eliminar: ${err.message}`);
    }
  };

  // Filtrado local según chips
  const clientesFiltrados = clientes.filter(c => {
    if (filtro === 'ADEUDO') return (c.Adeudo || 0) > 0;
    if (filtro === 'MAYOREO') return c.Mayoreo === 1;
    return true;
  });

  const totalAdeudoGlobal = clientes.reduce((acc, c) => acc + (c.Adeudo || 0), 0);
  const clientesConAdeudoCount = clientes.filter(c => (c.Adeudo || 0) > 0).length;

  return (
    <div className="flex-1 flex h-full overflow-hidden bg-slate-950 text-slate-100 select-none">
      {/* Columna Izquierda: Directorio y Filtros */}
      <div className="w-96 flex flex-col border-r border-slate-800 bg-slate-900/40">
        {/* Cabecera del directorio */}
        <div className="p-4 border-b border-slate-800 space-y-3">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-2">
              <Users className="w-5 h-5 text-blue-400" />
              <h2 className="font-bold text-slate-200 text-base">Directorio de Clientes</h2>
            </div>
            {esAdmin && (
              <button
                onClick={abrirNuevoCliente}
                className="bg-blue-600 hover:bg-blue-500 text-white text-xs font-bold px-3 py-1.5 rounded-lg flex items-center gap-1.5 transition cursor-pointer shadow-md shadow-blue-900/30"
              >
                <PlusCircle className="w-4 h-4" />
                <span>Nuevo</span>
              </button>
            )}
          </div>

          {/* Buscador */}
          <div className="relative">
            <Search className="absolute left-3 top-2.5 text-slate-400 w-4 h-4" />
            <input
              ref={searchInputRef}
              type="text"
              value={query}
              onChange={e => setQuery(e.target.value)}
              placeholder="Buscar por nombre, ID, teléfono o RFC..."
              className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-lg pl-9 pr-3 py-2 text-xs text-white placeholder-slate-500 focus:outline-none"
            />
          </div>

          {/* Chips de Filtro */}
          <div className="flex gap-1.5 text-xs">
            <button
              onClick={() => setFiltro('TODOS')}
              className={`px-2.5 py-1 rounded-md transition cursor-pointer font-medium ${
                filtro === 'TODOS'
                  ? 'bg-blue-600 text-white font-bold'
                  : 'bg-slate-800 text-slate-400 hover:text-white'
              }`}
            >
              Todos ({clientes.length})
            </button>
            <button
              onClick={() => setFiltro('ADEUDO')}
              className={`px-2.5 py-1 rounded-md transition cursor-pointer font-medium flex items-center gap-1 ${
                filtro === 'ADEUDO'
                  ? 'bg-amber-600 text-white font-bold'
                  : 'bg-slate-800 text-amber-400/80 hover:text-amber-300'
              }`}
            >
              Con Adeudo ({clientesConAdeudoCount})
            </button>
            <button
              onClick={() => setFiltro('MAYOREO')}
              className={`px-2.5 py-1 rounded-md transition cursor-pointer font-medium ${
                filtro === 'MAYOREO'
                  ? 'bg-purple-600 text-white font-bold'
                  : 'bg-slate-800 text-purple-400/80 hover:text-purple-300'
              }`}
            >
              Mayoreo
            </button>
          </div>
        </div>

        {/* Resumen rápido de cartera / cobranza */}
        {totalAdeudoGlobal > 0 && (
          <div className="px-4 py-2 bg-amber-950/20 border-b border-amber-900/30 flex justify-between items-center text-xs text-amber-300">
            <span>Cartera por cobrar global:</span>
            <span className="font-mono font-bold text-amber-400">${totalAdeudoGlobal.toFixed(2)}</span>
          </div>
        )}

        {/* Lista de Clientes */}
        <div className="flex-1 overflow-y-auto p-2 space-y-1.5">
          {clientesFiltrados.map(clnt => {
            const isSelected = clienteSeleccionado?.Id === clnt.Id;
            const tieneAdeudo = (clnt.Adeudo || 0) > 0;
            const esMay = clnt.Mayoreo === 1;

            return (
              <div
                key={clnt.Id}
                onClick={() => seleccionarCliente(clnt)}
                className={`p-3 rounded-lg border transition cursor-pointer ${
                  isSelected
                    ? 'bg-blue-950/50 border-blue-600/80 shadow-sm'
                    : 'bg-slate-900/60 border-slate-800/80 hover:border-slate-700 hover:bg-slate-800/40'
                }`}
              >
                <div className="flex justify-between items-start gap-2">
                  <div className="flex-1 min-w-0">
                    <div className="flex items-center gap-2">
                      <span className="font-semibold text-slate-200 text-sm truncate">{clnt.Nombre}</span>
                      <span className="text-[11px] text-slate-500 font-mono">#{clnt.Id}</span>
                    </div>

                    <div className="flex items-center gap-1.5 mt-1">
                      {esMay ? (
                        <span className="bg-purple-950 text-purple-300 border border-purple-800/80 px-1.5 py-0.2 rounded text-[10px] font-bold">
                          MAYOREO
                        </span>
                      ) : (
                        <span className="bg-slate-800 text-slate-400 px-1.5 py-0.2 rounded text-[10px]">
                          Menudeo
                        </span>
                      )}

                      {(clnt.Credito || 0) > 0 && (
                        <span className="bg-slate-800/90 text-slate-300 border border-slate-700 px-1.5 py-0.2 rounded text-[10px] font-mono">
                          Lím: ${(clnt.Credito || 0).toFixed(0)}
                        </span>
                      )}
                    </div>
                  </div>

                  {/* Saldo deudor */}
                  <div className="text-right shrink-0">
                    {tieneAdeudo ? (
                      <div>
                        <div className="text-xs font-bold text-amber-400 font-mono">
                          ${(clnt.Adeudo || 0).toFixed(2)}
                        </div>
                        <div className="text-[10px] text-amber-500 font-medium">Debe</div>
                      </div>
                    ) : (
                      <div className="text-[11px] text-slate-500 font-mono">$0.00</div>
                    )}
                  </div>
                </div>

                {clnt.Telcel && (
                  <div className="text-[11px] text-slate-400 mt-1.5 flex items-center gap-1">
                    <Phone className="w-3 h-3 text-slate-500" />
                    <span>{clnt.Telcel}</span>
                  </div>
                )}
              </div>
            );
          })}

          {clientesFiltrados.length === 0 && (
            <div className="text-center py-16 text-slate-500 text-xs">
              No se encontraron clientes con este filtro
            </div>
          )}
        </div>
      </div>

      {/* Columna Derecha: Detalle Completo del Cliente */}
      <div className="flex-1 flex flex-col overflow-hidden bg-slate-950">
        {clienteSeleccionado ? (
          <div className="flex-1 flex flex-col overflow-hidden">
            {/* Cabecera del Detalle */}
            <div className="p-5 border-b border-slate-800 bg-slate-900/60 flex justify-between items-start">
              <div>
                <div className="flex items-center gap-3">
                  <h1 className="text-2xl font-bold text-white">{clienteSeleccionado.Nombre}</h1>
                  <span className="text-sm bg-slate-800 text-slate-400 px-2.5 py-0.5 rounded-full font-mono">
                    ID: #{clienteSeleccionado.Id}
                  </span>
                  {clienteSeleccionado.Mayoreo === 1 && (
                    <span className="bg-purple-950 text-purple-300 border border-purple-800 px-2 py-0.5 rounded text-xs font-bold">
                      CLIENTE MAYOREO
                    </span>
                  )}
                  {clienteSeleccionado.Especial === 1 && (
                    <span className="bg-amber-950 text-amber-300 border border-amber-800 px-2 py-0.5 rounded text-xs font-bold">
                      PRECIO ESPECIAL (PME)
                    </span>
                  )}
                </div>

                <div className="flex items-center gap-6 text-xs text-slate-400 mt-2">
                  {clienteSeleccionado.Telcel && (
                    <span className="flex items-center gap-1.5">
                      <Phone className="w-3.5 h-3.5 text-blue-400" />
                      <span>{clienteSeleccionado.Telcel}</span>
                    </span>
                  )}
                  {clienteSeleccionado.RFC && (
                    <span className="flex items-center gap-1.5 font-mono">
                      <FileText className="w-3.5 h-3.5 text-emerald-400" />
                      <span>RFC: {clienteSeleccionado.RFC}</span>
                    </span>
                  )}
                  {clienteSeleccionado.Direccion && (
                    <span className="flex items-center gap-1.5">
                      <MapPin className="w-3.5 h-3.5 text-rose-400" />
                      <span>{clienteSeleccionado.Direccion}</span>
                    </span>
                  )}
                  {clienteSeleccionado.mail && (
                    <span className="flex items-center gap-1.5">
                      <Mail className="w-3.5 h-3.5 text-purple-400" />
                      <span>{clienteSeleccionado.mail}</span>
                    </span>
                  )}
                </div>
              </div>

              {/* Botones de Acción (Admin) */}
              {esAdmin && (
                <div className="flex items-center gap-2">
                  <button
                    onClick={() => abrirEditarCliente(clienteSeleccionado)}
                    className="flex items-center gap-1.5 bg-slate-800 hover:bg-slate-700 text-slate-200 px-3.5 py-2 rounded-lg text-xs font-semibold border border-slate-700 transition cursor-pointer"
                  >
                    <Edit3 className="w-3.5 h-3.5 text-blue-400" />
                    <span>Editar</span>
                  </button>

                  {clienteSeleccionado.Id !== 1 && (
                    <button
                      onClick={() => eliminarCliente(clienteSeleccionado)}
                      title="Eliminar cliente"
                      className="flex items-center gap-1.5 bg-rose-950/40 hover:bg-rose-900/60 text-rose-300 border border-rose-800/80 px-3 py-2 rounded-lg text-xs font-semibold transition cursor-pointer"
                    >
                      <Trash2 className="w-3.5 h-3.5" />
                      <span>Eliminar</span>
                    </button>
                  )}
                </div>
              )}
            </div>

            {/* Tarjetas de Saldos y Estado Financiero */}
            <div className="p-5 grid grid-cols-4 gap-4 border-b border-slate-800 bg-slate-900/20">
              <div className="bg-slate-900/80 p-4 rounded-xl border border-slate-800">
                <div className="text-xs text-slate-400 font-medium">Adeudo Actual</div>
                <div className={`text-2xl font-bold font-mono mt-1 ${
                  (clienteSeleccionado.Adeudo || 0) > 0 ? 'text-amber-400' : 'text-emerald-400'
                }`}>
                  ${(clienteSeleccionado.Adeudo || 0).toFixed(2)}
                </div>
                <div className="text-[11px] text-slate-500 mt-1">
                  {(clienteSeleccionado.Adeudo || 0) > 0 ? 'Saldo deudor pendiente' : 'Al corriente (Sin deuda)'}
                </div>
              </div>

              <div className="bg-slate-900/80 p-4 rounded-xl border border-slate-800">
                <div className="text-xs text-slate-400 font-medium">Límite de Crédito</div>
                <div className="text-2xl font-bold font-mono text-blue-400 mt-1">
                  ${(clienteSeleccionado.Credito || 0).toFixed(2)}
                </div>
                <div className="text-[11px] text-slate-500 mt-1">
                  {(clienteSeleccionado.Credito || 0) > 0 ? 'Línea de crédito autorizada' : 'Sin crédito asignado'}
                </div>
              </div>

              <div className="bg-slate-900/80 p-4 rounded-xl border border-slate-800">
                <div className="text-xs text-slate-400 font-medium">Crédito Disponible</div>
                <div className={`text-2xl font-bold font-mono mt-1 ${
                  Math.max(0, (clienteSeleccionado.Credito || 0) - (clienteSeleccionado.Adeudo || 0)) > 0
                    ? 'text-emerald-400'
                    : 'text-slate-400'
                }`}>
                  ${Math.max(0, (clienteSeleccionado.Credito || 0) - (clienteSeleccionado.Adeudo || 0)).toFixed(2)}
                </div>
                <div className="text-[11px] text-slate-500 mt-1">Para compras a fiado</div>
              </div>

              <div className="bg-slate-900/80 p-4 rounded-xl border border-slate-800">
                <div className="text-xs text-slate-400 font-medium">Última Compra</div>
                <div className="text-sm font-semibold text-slate-200 mt-2 flex items-center gap-1.5">
                  <Calendar className="w-4 h-4 text-slate-400" />
                  <span>{clienteSeleccionado.ultcompra || 'Sin registro'}</span>
                </div>
                <div className="text-[11px] text-slate-500 mt-1 font-mono">
                  {ventas.length} ventas registradas
                </div>
              </div>
            </div>

            {/* Pestañas de Historial */}
            <div className="flex border-b border-slate-800 bg-slate-900/40 px-5 text-sm">
              <button
                onClick={() => setTabHistorial('VENTAS')}
                className={`py-3 px-4 font-semibold border-b-2 transition cursor-pointer flex items-center gap-2 ${
                  tabHistorial === 'VENTAS'
                    ? 'border-blue-500 text-white'
                    : 'border-transparent text-slate-400 hover:text-slate-200'
                }`}
              >
                <Receipt className="w-4 h-4" />
                <span>Historial de Compras ({ventas.length})</span>
              </button>

              <button
                onClick={() => setTabHistorial('MOVIMIENTOS')}
                className={`py-3 px-4 font-semibold border-b-2 transition cursor-pointer flex items-center gap-2 ${
                  tabHistorial === 'MOVIMIENTOS'
                    ? 'border-blue-500 text-white'
                    : 'border-transparent text-slate-400 hover:text-slate-200'
                }`}
              >
                <CreditCard className="w-4 h-4" />
                <span>Movimientos y Abonos de Adeudo ({movimientos.length})</span>
              </button>
            </div>

            {/* Contenido de la Pestaña Activa */}
            <div className="flex-1 overflow-y-auto p-5">
              {cargandoDetalle ? (
                <div className="text-center py-20 text-slate-500 text-sm">Cargando historial...</div>
              ) : tabHistorial === 'VENTAS' ? (
                <div className="space-y-2">
                  <table className="w-full text-left text-xs">
                    <thead className="bg-slate-900 text-slate-400 uppercase text-[10px] tracking-wider border-b border-slate-800">
                      <tr>
                        <th className="p-3">Folio</th>
                        <th className="p-3">Fecha y Hora</th>
                        <th className="p-3">Total Venta</th>
                        <th className="p-3">Efectivo</th>
                        <th className="p-3">Tarjeta</th>
                        <th className="p-3">Transferencia</th>
                        <th className="p-3">Resta (Adeudo)</th>
                        <th className="p-3">Cajero</th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-slate-800/60 font-mono">
                      {ventas.map(v => (
                        <tr key={v.Folio} className="hover:bg-slate-900/40 transition">
                          <td className="p-3 font-bold text-blue-400">{v.Folio}</td>
                          <td className="p-3 text-slate-300">
                            {v.Fecha} {v.Hora}
                          </td>
                          <td className="p-3 font-bold text-white">${v.Total.toFixed(2)}</td>
                          <td className="p-3 text-slate-400">${(v.Pagoef || 0).toFixed(2)}</td>
                          <td className="p-3 text-slate-400">${(v.Pagotcr || 0).toFixed(2)}</td>
                          <td className="p-3 text-slate-400">${(v.Pagotran || 0).toFixed(2)}</td>
                          <td className="p-3">
                            {(v.Resta || 0) > 0 ? (
                              <span className="text-amber-400 font-bold bg-amber-950/60 px-2 py-0.5 rounded border border-amber-900/50">
                                ${(v.Resta || 0).toFixed(2)}
                              </span>
                            ) : (
                              <span className="text-emerald-400 font-semibold">$0.00</span>
                            )}
                          </td>
                          <td className="p-3 text-slate-400 font-sans">{v.Usuario}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>

                  {ventas.length === 0 && (
                    <div className="text-center py-20 text-slate-500 text-xs">
                      No hay compras registradas para este cliente.
                    </div>
                  )}
                </div>
              ) : (
                /* Pestaña Movimientos de Adeudo / Abonos */
                <div className="space-y-2">
                  <table className="w-full text-left text-xs">
                    <thead className="bg-slate-900 text-slate-400 uppercase text-[10px] tracking-wider border-b border-slate-800">
                      <tr>
                        <th className="p-3">Concepto / Detalle</th>
                        <th className="p-3">Fecha y Hora</th>
                        <th className="p-3">Pago / Abono</th>
                        <th className="p-3">Total Operación</th>
                        <th className="p-3">Saldo Adeudo Resultante</th>
                        <th className="p-3">Usuario</th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-slate-800/60 font-mono">
                      {movimientos.map((m, idx) => (
                        <tr key={idx} className="hover:bg-slate-900/40 transition">
                          <td className="p-3 font-sans font-semibold text-slate-200">{m.Detalle}</td>
                          <td className="p-3 text-slate-400">
                            {m.Fecha} {m.Hora}
                          </td>
                          <td className="p-3 font-bold text-emerald-400">
                            {(m.Pago || 0) > 0 ? `$${m.Pago.toFixed(2)}` : '-'}
                          </td>
                          <td className="p-3 text-slate-300">
                            {(m.Total || 0) > 0 ? `$${m.Total.toFixed(2)}` : '-'}
                          </td>
                          <td className="p-3 font-bold text-amber-400">
                            ${(m.Adeudo || 0).toFixed(2)}
                          </td>
                          <td className="p-3 text-slate-400 font-sans">{m.Usuario}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>

                  {movimientos.length === 0 && (
                    <div className="text-center py-20 text-slate-500 text-xs">
                      No hay movimientos de adeudo registrados para este cliente.
                    </div>
                  )}
                </div>
              )}
            </div>
          </div>
        ) : (
          <div className="flex-1 flex flex-col items-center justify-center text-slate-500 gap-3">
            <Users className="w-12 h-12 opacity-30" />
            <span className="text-sm">Selecciona un cliente del directorio para ver su historial y adeudo</span>
          </div>
        )}
      </div>

      {/* Modal Crear / Editar Cliente (Solo Administrador) */}
      {modalFormulario && (
        <div className="fixed inset-0 bg-black/70 backdrop-blur-sm flex items-center justify-center p-4 z-50">
          <div className="bg-slate-900 border border-slate-800 rounded-2xl max-w-lg w-full p-6 shadow-2xl">
            <div className="flex justify-between items-center mb-4 pb-3 border-b border-slate-800">
              <h3 className="text-lg font-bold flex items-center gap-2 text-white">
                <Users className="w-5 h-5 text-blue-400" />
                {modoEdicion ? `Editar Cliente #${formData.Id}` : 'Registrar Nuevo Cliente'}
              </h3>
              <button
                onClick={() => setModalFormulario(false)}
                className="text-slate-400 hover:text-white p-1 rounded-lg hover:bg-slate-800 transition cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            <form onSubmit={guardarCliente} className="space-y-4">
              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-1">Nombre Completo *</label>
                <input
                  type="text"
                  required
                  autoFocus
                  value={formData.Nombre || ''}
                  onChange={e => setFormData({ ...formData, Nombre: e.target.value })}
                  placeholder="Ej: Distribuidora Ciclista del Centro"
                  className="w-full bg-slate-950 border border-slate-700 rounded-lg px-3.5 py-2 text-sm text-white focus:outline-none focus:border-blue-500"
                />
              </div>

              <div className="grid grid-cols-2 gap-3">
                <div>
                  <label className="block text-xs font-semibold text-slate-300 mb-1">Teléfono Celular</label>
                  <input
                    type="text"
                    value={formData.Telcel || ''}
                    onChange={e => setFormData({ ...formData, Telcel: e.target.value })}
                    placeholder="771..."
                    className="w-full bg-slate-950 border border-slate-700 rounded-lg px-3 py-2 text-xs text-white focus:outline-none focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-xs font-semibold text-slate-300 mb-1">Teléfono Fijo</label>
                  <input
                    type="text"
                    value={formData.Telcasa || ''}
                    onChange={e => setFormData({ ...formData, Telcasa: e.target.value })}
                    placeholder="Tel. casa u oficina"
                    className="w-full bg-slate-950 border border-slate-700 rounded-lg px-3 py-2 text-xs text-white focus:outline-none focus:border-blue-500"
                  />
                </div>
              </div>

              <div className="grid grid-cols-2 gap-3">
                <div>
                  <label className="block text-xs font-semibold text-slate-300 mb-1">RFC</label>
                  <input
                    type="text"
                    value={formData.RFC || ''}
                    onChange={e => setFormData({ ...formData, RFC: e.target.value })}
                    placeholder="XAXX010101000"
                    className="w-full bg-slate-950 border border-slate-700 rounded-lg px-3 py-2 text-xs text-white uppercase font-mono focus:outline-none focus:border-blue-500"
                  />
                </div>

                <div>
                  <label className="block text-xs font-semibold text-slate-300 mb-1">Correo Electrónico</label>
                  <input
                    type="email"
                    value={formData.mail || ''}
                    onChange={e => setFormData({ ...formData, mail: e.target.value })}
                    placeholder="cliente@ejemplo.com"
                    className="w-full bg-slate-950 border border-slate-700 rounded-lg px-3 py-2 text-xs text-white focus:outline-none focus:border-blue-500"
                  />
                </div>
              </div>

              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-1">Dirección</label>
                <input
                  type="text"
                  value={formData.Direccion || ''}
                  onChange={e => setFormData({ ...formData, Direccion: e.target.value })}
                  placeholder="Calle, número, colonia, ciudad..."
                  className="w-full bg-slate-950 border border-slate-700 rounded-lg px-3 py-2 text-xs text-white focus:outline-none focus:border-blue-500"
                />
              </div>

              {/* Parámetros de Precio y Crédito */}
              <div className="p-3 bg-slate-950/70 rounded-xl border border-slate-800 space-y-3">
                <div className="text-xs font-bold text-slate-300 border-b border-slate-800/80 pb-1.5">
                  Condiciones Comerciales y Crédito
                </div>

                <div className="grid grid-cols-2 gap-3">
                  <label className="flex items-center gap-2 cursor-pointer text-xs text-slate-300">
                    <input
                      type="checkbox"
                      checked={formData.Mayoreo === 1}
                      onChange={e => setFormData({ ...formData, Mayoreo: e.target.checked ? 1 : 0 })}
                      className="rounded bg-slate-800 border-slate-700 text-blue-600 focus:ring-0 w-4 h-4 cursor-pointer"
                    />
                    <span>Cliente de Mayoreo</span>
                  </label>

                  <label className="flex items-center gap-2 cursor-pointer text-xs text-slate-300">
                    <input
                      type="checkbox"
                      checked={formData.Especial === 1}
                      onChange={e => setFormData({ ...formData, Especial: e.target.checked ? 1 : 0 })}
                      className="rounded bg-slate-800 border-slate-700 text-amber-600 focus:ring-0 w-4 h-4 cursor-pointer"
                    />
                    <span>Derecho a Precio Especial (PME)</span>
                  </label>
                </div>

                <div className="grid grid-cols-2 gap-3 pt-1">
                  <div>
                    <label className="block text-xs font-semibold text-slate-400 mb-1">Límite de Crédito ($)</label>
                    <input
                      type="number"
                      step="any"
                      min="0"
                      value={formData.Credito ?? 0}
                      onChange={e => setFormData({ ...formData, Credito: parseFloat(e.target.value) || 0 })}
                      className="w-full bg-slate-900 border border-slate-700 rounded px-3 py-1.5 text-xs text-white font-mono focus:outline-none focus:border-blue-500"
                    />
                  </div>

                  <div>
                    <label className="block text-xs font-semibold text-slate-400 mb-1">Adeudo Inicial / Actual ($)</label>
                    <input
                      type="number"
                      step="any"
                      min="0"
                      value={formData.Adeudo ?? 0}
                      onChange={e => setFormData({ ...formData, Adeudo: parseFloat(e.target.value) || 0 })}
                      className="w-full bg-slate-900 border border-slate-700 rounded px-3 py-1.5 text-xs text-white font-mono focus:outline-none focus:border-blue-500"
                    />
                  </div>
                </div>
              </div>

              <div className="flex gap-3 pt-2">
                <button
                  type="button"
                  onClick={() => setModalFormulario(false)}
                  className="flex-1 bg-slate-800 hover:bg-slate-700 py-2.5 rounded-lg text-xs font-semibold text-slate-300 cursor-pointer"
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  disabled={guardando}
                  className="flex-1 bg-blue-600 hover:bg-blue-500 disabled:opacity-50 py-2.5 rounded-lg text-xs font-bold text-white shadow-lg shadow-blue-900/40 cursor-pointer flex items-center justify-center gap-1.5"
                >
                  <Check className="w-4 h-4" />
                  <span>{guardando ? 'Guardando...' : modoEdicion ? 'Actualizar Cliente' : 'Crear Cliente'}</span>
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
