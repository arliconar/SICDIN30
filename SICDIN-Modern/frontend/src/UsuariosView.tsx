import { useState, useEffect, useRef } from 'react';
import {
  Users,
  UserCheck,
  UserX,
  UserPlus,
  Shield,
  ShieldCheck,
  ShieldAlert,
  Key,
  Lock,
  Eye,
  EyeOff,
  Phone,
  Clock,
  Edit3,
  Trash2,
  Search,
  Check,
  X,
  RefreshCw,
  AlertTriangle,
  Upload,
  Image as ImageIcon,
  User as UserIcon
} from 'lucide-react';
import { api, type Usuario } from './api';
import UserAvatar, { AVATAR_PRESETS } from './UserAvatar';

interface UsuariosViewProps {
  esAdmin: boolean;
  onUsuarioActualizado?: () => void;
}

export default function UsuariosView({ esAdmin, onUsuarioActualizado }: UsuariosViewProps) {
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const [cargando, setCargando] = useState<boolean>(true);
  const [errorCarga, setErrorCarga] = useState<string | null>(null);
  const [query, setQuery] = useState<string>('');
  const [filtroRol, setFiltroRol] = useState<'TODOS' | 'ADMIN' | 'USUARIO' | 'ACTIVOS' | 'INACTIVOS'>('TODOS');

  // Modal Crear / Editar
  const [modalAbierto, setModalAbierto] = useState<boolean>(false);
  const [usuarioEditando, setUsuarioEditando] = useState<Usuario | null>(null);

  // Form State
  const [formNombre, setFormNombre] = useState<string>('');
  const [formPass, setFormPass] = useState<string>('');
  const [formPassConfirm, setFormPassConfirm] = useState<string>('');
  const [formAdmin, setFormAdmin] = useState<boolean>(false);
  const [formActivo, setFormActivo] = useState<boolean>(true);
  const [formTelefono, setFormTelefono] = useState<string>('');
  const [formFoto, setFormFoto] = useState<string>('Hombre');
  const [modoAvatar, setModoAvatar] = useState<'PRESET' | 'SUBIR'>('PRESET');
  const [fotoSubidaBase64, setFotoSubidaBase64] = useState<string | null>(null);
  const [nombreArchivoSubido, setNombreArchivoSubido] = useState<string>('');
  const [mostrarPass, setMostrarPass] = useState<boolean>(false);
  const [guardando, setGuardando] = useState<boolean>(false);
  const [errorForm, setErrorForm] = useState<string | null>(null);

  const fileInputRef = useRef<HTMLInputElement>(null);

  // Modal Confirmación Eliminar
  const [usuarioAEliminar, setUsuarioAEliminar] = useState<Usuario | null>(null);
  const [eliminando, setEliminando] = useState<boolean>(false);

  useEffect(() => {
    if (esAdmin) {
      cargarUsuarios();
    }
  }, [esAdmin]);

  const cargarUsuarios = async () => {
    setCargando(true);
    setErrorCarga(null);
    try {
      const data = await api.listarUsuarios();
      setUsuarios(Array.isArray(data) ? data : []);
    } catch (err: any) {
      console.error('Error cargando usuarios:', err);
      setErrorCarga(err.message || 'Error al conectar con el servidor');
    } finally {
      setCargando(false);
    }
  };

  const abrirNuevoUsuario = () => {
    setUsuarioEditando(null);
    setFormNombre('');
    setFormPass('');
    setFormPassConfirm('');
    setFormAdmin(false);
    setFormActivo(true);
    setFormTelefono('');
    setFormFoto('Hombre');
    setModoAvatar('PRESET');
    setFotoSubidaBase64(null);
    setNombreArchivoSubido('');
    setMostrarPass(false);
    setErrorForm(null);
    setModalAbierto(true);
  };

  const abrirEditarUsuario = (user: Usuario) => {
    setUsuarioEditando(user);
    setFormNombre(user.Nombre);
    setFormPass(user.Pass);
    setFormPassConfirm(user.Pass);
    setFormAdmin(user.Admin === 1);
    setFormActivo(user.activo === 1);
    setFormTelefono(user.telefono || '');
    setFormFoto(user.Foto || 'Hombre');

    const esFotoArchivo = user.Foto && (
      user.Foto.startsWith('/uploads/') ||
      user.Foto.includes('\\') ||
      user.Foto.includes('/') ||
      user.Foto.startsWith('http') ||
      user.Foto.startsWith('data:')
    );

    setModoAvatar(esFotoArchivo ? 'SUBIR' : 'PRESET');
    setFotoSubidaBase64(null);
    setNombreArchivoSubido('');
    setMostrarPass(false);
    setErrorForm(null);
    setModalAbierto(true);
  };

  const handleArchivoFoto = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    if (!file.type.startsWith('image/')) {
      setErrorForm('Por favor selecciona un archivo de imagen válido (PNG, JPG, etc.)');
      return;
    }

    setNombreArchivoSubido(file.name);
    const reader = new FileReader();
    reader.onload = () => {
      const base64 = reader.result as string;
      setFotoSubidaBase64(base64);
    };
    reader.readAsDataURL(file);
  };

  const guardarUsuario = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrorForm(null);

    const nom = formNombre.trim();
    if (!nom) {
      setErrorForm('El nombre de usuario es obligatorio');
      return;
    }

    if (!usuarioEditando && !formPass.trim()) {
      setErrorForm('La contraseña es obligatoria para un usuario nuevo');
      return;
    }

    if (formPass !== formPassConfirm) {
      setErrorForm('Las contraseñas no coinciden');
      return;
    }

    setGuardando(true);
    try {
      let fotoFinal = formFoto;

      // Si subió una foto propia nueva, subirla y guardarla en carpeta local de backend
      if (modoAvatar === 'SUBIR' && fotoSubidaBase64) {
        const uploadRes = await api.subirFotoUsuario(fotoSubidaBase64, usuarioEditando?.Id);
        fotoFinal = uploadRes.url;
      }

      if (usuarioEditando) {
        await api.actualizarUsuario(usuarioEditando.Id, {
          Nombre: nom,
          Pass: formPass.trim(),
          Admin: formAdmin ? 1 : 0,
          activo: formActivo ? 1 : 0,
          telefono: formTelefono.trim(),
          Foto: fotoFinal
        });
      } else {
        await api.crearUsuario({
          Nombre: nom,
          Pass: formPass.trim(),
          Admin: formAdmin ? 1 : 0,
          activo: formActivo ? 1 : 0,
          telefono: formTelefono.trim(),
          Foto: fotoFinal
        });
      }

      setModalAbierto(false);
      await cargarUsuarios();
      if (onUsuarioActualizado) onUsuarioActualizado();
    } catch (err: any) {
      setErrorForm(err.message || 'Error guardando usuario');
    } finally {
      setGuardando(false);
    }
  };

  const toggleEstadoActivo = async (user: Usuario) => {
    try {
      const nuevoEstado = user.activo === 1 ? 0 : 1;
      await api.actualizarUsuario(user.Id, { activo: nuevoEstado });
      setUsuarios(prev => prev.map(u => u.Id === user.Id ? { ...u, activo: nuevoEstado } : u));
      if (onUsuarioActualizado) onUsuarioActualizado();
    } catch (err: any) {
      alert(`Error al cambiar estado: ${err.message}`);
    }
  };

  const confirmarEliminar = async () => {
    if (!usuarioAEliminar) return;
    setEliminando(true);
    try {
      await api.eliminarUsuario(usuarioAEliminar.Id);
      setUsuarioAEliminar(null);
      await cargarUsuarios();
      if (onUsuarioActualizado) onUsuarioActualizado();
    } catch (err: any) {
      alert(`Error: ${err.message}`);
    } finally {
      setEliminando(false);
    }
  };

  // Si no es Administrador, bloquear la vista con advertencia de seguridad
  if (!esAdmin) {
    return (
      <div className="flex-1 flex flex-col items-center justify-center p-6 bg-slate-950 text-slate-100 h-full">
        <div className="bg-slate-900 border border-rose-500/30 rounded-2xl p-8 max-w-md text-center shadow-2xl flex flex-col items-center gap-4">
          <div className="p-4 bg-rose-500/10 text-rose-400 rounded-full border border-rose-500/20">
            <ShieldAlert className="w-12 h-12" />
          </div>
          <h2 className="text-xl font-bold text-white">Acceso Restringido</h2>
          <p className="text-sm text-slate-400 leading-relaxed">
            El módulo de <strong>Control de Usuarios</strong> está reservado exclusivamente para perfiles con privilegios de <strong>Administrador</strong>.
          </p>
          <div className="p-3 bg-slate-950 rounded-xl border border-slate-800 text-xs text-slate-400 flex items-center gap-2">
            <Lock className="w-4 h-4 text-amber-400 shrink-0" />
            <span>Cambie el selector de perfil a <strong>Admin</strong> en la barra superior para desbloquear este panel.</span>
          </div>
        </div>
      </div>
    );
  }

  // Filtrado seguro de usuarios
  const listaSegura = Array.isArray(usuarios) ? usuarios : [];
  const usuariosFiltrados = listaSegura.filter(u => {
    const q = query.toLowerCase().trim();
    const coincideTexto = !q || (
      u.Nombre.toLowerCase().includes(q) ||
      String(u.Id).includes(q) ||
      (u.telefono && u.telefono.toLowerCase().includes(q))
    );

    if (!coincideTexto) return false;

    if (filtroRol === 'ADMIN') return u.Admin === 1;
    if (filtroRol === 'USUARIO') return u.Admin === 0;
    if (filtroRol === 'ACTIVOS') return u.activo === 1;
    if (filtroRol === 'INACTIVOS') return u.activo === 0;
    return true;
  });

  const conteoActivos = listaSegura.filter(u => u.activo === 1).length;
  const conteoAdmins = listaSegura.filter(u => u.Admin === 1).length;

  return (
    <div className="flex-1 flex flex-col h-full bg-slate-950 text-slate-100 overflow-hidden select-none">
      {/* Cabecera */}
      <div className="p-4 bg-slate-900/60 border-b border-slate-800 flex items-center justify-between">
        <div className="flex items-center gap-3">
          <div className="p-2 bg-blue-500/10 text-blue-400 rounded-lg border border-blue-500/20">
            <ShieldCheck className="w-5 h-5 text-amber-400" />
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h2 className="text-lg font-bold text-white tracking-wide">Control de Usuarios</h2>
              <span className="text-[10px] bg-amber-500/20 text-amber-300 border border-amber-500/30 px-2 py-0.5 rounded font-mono font-bold uppercase tracking-wider">
                Exclusivo Admin
              </span>
            </div>
            <p className="text-xs text-slate-400">Creación, edición, contraseñas, fotos personalizadas y permisos de acceso</p>
          </div>
        </div>

        <div className="flex items-center gap-3">
          <button
            onClick={cargarUsuarios}
            disabled={cargando}
            className="p-2 bg-slate-800 hover:bg-slate-700 text-slate-300 rounded-xl border border-slate-700 cursor-pointer transition"
            title="Refrescar lista"
          >
            <RefreshCw className={`w-4 h-4 ${cargando ? 'animate-spin text-blue-400' : ''}`} />
          </button>

          <button
            onClick={abrirNuevoUsuario}
            className="flex items-center gap-2 bg-blue-600 hover:bg-blue-500 text-white px-4 py-2 rounded-xl text-xs font-bold shadow-lg shadow-blue-600/20 cursor-pointer transition border border-blue-400/30"
          >
            <UserPlus className="w-4 h-4" />
            <span>Nuevo Usuario</span>
          </button>
        </div>
      </div>

      {/* Barra de Filtros y Búsqueda */}
      <div className="p-3 bg-slate-900/30 border-b border-slate-800/80 flex flex-wrap items-center justify-between gap-3">
        {/* Buscador */}
        <div className="relative flex-1 min-w-[260px] max-w-md">
          <Search className="absolute left-3.5 top-2.5 text-slate-500 w-4 h-4" />
          <input
            type="text"
            value={query}
            onChange={e => setQuery(e.target.value)}
            placeholder="Buscar por nombre, ID o teléfono..."
            className="w-full bg-slate-900 border border-slate-700 focus:border-blue-500 rounded-xl pl-9 pr-4 py-2 text-xs text-white focus:outline-none placeholder-slate-500 transition"
          />
        </div>

        {/* Chips de filtro */}
        <div className="flex items-center gap-1.5 text-xs">
          <button
            onClick={() => setFiltroRol('TODOS')}
            className={`px-3 py-1.5 rounded-lg font-semibold cursor-pointer transition ${
              filtroRol === 'TODOS'
                ? 'bg-blue-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            Todos ({usuarios.length})
          </button>
          <button
            onClick={() => setFiltroRol('ADMIN')}
            className={`px-3 py-1.5 rounded-lg font-semibold cursor-pointer transition flex items-center gap-1.5 ${
              filtroRol === 'ADMIN'
                ? 'bg-amber-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            <Shield className="w-3.5 h-3.5 text-amber-300" />
            <span>Admins ({conteoAdmins})</span>
          </button>
          <button
            onClick={() => setFiltroRol('USUARIO')}
            className={`px-3 py-1.5 rounded-lg font-semibold cursor-pointer transition flex items-center gap-1.5 ${
              filtroRol === 'USUARIO'
                ? 'bg-blue-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            <UserIcon className="w-3.5 h-3.5 text-blue-300" />
            <span>Cajeros ({usuarios.length - conteoAdmins})</span>
          </button>
          <button
            onClick={() => setFiltroRol('ACTIVOS')}
            className={`px-3 py-1.5 rounded-lg font-semibold cursor-pointer transition flex items-center gap-1.5 ${
              filtroRol === 'ACTIVOS'
                ? 'bg-emerald-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            <UserCheck className="w-3.5 h-3.5 text-emerald-300" />
            <span>Activos ({conteoActivos})</span>
          </button>
          <button
            onClick={() => setFiltroRol('INACTIVOS')}
            className={`px-3 py-1.5 rounded-lg font-semibold cursor-pointer transition flex items-center gap-1.5 ${
              filtroRol === 'INACTIVOS'
                ? 'bg-rose-600 text-white shadow-sm'
                : 'bg-slate-800 text-slate-400 hover:text-white'
            }`}
          >
            <UserX className="w-3.5 h-3.5 text-rose-300" />
            <span>Inactivos ({usuarios.length - conteoActivos})</span>
          </button>
        </div>
      </div>

      {/* Lista de Usuarios */}
      <div className="flex-1 overflow-y-auto p-4">
        {errorCarga ? (
          <div className="h-64 flex flex-col items-center justify-center gap-3 text-rose-400">
            <AlertTriangle className="w-10 h-10 text-rose-500" />
            <p className="text-sm font-semibold">{errorCarga}</p>
            <button
              onClick={cargarUsuarios}
              className="px-4 py-2 bg-slate-800 hover:bg-slate-700 text-white rounded-xl text-xs font-bold border border-slate-700 cursor-pointer flex items-center gap-2 transition"
            >
              <RefreshCw className="w-4 h-4" />
              <span>Reintentar Conexión</span>
            </button>
          </div>
        ) : cargando ? (
          <div className="h-64 flex flex-col items-center justify-center gap-3 text-slate-400">
            <RefreshCw className="w-8 h-8 animate-spin text-blue-400" />
            <span className="text-sm">Cargando directorio de usuarios...</span>
          </div>
        ) : usuariosFiltrados.length === 0 ? (
          <div className="h-64 flex flex-col items-center justify-center gap-2 text-slate-500">
            <Users className="w-10 h-10 opacity-30" />
            <p className="text-sm">No se encontraron usuarios con el filtro seleccionado</p>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-3">
            {usuariosFiltrados.map(user => {
              const esActivo = user.activo === 1;
              const esAdministrador = user.Admin === 1;

              return (
                <div
                  key={user.Id}
                  className={`bg-slate-900 border rounded-2xl p-4 transition shadow-sm hover:shadow-md flex flex-col justify-between ${
                    !esActivo
                      ? 'border-rose-900/50 bg-rose-950/10 opacity-75'
                      : esAdministrador
                      ? 'border-amber-500/30 hover:border-amber-500/50'
                      : 'border-slate-800 hover:border-slate-700'
                  }`}
                >
                  <div>
                    {/* Fila superior con Avatar, Nombre y Badges */}
                    <div className="flex items-start justify-between gap-3 mb-3">
                      <div className="flex items-center gap-3 min-w-0">
                        <UserAvatar foto={user.Foto} nombre={user.Nombre} size="lg" />
                        <div className="min-w-0">
                          <div className="flex items-center gap-2">
                            <h3 className="text-base font-bold text-white tracking-wide truncate">{user.Nombre}</h3>
                            <span className="text-[10px] text-slate-400 font-mono font-bold bg-slate-800 px-1.5 py-0.5 rounded shrink-0">
                              #{user.Id}
                            </span>
                          </div>
                          <div className="text-xs text-slate-400 flex items-center gap-1.5 mt-0.5">
                            {esAdministrador ? (
                              <span className="text-amber-400 font-semibold flex items-center gap-1">
                                <Shield className="w-3 h-3" />
                                Administrador
                              </span>
                            ) : (
                              <span className="text-blue-400 font-semibold flex items-center gap-1">
                                <UserIcon className="w-3 h-3" />
                                Usuario / Cajero
                              </span>
                            )}
                          </div>
                        </div>
                      </div>

                      {/* Badge Activo / Inactivo */}
                      {esActivo ? (
                        <span className="text-[10px] bg-emerald-500/10 text-emerald-400 border border-emerald-500/30 px-2 py-0.5 rounded-full font-bold flex items-center gap-1 shrink-0">
                          <Check className="w-3 h-3" />
                          Activo
                        </span>
                      ) : (
                        <span className="text-[10px] bg-rose-500/10 text-rose-400 border border-rose-500/30 px-2 py-0.5 rounded-full font-bold flex items-center gap-1 shrink-0">
                          <X className="w-3 h-3" />
                          Inactivo
                        </span>
                      )}
                    </div>

                    {/* Datos complementarios */}
                    <div className="bg-slate-950/70 p-2.5 rounded-xl border border-slate-800/80 space-y-1.5 text-xs text-slate-300 font-mono mb-3">
                      <div className="flex items-center justify-between">
                        <span className="text-slate-500 text-[11px] flex items-center gap-1">
                          <Key className="w-3 h-3 text-slate-400" /> Contraseña:
                        </span>
                        <span className="text-amber-300 font-mono tracking-wider font-bold">
                          ••••••••
                        </span>
                      </div>

                      {user.telefono && (
                        <div className="flex items-center justify-between">
                          <span className="text-slate-500 text-[11px] flex items-center gap-1">
                            <Phone className="w-3 h-3 text-slate-400" /> Teléfono:
                          </span>
                          <span className="text-slate-300 font-sans">{user.telefono}</span>
                        </div>
                      )}

                      <div className="flex items-center justify-between">
                        <span className="text-slate-500 text-[11px] flex items-center gap-1">
                          <Clock className="w-3 h-3 text-slate-400" /> Último acceso:
                        </span>
                        <span className="text-slate-400 text-[11px]">{user.Fu || 'Sin registros'}</span>
                      </div>
                    </div>
                  </div>

                  {/* Acciones */}
                  <div className="flex items-center gap-2 pt-2 border-t border-slate-800/80">
                    <button
                      onClick={() => toggleEstadoActivo(user)}
                      className={`flex-1 py-1.5 px-2 rounded-lg text-xs font-semibold border transition cursor-pointer flex items-center justify-center gap-1.5 ${
                        esActivo
                          ? 'bg-rose-500/10 hover:bg-rose-600 text-rose-300 hover:text-white border-rose-500/30'
                          : 'bg-emerald-500/10 hover:bg-emerald-600 text-emerald-300 hover:text-white border-emerald-500/30'
                      }`}
                      title={esActivo ? 'Inhabilitar inicio de sesión' : 'Habilitar usuario'}
                    >
                      {esActivo ? <UserX className="w-3.5 h-3.5" /> : <UserCheck className="w-3.5 h-3.5" />}
                      <span>{esActivo ? 'Desactivar' : 'Activar'}</span>
                    </button>

                    <button
                      onClick={() => abrirEditarUsuario(user)}
                      className="py-1.5 px-3 rounded-lg text-xs font-semibold bg-slate-800 hover:bg-slate-700 text-slate-200 border border-slate-700 transition cursor-pointer flex items-center gap-1"
                      title="Editar usuario"
                    >
                      <Edit3 className="w-3.5 h-3.5 text-blue-400" />
                      <span>Editar</span>
                    </button>

                    <button
                      onClick={() => setUsuarioAEliminar(user)}
                      className="p-1.5 rounded-lg text-xs font-semibold bg-rose-600/10 hover:bg-rose-600 text-rose-400 hover:text-white border border-rose-500/30 transition cursor-pointer"
                      title="Eliminar usuario"
                    >
                      <Trash2 className="w-3.5 h-3.5" />
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>

      {/* 
        =======================================================
        MODAL CREAR / EDITAR USUARIO (CON SUBIDA DE FOTO PROPIA)
        =======================================================
      */}
      {modalAbierto && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm z-50 flex items-center justify-center p-4">
          <div className="bg-slate-900 border border-slate-800 rounded-2xl max-w-lg w-full p-6 shadow-2xl flex flex-col max-h-[95vh] overflow-y-auto">
            {/* Header del Modal */}
            <div className="flex items-center justify-between border-b border-slate-800 pb-3 mb-4">
              <div className="flex items-center gap-2.5">
                <div className="p-2 bg-blue-500/10 text-blue-400 rounded-lg border border-blue-500/20">
                  {usuarioEditando ? <Edit3 className="w-5 h-5" /> : <UserPlus className="w-5 h-5" />}
                </div>
                <div>
                  <h3 className="text-base font-bold text-white">
                    {usuarioEditando ? `Editar Usuario: ${usuarioEditando.Nombre}` : 'Crear Nuevo Usuario'}
                  </h3>
                  <p className="text-xs text-slate-400">
                    {usuarioEditando ? `Modificando ID #${usuarioEditando.Id}` : 'Asignación de credenciales, foto y rol de acceso'}
                  </p>
                </div>
              </div>
              <button
                onClick={() => setModalAbierto(false)}
                className="p-1 text-slate-400 hover:text-white rounded hover:bg-slate-800 cursor-pointer"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {/* Error Message */}
            {errorForm && (
              <div className="mb-4 p-3 bg-rose-500/10 border border-rose-500/30 rounded-xl text-xs text-rose-300 flex items-center gap-2">
                <AlertTriangle className="w-4 h-4 shrink-0 text-rose-400" />
                <span>{errorForm}</span>
              </div>
            )}

            {/* Formulario */}
            <form onSubmit={guardarUsuario} className="space-y-4">
              {/* Nombre de Usuario */}
              <div>
                <label className="block text-xs font-bold uppercase text-slate-300 mb-1">
                  Nombre de Usuario <span className="text-rose-400">*</span>
                </label>
                <input
                  type="text"
                  required
                  value={formNombre}
                  onChange={e => setFormNombre(e.target.value)}
                  placeholder="Ej. Angel, Papa, Carlos..."
                  className="w-full bg-slate-950 border border-slate-700 rounded-xl px-3 py-2 text-sm text-white focus:outline-none focus:border-blue-500"
                />
              </div>

              {/* Contraseña y Confirmación */}
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                <div>
                  <label className="block text-xs font-bold uppercase text-slate-300 mb-1">
                    {usuarioEditando ? 'Contraseña (dejar igual o cambiar)' : 'Contraseña *'}
                  </label>
                  <div className="relative">
                    <input
                      type={mostrarPass ? 'text' : 'password'}
                      required={!usuarioEditando}
                      value={formPass}
                      onChange={e => setFormPass(e.target.value)}
                      placeholder="Contraseña"
                      className="w-full bg-slate-950 border border-slate-700 rounded-xl px-3 py-2 pr-9 text-sm text-white focus:outline-none focus:border-blue-500"
                    />
                    <button
                      type="button"
                      onClick={() => setMostrarPass(prev => !prev)}
                      className="absolute right-2.5 top-2.5 text-slate-500 hover:text-slate-300 cursor-pointer"
                      title={mostrarPass ? 'Ocultar' : 'Mostrar'}
                    >
                      {mostrarPass ? <EyeOff className="w-4 h-4" /> : <Eye className="w-4 h-4" />}
                    </button>
                  </div>
                </div>

                <div>
                  <label className="block text-xs font-bold uppercase text-slate-300 mb-1">
                    Confirmar Contraseña
                  </label>
                  <input
                    type={mostrarPass ? 'text' : 'password'}
                    value={formPassConfirm}
                    onChange={e => setFormPassConfirm(e.target.value)}
                    placeholder="Repite contraseña"
                    className={`w-full bg-slate-950 border rounded-xl px-3 py-2 text-sm text-white focus:outline-none ${
                      formPass && formPassConfirm && formPass !== formPassConfirm
                        ? 'border-rose-500 focus:border-rose-500'
                        : 'border-slate-700 focus:border-blue-500'
                    }`}
                  />
                  {formPass && formPassConfirm && formPass !== formPassConfirm && (
                    <span className="text-[10px] text-rose-400 mt-1 block">No coinciden</span>
                  )}
                </div>
              </div>

              {/* Teléfono */}
              <div>
                <label className="block text-xs font-bold uppercase text-slate-300 mb-1">
                  Teléfono / Móvil (Opcional)
                </label>
                <input
                  type="text"
                  value={formTelefono}
                  onChange={e => setFormTelefono(e.target.value)}
                  placeholder="Ej. 7711234567"
                  className="w-full bg-slate-950 border border-slate-700 rounded-xl px-3 py-2 text-sm text-white focus:outline-none focus:border-blue-500"
                />
              </div>

              {/* Rol: Administrador vs Cajero */}
              <div>
                <label className="block text-xs font-bold uppercase text-slate-300 mb-1.5">
                  Rol y Permisos
                </label>
                <div className="grid grid-cols-2 gap-3">
                  <div
                    onClick={() => setFormAdmin(true)}
                    className={`p-3 rounded-xl border cursor-pointer transition flex items-start gap-2.5 ${
                      formAdmin
                        ? 'bg-amber-500/10 border-amber-500/50 text-white'
                        : 'bg-slate-950 border-slate-800 text-slate-400 hover:border-slate-700'
                    }`}
                  >
                    <Shield className={`w-5 h-5 shrink-0 ${formAdmin ? 'text-amber-400' : 'text-slate-500'}`} />
                    <div>
                      <div className="text-xs font-bold text-white">Administrador</div>
                      <div className="text-[10px] text-slate-400 leading-tight mt-0.5">
                        Acceso total, usuarios, precios y adeudos
                      </div>
                    </div>
                  </div>

                  <div
                    onClick={() => setFormAdmin(false)}
                    className={`p-3 rounded-xl border cursor-pointer transition flex items-start gap-2.5 ${
                      !formAdmin
                        ? 'bg-blue-500/10 border-blue-500/50 text-white'
                        : 'bg-slate-950 border-slate-800 text-slate-400 hover:border-slate-700'
                    }`}
                  >
                    <UserIcon className={`w-5 h-5 shrink-0 ${!formAdmin ? 'text-blue-400' : 'text-slate-500'}`} />
                    <div>
                      <div className="text-xs font-bold text-white">Usuario / Cajero</div>
                      <div className="text-[10px] text-slate-400 leading-tight mt-0.5">
                        Ventas mostrador, catálogo y libreta
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              {/* 
                =======================================================
                SELECTOR DE FOTO / AVATAR: SUBIR IMAGEN O ELEGIR ICONO
                =======================================================
              */}
              <div className="bg-slate-950 p-3.5 rounded-xl border border-slate-800 space-y-3">
                <div className="flex items-center justify-between">
                  <label className="text-xs font-bold uppercase text-slate-300">
                    Foto o Avatar de Perfil
                  </label>
                  {/* Selector de Modo */}
                  <div className="flex items-center bg-slate-900 p-0.5 rounded-lg border border-slate-800 text-xs">
                    <button
                      type="button"
                      onClick={() => setModoAvatar('SUBIR')}
                      className={`px-2.5 py-1 rounded-md font-bold transition flex items-center gap-1 cursor-pointer ${
                        modoAvatar === 'SUBIR'
                          ? 'bg-blue-600 text-white shadow-xs'
                          : 'text-slate-400 hover:text-slate-200'
                      }`}
                    >
                      <Upload className="w-3 h-3" />
                      <span>Subir Foto Propia</span>
                    </button>
                    <button
                      type="button"
                      onClick={() => setModoAvatar('PRESET')}
                      className={`px-2.5 py-1 rounded-md font-bold transition flex items-center gap-1 cursor-pointer ${
                        modoAvatar === 'PRESET'
                          ? 'bg-blue-600 text-white shadow-xs'
                          : 'text-slate-400 hover:text-slate-200'
                      }`}
                    >
                      <ImageIcon className="w-3 h-3" />
                      <span>Elegir Icono</span>
                    </button>
                  </div>
                </div>

                {modoAvatar === 'SUBIR' ? (
                  <div className="space-y-3">
                    <div className="flex items-center gap-4">
                      {/* Previsualización */}
                      <div className="w-16 h-16 rounded-2xl border-2 border-dashed border-blue-500/40 bg-slate-900 flex items-center justify-center overflow-hidden shrink-0 shadow-inner">
                        {fotoSubidaBase64 ? (
                          <img src={fotoSubidaBase64} alt="Preview" className="w-full h-full object-cover" />
                        ) : formFoto && formFoto !== 'Hombre' && formFoto !== 'Mujer' && formFoto !== 'Carro' && formFoto !== 'Bicicleta' && formFoto !== 'Flor' ? (
                          <UserAvatar foto={formFoto} nombre={formNombre} size="xl" className="w-full h-full rounded-none" />
                        ) : (
                          <Upload className="w-6 h-6 text-slate-500" />
                        )}
                      </div>

                      {/* Botón de carga y descripción */}
                      <div className="flex-1">
                        <input
                          ref={fileInputRef}
                          type="file"
                          accept="image/*"
                          onChange={handleArchivoFoto}
                          className="hidden"
                        />
                        <button
                          type="button"
                          onClick={() => fileInputRef.current?.click()}
                          className="px-3.5 py-2 bg-blue-600/20 hover:bg-blue-600 text-blue-300 hover:text-white rounded-xl text-xs font-bold border border-blue-500/30 transition cursor-pointer flex items-center gap-2"
                        >
                          <Upload className="w-4 h-4" />
                          <span>Seleccionar Foto del Equipo...</span>
                        </button>
                        <p className="text-[11px] text-slate-400 mt-1">
                          {nombreArchivoSubido ? (
                            <span className="text-emerald-400 font-medium">Foto seleccionada: {nombreArchivoSubido}</span>
                          ) : (
                            <span>Se guardará en la carpeta local de SICDIN (PNG, JPG, WebP)</span>
                          )}
                        </p>
                      </div>
                    </div>
                  </div>
                ) : (
                  /* Modo Iconos Predeterminados */
                  <div className="grid grid-cols-5 gap-2">
                    {AVATAR_PRESETS.map(opt => {
                      const IconComp = opt.icon;
                      const seleccionado = formFoto.toLowerCase() === opt.id.toLowerCase();
                      return (
                        <button
                          key={opt.id}
                          type="button"
                          onClick={() => setFormFoto(opt.id)}
                          className={`p-2.5 rounded-xl border flex flex-col items-center gap-1.5 cursor-pointer transition ${
                            seleccionado
                              ? 'bg-blue-600/20 border-blue-500 text-white shadow-sm'
                              : 'bg-slate-900 border-slate-800 text-slate-400 hover:border-slate-700'
                          }`}
                        >
                          <IconComp className={`w-5 h-5 ${seleccionado ? 'text-blue-400' : 'text-slate-400'}`} />
                          <span className="text-[10px] font-semibold">{opt.label}</span>
                        </button>
                      );
                    })}
                  </div>
                )}
              </div>

              {/* Estado: Activo / Inactivo */}
              <div className="flex items-center gap-3 pt-1">
                <label className="flex items-center gap-2 cursor-pointer text-xs font-semibold text-slate-200">
                  <input
                    type="checkbox"
                    checked={formActivo}
                    onChange={e => setFormActivo(e.target.checked)}
                    className="w-4 h-4 rounded bg-slate-950 border-slate-700 text-blue-600 focus:ring-0 cursor-pointer"
                  />
                  <span>Usuario Activo (Permitir iniciar sesión y operar ventas)</span>
                </label>
              </div>

              {/* Botones del Modal */}
              <div className="pt-4 border-t border-slate-800 flex justify-end gap-2.5">
                <button
                  type="button"
                  onClick={() => setModalAbierto(false)}
                  className="px-4 py-2 bg-slate-800 hover:bg-slate-700 text-slate-300 rounded-xl text-xs font-semibold cursor-pointer"
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  disabled={guardando}
                  className="flex items-center gap-2 px-5 py-2 bg-blue-600 hover:bg-blue-500 text-white rounded-xl text-xs font-bold shadow-lg shadow-blue-600/20 cursor-pointer transition border border-blue-400/30"
                >
                  {guardando ? (
                    <>
                      <RefreshCw className="w-4 h-4 animate-spin" />
                      <span>Guardando...</span>
                    </>
                  ) : (
                    <>
                      <Check className="w-4 h-4" />
                      <span>{usuarioEditando ? 'Guardar Cambios' : 'Crear Usuario'}</span>
                    </>
                  )}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* 
        =======================================================
        MODAL DE CONFIRMACIÓN PARA ELIMINAR
        =======================================================
      */}
      {usuarioAEliminar && (
        <div className="fixed inset-0 bg-black/80 backdrop-blur-sm z-50 flex items-center justify-center p-4">
          <div className="bg-slate-900 border border-rose-500/30 rounded-2xl max-w-sm w-full p-6 shadow-2xl flex flex-col gap-4">
            <div className="flex items-center gap-3">
              <div className="p-3 bg-rose-500/10 text-rose-400 rounded-xl border border-rose-500/20">
                <AlertTriangle className="w-6 h-6" />
              </div>
              <div>
                <h3 className="text-base font-bold text-white">¿Eliminar Usuario?</h3>
                <p className="text-xs text-slate-400">Esta acción no se puede deshacer</p>
              </div>
            </div>

            <div className="text-xs text-slate-300 bg-slate-950 p-3 rounded-xl border border-slate-800 flex items-center gap-3">
              <UserAvatar foto={usuarioAEliminar.Foto} nombre={usuarioAEliminar.Nombre} size="md" />
              <div>
                <div className="font-bold text-white">#{usuarioAEliminar.Id}: {usuarioAEliminar.Nombre}</div>
                <div className="text-slate-400 text-[11px]">{usuarioAEliminar.Admin === 1 ? 'Administrador' : 'Cajero'}</div>
              </div>
            </div>

            <div className="flex justify-end gap-2.5 pt-2">
              <button
                type="button"
                onClick={() => setUsuarioAEliminar(null)}
                className="px-4 py-2 bg-slate-800 hover:bg-slate-700 text-slate-300 rounded-xl text-xs font-semibold cursor-pointer"
              >
                Cancelar
              </button>
              <button
                type="button"
                onClick={confirmarEliminar}
                disabled={eliminando}
                className="flex items-center gap-1.5 px-4 py-2 bg-rose-600 hover:bg-rose-500 text-white rounded-xl text-xs font-bold shadow-lg shadow-rose-600/20 cursor-pointer transition"
              >
                {eliminando ? <RefreshCw className="w-4 h-4 animate-spin" /> : <Trash2 className="w-4 h-4" />}
                <span>Eliminar Usuario</span>
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
