using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_Datos;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Capa_Logica
{
    public class ClassPedidoLogica
    {
        private ClassPedido pedido = new ClassPedido();
        public List<string> ErroresValidacion => pedido.ErroresValidacion;
        //Obtener siguiente id pedido
        public int ObtenerSiguienteIdPedido()
        {
            return pedido.ObtenerSiguienteIdPedido();
        }

        //Guardar pedido
        public Boolean GuardarPedido(DateTime fecha_creacion, DateTime fecha_entrega, string id_cliente, string id_estado, string total, string numero_factura, string vendedor)
        {
            try
            {
                PEDIDO nuevoPedido = new PEDIDO
                {
                    fecha_creacion = fecha_creacion,
                    fecha_entrega = fecha_entrega,
                    id_cliente = Convert.ToInt32(id_cliente),
                    id_estado = Convert.ToInt32(id_estado),
                    total = Convert.ToDecimal(total),
                    numero_factura = Convert.ToInt32(numero_factura),
                    vendedor = vendedor
                };
                return pedido.GuardarPedido(nuevoPedido);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al agregar el pedido: " + ex.Message);
                return false;
            }
        }
        //guardar detallepedido
        public Boolean GuardarDetallePedido(string cantidad, string descuento, string ID_detalle_pedido, string id_pedido, string id_producto, string ID_presentacion, string cantidad_bultos, string precio_unitario)
        {
            try
            {
                DETALLE_PEDIDO nuevoDetallePedido = new DETALLE_PEDIDO()
                {
                    cantidad = Convert.ToInt32(cantidad),
                    descuento = Convert.ToDecimal(descuento),
                    ID_detalle_pedido = Convert.ToInt32(ID_detalle_pedido),
                    id_pedido = Convert.ToInt32(id_pedido),
                    id_producto = Convert.ToInt32(id_producto),
                    ID_presentacion = Convert.ToInt32(ID_presentacion),
                    cantidad_bultos = Convert.ToInt32(cantidad_bultos),
                    precio_unitario = Convert.ToDecimal(precio_unitario)
                };
                return pedido.GuardarDetallePedido(nuevoDetallePedido);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Add("Error al agregar el detalle del pedido: " + ex.Message);
                return false;
            }
        }
        //crea pedido y devuelve id_pedido
        public int CrearPedido(DateTime fecha_creacion, DateTime fecha_entrega, int id_cliente, int id_estado, decimal total, string vendedor)
        {
            try
            {
                PEDIDO pedidoNuevo = new PEDIDO()
                {
                    fecha_creacion = fecha_creacion,
                    fecha_entrega = fecha_entrega,
                    id_cliente = Convert.ToInt32(id_cliente),
                    id_estado = Convert.ToInt32(id_estado),
                    total = total,
                    vendedor = vendedor
                };
                return pedido.CrearPedido(pedidoNuevo);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al agregar el pedido: " + ex.Message);
                return -1;
            }
        }
        //guardar detalles
        public bool GuardarDetalles(List<DETALLE_PEDIDO> detalles)
        {
            var duplicados = detalles.GroupBy(d => new { d.id_pedido, d.ID_detalle_pedido })
                         .Where(g => g.Count() > 1)
                         .Select(g => g.Key)
                         .ToList();

            if (duplicados.Any())
            {
                ErroresValidacion.Add("Hay detalles duplicados en el pedido.");
                return false;
            }
            try
            {
                return pedido.GuardarDetalles(detalles);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al agregar los detalles" + ex.Message);
                return false;
            }
        }
        public bool GenerarFactura(int id_pedido)
        {
            bool resultado = pedido.GenerarFactura(id_pedido);

            return resultado;
        }
        //generar numero de factura y devolver numero
        public int GenerarNumeroFactura(int id_pedido)
        {
            return pedido.GenerarNumeroFactura(id_pedido);
        }
        public List<DETALLE_PEDIDO> ObtenerDetallesPedido(int id_pedido)
        {
            return pedido.ObtenerDetallesPedido(id_pedido);
        }
        public PEDIDO ObtenerPedidoPorId(int id_pedido)
        {
            return pedido.ObtenerPedidoPorId(id_pedido);
        }
        public List<ESTADO_PEDIDO> ObtenerEstadosPedido()
        {
            return pedido.ObtenerEstadosPedido();
        }
        public int? ObtenerIdClientePorTexto(string texto)
        {
            return pedido.BuscarIdClientePorTexto(texto);
        }
        //Obtener pedidos por id_cliente
        public List<PEDIDO> ObtenerPedidosPorIdCliente(int id_cliente)
        {
            return pedido.ObtenerPedidosPorIdCliente(id_cliente);

        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorIdCliente(int id_cliente)
        {
            return pedido.ObtenerPedidosEntregadosPorIdCliente(id_cliente);
        }
        //Obtener todos los pedidos
        public List<PEDIDO> ObtenerTodosLosPedidos()
        {
            return pedido.ObtenerTodosLosPedidos();

        }
        public List<PEDIDO> ObtenerPedidosEntregados()
        {
            return pedido.ObtenerPedidosEntregados();
        }
        //Obtener pedido por numero de factura
        public PEDIDO ObtenerPedidoPorNumeroFactura(int numero_factura)
        {
            return pedido.ObtenerPedidoPorNumeroFactura(numero_factura);
        }
        public PEDIDO ObtenerPedidoEntregadoPorNumeroFactura(int numero_factura)
        {
            return pedido.ObtenerPedidoEntregadoPorNumeroFactura(numero_factura);
        }
        //Obtener pedidos por zona seleccionada en comboBox
        public List<PEDIDO> ObtenerPedidosPorZona(int zona)
        {
            return pedido.ObtenerPedidosPorZona(zona);
        }
        public List<PEDIDO> ObtenerPedidosPendientesPorZona(int zona)
        {
            return pedido.ObtenerPedidosPendientesPorZona(zona);
        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorZona(int zona)
        {
            return pedido.ObtenerPedidosEntregadosPorZona(zona);
        }
        public List<PEDIDO> ObtenerPedidosCanceladosPorZona(int zona)
        {
            return pedido.ObtenerPedidosCanceladosPorZona(zona);
        }
        //Obtener pedidos por estado seleccionado en comboBox
        public List<PEDIDO> ObtenerPedidosPorEstado(int estado)
        {
            return pedido.ObtenerPedidosPorEstado(estado);
        }
        //Obtener pedidos por fecha de entrega
        public List<PEDIDO> ObtenerPedidosPorFechaEntrega(DateTime fecha_entrega)
        {
            return pedido.ObtenerPedidosPorFechaEntrega(fecha_entrega);
        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorFechaEntrega(DateTime fecha_entrega)
        {
            return pedido.ObtenerPedidosEntregadosPorFechaEntrega(fecha_entrega);
        }
        //Obtener pedidos por fecha de creacion
        public List<PEDIDO> ObtenerPedidosPorFechaCreacion(DateTime fecha_creacion)
        {
            return pedido.ObtenerPedidosPorFechaCreacion(fecha_creacion);
        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorFechaCreacion(DateTime fecha_creacion)
        {
            return pedido.ObtenerPedidosEntregadosPorFechaCreacion(fecha_creacion);
        }
        //obtener pedidos de un monto menor o igual al ingresado
        public List<PEDIDO> ObtenerPedidosPorMontoMaximo(decimal monto_maximo)
        {
            return pedido.ObtenerPedidosPorMontoMaximo(monto_maximo);
        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorMontoMaximo(decimal montoMaximo)
        {
            return pedido.ObtenerPedidosEntregadosPorMontoMaximo(montoMaximo);
        }
        //obtener pedidos por vendedor
        public List<PEDIDO> ObtenerPedidosPorVendedor(string vendedor)
        {
            return pedido.ObtenerPedidosPorVendedor(vendedor);
        }
        public List<PEDIDO> ObtenerPedidosEntregadosPorVendedor(string vendedor)
        {
            return pedido.ObtenerPedidosEntregadosPorVendedor(vendedor);
        }
        //Actualizar pedido
        public PEDIDO ModificarPedido(int id_pedido, DateTime fecha_creacion, DateTime fecha_entrega, int id_cliente, int id_estado, decimal total, int? numero_factura, string vendedor)
        {
            try
            {
                PEDIDO pedidoModificado = new PEDIDO()
                {
                    id_pedido = id_pedido,
                    fecha_creacion = fecha_creacion,
                    fecha_entrega = fecha_entrega,
                    id_cliente = id_cliente,
                    id_estado = id_estado,
                    total = total,
                    numero_factura = numero_factura,
                    vendedor = vendedor
                };
                return pedido.UpdatePedido(pedidoModificado);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al modificar el pedido: " + ex.Message);
                return null;
            }
        }
        public bool EliminarPedido(int id_pedido)
        {
            try
            {
                return pedido.EliminarPedido(id_pedido);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al eliminar el pedido: " + ex.Message);
                return false;
            }
        }
        //Eliminar detalles de un pedido
        public bool EliminarDetallesPedido(int id_pedido)
        {
            try
            {
                return pedido.EliminarDetallesPedido(id_pedido);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al eliminar los detalles del pedido: " + ex.Message);
                return false;
            }

        }
        public ESTADO_PEDIDO ObtenerEstadoPorId(int id_estado)
        {
            return pedido.ObtenerEstadoPorId(id_estado);
        }
        public bool ActualizarStock(int id_producto, int ID_presentacion, int cantidad)
        {
            try
            {
                return pedido.ActualizarStock(id_producto, ID_presentacion, cantidad);
            }
            catch (Exception ex)
            {
                pedido.ErroresValidacion.Clear();
                pedido.ErroresValidacion.Add("Error al actualizar el stock del producto" + ex.Message);
                return false;
            }
        }
        public List<PEDIDO> ObtenerPedidosConSaldoPendientePorVendedor(string nombreUsuario)
        {
            return pedido.ObtenerPedidosConSaldoPendientePorVendedor(nombreUsuario);
        }
        public List<PEDIDO> ObtenerPedidosSaldadosPorVendedor(string nombreUsuario)
        {
            return pedido.ObtenerPedidosSaldadosPorVendedor(nombreUsuario);
        }
        public List<PEDIDO> ObtenerPedidosSaldadosUltimoMesPorVendedor(string nombreUsuario)
        {
            return pedido.ObtenerPedidosSaldadosUltimoMesPorVendedor(nombreUsuario);
        }
        public List<ProductoPresentacionVenta> ObtenerProductoPresentacionMasVendidosConCantidadPorPreventista(string vendedor)
        {
            return pedido.ObtenerProductoPresentacionMasVendidosConCantidadPorPreventista(vendedor);
        }
        public int? ObtenerIdClientePorTextoYZona(string texto, int idZona)
        {
            return pedido.ObtenerIdClientePorTextoYZona(texto, idZona);
        }
        public List<PEDIDO> ObtenerPedidosPorEstadoYVendedor(int idEstado, string vendedor)
        {
            return pedido.ObtenerPedidosPorEstadoYVendedor(idEstado, vendedor);
        }
        public PEDIDO ObtenerPedidoPorNumeroFacturaYVendedor(int numeroFactura, string vendedor)
        {
            return pedido.ObtenerPedidoPorNumeroFacturaYVendedor(numeroFactura, vendedor);  
        }
        public List<PEDIDO> ObtenerPedidosPorMontoYPreventista(decimal montoMaximo, string nombrePreventista)
        {
            return pedido.ObtenerPedidosPorMontoYPreventista(montoMaximo, nombrePreventista);
        }
        public List<PEDIDO> ObtenerPedidosPorFechaEntregaYPreventista(DateTime fechaEntrega, string preventista)
        {
            return pedido.ObtenerPedidosPorFechaEntregaYPreventista(fechaEntrega, preventista);
        }

        public List<PEDIDO> ObtenerPedidosPorFechaCreacionYPreventista(DateTime fechaCreacion, string preventista)
        {
            return pedido.ObtenerPedidosPorFechaCreacionYPreventista(fechaCreacion, preventista);
        }
        public List<PEDIDO> ObtenerPedidosSaldadosPorVendedorYFechas(string vendedor, DateTime fechaDesde, DateTime fechaHasta)
        {
            return pedido.ObtenerPedidosSaldadosPorVendedorYFechas(vendedor, fechaDesde, fechaHasta);
        }
    }
}
