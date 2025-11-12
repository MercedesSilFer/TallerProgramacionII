using Capa_Entidades;
using Capa_Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ArimaERP.EmpleadoClientes
{
    public partial class FormCliente : Form
    {
        private ClassClienteLogica clienteLogica = new ClassClienteLogica();
        ClassAuditoriaLogica auditoria = new ClassAuditoriaLogica();
        private bool limpiarPresionado = false;

        public FormCliente()
        {
            InitializeComponent();
        }



        private void txtBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (txtBoxNombre.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxNombre, "Solo se permiten letras.");
            }
            else if (!char.IsControl(e.KeyChar) && txtBoxNombre.Text.Length >= 30)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxNombre, "El nombre no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtBoxNombre, "");
            }
        }
        private void txtBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (txtBoxApellido.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxApellido, "Solo se permiten letras.");
            }
            else if (!char.IsControl(e.KeyChar) && txtBoxApellido.Text.Length >= 30)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxApellido, "El apellido no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtBoxApellido, "");
            }
        }
        private void txtBoxDni_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo números y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxDni, "Solo se permiten números.");
            }
            // Limitar a 8 caracteres
            else if (!char.IsControl(e.KeyChar) && txtBoxDni.Text.Length >= 8)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxDni, "Debe ingresar entre 7 y 8 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtBoxDni, "");
            }
        }

        // Validar longitud al perder el foco
        private void txtBoxDni_Validating(object sender, CancelEventArgs e)
        {

            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxDni.Text)|| this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxDni, "");
                return;
            }

            // Validar solo si hay caracteres
            if (txtBoxDni.Text.Length < 7 || txtBoxDni.Text.Length > 8)
            {
                errorProvider1.SetError(txtBoxDni, "El DNI debe tener entre 7 y 8 dígitos.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxDni, "");
            }
        }



        private void txtBoxCuil_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxCuil, "Solo se permiten números.");
            }
            // Limitar a 11 caracteres
            else if (!char.IsControl(e.KeyChar) && txtBoxCuil.Text.Length >= 11)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxCuil, "El CUIL debe tener 11 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtBoxCuil, "");
            }
        }
        // Validar longitud al perder el foco
        private void txtBoxCuil_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxCuil.Text)|| this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxCuil, "");
                return;
            }
            // Validar solo si hay caracteres
            if (txtBoxCuil.Text.Length != 11)
            {
                errorProvider1.SetError(txtBoxCuil, "El CUIL debe tener 11 dígitos.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxCuil, "");
            }
        }

        private void txtBoxMovil_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxMovil, "Solo se permiten números.");
            }
            // Limitar a 10 caracteres
            else if (!char.IsControl(e.KeyChar) && txtBoxMovil.Text.Length >= 10)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxMovil, "El número de móvil debe tener 10 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtBoxMovil, "");
            }

        }

        private void txtMail_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío o se presiono btnLimpiar o cerrar el formulario
            // 
            if (string.IsNullOrWhiteSpace(txtMail.Text)|| this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtMail, "");
                limpiarPresionado = false;
                return;
            }
            // Validar formato de email básico
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtMail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(txtMail, "Formato de email inválido.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtMail, "");
            }
        }

        private void txtBoxCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (txtBoxCalle.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, números, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxCalle, "Solo se permiten letras y números.");
            }
            else if (!char.IsControl(e.KeyChar) && txtBoxCalle.Text.Length >= 20)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxCalle, "La calle no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtBoxCalle, "");
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtNumero, "Solo se permiten números.");
            }
            else if (!char.IsControl(e.KeyChar) && txtNumero.Text.Length >= 5)
            {
                e.Handled = true;
                errorProvider1.SetError(txtNumero, "El número no debe exceder los 5 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtNumero, "");
            }
        }

        private void txtBoxLocalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (txtBoxLocalidad.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxLocalidad, "Solo se permiten letras.");
            }
            else if (!char.IsControl(e.KeyChar) && txtBoxLocalidad.Text.Length >= 20)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxLocalidad, "La localidad no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtBoxLocalidad, "");
            }

        }

        private void txtBoxNombre_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxNombre.Text)|| this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxNombre, "");
                return;
            }
            //Validar que tenga al menos 2 caracteres
            if (txtBoxNombre.Text.Length < 2)
            {
                errorProvider1.SetError(txtBoxNombre, "El nombre debe tener al menos 2 caracteres.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxNombre, "");
            }
        }

        private void txtBoxApellido_Validating(object sender, CancelEventArgs e)
        {
                        // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxApellido.Text)|| this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxApellido, "");
                return;
            }
            //Validar que tenga al menos 2 caracteres
            if (txtBoxApellido.Text.Length < 2)
            {
                errorProvider1.SetError(txtBoxApellido, "El apellido debe tener al menos 2 caracteres.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxApellido, "");
            }
        }

        private void txtBoxCalle_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxCalle.Text) || this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxCalle, "");
                return;
            }
            //Validar que tenga al menos 2 caracteres
            if (txtBoxCalle.Text.Length < 2)
            {
                errorProvider1.SetError(txtBoxCalle, "La calle debe tener al menos 2 caracteres.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxCalle, "");
            }
        }

        private void txtBoxLocalidad_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxLocalidad.Text) || this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxLocalidad, "");
                return;
            }
            //Validar que tenga al menos 3 caracteres
            if (txtBoxLocalidad.Text.Length < 3)
            {
                errorProvider1.SetError(txtBoxLocalidad, "La localidad debe tener al menos 3 caracteres.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxLocalidad, "");
            }
        }

        private void txtBoxProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (txtBoxProvincia.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir solo letras, espacios y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxProvincia, "Solo se permiten letras.");
            }
            else if (!char.IsControl(e.KeyChar) && txtBoxProvincia.Text.Length >= 20)
            {
                e.Handled = true;
                errorProvider1.SetError(txtBoxProvincia, "La provincia no debe exceder los 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtBoxProvincia, "");
            }
        }

        private void txtBoxProvincia_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtBoxProvincia.Text) || this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtBoxProvincia, "");
                return;
            }
            //Validar que tenga al menos 5 caracteres
            if (txtBoxProvincia.Text.Length < 5)
            {
                errorProvider1.SetError(txtBoxProvincia, "La provincia debe tener al menos 5 caracteres.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBoxProvincia, "");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que no existan campos editables vacíos, combobox sin seleccionar o datepicker con fecha futura, excepto razon social que puede estar vacío
            if (string.IsNullOrWhiteSpace(txtBoxNombre.Text) ||
                string.IsNullOrWhiteSpace(txtBoxApellido.Text) ||
                string.IsNullOrWhiteSpace(txtBoxDni.Text) ||
                string.IsNullOrWhiteSpace(txtBoxCuil.Text) ||
                string.IsNullOrWhiteSpace(txtBoxMovil.Text) ||
                string.IsNullOrWhiteSpace(txtMail.Text) ||
                string.IsNullOrWhiteSpace(txtBoxCalle.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtBoxLocalidad.Text) ||
                string.IsNullOrWhiteSpace(txtBoxProvincia.Text) ||
                string.IsNullOrWhiteSpace(txtCodigoPostal.Text)
                )
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if((int)comboBoxSeleccionarTamano.SelectedValue == 0 ||
                (int)comboBoxSeleccionarZona.SelectedValue == 0 ||
                comboBoxCondicionFrenteIVA.SelectedIndex == 0 ||
                dateTimePickerFechaAlta.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Por favor seleccione una opción válida en los combobox y asegúrese que la fecha de alta no sea futura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clienteLogica.ExisteClientePorDni(txtBoxDni.Text))
            {
                MessageBox.Show("Ya existe un cliente con el DNI ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clienteLogica.ExisteClientePorCuitCuil(txtBoxCuil.Text))
            {
                MessageBox.Show("Ya existe un cliente con el CUIT/CUIL ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clienteLogica.ExisteClientePorEmail(txtMail.Text))
            {
                MessageBox.Show("Ya existe un cliente con el email ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string id_tamano = comboBoxSeleccionarTamano.SelectedValue.ToString();
            string id_zona = comboBoxSeleccionarZona.SelectedValue.ToString();

            if (id_tamano == null || id_zona == null)
            {
                MessageBox.Show("Por favor seleccione una opción válida en los combobox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var idCliente = clienteLogica.AgregarCliente(
              txtBoxNombre.Text, txtBoxApellido.Text, id_tamano, id_zona, dateTimePickerFechaAlta.Value,
              txtMail.Text, txtBoxMovil.Text, txtBoxDni.Text, txtBoxCuil.Text, txtBoxCalle.Text, txtNumero.Text,
              txtBoxLocalidad.Text, txtBoxProvincia.Text, txtRazonSocial.Text, checkBoxActivo.Checked,
              checkBoxConfiable.Checked, comboBoxCondicionFrenteIVA.Text, txtCodigoPostal.Text
            );

            string usuarioActual = ObtenerUsuarioActual();

            if (idCliente.HasValue)
            {
                int id = idCliente.Value;
                var cliente = clienteLogica.ObtenerClientePorId(id);

                if (cliente != null)
                {
                    string valorNuevo =
                    $"{cliente.nombre}, {cliente.apellido}, DNI: {cliente.dni}, " +
                    $"CUIL/CUIT: {cliente.cuil_cuit}, {cliente.email}, " +
                    $"Calle: {cliente.calle} Numero: {cliente.numero}, Ciudad: {cliente.ciudad}, " +
                    $"Fecha Alta: {cliente.fecha_alta:dd/MM/yyyy}, Estado: {(cliente.estado ? "Activo" : "Inactivo")} "
                    ;

                    bool registrado = auditoria.Registrar(
                        valorAnterior: "-",
                        valorNuevo: valorNuevo,
                        nombreAccion: "Alta",
                        nombreEntidad: "CLIENTE",
                        usuario: usuarioActual
                    );

                    if (!registrado)
                    {
                        MessageBox.Show("Cliente guardado, pero no se pudo registrar auditoría:\n" +
                            string.Join("\n", auditoria.ErroresValidacion),
                            "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Cliente guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Si el cliente es confiable, crear cuenta corriente
                    if (cliente.confiable)
                    {
                        bool cuentaCreada = clienteLogica.CrearCuentaCorriente(cliente.id_cliente, 0);

                        if (cuentaCreada)
                        {
                            MessageBox.Show("Cuenta corriente creada para el cliente confiable.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Obtener la cuenta corriente recién creada
                            var cuentaCorriente = clienteLogica.ObtenerCuentaCorrientePorIdCliente(cliente.id_cliente);

                            if (cuentaCorriente != null)
                            {
                                string valorNuevoCuenta =
                                    $"ClienteID: {cuentaCorriente.id_cliente}, " +
                                    $"Saldo Actual: {cuentaCorriente.saldo_actual:N2}, Fecha último movimiento: {cuentaCorriente.fecha_ultimo_movimiento}";

                                bool registradoCuenta = auditoria.Registrar(
                                    valorAnterior: "-",
                                    valorNuevo: valorNuevoCuenta,
                                    nombreAccion: "Alta",
                                    nombreEntidad: "CUENTA_CORRIENTE",
                                    usuario: usuarioActual
                                );

                                if (!registradoCuenta)
                                {
                                    MessageBox.Show("Cuenta corriente creada, pero no se pudo registrar auditoría:\n" +
                                        string.Join("\n", auditoria.ErroresValidacion),
                                        "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo crear la cuenta corriente para el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener los datos del cliente para registrar auditoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                foreach (var error in clienteLogica.ErroresValidacion)
                {
                    MessageBox.Show(error, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


        }
        private string ObtenerUsuarioActual()
        {
            // Aquí debes implementar la lógica para obtener el nombre del usuario actual
            return UsuarioSesion.Nombre; // Ejemplo: retorna el nombre del usuario desde una clase estática de sesión
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cerrar el formulario sin guardar cambios
            this.Close();
        }

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Convertir a mayúscula la primera letra
            if (txtRazonSocial.Text.Length == 0 && char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            // Permitir cadena de texto alfanumérica, comas, puntos, espacios y teclas de control (como borrar) menor o igual a 20 caracteres 
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                errorProvider1.SetError(txtRazonSocial, "Solo se permiten letras, números, comas y puntos.");
            }
            else if (!char.IsControl(e.KeyChar) && txtRazonSocial.Text.Length >= 30)
            {
                e.Handled = true;
                errorProvider1.SetError(txtRazonSocial, "La razón social no debe exceder los 30 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtRazonSocial, "");
            }

        }

        private void txtRazonSocial_Validating(object sender, CancelEventArgs e)
        {
            // Permitir salir si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || this.IsDisposed || limpiarPresionado)
            {
                errorProvider1.SetError(txtRazonSocial, "");
                return;
            }
            //Validar que tenga al menos 2 caracteres
            if (txtRazonSocial.Text.Length < 2)
            {
                errorProvider1.SetError(txtRazonSocial, "La razón social debe tener al menos 2 caracteres.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtRazonSocial, "");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarPresionado = true;
            // Limpiar todos los campos del formulario DEJANDO LOS COMBOBOX EN SU ESTADO INICIAL Y DATEPICKER EN LA FECHA ACTUAL
            txtBoxNombre.Clear();
            txtBoxApellido.Clear();
            txtBoxDni.Clear();
            txtBoxCuil.Clear();
            txtBoxMovil.Clear();
            txtMail.Clear();
            txtBoxCalle.Clear();
            txtNumero.Clear();
            txtBoxLocalidad.Clear();
            txtBoxProvincia.Clear();
            txtRazonSocial.Clear();
            dateTimePickerFechaAlta.Value = DateTime.Now;
            errorProvider1.Clear();
            comboBoxCondicionFrenteIVA.SelectedIndex = 0;
            checkBoxActivo.Checked = false;
            checkBoxConfiable.Checked = false;
            txtCodigoPostal.Clear();
            //cargar combobox de base de datos
            var tamanos = clienteLogica.ObtenerTamanos();
            comboBoxSeleccionarTamano.Items.Clear();
            comboBoxSeleccionarTamano.Items.Add("Seleccione tamaño");
            foreach (var tamano in tamanos)
            {
                string descripcion = $"{tamano.id_tamano} - {tamano.descripcion}";
                comboBoxSeleccionarTamano.Items.Add(descripcion);
            }
            comboBoxSeleccionarTamano.SelectedIndex = 0;

            var zonas = clienteLogica.ObtenerZonas();
            comboBoxSeleccionarZona.Items.Clear();
            comboBoxSeleccionarZona.Items.Add("Seleccione zona");
            foreach (var zona in zonas)
            {
                string nombre = $"{zona.id_zona} - {zona.nombre}";
                comboBoxSeleccionarZona.Items.Add(nombre);
            }
            comboBoxSeleccionarZona.SelectedIndex = 0;

        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            // Cargar tamaños
            var tamanos = clienteLogica.ObtenerTamanos();
            tamanos.Insert(0, new TAMAÑO_NEGOCIO { id_tamano = 0, descripcion = "Seleccione tamaño" });

            comboBoxSeleccionarTamano.DataSource = tamanos;
            comboBoxSeleccionarTamano.DisplayMember = "descripcion";
            comboBoxSeleccionarTamano.ValueMember = "id_tamano";
            comboBoxSeleccionarTamano.SelectedIndex = 0;

            // Cargar zonas
            var zonas = clienteLogica.ObtenerZonas();
            zonas.Insert(0, new ZONA { id_zona = 0, nombre = "Seleccione zona" });

            comboBoxSeleccionarZona.DataSource = zonas;
            comboBoxSeleccionarZona.DisplayMember = "nombre";
            comboBoxSeleccionarZona.ValueMember = "id_zona";
            comboBoxSeleccionarZona.SelectedIndex = 0;
        }

        private void txtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (como borrar) y no mas de 8 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtCodigoPostal, "Solo se permiten números.");
            }
            else if (!char.IsControl(e.KeyChar) && txtCodigoPostal.Text.Length >= 8)
            {
                e.Handled = true;
                errorProvider1.SetError(txtCodigoPostal, "El código postal no debe exceder los 8 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtCodigoPostal, "");
            }
        }                
    }
}

