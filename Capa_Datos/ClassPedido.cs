using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                        context.DETALLE_PEDIDO.Add(detalle);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
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
        public List<PEDIDO> ObtenerPedidosEntregadosPorIdCliente(int id_cliente)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .Where(p => p.id_cliente == id_cliente && p.id_estado == 3)
                              .ToList();
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
        public List<PEDIDO> ObtenerPedidosEntregados()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .Where(p => p.id_estado == 3)
                              .ToList();
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
        public PEDIDO ObtenerPedidoEntregadoPorNumeroFactura(int numero_factura)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .FirstOrDefault(p => p.numero_factura == numero_factura && p.id_estado == 3);
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

        public List<PEDIDO> ObtenerPedidosPendientesPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = from p in context.PEDIDO
                              join c in context.CLIENTE on p.id_cliente equals c.id_cliente
                              where c.id_zona == id_zona
                              && p.id_estado == 1
                              select p;
                return pedidos.ToList();
            }
        }

        public List<PEDIDO> ObtenerPedidosEntregadosPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = from p in context.PEDIDO
                              join c in context.CLIENTE on p.id_cliente equals c.id_cliente
                              where c.id_zona == id_zona
                              && p.id_estado == 3
                              select p;
                return pedidos.ToList();
            }
        }

        public List<PEDIDO> ObtenerPedidosCanceladosPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = from p in context.PEDIDO
                              join c in context.CLIENTE on p.id_cliente equals c.id_cliente
                              where c.id_zona == id_zona
                              && p.id_estado == 4
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
        public List<PEDIDO> ObtenerPedidosEntregadosPorFechaEntrega(DateTime fecha_entrega)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .Where(p => DbFunctions.TruncateTime(p.fecha_entrega) == fecha_entrega.Date
                                       && p.id_estado == 3)
                              .ToList();
            }
        }

        public List<PEDIDO> ObtenerPedidosPorFechaCreacion(DateTime fecha_creacion)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO.Where(p => p.fecha_creacion == fecha_creacion).ToList();
            }
        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorFechaCreacion(DateTime fecha_creacion)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .Where(p => DbFunctions.TruncateTime(p.fecha_creacion) == fecha_creacion.Date
                                       && p.id_estado == 3)
                              .ToList();
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

        public List<PEDIDO> ObtenerPedidosPorMontoYPreventista(decimal montoMaximo, string nombrePreventista)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                    .Where(p => p.total <= montoMaximo && p.vendedor == nombrePreventista)
                    .ToList();
            }
        }

        public List<PEDIDO> ObtenerPedidosEntregadosPorMontoMaximo(decimal montoMaximo)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .Where(p => p.total <= montoMaximo && p.id_estado == 3)
                              .ToList();
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
        public List<PEDIDO> ObtenerPedidosEntregadosPorVendedor(string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                              .Where(p => p.vendedor == vendedor && p.id_estado == 3)
                              .ToList();
            }
        }

        public bool EliminarPedido(int id_pedido)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var pedido = context.PEDIDO.FirstOrDefault(p => p.id_pedido == id_pedido);
                    if (pedido != null)
                    {
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


        public bool ActualizarStock(int id_producto, int ID_presentacion, int cantidad)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var registroStock = context.stock.FirstOrDefault(s =>
                        s.id_producto == id_producto &&
                        s.ID_presentacion == ID_presentacion);

                    if (registroStock != null)
                    {
                        registroStock.stock_actual += cantidad;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        ErroresValidacion.Clear();
                        ErroresValidacion.Add("No se encontró el registro de stock para el producto y presentación especificados.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error al actualizar el stock: " + ex.Message);
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


        public List<PEDIDO> ObtenerPedidosPorZonaYEstado(int id_zona, int id_estado)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = from p in context.PEDIDO
                              join c in context.CLIENTE on p.id_cliente equals c.id_cliente
                              where c.id_zona == id_zona && p.id_estado == id_estado
                              select p;

                return pedidos.ToList();
            }
        }
        public List<PEDIDO> ObtenerPedidosConSaldoPendientePorVendedor(string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = context.PEDIDO
                    .Where(p => p.vendedor == vendedor)
                    .ToList();

                var resultado = new List<PEDIDO>();

                foreach (var pedido in pedidos)
                {
                    var ultimoPago = context.pedido_pago
                        .Where(pp => pp.id_pedido == pedido.id_pedido)
                        .OrderByDescending(pp => pp.id_pago)
                        .FirstOrDefault();

                    if (ultimoPago == null || ultimoPago.saldo > 0)
                    {
                        resultado.Add(pedido);
                    }
                }

                return resultado;

            }
        }
        public List<PEDIDO> ObtenerPedidosSaldadosPorVendedor(string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidos = context.PEDIDO
                    .Where(p => p.vendedor == vendedor)
                    .ToList();

                var resultado = new List<PEDIDO>();

                foreach (var pedido in pedidos)
                {
                    var ultimoPago = context.pedido_pago
                        .Where(pp => pp.id_pedido == pedido.id_pedido)
                        .OrderByDescending(pp => pp.id_pago)
                        .FirstOrDefault();

                    if (ultimoPago.saldo == 0)
                    {
                        resultado.Add(pedido);
                    }
                }

                return resultado;

            }
        }
        public List<PEDIDO> ObtenerPedidosSaldadosUltimoMesPorVendedor(string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                // Obtener primer y último día del mes actual
                DateTime inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

                var pedidos = context.PEDIDO
                    .Where(p => p.vendedor == vendedor &&
                                p.fecha_entrega >= inicioMes &&
                                p.fecha_entrega <= finMes)
                    .ToList();

                var resultado = new List<PEDIDO>();

                foreach (var pedido in pedidos)
                {
                    var ultimoPago = context.pedido_pago
                        .Where(pp => pp.id_pedido == pedido.id_pedido)
                        .OrderByDescending(pp => pp.id_pago)
                        .FirstOrDefault();

                    if (ultimoPago != null && ultimoPago.saldo == 0)
                    {
                        resultado.Add(pedido);
                    }
                }

                return resultado;
            }
        }
        public List<ProductoPresentacionVenta> ObtenerProductoPresentacionMasVendidosConCantidadPorPreventista(string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                var pedidosSaldados = context.PEDIDO
                    .Where(p => p.vendedor == vendedor)
                    .ToList()
                    .Where(p =>
                    {
                        var ultimoPago = context.pedido_pago
                            .Where(pp => pp.id_pedido == p.id_pedido)
                            .OrderByDescending(pp => pp.id_pago)
                            .FirstOrDefault();

                        return ultimoPago != null && ultimoPago.saldo == 0;
                    })
                    .Select(p => p.id_pedido)
                    .ToList();

                var detalles = context.DETALLE_PEDIDO
                    .Where(dp => pedidosSaldados.Contains(dp.id_pedido))
                    .ToList();

                var agrupados = detalles
                    .GroupBy(dp => new { dp.id_producto, dp.ID_presentacion })
                    .Select(g =>
                    {
                        var unidadesBulto = context.producto_presentacion
                            .Where(pp => pp.id_producto == g.Key.id_producto && pp.ID_presentacion == g.Key.ID_presentacion)
                            .Select(pp => pp.unidades_bulto)
                            .FirstOrDefault();

                        var cantidadTotal = g.Sum(dp =>
                            (dp.cantidad ?? 0) + ((dp.cantidad_bultos ?? 0) * unidadesBulto)
                        );

                        return new
                        {
                            g.Key.id_producto,
                            g.Key.ID_presentacion,
                            cantidadTotal
                        };
                    })
                    .OrderByDescending(x => x.cantidadTotal)
                    .ToList();

                var resultado = new List<ProductoPresentacionVenta>();
                foreach (var item in agrupados)
                {
                    var pp = context.producto_presentacion
                        .FirstOrDefault(x => x.id_producto == item.id_producto && x.ID_presentacion == item.ID_presentacion);

                    if (pp != null)
                    {
                        resultado.Add(new ProductoPresentacionVenta
                        {
                            ProductoPresentacion = pp,
                            CantidadVendida = item.cantidadTotal
                        });
                    }
                }

                return resultado;
            }
        }
        public int? ObtenerIdClientePorTextoYZona(string texto, int idZona)
        {
            using (var context = new ArimaERPEntities())
            {
                int dniBuscado;
                bool esDni = int.TryParse(texto, out dniBuscado);

                var cliente = context.CLIENTE
                    .Where(c => c.id_zona == idZona && c.estado == true &&
                                (esDni ? c.dni == dniBuscado : c.email == texto))
                    .Select(c => (int?)c.id_cliente)
                    .FirstOrDefault();

                return cliente;
            }
        }
        public List<PEDIDO> ObtenerPedidosPorEstadoYVendedor(int idEstado, string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                    .Where(p => p.id_estado == idEstado && p.vendedor == vendedor)
                    .ToList();
            }
        }
        public PEDIDO ObtenerPedidoPorNumeroFacturaYVendedor(int numeroFactura, string vendedor)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                    .FirstOrDefault(p => p.numero_factura == numeroFactura && p.vendedor == vendedor);
            }
        }
        public List<PEDIDO> ObtenerPedidosPorFechaEntregaYPreventista(DateTime fechaEntrega, string preventista)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                    .Where(p => p.fecha_entrega == fechaEntrega && p.vendedor == preventista)
                    .ToList();
            }
        }

        public List<PEDIDO> ObtenerPedidosPorFechaCreacionYPreventista(DateTime fechaCreacion, string preventista)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PEDIDO
                    .Where(p => p.fecha_creacion == fechaCreacion && p.vendedor == preventista)
                    .ToList();
            }
        }
    }
}




