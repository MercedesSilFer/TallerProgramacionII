using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidades;
using System.Security.Cryptography;
using Capa_Utilidades;
using System.Runtime.CompilerServices;

namespace Capa_Logica
{

    public class ClassUsuarioLogica
    {
        private ClassUsuarios usuario = new ClassUsuarios();
        public List<string> ErroresValidacion => usuario.ErroresValidacion;

        //obtener usuarios por rol
        public List<USUARIOS> ObtenerUsuariosPorRol(int id_rol)
        {
            return usuario.ObtenerUsuariosPorRol(id_rol);
        }
        //existe usuario segun su nombre
        public USUARIOS ObtenerUsuarioPorNombre(string nombre)
        {
            return usuario.ObtenerUsuarioPorNombre(nombre); // desde capa datos
        }

        //agregar nuevo usuario
        public Boolean SalvarUsuario(string nombre, string password, string id_rol)
        {
            try
            {
                //encriptar contraseña               

                // Encriptar contraseña con SHA256
                string hashedPassword = PasswordHasher.HashPassword(password);


                USUARIOS nuevoUsuario = new USUARIOS
                {
                    nombre = nombre,
                    contraseña = hashedPassword,
                    id_rol = Convert.ToInt32(id_rol),
                    estado = true,
                };
                return usuario.SalvarUsuario(nuevoUsuario);
            }
            catch (Exception ex)
            {
                usuario.ErroresValidacion.Clear();
                usuario.ErroresValidacion.Add("Error al agregar el usuario: " + ex.Message);
                return false;
            }
        }
        //obtener todos los usuarios
        public List<USUARIOS> ObtenerUsuarios()
        {
            return ClassUsuarios.ListarUsuarios();
        }
        //modificar usuario mediante su nombre
        public USUARIOS ModificarUsuario(string nombre, string password, string id_rol, bool estado)
        {
            try
            {
                string hashedPassword;

                // Si la contraseña ya está encriptada, no la vuelvas a encriptar
                if (PasswordHasher.IsHashed(password))
                {
                    hashedPassword = password;
                }
                else
                {
                    hashedPassword = PasswordHasher.HashPassword(password);
                }
                //crear objeto usuario modificado
                USUARIOS usuarioModificado = new USUARIOS
                {
                    nombre = nombre,
                    contraseña = hashedPassword,
                    id_rol = Convert.ToInt32(id_rol),
                    estado = estado,
                };
                return usuario.ModificarUsuario(usuarioModificado);    
                
            }
            catch (Exception ex)
            {
                usuario.ErroresValidacion.Clear();
                usuario.ErroresValidacion.Add("Error al modificar el usuario: " + ex.Message);
                return null;
            }

        }
        public string ObtenerUsuarioPreventistaPorZona(int idZona)
        {
            return usuario.ObtenerUsuarioPreventistaPorZona(idZona);
        }


    }
}
    

