import { useState, useEffect, useRef } from 'react';
import { ChevronLeft, ChevronRight, Calendar as CalendarIcon, ChevronDown } from 'lucide-react';

interface WinFormsDatePickerProps {
  value: string; // Formato YYYY-MM-DD
  onChange: (dateStr: string) => void;
  className?: string;
}

const MESES = [
  'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
  'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
];

const DIAS_SEMANA_CORTOS = ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'];
const DIAS_SEMANA_LARGOS = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];

export default function WinFormsDatePicker({ value, onChange, className = '' }: WinFormsDatePickerProps) {
  const [abierto, setAbierto] = useState(false);

  // Parsear valor inicial de forma segura sin desface de zona horaria
  const [añoSeleccionado, mesSeleccionado] = (value || new Date().toISOString().slice(0, 10))
    .split('-')
    .map(Number);

  // Estado del mes y año que se está visualizando en el calendario desplegado
  const [añoVista, setAñoVista] = useState<number>(añoSeleccionado || new Date().getFullYear());
  const [mesVista, setMesVista] = useState<number>((mesSeleccionado ? mesSeleccionado - 1 : new Date().getMonth()));

  const contenedorRef = useRef<HTMLDivElement>(null);

  // Sincronizar vista cuando cambia el valor externo
  useEffect(() => {
    if (value) {
      const [y, m] = value.split('-').map(Number);
      if (y && m) {
        setAñoVista(y);
        setMesVista(m - 1);
      }
    }
  }, [value]);

  // Cerrar al hacer clic afuera
  useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (contenedorRef.current && !contenedorRef.current.contains(e.target as Node)) {
        setAbierto(false);
      }
    };
    if (abierto) {
      document.addEventListener('mousedown', handleClickOutside);
    }
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [abierto]);

  // Formatear fecha para el display estilo Windows Forms Long Date: "Lunes, 26 de octubre de 2026"
  const textoFechaDisplay = () => {
    if (!value) return 'Seleccionar fecha...';
    const [y, m, d] = value.split('-').map(Number);
    const dateObj = new Date(y, m - 1, d);
    const nombreDia = DIAS_SEMANA_LARGOS[dateObj.getDay()] || '';
    const nombreMes = MESES[m - 1] || '';
    return `${nombreDia}, ${d} de ${nombreMes} de ${y}`;
  };

  // Hoy en formato YYYY-MM-DD
  const hoyStr = new Date().toISOString().slice(0, 10);
  const [hoyY, hoyM, hoyD] = hoyStr.split('-').map(Number);

  const retrocederMes = (e: React.MouseEvent) => {
    e.stopPropagation();
    if (mesVista === 0) {
      setMesVista(11);
      setAñoVista(prev => prev - 1);
    } else {
      setMesVista(prev => prev - 1);
    }
  };

  const avanzarMes = (e: React.MouseEvent) => {
    e.stopPropagation();
    if (mesVista === 11) {
      setMesVista(0);
      setAñoVista(prev => prev + 1);
    } else {
      setMesVista(prev => prev + 1);
    }
  };

  const seleccionarDia = (dateStr: string) => {
    onChange(dateStr);
    setAbierto(false);
  };

  const irAHoy = (e: React.MouseEvent) => {
    e.stopPropagation();
    onChange(hoyStr);
    setAñoVista(hoyY);
    setMesVista(hoyM - 1);
    setAbierto(false);
  };

  // Generar cuadrícula de 42 celdas estilo MonthCalendar
  const generarCeldasCalendario = () => {
    const primerDiaSemana = new Date(añoVista, mesVista, 1).getDay(); // 0 = Domingo
    const diasEnMes = new Date(añoVista, mesVista + 1, 0).getDate();
    const diasEnMesAnterior = new Date(añoVista, mesVista, 0).getDate();

    const celdas = [];

    // Días remanentes del mes previo
    for (let i = primerDiaSemana - 1; i >= 0; i--) {
      const day = diasEnMesAnterior - i;
      const prevM = mesVista === 0 ? 11 : mesVista - 1;
      const prevY = mesVista === 0 ? añoVista - 1 : añoVista;
      const dateStr = `${prevY}-${String(prevM + 1).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
      celdas.push({ day, esMesActual: false, dateStr });
    }

    // Días del mes en curso
    for (let day = 1; day <= diasEnMes; day++) {
      const dateStr = `${añoVista}-${String(mesVista + 1).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
      celdas.push({ day, esMesActual: true, dateStr });
    }

    // Días del mes siguiente para completar la cuadrícula de 42 celdas
    let siguienteDia = 1;
    while (celdas.length < 42) {
      const nextM = mesVista === 11 ? 0 : mesVista + 1;
      const nextY = mesVista === 11 ? añoVista + 1 : añoVista;
      const dateStr = `${nextY}-${String(nextM + 1).padStart(2, '0')}-${String(siguienteDia).padStart(2, '0')}`;
      celdas.push({ day: siguienteDia, esMesActual: false, dateStr });
      siguienteDia++;
    }

    return celdas;
  };

  return (
    <div ref={contenedorRef} className={`relative inline-block ${className}`}>
      {/* 
        Control principal estilo DateTimePicker de Windows Forms
        Caja de texto que muestra la fecha en formato largo + botón desplegable
      */}
      <div
        onClick={() => setAbierto(prev => !prev)}
        className="flex items-center justify-between gap-2.5 bg-slate-900 hover:bg-slate-800/90 border border-slate-700/80 hover:border-blue-500 rounded-xl px-3.5 py-1.5 cursor-pointer transition shadow-inner group/picker select-none"
      >
        <div className="flex items-center gap-2 min-w-0">
          <CalendarIcon className="w-4 h-4 text-blue-400 group-hover/picker:text-blue-300 shrink-0" />
          <span className="text-xs font-semibold text-slate-100 font-sans tracking-wide capitalize truncate">
            {textoFechaDisplay()}
          </span>
        </div>

        <div className="p-0.5 rounded text-slate-400 group-hover/picker:text-white transition shrink-0 border-l border-slate-800 pl-2">
          <ChevronDown className={`w-3.5 h-3.5 transition-transform duration-200 ${abierto ? 'rotate-180 text-blue-400' : ''}`} />
        </div>
      </div>

      {/* 
        Desplegable estilo MonthCalendar de Windows Forms
      */}
      {abierto && (
        <div className="absolute left-0 top-full mt-2 z-50 bg-slate-900 border border-slate-700 rounded-2xl shadow-2xl p-4 w-72 animate-in fade-in zoom-in-95 duration-150 backdrop-blur-md">
          {/* Cabecera del Mes: < [Mes Año] > */}
          <div className="flex items-center justify-between pb-3 border-b border-slate-800">
            <button
              type="button"
              onClick={retrocederMes}
              title="Mes anterior"
              className="p-1.5 hover:bg-slate-800 text-slate-400 hover:text-white rounded-lg transition cursor-pointer"
            >
              <ChevronLeft className="w-4 h-4" />
            </button>

            <div className="text-xs font-bold text-white tracking-wide">
              {MESES[mesVista]} {añoVista}
            </div>

            <button
              type="button"
              onClick={avanzarMes}
              title="Mes siguiente"
              className="p-1.5 hover:bg-slate-800 text-slate-400 hover:text-white rounded-lg transition cursor-pointer"
            >
              <ChevronRight className="w-4 h-4" />
            </button>
          </div>

          {/* Días de la semana: Do Lu Ma Mi Ju Vi Sá */}
          <div className="grid grid-cols-7 gap-1 text-center py-2 border-b border-slate-800/80">
            {DIAS_SEMANA_CORTOS.map((d, idx) => (
              <div
                key={d}
                className={`text-[10px] font-bold uppercase tracking-wider ${
                  idx === 0 || idx === 6 ? 'text-amber-400' : 'text-slate-400'
                }`}
              >
                {d}
              </div>
            ))}
          </div>

          {/* Cuadrícula de Días (6 semanas x 7 días) */}
          <div className="grid grid-cols-7 gap-1 pt-2">
            {generarCeldasCalendario().map(({ day, esMesActual, dateStr }) => {
              const esSeleccionado = dateStr === value;
              const esHoy = dateStr === hoyStr;

              return (
                <button
                  key={dateStr}
                  type="button"
                  onClick={() => seleccionarDia(dateStr)}
                  className={`h-8 w-8 text-xs font-mono font-semibold rounded-lg flex items-center justify-center transition cursor-pointer relative ${
                    esSeleccionado
                      ? 'bg-blue-600 text-white font-bold shadow-md shadow-blue-900 border border-blue-400'
                      : esMesActual
                      ? 'text-slate-200 hover:bg-slate-800 hover:text-white'
                      : 'text-slate-600 hover:bg-slate-800/50 hover:text-slate-400'
                  } ${esHoy && !esSeleccionado ? 'border border-blue-500/60 font-bold text-blue-300' : ''}`}
                >
                  {day}
                  {esHoy && (
                    <span
                      className={`absolute bottom-0.5 w-1 h-1 rounded-full ${
                        esSeleccionado ? 'bg-white' : 'bg-blue-400'
                      }`}
                    />
                  )}
                </button>
              );
            })}
          </div>

          {/* Pie de Página estilo Windows Forms: Botón Hoy */}
          <div className="mt-3 pt-2.5 border-t border-slate-800 flex justify-between items-center text-xs">
            <button
              type="button"
              onClick={irAHoy}
              className="text-[11px] font-bold text-blue-400 hover:text-blue-300 transition cursor-pointer flex items-center gap-1.5 hover:underline"
            >
              <span>Hoy:</span>
              <span className="font-mono text-slate-300">
                {hoyD} de {MESES[hoyM - 1]}
              </span>
            </button>

            <button
              type="button"
              onClick={() => setAbierto(false)}
              className="text-[11px] text-slate-400 hover:text-slate-200 px-2 py-0.5 rounded hover:bg-slate-800 transition cursor-pointer"
            >
              Cerrar
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
