using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using System.Data.Entity;
using Capa_Datos;

namespace Capa_Logica
{
    public class ClassEmpleadoLogica
    {
        private ClassEmpleado empleado = new ClassEmpleado();
        public List<string> ErroresValidacion => empleado.ErroresValidacion;
        //agregar empleado
        public Boolean AgregarEmpleado(string nombre_usuario, string nombre, string apellido, string email, string direccion, string dni, string telefono)
        {
            try
            {
                //validar si el empleado ya existe por dni o nombre de usuario
                if (empleado.ExisteEmpleado(Convert.ToInt32(dni)))
                {
                    empleado.ErroresValidacion.Clear();
                    empleado.ErroresValidacion.Add("El empleado con DNI " + dni + " ya existe.");
                    return false;
                }
                if (empleado.ExisteEmpleadoPorNombreUsuario(nombre_usuario))
                {
                    empleado.ErroresValidacion.Clear();
                    empleado.ErroresValidacion.Add("El empleado con nombre de usuario " + nombre_usuario + " ya existe.");
                    return false;
                }
                if (empleado.ExisteEmpleadoPorEmail(email))
                {
                    empleado.ErroresValidacion.Clear();
                    empleado.ErroresValidacion.Add("El empleado con email " + email + " ya existe.");
                    return false;
                }
                if (empleado.ExisteEmpleadoPorTelefono(Convert.ToInt64(telefono)))
                {
                    empleado.ErroresValidacion.Clear();
                    empleado.ErroresValidacion.Add("El empleado con telefono " + telefono + " ya existe.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error al validar el empleado: " + ex.Message);
                return false;
            }

            try
            {
                Empleado nuevoEmpleado = new Empleado
                {
                    nombre_usuario = nombre_usuario,
                    nombre = nombre,
                    apellido = apellido,
                    email = email,
                    direccion = direccion,
                    dni = Convert.ToInt32(dni),
                    telefono = Convert.ToInt64(telefono),

                };
                return empleado.AgregarEmpleado(nuevoEmpleado);
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error al agregar el usuario: " + ex.Message);
                return false;
            }
        }
        //obtener empeado por nombre de usuario
        public Empleado ObtenerEmpleadoPorNombreUsuario(string nombre_usuario)
        {
            try
            {
                return empleado.ObtenerEmpleadoPorNombreUsuario(nombre_usuario);
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error al obtener el empleado: " + ex.Message);
                return null;
            }
        }
        public bool ActualizarEmpleado(string nombreUsuario, string nombre, string apellido, string email, string direccion, string dni, string telefono)
        {
            // Llamar a capa de datos
            return empleado.ModificarEmpleado(nombreUsuario, nombre, apellido, email, direccion, dni, telefono);
        }
        //existe empleado por dni
        public bool ExisteEmpleado(int dni)
        {
            return empleado.ExisteEmpleado(dni);
        }
        //existe empleado por nombre usuario
        public bool ExisteEmpleadoPorNombreUsuario(string nombreUsuario)
        {
            return empleado.ExisteEmpleadoPorNombreUsuario(nombreUsuario);
        }
        //existe empleado por email
        public bool ExisteEmpleadoPorEmail(string email)
        {
            return empleado.ExisteEmpleadoPorEmail(email);
        }
        //existe empleado por telefono
        public bool ExisteEmpleadoPorTelefono(long telefono)
        {
            return empleado.ExisteEmpleadoPorTelefono(telefono);
        }
        //eliminar empleado por nombre de usuario
        public bool EliminarEmpleado(string nombreUsuario)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var empleadoAEliminar = context.Empleado.FirstOrDefault(e => e.nombre_usuario == nombreUsuario);
                    if (empleadoAEliminar == null)
                    {
                        empleado.ErroresValidacion.Clear();
                        empleado.ErroresValidacion.Add("El empleado con nombre de usuario " + nombreUsuario + " no existe.");
                        return false;
                    }
                    context.Empleado.Remove(empleadoAEliminar);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error al eliminar el empleado: " + ex.Message);
                return false;
            }
        }
        //obtener empleados por rol de usuario
        public List<Empleado> ObtenerEmpleadosPorRol(int id_rol)
        {
            try
            {
                return empleado.ObtenerEmpleadosPorRol(id_rol);
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error al obtener los empleados por rol: " + ex.Message);
                return new List<Empleado>();
            }
        }
        //Obtener Empleado por DNI
        public Empleado ObtenerEmpleadoPorDNI(int dni)
        {
            try
            {
                return empleado.ObtenerEmpleadoPorDni(dni);
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error al obtener el empleado por DNI: " + ex.Message);
                return null;
            }
        }
        public Empleado BuscarPreventistaPorTexto(string texto)
        {
            try
            {
                return empleado.BuscarPreventistaPorTexto(texto);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                empleado.ErroresValidacion.Clear();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        string mensaje = $"Entidad: {validationErrors.Entry.Entity.GetType().Name}, Campo: {error.PropertyName}, Error: {error.ErrorMessage}";
                        empleado.ErroresValidacion.Add(mensaje);
                    }
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error de operación: " + ex.Message);
                return null;
            }
            catch (NullReferenceException ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Referencia nula: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                empleado.ErroresValidacion.Clear();
                empleado.ErroresValidacion.Add("Error inesperado: " + ex.Message);
                return null;
            }
        }

    }
}
