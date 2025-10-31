using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Entidades;
using Capa_Logica;

namespace ArimaERP.EmpleadoClientes
{
    public partial class FormModificacionCliente : Form
        
    {
        private ClassClienteLogica clienteLogica = new ClassClienteLogica();
        public FormModificacionCliente()
        {
            InitializeComponent();
        }

        private void AbrirFormularioEdicion()
        {
            if (dataGridListarClientes.CurrentRow != null)
            {
                DataGridViewRow fila = dataGridListarClientes.CurrentRow;

                CLIENTE clienteSeleccionado = new CLIENTE
                {
                    id_cliente = Convert.ToInt32(fila.Cells["id_cliente"].Value),
                    dni = Convert.ToInt32(fila.Cells["dni"].Value),
                    nombre = fila.Cells["nombre"].Value?.ToString(),
                    apellido = fila.Cells["apellido"].Value?.ToString(),
                    telefono = Convert.ToInt64(fila.Cells["telefono"].Value),
                    email = fila.Cells["email"].Value?.ToString(),
                    razon_social = fila.Cells["razon_social"].Value?.ToString(),
                    cuil_cuit = Convert.ToInt64(fila.Cells["cuil_cuit"].Value),
                    fecha_alta = Convert.ToDateTime(fila.Cells["fecha_alta"].Value),
                    estado = fila.Cells["estado"].Value?.ToString() == "Activo",
                    confiable = fila.Cells["confiable"].Value?.ToString() == "Si",
                    condicion_frenteIVA = fila.Cells["condicion_frenteIVA"].Value?.ToString(),
                    calle = fila.Cells["calle"].Value?.ToString(),
                    numero = Convert.ToInt32(fila.Cells["numero"].Value),
                    ciudad = fila.Cells["ciudad"].Value?.ToString(),
                    provincia = fila.Cells["provincia"].Value?.ToString(),
                    cod_postal = Convert.ToInt32(fila.Cells["cod_postal"].Value),
                    id_zona = Convert.ToInt32(fila.Cells["id_zona"].Value),
                    id_tamano = Convert.ToInt32(fila.Cells["id_tamano"].Value),                  

                };
                FormEditarCliente formEditarCliente = new FormEditarCliente(clienteSeleccionado);
                // Suscribirse al evento               
                formEditarCliente.ShowDialog();
                // Después de cerrar el formulario de edición, recargar los datos
                FormModificacionCliente_Load(this, EventArgs.Empty);                 
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                if (e.RowIndex >= 0)
                {
                    AbrirFormularioEdicion();
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridListarClientes.CurrentRow != null)
            {
                AbrirFormularioEdicion();
                e.SuppressKeyPress = true;
            }
        }

        private void FormModificacionCliente_Load(object sender, EventArgs e)
        {
            //configurar la coleccion de columnas del data grid segun la tabla cliente
            dataGridListarClientes.Columns.Clear();
            dataGridListarClientes.Columns.Add("id_cliente", "ID");
            dataGridListarClientes.Columns.Add("dni", "DNI");
            dataGridListarClientes.Columns.Add("nombre", "Nombre");
            dataGridListarClientes.Columns.Add("apellido", "Apellido");
            dataGridListarClientes.Columns.Add("telefono", "Teléfono");
            dataGridListarClientes.Columns.Add("email", "Email");
            dataGridListarClientes.Columns.Add("razon_social", "Razón Social");
            dataGridListarClientes.Columns.Add("cuil_cuit", "CUIL/CUIT");
            dataGridListarClientes.Columns.Add("fecha_alta", "Fecha de Alta");
            dataGridListarClientes.Columns.Add("estado", "Estado");
            dataGridListarClientes.Columns.Add("confiable", "Confiable");
            dataGridListarClientes.Columns.Add("condicion_frenteIVA", "Condición frente al IVA");
            dataGridListarClientes.Columns.Add("calle", "Calle");
            dataGridListarClientes.Columns.Add("numero", "Número");
            dataGridListarClientes.Columns.Add("ciudad", "Ciudad");
            dataGridListarClientes.Columns.Add("provincia", "Provincia");
            dataGridListarClientes.Columns.Add("cod_postal", "Código Postal");
            dataGridListarClientes.Columns.Add("tamano", "Tamaño");
            dataGridListarClientes.Columns.Add("zona", "Zona");
            dataGridListarClientes.Columns.Add("id_tamano", "ID Tamaño");
            dataGridListarClientes.Columns["id_tamano"].Visible = false;
            dataGridListarClientes.Columns.Add("id_zona", "ID Zona");
            dataGridListarClientes.Columns["id_zona"].Visible = false;            
            dataGridListarClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridListarClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridListarClientes.MultiSelect = false;
            dataGridListarClientes.ReadOnly = true;
            dataGridListarClientes.AllowUserToAddRows = false;
            //agregar clientes desde tabla cliente
            List<CLIENTE> listaClientes = clienteLogica.ObtenerClientes();
            var zonas = clienteLogica.ObtenerZonas();
            var tamanos = clienteLogica.ObtenerTamanos();
            foreach (var cliente in listaClientes)
            {
                string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                string confiableTexto = cliente.confiable ? "Si" : "No";
                // Buscar nombre de zona
                string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                // Buscar nombre de tamaño
                string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                dataGridListarClientes.Rows.Add(cliente.id_cliente, cliente.dni, cliente.nombre, cliente.apellido, cliente.telefono, cliente.email, cliente.razon_social, cliente.cuil_cuit, cliente.fecha_alta, estadoTexto, confiableTexto, cliente.condicion_frenteIVA, cliente.calle, cliente.numero, cliente.ciudad, cliente.provincia, cliente.cod_postal, nombreTamano, nombreZona, cliente.id_tamano, cliente.id_zona);
            }
            //CARGAR COMBOBOX ZONA
            zonas.Insert(0, new ZONA { id_zona = 0, nombre = "Seleccione zona" });
            comboBoxBuscarClienteZona.DataSource = zonas;
            comboBoxBuscarClienteZona.DisplayMember = "nombre";
            comboBoxBuscarClienteZona.ValueMember = "id_zona";
            comboBoxBuscarClienteZona.SelectedIndex = 0;
        }

        private void txtNombreApellido_KeyDown(object sender, KeyEventArgs e)
        {
            
            //al presionar enter buscar por nombre o por apellido
            if (e.KeyCode == Keys.Enter)
            {
                //
                string filtro = txtNombreApellido.Text.ToLower();
               //buscar por nombre o apellido en tabla CLIENTE de base de datos
                // Limpiar las filas actuales del DataGridView
                dataGridListarClientes.Rows.Clear();
                List<CLIENTE> listaClientes = clienteLogica.ObtenerClientes();
                var clientesFiltrados = listaClientes.Where(cliente =>
                    (cliente.nombre != null && cliente.nombre.ToLower().Contains(filtro)) ||
                    (cliente.apellido != null && cliente.apellido.ToLower().Contains(filtro))
                ).ToList();
                var zonas = clienteLogica.ObtenerZonas();
                var tamanos = clienteLogica.ObtenerTamanos();
                foreach (var cliente in clientesFiltrados)
                {
                    string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                    string confiableTexto = cliente.confiable ? "Si" : "No";
                    string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                    string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                    dataGridListarClientes.Rows.Add(
                        cliente.id_cliente,
                        cliente.dni,
                        cliente.nombre,
                        cliente.apellido,
                        cliente.telefono,
                        cliente.email,
                        cliente.razon_social,
                        cliente.cuil_cuit,
                        cliente.fecha_alta,
                        estadoTexto,
                        confiableTexto,
                        cliente.condicion_frenteIVA,
                        cliente.calle,
                        cliente.numero,
                        cliente.ciudad,
                        cliente.provincia,
                        cliente.cod_postal,
                        nombreTamano,
                        nombreZona,
                         cliente.id_tamano, cliente.id_zona
                    );
                }
            }
        }

        private void txtBuscarDni_KeyDown(object sender, KeyEventArgs e)
        {
            //Validar que se ingresaron valores numericos
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtBuscarDni.Text))
                {
                    if (!long.TryParse(txtBuscarDni.Text, out _))
                    {
                        MessageBox.Show("Por favor, ingrese solo valores numéricos para el DNI.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBuscarDni.Clear();
                        return;
                    }
                }
                string filtro = txtBuscarDni.Text.ToLower();
                //buscar por dni en tabla CLIENTE de base de datos
                // Limpiar las filas actuales del DataGridView
                dataGridListarClientes.Rows.Clear();
                List<CLIENTE> listaClientes = clienteLogica.ObtenerClientes();
                var clientesFiltrados = listaClientes.Where(cliente =>
                    cliente.dni.ToString().ToLower().Contains(filtro)
                ).ToList();
                var zonas = clienteLogica.ObtenerZonas();
                var tamanos = clienteLogica.ObtenerTamanos();
                foreach (var cliente in clientesFiltrados)
                {
                    string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                    string confiableTexto = cliente.confiable ? "Si" : "No";
                    string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                    string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                    dataGridListarClientes.Rows.Add(
                        cliente.id_cliente,
                        cliente.dni,
                        cliente.nombre,
                        cliente.apellido,
                        cliente.telefono,
                        cliente.email,
                        cliente.razon_social,
                        cliente.cuil_cuit,
                        cliente.fecha_alta,
                        estadoTexto,
                        confiableTexto,
                        cliente.condicion_frenteIVA,
                        cliente.calle,
                        cliente.numero,
                        cliente.ciudad,
                        cliente.provincia,
                        cliente.cod_postal,
                        nombreTamano,
                        nombreZona,
                         cliente.id_tamano, cliente.id_zona
                    );
                }
            }
        }

        private void txtBusacarEmail_KeyDown(object sender, KeyEventArgs e)
        {   //buscar por email en tabla CLIENTE de base de datos
            // Limpiar las filas actuales del DataGridView
            dataGridListarClientes.Rows.Clear();
            List<CLIENTE> listaClientes = clienteLogica.ObtenerClientes();
            string filtro = txtBusacarEmail.Text.ToLower();
            var clientesFiltrados = listaClientes.Where(cliente =>
                (cliente.email != null && cliente.email.ToLower().Contains(filtro))
            ).ToList();
            foreach (var cliente in clientesFiltrados)
            {
                string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                string confiableTexto = cliente.confiable ? "Si" : "No";
                var zonas = clienteLogica.ObtenerZonas();
                var tamanos = clienteLogica.ObtenerTamanos();
                string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                dataGridListarClientes.Rows.Add(
                    cliente.id_cliente,
                    cliente.dni,
                    cliente.nombre,
                    cliente.apellido,
                    cliente.telefono,
                    cliente.email,
                    cliente.razon_social,
                    cliente.cuil_cuit,
                    cliente.fecha_alta,
                    estadoTexto,
                    confiableTexto,
                    cliente.condicion_frenteIVA,
                    cliente.calle,
                    cliente.numero,
                    cliente.ciudad,
                    cliente.provincia,
                    cliente.cod_postal,
                    nombreTamano,
                    nombreZona,
                     cliente.id_tamano, cliente.id_zona
                );
            }           
        }

        private void comboBoxBuscarClienteZona_SelectedIndexChanged(object sender, EventArgs e)
        {

            {
                // Limpiar las filas actuales del DataGridView
                dataGridListarClientes.Rows.Clear();

                // Obtener zonas y tamaños para enriquecer los datos
                var zonas = clienteLogica.ObtenerZonas();
                var tamanos = clienteLogica.ObtenerTamanos();

                List<CLIENTE> listaClientes;

                if (comboBoxBuscarClienteZona.SelectedValue is int idZona && idZona > 0)
                {
                    listaClientes = clienteLogica.ClientesPorZona(idZona);
                }
                else
                {
                    listaClientes = clienteLogica.ObtenerClientes();
                }

                foreach (var cliente in listaClientes)
                {
                    string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                    string confiableTexto = cliente.confiable ? "Si" : "No";
                    string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                    string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                    dataGridListarClientes.Rows.Add(
                        cliente.id_cliente,
                        cliente.dni,
                        cliente.nombre,
                        cliente.apellido,
                        cliente.telefono,
                        cliente.email,
                        cliente.razon_social,
                        cliente.cuil_cuit,
                        cliente.fecha_alta,
                        estadoTexto,
                        confiableTexto,
                        cliente.condicion_frenteIVA,
                        cliente.calle,
                        cliente.numero,
                        cliente.ciudad,
                        cliente.provincia,
                        cliente.cod_postal,
                        nombreTamano,
                        nombreZona,
                         cliente.id_tamano, cliente.id_zona
                    );
                }
            }
        }

        private void textBoxBUSCARGENERAL_TextChanged(object sender, EventArgs e)
        {
            //busqueda general en todas las columnas de la tabla cliente
            var zonas = clienteLogica.ObtenerZonas();
            var tamanos = clienteLogica.ObtenerTamanos();

            List<CLIENTE> listaClientes = clienteLogica.ObtenerClientes();
            string filtro = textBoxBUSCARGENERAL.Text.ToLower();
            if (string.IsNullOrEmpty(filtro)) {
                return;
            }
            var clientesFiltrados = listaClientes.Where(cliente =>
                (cliente.nombre != null && cliente.nombre.ToLower().Contains(filtro)) ||
                (cliente.apellido != null && cliente.apellido.ToLower().Contains(filtro)) ||
                (cliente.dni.ToString().ToLower().Contains(filtro)) ||
                (cliente.email != null && cliente.email.ToLower().Contains(filtro)) ||
                (cliente.razon_social != null && cliente.razon_social.ToLower().Contains(filtro)) ||
                (cliente.cuil_cuit.ToString().ToLower().Contains(filtro)) ||
                (cliente.fecha_alta.ToString("dd/MM/yyyy").ToLower().Contains(filtro)) ||
                (cliente.estado ? "activo" : "inactivo").Contains(filtro) ||
                (cliente.confiable ? "si" : "no").Contains(filtro) ||
                (cliente.condicion_frenteIVA != null && cliente.condicion_frenteIVA.ToLower().Contains(filtro)) ||
                (cliente.calle != null && cliente.calle.ToLower().Contains(filtro)) ||
                (cliente.numero.ToString().ToLower().Contains(filtro)) ||
                (cliente.ciudad != null && cliente.ciudad.ToLower().Contains(filtro)) ||
                (cliente.provincia != null && cliente.provincia.ToLower().Contains(filtro)) ||
                (cliente.cod_postal.ToString().ToLower().Contains(filtro)) ||
                (tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion.ToLower().Contains(filtro) ?? false) ||
                (zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre.ToLower().Contains(filtro) ?? false)
            ).ToList();
            // Limpiar las filas actuales del DataGridView
            dataGridListarClientes.Rows.Clear();
            foreach (var cliente in clientesFiltrados)
            {
                string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                string confiableTexto = cliente.confiable ? "Si" : "No";
                string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                dataGridListarClientes.Rows.Add(
                    cliente.id_cliente,
                    cliente.dni,
                    cliente.nombre,
                    cliente.apellido,
                    cliente.telefono,
                    cliente.email,
                    cliente.razon_social,
                    cliente.cuil_cuit,
                    cliente.fecha_alta,
                    estadoTexto,
                    confiableTexto,
                    cliente.condicion_frenteIVA,
                    cliente.calle,
                    cliente.numero,
                    cliente.ciudad,
                    cliente.provincia,
                    cliente.cod_postal,
                    nombreTamano,
                    nombreZona,
                     cliente.id_tamano, cliente.id_zona
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //salir del form
            Close();
        }

        private void txtNombreApellido_KeyPress(object sender, KeyPressEventArgs e)
        {       
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                MessageBox.Show("Por favor, ingrese solo letras y espacios para el nombre o apellido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true; // evita que el carácter no válido se ingrese en el TextBox
            }      
        }      
    }
}