using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class ClassPedido
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        //Obtener siguiente id pedido
        public int ObtenerSiguienteIdPedido()
        {
            using (var context = new ArimaERPEntities())
            {
                int maxId = context.PEDIDO.Any() ? context.PEDIDO.Max(p => p.id_pedido) : 0;
                return maxId + 1;
            }
        }
        //Guardar pedido
        public bool GuardarPedido(PEDIDO pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.PEDIDO.Add(pedido);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return false;
            }
        }
        //guardar detalle pedido
        public Boolean GuardarDetallePedido(DETALLE_PEDIDO detallePedido)
        {

            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.DETALLE_PEDIDO.Add(detallePedido);
                    context.SaveChanges();
                }
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                ErroresValidacion.Clear();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        string mensaje = $"Entidad: {validationErrors.Entry.Entity.GetType().Name}, Campo: {error.PropertyName}, Error: {error.ErrorMessage}";
                        ErroresValidacion.Add(mensaje);

                    }
                }
                return false;
            }
        }
        // Pseudocódigo detallado:
        // 1. Limpiar ErroresValidacion al inicio.
        // 2. Validar lista de detalles nula o vacía -> agregar error y devolver false.
        // 3. Abrir contexto de EF.
        // 4. Para cada detalle:
        //    a. Obtener producto_presentacion correspondiente.
        //       - Si no existe, agregar error y continuar (NO agregar el detalle al contexto).
        //    b. Calcular unidadesTotales = cantidad + cantidad_bultos * unidades_bulto.
        //    c. Obtener el registro de stock para producto y presentación.
        //       - Si no existe, agregar error y continuar (NO agregar el detalle).
        //    d. Verificar si hay stock suficiente:
        //       - Si stock.stock_actual - unidadesTotales < 0 -> insuficiente: agregar error y continuar.
        //       - Si suficiente: restar unidadesTotales de stock.stock_actual.
        //    e. Agregar el detalle al contexto sólo si las validaciones anteriores pasaron.
        // 5. Después del bucle, llamar a context.SaveChanges() para persistir cambios en detalles y stock.
        // 6. Manejar DbEntityValidationException para llenar ErroresValidacion con mensajes detallados.
        // 7. Manejar Exception general agregando mensajes y devolviendo false.

        public bool GuardarDetalles(List<DETALLE_PEDIDO> detalles)
        {
            ErroresValidacion.Clear();

            if (detalles == null || detalles.Count == 0)
            {
                ErroresValidacion.Add("No se recibieron detalles para guardar.");
                return false;
            }

            try
            {
                using (var context = new ArimaERPEntities())
                {
                    foreach (var detalle in detalles)
                    {
                        // Obtener producto_presentacion para calcular unidades por bulto
                        var productoPresentacion = context.producto_presentacion
                            .FirstOrDefault(pp => pp.id_producto == detalle.id_producto && pp.ID_presentacion == detalle.ID_presentacion);

                        if (productoPresentacion == null)
                        {
                            ErroresValidacion.Add($"No se encontró presentación para el producto {detalle.id_producto}.");
                            continue;
                        }

                        int unidadesTotales = (detalle.cantidad ?? 0) + (detalle.cantidad_bultos ?? 0) * productoPresentacion.unidades_bulto;

                        // Obtener el stock correspondiente
                        var stock = context.stock
                            .FirstOrDefault(s => s.id_producto == detalle.id_producto && s.ID_presentacion == detalle.ID_presentacion);

                        if (stock == null)
                        {
                            ErroresValidacion.Add($"No se encontró stock para el producto {detalle.id_producto} con presentación {detalle.ID_presentacion}.");
                            continue;
                        }

                        // Verificar si hay suficiente stock (condición corregida)
                        if (stock.stock_actual - unidadesTotales < 0)
                        {
                            ErroresValidacion.Add($"Stock insuficiente para el producto {detalle.id_producto}. Solicitado: {unidadesTotales}, Disponible: {stock.stock_actual}.");
                            continue;
                        }

                        // Descontar del stock y agregar el detalle al contexto
                        stock.stock_actual -= unidadesTotales;
                        context.DETALLE_PEDIDO.Add(detalle);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                ErroresValidacion.Clear();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        string mensaje = $"Entidad: {validationErrors.Entry.Entity.GetType().Name}, Campo: {error.PropertyName}, Error: {error.ErrorMessage}";
                        ErroresValidacion.Add(mensaje);
                    }
                }

                if (ex.InnerException != null)
                    ErroresValidacion.Add("Detalle interno: " + ex.InnerException.Message);

                return false;
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error general al guardar detalles: " + ex.Message);

                if (ex.InnerException != null)
                    ErroresValidacion.Add("Inner: " + ex.InnerException.Message);

                if (ex.InnerException?.InnerException != null)
                    ErroresValidacion.Add("Inner deeper: " + ex.InnerException.InnerException.Message);

                return false;
            }
        }


        //guardar pedido y devolver id_pedido
        public int CrearPedido(PEDIDO pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.PEDIDO.Add(pedido);
                    context.SaveChanges(); // Aquí se genera el id_pedido automáticamente

                    return pedido.id_pedido; // Devuelve el ID generado
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                ErroresValidacion.Clear();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        string mensaje = $"Entidad: {validationErrors.Entry.Entity.GetType().Name}, Campo: {error.PropertyName}, Error: {error.ErrorMessage}";
                        ErroresValidacion.Add(mensaje);
                    }
                }
                if (ex.InnerException != null)
                    ErroresValidacion.Add("Inner: " + ex.InnerException.Message);

                if (ex.InnerException?.InnerException != null)
                    ErroresValidacion.Add("Inner deeper: " + ex.InnerException.InnerException.Message);
                return -1;
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error general al guardar el pedido: " + ex.Message);

                if (ex.InnerException != null)
                    ErroresValidacion.Add("Inner: " + ex.InnerException.Message);

                if (ex.InnerException?.InnerException != null)
                    ErroresValidacion.Add("Inner deeper: " + ex.InnerException.InnerException.Message);

                return -1;
            }
        }
        public PEDIDO ObtenerPedidoPorId(int id_pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.PEDIDO.FirstOrDefault(p => p.id_pedido == id_pedido);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        public bool GenerarFactura(int id_pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var pedido = context.PEDIDO.FirstOrDefault(p => p.id_pedido == id_pedido);
                    if (pedido == null)
                    {
                        ErroresValidacion.Clear();
                        ErroresValidacion.Add("No se encontró el pedido con el ID especificado.");
                        return false;
                    }

                    if (pedido.numero_factura != null)
                    {
                        ErroresValidacion.Clear();
                        ErroresValidacion.Add("Este pedido ya tiene una factura asignada.");
                        return false;
                    }

                    int ultimoNumero = context.PEDIDO
                        .Where(p => p.numero_factura != null)
                        .Max(p => p.numero_factura) ?? 0;

                    pedido.numero_factura = ultimoNumero + 1;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                ErroresValidacion.Clear();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        string mensaje = $"Entidad: {validationErrors.Entry.Entity.GetType().Name}, Campo: {error.PropertyName}, Error: {error.ErrorMessage}";
                        ErroresValidacion.Add(mensaje);
                    }
                }
                if (ex.InnerException != null)
                    ErroresValidacion.Add("Detalle interno: " + ex.InnerException.Message);
                return false;
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error general al generar factura: " + ex.Message);
                if (ex.InnerException != null)
                    ErroresValidacion.Add("Inner: " + ex.InnerException.Message);
                if (ex.InnerException?.InnerException != null)
                    ErroresValidacion.Add("Inner deeper: " + ex.InnerException.InnerException.Message);
                return false;
            }
        }
        //generar numero de factura para un pedido y devolver el numero de factura
        public int GenerarNumeroFactura(int id_pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var pedido = context.PEDIDO.FirstOrDefault(p => p.id_pedido == id_pedido);
                    if (pedido == null)
                    {
                        ErroresValidacion.Clear();
                        ErroresValidacion.Add("No se encontró el pedido con el ID especificado.");
                        return -1;
                    }
                    if (pedido.numero_factura != null)
                    {
                        return pedido.numero_factura.Value; // Ya tiene un número de factura asignado
                    }
                    int ultimoNumero = context.PEDIDO
                        .Where(p => p.numero_factura != null)
                        .Max(p => p.numero_factura) ?? 0;
                    pedido.numero_factura = ultimoNumero + 1;
                    context.SaveChanges();
                    return pedido.numero_factura.Value;
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error al generar número de factura: " + ex.Message);
                return -1;
            }
        }
        //obtener detalles del pedido por id_pedido
        public List<DETALLE_PEDIDO> ObtenerDetallesPedido(int id_pedido)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.DETALLE_PEDIDO.Where(d => d.id_pedido == id_pedido).ToList();
            }
        }
        //Actualizar pedido
        public PEDIDO UpdatePedido(PEDIDO pedido)
        {
            using (var context = new ArimaERPEntities())
            {
                var existingPedido = context.PEDIDO.Find(pedido.id_pedido);
                if (existingPedido != null)
                {
                    context.Entry(existingPedido).CurrentValues.SetValues(pedido);
                    context.SaveChanges();
                }
                return existingPedido;
            }
        }
        //Obtener List de ESTADO_PEDIDO
        public List<ESTADO_PEDIDO> ObtenerEstadosPedido()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.ESTADO_PEDIDO.ToList();
            }
        }
        public int? BuscarIdClientePorTexto(string texto)
        {
            using (var contexto = new ArimaERPEntities())
            {
                var cliente = contexto.CLIENTE
                    .Where(c =>
                        c.email.Contains(texto) ||
                        SqlFunctions.StringConvert((double?)c.dni).Trim().Contains(texto)
                    )
                    .FirstOrDefault();

                return cliente?.id_cliente;
            }
        }
        //Obtener pedidos por id_cliente
        public List<PEDIDO> ObtenerPedidosPorIdCliente(int id_cliente)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.id_cliente == id_cliente).ToList();
            }
        }
        //Obtener todos los pedidos
        public List<PEDIDO> ObtenerTodosLosPedidos()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.ToList();
            }
        }
        //Obtener pedido por numero de factura
        public PEDIDO ObtenerPedidoPorNumeroFactura(int numero_factura)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.FirstOrDefault(p => p.numero_factura == numero_factura);
            }
        }
        //Obtener pedidos por zona
        public List<PEDIDO> ObtenerPedidosPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = from p in context.PEDIDO
                              join c in context.CLIENTE on p.id_cliente equals c.id_cliente
                              where c.id_zona == id_zona
                              select p;
                return pedidos.ToList();
            }
        }
        //Obtener pedidos por estado
        public List<PEDIDO> ObtenerPedidosPorEstado(int id_estado)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.id_estado == id_estado).ToList();
            }
        }
        //Obtener pedidos por fecha de entrega obtenida de dateTimePicker1
        public List<PEDIDO> ObtenerPedidosPorFechaEntrega(DateTime fecha_entrega)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.fecha_entrega == fecha_entrega).ToList();
            }
        }
        public List<PEDIDO> ObtenerPedidosPorFechaCreacion(DateTime fecha_creacion)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.fecha_creacion == fecha_creacion).ToList();
            }
        }
        //Obtener pedidos de un monto menor o igual al indicado
        public List<PEDIDO> ObtenerPedidosPorMontoMaximo(decimal montoMaximo)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.total <= montoMaximo).ToList();
            }
        }
        //Obtener pedidos por vendedor
        public List<PEDIDO> ObtenerPedidosPorVendedor(string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.vendedor == vendedor).ToList();
            }
        }
        public bool EliminarPedido(int id_pedido)
        {
            // Eliminar los DETALLE_PEDIDO relacionados y luego el PEDIDO
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var pedido = context.PEDIDO.FirstOrDefault(p => p.id_pedido == id_pedido);
                    if (pedido != null)
                    {
                        // Eliminar los detalles relacionados
                        var detalles = context.DETALLE_PEDIDO.Where(d => d.id_pedido == id_pedido).ToList();
                        foreach (var detalle in detalles)
                        {
                            context.DETALLE_PEDIDO.Remove(detalle);
                        }

                        // Eliminar el pedido
                        context.PEDIDO.Remove(pedido);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        ErroresValidacion.Clear();
                        ErroresValidacion.Add("No se encontró el pedido con el ID especificado.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error al eliminar el pedido: " + ex.Message);
                return false;
            }
        }
        //Eliminar detalles de un pedido
        public bool EliminarDetallesPedido(int id_pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var detalles = context.DETALLE_PEDIDO.Where(d => d.id_pedido == id_pedido).ToList();
                    foreach (var detalle in detalles)
                    {
                        context.DETALLE_PEDIDO.Remove(detalle);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error al eliminar los detalles del pedido: " + ex.Message);
                return false;
            }
        }
        //Obtener ESTADO_PEDIDO  por id_estado
        public ESTADO_PEDIDO ObtenerEstadoPorId(int id_estado)
        {            
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.ESTADO_PEDIDO.FirstOrDefault(ep => ep.id_estado == id_estado);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }            
        }       
    }
}




