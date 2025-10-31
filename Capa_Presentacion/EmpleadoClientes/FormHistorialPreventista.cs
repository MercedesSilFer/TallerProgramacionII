using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Logica;
using Capa_Entidades;

namespace ArimaERP.EmpleadoClientes
{
    public partial class FormHistorialPreventista : Form
    {
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        ClassPedidoLogica pedidoLogica = new ClassPedidoLogica();
        ClassZonaLogica zonaLogica = new ClassZonaLogica();
        ClassClienteLogica clienteLogica = new ClassClienteLogica();
        
        public FormHistorialPreventista()
        {
            InitializeComponent();
        }

        private void registrarPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscarPreventista_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscarPreventista_Click(object sender, EventArgs e)
        {
            //limpiar 
            lblNombreApellido.Text = string.Empty;
            lblDni.Text = string.Empty;
            lblMail.Text = string.Empty;
            lblnombre_usuario.Text = string.Empty;

            //Habilitar el uso del DataGridView
            dataGridViewHistorial.Enabled = true;
            //Habilitar menu contextual
            menuStripHistorialPreventista.Enabled = true;

            string textoBusqueda = txtBuscarPreventista.Text.Trim();
            if (string.IsNullOrEmpty(textoBusqueda))
            {
                MessageBox.Show("Ingrese un valor para buscar.");
                return;
            }

            var preventista = empleadoLogica.BuscarPreventistaPorTexto(textoBusqueda);
            if (preventista != null)
            {
                lblNombreApellido.Text = $"{preventista.nombre} {preventista.apellido}";
                lblDni.Text = preventista.dni.ToString();
                lblMail.Text = preventista.email;
                lblnombre_usuario.Text = preventista.nombre_usuario;            
            }
            else
            {
                MessageBox.Show("No se encontró ningún preventista con ese dato.");
                //limpiar 
                lblNombreApellido.Text= string.Empty;
                lblDni.Text = string.Empty;
                lblMail.Text = string.Empty;
                lblnombre_usuario.Text= string.Empty;
                return;
            }
        }

        private void listarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar clientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes de la zona
            var clientes = clienteLogica.ClientesPorZona(id_zona);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en esta zona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            MessageBox.Show($"Se listaron {clientes.Count} cliente(s) de la zona.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void noConfiablesToolStripMenuItem_Click(object sender, EventArgs e)
        { // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar clientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes de la zona
            var clientes = clienteLogica.ClientesNoConfiablesPorZona(id_zona);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en esta zona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            MessageBox.Show($"Se listaron {clientes.Count} cliente(s) no confiables.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("ID Pedido", "ID Pedido");
            dataGridViewHistorial.Columns.Add("Detalle", "Detalle");
            dataGridViewHistorial.Columns.Add("Fecha", "Fecha");
            dataGridViewHistorial.Columns.Add("Monto", "Monto");
            dataGridViewHistorial.Columns.Add("Estado", "Estado");
            dataGridViewHistorial.Columns.Add("Cliente", "Cliente");

            // Validar que haya un preventista definido
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            // Obtener pedidos del preventista
            var pedidos = pedidoLogica.ObtenerPedidosPorZona(id_zona);
            if (pedidos == null || pedidos.Count == 0)
            {
                MessageBox.Show("No se encontraron pedidos para este preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar pedidos en el DataGridView
            foreach (var pedido in pedidos)
            {
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                var estado = pedidoLogica.ObtenerEstadoPorId(pedido.id_estado);

                string detalle = $"Factura Nº {pedido.numero_factura}";
                string fecha = pedido.fecha_entrega.ToString("dd/MM/yyyy");
                string clienteNombre = cliente != null ? $"{cliente.nombre} {cliente.apellido}" : "Desconocido";
                string estadoNombre = estado != null ? estado.descripcion : "Sin estado";

                dataGridViewHistorial.Rows.Add(
                    pedido.id_pedido,
                    detalle,
                    fecha,
                    pedido.total.ToString("C"),
                    estadoNombre,
                    clienteNombre
                );
            }

            MessageBox.Show($"Se listaron {pedidos.Count} pedido(s) del preventista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cargar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Columns.Add("ID Venta", "ID Venta");
            dataGridViewHistorial.Columns.Add("Detalle", "Detalle");
            dataGridViewHistorial.Columns.Add("Fecha", "Fecha");
            dataGridViewHistorial.Columns.Add("Monto", "Monto");
            dataGridViewHistorial.Columns.Add("Cliente", "Cliente");

        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cargar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Columns.Add("ID Producto", "ID Producto");
            dataGridViewHistorial.Columns.Add("Nombre", "Nombre");
            dataGridViewHistorial.Columns.Add("Descripcion", "Descripcion");
            dataGridViewHistorial.Columns.Add("Precio", "Precio");
            dataGridViewHistorial.Columns.Add("Stock", "Stock");
            dataGridViewHistorial.Columns.Add("Categoria", "Categoria");
            dataGridViewHistorial.Columns.Add("Proveedor", "Proveedor");
            dataGridViewHistorial.Columns.Add("apresentacion", "Presentacion");
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cargar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Columns.Add("ID Pago", "ID Pago");
            dataGridViewHistorial.Columns.Add("Monto", "Monto");
            dataGridViewHistorial.Columns.Add("Fecha", "Fecha");
            dataGridViewHistorial.Columns.Add("Metodo de Pago", "Metodo de Pago");
            dataGridViewHistorial.Columns.Add("Cliente", "Cliente");

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Cerrar el formulario
            this.Close();
        }

        private void FormHistorialPreventista_Load(object sender, EventArgs e)
        {
            dataGridViewHistorial.RowHeadersVisible = false;
            dataGridViewHistorial.Rows.Clear();
            //no permitir modificar el ancho de las filas
            dataGridViewHistorial.AllowUserToResizeRows = false;
            //ocupar todo el espacio del ancho
            dataGridViewHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewHistorial.ReadOnly = true;

        }

        private void confiablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar clientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes de la zona
            var clientes = clienteLogica.ClientesConfiablesPorZona(id_zona);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en esta zona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            MessageBox.Show($"Se listaron {clientes.Count} cliente(s) confiables.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void pequeñosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar clientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes pequeños de la zona

            var clientes = clienteLogica.ObtenerClientesPorTamanio(1);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en esta zona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            MessageBox.Show($"Se listaron {clientes.Count} cliente(s) pequeño(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void medianosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar clientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes pequeños de la zona

            var clientes = clienteLogica.ObtenerClientesPorTamanio(2);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en esta zona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            MessageBox.Show($"Se listaron {clientes.Count} cliente(s) mediano(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void grandesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = lblnombre_usuario.Text.Trim();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar clientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes pequeños de la zona

            var clientes = clienteLogica.ObtenerClientesPorTamanio(3);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en esta zona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            MessageBox.Show($"Se listaron {clientes.Count} cliente(s) mediano(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void entregadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void canceladosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
