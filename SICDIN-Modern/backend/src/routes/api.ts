import { Router } from 'express';
import { ArticulosController } from '../controllers/ArticulosController';
import { VentasController } from '../controllers/VentasController';
import { ClientesController } from '../controllers/OtrosControllers';
import { ReparacionesController } from '../controllers/ReparacionesController';
import { UsuariosController } from '../controllers/UsuariosController';
import { WhatsAppController } from '../controllers/WhatsAppController';
import { syncEngine } from '../sync/SyncEngine';

const router = Router();

// Catálogo / Inventario
router.get('/articulos', ArticulosController.buscar);
router.get('/articulos/:clave', ArticulosController.obtenerPorClave);
router.post('/articulos', ArticulosController.guardar);
router.post('/articulos/:clave/existencias', ArticulosController.modificarExistencias);

// Ventas / Caja
router.post('/ventas', VentasController.crearVenta);
router.post('/ventas/rapida', VentasController.crearVentaRapida);
router.get('/ventas/dia', VentasController.ventasDelDia);
router.get('/ventas', VentasController.ultimasVentas);
router.get('/ventas/:folio', VentasController.obtenerDetalleVenta);

// Clientes y Libreta
router.get('/clientes', ClientesController.listar);
router.get('/clientes/:id', ClientesController.obtenerDetalle);
router.post('/clientes', ClientesController.crear);
router.put('/clientes/:id', ClientesController.actualizar);
router.delete('/clientes/:id', ClientesController.eliminar);

// Usuarios (Autenticación y Control de Usuarios)
router.post('/usuarios/login', UsuariosController.login);
router.get('/usuarios', UsuariosController.listar);
router.get('/usuarios/foto', UsuariosController.obtenerFotoArchivo);
router.post('/usuarios/upload-foto', UsuariosController.subirFoto);
router.get('/usuarios/:id', UsuariosController.obtener);
router.post('/usuarios', UsuariosController.crear);
router.put('/usuarios/:id', UsuariosController.actualizar);
router.delete('/usuarios/:id', UsuariosController.eliminar);

// Taller y Reparaciones
router.get('/reparaciones', ReparacionesController.listar);
router.get('/reparaciones/:folio', ReparacionesController.obtenerDetalle);
router.post('/reparaciones', ReparacionesController.crear);
router.put('/reparaciones/:folio/bicicletas/:biciId/estado', ReparacionesController.cambiarEstado);
router.post('/reparaciones/:folio/pagos', ReparacionesController.registrarPago);
router.put('/reparaciones/:folio/articulos/:clave/colocado', ReparacionesController.toggleColocado);
router.get('/trabajadores', ReparacionesController.listarTrabajadores);

// Sincronización Nube / Deltas
router.post('/sync/pull', (req, res) => {
  const result = syncEngine.processMyInbox();
  res.json({ message: 'Sincronización procesada', ...result });
});

router.get('/sync/destinos', (req, res) => {
  res.json({ destinos: syncEngine.getDestinationStores() });
});

// WhatsApp del Negocio (Baileys)
router.get('/whatsapp/status', WhatsAppController.getStatus);
router.post('/whatsapp/send', WhatsAppController.enviarComprobante);
router.post('/whatsapp/logout', WhatsAppController.logout);
router.post('/whatsapp/restart', WhatsAppController.restart);

export default router;
