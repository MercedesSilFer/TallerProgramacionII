using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class ClassPago
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        //Obtener métodos de pago
        public List<METODO_PAGO> ObtenerMetodosPago()
        {
            using (var contexto = new ArimaERPEntities())
            {
                return contexto.METODO_PAGO.ToList();
            }
        }
        //Obtener pedido_pago por id_pedido
        public List<pedido_pago> ObtenerPagosPorIdPedido(int idPedido)
        {
            try
            {
                using (var contexto = new ArimaERPEntities())
                {
                    return contexto.pedido_pago.Where(pp => pp.id_pedido == idPedido).ToList();

                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //Obtener pago por id_pago
        public PAGO ObtenerPagoPorId(int idPago)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.PAGO.FirstOrDefault(p => p.id_pago == idPago);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //Obtener METODO_PAGO por id_metodo
        public METODO_PAGO ObtenerMetodoPagoPorId(int idMetodo)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.METODO_PAGO.FirstOrDefault(mp=> mp.id_metodo == idMetodo);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        public decimal? ObtenerSaldoActualPorIdPedido(int idPedido)
        {
            try
            {
                using (var contexto = new ArimaERPEntities())
                {
                    var ultimoPedidoPago = contexto.pedido_pago
                        .Join(contexto.PAGO,
                              pp => pp.id_pago,
                              p => p.id_pago,
                              (pp, p) => new { PedidoPago = pp, Pago = p })
                        .Where(x => x.PedidoPago.id_pedido == idPedido)
                        .OrderByDescending(x => x.Pago.fecha)
                        .Select(x => x.PedidoPago.saldo)
                        .FirstOrDefault();

                    return ultimoPedidoPago;
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }

        public pedido_pago ObtenerUltimoPedidoPagoPorIdPedido(int idPedido)
        {
            try
            {
                using (var contexto = new ArimaERPEntities())
                {
                    var ultimoPedidoPago = contexto.pedido_pago
                        .Join(contexto.PAGO,
                              pp => pp.id_pago,
                              p => p.id_pago,
                              (pp, p) => new { PedidoPago = pp, Pago = p })
                        .Where(x => x.PedidoPago.id_pedido == idPedido)
                        .OrderByDescending(x => x.Pago.id_pago)
                        .Select(x => x.PedidoPago)
                        .FirstOrDefault();

                    return ultimoPedidoPago;
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }

        //Crear nuevo PAGO
        public int CrearNuevoPago(PAGO pago)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.PAGO.Add(pago);
                    context.SaveChanges();
                    return pago.id_pago; // Devuelve el ID generado
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
                return -1; // Indica error
            }
        }

        //Crear nuevo pedido_pago
        public Boolean CrearNuevopedido_pago(pedido_pago nuevoPedidoPago)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.pedido_pago.Add(nuevoPedidoPago);
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
        //Obtener 
    }
}
