using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capa_Datos
{
    public class ClassCliente
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        public Boolean SalvarCliente(CLIENTE cliente)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.CLIENTE.Add(cliente);
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
        //Devuelve lista de clientes
        public static List<CLIENTE> ListarClientes()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.ToList();
            }
        }
        // Obtiene un cliente por su ID
        public static CLIENTE ObtenerClientePorId(int id)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.FirstOrDefault(c => c.id_cliente == id);
            }
        }
        // Actualiza un cliente existente
        public static CLIENTE UpdateCliente(CLIENTE cliente)
        {
            using (var context = new ArimaERPEntities())
            {
                var existingCliente = context.CLIENTE.Find(cliente.id_cliente);
                if (existingCliente != null)
                {
                    context.Entry(existingCliente).CurrentValues.SetValues(cliente);
                    context.SaveChanges();
                }
                return existingCliente;
            }
        }
        //Inactiva un cliente
        public static bool InactivarCliente(int id)
        {
            using (var context = new ArimaERPEntities())
            {
                var cliente = context.CLIENTE.Find(id);
                if (cliente != null)
                {
                    cliente.estado = false; // Asumiendo que 'estado' es un campo booleano
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        //Activa un cliente
        public static bool ActivarCliente(int id)
        {
            using (var context = new ArimaERPEntities())
            {
                var cliente = context.CLIENTE.Find(id);
                if (cliente != null)
                {
                    cliente.estado = true; // Asumiendo que 'estado' es un campo booleano
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        //Existe un cliente por su id
        public static bool ExisteCliente(int id)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Any(c => c.id_cliente == id);
            }
        }
        //Buscar cliente por dni, previa conversion a int
        public static CLIENTE BuscarClientePorDNI(string dni)
        {
            using (var context = new ArimaERPEntities())
            {
                if (int.TryParse(dni, out int dniInt))
                {
                    return context.CLIENTE.FirstOrDefault(c => c.dni == dniInt);
                }
                return null; // Retorna null si la conversión falla
            }

        }
        //existe cliente por dni, previa conversion a int
        public static bool ExisteClientePorDNI(string dni)
        {
            using (var context = new ArimaERPEntities())
            {
                if (int.TryParse(dni, out int dniInt))
                {
                    return context.CLIENTE.Any(c => c.dni == dniInt);
                }
                return false; // Retorna false si la conversión falla
            }
        }
        //Existe cliente por cuit/cuil, previa conversion a long
        public static bool ExisteClientePorCUIT_CUIL(string cuit_cuil)
        {
            using (var context = new ArimaERPEntities())
            {
                if (long.TryParse(cuit_cuil, out long cuitCuilLong))
                {
                    return context.CLIENTE.Any(c => c.cuil_cuit == cuitCuilLong);
                }
                return false; // Retorna false si la conversión falla
            }
        }
        //Existe cliente por email
        public static bool ExisteClientePorEmail(string email)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Any(c => c.email == email);
            }
        }
        //clientes por zona
        public static List<CLIENTE> ClientesPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Where(c => c.id_zona == id_zona).ToList();
            }
        }
        //existe mail de otro cliente
        public static bool ExisteEmailDeOtroCliente(int id_cliente, string email)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Any(c => c.email == email && c.id_cliente != id_cliente);
            }
        }
        //obtener solo clientes inactivos
        public static List<CLIENTE> ObtenerClientesInactivos()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Where(c => c.estado == false).ToList();
            }
        }
        //obtener solo clientes activos
        public static List<CLIENTE> ObtenerClientesActivos()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Where(c => c.estado == true).ToList();
            }
        }
        //obtener clientes por tamaño
        public static List<CLIENTE> ObtenerClientesPorTamanio(int tamanio)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Where(c => c.id_tamano== tamanio).ToList();
            }
        }
        //obtener solo clientes confiables
        public static List<CLIENTE> ObtenerClientesConfiables()
            {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Where(c => c.confiable == true).ToList();
            }
        }
        //obtener solo clientes no confiables
        public static List<CLIENTE> ObtenerClientesNoConfiables()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE.Where(c => c.confiable == false).ToList();
            }
        }
        //Obtener CUENTA_CORRIENTE por id_cliente
        public CUENTA_CORRIENTE ObtenerCuentaCorrientePorIdCliente(int idCliente)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CUENTA_CORRIENTE.FirstOrDefault(c => c.id_cliente == idCliente);
            }
        }
        public bool ActualizarSaldoCuentaCorriente(int idCliente, decimal monto)
        {
            try
            {
                using (var contexto = new ArimaERPEntities())
                {
                    var cuenta = contexto.CUENTA_CORRIENTE.FirstOrDefault(c => c.id_cliente == idCliente);
                    if (cuenta != null)
                    {
                        cuenta.saldo_actual += monto;
                        cuenta.fecha_ultimo_movimiento = DateTime.Today;
                        contexto.SaveChanges();
                        return true;
                    }
                    else
                    {
                        // Si no existe, crear nueva cuenta
                        var nuevaCuenta = new CUENTA_CORRIENTE
                        {
                            id_cliente = idCliente,
                            saldo_actual = monto,
                            fecha_ultimo_movimiento = DateTime.Today
                        };
                        contexto.CUENTA_CORRIENTE.Add(nuevaCuenta);
                        contexto.SaveChanges();
                        return true;
                    }
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
                ErroresValidacion.Add("Error general al guardar saldo en cuenta corriente o crear cuenta corriente: " + ex.Message);

                if (ex.InnerException != null)
                    ErroresValidacion.Add("Inner: " + ex.InnerException.Message);

                if (ex.InnerException?.InnerException != null)
                    ErroresValidacion.Add("Inner deeper: " + ex.InnerException.InnerException.Message);

                return false;
            }
        }
        public List<CLIENTE> ClientesNoConfiablesPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE
                    .Where(c => c.id_zona == id_zona && c.confiable == false)
                    .ToList();
            }
        }
        //Obtener clientes confiables
        public List<CLIENTE> ClientesConfiablesPorZona(int id_zona)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.CLIENTE
                    .Where(c => c.id_zona == id_zona && c.confiable == true)
                    .ToList();
            }
        }
    }
}
