using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;


namespace Capa_Datos
{
    public class ClassUsuarios
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        //usuarios por rol
        public List<USUARIOS> ObtenerUsuariosPorRol(int id_rol)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.USUARIOS.Where(u => u.id_rol == id_rol).ToList();
            }
        }
        //agregar nuevo usuario
        public Boolean SalvarUsuario(USUARIOS nuevoUsuario)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    context.USUARIOS.Add(nuevoUsuario);
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
        //obtener todos los usuarios
        public static List<USUARIOS> ListarUsuarios()
        {
            using (var context = new ArimaERPEntities())
            {
                return context.USUARIOS.ToList();
            }
        }
        //obtener usuario por nombre
        
        public USUARIOS ObtenerUsuarioPorNombre(string nombre)
        {
            using (var context = new ArimaERPEntities()) 
            {
                return context.USUARIOS
                    .Include(u => u.ROL) // opcional, si querés traer el rol también
                    .FirstOrDefault(u => u.nombre == nombre);
            }
        }
        //modificar usuario
        public USUARIOS ModificarUsuario(USUARIOS usuarioModificado)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var usuarioExistente = context.USUARIOS.Find(usuarioModificado.nombre);
                    if (usuarioExistente == null)
                    {
                        ErroresValidacion.Clear();
                        ErroresValidacion.Add("El usuario no existe.");
                        return usuarioExistente;
                    }
                    // actualizar solo campos modificados
                    usuarioModificado.nombre = usuarioExistente.nombre; // Asegurarse de no cambiar el nombre
                    context.Entry(usuarioExistente).CurrentValues.SetValues(usuarioModificado);
                    context.SaveChanges();
                    return usuarioExistente;

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
        }
        //Obtener nombre preventista (usuario id_rol = 5) por id_zona
        public string ObtenerUsuarioPreventistaPorZona(int idZona)
        {
            try
            {
                using (var contexto = new ArimaERPEntities())
                {
                    var zona = contexto.ZONA.FirstOrDefault(z => z.id_zona == idZona);
                    return zona?.preventista; // preventsta es FK a USUARIOS.nombre
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add("Error al obtener el preventista: " + ex.Message);
                return null;
            }
        }

    }
}
