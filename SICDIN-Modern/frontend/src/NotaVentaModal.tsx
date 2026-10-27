import { useState, useMemo } from 'react';
import {
  Printer,
  Receipt,
  X,
  FileText,
  Smartphone,
  Download,
  Check,
  AlertCircle,
  Send
} from 'lucide-react';
import { toPng, toJpeg } from 'html-to-image';
import { numeroALetras } from './utils/numeroALetras';
import { api } from './api';
import AjustesModal from './AjustesModal';

export interface NotaVentaData {
  folio: string;
  fecha?: string;
  hora?: string;
  usuario?: string;
  cliente: {
    Nombre: string;
    Direccion?: string;
    Telcel?: string;
    Telcasa?: string;
    RFC?: string;
    mail?: string;
  };
  items: Array<{
    Clave: string;
    Descripcion: string;
    Cantidad: number;
    Precio: number;
    Importe: number;
    Paquete?: string;
  }>;
  subtotal: number;
  descuento?: number;
  adeudoPrevio?: number;
  total: number;
  pagoEfectivo?: number;
  pagoTarjeta?: number;
  pagoTransfer?: number;
  cambio?: number;
  nuevoAdeudo?: number;
}

/**
 * Divide la lista de artículos para que quepan exactamente en hojas de Media Carta (Media Hoja: 216mm x 140mm).
 * Si hay más artículos de los que caben con el bloque de totales (~8), crea páginas de continuación
 * con encabezado y el aviso "Continúa en página siguiente --->" al estilo del SICDIN original.
 */
function paginarArticulos<T>(items: T[]): T[][] {
  const CAPACIDAD_SOLA = 8;
  const CAPACIDAD_INTERMEDIA = 10;
  const CAPACIDAD_FINAL = 7;

  if (items.length <= CAPACIDAD_SOLA) {
    return [items];
  }

  const paginas: T[][] = [];
  let restantes = [...items];

  while (restantes.length > 0) {
    if (restantes.length <= CAPACIDAD_FINAL) {
      paginas.push(restantes);
      break;
    }

    let tomar = CAPACIDAD_INTERMEDIA;
    if (restantes.length - tomar < 2) {
      tomar = Math.max(1, restantes.length - 2);
    }

    paginas.push(restantes.slice(0, tomar));
    restantes = restantes.slice(tomar);
  }

  return paginas;
}

interface NotaVentaModalProps {
  datos: NotaVentaData;
  onCerrar: () => void;
}

export default function NotaVentaModal({ datos, onCerrar }: NotaVentaModalProps) {
  const [formato, setFormato] = useState<'MEDIA_HOJA' | 'TICKET'>(() => {
    return (localStorage.getItem('sicdin_formato_impresion_default') as 'MEDIA_HOJA' | 'TICKET') || 'MEDIA_HOJA';
  });
  const [enviandoWs, setEnviandoWs] = useState<boolean>(false);
  const [descargandoPng, setDescargandoPng] = useState<boolean>(false);
  const [telefonoWs, setTelefonoWs] = useState<string>(() => datos.cliente.Telcel || datos.cliente.Telcasa || '');
  const [mostrarInputTelefono, setMostrarInputTelefono] = useState<boolean>(false);
  const [mensajeWs, setMensajeWs] = useState<{ tipo: 'exito' | 'error'; texto: string } | null>(null);
  const [modalVinculoWs, setModalVinculoWs] = useState<boolean>(false);

  const nombreTienda = localStorage.getItem('sicdin_nombre_tienda') || 'CASA LICONA';
  const giroTienda = localStorage.getItem('sicdin_giro_tienda') || 'Bicicletas, Refacciones y Accesorios';
  const direccionTienda = localStorage.getItem('sicdin_direccion_tienda') || 'Cto. Vicente Aguirre del Castillo, Local 71, Col. Juan C. Doria';
  const telefonosTienda = localStorage.getItem('sicdin_telefonos_tienda') || 'Pachuca de Soto, Hgo. • Tel. (771) 710-7860 • Cel. 771-121-60-98';

  const fechaObj = datos.fecha ? new Date(datos.fecha + 'T12:00:00') : new Date();
  const meses = [
    'enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio',
    'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre'
  ];
  const fechaTexto = `Pachuca de Soto, Hgo. a ${fechaObj.getDate()} de ${meses[fechaObj.getMonth()]} del ${fechaObj.getFullYear()}`;

  const totalPagado = (datos.pagoEfectivo || 0) + (datos.pagoTarjeta || 0) + (datos.pagoTransfer || 0);

  const paginasMediaHoja = useMemo(() => {
    return paginarArticulos(datos.items || []);
  }, [datos.items]);

  const ejecutarImpresion = () => {
    const elementoAImprimir = obtenerElementoImpresion();
    if (!elementoAImprimir) return;

    // Remover iframe previo si existiera
    const anterior = document.getElementById('iframe-impresion-sicdin');
    if (anterior) anterior.remove();

    const iframe = document.createElement('iframe');
    iframe.id = 'iframe-impresion-sicdin';
    iframe.style.position = 'fixed';
    iframe.style.top = '-9999px';
    iframe.style.left = '-9999px';
    iframe.style.width = '0';
    iframe.style.height = '0';
    iframe.style.border = 'none';
    document.body.appendChild(iframe);

    const iframeDoc = iframe.contentWindow?.document;
    if (!iframeDoc) return;

    let estilosHtml = '';
    document.querySelectorAll('link[rel="stylesheet"], style').forEach(el => {
      estilosHtml += el.outerHTML;
    });

    const estilosImpresion = `
      <style>
        @page {
          size: ${formato === 'MEDIA_HOJA' ? 'auto' : '80mm auto'};
          margin: 0;
        }
        *, *::before, *::after {
          box-sizing: border-box !important;
          -webkit-print-color-adjust: exact !important;
          print-color-adjust: exact !important;
        }
        html, body {
          margin: 0 !important;
          padding: 0 !important;
          background: #ffffff !important;
          color: #000000 !important;
          width: 100% !important;
          height: auto !important;
        }
        #area-impresion-media-hoja {
          display: block !important;
          width: 100% !important;
          margin: 0 !important;
          padding: 0 !important;
        }
        .pagina-media-hoja {
          width: 215.9mm !important;
          height: 138mm !important;
          max-height: 138mm !important;
          min-height: 138mm !important;
          box-sizing: border-box !important;
          padding: 5mm 6mm !important;
          margin: 0 auto !important;
          display: flex !important;
          flex-direction: column !important;
          justify-content: space-between !important;
          page-break-after: always !important;
          break-after: page !important;
          page-break-inside: avoid !important;
          break-inside: avoid !important;
          background: #ffffff !important;
          overflow: hidden !important;
          border: none !important;
          box-shadow: none !important;
        }
        .pagina-media-hoja:last-child {
          page-break-after: auto !important;
          break-after: auto !important;
        }
        #area-impresion-ticket {
          width: 80mm !important;
          margin: 0 auto !important;
          padding: 3mm !important;
          background: #ffffff !important;
          border: none !important;
          box-shadow: none !important;
        }
      </style>
    `;

    iframeDoc.open();
    iframeDoc.write(`
      <!DOCTYPE html>
      <html>
        <head>
          <title>Impresión Comprobante - ${datos.folio}</title>
          ${estilosHtml}
          ${estilosImpresion}
        </head>
        <body>
          ${elementoAImprimir.outerHTML}
        </body>
      </html>
    `);
    iframeDoc.close();

    // Pequeño retardo para asegurar que imágenes y fuentes carguen antes del diálogo
    setTimeout(() => {
      try {
        iframe.contentWindow?.focus();
        iframe.contentWindow?.print();
      } catch (e) {
        console.error('Error al imprimir iframe:', e);
        window.print();
      }
    }, 250);
  };

  const obtenerElementoImpresion = (): HTMLElement | null => {
    const id = formato === 'MEDIA_HOJA' ? 'area-impresion-media-hoja' : 'area-impresion-ticket';
    return document.getElementById(id);
  };

  const generarImagenesBase64 = async (formatoSalida: 'jpeg' | 'png' = 'jpeg'): Promise<string[]> => {
    const elementos: HTMLElement[] = [];
    if (formato === 'MEDIA_HOJA') {
      paginasMediaHoja.forEach((_, idx) => {
        const el = document.getElementById(`pagina-media-hoja-${idx}`);
        if (el) elementos.push(el);
      });
      // Fallback si por alguna razón no se encuentran por id
      if (elementos.length === 0) {
        const parent = document.getElementById('area-impresion-media-hoja');
        if (parent) {
          const hijos = Array.from(parent.querySelectorAll('.pagina-media-hoja')) as HTMLElement[];
          elementos.push(...hijos);
        }
      }
    } else {
      const el = document.getElementById('area-impresion-ticket');
      if (el) elementos.push(el);
    }

    if (elementos.length === 0) {
      throw new Error('No se encontraron elementos para generar la imagen del comprobante');
    }

    const imagenes: string[] = [];
    for (const el of elementos) {
      if (formatoSalida === 'png') {
        const img = await toPng(el, {
          backgroundColor: '#ffffff',
          pixelRatio: 2,
          cacheBust: true
        });
        imagenes.push(img);
      } else {
        const img = await toJpeg(el, {
          quality: 0.95,
          backgroundColor: '#ffffff',
          pixelRatio: 2,
          cacheBust: true
        });
        imagenes.push(img);
      }
    }

    return imagenes;
  };

  const handleDescargarPng = async () => {
    setDescargandoPng(true);
    try {
      const imagenes = await generarImagenesBase64('png');
      imagenes.forEach((dataUrl, idx) => {
        const sufijo = imagenes.length > 1 ? `_pag${idx + 1}` : '';
        const a = document.createElement('a');
        a.download = `Comprobante_${datos.folio}${sufijo}_${formato.toLowerCase()}.png`;
        a.href = dataUrl;
        a.click();
      });
    } catch (err: any) {
      alert('Error al generar la imagen: ' + (err.message || 'Error desconocido'));
    } finally {
      setDescargandoPng(false);
    }
  };

  const handleDispararEnvioWhatsApp = async (telDestino?: string) => {
    const num = (telDestino !== undefined ? telDestino : telefonoWs).replace(/\D/g, '');
    if (num.length < 10) {
      setMostrarInputTelefono(true);
      return;
    }

    setEnviandoWs(true);
    setMensajeWs(null);

    try {
      const imagenes = await generarImagenesBase64('jpeg');
      const folioLimpio = datos.folio && datos.folio !== 'undefined' ? datos.folio.trim() : '';

      const plantillaGuardada = localStorage.getItem('sicdin_whatsapp_template') ||
        '🚲 *Casa Licona - Bicicletas e Innovaciones*\n\nEstimado(a) *{cliente}*:\nAdjuntamos su comprobante de venta digital con Folio: *{folio}*.\nTotal de la nota: *${total} MXN*.\n\n¡Agradecemos su preferencia y confianza!';

      const mensajeBase = plantillaGuardada
        .replace('{cliente}', datos.cliente.Nombre || 'Cliente')
        .replace('{folio}', folioLimpio || 'S/F')
        .replace('{total}', datos.total.toFixed(2));

      for (let i = 0; i < imagenes.length; i++) {
        let mensajePagina = mensajeBase;
        if (imagenes.length > 1) {
          if (i === 0) {
            mensajePagina += `\n\n📄 *[Hoja 1 de ${imagenes.length} - Artículos]*`;
          } else if (i === imagenes.length - 1) {
            mensajePagina = `🚲 *Casa Licona - Folio ${folioLimpio}*\n\n📄 *[Hoja ${i + 1} de ${imagenes.length} - Totales y Cierre]*`;
          } else {
            mensajePagina = `🚲 *Casa Licona - Folio ${folioLimpio}*\n\n📄 *[Hoja ${i + 1} de ${imagenes.length} - Continuación]*`;
          }
        }

        await api.enviarTicketWhatsApp({
          telefono: num,
          imagenBase64: imagenes[i],
          folio: folioLimpio,
          mensaje: mensajePagina
        });

        // Breve pausa para asegurar el orden cronológico en WhatsApp
        if (i < imagenes.length - 1) {
          await new Promise(r => setTimeout(r, 800));
        }
      }

      setMensajeWs({
        tipo: 'exito',
        texto: imagenes.length > 1
          ? `¡Se enviaron las ${imagenes.length} hojas del comprobante exitosamente por WhatsApp a +52 ${num}!`
          : `¡Comprobante enviado exitosamente por WhatsApp a +52 ${num}!`
      });
      setMostrarInputTelefono(false);
    } catch (err: any) {
      const msg = err.message || 'Error al enviar por WhatsApp';
      setMensajeWs({
        tipo: 'error',
        texto: msg
      });
      if (msg.includes('no está conectado')) {
        setModalVinculoWs(true);
      }
    } finally {
      setEnviandoWs(false);
    }
  };

  return (
    <div id="modal-comprobante-wrapper" className="fixed inset-0 bg-black/85 backdrop-blur-sm z-50 flex items-center justify-center p-2 sm:p-4 overflow-y-auto">
      {/* Contenedor Modal */}
      <div id="modal-comprobante-dialog" className="bg-slate-900 border border-slate-700/80 rounded-2xl max-w-4xl w-full shadow-2xl flex flex-col my-auto max-h-[95vh] overflow-hidden">
        {/* Cabecera del Modal (No se imprime) */}
        <div className="p-3.5 bg-slate-900 border-b border-slate-800 flex items-center justify-between print:hidden">
          <div className="flex items-center gap-3">
            <div className="p-2 bg-blue-500/10 text-blue-400 rounded-lg border border-blue-500/20">
              <Printer className="w-5 h-5 text-blue-400" />
            </div>
            <div>
              <div className="flex items-center gap-2">
                <h3 className="text-sm font-bold text-white tracking-wide">Comprobante de Venta</h3>
                <span className="text-[10px] font-mono font-bold bg-blue-600/20 text-blue-300 border border-blue-500/30 px-2 py-0.5 rounded">
                  {datos.folio}
                </span>
              </div>
              <p className="text-[11px] text-slate-400">Seleccione el formato e imprima en su impresora local</p>
            </div>
          </div>

          <div className="flex items-center gap-2">
            {/* Selector de formato */}
            <div className="flex items-center bg-slate-950 p-0.5 rounded-lg border border-slate-800 text-xs">
              <button
                onClick={() => setFormato('MEDIA_HOJA')}
                className={`px-3 py-1.5 rounded-md font-bold transition flex items-center gap-1.5 cursor-pointer ${
                  formato === 'MEDIA_HOJA'
                    ? 'bg-blue-600 text-white shadow-sm'
                    : 'text-slate-400 hover:text-white'
                }`}
              >
                <FileText className="w-3.5 h-3.5" />
                <span>Media Hoja</span>
              </button>
              <button
                onClick={() => setFormato('TICKET')}
                className={`px-3 py-1.5 rounded-md font-bold transition flex items-center gap-1.5 cursor-pointer ${
                  formato === 'TICKET'
                    ? 'bg-blue-600 text-white shadow-sm'
                    : 'text-slate-400 hover:text-white'
                }`}
              >
                <Receipt className="w-3.5 h-3.5" />
                <span>Ticket Térmico</span>
              </button>
            </div>

            {/* Botón Descargar Imagen PNG */}
            <button
              onClick={handleDescargarPng}
              disabled={descargandoPng}
              title="Descargar imagen PNG para guardar o enviar por correo"
              className="flex items-center gap-1.5 px-3 py-1.5 bg-slate-800 hover:bg-slate-700 text-slate-200 rounded-lg text-xs font-semibold cursor-pointer transition border border-slate-700"
            >
              <Download className={`w-3.5 h-3.5 ${descargandoPng ? 'animate-bounce text-blue-400' : 'text-slate-400'}`} />
              <span className="hidden sm:inline">Descargar PNG</span>
            </button>

            {/* Botón Enviar por WhatsApp con Baileys */}
            <button
              onClick={() => handleDispararEnvioWhatsApp()}
              disabled={enviandoWs}
              title="Enviar imagen de la nota directamente al WhatsApp del cliente"
              className="flex items-center gap-1.5 px-3.5 py-1.5 bg-emerald-600 hover:bg-emerald-500 text-white rounded-lg text-xs font-bold shadow-md cursor-pointer transition border border-emerald-400/40"
            >
              <Smartphone className={`w-3.5 h-3.5 ${enviandoWs ? 'animate-pulse text-white' : ''}`} />
              <span>{enviandoWs ? 'Enviando...' : 'WhatsApp'}</span>
            </button>

            {/* Botón Imprimir */}
            <button
              onClick={ejecutarImpresion}
              className="flex items-center gap-1.5 px-4 py-1.5 bg-blue-600 hover:bg-blue-500 text-white rounded-lg text-xs font-bold shadow-md cursor-pointer transition border border-blue-400/30"
            >
              <Printer className="w-4 h-4" />
              <span>Imprimir</span>
            </button>

            <button
              onClick={onCerrar}
              className="p-1.5 text-slate-400 hover:text-white rounded-lg hover:bg-slate-800 transition cursor-pointer"
            >
              <X className="w-5 h-5" />
            </button>
          </div>
        </div>

        {/* Barra de Solicitud de Teléfono si no estaba registrado */}
        {mostrarInputTelefono && (
          <div className="p-3 bg-emerald-950/70 border-b border-emerald-800/80 flex flex-wrap items-center justify-between gap-3 text-xs animate-in slide-in-from-top-2">
            <div className="flex items-center gap-2 text-emerald-200">
              <Smartphone className="w-4 h-4 text-emerald-400 shrink-0" />
              <span>Ingresa el número celular del cliente (10 dígitos):</span>
            </div>
            <div className="flex items-center gap-2">
              <input
                type="tel"
                value={telefonoWs}
                onChange={e => setTelefonoWs(e.target.value)}
                placeholder="Ej. 7711216098"
                className="bg-slate-900 border border-emerald-700/80 focus:border-emerald-400 text-white font-mono px-3 py-1 rounded-lg text-xs w-44 focus:outline-none"
                autoFocus
                onKeyDown={e => {
                  if (e.key === 'Enter') handleDispararEnvioWhatsApp(telefonoWs);
                }}
              />
              <button
                onClick={() => handleDispararEnvioWhatsApp(telefonoWs)}
                disabled={enviandoWs}
                className="px-3 py-1 bg-emerald-600 hover:bg-emerald-500 text-white font-bold rounded-lg text-xs transition cursor-pointer flex items-center gap-1 shadow"
              >
                <Send className="w-3 h-3" />
                <span>Enviar</span>
              </button>
              <button
                onClick={() => setMostrarInputTelefono(false)}
                className="p-1 text-slate-400 hover:text-white rounded"
              >
                <X className="w-3.5 h-3.5" />
              </button>
            </div>
          </div>
        )}

        {/* Notificación de Resultado de WhatsApp */}
        {mensajeWs && (
          <div
            className={`p-2.5 px-4 text-xs flex items-center justify-between border-b ${
              mensajeWs.tipo === 'exito'
                ? 'bg-emerald-950/80 text-emerald-200 border-emerald-800'
                : 'bg-rose-950/80 text-rose-200 border-rose-800'
            }`}
          >
            <div className="flex items-center gap-2">
              {mensajeWs.tipo === 'exito' ? (
                <Check className="w-4 h-4 text-emerald-400 shrink-0" />
              ) : (
                <AlertCircle className="w-4 h-4 text-rose-400 shrink-0" />
              )}
              <span>{mensajeWs.texto}</span>
            </div>
            {mensajeWs.tipo === 'error' && mensajeWs.texto.includes('no está conectado') && (
              <button
                onClick={() => setModalVinculoWs(true)}
                className="px-2.5 py-1 bg-emerald-600 hover:bg-emerald-500 text-white font-bold rounded text-[11px] ml-3 transition cursor-pointer shrink-0"
              >
                Escanear QR de WhatsApp
              </button>
            )}
            <button
              onClick={() => setMensajeWs(null)}
              className="text-slate-400 hover:text-white p-0.5 ml-2 cursor-pointer"
            >
              <X className="w-3.5 h-3.5" />
            </button>
          </div>
        )}

        {/* Área de Visualización e Impresión */}
        <div id="modal-comprobante-body" className="flex-1 overflow-y-auto p-4 sm:p-6 bg-slate-950/60 flex justify-center">
          {/* ========================================================================= */}
          {/* FORMATO 1: NOTA DE VENTA / REMISIÓN EN MEDIA HOJA (Media Carta Horizontal)*/}
          {/* ========================================================================= */}
          {formato === 'MEDIA_HOJA' ? (
            <div
              id="area-impresion-media-hoja"
              className="flex flex-col gap-6 print:gap-0 w-full max-w-[216mm]"
            >
              {paginasMediaHoja.map((itemsPagina, indexPag) => {
                const esUltima = indexPag === paginasMediaHoja.length - 1;
                const numPag = indexPag + 1;
                const totalPags = paginasMediaHoja.length;

                return (
                  <div
                    key={indexPag}
                    id={`pagina-media-hoja-${indexPag}`}
                    className="pagina-media-hoja bg-white text-black p-5 rounded-lg shadow-xl w-full min-h-[140mm] font-sans flex flex-col justify-between text-[11px] leading-tight border border-slate-300 print:border-none print:shadow-none print:m-0 print:p-4 print:w-full"
                    style={{
                      pageBreakAfter: esUltima ? 'auto' : 'always',
                      breakAfter: esUltima ? 'auto' : 'page'
                    }}
                  >
                    {/* Encabezado */}
                    <div>
                      <div className="flex items-start justify-between border-b-2 border-black pb-2 mb-2">
                        {/* Logo y Datos de la Tienda */}
                        <div className="flex items-start gap-3">
                          <img
                            src="/logo.png"
                            alt="Logo"
                            className="w-13 h-13 object-contain shrink-0"
                          />
                          <div>
                            <h1 className="text-base font-extrabold uppercase tracking-wide text-black leading-tight">
                              {nombreTienda}
                            </h1>
                            <div className="text-[10px] font-semibold text-gray-800">
                              {giroTienda}
                            </div>
                            <div className="text-[9px] text-gray-700 leading-snug mt-0.5">
                              {direccionTienda}<br />
                              {telefonosTienda}
                            </div>
                          </div>
                        </div>

                        {/* Cuadro de Folio y Fecha */}
                        <div className="text-right">
                          <div className="inline-block border-2 border-black px-3 py-1 rounded bg-gray-50 mb-1">
                            <div className="text-[9px] font-extrabold uppercase text-gray-600">
                              {totalPags > 1 ? `REMISIÓN (${numPag}/${totalPags})` : 'REMISIÓN / NOTA'}
                            </div>
                            <div className="text-sm font-black font-mono text-black">{datos.folio}</div>
                          </div>
                          <div className="text-[9px] font-semibold text-gray-800">{fechaTexto}</div>
                          <div className="text-[9px] text-gray-600">
                            Atendido por: {datos.usuario || 'ADMIN'}
                            {totalPags > 1 && (
                              <span className="font-bold text-black ml-1.5 font-mono">
                                [Pág. {numPag} de {totalPags}]
                              </span>
                            )}
                          </div>
                        </div>
                      </div>

                      {/* Datos del Cliente */}
                      <div className="bg-gray-50 border border-gray-300 rounded p-1.5 mb-2 grid grid-cols-2 gap-x-4 gap-y-0.5 text-[10px]">
                        <div>
                          <span className="font-bold text-gray-700">CLIENTE: </span>
                          <span className="font-extrabold text-black uppercase">{datos.cliente.Nombre || 'MOSTRADOR'}</span>
                        </div>
                        <div>
                          <span className="font-bold text-gray-700">TELÉFONO: </span>
                          <span>{datos.cliente.Telcel || datos.cliente.Telcasa || 'N/A'}</span>
                        </div>
                        <div className="col-span-2">
                          <span className="font-bold text-gray-700">DIRECCIÓN: </span>
                          <span>{datos.cliente.Direccion || 'Ciudad'}</span>
                        </div>
                      </div>

                      {/* Tabla de Artículos */}
                      <table className="w-full border-collapse mb-1">
                        <thead>
                          <tr className="border-y-2 border-black bg-gray-100 text-[10px]">
                            <th className="py-1 px-1.5 text-center font-bold w-12">CANT.</th>
                            <th className="py-1 px-2 text-left font-bold w-24">CLAVE</th>
                            <th className="py-1 px-2 text-left font-bold">DESCRIPCIÓN</th>
                            <th className="py-1 px-2 text-right font-bold w-20">PRECIO</th>
                            <th className="py-1 px-2 text-right font-bold w-24">IMPORTE</th>
                          </tr>
                        </thead>
                        <tbody className="divide-y divide-gray-200 text-[10px]">
                          {itemsPagina.map((it, idx) => (
                            <tr key={idx} className="hover:bg-gray-50">
                              <td className="py-0.5 px-1.5 text-center font-bold font-mono">{it.Cantidad}</td>
                              <td className="py-0.5 px-2 text-left font-mono font-medium text-gray-800">{it.Clave}</td>
                              <td className="py-0.5 px-2 text-left font-medium">
                                {it.Descripcion}
                                {it.Paquete && <span className="text-[9px] text-gray-500 ml-1">({it.Paquete})</span>}
                              </td>
                              <td className="py-0.5 px-2 text-right font-mono">${it.Precio.toFixed(2)}</td>
                              <td className="py-0.5 px-2 text-right font-mono font-bold">${it.Importe.toFixed(2)}</td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>

                    {/* Pie de Nota: si es última página muestra Totales y Leyenda; si no, aviso de continuación */}
                    {esUltima ? (
                      <div className="border-t-2 border-black pt-2 mt-auto">
                        <div className="grid grid-cols-12 gap-3">
                          {/* Izquierda: Total con letra y leyenda (SIN firmas ni sello) */}
                          <div className="col-span-7 flex flex-col justify-between">
                            <div>
                              <div className="text-[9px] font-bold uppercase text-gray-700">Importe con Letra:</div>
                              <div className="text-[10px] font-bold italic text-black bg-gray-50 p-1 rounded border border-gray-200">
                                ({numeroALetras(datos.total)})
                              </div>
                            </div>

                            <div className="text-[8px] text-gray-600 mt-2 leading-tight">
                              * No se aceptan devoluciones. Todo cambio genera una nota de crédito válida por 30 días naturales.
                              Garantía sujeta a términos del fabricante y presentación de este comprobante original.
                            </div>

                            <div className="mt-2 text-[10px] font-bold text-gray-800">
                              ¡Agradecemos su preferencia y confianza!
                            </div>
                          </div>

                          {/* Derecha: Desglose financiero y Total */}
                          <div className="col-span-5 bg-gray-50 border border-gray-300 rounded p-2 text-[10px] font-mono space-y-1">
                            <div className="flex justify-between">
                              <span className="text-gray-700">SUBTOTAL VENTA:</span>
                              <span className="font-bold">${datos.subtotal.toFixed(2)}</span>
                            </div>

                            {(datos.adeudoPrevio || 0) > 0 && (
                              <div className="flex justify-between text-amber-900">
                                <span>ADEUDO ANTERIOR:</span>
                                <span className="font-bold">+${datos.adeudoPrevio?.toFixed(2)}</span>
                              </div>
                            )}

                            {(datos.descuento || 0) > 0 && (
                              <div className="flex justify-between text-emerald-800">
                                <span>DESCUENTO:</span>
                                <span className="font-bold">-${datos.descuento?.toFixed(2)}</span>
                              </div>
                            )}

                            <div className="flex justify-between text-xs font-black border-t-2 border-b-2 border-black py-1 my-1">
                              <span>TOTAL CONSOLIDADO:</span>
                              <span>${datos.total.toFixed(2)}</span>
                            </div>

                            {/* Desglose de Pago */}
                            <div className="pt-1 text-[9px] text-gray-700 space-y-0.5">
                              {(datos.pagoEfectivo || 0) > 0 && (
                                <div className="flex justify-between">
                                  <span>Pago Efectivo:</span>
                                  <span>${datos.pagoEfectivo?.toFixed(2)}</span>
                                </div>
                              )}
                              {(datos.pagoTarjeta || 0) > 0 && (
                                <div className="flex justify-between">
                                  <span>Pago Tarjeta:</span>
                                  <span>${datos.pagoTarjeta?.toFixed(2)}</span>
                                </div>
                              )}
                              {(datos.pagoTransfer || 0) > 0 && (
                                <div className="flex justify-between">
                                  <span>Pago Transferencia:</span>
                                  <span>${datos.pagoTransfer?.toFixed(2)}</span>
                                </div>
                              )}
                            </div>

                            {(datos.cambio || 0) > 0 && (
                              <div className="flex justify-between text-emerald-700 font-bold border-t border-dashed border-gray-300 pt-1">
                                <span>CAMBIO ENTREGADO:</span>
                                <span>${datos.cambio?.toFixed(2)}</span>
                              </div>
                            )}

                            {(datos.nuevoAdeudo || 0) > 0 && (
                              <div className="flex justify-between text-rose-700 font-bold border-t border-dashed border-gray-300 pt-1">
                                <span>SALDO PENDIENTE:</span>
                                <span>${datos.nuevoAdeudo?.toFixed(2)}</span>
                              </div>
                            )}
                          </div>
                        </div>
                      </div>
                    ) : (
                      <div className="border-t-2 border-black pt-2 mt-auto flex justify-between items-center text-[10px]">
                        <span className="text-gray-500 italic">Continúa desglose en la siguiente hoja...</span>
                        <span className="font-bold font-mono text-black bg-gray-100 px-2 py-0.5 rounded border border-gray-300">
                          Continúa Página {numPag + 1} de {totalPags} ➔
                        </span>
                      </div>
                    )}
                  </div>
                );
              })}
            </div>
          ) : (
            /* ========================================================================= */
            /* FORMATO 2: TICKET TÉRMICO (58mm / 80mm Rollo)                             */
            /* ========================================================================= */
            <div
              id="area-impresion-ticket"
              className="bg-white text-black p-4 rounded-lg shadow-xl w-72 font-mono text-[11px] leading-tight border border-slate-300 print:border-none print:shadow-none print:m-0 print:p-2 print:w-full"
            >
              {/* Encabezado Ticket */}
              <div className="text-center pb-2 border-b border-dashed border-black mb-2">
                <img src="/logo.png" alt="Logo" className="w-10 h-10 object-contain mx-auto mb-1" />
                <div className="text-sm font-black tracking-wide uppercase">{nombreTienda}</div>
                <div className="text-[10px] font-semibold">{giroTienda}</div>
                <div className="text-[9px] text-gray-700 mt-1">
                  {direccionTienda}<br />
                  {telefonosTienda}
                </div>
              </div>

              {/* Folio y Fecha */}
              <div className="text-[10px] pb-2 border-b border-dashed border-black mb-2 space-y-0.5">
                <div className="flex justify-between">
                  <span>FOLIO:</span>
                  <span className="font-black text-xs">{datos.folio}</span>
                </div>
                <div className="flex justify-between">
                  <span>FECHA:</span>
                  <span>{datos.fecha || new Date().toISOString().slice(0, 10)} {datos.hora || ''}</span>
                </div>
                <div className="flex justify-between">
                  <span>CAJERO:</span>
                  <span>{datos.usuario || 'ADMIN'}</span>
                </div>
                <div className="flex justify-between">
                  <span>CLIENTE:</span>
                  <span className="font-bold truncate max-w-[140px]">{datos.cliente.Nombre || 'MOSTRADOR'}</span>
                </div>
              </div>

              {/* Items Ticket */}
              <div className="border-b border-dashed border-black pb-2 mb-2">
                <div className="flex justify-between font-bold text-[10px] border-b border-black pb-1 mb-1">
                  <span>DESCRIPCIÓN</span>
                  <span>IMPORTE</span>
                </div>
                {datos.items.map((it, idx) => (
                  <div key={idx} className="py-0.5 text-[10px]">
                    <div className="font-semibold truncate">{it.Descripcion}</div>
                    <div className="flex justify-between text-gray-600 pl-2">
                      <span>{it.Cantidad} x ${it.Precio.toFixed(2)}</span>
                      <span className="font-bold text-black">${it.Importe.toFixed(2)}</span>
                    </div>
                  </div>
                ))}
              </div>

              {/* Totales Ticket */}
              <div className="space-y-1 text-[11px] pb-2 border-b border-dashed border-black mb-2">
                <div className="flex justify-between">
                  <span>Subtotal:</span>
                  <span>${datos.subtotal.toFixed(2)}</span>
                </div>
                {(datos.adeudoPrevio || 0) > 0 && (
                  <div className="flex justify-between">
                    <span>Adeudo Anterior:</span>
                    <span>+${datos.adeudoPrevio?.toFixed(2)}</span>
                  </div>
                )}
                {(datos.descuento || 0) > 0 && (
                  <div className="flex justify-between">
                    <span>Descuento:</span>
                    <span>-${datos.descuento?.toFixed(2)}</span>
                  </div>
                )}
                <div className="flex justify-between font-black text-sm border-t border-black pt-1">
                  <span>TOTAL:</span>
                  <span>${datos.total.toFixed(2)}</span>
                </div>

                <div className="pt-1 text-[10px] text-gray-700">
                  <div className="flex justify-between">
                    <span>Pagado:</span>
                    <span>${totalPagado.toFixed(2)}</span>
                  </div>
                  {(datos.cambio || 0) > 0 && (
                    <div className="flex justify-between font-bold">
                      <span>Cambio:</span>
                      <span>${datos.cambio?.toFixed(2)}</span>
                    </div>
                  )}
                  {(datos.nuevoAdeudo || 0) > 0 && (
                    <div className="flex justify-between font-bold text-rose-800">
                      <span>Resta Adeudo:</span>
                      <span>${datos.nuevoAdeudo?.toFixed(2)}</span>
                    </div>
                  )}
                </div>
              </div>

              {/* Pie Ticket */}
              <div className="text-center text-[9px] text-gray-700 space-y-1">
                <div className="font-semibold italic">({numeroALetras(datos.total)})</div>
                <div>¡Gracias por su compra!</div>
                <div>Conserve este comprobante para cualquier aclaración o garantía</div>
              </div>
            </div>
          )}
        </div>

        {/* Pie Modal (Acciones) */}
        <div className="p-3 bg-slate-900 border-t border-slate-800 flex items-center justify-between text-xs print:hidden">
          <div className="text-slate-400 text-[11px]">
            {formato === 'MEDIA_HOJA' ? 'Orientación recomendada: Horizontal (Media Carta / Carta dividida)' : 'Orientación recomendada: Vertical (80mm)'}
          </div>
          <div className="flex items-center gap-2">
            <button
              onClick={onCerrar}
              className="px-4 py-2 bg-slate-800 hover:bg-slate-700 text-slate-300 rounded-xl font-semibold cursor-pointer transition"
            >
              Cerrar
            </button>
            <button
              onClick={ejecutarImpresion}
              className="flex items-center gap-2 px-5 py-2 bg-blue-600 hover:bg-blue-500 text-white rounded-xl font-bold shadow-lg shadow-blue-600/30 cursor-pointer transition border border-blue-400/30"
            >
              <Printer className="w-4 h-4" />
              <span>Imprimir {formato === 'MEDIA_HOJA' ? 'Nota Media Hoja' : 'Ticket'}</span>
            </button>
          </div>
        </div>
      </div>

      {/* Estilos CSS dedicados a la impresión directa del navegador (@media print) */}
      <style>{`
        @media print {
          /* 1. Resetear la jerarquía de la app para que no restrinja la altura a 100vh ni corte páginas */
          html, body, #root, #root > div {
            overflow: visible !important;
            height: auto !important;
            min-height: 0 !important;
            max-height: none !important;
            background: #ffffff !important;
          }

          /* 2. Resetear todos los contenedores padre del modal */
          #modal-comprobante-wrapper {
            visibility: visible !important;
            position: static !important;
            display: block !important;
            inset: auto !important;
            background: transparent !important;
            padding: 0 !important;
            margin: 0 !important;
            overflow: visible !important;
            width: 100% !important;
            height: auto !important;
            min-height: 0 !important;
          }

          #modal-comprobante-dialog {
            visibility: visible !important;
            position: static !important;
            display: block !important;
            background: transparent !important;
            border: none !important;
            box-shadow: none !important;
            max-width: none !important;
            max-height: none !important;
            width: 100% !important;
            height: auto !important;
            overflow: visible !important;
            margin: 0 !important;
            padding: 0 !important;
          }

          #modal-comprobante-body {
            visibility: visible !important;
            position: static !important;
            display: block !important;
            background: transparent !important;
            overflow: visible !important;
            height: auto !important;
            width: 100% !important;
            margin: 0 !important;
            padding: 0 !important;
          }

          /* 3. Área de Media Hoja: flujo estático normal para que las páginas fluyan secuencialmente sin sobreponerse */
          #area-impresion-media-hoja {
            visibility: visible !important;
            position: static !important;
            display: block !important;
            width: 100% !important;
            max-width: none !important;
            margin: 0 !important;
            padding: 0 !important;
            overflow: visible !important;
          }

          #area-impresion-media-hoja * {
            visibility: visible !important;
          }

          /* 4. Cada página física de Media Hoja */
          .pagina-media-hoja {
            visibility: visible !important;
            position: relative !important;
            display: flex !important;
            flex-direction: column !important;
            justify-content: space-between !important;
            width: 100% !important;
            max-width: 215.9mm !important;
            height: 138mm !important;
            min-height: 138mm !important;
            max-height: 138mm !important;
            box-sizing: border-box !important;
            padding: 5mm 6mm !important;
            margin: 0 auto !important;
            page-break-after: always !important;
            break-after: page !important;
            page-break-inside: avoid !important;
            break-inside: avoid !important;
            background: #ffffff !important;
            border: none !important;
            box-shadow: none !important;
            overflow: hidden !important;
          }

          .pagina-media-hoja:last-child {
            page-break-after: auto !important;
            break-after: auto !important;
          }

          /* 5. Formato Ticket térmico continuo */
          #area-impresion-ticket {
            visibility: visible !important;
            position: static !important;
            display: block !important;
            width: 80mm !important;
            margin: 0 auto !important;
            padding: 2mm !important;
            box-shadow: none !important;
            border: none !important;
          }

          #area-impresion-ticket * {
            visibility: visible !important;
          }

          @page {
            size: ${formato === 'MEDIA_HOJA' ? 'auto' : '80mm auto'};
            margin: 0;
          }
        }
      `}</style>
      {/* Modal para vincular WhatsApp si no está conectado */}
      {modalVinculoWs && (
        <AjustesModal seccionInicial="whatsapp" onClose={() => setModalVinculoWs(false)} />
      )}
    </div>
  );
}
