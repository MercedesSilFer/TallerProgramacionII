using Capa_Entidades;
using Capa_Logica;
using Capa_Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ArimaERP.Administrador
{
    public partial class FormRegistrarUsuario : Form
    {        
        ClassRolLogica logicaRol = new ClassRolLogica();
        ClassUsuarioLogica usuarioLog = new ClassUsuarioLogica();
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        private string contrasenaOriginal;

        public FormRegistrarUsuario()
        {
            InitializeComponent();
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            btnModificarUsuario.Enabled = true;
            //activar btnModificar y btnBaja
            btnModificar.Enabled = true;
            btnBaja.Enabled = true;
            btnCancelar.Enabled = true;
            //desactivar btnNuevo
            btnCrear.Enabled = false;
            
            //buscar por nombre de usuario y cargar en texBoxUsuario y texBoxContrasena
            // Obtener el nombre ingresado
            string nombreBuscado = textBoxBusqueda.Text.Trim();
            if (string.IsNullOrEmpty(nombreBuscado))
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario para buscar.");
                return;
            }
            // Buscar el usuario en la capa lógica
            USUARIOS usuarioEncontrado = usuarioLog.ObtenerUsuarioPorNombre(nombreBuscado);

            if (usuarioEncontrado != null)
            {
                // Cargar los datos en los TextBox
                textBoxUsuario.Text = usuarioEncontrado.nombre;
                textBoxContrasena.Text = usuarioEncontrado.contraseña; // Mostrar la contraseña en texto plano
                //focus en textBoxContrasena
                textBoxContrasena.Focus();
                // Seleccionar el rol del usuario
                comboBoxRol.SelectedValue = usuarioEncontrado.id_rol;
                //cargar estado del usuario en el checkBoxEstado
                checkBoxEstado.Checked = usuarioEncontrado.estado;
                // Deshabilitar la edición del textBoxUsuario
                textBoxUsuario.Enabled = false;
                contrasenaOriginal = usuarioEncontrado.contraseña; // Guardás el hash original
                //si el usuario tiene un empleado asociado, cargar los datos en los campos correspondientes
                var empleadoAsociado = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(usuarioEncontrado.nombre);
                if (empleadoAsociado != null)
                {
                    textBoxNombre.Text = empleadoAsociado.nombre;
                    textBoxApellido.Text = empleadoAsociado.apellido;
                    textBoxMail.Text = empleadoAsociado.email;
                    textBoxDireccion.Text = empleadoAsociado.direccion;
                    textBoxDNI.Text = empleadoAsociado.dni.ToString();
                    textBoxTelefono.Text = empleadoAsociado.telefono.ToString();
                    btnCrearEmpleado.Enabled = false;
                    btnModificar.Enabled = true;
                    btnBaja.Enabled = true;
                }
                else
                {
                    // Si no hay empleado asociado, limpiar los campos relacionados
                    textBoxNombre.Clear();
                    textBoxApellido.Clear();
                    textBoxMail.Clear();
                    textBoxDireccion.Clear();
                    textBoxDNI.Clear();
                    textBoxTelefono.Clear();
                    btnCrearEmpleado.Enabled = true;
                    btnBaja.Enabled = false;
                    btnModificar.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Usuario no encontrado.");
                textBoxUsuario.Clear();
                textBoxContrasena.Clear();
            }
        }

        private void textBoxUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //No permitir espacios en blanco
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxUsuario, "No se permiten espacios en blanco.");
            }
            else
            {
                errorProvider1.SetError(textBoxUsuario, "");
            }
            // No permitir más de 20 caracteres
            if (textBoxUsuario.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxUsuario, "No se permiten más de 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxUsuario, "");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //No permitir espacios en blanco
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxContrasena, "No se permiten espacios en blanco.");
            }
            else
            {
                errorProvider1.SetError(textBoxContrasena, "");
            }
            // No permitir más de 20 caracteres
            if (textBoxContrasena.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxContrasena, "No se permiten más de 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxContrasena, "");
            }
        }

        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (textBoxNombre.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxNombre, "Solo se permiten letras.");
            }
            else if (!char.IsControl(e.KeyChar) && textBoxNombre.Text.Length >= 20)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxNombre, "El nombre no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxNombre, "");
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (textBoxApellido.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxApellido, "Solo se permiten letras.");
            }
            else if (!char.IsControl(e.KeyChar) && textBoxApellido.Text.Length >= 20)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxApellido, "El apellido no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxApellido, "");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // No permitir más de 50 caracteres
            if (textBoxMail.Text.Length >= 50 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxMail, "No se permiten más de 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxMail, "");
            }
        }

        private void textBoxDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // No permitir más de 50 caracteres
            if (textBoxDireccion.Text.Length >= 50 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxDireccion, "No se permiten más de 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxDireccion, "");
            }
        }

        private void textBoxDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // No permitir letras ni caracteres especiales
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxDNI, "No se permiten letras ni caracteres especiales.");
            }
            else if (textBoxDNI.Text.Length >= 8 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxDNI, "No se permiten más de 8 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxDNI, "");
            }
        }

        private void textBoxTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // No permitir letras ni caracteres especiales
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxTelefono, "No se permiten letras ni caracteres especiales.");
            }
            else if (textBoxTelefono.Text.Length >= 15 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxTelefono, "No se permiten más de 15 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxTelefono, "");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnModificarUsuario.Enabled = true;
            //habilitar btnNuevo
            btnCrear.Enabled = true;
            //deshabilitar btnModificar y btnBaja
            btnModificar.Enabled = true;
            btnBaja.Enabled = false;
            btnCancelar.Enabled = false;
            comboBoxRol.Enabled = true;
            textBoxUsuario.Enabled = true;
            btnCrearEmpleado.Enabled= true;
            //limpiar campos
            btnLimpiar.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cerrar formulario
            this.Close();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            //solicitar confirmación
            var result = MessageBox.Show("¿Está seguro que desea dar de baja este empleado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //verificar que el usuario existe
                USUARIOS usuarioExistente = usuarioLog.ObtenerUsuarioPorNombre(textBoxUsuario.Text);
                if (usuarioExistente == null)
                {
                    MessageBox.Show("El usuario del empleado no existe.");
                    return;
                }
                //dar de baja empleado
                if (!empleadoLogica.EliminarEmpleado(textBoxUsuario.Text))
                {
                    string errores = string.Join("\n", empleadoLogica.ErroresValidacion);
                    MessageBox.Show("No se pudo dar de baja el empleado:\n" + errores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else {                    
                    MessageBox.Show("Empleado dado de baja con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //habilitar btnNuevo
                btnCrear.Enabled = true;
                //deshabilitar btnModificar y btnBaja
                btnModificar.Enabled = false;
                btnBaja.Enabled = false;
                btnCancelar.Enabled = false;
                //limpiar campos
                btnLimpiar.PerformClick();
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos obligatorios estén completos para crear usuario
            if (string.IsNullOrWhiteSpace(textBoxUsuario.Text) ||
                string.IsNullOrWhiteSpace(textBoxContrasena.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios para crear nuevo usuario ", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un rol para el usuario.", "Rol no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validar que no haya errores en los campos
            if (errorProvider1.GetError(textBoxUsuario) != "" ||
                errorProvider1.GetError(textBoxContrasena) != "" ||
                errorProvider1.GetError(textBoxNombre) != "" ||
                errorProvider1.GetError(textBoxApellido) != "" ||
                errorProvider1.GetError(textBoxMail) != "" ||
                errorProvider1.GetError(textBoxDireccion) != "" ||
                errorProvider1.GetError(textBoxDNI) != "" ||
                errorProvider1.GetError(textBoxTelefono) != ""
            )
            {
                MessageBox.Show("Por favor, corrija los errores en los campos antes de crear el usuario.", "Errores en el formulario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //comprobar que no existe otro usuario con el mismo nombre
            var usuariosExistentes = usuarioLog.ObtenerUsuarios();
            if (usuariosExistentes.Any(u => u.nombre.Equals(textBoxUsuario.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un usuario con ese nombre. Por favor, elija otro nombre de usuario.", "Nombre de usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Crear nuevo usuario
            bool exito = usuarioLog.SalvarUsuario(
            textBoxUsuario.Text,
            textBoxContrasena.Text,
            comboBoxRol.SelectedValue.ToString()
        );
            if (exito)
            {
                MessageBox.Show("Usuario creado con éxito.", "Creación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //limpiar campos
                btnLimpiar.PerformClick();
            }
            else
            {
                // Mostrar errores de validación
                string errores = string.Join("\n", usuarioLog.ErroresValidacion);
                MessageBox.Show("No se pudo crear el usuario debido a los siguientes errores:\n" + errores, "Error al crear usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxMail_Validated(object sender, EventArgs e)
        {
            // Validar formato de email
            try
            {
                var addr = new System.Net.Mail.MailAddress(textBoxMail.Text);
                if (addr.Address != textBoxMail.Text)
                {
                    errorProvider1.SetError(textBoxMail, "Formato de email inválido.");
                }
                else
                {
                    errorProvider1.SetError(textBoxMail, "");
                }
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(textBoxMail.Text))
                {
                    errorProvider1.SetError(textBoxMail, "Formato de email inválido.");
                }
                else
                {
                    errorProvider1.SetError(textBoxMail, "");
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            //limpiar todos los campos y reiniciar comboBox
            textBoxUsuario.Clear();
            textBoxContrasena.Clear();
            textBoxNombre.Clear();
            textBoxApellido.Clear();
            textBoxMail.Clear();
            textBoxDireccion.Clear();
            textBoxDNI.Clear();
            textBoxTelefono.Clear();
            comboBoxRol.SelectedIndex = -1;
            checkBoxEstado.Checked = false;
            textBoxBusqueda.Clear();
            //habilitar btnCrear
            btnCrear.Enabled = true;
            //deshabilitar btnModificarUsuario
            btnModificarUsuario.Enabled = false;
            //habilitar textBoxUsuario
            textBoxUsuario.Enabled = true;
            //limpiar errores
            errorProvider1.Clear();
            btnBaja.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void FormRegistrarUsuario_Load(object sender, EventArgs e)
        {
            btnModificarUsuario.Enabled = false;
            //cargar roles en comboBoxCargarRoles con estado true
            List<ROL> listaRoles = ClassRolLogica.ListarRoles();
            comboBoxRol.DataSource = listaRoles;
            comboBoxRol.DisplayMember = "descripcion";
            comboBoxRol.ValueMember = "id_rol";
            comboBoxRol.SelectedIndex = -1; // No seleccionar ningún elemento al cargar
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsuario.Text))
            {
                MessageBox.Show("Por favor, seleccione un usuario para actualizar.", "Usuario no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //verificar que los campos nombre, apellido, email, direccion, dni y telefono no esten vacíos
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text) ||
                string.IsNullOrWhiteSpace(textBoxApellido.Text) ||
                string.IsNullOrWhiteSpace(textBoxMail.Text) ||
                string.IsNullOrWhiteSpace(textBoxDireccion.Text) ||
                string.IsNullOrWhiteSpace(textBoxDNI.Text) ||
                string.IsNullOrWhiteSpace(textBoxTelefono.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios para actualizar un empleado ", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var empleadoExistente = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(textBoxUsuario.Text);
            if (empleadoExistente == null)
            {
                MessageBox.Show("El empleado no existe. Por favor, verifique el nombre de usuario.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // verificar que no hay otro empleado con el mismo dni, email o telefono
            if (empleadoExistente.dni.ToString() != textBoxDNI.Text && empleadoLogica.ExisteEmpleado(Convert.ToInt32(textBoxDNI.Text)))
            {
                MessageBox.Show("Ya existe otro empleado con ese DNI. Por favor, verifique el DNI.", "DNI duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (empleadoExistente.email != textBoxMail.Text && empleadoLogica.ExisteEmpleadoPorEmail(textBoxMail.Text))
            {
                MessageBox.Show("Ya existe otro empleado con ese email. Por favor, verifique el email.", "Email duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (empleadoExistente.telefono.ToString() != textBoxTelefono.Text && empleadoLogica.ExisteEmpleadoPorTelefono(Convert.ToInt64(textBoxTelefono.Text)))
            {
                MessageBox.Show("Ya existe otro empleado con ese teléfono. Por favor, verifique el teléfono.", "Teléfono duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (empleadoLogica.ActualizarEmpleado(
                textBoxUsuario.Text,
                textBoxNombre.Text,
                textBoxApellido.Text,
                textBoxMail.Text,
                textBoxDireccion.Text,
                textBoxDNI.Text,
                textBoxTelefono.Text))
            {
                MessageBox.Show("Empleado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //actualizar empleado desde la base de datos
                //si el usuario tiene un empleado asociado, cargar los datos en los campos correspondientes
                var empleadoAsociado = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(textBoxUsuario.Text);
                if (empleadoAsociado != null)
                {
                    textBoxNombre.Text = empleadoAsociado.nombre;
                    textBoxApellido.Text = empleadoAsociado.apellido;
                    textBoxMail.Text = empleadoAsociado.email;
                    textBoxDireccion.Text = empleadoAsociado.direccion;
                    textBoxDNI.Text = empleadoAsociado.dni.ToString();
                    textBoxTelefono.Text = empleadoAsociado.telefono.ToString();
                }
                else
                {
                    string errores = string.Join("\n", empleadoLogica.ErroresValidacion);
                    MessageBox.Show("No se pudo actualizar el empleado:\n" + errores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }      

        private void btnCrearEmpleado_Click(object sender, EventArgs e)
        {
            //solicitar la busqueda del usuario en el caso de que no se haya buscado
            if (string.IsNullOrWhiteSpace(textBoxUsuario.Text))
            {
                MessageBox.Show("Por favor, busque y seleccione un usuario antes de crear un empleado.", "Usuario no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //buscar el usuario en la base de datos
                USUARIOS usuarioEncontrado = usuarioLog.ObtenerUsuarioPorNombre(textBoxUsuario.Text);
                if (usuarioEncontrado == null)
                {
                    MessageBox.Show("El usuario no existe. Por favor, verifique el nombre de usuario.", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //verificar que los campos nombre, apellido, email, direccion, dni y telefono no esten vacíos
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text) ||
                string.IsNullOrWhiteSpace(textBoxApellido.Text) ||
                string.IsNullOrWhiteSpace(textBoxMail.Text) ||
                string.IsNullOrWhiteSpace(textBoxDireccion.Text) ||
                string.IsNullOrWhiteSpace(textBoxDNI.Text) ||
                string.IsNullOrWhiteSpace(textBoxTelefono.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios para crear un empleado ", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //
            
            if(empleadoLogica.AgregarEmpleado(textBoxUsuario.Text, textBoxNombre.Text, textBoxApellido.Text, textBoxMail.Text, textBoxDireccion.Text, textBoxDNI.Text, textBoxTelefono.Text))
            {
                // Si todos los campos son válidos mostrar mensaje de éxito
                MessageBox.Show("Empleado guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Mostrar errores de validación
                string errores = string.Join("\n", empleadoLogica.ErroresValidacion);
                MessageBox.Show("No se pudo crear el empleado debido a los siguientes errores:\n" + errores, "Error al crear empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            string nuevaContrasena = textBoxContrasena.Text;
            string contrasenaFinal;
            if(string.IsNullOrWhiteSpace(textBoxUsuario.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre de usuario para modificar.");
                return;
            }
            USUARIOS usuarioExistente = usuarioLog.ObtenerUsuarioPorNombre(textBoxUsuario.Text);
            if (string.IsNullOrWhiteSpace(textBoxContrasena.Text))
            {                
                MessageBox.Show("Por favor, complete el campo contraseña para modificar el usuario ", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxContrasena.Text = usuarioExistente.contraseña;
                textBoxContrasena.Focus();
                return;
            }
            //verificar que el usuario existe            
            if (usuarioExistente == null)
            {
                MessageBox.Show("El usuario no existe.");
                return;
            }
            // Verificar si la contraseña ingresada es distinta (asumiendo que el campo muestra texto plano)
            if (!PasswordHasher.VerifyPassword(nuevaContrasena, usuarioExistente.contraseña))
            {
                contrasenaFinal = nuevaContrasena; // Se encripta en capa lógica
            }
            else
            {
                contrasenaFinal = usuarioExistente.contraseña; // Ya está encriptada
            }
            
            if (comboBoxRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un rol para el usuario.", "Rol no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validar que no haya errores en los campos
            if (errorProvider1.GetError(textBoxUsuario) != "" ||
                errorProvider1.GetError(textBoxContrasena) != "" ||
                errorProvider1.GetError(textBoxNombre) != "" ||
                errorProvider1.GetError(textBoxApellido) != "" ||
                errorProvider1.GetError(textBoxMail) != "" ||
                errorProvider1.GetError(textBoxDireccion) != "" ||
                errorProvider1.GetError(textBoxDNI) != "" ||
                errorProvider1.GetError(textBoxTelefono) != ""
            )
            {
                MessageBox.Show("Por favor, corrija los errores en los campos antes de modificar el usuario.", "Errores en el formulario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //obtener los valores del formulario
            string nombre = textBoxUsuario.Text;
            string id_rol = comboBoxRol.SelectedValue.ToString(); // Asegurate que SelectedValue esté bien configurado
            bool estado = checkBoxEstado.Checked;
            DialogResult confirmacion = MessageBox.Show(
            "¿Está seguro de que desea modificar los datos del usuario?",
            "Confirmar modificación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
            {
                MessageBox.Show("Modificación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Llamar a capa lógica
            USUARIOS usuarioModificado = usuarioLog.ModificarUsuario(nombre, contrasenaFinal, id_rol, estado);

            if (usuarioModificado != null)
            {
                MessageBox.Show("Usuario modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpiar.PerformClick();
            }
            else
            {
                MessageBox.Show("No se pudo modificar el usuario. Verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
