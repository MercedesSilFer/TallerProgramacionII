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
        ClassPagoLogica pagoLogica = new ClassPagoLogica();
        ClassProductoLogica productoLogica = new ClassProductoLogica();
        private string nombreUsuarioSeleccionado;


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
            lblNombreApellido.Text = "Nombre: ";
            lblDni.Text = "DNI: ";
            lblMail.Text = "Correo: ";
            lblUsuario.Text = "Usuario: ";
            lblnombre_usuario.Text = " ";


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
                lblNombreApellido.Text = $"Nombre: {preventista.nombre} {preventista.apellido}";
                lblDni.Text = $"DNI: {preventista.dni}";
                lblMail.Text = $"Correo: {preventista.email}";
                lblUsuario.Text = "Usuario: ";
                lblnombre_usuario.Text = preventista.nombre_usuario;
                nombreUsuarioSeleccionado = preventista.nombre_usuario;
            }
            else
            {
                MessageBox.Show("No se encontró ningún preventista con ese dato.");
                //limpiar 
                lblNombreApellido.Text = "Nombre: ";
                lblDni.Text = "DNI: ";
                lblMail.Text = "Correo: ";
                lblUsuario.Text = "Usuario: ";
                lblnombre_usuario.Text = " ";
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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            //Mensaje de prueba
            //MessageBox.Show($"Se listaron {clientes.Count} cliente(s) de la zona.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void noConfiablesToolStripMenuItem_Click(object sender, EventArgs e)
        { // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("nombre", "Nombre y Apellido");
            dataGridViewHistorial.Columns.Add("DNI", "DNI");
            dataGridViewHistorial.Columns.Add("Direccion", "Dirección");

            // Validar que haya un nombre de usuario cargado
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            //MessageBox.Show($"Se listaron {clientes.Count} cliente(s) no confiables.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            string nombreUsuario = nombreUsuarioSeleccionado;
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

            //MessageBox.Show($"Se listaron {pedidos.Count} pedido(s) del preventista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cargar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Columns.Add("ID Producto", "ID Producto");
            dataGridViewHistorial.Columns.Add("Nombre", "Nombre");
            dataGridViewHistorial.Columns.Add("ID_presentacion", "ID_presentacion");
            dataGridViewHistorial.Columns.Add("presentacion", "Presentación");
            dataGridViewHistorial.Columns.Add("Precio", "Precio");
            dataGridViewHistorial.Columns.Add("Cantidad Vendida", "Cantidad Vendida");
            // Validar preventista
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("Debe seleccionar un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener productos más vendidos con cantidad
            var productosVendidos = pedidoLogica.ObtenerProductoPresentacionMasVendidosConCantidadPorPreventista(nombreUsuario);
            if (productosVendidos == null || productosVendidos.Count == 0)
            {
                MessageBox.Show("No se encontraron productos vendidos para este preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar cada producto en el DataGridView
            foreach (var item in productosVendidos)
            {
                var pp = item.ProductoPresentacion;

                // Obtener datos relacionados
                var producto = productoLogica.ObtenerProductoPorId(pp.id_producto);
                var presentacion = productoLogica.ObtenerPresentacionPorId(pp.ID_presentacion);
                
                // Preparar valores
                string nombreProducto = producto?.nombre ?? "Sin nombre";
                string descripcionPresentacion = presentacion?.descripcion ?? "Sin presentación";
                string precio = pp.precioLista.ToString("C");
                int cantidadVendida = item.CantidadVendida;
                

                // Agregar fila
                dataGridViewHistorial.Rows.Add(
                    pp.id_producto,
                    nombreProducto,
                    pp.ID_presentacion,
                    descripcionPresentacion,
                    precio,
                    cantidadVendida
                );

            }
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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("No hay clientes registrados en la zona del preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            //MessageBox.Show($"Se listaron {clientes.Count} cliente(s) confiables.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("No hay clientes registrados en la zona asignada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            //MessageBox.Show($"Se listaron {clientes.Count} cliente(s) pequeño(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("No hay clientes registrados en la zona del preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            //MessageBox.Show($"Se listaron {clientes.Count} cliente(s) mediano(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes grandes de la zona

            var clientes = clienteLogica.ObtenerClientesPorTamanio(3);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en la zona del preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar clientes en el DataGridView
            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                string direccion = $"{cliente.calle} {cliente.numero}";
                dataGridViewHistorial.Rows.Add(nombreCompleto, cliente.dni, direccion);
            }

            //MessageBox.Show($"Se listaron {clientes.Count} cliente(s) mediano(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pendientesToolStripMenuItem_Click(object sender, EventArgs e)
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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            // Obtener pedidos del preventista
            var pedidos = pedidoLogica.ObtenerPedidosPendientesPorZona(id_zona);
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

            //MessageBox.Show($"Se listaron {pedidos.Count} pedido(s) del preventista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void entregadosToolStripMenuItem_Click(object sender, EventArgs e)
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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            // Obtener pedidos del preventista
            var pedidos = pedidoLogica.ObtenerPedidosEntregadosPorZona(id_zona);
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
        }

        private void canceladosToolStripMenuItem_Click(object sender, EventArgs e)
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
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(nombreUsuario);
            // Obtener pedidos del preventista
            var pedidos = pedidoLogica.ObtenerPedidosCanceladosPorZona(id_zona);
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
        }

        private void conSaldosPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Aqui debo implementar la funcionalidad para listar pedidos con saldos pendientes, que son aquellos que no poseen un registro en la tabla pedido_pago o el ultimo registro pedido_pago tiene un saldo mayor a 0.
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("ID Pedido", "ID Pedido");
            dataGridViewHistorial.Columns.Add("Detalle", "Detalle");
            dataGridViewHistorial.Columns.Add("Fecha", "Fecha");
            dataGridViewHistorial.Columns.Add("Monto", "Monto");
            dataGridViewHistorial.Columns.Add("Saldo Pendiente", "Saldo Pendiente");
            dataGridViewHistorial.Columns.Add("Estado", "Estado");
            dataGridViewHistorial.Columns.Add("Cliente", "Cliente");            

            // Validar que haya un preventista definido
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Obtener pedidos del preventista con saldo pendiente
            var pedidosConSaldo = pedidoLogica.ObtenerPedidosConSaldoPendientePorVendedor(nombreUsuario);
            if (pedidosConSaldo == null || pedidosConSaldo.Count == 0)
            {
                MessageBox.Show("No se encontraron pedidos con saldo pendiente para este preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar pedidos en el DataGridView
            foreach (var pedido in pedidosConSaldo)
            {
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                var estado = pedidoLogica.ObtenerEstadoPorId(pedido.id_estado);
                var ultimo_pedido_pago = pagoLogica.ObtenerUltimoPedidoPagoPorIdPedido(pedido.id_pedido);
                decimal saldo;
                if(ultimo_pedido_pago!= null)
                {
                    saldo = ultimo_pedido_pago.saldo;
                }
                else
                {
                    saldo = pedido.total;
                }
                    string detalle = $"Factura Nº {pedido.numero_factura}";
                string fecha = pedido.fecha_entrega.ToString("dd/MM/yyyy");
                string clienteNombre = cliente != null ? $"{cliente.nombre} {cliente.apellido}" : "Desconocido";
                string estadoNombre = estado != null ? estado.descripcion : "Sin estado";

                dataGridViewHistorial.Rows.Add(
                    pedido.id_pedido,
                    detalle,
                    fecha,
                    pedido.total.ToString("C"),
                    saldo,
                    estadoNombre,
                    clienteNombre
                );
            }
        }

        private void saldadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Aqui debo implementar la funcionalidad para listar pedidos saldados, saldo 0 en ultimo pedido_pago
            // Limpiar y configurar columnas del DataGridView
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("ID Pedido", "ID Pedido");
            dataGridViewHistorial.Columns.Add("Detalle", "Detalle");
            dataGridViewHistorial.Columns.Add("Fecha", "Fecha");
            dataGridViewHistorial.Columns.Add("Monto", "Monto");
            dataGridViewHistorial.Columns.Add("Saldo Pendiente", "Saldo");
            dataGridViewHistorial.Columns.Add("Estado", "Estado");
            dataGridViewHistorial.Columns.Add("Cliente", "Cliente");            

            // Validar que haya un preventista definido
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Obtener pedidos del preventista con saldo pendiente
            var pedidosConSaldo = pedidoLogica.ObtenerPedidosSaldadosPorVendedor(nombreUsuario);
            if (pedidosConSaldo == null || pedidosConSaldo.Count == 0)
            {
                MessageBox.Show("No se encontraron pedidos con saldo pendiente para este preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar pedidos en el DataGridView
            foreach (var pedido in pedidosConSaldo)
            {
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                var estado = pedidoLogica.ObtenerEstadoPorId(pedido.id_estado);
                var ultimo_pedido_pago = pagoLogica.ObtenerUltimoPedidoPagoPorIdPedido(pedido.id_pedido);
                decimal saldo;
                if(ultimo_pedido_pago!= null)
                {
                    saldo = ultimo_pedido_pago.saldo;
                }
                else
                {
                    saldo = pedido.total;
                }
                    string detalle = $"Factura Nº {pedido.numero_factura}";
                string fecha = pedido.fecha_entrega.ToString("dd/MM/yyyy");
                string clienteNombre = cliente != null ? $"{cliente.nombre} {cliente.apellido}" : "Desconocido";
                string estadoNombre = estado != null ? estado.descripcion : "Sin estado";

                dataGridViewHistorial.Rows.Add(
                    pedido.id_pedido,
                    detalle,
                    fecha,
                    pedido.total.ToString("C"),
                    saldo,
                    estadoNombre,
                    clienteNombre
                );
            }
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Desarrollar aquí la lógica para obtener los pedidos del ultimo mes saldados del preventista 
            dataGridViewHistorial.Columns.Clear();
            dataGridViewHistorial.Rows.Clear();
            dataGridViewHistorial.Columns.Add("ID Pedido", "ID Pedido");
            dataGridViewHistorial.Columns.Add("Detalle", "Detalle");
            dataGridViewHistorial.Columns.Add("Fecha", "Fecha");
            dataGridViewHistorial.Columns.Add("Monto", "Monto");
            dataGridViewHistorial.Columns.Add("Saldo Pendiente", "Saldo");
            dataGridViewHistorial.Columns.Add("Estado", "Estado");
            dataGridViewHistorial.Columns.Add("Cliente", "Cliente");

            // Validar que haya un preventista definido
            string nombreUsuario = nombreUsuarioSeleccionado;
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Obtener pedidos del preventista con saldo pendiente
            var pedidosConSaldo = pedidoLogica.ObtenerPedidosSaldadosUltimoMesPorVendedor(nombreUsuario);
            if (pedidosConSaldo == null || pedidosConSaldo.Count == 0)
            {
                MessageBox.Show("No se encontraron pedidos con saldo pendiente para este preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar pedidos en el DataGridView
            foreach (var pedido in pedidosConSaldo)
            {
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                var estado = pedidoLogica.ObtenerEstadoPorId(pedido.id_estado);
                var ultimo_pedido_pago = pagoLogica.ObtenerUltimoPedidoPagoPorIdPedido(pedido.id_pedido);
                decimal saldo;
                if (ultimo_pedido_pago != null)
                {
                    saldo = ultimo_pedido_pago.saldo;
                }
                else
                {
                    saldo = pedido.total;
                }
                string detalle = $"Factura Nº {pedido.numero_factura}";
                string fecha = pedido.fecha_entrega.ToString("dd/MM/yyyy");
                string clienteNombre = cliente != null ? $"{cliente.nombre} {cliente.apellido}" : "Desconocido";
                string estadoNombre = estado != null ? estado.descripcion : "Sin estado";

                dataGridViewHistorial.Rows.Add(
                    pedido.id_pedido,
                    detalle,
                    fecha,
                    pedido.total.ToString("C"),
                    saldo,
                    estadoNombre,
                    clienteNombre
                );
            }

        }

      
    }
}
