using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_Datos;

namespace Capa_Logica
{
    public class ClassPagoLogica
    {
        //Instancia de la clase ClassPago
        private ClassPago pagoPedido = new ClassPago();
        private PAGO pago = new PAGO();
        private pedido_pago nuevoPedidoPago = new pedido_pago();
        public List<string> ErroresValidacion => pagoPedido.ErroresValidacion;
        //Obtener métodos de pago
        public List<METODO_PAGO> ObtenerMetodosPago()
        {
            return pagoPedido.ObtenerMetodosPago();
        }
        //Obtener pagos por id_pedido de la tabla pedido_pago
        public List<pedido_pago> ObtenerPedidoPagosPorIdPedido(int idPedido)
        {
            return pagoPedido.ObtenerPagosPorIdPedido(idPedido);
        }
        //Obtener PAGO por id_pago
        public PAGO ObtenerPagoPorId(int id_pago)
        {
            return pagoPedido.ObtenerPagoPorId(id_pago);
        }
        //Obtener METODO_PAGO por id_metodo
        public METODO_PAGO ObtenerMetodoPagoPorId(int idMetodo)
        {
            return pagoPedido.ObtenerMetodoPagoPorId(idMetodo);
        }
        public decimal ObtenerSaldoActual(int idPedido)
        {
            var saldo = pagoPedido.ObtenerSaldoActualPorIdPedido(idPedido);
            return saldo ?? 0; // Si no hay registros, devolvemos 0
        }
        public pedido_pago ObtenerUltimoPedidoPagoPorIdPedido(int idPedido)
        {
            return pagoPedido.ObtenerUltimoPedidoPagoPorIdPedido(idPedido);
        }
        public int CrearNuevoPago(decimal monto, DateTime fecha, int id_metodo, int id_cliente)
        {
            try
            {
                var pago = new PAGO
                {
                    monto = monto,
                    fecha = fecha,
                    id_metodo = id_metodo,
                    id_cliente = id_cliente
                };

                return pagoPedido.CrearNuevoPago(pago); // Devuelve el ID generado
            }
            catch (Exception ex)
            {
                pagoPedido.ErroresValidacion.Clear();
                pagoPedido.ErroresValidacion.Add("Error al agregar el pago: " + ex.Message);
                return -1;
            }
        }

        //Crear nuevo pedido pago
        public Boolean CrearNuevopedido_pago(int id_pedido, int id_pago, decimal saldo)
        {
            try
            {
                nuevoPedidoPago.id_pedido = id_pedido;
                nuevoPedidoPago.id_pago = id_pago;
                nuevoPedidoPago.saldo = saldo;
                return pagoPedido.CrearNuevopedido_pago(nuevoPedidoPago);
            }
            catch (Exception ex)
            {
                pagoPedido.ErroresValidacion.Clear();
                pagoPedido.ErroresValidacion.Add("Error al agregar el pago del pedido: " + ex.Message);
                return false;
            }
        }       
    }
}
