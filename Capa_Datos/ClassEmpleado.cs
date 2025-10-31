using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;


namespace Capa_Datos
{
    public class ClassEmpleado
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        //agregar empleado
        public Boolean AgregarEmpleado(Empleado empleado)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.Empleado.Add(empleado);
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
        //existe empleado
        public bool ExisteEmpleado(int dni)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado.Any(e => e.dni == dni);
            }
        }
        //existe empleado por nombre usuario
        public bool ExisteEmpleadoPorNombreUsuario(string nombreUsuario)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado.Any(e => e.nombre_usuario == nombreUsuario);
            }
        }
        //existe empleado por email
        public bool ExisteEmpleadoPorEmail(string email)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado.Any(e => e.email == email);
            }
        }
        //existe empleado por telefono
        public bool ExisteEmpleadoPorTelefono(long telefono)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado.Any(e => e.telefono == telefono);
            }
        }
        //obtener empleado por nombre de usuario
        public Empleado ObtenerEmpleadoPorNombreUsuario(string nombreUsuario)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado.FirstOrDefault(e => e.nombre_usuario == nombreUsuario);
            }
        }
        //update empleado
        public bool ModificarEmpleado(string nombreUsuario, string nombre, string apellido, string email, string direccion, string dni, string telefono)
        {
            using (var context = new ArimaERPEntities())
            {
                var empleado = context.Empleado.FirstOrDefault(e => e.nombre_usuario == nombreUsuario);
                if (empleado == null)
                    return false;

                // Actualizar campos
                empleado.nombre = nombre;
                empleado.apellido = apellido;
                empleado.email = email;
                empleado.direccion = direccion;
                empleado.dni = Convert.ToInt32(dni);
                empleado.telefono = Convert.ToInt64(telefono);

                context.SaveChanges();
                return true;
            }
        }
        //eliminar empleado con nombre de usuario
        public bool EliminarEmpleado(string nombreUsuario)
        {
            using (var context = new ArimaERPEntities())
            {
                var empleado = context.Empleado.FirstOrDefault(e => e.nombre_usuario == nombreUsuario);
                if (empleado == null)
                    return false;
                context.Empleado.Remove(empleado);
                context.SaveChanges();
                return true;
            }
        }
        //obtener empleados por rol de usuario
        public List<Empleado> ObtenerEmpleadosPorRol(int id_rol)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado
                    .Include(e => e.USUARIOS) // Incluir la entidad relacionada USUARIOS
                    .Where(e => e.USUARIOS.id_rol == id_rol)
                    .ToList();
            }
        }
        //Obtener Empleado por dni
        public Empleado ObtenerEmpleadoPorDni(int dni)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.Empleado.FirstOrDefault(e => e.dni == dni);
            }
        }
        public Empleado BuscarPreventistaPorTexto(string texto)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    texto = texto.ToLower();

                    var resultado = context.Empleado
                        .Include(e => e.USUARIOS)
                        .Where(e => e.USUARIOS.id_rol == 5 &&
                            (
                                e.nombre.ToLower().Contains(texto) ||
                                e.apellido.ToLower().Contains(texto) ||
                                e.email.ToLower().Contains(texto) ||
                                (e.direccion != null && e.direccion.ToLower().Contains(texto)) ||
                                e.nombre_usuario.ToLower().Contains(texto) ||
                                e.dni.ToString().Contains(texto) ||
                                e.telefono.ToString().Contains(texto)
                            ))
                        .FirstOrDefault();

                    return resultado;
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
                return null;
            }
            catch (InvalidOperationException ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error de operación: " + ex.Message);
                return null;
            }
            catch (NullReferenceException ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Referencia nula: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error inesperado: " + ex.Message);
                return null;
            }
        }

    }
}
