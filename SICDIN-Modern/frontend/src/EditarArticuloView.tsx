import { useState } from 'react';
import { Save, X, Package, DollarSign, Barcode, CheckCircle, AlertCircle } from 'lucide-react';
import { api, type Articulo } from './api';

interface EditarArticuloViewProps {
  clave: string;
  articuloInicial: Articulo;
  onGuardado: (artActualizado: Articulo) => void;
  onCerrar: () => void;
}

export default function EditarArticuloView({
  clave,
  articuloInicial,
  onGuardado,
  onCerrar
}: EditarArticuloViewProps) {
  const [formData, setFormData] = useState<Articulo>({ ...articuloInicial });
  const [guardando, setGuardando] = useState(false);
  const [mensaje, setMensaje] = useState<{ tipo: 'ok' | 'error'; texto: string } | null>(null);

  const handleChange = (campo: keyof Articulo, valor: any) => {
    setFormData(prev => ({
      ...prev,
      [campo]: valor
    }));
  };

  const handleGuardar = async (e: React.FormEvent) => {
    e.preventDefault();
    setGuardando(true);
    setMensaje(null);

    try {
      const res = await api.guardarArticulo({
        ...formData,
        Menudeo: Number(formData.Menudeo) || 0,
        Mayoreo: Number(formData.Mayoreo) || 0,
        PME: Number(formData.PME) || 0,
        Especial: Number(formData.Especial) || 0,
        Existencias: Number(formData.Existencias) || 0
      });

      if (res.success) {
        setMensaje({ tipo: 'ok', texto: 'Artículo actualizado correctamente (delta emitido para sincronización)' });
        onGuardado(formData);
      } else {
        setMensaje({ tipo: 'error', texto: 'No se pudo guardar el artículo' });
      }
    } catch (err: any) {
      setMensaje({ tipo: 'error', texto: err.message || 'Error de conexión' });
    } finally {
      setGuardando(false);
    }
  };

  return (
    <div className="flex-1 flex flex-col h-full bg-slate-950 text-slate-100 overflow-y-auto">
      {/* Cabecera */}
      <div className="p-4 bg-slate-900/60 border-b border-slate-800 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <div className="p-2 bg-amber-500/10 text-amber-400 rounded-lg border border-amber-500/20">
            <Package className="w-5 h-5" />
          </div>
          <div>
            <h2 className="text-lg font-bold text-white tracking-wide">
              Editar Artículo: <span className="text-amber-400 font-mono">{clave}</span>
            </h2>
            <p className="text-xs text-slate-400">Panel exclusivo de Administrador</p>
          </div>
        </div>

        <button
          onClick={onCerrar}
          className="flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-300 text-xs border border-slate-700 transition cursor-pointer"
        >
          <X className="w-4 h-4" />
          <span>Cerrar pestaña</span>
        </button>
      </div>

      {/* Contenido / Formulario */}
      <div className="max-w-4xl w-full mx-auto p-6">
        {mensaje && (
          <div
            className={`mb-6 p-4 rounded-xl flex items-center gap-3 text-sm border ${
              mensaje.tipo === 'ok'
                ? 'bg-emerald-950/60 text-emerald-300 border-emerald-800'
                : 'bg-rose-950/60 text-rose-300 border-rose-800'
            }`}
          >
            {mensaje.tipo === 'ok' ? (
              <CheckCircle className="w-5 h-5 text-emerald-400 shrink-0" />
            ) : (
              <AlertCircle className="w-5 h-5 text-rose-400 shrink-0" />
            )}
            <span>{mensaje.texto}</span>
          </div>
        )}

        <form onSubmit={handleGuardar} className="space-y-6">
          {/* Tarjeta de Información General */}
          <div className="bg-slate-900/90 border border-slate-800 rounded-xl p-5 shadow-sm space-y-4">
            <h3 className="text-sm font-bold uppercase tracking-wider text-slate-400 border-b border-slate-800 pb-2 flex items-center gap-2">
              <Package className="w-4 h-4 text-blue-400" />
              Datos Principales
            </h3>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <div>
                <label className="block text-xs font-semibold text-slate-400 mb-1.5">Clave (ID):</label>
                <input
                  type="text"
                  disabled
                  value={formData.Clave}
                  className="w-full bg-slate-950/80 border border-slate-800 rounded-lg px-3 py-2 text-sm text-slate-400 font-mono cursor-not-allowed"
                />
              </div>

              <div className="md:col-span-2">
                <label className="block text-xs font-semibold text-slate-300 mb-1.5">
                  Descripción del Producto:
                </label>
                <input
                  type="text"
                  required
                  value={formData.Descripcion}
                  onChange={e => handleChange('Descripcion', e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-sm text-white focus:outline-none transition"
                />
              </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 pt-2">
              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-1.5 flex items-center gap-1.5">
                  <Barcode className="w-4 h-4 text-slate-400" />
                  Código de Barras (Número exacto):
                </label>
                <input
                  type="text"
                  value={formData.cbarra || ''}
                  onChange={e => handleChange('cbarra', e.target.value)}
                  placeholder="Ej: 750123456789"
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-sm font-mono text-white focus:outline-none transition"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-1.5">
                  Unidad / Paquete:
                </label>
                <input
                  type="text"
                  value={formData.Paquete || 'PZA'}
                  onChange={e => handleChange('Paquete', e.target.value)}
                  placeholder="PZA, JGO, METRO, etc."
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-sm text-white focus:outline-none transition"
                />
              </div>
            </div>
          </div>

          {/* Tarjeta de Precios e Inventario */}
          <div className="bg-slate-900/90 border border-slate-800 rounded-xl p-5 shadow-sm space-y-4">
            <h3 className="text-sm font-bold uppercase tracking-wider text-slate-400 border-b border-slate-800 pb-2 flex items-center gap-2">
              <DollarSign className="w-4 h-4 text-emerald-400" />
              Precios y Existencias
            </h3>

            <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-5 gap-4">
              <div>
                <label className="block text-xs font-semibold text-emerald-400 mb-1.5">
                  Precio Menudeo ($):
                </label>
                <input
                  type="number"
                  step="0.01"
                  required
                  value={formData.Menudeo}
                  onChange={e => handleChange('Menudeo', e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-emerald-500 rounded-lg px-3 py-2 text-sm font-mono font-bold text-emerald-400 focus:outline-none transition"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-slate-300 mb-1.5">
                  Precio Mayoreo ($):
                </label>
                <input
                  type="number"
                  step="0.01"
                  value={formData.Mayoreo}
                  onChange={e => handleChange('Mayoreo', e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-sm font-mono text-white focus:outline-none transition"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-amber-300 mb-1.5">
                  Precio PME ($):
                </label>
                <input
                  type="number"
                  step="0.01"
                  value={formData.PME}
                  onChange={e => handleChange('PME', e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-amber-500 rounded-lg px-3 py-2 text-sm font-mono text-amber-300 focus:outline-none transition"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-purple-300 mb-1.5">
                  Precio Especial ($):
                </label>
                <input
                  type="number"
                  step="0.01"
                  value={formData.Especial}
                  onChange={e => handleChange('Especial', e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-purple-500 rounded-lg px-3 py-2 text-sm font-mono text-purple-300 focus:outline-none transition"
                />
              </div>

              <div>
                <label className="block text-xs font-semibold text-blue-400 mb-1.5">
                  Existencias (Stock):
                </label>
                <input
                  type="number"
                  value={formData.Existencias}
                  onChange={e => handleChange('Existencias', e.target.value)}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-lg px-3 py-2 text-sm font-mono font-bold text-white focus:outline-none transition"
                />
              </div>
            </div>
          </div>

          {/* Botones de Acción */}
          <div className="flex justify-end gap-3 pt-2">
            <button
              type="button"
              onClick={onCerrar}
              className="px-5 py-2.5 rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-300 text-sm font-semibold transition cursor-pointer"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={guardando}
              className="flex items-center gap-2 px-6 py-2.5 rounded-lg bg-emerald-600 hover:bg-emerald-500 text-white text-sm font-bold shadow-lg shadow-emerald-950 transition cursor-pointer disabled:opacity-50"
            >
              <Save className="w-4 h-4" />
              <span>{guardando ? 'Guardando...' : 'Guardar Cambios'}</span>
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
