import { useState, useEffect, useRef } from 'react';
import { Lock, Key, Eye, EyeOff, ShieldCheck, UserCheck, AlertTriangle, ArrowRight, X, Check } from 'lucide-react';
import { api, type Usuario } from './api';
import UserAvatar from './UserAvatar';

interface LoginModalProps {
  esObligatorio: boolean; // Si es el inicio de la app, no se puede cerrar sin autenticar
  usuarios: Usuario[];
  usuarioSeleccionadoInicial?: Usuario | null;
  onLoginExitoso: (user: Usuario) => void;
  onCancelar?: () => void;
}

export default function LoginModal({
  esObligatorio,
  usuarios,
  usuarioSeleccionadoInicial,
  onLoginExitoso,
  onCancelar
}: LoginModalProps) {
  // Filtrar solo usuarios activos
  const usuariosActivos = usuarios.filter(u => u.activo === 1);

  // Seleccionar por defecto Angel si existe, o el primer usuario
  const [usuarioElegido, setUsuarioElegido] = useState<Usuario | null>(() => {
    if (usuarioSeleccionadoInicial) return usuarioSeleccionadoInicial;
    const angel = usuariosActivos.find(u => u.Nombre.toLowerCase() === 'angel' || u.Id === 1);
    return angel || usuariosActivos[0] || null;
  });

  const [password, setPassword] = useState<string>('');
  const [mostrarPass, setMostrarPass] = useState<boolean>(false);
  const [cargando, setCargando] = useState<boolean>(false);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);
  const [loginExitoso, setLoginExitoso] = useState<Usuario | null>(null);

  const passInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    // Si no había usuario elegido y se cargan los usuarios, seleccionar Angel
    if (!usuarioElegido && usuariosActivos.length > 0) {
      const angel = usuariosActivos.find(u => u.Nombre.toLowerCase() === 'angel' || u.Id === 1);
      setUsuarioElegido(angel || usuariosActivos[0]);
    }
  }, [usuariosActivos, usuarioElegido]);

  useEffect(() => {
    // Enfocar input de contraseña
    passInputRef.current?.focus();
    setPassword('');
    setErrorMsg(null);
  }, [usuarioElegido]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!usuarioElegido) return;

    setErrorMsg(null);
    setCargando(true);

    try {
      const res = await api.loginUsuario(usuarioElegido.Id, password);
      if (res.success) {
        setLoginExitoso(res.usuario);
        setTimeout(() => {
          onLoginExitoso(res.usuario);
        }, 650);
      }
    } catch (err: any) {
      setErrorMsg(err.message || 'Contraseña incorrecta');
      passInputRef.current?.focus();
      passInputRef.current?.select();
    } finally {
      setCargando(false);
    }
  };

  // Pantalla de animación de bienvenida exitosa
  if (loginExitoso) {
    return (
      <div className="fixed inset-0 bg-black/85 backdrop-blur-md z-50 flex items-center justify-center p-4 select-none">
        <div className="bg-slate-900 border border-blue-500/40 rounded-3xl max-w-md w-full p-8 shadow-2xl flex flex-col items-center text-center gap-5 relative animate-pop-success shadow-blue-600/20">
          <div className="relative">
            <div className="absolute -inset-2 bg-gradient-to-r from-blue-600 to-emerald-500 rounded-2xl blur-md opacity-60 animate-pulse" />
            <div className="relative">
              <UserAvatar foto={loginExitoso.Foto} nombre={loginExitoso.Nombre} size="xl" className="border-2 border-white/60 shadow-xl" />
              <div className="absolute -bottom-1.5 -right-1.5 bg-emerald-500 text-white p-1 rounded-full border-2 border-slate-900 shadow">
                <Check className="w-4 h-4 stroke-[3]" />
              </div>
            </div>
          </div>

          <div>
            <span className="text-[11px] font-bold tracking-widest text-emerald-400 uppercase font-mono bg-emerald-500/10 border border-emerald-500/30 px-2.5 py-1 rounded-full">
              ¡Acceso Concedido!
            </span>
            <h2 className="text-2xl font-black text-white mt-2 tracking-wide">
              {loginExitoso.Nombre}
            </h2>
            <p className="text-xs text-slate-400 mt-1">
              Iniciando entorno de {loginExitoso.Admin === 1 ? 'Administración' : 'Caja y Mostrador'}...
            </p>
          </div>

          <div className="w-full bg-slate-950 h-1.5 rounded-full overflow-hidden border border-slate-800 mt-1">
            <div className="h-full bg-gradient-to-r from-blue-500 to-emerald-400 animate-progress-fill rounded-full" />
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="fixed inset-0 bg-black/85 backdrop-blur-md z-50 flex items-center justify-center p-4 select-none">
      <div className="bg-slate-900 border border-slate-800 rounded-3xl max-w-lg w-full p-6 sm:p-8 shadow-2xl flex flex-col gap-6 relative">
        {/* Botón cerrar sólo si no es inicio obligatorio */}
        {!esObligatorio && onCancelar && (
          <button
            onClick={onCancelar}
            className="absolute top-5 right-5 p-1.5 text-slate-400 hover:text-white rounded-xl hover:bg-slate-800 cursor-pointer transition"
            title="Cancelar"
          >
            <X className="w-5 h-5" />
          </button>
        )}

        {/* Encabezado con Logo */}
        <div className="flex flex-col items-center text-center gap-2">
          <div className="flex items-center gap-3">
            <img src="/logo.png" alt="SICDIN" className="w-10 h-10 object-contain drop-shadow" />
            <h1 className="text-xl font-extrabold text-white tracking-wider font-mono">SICDIN 4.0</h1>
          </div>
          <p className="text-xs text-slate-400 font-medium">
            {esObligatorio ? 'Bienvenido. Selecciona tu usuario e ingresa tu contraseña' : 'Cambiar Sesión de Usuario'}
          </p>
        </div>

        {/* Error Alert */}
        {errorMsg && (
          <div className="p-3 bg-rose-500/10 border border-rose-500/30 rounded-xl text-xs text-rose-300 flex items-center gap-2.5 animate-shake">
            <AlertTriangle className="w-4 h-4 shrink-0 text-rose-400" />
            <span>{errorMsg}</span>
          </div>
        )}

        {/* Selección de Usuario (Tarjetas visuales con Foto) */}
        <div>
          <label className="block text-[11px] font-bold uppercase text-slate-400 mb-2 tracking-wider">
            Usuarios Disponibles
          </label>
          <div className="grid grid-cols-2 sm:grid-cols-3 gap-2.5 max-h-48 overflow-y-auto p-1">
            {usuariosActivos.map(u => {
              const seleccionado = usuarioElegido?.Id === u.Id;
              const esAdmin = u.Admin === 1;

              return (
                <button
                  key={u.Id}
                  type="button"
                  onClick={() => setUsuarioElegido(u)}
                  className={`flex items-center gap-2.5 p-2 rounded-2xl border text-left cursor-pointer transition ${
                    seleccionado
                      ? 'bg-blue-600/20 border-blue-500 ring-2 ring-blue-500/30 shadow-md'
                      : 'bg-slate-950/70 border-slate-800 hover:border-slate-700 hover:bg-slate-850'
                  }`}
                >
                  <UserAvatar foto={u.Foto} nombre={u.Nombre} size="sm" />
                  <div className="min-w-0 flex-1">
                    <div className="text-xs font-bold text-white truncate">{u.Nombre}</div>
                    <div className="text-[10px] text-slate-400 flex items-center gap-1">
                      {esAdmin ? (
                        <span className="text-amber-400 font-semibold flex items-center gap-0.5">
                          <ShieldCheck className="w-2.5 h-2.5" />
                          Admin
                        </span>
                      ) : (
                        <span className="text-blue-400 font-semibold flex items-center gap-0.5">
                          <UserCheck className="w-2.5 h-2.5" />
                          Cajero
                        </span>
                      )}
                    </div>
                  </div>
                </button>
              );
            })}
          </div>
        </div>

        {/* Perfil Seleccionado y Formulario de Contraseña */}
        {usuarioElegido && (
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="flex items-center justify-between p-3 bg-slate-950 rounded-2xl border border-slate-800/80">
              <div className="flex items-center gap-3">
                <UserAvatar foto={usuarioElegido.Foto} nombre={usuarioElegido.Nombre} size="md" />
                <div>
                  <div className="text-sm font-bold text-white flex items-center gap-2">
                    <span>{usuarioElegido.Nombre}</span>
                    <span className="text-[10px] bg-slate-800 text-slate-300 px-1.5 py-0.2 rounded font-mono font-bold">
                      #{usuarioElegido.Id}
                    </span>
                  </div>
                  <div className="text-[11px] text-slate-400">
                    {usuarioElegido.Admin === 1 ? 'Acceso Administrador' : 'Acceso Cajero / Ventas'}
                  </div>
                </div>
              </div>

              <div className="p-2 bg-blue-500/10 text-blue-400 rounded-xl border border-blue-500/20">
                <Lock className="w-4 h-4" />
              </div>
            </div>

            <div>
              <label className="block text-[11px] font-bold uppercase text-slate-300 mb-1.5 tracking-wider">
                Ingresa tu Contraseña
              </label>
              <div className="relative">
                <div className="absolute left-3.5 top-3 text-slate-500">
                  <Key className="w-4 h-4" />
                </div>
                <input
                  ref={passInputRef}
                  type={mostrarPass ? 'text' : 'password'}
                  required
                  value={password}
                  onChange={e => setPassword(e.target.value)}
                  placeholder={`Contraseña de ${usuarioElegido.Nombre}...`}
                  className="w-full bg-slate-950 border border-slate-700 focus:border-blue-500 rounded-xl pl-10 pr-10 py-2.5 text-sm text-white focus:outline-none placeholder-slate-500 transition shadow-inner font-mono"
                />
                <button
                  type="button"
                  onClick={() => setMostrarPass(prev => !prev)}
                  className="absolute right-3 top-3 text-slate-500 hover:text-slate-300 cursor-pointer"
                  title={mostrarPass ? 'Ocultar' : 'Mostrar'}
                >
                  {mostrarPass ? <EyeOff className="w-4 h-4" /> : <Eye className="w-4 h-4" />}
                </button>
              </div>
            </div>

            <div className="flex items-center justify-end gap-2.5 pt-2">
              {!esObligatorio && onCancelar && (
                <button
                  type="button"
                  onClick={onCancelar}
                  className="px-4 py-2.5 bg-slate-800 hover:bg-slate-700 text-slate-300 rounded-xl text-xs font-semibold cursor-pointer transition"
                >
                  Cancelar
                </button>
              )}

              <button
                type="submit"
                disabled={cargando}
                className="flex-1 sm:flex-none flex items-center justify-center gap-2 px-6 py-2.5 bg-blue-600 hover:bg-blue-500 text-white rounded-xl text-xs font-bold shadow-lg shadow-blue-600/30 cursor-pointer transition border border-blue-400/30"
              >
                <span>{cargando ? 'Verificando...' : 'Iniciar Sesión'}</span>
                <ArrowRight className="w-4 h-4" />
              </button>
            </div>
          </form>
        )}
      </div>
    </div>
  );
}
