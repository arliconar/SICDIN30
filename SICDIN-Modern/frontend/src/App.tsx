import { useState, useEffect } from 'react';
import { ShoppingCart, RefreshCw, Layers, Plus, X, Menu, ShieldCheck, Users, Edit3, Wrench, Receipt, Check, LogOut, Key, ChevronDown, Settings } from 'lucide-react';
import { api, type Articulo, type Usuario } from './api';
import UserAvatar from './UserAvatar';
import LoginModal from './LoginModal';
import AjustesModal from './AjustesModal';
import VentaView from './VentaView';
import CatalogoView from './CatalogoView';
import EditarArticuloView from './EditarArticuloView';
import ClientesView from './ClientesView';
import ReparacionesView from './ReparacionesView';
import NuevaReparacionView from './NuevaReparacionView';
import LibretaVentasView from './LibretaVentasView';
import UsuariosView from './UsuariosView';

export type RolUsuario = 'ADMIN' | 'USUARIO';

interface VentanaTab {
  id: string;
  tipo: 'venta' | 'catalogo' | 'libreta' | 'editar' | 'clientes' | 'reparaciones' | 'nueva_reparacion' | 'usuarios';
  title: string;
  articulo?: Articulo;
}

const SESSION_STORAGE_KEY = 'sicdin_sesion_activa';

export default function App() {
  // Inicialización de sesión desde sessionStorage:
  // - Persiste durante recargas de página (F5 o Ctrl+R).
  // - Se destruye automáticamente al cerrar la pestaña del navegador.
  const [usuarioActual, setUsuarioActual] = useState<Usuario | null>(() => {
    try {
      const saved = sessionStorage.getItem(SESSION_STORAGE_KEY);
      return saved ? JSON.parse(saved) : null;
    } catch {
      return null;
    }
  });

  const [sesionIniciada, setSesionIniciada] = useState<boolean>(() => {
    return !!sessionStorage.getItem(SESSION_STORAGE_KEY);
  });

  const [modalLoginAbierto, setModalLoginAbierto] = useState<boolean>(() => {
    return !sessionStorage.getItem(SESSION_STORAGE_KEY);
  });

  const [usuarioLoginObjetivo, setUsuarioLoginObjetivo] = useState<Usuario | null>(null);

  // Perfil actual determinado exclusivamente por el usuario autenticado
  const rolActual: RolUsuario = Number(usuarioActual?.Admin) === 1 ? 'ADMIN' : 'USUARIO';

  const [listaUsuarios, setListaUsuarios] = useState<Usuario[]>([]);
  const [selectorUsuarioAbierto, setSelectorUsuarioAbierto] = useState<boolean>(false);

  const [tabs, setTabs] = useState<VentanaTab[]>([
    { id: 'cat_init', tipo: 'catalogo', title: 'Catálogo de Artículos' },
    { id: 'libreta_init', tipo: 'libreta', title: 'Libreta de Ventas' }
  ]);
  const [activeTabId, setActiveTabId] = useState<string>('cat_init');
  const [contadorVentas, setContadorVentas] = useState(1);
  const [syncStatus, setSyncStatus] = useState<string | null>(null);
  const [syncing, setSyncing] = useState(false);
  const [menuAbierto, setMenuAbierto] = useState(false);
  const [modalAjustesAbierto, setModalAjustesAbierto] = useState<boolean>(false);
  const [estadoWhatsApp, setEstadoWhatsApp] = useState<'CONNECTED' | 'QR_READY' | 'CONNECTING' | 'DISCONNECTED'>('DISCONNECTED');
  const [notificacionBienvenida, setNotificacionBienvenida] = useState<Usuario | null>(null);

  useEffect(() => {
    cargarUsuariosApp();
    verificarEstadoWhatsApp();
    const intv = setInterval(verificarEstadoWhatsApp, 10000);
    return () => clearInterval(intv);
  }, []);

  const verificarEstadoWhatsApp = async () => {
    try {
      const res = await api.obtenerEstadoWhatsApp();
      setEstadoWhatsApp(res.status);
    } catch {
      // Ignorar si el backend está ocupado
    }
  };

  const cargarUsuariosApp = async () => {
    try {
      const data = await api.listarUsuarios();
      const safeData = Array.isArray(data) ? data : [];
      setListaUsuarios(safeData);

      const stored = sessionStorage.getItem(SESSION_STORAGE_KEY);
      if (stored) {
        try {
          const uLocal = JSON.parse(stored);
          const actualizado = safeData.find(u => u.Id === uLocal.Id);
          if (actualizado) {
            setUsuarioActual(actualizado);
            sessionStorage.setItem(SESSION_STORAGE_KEY, JSON.stringify(actualizado));
            document.title = `SICDIN 4.0: ${actualizado.Nombre}`;
          }
        } catch {}
      } else {
        // Sugerir Angel por defecto si aún no hay usuario
        const angel = safeData.find(u => u.Nombre.toLowerCase() === 'angel' || u.Id === 1);
        if (angel && !usuarioActual) {
          setUsuarioLoginObjetivo(angel);
        }
      }
    } catch (err) {
      console.error('Error cargando usuarios en App:', err);
    }
  };

  const handleLoginExitoso = (user: Usuario) => {
    sessionStorage.setItem(SESSION_STORAGE_KEY, JSON.stringify(user));
    setUsuarioActual(user);
    setSesionIniciada(true);
    setModalLoginAbierto(false);
    setSelectorUsuarioAbierto(false);
    document.title = `SICDIN 4.0: ${user.Nombre}`;
    setNotificacionBienvenida(user);
    setTimeout(() => {
      setNotificacionBienvenida(null);
    }, 3600);
  };

  const handleCerrarSesion = () => {
    sessionStorage.removeItem(SESSION_STORAGE_KEY);
    setSesionIniciada(false);
    setUsuarioLoginObjetivo(usuarioActual);
    setModalLoginAbierto(true);
    setSelectorUsuarioAbierto(false);
    document.title = 'SICDIN 4.0';
  };

  const handleAbrirCambioSesion = (u?: Usuario) => {
    setUsuarioLoginObjetivo(u || null);
    setModalLoginAbierto(true);
    setSelectorUsuarioAbierto(false);
  };

  const iniciarNuevaVenta = () => {
    const newId = `v_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'venta',
      title: `Venta ${contadorVentas}`
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setContadorVentas(prev => prev + 1);
    setMenuAbierto(false);
  };

  const abrirCatalogo = () => {
    const existing = tabs.find(t => t.tipo === 'catalogo');
    if (existing) {
      setActiveTabId(existing.id);
      setMenuAbierto(false);
      return;
    }
    const newId = `cat_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'catalogo',
      title: 'Catálogo de Artículos'
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setMenuAbierto(false);
  };

  const abrirLibreta = () => {
    const existing = tabs.find(t => t.tipo === 'libreta');
    if (existing) {
      setActiveTabId(existing.id);
      setMenuAbierto(false);
      return;
    }
    const newId = `lib_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'libreta',
      title: 'Libreta de Ventas'
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setMenuAbierto(false);
  };

  const abrirClientes = () => {
    const existing = tabs.find(t => t.tipo === 'clientes');
    if (existing) {
      setActiveTabId(existing.id);
      setMenuAbierto(false);
      return;
    }
    const newId = `clnts_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'clientes',
      title: 'Clientes y Adeudos'
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setMenuAbierto(false);
  };

  const abrirReparaciones = () => {
    const existing = tabs.find(t => t.tipo === 'reparaciones');
    if (existing) {
      setActiveTabId(existing.id);
      setMenuAbierto(false);
      return;
    }
    const newId = `rep_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'reparaciones',
      title: 'Taller de Reparaciones'
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setMenuAbierto(false);
  };

  const abrirNuevaReparacion = () => {
    const newId = `nrep_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'nueva_reparacion',
      title: 'Nueva Orden de Taller'
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setMenuAbierto(false);
  };

  const abrirUsuarios = () => {
    if (rolActual !== 'ADMIN') {
      alert('Acceso restringido: Solo el perfil Administrador puede gestionar usuarios.');
      return;
    }
    const existing = tabs.find(t => t.tipo === 'usuarios');
    if (existing) {
      setActiveTabId(existing.id);
      setMenuAbierto(false);
      return;
    }
    const newId = `usu_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'usuarios',
      title: 'Control de Usuarios'
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
    setMenuAbierto(false);
  };

  // Abrir pestaña de edición de un artículo (exclusivo admin)
  const abrirEditarArticulo = (art: Articulo) => {
    if (rolActual !== 'ADMIN') return;

    const existingTab = tabs.find(t => t.tipo === 'editar' && t.articulo?.Clave === art.Clave);
    if (existingTab) {
      setActiveTabId(existingTab.id);
      return;
    }

    const newId = `edit_${art.Clave}_${Date.now()}`;
    const newTab: VentanaTab = {
      id: newId,
      tipo: 'editar',
      title: `Editar ${art.Clave}`,
      articulo: art
    };
    setTabs(prev => [...prev, newTab]);
    setActiveTabId(newId);
  };

  const cerrarPestana = (id: string, e?: React.MouseEvent) => {
    if (e) e.stopPropagation();
    const tabToClose = tabs.find(t => t.id === id);
    if (tabToClose?.tipo === 'catalogo' || tabToClose?.tipo === 'libreta') return;
    if (tabs.length === 1) return; // Mantener al menos una pestaña
    const newTabs = tabs.filter(t => t.id !== id);
    setTabs(newTabs);
    if (activeTabId === id) {
      setActiveTabId(newTabs[newTabs.length - 1].id);
    }
  };

  const handleSync = async () => {
    setSyncing(true);
    try {
      const res = await api.syncPull();
      setSyncStatus(`Sync OK: ${res.processed} deltas`);
    } catch {
      setSyncStatus('Error sync');
    } finally {
      setSyncing(false);
      setTimeout(() => setSyncStatus(null), 4000);
    }
  };

  return (
    <div className="relative flex h-screen w-screen overflow-hidden bg-slate-950 text-slate-100 font-sans select-none">
      {/* 
        MENÚ LATERAL (Solo se abre y cierra haciendo clic en el botón de Menú)
      */}
      {menuAbierto && (
        <div 
          className="fixed inset-0 top-14 bg-black/40 backdrop-blur-xs z-40 transition-opacity"
          onClick={() => setMenuAbierto(false)}
        />
      )}

      <div
        className={`fixed top-14 left-0 bottom-0 z-40 w-64 bg-slate-900/98 backdrop-blur-xl border-r border-slate-700/80 shadow-2xl flex flex-col transition-transform duration-200 ease-out ${
          menuAbierto ? 'translate-x-0' : '-translate-x-full'
        }`}
      >
        <div className="p-4 border-b border-slate-800 flex items-center justify-between">
          <div className="flex items-center gap-2.5">
            <img src="/logo.png" alt="SICDIN" className="w-8 h-8 object-contain rounded" />
            <div>
              <div className="text-xs font-bold text-white tracking-wide">MENÚ PRINCIPAL</div>
              <div className="text-[10px] text-blue-400 font-mono font-bold tracking-wider">SICDIN 4.0</div>
            </div>
          </div>
          <button
            onClick={() => setMenuAbierto(false)}
            className="p-1 text-slate-400 hover:text-white rounded hover:bg-slate-800 cursor-pointer"
            title="Cerrar menú"
          >
            <X className="w-4 h-4" />
          </button>
        </div>

        <div className="p-3 space-y-2 flex-1">
          {/* Iniciar Venta Nueva */}
          <button
            onClick={iniciarNuevaVenta}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-blue-600/10 hover:bg-blue-600 text-blue-300 hover:text-white border border-blue-500/20 hover:border-blue-500 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <ShoppingCart className="w-4 h-4 text-blue-400 group-hover/btn:text-white" />
            <div className="text-left">
              <div className="text-xs font-bold">Iniciar Venta</div>
              <div className="text-[10px] font-normal text-slate-400 group-hover/btn:text-blue-100">
                Abre una ventana de venta
              </div>
            </div>
          </button>

          {/* Ver Catálogo */}
          <button
            onClick={abrirCatalogo}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-slate-800/60 hover:bg-slate-800 text-slate-200 hover:text-white border border-slate-700/60 hover:border-slate-600 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <Layers className="w-4 h-4 text-emerald-400" />
            <div className="text-left">
              <div className="text-xs font-bold">Ver Catálogo</div>
              <div className="text-[10px] font-normal text-slate-400">
                Precios, PME, Mayoreo y Stock
              </div>
            </div>
          </button>

          {/* Libreta de Ventas (Ventas del Día) */}
          <button
            onClick={abrirLibreta}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-slate-800/60 hover:bg-slate-800 text-slate-200 hover:text-white border border-slate-700/60 hover:border-slate-600 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <Receipt className="w-4 h-4 text-blue-400" />
            <div className="text-left">
              <div className="text-xs font-bold">Libreta de Ventas</div>
              <div className="text-[10px] font-normal text-slate-400">
                Ventas del día y libro de caja
              </div>
            </div>
          </button>

          {/* Ver Clientes */}
          <button
            onClick={abrirClientes}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-slate-800/60 hover:bg-slate-800 text-slate-200 hover:text-white border border-slate-700/60 hover:border-slate-600 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <Users className="w-4 h-4 text-purple-400" />
            <div className="text-left">
              <div className="text-xs font-bold">Clientes y Adeudos</div>
              <div className="text-[10px] font-normal text-slate-400">
                Adeudos, compras y directorio
              </div>
            </div>
          </button>

          {/* Taller de Reparaciones */}
          <button
            onClick={abrirReparaciones}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-slate-800/60 hover:bg-slate-800 text-slate-200 hover:text-white border border-slate-700/60 hover:border-slate-600 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <Wrench className="w-4 h-4 text-amber-400" />
            <div className="text-left">
              <div className="text-xs font-bold">Taller de Reparaciones</div>
              <div className="text-[10px] font-normal text-slate-400">
                Bicicletas, refacciones y entregas
              </div>
            </div>
          </button>

          {/* Nueva Orden de Taller */}
          <button
            onClick={abrirNuevaReparacion}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-amber-600/10 hover:bg-amber-600 text-amber-300 hover:text-white border border-amber-500/20 hover:border-amber-500 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <Wrench className="w-4 h-4 text-amber-400 group-hover/btn:text-white" />
            <div className="text-left">
              <div className="text-xs font-bold">+ Nueva Orden de Taller</div>
              <div className="text-[10px] font-normal text-slate-400 group-hover/btn:text-amber-100">
                Multi-bicicletas y refacciones
              </div>
            </div>
          </button>

          {/* Control de Usuarios (Exclusivo Administrador) */}
          {rolActual === 'ADMIN' && (
            <button
              onClick={abrirUsuarios}
              className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-gradient-to-r from-amber-500/15 to-orange-500/10 hover:from-amber-500/30 hover:to-orange-500/20 text-amber-300 hover:text-white border border-amber-500/30 hover:border-amber-500/60 transition text-sm font-semibold cursor-pointer group/btn shadow-xs"
            >
              <ShieldCheck className="w-4 h-4 text-amber-400 group-hover/btn:text-white shrink-0" />
              <div className="text-left flex-1 min-w-0">
                <div className="flex items-center justify-between">
                  <div className="text-xs font-bold text-amber-200 group-hover/btn:text-white">Control Usuarios</div>
                  <span className="text-[9px] bg-amber-500/20 text-amber-300 px-1.5 py-0.5 rounded font-bold uppercase tracking-wider border border-amber-500/30">
                    Admin
                  </span>
                </div>
                <div className="text-[10px] font-normal text-slate-400 group-hover/btn:text-amber-100 truncate">
                  Cuentas, contraseñas y accesos
                </div>
              </div>
            </button>
          )}

          {/* Menú de Ajustes del Sistema (WhatsApp, Impresión, Tienda) */}
          <button
            onClick={() => {
              setModalAjustesAbierto(true);
              setMenuAbierto(false);
            }}
            className="w-full flex items-center gap-3 px-3 py-2.5 rounded-xl bg-slate-800/60 hover:bg-slate-800 text-slate-200 hover:text-white border border-slate-700/60 transition text-sm font-semibold cursor-pointer group/btn"
          >
            <Settings className="w-4 h-4 text-blue-400 group-hover/btn:rotate-45 transition-transform" />
            <div className="text-left flex-1 min-w-0">
              <div className="flex items-center justify-between">
                <div className="text-xs font-bold">Ajustes del Sistema</div>
                <span
                  className={`text-[9px] px-1.5 py-0.2 rounded font-bold uppercase tracking-wider border ${
                    estadoWhatsApp === 'CONNECTED'
                      ? 'bg-emerald-950 text-emerald-300 border-emerald-800'
                      : 'bg-slate-800 text-slate-400 border-slate-700'
                  }`}
                >
                  {estadoWhatsApp === 'CONNECTED' ? 'WhatsApp OK' : 'Ajustes'}
                </span>
              </div>
              <div className="text-[10px] font-normal text-slate-400 group-hover/btn:text-blue-100 truncate">
                WhatsApp, Impresión y Tienda
              </div>
            </div>
          </button>
        </div>

        <div className="p-3 border-t border-slate-800 text-[11px] text-slate-500 text-center">
          Usa el botón de Menú en la barra superior.
        </div>
      </div>

      {/* ÁREA PRINCIPAL */}
      <div className="flex-1 flex flex-col h-full overflow-hidden">
        {/* Barra Superior con Selector de Perfil y Pestañas */}
        <header className="h-14 px-3 border-b border-slate-800 flex items-center justify-between bg-slate-900/90 backdrop-blur z-20">
          <div className="flex items-center gap-2 overflow-x-auto max-w-3xl py-1">
            {/* Logo de la aplicación */}
            <div className="flex items-center gap-1.5 mr-1 pl-0.5 shrink-0" title="SICDIN 4.0">
              <img src="/logo.png" alt="SICDIN" className="w-6 h-6 object-contain rounded" />
              <span className="text-xs font-bold font-mono tracking-wider text-blue-400 hidden lg:inline">SICDIN 4.0</span>
            </div>

            {/* Botón de Menú dedicado */}
            <button
              onClick={() => setMenuAbierto(prev => !prev)}
              title="Abrir/Cerrar Menú"
              className={`p-2 rounded-lg border transition cursor-pointer flex items-center gap-1.5 text-xs font-bold mr-1 ${
                menuAbierto
                  ? 'bg-blue-600 text-white border-blue-500'
                  : 'bg-slate-800 hover:bg-slate-700 text-slate-300 border-slate-700'
              }`}
            >
              <Menu className="w-4 h-4" />
              <span className="hidden sm:inline">Menú</span>
            </button>

            {/* Pestañas de Ventanas Activas */}
            {tabs.map(tab => (
              <div
                key={tab.id}
                onClick={() => setActiveTabId(tab.id)}
                className={`flex items-center gap-2 px-3.5 py-1.5 rounded-lg text-xs font-medium cursor-pointer transition border whitespace-nowrap ${
                  activeTabId === tab.id
                    ? 'bg-blue-600 text-white border-blue-500 shadow-sm font-semibold'
                    : 'bg-slate-800/80 text-slate-400 border-slate-700/60 hover:bg-slate-800 hover:text-slate-200'
                }`}
              >
                {tab.tipo === 'venta' && <ShoppingCart className="w-3.5 h-3.5" />}
                {tab.tipo === 'catalogo' && <Layers className="w-3.5 h-3.5 text-emerald-400" />}
                {tab.tipo === 'libreta' && <Receipt className="w-3.5 h-3.5 text-blue-400" />}
                {tab.tipo === 'clientes' && <Users className="w-3.5 h-3.5 text-purple-400" />}
                {tab.tipo === 'reparaciones' && <Wrench className="w-3.5 h-3.5 text-amber-400" />}
                {tab.tipo === 'nueva_reparacion' && <Wrench className="w-3.5 h-3.5 text-blue-400" />}
                {tab.tipo === 'usuarios' && <ShieldCheck className="w-3.5 h-3.5 text-amber-300" />}
                {tab.tipo === 'editar' && <Edit3 className="w-3.5 h-3.5 text-amber-400" />}
                <span>{tab.title}</span>
                {tabs.length > 1 && tab.tipo !== 'catalogo' && tab.tipo !== 'libreta' && (
                  <button
                    onClick={e => cerrarPestana(tab.id, e)}
                    className="hover:text-rose-300 p-0.5 rounded cursor-pointer ml-1"
                    title="Cerrar ventana"
                  >
                    <X className="w-3 h-3" />
                  </button>
                )}
              </div>
            ))}

            {/* Botón rápido para agregar venta */}
            <button
              onClick={iniciarNuevaVenta}
              title="Iniciar otra venta nueva"
              className="p-1.5 rounded-lg bg-slate-800 hover:bg-slate-700 text-slate-300 border border-slate-700 cursor-pointer transition shrink-0"
            >
              <Plus className="w-4 h-4" />
            </button>
          </div>

          {/* Selector de Perfil, Usuario en Sesión y Sincronización */}
          <div className="flex items-center gap-2.5">
            {/* Widget del Usuario Activo / Logeado (Visible en todo momento) */}
            <div className="relative">
              <button
                onClick={() => setSelectorUsuarioAbierto(prev => !prev)}
                className="flex items-center gap-2 px-2.5 py-1 bg-slate-950/80 hover:bg-slate-800 rounded-xl border border-slate-700/80 transition cursor-pointer shadow-sm group"
                title="Hacer clic para cambiar de usuario o cerrar sesión"
              >
                <UserAvatar foto={usuarioActual?.Foto} nombre={usuarioActual?.Nombre} size="sm" />
                <div className="text-left hidden sm:block">
                  <div className="flex items-center gap-1.5">
                    <span className="text-xs font-bold text-white group-hover:text-blue-300 transition">
                      {usuarioActual?.Nombre || 'Iniciar Sesión'}
                    </span>
                    {usuarioActual?.Admin === 1 ? (
                      <span className="text-[9px] bg-amber-500/20 text-amber-300 border border-amber-500/30 px-1.5 py-0.2 rounded font-bold uppercase tracking-wider">
                        Admin
                      </span>
                    ) : usuarioActual ? (
                      <span className="text-[9px] bg-blue-500/20 text-blue-300 border border-blue-500/30 px-1.5 py-0.2 rounded font-bold uppercase tracking-wider">
                        Cajero
                      </span>
                    ) : null}
                  </div>
                  <div className="text-[10px] text-slate-400 font-sans">
                    {sesionIniciada ? 'Sesión activa' : 'Sin autenticar'}
                  </div>
                </div>
                <ChevronDown className="w-3.5 h-3.5 text-slate-400 ml-0.5 group-hover:text-white transition" />
              </button>

              {/* Menú desplegable para alternar de usuario con autenticación */}
              {selectorUsuarioAbierto && (
                <div className="absolute right-0 top-12 mt-1 w-64 bg-slate-900 border border-slate-700 rounded-2xl shadow-2xl p-2 z-50">
                  <div className="px-3 py-2 border-b border-slate-800 text-[11px] font-bold text-slate-400 uppercase tracking-wider flex items-center justify-between">
                    <span>Sesión de Usuario</span>
                    <button
                      onClick={() => setSelectorUsuarioAbierto(false)}
                      className="text-slate-500 hover:text-white p-0.5 rounded cursor-pointer"
                    >
                      <X className="w-3.5 h-3.5" />
                    </button>
                  </div>

                  <div className="py-2 border-b border-slate-800 space-y-1">
                    <button
                      onClick={() => handleAbrirCambioSesion()}
                      className="w-full flex items-center gap-2.5 px-3 py-2 rounded-xl text-xs font-semibold bg-blue-600/10 hover:bg-blue-600 text-blue-300 hover:text-white transition cursor-pointer text-left"
                    >
                      <Key className="w-4 h-4 text-blue-400" />
                      <span>Cambiar de Usuario...</span>
                    </button>

                    <button
                      onClick={handleCerrarSesion}
                      className="w-full flex items-center gap-2.5 px-3 py-2 rounded-xl text-xs font-semibold bg-rose-600/10 hover:bg-rose-600 text-rose-300 hover:text-white transition cursor-pointer text-left"
                    >
                      <LogOut className="w-4 h-4 text-rose-400" />
                      <span>Cerrar Sesión</span>
                    </button>
                  </div>

                  <div className="px-3 pt-2 pb-1 text-[10px] font-bold text-slate-500 uppercase tracking-wider">
                    Usuarios Rápidos (Pide Contraseña)
                  </div>

                  <div className="max-h-52 overflow-y-auto py-1 space-y-1">
                    {listaUsuarios.filter(u => u.activo === 1).map(u => (
                      <button
                        key={u.Id}
                        onClick={() => handleAbrirCambioSesion(u)}
                        className={`w-full flex items-center gap-2.5 px-2.5 py-1.5 rounded-xl text-xs transition cursor-pointer text-left ${
                          usuarioActual?.Id === u.Id
                            ? 'bg-blue-600/20 text-white border border-blue-500/40 font-bold'
                            : 'hover:bg-slate-800 text-slate-300'
                        }`}
                      >
                        <UserAvatar foto={u.Foto} nombre={u.Nombre} size="xs" />
                        <div className="flex-1 min-w-0">
                          <div className="font-semibold truncate">{u.Nombre}</div>
                          <div className="text-[10px] text-slate-400">{u.Admin === 1 ? 'Admin' : 'Cajero'}</div>
                        </div>
                        {usuarioActual?.Id === u.Id && <Check className="w-3.5 h-3.5 text-blue-400 shrink-0" />}
                      </button>
                    ))}
                  </div>
                </div>
              )}
            </div>

            {/* Botón de Ajustes del Sistema */}
            <button
              onClick={() => setModalAjustesAbierto(true)}
              title="Ajustes del Sistema (WhatsApp, Impresión y Tienda)"
              className="flex items-center gap-1.5 px-2.5 py-1.5 rounded-lg text-xs font-semibold bg-slate-800 hover:bg-slate-700 text-slate-200 border border-slate-700 transition cursor-pointer"
            >
              <Settings className="w-3.5 h-3.5 text-blue-400" />
              <span className="hidden xl:inline">Ajustes</span>
              <span
                title={`WhatsApp: ${estadoWhatsApp === 'CONNECTED' ? 'Conectado' : 'Requiere vincular'}`}
                className={`w-2 h-2 rounded-full ${
                  estadoWhatsApp === 'CONNECTED'
                    ? 'bg-emerald-400 shadow-[0_0_6px_#10b981]'
                    : estadoWhatsApp === 'QR_READY'
                    ? 'bg-amber-400 animate-pulse'
                    : 'bg-slate-500'
                }`}
              />
            </button>

            {/* Sincronización Nube */}
            {syncStatus && (
              <span className="text-xs text-emerald-400 bg-emerald-950/80 px-2 py-1 rounded border border-emerald-800 font-mono">
                {syncStatus}
              </span>
            )}
            <button
              onClick={handleSync}
              disabled={syncing}
              className="flex items-center gap-2 text-xs bg-slate-800 hover:bg-slate-700 px-3 py-1.5 rounded-lg text-slate-300 cursor-pointer transition border border-slate-700"
            >
              <RefreshCw className={`w-3.5 h-3.5 ${syncing ? 'animate-spin text-blue-400' : ''}`} />
              Sincronizar
            </button>
          </div>
        </header>

        {/* Contenedor de la Ventana Activa */}
        <div className="flex-1 flex overflow-hidden relative">
          {/* Toast / Banner flotante con animación bonita de Bienvenida */}
          {notificacionBienvenida && (
            <div className="fixed top-16 left-1/2 -translate-x-1/2 z-50 pointer-events-none animate-welcome-toast">
              <div className="bg-slate-900/95 backdrop-blur-xl border border-blue-500/40 text-white px-5 py-3 rounded-2xl shadow-2xl flex items-center gap-3.5 shadow-blue-500/20">
                <UserAvatar foto={notificacionBienvenida.Foto} nombre={notificacionBienvenida.Nombre} size="md" />
                <div>
                  <div className="flex items-center gap-2">
                    <span className="text-xs text-blue-400 font-bold uppercase tracking-wider">¡Bienvenido(a)!</span>
                    <span className="text-[10px] bg-blue-500/20 text-blue-300 px-1.5 py-0.5 rounded font-mono font-bold">
                      {notificacionBienvenida.Admin === 1 ? 'Administrador' : 'Cajero'}
                    </span>
                  </div>
                  <div className="text-sm font-extrabold text-white">
                    {notificacionBienvenida.Nombre}
                  </div>
                </div>
                <div className="w-2 h-2 rounded-full bg-emerald-400 animate-pulse ml-2" />
              </div>
            </div>
          )}

          {tabs.map(tab => (
            <div
              key={tab.id}
              className={`flex-1 h-full ${activeTabId === tab.id ? 'flex animate-tab-enter' : 'hidden'}`}
            >
              {tab.tipo === 'venta' && (
                <VentaView
                  id={tab.id}
                  tabTitle={tab.title}
                  usuarioActualNombre={usuarioActual?.Nombre || 'ADMIN'}
                  onClose={() => cerrarPestana(tab.id)}
                />
              )}
              {tab.tipo === 'catalogo' && (
                <CatalogoView
                  esAdmin={rolActual === 'ADMIN'}
                  onEditarArticulo={abrirEditarArticulo}
                />
              )}
              {tab.tipo === 'libreta' && (
                <LibretaVentasView activa={activeTabId === tab.id} />
              )}
              {tab.tipo === 'clientes' && (
                <ClientesView esAdmin={rolActual === 'ADMIN'} />
              )}
              {tab.tipo === 'usuarios' && (
                <UsuariosView
                  esAdmin={rolActual === 'ADMIN'}
                  onUsuarioActualizado={cargarUsuariosApp}
                />
              )}
              {tab.tipo === 'reparaciones' && (
                <ReparacionesView
                  esAdmin={rolActual === 'ADMIN'}
                  onNuevaOrden={abrirNuevaReparacion}
                />
              )}
              {tab.tipo === 'nueva_reparacion' && (
                <NuevaReparacionView
                  onOrdenGuardada={_folio => {
                    cerrarPestana(tab.id);
                    abrirReparaciones();
                  }}
                  onCerrar={() => cerrarPestana(tab.id)}
                />
              )}
              {tab.tipo === 'editar' && tab.articulo && (
                <EditarArticuloView
                  clave={tab.articulo.Clave}
                  articuloInicial={tab.articulo}
                  onGuardado={artActualizado => {
                    tab.articulo = artActualizado;
                  }}
                  onCerrar={() => cerrarPestana(tab.id)}
                />
              )}
            </div>
          ))}
        </div>
      </div>

      {/* Ventana Modal de Inicio de Sesión y Autenticación con Contraseña */}
      {modalLoginAbierto && (
        <LoginModal
          esObligatorio={!sesionIniciada}
          usuarios={listaUsuarios}
          usuarioSeleccionadoInicial={usuarioLoginObjetivo || usuarioActual}
          onLoginExitoso={handleLoginExitoso}
          onCancelar={sesionIniciada ? () => setModalLoginAbierto(false) : undefined}
        />
      )}

      {/* Modal de Ajustes del Sistema (WhatsApp, Impresión y Tienda) */}
      {modalAjustesAbierto && (
        <AjustesModal
          onClose={() => {
            setModalAjustesAbierto(false);
            verificarEstadoWhatsApp();
          }}
        />
      )}
    </div>
  );
}
