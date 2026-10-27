import { useState, useEffect } from 'react';
import {
  Settings,
  Smartphone,
  Printer,
  Info,
  AlertCircle,
  RefreshCw,
  LogOut,
  X,
  QrCode,
  ShieldCheck,
  Store,
  MessageSquare,
  FileText,
  Receipt,
  Save,
  Check
} from 'lucide-react';
import { api, type WhatsAppStatus } from './api';

export type SeccionAjustes = 'whatsapp' | 'impresion' | 'sistema';

interface AjustesModalProps {
  seccionInicial?: SeccionAjustes;
  onClose: () => void;
}

export default function AjustesModal({ seccionInicial = 'whatsapp', onClose }: AjustesModalProps) {
  const [seccionActiva, setSeccionActiva] = useState<SeccionAjustes>(seccionInicial);

  // Estados de WhatsApp
  const [estadoWs, setEstadoWs] = useState<WhatsAppStatus | null>(null);
  const [cargandoWs, setCargandoWs] = useState<boolean>(true);
  const [desconectandoWs, setDesconectandoWs] = useState<boolean>(false);
  const [reiniciandoWs, setReiniciandoWs] = useState<boolean>(false);
  const [errorWs, setErrorWs] = useState<string | null>(null);

  // Estados de Configuración de Mensaje de WhatsApp
  const [plantillaWs, setPlantillaWs] = useState<string>(() => {
    return localStorage.getItem('sicdin_whatsapp_template') ||
      '🚲 *Casa Licona - Bicicletas e Innovaciones*\n\nEstimado(a) *{cliente}*:\nAdjuntamos su comprobante de venta digital con Folio: *{folio}*.\nTotal de la nota: *${total} MXN*.\n\n¡Agradecemos su preferencia y confianza!';
  });
  const [plantillaGuardada, setPlantillaGuardada] = useState<boolean>(false);

  // Estados de Configuración de Impresión y Tienda
  const [formatoDefault, setFormatoDefault] = useState<'MEDIA_HOJA' | 'TICKET'>(() => {
    return (localStorage.getItem('sicdin_formato_impresion_default') as 'MEDIA_HOJA' | 'TICKET') || 'MEDIA_HOJA';
  });

  const [nombreTienda, setNombreTienda] = useState<string>(() => {
    return localStorage.getItem('sicdin_nombre_tienda') || 'CASA LICONA';
  });
  const [giroTienda, setGiroTienda] = useState<string>(() => {
    return localStorage.getItem('sicdin_giro_tienda') || 'Bicicletas, Refacciones y Accesorios';
  });
  const [direccionTienda, setDireccionTienda] = useState<string>(() => {
    return localStorage.getItem('sicdin_direccion_tienda') || 'Cto. Vicente Aguirre del Castillo, Local 71, Col. Juan C. Doria, Pachuca de Soto, Hgo.';
  });
  const [telefonosTienda, setTelefonosTienda] = useState<string>(() => {
    return localStorage.getItem('sicdin_telefonos_tienda') || 'Tel. (771) 710-7860 • Cel. 771-121-60-98';
  });
  const [ajustesTiendaGuardados, setAjustesTiendaGuardados] = useState<boolean>(false);

  // Consulta periódica del estado de WhatsApp mientras el modal esté abierto
  const consultarEstadoWs = async () => {
    try {
      const data = await api.obtenerEstadoWhatsApp();
      setEstadoWs(data);
      setErrorWs(null);
    } catch (err: any) {
      setErrorWs(err.message || 'No se pudo contactar al servicio de WhatsApp.');
    } finally {
      setCargandoWs(false);
    }
  };

  useEffect(() => {
    consultarEstadoWs();
    const intervalo = setInterval(() => {
      consultarEstadoWs();
    }, 3000);
    return () => clearInterval(intervalo);
  }, []);

  const handleDesconectarWs = async () => {
    if (!window.confirm('¿Deseas desvincular el celular del negocio de este equipo? Se solicitará escanear el código QR nuevamente para enviar notas.')) {
      return;
    }
    setDesconectandoWs(true);
    try {
      await api.desconectarWhatsApp();
      await consultarEstadoWs();
    } catch (err: any) {
      alert(err.message || 'Error al desconectar');
    } finally {
      setDesconectandoWs(false);
    }
  };

  const handleReiniciarWs = async () => {
    setReiniciandoWs(true);
    try {
      await api.reiniciarWhatsApp();
      await consultarEstadoWs();
    } catch (err: any) {
      alert(err.message || 'Error al reiniciar servicio');
    } finally {
      setReiniciandoWs(false);
    }
  };

  const handleGuardarPlantillaWs = () => {
    localStorage.setItem('sicdin_whatsapp_template', plantillaWs);
    setPlantillaGuardada(true);
    setTimeout(() => setPlantillaGuardada(false), 2500);
  };

  const handleGuardarAjustesImpresion = () => {
    localStorage.setItem('sicdin_formato_impresion_default', formatoDefault);
    localStorage.setItem('sicdin_nombre_tienda', nombreTienda);
    localStorage.setItem('sicdin_giro_tienda', giroTienda);
    localStorage.setItem('sicdin_direccion_tienda', direccionTienda);
    localStorage.setItem('sicdin_telefonos_tienda', telefonosTienda);
    setAjustesTiendaGuardados(true);
    setTimeout(() => setAjustesTiendaGuardados(false), 2500);
  };

  return (
    <div className="fixed inset-0 bg-black/85 backdrop-blur-sm z-50 flex items-center justify-center p-3 sm:p-5 overflow-y-auto">
      <div className="bg-slate-900 border border-slate-700/80 rounded-2xl max-w-3xl w-full shadow-2xl flex flex-col overflow-hidden my-auto max-h-[92vh] animate-in fade-in zoom-in-95 duration-150">
        {/* Cabecera Principal */}
        <div className="p-4 bg-slate-900 border-b border-slate-800 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="p-2.5 bg-blue-500/10 text-blue-400 rounded-xl border border-blue-500/20">
              <Settings className="w-5 h-5 text-blue-400" />
            </div>
            <div>
              <div className="flex items-center gap-2">
                <h3 className="text-base font-bold text-white tracking-wide">Ajustes del Sistema</h3>
                <span className="bg-slate-800 text-slate-300 border border-slate-700 px-2 py-0.5 rounded text-[10px] font-bold">
                  SICDIN 4.0
                </span>
              </div>
              <p className="text-xs text-slate-400">Configuración de WhatsApp, tickets, tienda y sistema</p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="p-1.5 text-slate-400 hover:text-white rounded-lg hover:bg-slate-800 transition cursor-pointer"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Barra de Pestañas / Secciones */}
        <div className="flex border-b border-slate-800 bg-slate-950/60 px-4 pt-2 gap-2 overflow-x-auto text-xs">
          {/* Pestaña WhatsApp */}
          <button
            onClick={() => setSeccionActiva('whatsapp')}
            className={`px-4 py-2.5 rounded-t-xl font-bold transition flex items-center gap-2 cursor-pointer border-t border-x ${
              seccionActiva === 'whatsapp'
                ? 'bg-slate-900 text-emerald-400 border-slate-700 border-b-slate-900 -mb-px'
                : 'border-transparent text-slate-400 hover:text-slate-200'
            }`}
          >
            <Smartphone className="w-4 h-4 text-emerald-400" />
            <span>WhatsApp del Negocio</span>
            <span
              className={`w-2 h-2 rounded-full ${
                estadoWs?.status === 'CONNECTED'
                  ? 'bg-emerald-400 shadow-[0_0_6px_#10b981]'
                  : estadoWs?.status === 'QR_READY'
                  ? 'bg-amber-400 animate-pulse'
                  : 'bg-slate-600'
              }`}
            />
          </button>

          {/* Pestaña Impresión y Tienda */}
          <button
            onClick={() => setSeccionActiva('impresion')}
            className={`px-4 py-2.5 rounded-t-xl font-bold transition flex items-center gap-2 cursor-pointer border-t border-x ${
              seccionActiva === 'impresion'
                ? 'bg-slate-900 text-blue-400 border-slate-700 border-b-slate-900 -mb-px'
                : 'border-transparent text-slate-400 hover:text-slate-200'
            }`}
          >
            <Printer className="w-4 h-4 text-blue-400" />
            <span>Impresión y Membrete</span>
          </button>

          {/* Pestaña Sistema */}
          <button
            onClick={() => setSeccionActiva('sistema')}
            className={`px-4 py-2.5 rounded-t-xl font-bold transition flex items-center gap-2 cursor-pointer border-t border-x ${
              seccionActiva === 'sistema'
                ? 'bg-slate-900 text-purple-400 border-slate-700 border-b-slate-900 -mb-px'
                : 'border-transparent text-slate-400 hover:text-slate-200'
            }`}
          >
            <Info className="w-4 h-4 text-purple-400" />
            <span>Información del Sistema</span>
          </button>
        </div>

        {/* Contenido de la Sección Activa */}
        <div className="flex-1 overflow-y-auto p-5 sm:p-6 space-y-5">
          {/* ========================================================================= */}
          {/* SECCIÓN 1: WHATSAPP DEL NEGOCIO                                           */}
          {/* ========================================================================= */}
          {seccionActiva === 'whatsapp' && (
            <div className="space-y-5">
              {/* Tarjeta de Estado de Conexión */}
              <div className="bg-slate-950/80 border border-slate-800 rounded-2xl p-5 shadow-sm">
                <div className="flex items-center justify-between pb-3 mb-4 border-b border-slate-800">
                  <div className="flex items-center gap-2.5">
                    <Smartphone className="w-5 h-5 text-emerald-400" />
                    <div>
                      <h4 className="text-sm font-bold text-white">Dispositivo WhatsApp Vinculado</h4>
                      <p className="text-[11px] text-slate-400">Conexión directa vía Baileys sin costos por mensaje</p>
                    </div>
                  </div>

                  {estadoWs?.status === 'CONNECTED' ? (
                    <span className="bg-emerald-950 text-emerald-300 border border-emerald-800/80 px-2.5 py-1 rounded-full text-[10px] font-bold flex items-center gap-1.5 shadow-sm">
                      <span className="w-2 h-2 rounded-full bg-emerald-400 animate-pulse" />
                      CONECTADO
                    </span>
                  ) : estadoWs?.status === 'QR_READY' ? (
                    <span className="bg-amber-950 text-amber-300 border border-amber-800/80 px-2.5 py-1 rounded-full text-[10px] font-bold flex items-center gap-1.5">
                      <span className="w-2 h-2 rounded-full bg-amber-400 animate-pulse" />
                      ESPERANDO ESCANEO QR
                    </span>
                  ) : (
                    <span className="bg-slate-800 text-slate-300 border border-slate-700 px-2.5 py-1 rounded-full text-[10px] font-bold">
                      DESCONECTADO
                    </span>
                  )}
                </div>

                {/* Vista según estado */}
                {cargandoWs && !estadoWs ? (
                  <div className="py-8 flex flex-col items-center justify-center gap-2 text-slate-400 text-xs">
                    <RefreshCw className="w-6 h-6 animate-spin text-emerald-400" />
                    <span>Verificando conexión de WhatsApp...</span>
                  </div>
                ) : errorWs ? (
                  <div className="p-3 bg-rose-500/10 border border-rose-500/20 rounded-xl text-rose-300 text-xs flex items-center gap-2">
                    <AlertCircle className="w-4 h-4 shrink-0" />
                    <span>{errorWs}</span>
                  </div>
                ) : estadoWs?.status === 'CONNECTED' ? (
                  /* Celular Conectado */
                  <div className="space-y-4">
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                      <div className="p-3.5 bg-slate-900 border border-slate-800 rounded-xl">
                        <div className="text-[10px] text-slate-500 uppercase font-bold tracking-wider">Número Vinculado</div>
                        <div className="text-base font-bold text-emerald-400 font-mono mt-0.5">
                          +{estadoWs.phoneNumber || '52 (Registrado)'}
                        </div>
                      </div>

                      <div className="p-3.5 bg-slate-900 border border-slate-800 rounded-xl">
                        <div className="text-[10px] text-slate-500 uppercase font-bold tracking-wider">Nombre del Perfil</div>
                        <div className="text-base font-bold text-white mt-0.5 truncate">
                          {estadoWs.name || 'Casa Licona'}
                        </div>
                      </div>
                    </div>

                    <div className="flex items-center justify-between pt-1">
                      <div className="flex items-center gap-2 text-xs text-slate-400">
                        <ShieldCheck className="w-4 h-4 text-emerald-400 shrink-0" />
                        <span>Sesión persistente activa en este equipo.</span>
                      </div>

                      <button
                        onClick={handleDesconectarWs}
                        disabled={desconectandoWs}
                        className="px-3.5 py-1.5 bg-rose-500/10 hover:bg-rose-500/20 text-rose-300 border border-rose-500/30 rounded-xl text-xs font-semibold flex items-center gap-1.5 transition cursor-pointer disabled:opacity-50"
                      >
                        <LogOut className="w-3.5 h-3.5" />
                        <span>{desconectandoWs ? 'Desvinculando...' : 'Desvincular Celular'}</span>
                      </button>
                    </div>
                  </div>
                ) : estadoWs?.status === 'QR_READY' && estadoWs.qrCode ? (
                  /* QR Listo para escanear */
                  <div className="flex flex-col md:flex-row items-center gap-6 py-2">
                    <div className="bg-white p-3 rounded-2xl shadow-xl border-4 border-emerald-500/40 shrink-0">
                      <img
                        src={estadoWs.qrCode}
                        alt="Código QR de WhatsApp"
                        className="w-48 h-48 object-contain"
                      />
                    </div>

                    <div className="space-y-3 text-xs text-slate-300 flex-1">
                      <div className="font-semibold text-white flex items-center gap-1.5">
                        <QrCode className="w-4 h-4 text-emerald-400" />
                        <span>Cómo vincular el teléfono del negocio:</span>
                      </div>
                      <ol className="list-decimal list-inside space-y-1.5 text-slate-400 pl-1 text-[11px]">
                        <li>Abre WhatsApp en el celular del negocio.</li>
                        <li>Toca <strong className="text-slate-200">Ajustes / Menú (⋮)</strong> &gt; <strong className="text-slate-200">Dispositivos vinculados</strong>.</li>
                        <li>Toca <strong className="text-emerald-400">Vincular un dispositivo</strong> y apunta tu cámara aquí.</li>
                      </ol>

                      <div className="pt-2">
                        <button
                          onClick={handleReiniciarWs}
                          disabled={reiniciandoWs}
                          className="px-3 py-1.5 bg-slate-800 hover:bg-slate-700 text-slate-300 hover:text-white rounded-lg text-xs font-semibold flex items-center gap-1.5 transition cursor-pointer"
                        >
                          <RefreshCw className={`w-3.5 h-3.5 ${reiniciandoWs ? 'animate-spin text-emerald-400' : ''}`} />
                          <span>Regenerar código QR</span>
                        </button>
                      </div>
                    </div>
                  </div>
                ) : (
                  <div className="py-6 text-center text-xs text-slate-400 flex flex-col items-center gap-2">
                    <RefreshCw className="w-5 h-5 animate-spin text-emerald-400" />
                    <span>Iniciando servicio de WhatsApp seguro...</span>
                  </div>
                )}
              </div>

              {/* Plantilla de Mensaje de Acompañamiento de la Nota */}
              <div className="bg-slate-950/80 border border-slate-800 rounded-2xl p-5 shadow-sm space-y-3">
                <div className="flex items-center gap-2">
                  <MessageSquare className="w-4 h-4 text-emerald-400" />
                  <h4 className="text-sm font-bold text-white">Plantilla del Mensaje de WhatsApp</h4>
                </div>
                <p className="text-[11px] text-slate-400">
                  Texto que acompaña la imagen del comprobante. Usa las variables <code className="text-emerald-300 bg-slate-900 px-1 py-0.5 rounded">{'{cliente}'}</code>, <code className="text-emerald-300 bg-slate-900 px-1 py-0.5 rounded">{'{folio}'}</code> y <code className="text-emerald-300 bg-slate-900 px-1 py-0.5 rounded">{'{total}'}</code>.
                </p>

                <textarea
                  value={plantillaWs}
                  onChange={e => setPlantillaWs(e.target.value)}
                  rows={4}
                  className="w-full bg-slate-900 border border-slate-700 focus:border-emerald-500 rounded-xl p-3 text-xs text-white font-mono focus:outline-none"
                />

                <div className="flex items-center justify-between pt-1">
                  <button
                    onClick={() => {
                      setPlantillaWs('🚲 *Casa Licona - Bicicletas e Innovaciones*\n\nEstimado(a) *{cliente}*:\nAdjuntamos su comprobante de venta digital con Folio: *{folio}*.\nTotal de la nota: *${total} MXN*.\n\n¡Agradecemos su preferencia y confianza!');
                    }}
                    className="text-[11px] text-slate-500 hover:text-slate-300 underline cursor-pointer"
                  >
                    Restaurar predeterminado
                  </button>

                  <button
                    onClick={handleGuardarPlantillaWs}
                    className="px-4 py-1.5 bg-emerald-600 hover:bg-emerald-500 text-white rounded-xl text-xs font-bold transition cursor-pointer flex items-center gap-1.5 shadow"
                  >
                    {plantillaGuardada ? (
                      <>
                        <Check className="w-3.5 h-3.5 text-white" />
                        <span>¡Guardado!</span>
                      </>
                    ) : (
                      <>
                        <Save className="w-3.5 h-3.5" />
                        <span>Guardar Plantilla</span>
                      </>
                    )}
                  </button>
                </div>
              </div>
            </div>
          )}

          {/* ========================================================================= */}
          {/* SECCIÓN 2: IMPRESIÓN Y DATOS DEL NEGOCIO                                  */}
          {/* ========================================================================= */}
          {seccionActiva === 'impresion' && (
            <div className="space-y-5">
              {/* Formato por defecto */}
              <div className="bg-slate-950/80 border border-slate-800 rounded-2xl p-5 shadow-sm space-y-3">
                <div className="flex items-center gap-2">
                  <Printer className="w-4 h-4 text-blue-400" />
                  <h4 className="text-sm font-bold text-white">Formato de Impresión Predeterminado</h4>
                </div>
                <p className="text-[11px] text-slate-400">
                  Selecciona qué formato debe abrirse automáticamente al dar clic en cobrar o imprimir una nota:
                </p>

                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 pt-1">
                  <div
                    onClick={() => setFormatoDefault('MEDIA_HOJA')}
                    className={`p-4 rounded-xl border cursor-pointer transition flex items-start gap-3 ${
                      formatoDefault === 'MEDIA_HOJA'
                        ? 'bg-blue-600/10 border-blue-500 text-white'
                        : 'bg-slate-900 border-slate-800 text-slate-400 hover:text-white'
                    }`}
                  >
                    <FileText className={`w-5 h-5 shrink-0 ${formatoDefault === 'MEDIA_HOJA' ? 'text-blue-400' : 'text-slate-500'}`} />
                    <div>
                      <div className="text-xs font-bold">Media Hoja (Media Carta)</div>
                      <div className="text-[10px] text-slate-400 mt-0.5">
                        Formato clásico tipo remisión horizontal con membrete, tabla y firma.
                      </div>
                    </div>
                  </div>

                  <div
                    onClick={() => setFormatoDefault('TICKET')}
                    className={`p-4 rounded-xl border cursor-pointer transition flex items-start gap-3 ${
                      formatoDefault === 'TICKET'
                        ? 'bg-blue-600/10 border-blue-500 text-white'
                        : 'bg-slate-900 border-slate-800 text-slate-400 hover:text-white'
                    }`}
                  >
                    <Receipt className={`w-5 h-5 shrink-0 ${formatoDefault === 'TICKET' ? 'text-blue-400' : 'text-slate-500'}`} />
                    <div>
                      <div className="text-xs font-bold">Ticket Térmico (80mm)</div>
                      <div className="text-[10px] text-slate-400 mt-0.5">
                        Formato para impresora de rollos térmicos continua de punto de venta.
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              {/* Membrete de la Tienda */}
              <div className="bg-slate-950/80 border border-slate-800 rounded-2xl p-5 shadow-sm space-y-3">
                <div className="flex items-center gap-2">
                  <Store className="w-4 h-4 text-blue-400" />
                  <h4 className="text-sm font-bold text-white">Membrete Comercial en Notas</h4>
                </div>
                <p className="text-[11px] text-slate-400">
                  Estos datos aparecen impresos en la cabecera de las notas y tickets físicos y digitales:
                </p>

                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 text-xs">
                  <div>
                    <label className="block text-[11px] text-slate-400 mb-1">Nombre Comercial</label>
                    <input
                      type="text"
                      value={nombreTienda}
                      onChange={e => setNombreTienda(e.target.value)}
                      className="w-full bg-slate-900 border border-slate-700 rounded-xl px-3 py-1.5 text-white focus:outline-none focus:border-blue-500"
                    />
                  </div>

                  <div>
                    <label className="block text-[11px] text-slate-400 mb-1">Giro / Eslogan</label>
                    <input
                      type="text"
                      value={giroTienda}
                      onChange={e => setGiroTienda(e.target.value)}
                      className="w-full bg-slate-900 border border-slate-700 rounded-xl px-3 py-1.5 text-white focus:outline-none focus:border-blue-500"
                    />
                  </div>

                  <div className="sm:col-span-2">
                    <label className="block text-[11px] text-slate-400 mb-1">Dirección de la Tienda</label>
                    <input
                      type="text"
                      value={direccionTienda}
                      onChange={e => setDireccionTienda(e.target.value)}
                      className="w-full bg-slate-900 border border-slate-700 rounded-xl px-3 py-1.5 text-white focus:outline-none focus:border-blue-500"
                    />
                  </div>

                  <div className="sm:col-span-2">
                    <label className="block text-[11px] text-slate-400 mb-1">Teléfonos de Contacto</label>
                    <input
                      type="text"
                      value={telefonosTienda}
                      onChange={e => setTelefonosTienda(e.target.value)}
                      className="w-full bg-slate-900 border border-slate-700 rounded-xl px-3 py-1.5 text-white focus:outline-none focus:border-blue-500"
                    />
                  </div>
                </div>

                <div className="flex justify-end pt-2">
                  <button
                    onClick={handleGuardarAjustesImpresion}
                    className="px-4 py-1.5 bg-blue-600 hover:bg-blue-500 text-white rounded-xl text-xs font-bold transition cursor-pointer flex items-center gap-1.5 shadow"
                  >
                    {ajustesTiendaGuardados ? (
                      <>
                        <Check className="w-3.5 h-3.5 text-white" />
                        <span>¡Guardado!</span>
                      </>
                    ) : (
                      <>
                        <Save className="w-3.5 h-3.5" />
                        <span>Guardar Ajustes de Impresión</span>
                      </>
                    )}
                  </button>
                </div>
              </div>
            </div>
          )}

          {/* ========================================================================= */}
          {/* SECCIÓN 3: INFORMACIÓN DEL SISTEMA                                        */}
          {/* ========================================================================= */}
          {seccionActiva === 'sistema' && (
            <div className="space-y-4">
              <div className="bg-slate-950/80 border border-slate-800 rounded-2xl p-5 shadow-sm space-y-4">
                <div className="flex items-center gap-3">
                  <img src="/logo.png" alt="Logo" className="w-12 h-12 object-contain rounded" />
                  <div>
                    <h4 className="text-base font-bold text-white">SICDIN Modern 4.0</h4>
                    <p className="text-xs text-blue-400 font-mono">Sistema Integral de Control Digital e Inventarios</p>
                  </div>
                </div>

                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 text-xs font-mono pt-2">
                  <div className="p-3 bg-slate-900 border border-slate-800 rounded-xl">
                    <div className="text-[10px] text-slate-500 uppercase font-bold">Base de Datos</div>
                    <div className="text-slate-200 mt-0.5">SQLite (Modo WAL Concurrente)</div>
                    <div className="text-[10px] text-emerald-400 mt-1">✓ sicdin.db local activa</div>
                  </div>

                  <div className="p-3 bg-slate-900 border border-slate-800 rounded-xl">
                    <div className="text-[10px] text-slate-500 uppercase font-bold">Motor de WhatsApp</div>
                    <div className="text-slate-200 mt-0.5">Baileys WebSocket v6.7</div>
                    <div className="text-[10px] text-emerald-400 mt-1">
                      {estadoWs?.status === 'CONNECTED' ? '✓ Sesión vinculada y activa' : '⚪ Pendiente de vincular'}
                    </div>
                  </div>

                  <div className="p-3 bg-slate-900 border border-slate-800 rounded-xl">
                    <div className="text-[10px] text-slate-500 uppercase font-bold">Sincronización en Nube</div>
                    <div className="text-slate-200 mt-0.5">Deltas JSON / Multi-tienda</div>
                    <div className="text-[10px] text-blue-400 mt-1">✓ Motor SyncEngine listo</div>
                  </div>

                  <div className="p-3 bg-slate-900 border border-slate-800 rounded-xl">
                    <div className="text-[10px] text-slate-500 uppercase font-bold">Arquitectura</div>
                    <div className="text-slate-200 mt-0.5">React 19 + Node.js Express</div>
                    <div className="text-[10px] text-slate-500 mt-1">Pachuca de Soto, Hgo.</div>
                  </div>
                </div>
              </div>
            </div>
          )}
        </div>

        {/* Pie del Modal */}
        <div className="p-3.5 bg-slate-900 border-t border-slate-800 flex items-center justify-end">
          <button
            onClick={onClose}
            className="px-5 py-2 bg-slate-800 hover:bg-slate-700 text-white rounded-xl text-xs font-bold transition cursor-pointer"
          >
            Cerrar
          </button>
        </div>
      </div>
    </div>
  );
}
