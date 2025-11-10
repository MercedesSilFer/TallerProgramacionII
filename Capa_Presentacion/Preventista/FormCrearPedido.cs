using ArimaERP.EmpleadoClientes;
using Capa_Entidades;
using Capa_Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArimaERP.Preventista
{
    public partial class FormCrearPedido : Form
    {
        ClassClienteLogica clienteLogica = new ClassClienteLogica();
        ClassPedidoLogica pedidoLogica = new ClassPedidoLogica();
        ClassFamiliaLogica familiaLogica = new ClassFamiliaLogica();
        ClassMarcaLogica marcaLogica = new ClassMarcaLogica();
        ClassProveedorLogica proveedorLogica = new ClassProveedorLogica();
        ClassProductoLogica productoLogica = new ClassProductoLogica();
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        ClassAuditoriaLogica auditoriaLogica = new ClassAuditoriaLogica();
        ClassZonaLogica zonaLogica = new ClassZonaLogica();
        private bool cancelarModificacion = false;
        private string usuarioActual;
        public FormCrearPedido()
        {
            InitializeComponent();
            usuarioActual = ObtenerUsuarioActual();
        }
        private string ObtenerUsuarioActual()
        {

            return UsuarioSesion.Nombre;
        }
        private void FormCrearPedido_Load(object sender, EventArgs e)
        {
            lblFechaHora.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //crear dgvResultados con columnas:
            dgvResultados.Columns.Add("id_cliente", "ID");
            dgvResultados.Columns.Add("DNI", "DNI");
            dgvResultados.Columns.Add("Nombre", "Nombre");
            dgvResultados.Columns.Add("Apellido", "Apellido");
            dgvResultados.Columns.Add("Telefono", "Teléfono");
            dgvResultados.Columns.Add("Email", "Email");
            dgvResultados.Columns.Add("RazonSocial", "Razón Social");
            dgvResultados.Columns.Add("CUIT_CUIL", "CUIT/CUIL");
            dgvResultados.Columns.Add("FechaAlta", "Fecha de Alta");
            dgvResultados.Columns.Add("Estado", "Estado");
            dgvResultados.Columns.Add("Confiable", "Confiable");
            dgvResultados.Columns.Add("CondicionFrenteIVA", "Condición frente al IVA");
            dgvResultados.Columns.Add("Calle", "Calle");
            dgvResultados.Columns.Add("Numero", "Número");
            dgvResultados.Columns.Add("Ciudad", "Ciudad");
            dgvResultados.Columns.Add("Provincia", "Provincia");
            dgvResultados.Columns.Add("CodPostal", "Código Postal");
            dgvResultados.Columns.Add("Tamano_Negocio", "Tamaño del Negocio");
            dgvResultados.Columns.Add("Zona", "Zona");
            dgvResultados.Columns.Add("id_tamano", "id_tamano");
            dgvResultados.Columns.Add("id_zona", "id_zona");
            dgvResultados.Columns["id_tamano"].Visible = false;
            dgvResultados.Columns["id_zona"].Visible = false;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.MultiSelect = false;
            dgvResultados.ReadOnly = true;
            dgvResultados.AllowUserToAddRows = false;

            dgvResultados.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            CargarTodosLosClientesActivos();
            
            //cargar comboBoxMarca de base de datos
            var marcas = marcaLogica.ObtenerTodasLasMarcas();
            comboBoxMarca.DataSource = marcas;
            //cargar comboBoxFamilia de base de datos
            var familias = familiaLogica.ObtenerTodasLasFamilias();
            comboBoxFamilia.DataSource = familias;
            comboBoxFamilia.DisplayMember = "descripcion";
            comboBoxFamilia.ValueMember = "id_familia";
            comboBoxFamilia.SelectedIndex = -1; // No seleccionar nada al inicio
            comboBoxMarca.DisplayMember = "nombre";
            comboBoxMarca.ValueMember = "id_marca";
            comboBoxMarca.SelectedIndex = -1; // No seleccionar nada al inicio            
            //cargar dataGridViewProductos con columnas
            dataGridViewProductos.Columns.Add("nombre", "Nombre");
            dataGridViewProductos.Columns.Add("presentacion", "Presentación");
            dataGridViewProductos.Columns.Add("cod_producto", "Código");
            dataGridViewProductos.Columns.Add("precioLista", "Precio");
            dataGridViewProductos.Columns.Add("stock", "Stock");
            dataGridViewProductos.Columns.Add("marca", "Marca");
            dataGridViewProductos.Columns.Add("ID_presentacion", "id_Presentacion");
            dataGridViewProductos.Columns["ID_presentacion"].Visible = false;
            dataGridViewProductos.Columns.Add("id_producto", "ID_producto");
            dataGridViewProductos.Columns["id_producto"].Visible = false;
            //agregar un boton que sirva para seleccionar el producto y cargar al dataGridViewDetallePedido
            DataGridViewButtonColumn btnAgregarProducto = new DataGridViewButtonColumn();
            btnAgregarProducto.HeaderText = "Acción";
            btnAgregarProducto.Text = "Agregar al Detalle";
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.UseColumnTextForButtonValue = true;
            dataGridViewProductos.Columns.Add(btnAgregarProducto);
            dataGridViewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProductos.MultiSelect = false;
            dataGridViewProductos.ReadOnly = true;
            dataGridViewProductos.AllowUserToAddRows = false;
            dataGridViewProductos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12);
            dataGridViewProductos.RowTemplate.Height = 28;
            //cargar dataGridViewProductos con todos los productos de la base de datos
            CargarTodosLosProductosActivosConStock();
            //cargar dataGridViewDetallePedido con columnas
            dataGridViewDetallePedido.Columns.Add("ID_detalle_pedido", "ID_Detalle");
            dataGridViewDetallePedido.Columns.Add("id_producto", "ID_producto");
            dataGridViewDetallePedido.Columns.Add("ID_presentacion", "ID_presentacion");
            dataGridViewDetallePedido.Columns.Add("nombre", "Nombre");
            dataGridViewDetallePedido.Columns.Add("presentacion", "Presentación");
            dataGridViewDetallePedido.Columns.Add("cantidad_unidad", "Cantidad Unidades");
            dataGridViewDetallePedido.Columns.Add("cantidad_bultos", "Cantidad Bultos");
            dataGridViewDetallePedido.Columns.Add("precio_unitario", "Precio Unitario");
            dataGridViewDetallePedido.Columns.Add("subtotal", "Subtotal");
            dataGridViewDetallePedido.Columns.Add("descuento", "Descuento(%)");
            dataGridViewDetallePedido.Columns.Add("total", "Total");
            //permitir editar solo cantidad_unidad, cantidad_bultos y descuento
            dataGridViewDetallePedido.Columns["cantidad_unidad"].ReadOnly = false;
            dataGridViewDetallePedido.Columns["cantidad_bultos"].ReadOnly = false;
            dataGridViewDetallePedido.Columns["descuento"].ReadOnly = false;
            //no permitir la edición de las demás columnas            
            dataGridViewDetallePedido.Columns["ID_detalle_pedido"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["id_producto"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["ID_presentacion"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["nombre"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["presentacion"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["precio_unitario"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["subtotal"].ReadOnly = true;
            dataGridViewDetallePedido.Columns["total"].ReadOnly = true;
            //agregar boton de eliminar detalle
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.HeaderText = "Acción";
            btnEliminar.Text = "Eliminar";
            btnEliminar.Name = "btnEliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            dataGridViewDetallePedido.Columns.Add(btnEliminar);
            dataGridViewDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDetallePedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDetallePedido.ReadOnly = false;
            dataGridViewDetallePedido.MultiSelect = false;
            dataGridViewDetallePedido.Columns["id_producto"].Visible = false;
            dataGridViewDetallePedido.Columns["ID_presentacion"].Visible = false;
            dataGridViewDetallePedido.Columns["total"].Visible = false;
            dataGridViewDetallePedido.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12);
        }

        private void CargarDatosPedidoDesdeFila(int rowIndex)
        {
            // Obtener datos del cliente desde la fila seleccionada
            DataGridViewRow fila = dgvResultados.Rows[rowIndex];
            string nombre = fila.Cells["Nombre"].Value.ToString();
            string apellido = fila.Cells["Apellido"].Value.ToString();
            string zona = fila.Cells["Zona"].Value.ToString();
            string telefono = fila.Cells["Telefono"].Value.ToString();
            //Obtener usuario actual
            string usuarioActual = ObtenerUsuarioActual();
            //obtener nombre de empleado de tabla Empleado partir de nombre_usuario que corresponde al usuario actual
            Empleado empleado = new ClassEmpleadoLogica().ObtenerEmpleadoPorNombreUsuario(usuarioActual);
            if (empleado != null)
            {
                lblVendedor.Text = $"Preventista: {empleado.nombre} {empleado.apellido}";
            }
            else
            {
                lblVendedor.Text = "Empleado no encontrado";
            }
            // Cargar en labels agregando datos de la fila seleccionada al texto existente

            lblNombre.Text = $"Cliente: {nombre} {apellido}";
            
            lblTelefono.Text = $"Teléfono: {telefono}";
            lblDireccion.Text = $"Dirección: {fila.Cells["Calle"].Value} {fila.Cells["Numero"].Value}, {fila.Cells["Ciudad"].Value}, {fila.Cells["Provincia"].Value}, CP: {fila.Cells["codPostal"].Value}";
            int siguienteIdPedido = pedidoLogica.ObtenerSiguienteIdPedido();
            lblNumeroPedido.Text = $"Pedido N°: {siguienteIdPedido}";
        }
        private void CargarTodosLosClientesActivos()
        {
            dgvResultados.Rows.Clear();
            if (string.IsNullOrEmpty(usuarioActual))
            {
                MessageBox.Show("No se ha definido un preventista", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
            if (id_zona == 0)
            {
                MessageBox.Show("No se encontró una zona asociada al preventista actual.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener clientes de la zona
            var clientes = clienteLogica.ClientesPorZona(id_zona);
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("No hay clientes registrados en la zona del preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //cargar zonas en comboBoxClienteZona
            var zonas = clienteLogica.ObtenerZonas();
            //cargar dgvResultados
            foreach (var cliente in clientes)
            {
                string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                string confiableTexto = cliente.confiable ? "Si" : "No";
                string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                string nombreTamano = clienteLogica.ObtenerTamanos().FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                dgvResultados.Rows.Add(
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
        private void CargarTodosLosProductosActivosConStock()
        {
            //limpiar dataGridViewProductos
            dataGridViewProductos.Rows.Clear();
            //cargar dataGridViewProductos con todos los productos de la base de datos
            var productosPresentacion = new ClassProductoLogica().ListarProductoPresentacionActivosConStock();
            var productos = new ClassProductoLogica().ObtenerListaProductos();
            var presentaciones = new ClassProductoLogica().ObtenerListaPresentaciones();
            var marcasLista = new ClassMarcaLogica().ObtenerTodasLasMarcas();
            foreach (var pp in productosPresentacion)
            {
                var producto = productos.FirstOrDefault(p => p.id_producto == pp.id_producto);
                var presentacion = presentaciones.FirstOrDefault(pr => pr.ID_presentacion == pp.ID_presentacion);
                var marca = marcasLista.FirstOrDefault(m => m.id_marca == producto.id_marca);
                var stockProducto = new ClassProductoLogica().ObtenerStockPorProductoYPresentacion(pp.id_producto, pp.ID_presentacion);
                if (producto != null && presentacion != null)
                {
                    dataGridViewProductos.Rows.Add(
                        producto.nombre,
                        presentacion.descripcion,
                        pp.cod_producto,
                        pp.precioLista,
                        stockProducto.stock_actual,
                        marca != null ? marca.nombre : "Marca desconocida",
                        pp.ID_presentacion,
                        pp.id_producto
                        );
                }
            }
        }
        private void timerFechaHora_Tick(object sender, EventArgs e)
        {
            lblFechaHora.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void btnVerTodosClientes_Click(object sender, EventArgs e)
        {
            CargarTodosLosClientesActivos();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            //clear textBoxDNI
            textBoxDNI.Clear();
            //buscar cliente por cualquier campo de tabla CLIENTE a partir de txtBuscarCliente
            // y cargar datos en el dgvResultados
            string filtro = txtBuscarCliente.Text.ToLower();
            if (string.IsNullOrEmpty(filtro))
            {
                MessageBox.Show("Ingrese un término de búsqueda.");
                return;
            }
            //busqueda general en todas las columnas de la tabla cliente
            var zonas = clienteLogica.ObtenerZonas();
            var tamanos = clienteLogica.ObtenerTamanos();
            // Buscar zona asociada al preventista
            int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
            var listaClientes = clienteLogica.ClientesPorZona(id_zona);
            var clientesFiltrados = listaClientes.Where(cliente =>
                (cliente.nombre != null && cliente.nombre.ToLower().Contains(filtro)) ||
                (cliente.apellido != null && cliente.apellido.ToLower().Contains(filtro)) ||
                (cliente.dni.ToString().ToLower().Contains(filtro)) ||
                (cliente.telefono.ToString().ToLower().Contains(filtro)) ||
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
            dgvResultados.Rows.Clear();
            foreach (var cliente in clientesFiltrados)
            {
                string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                string confiableTexto = cliente.confiable ? "Si" : "No";
                string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                dgvResultados.Rows.Add(
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

        private void textBoxDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo numeros y control y no mas de 8 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxDNI, "Solo se permiten números.");
            }
            else
            {
                errorProvider1.SetError(textBoxDNI, "");
            }
            if (textBoxDNI.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxDNI, "No se permiten más de 8 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxDNI, "");
            }
        }

        private void textBoxDNI_KeyDown(object sender, KeyEventArgs e)
        {
            //limpiar txtBuscarCliente
            txtBuscarCliente.Clear();
            //si presiona enter buscar cliente por dni
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                string dni = textBoxDNI.Text;
                if (string.IsNullOrEmpty(dni))
                {
                    MessageBox.Show("Ingrese un DNI para buscar.");
                    return;
                }
                //buscar cliente por dni y cargar datos en el dgvResultados
                var zonas = clienteLogica.ObtenerZonas();
                var tamanos = clienteLogica.ObtenerTamanos();
                // Buscar zona asociada al preventista
                int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
                var listaClientes = clienteLogica.ClientesPorZona(id_zona);
                var clientesFiltrados = listaClientes.Where(cliente =>
                    (cliente.dni.ToString().ToLower().Contains(dni))
                ).ToList();
                // Limpiar las filas actuales del DataGridView
                dgvResultados.Rows.Clear();
                foreach (var cliente in clientesFiltrados)
                {
                    string estadoTexto = cliente.estado ? "Activo" : "Inactivo";
                    string confiableTexto = cliente.confiable ? "Si" : "No";
                    string nombreZona = zonas.FirstOrDefault(z => z.id_zona == cliente.id_zona)?.nombre ?? "Zona desconocida";
                    string nombreTamano = tamanos.FirstOrDefault(t => t.id_tamano == cliente.id_tamano)?.descripcion ?? "Tamaño desconocido";
                    dgvResultados.Rows.Add(
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

        private void btnRegistrarNuevo_Click(object sender, EventArgs e)
        {
            // Instanciar el formulario
            FormAltaCliente formCliente = new FormAltaCliente();
            //Ubicar en el centro de pnlVistaMenuSecundario como showDialog
            formCliente.StartPosition = FormStartPosition.CenterParent;
            formCliente.ShowDialog();
        }

        private void textBoxCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permitir solo numeros y no mas de 10 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxCodigo, "Solo se permiten números.");
            }
            else
            {
                errorProvider1.SetError(textBoxCodigo, "");
            }
            if (textBoxCodigo.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxCodigo, "No se permiten más de 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxCodigo, "");
            }
        }

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            //limpiar dataGridViewProductos
            dataGridViewProductos.Rows.Clear();
            //si presiona enter buscar producto por codigo y cargar datos en el dataGridViewProductos a partir de PRESENTACION, PRODUCTO Y producto_presentacion
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                string codigo = textBoxCodigo.Text;
                if (string.IsNullOrEmpty(codigo))
                {
                    MessageBox.Show("Ingrese un código para buscar.");
                    return;
                }
                //buscar producto por codigo y cargar datos en el dataGridViewProductos a partir de PRESENTACION, PRODUCTO Y producto_presentacion
                int codProducto;
                if (!int.TryParse(codigo, out codProducto))
                {
                    MessageBox.Show("El código debe ser un número válido.");
                    return;
                }
                var productoPresentacion = new ClassProductoLogica().ObtenerProductoPresentacionPorCodigo(codProducto);
                if (productoPresentacion != null)
                {
                    var producto = new ClassProductoLogica().ObtenerProductoPorId(productoPresentacion.id_producto);
                    var presentacion = new ClassProductoLogica().ObtenerPresentacionPorId(productoPresentacion.ID_presentacion);
                    var stockProducto = new ClassProductoLogica().ObtenerStockPorProductoYPresentacion(productoPresentacion.id_producto, productoPresentacion.ID_presentacion);
                    var marca = new ClassMarcaLogica().ObtenerMarcaPorId(producto.id_marca);
                    if (producto != null && presentacion != null)
                    {
                        // Agregar fila al dataGridViewProductos
                        dataGridViewProductos.Rows.Add(
                            producto.nombre,
                            presentacion.descripcion,
                            productoPresentacion.cod_producto,
                            productoPresentacion.precioLista,
                            stockProducto.stock_actual,
                            marca.nombre,
                            productoPresentacion.ID_presentacion,
                            productoPresentacion.id_producto

                        );
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el producto o la presentación asociada.");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún producto con el código proporcionado.");
                    if (new ClassProductoLogica().ErroresValidacion.Any())
                    {
                        MessageBox.Show(string.Join("\n", new ClassProductoLogica().ErroresValidacion));
                    }
                }
            }
        }

        private void comboBoxFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {

            //buscar productos y producto_presentacion por familia seleccionada en combobox
            if (comboBoxFamilia.SelectedIndex == -1) return;

            if (comboBoxFamilia.SelectedValue is int idFamiliaSeleccionada && idFamiliaSeleccionada > 0)
            {

                var productos = productoLogica.ObtenerProductosPorFamilia(idFamiliaSeleccionada);
                // Obtener presentaciones activas con stock
                var productosPresentacion = productoLogica.ListarProductoPresentacionActivosConStock()
                                            .Where(pp => productos.Any(p => p.id_producto == pp.id_producto))
                                            .ToList();
                var presentaciones = productoLogica.ObtenerListaPresentaciones();
                var marcasLista = marcaLogica.ObtenerTodasLasMarcas();

                // Limpiar el DataGridView antes de cargar nuevos datos
                dataGridViewProductos.Rows.Clear();
                comboBoxFamilia.SelectedIndex = -1;
                foreach (var pp in productosPresentacion)
                {
                    var producto = productos.FirstOrDefault(p => p.id_producto == pp.id_producto);
                    var presentacion = presentaciones.FirstOrDefault(pr => pr.ID_presentacion == pp.ID_presentacion);
                    var marca = marcasLista.FirstOrDefault(m => m.id_marca == producto.id_marca);
                    var stockProducto = new ClassProductoLogica().ObtenerStockPorProductoYPresentacion(pp.id_producto, pp.ID_presentacion);

                    if (producto != null && presentacion != null)
                    {
                        dataGridViewProductos.Rows.Add(
                            producto.nombre,
                            presentacion.descripcion,
                            pp.cod_producto,
                            pp.precioLista,
                            stockProducto.stock_actual,
                            marca != null ? marca.nombre : "Marca desconocida",
                            pp.ID_presentacion,
                            pp.id_producto
                        );
                    }
                }
            }
        }

        private void comboBoxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            //buscar productos y producto_presentacion por marca seleccionada en combobox
            if (comboBoxMarca.SelectedIndex == -1) return;

            if (comboBoxMarca.SelectedValue is int idMarcaSeleccionada && idMarcaSeleccionada > 0)
            {

                var productos = productoLogica.ObtenerProductoPorMarca(idMarcaSeleccionada);
                // Obtener presentaciones activas con stock
                var productosPresentacion = productoLogica.ListarProductoPresentacionActivosConStock()
                                            .Where(pp => productos.Any(p => p.id_producto == pp.id_producto))
                                            .ToList();
                var presentaciones = productoLogica.ObtenerListaPresentaciones();
                var familiaLista = familiaLogica.ObtenerTodasLasFamilias();
                var marca = marcaLogica.ObtenerMarcaPorId(idMarcaSeleccionada);

                // Limpiar el DataGridView antes de cargar nuevos datos
                dataGridViewProductos.Rows.Clear();
                comboBoxMarca.SelectedIndex = -1;


                foreach (var pp in productosPresentacion)
                {
                    var producto = productos.FirstOrDefault(p => p.id_producto == pp.id_producto);
                    var presentacion = presentaciones.FirstOrDefault(pr => pr.ID_presentacion == pp.ID_presentacion);
                    var stockProducto = productoLogica.ObtenerStockPorProductoYPresentacion(pp.id_producto, pp.ID_presentacion);

                    if (producto != null && presentacion != null)
                    {
                        dataGridViewProductos.Rows.Add(
                            producto.nombre,
                            presentacion.descripcion,
                            pp.cod_producto,
                            pp.precioLista,
                            stockProducto.stock_actual,
                            marca.nombre,
                            pp.ID_presentacion,
                            pp.id_producto
                        );
                    }
                }
            }
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            //limpiar dataGridViewProductos            
            CargarTodosLosProductosActivosConStock();
        }

        private void dgvResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarDatosPedidoDesdeFila(e.RowIndex);
            }
        }

        private void dgvResultados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvResultados.CurrentRow != null)
            {
                e.Handled = true;
                CargarDatosPedidoDesdeFila(dgvResultados.CurrentRow.Index);
            }
        }

        private void dataGridViewProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //si se presiona el btnAgregarD eliminar fila
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewProductos.Columns["btnAgregarProducto"].Index)
            {

                DataGridViewRow fila = dataGridViewProductos.Rows[e.RowIndex];
                // Verificar si el producto ya está en el detalle del pedido
                foreach (DataGridViewRow detalleFila in dataGridViewDetallePedido.Rows)
                {
                    if (detalleFila.Cells["id_producto"].Value.ToString() == fila.Cells["id_producto"].Value.ToString() &&
                        detalleFila.Cells["ID_presentacion"].Value.ToString() == fila.Cells["ID_presentacion"].Value.ToString())
                    {
                        MessageBox.Show("El producto ya está en el detalle del pedido.");
                        return;
                    }
                }
                //obtener unidades por bulto de la tabla producto_presentacion
                int id_producto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                int ID_presentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(id_producto, ID_presentacion);
                if (productoPresentacion == null)
                {
                    MessageBox.Show("No se encontró el producto con la presentación seleccionada.");
                    return;
                }
                //obtener unidades por bulto
                int unidadesPorBulto = productoPresentacion.unidades_bulto;
                // Agregar el producto al detalle del pedido con cantidad inicial de 0 unidad y 0 bulto
                decimal precioUnitario = Convert.ToDecimal(fila.Cells["precioLista"].Value);
                int cantidadUnidades = 0;
                int cantidadBultos = 0;
                decimal subtotal = precioUnitario * cantidadUnidades + cantidadBultos * unidadesPorBulto * precioUnitario;
                decimal descuento = 0; // Inicialmente sin descuento
                decimal total = subtotal - (subtotal * descuento / 100);
                dataGridViewDetallePedido.Rows.Add(
                    dataGridViewDetallePedido.Rows.Count + 1,
                    fila.Cells["id_producto"].Value,
                    fila.Cells["ID_presentacion"].Value,
                    fila.Cells["nombre"].Value,
                    fila.Cells["presentacion"].Value,
                    cantidadUnidades,
                    cantidadBultos,
                    precioUnitario,
                    subtotal,
                    descuento,
                    total
                );
            }
        }

        private void dataGridViewProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Agregar producto seleccionado a dataGridViewDetallePedido
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridViewProductos.Rows[e.RowIndex];
                // Verificar si el producto ya está en el detalle del pedido
                foreach (DataGridViewRow detalleFila in dataGridViewDetallePedido.Rows)
                {
                    if (detalleFila.Cells["id_producto"].Value.ToString() == fila.Cells["id_producto"].Value.ToString() &&
                        detalleFila.Cells["ID_presentacion"].Value.ToString() == fila.Cells["ID_presentacion"].Value.ToString())
                    {
                        MessageBox.Show("El producto ya está en el detalle del pedido.");
                        return;
                    }
                }
                //obtener unidades por bulto de la tabla producto_presentacion
                int id_producto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                int ID_presentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(id_producto, ID_presentacion);
                if (productoPresentacion == null)
                {
                    MessageBox.Show("No se encontró el producto con la presentación seleccionada.");
                    return;
                }
                //obtener unidades por bulto
                int unidadesPorBulto = productoPresentacion.unidades_bulto;
                // Agregar el producto al detalle del pedido con cantidad inicial de 0 unidad y 0 bulto
                decimal precioUnitario = Convert.ToDecimal(fila.Cells["precioLista"].Value);
                int cantidadUnidades = 0;
                int cantidadBultos = 0;
                decimal subtotal = precioUnitario * cantidadUnidades + cantidadBultos * unidadesPorBulto * precioUnitario;
                decimal descuento = 0; // Inicialmente sin descuento
                decimal total = subtotal - (subtotal * descuento / 100);
                dataGridViewDetallePedido.Rows.Add(
                    dataGridViewDetallePedido.Rows.Count + 1,
                    fila.Cells["id_producto"].Value,
                    fila.Cells["ID_presentacion"].Value,
                    fila.Cells["nombre"].Value,
                    fila.Cells["presentacion"].Value,
                    cantidadUnidades,
                    cantidadBultos,
                    precioUnitario,
                    subtotal,
                    descuento,
                    total
                );
            }
        }

        private void dataGridViewDetallePedido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //si se presiona el boton eliminar eliminar fila
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewDetallePedido.Columns["btnEliminar"].Index)
            {
                dataGridViewDetallePedido.Rows.RemoveAt(e.RowIndex);
                ReasignarIdsDetallePedido();
            }
        }
        private void ReasignarIdsDetallePedido()
        {
            int contador = 1;

            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;

                fila.Cells["ID_detalle_pedido"].Value = contador;
                contador++;
            }
        }

        private void dataGridViewDetallePedido_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (cancelarModificacion || e.RowIndex < 0) return;

            var fila = dataGridViewDetallePedido.Rows[e.RowIndex];
            string columna = dataGridViewDetallePedido.Columns[e.ColumnIndex].Name;
            string valor = e.FormattedValue?.ToString() ?? "";

            if (columna == "cantidad_unidad" || columna == "cantidad_bultos")
            {
                if (!int.TryParse(valor, out int resultado) || resultado < 0)
                {
                    MessageBox.Show($"La columna '{columna}' debe contener un número entero positivo.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fila.Cells[columna].Value = 0;
                }
            }
            else if (columna == "descuento")
            {
                if (!decimal.TryParse(valor, out decimal resultado) || resultado < 0)
                {
                    MessageBox.Show("El valor de descuento debe ser un número decimal positivo.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fila.Cells["descuento"].Value = 0;
                }
            }
        }

        private void dataGridViewDetallePedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dataGridViewDetallePedido.Rows[e.RowIndex];
            string columna = dataGridViewDetallePedido.Columns[e.ColumnIndex].Name;

            if (columna == "cantidad_unidad" || columna == "cantidad_bultos" || columna == "descuento")
            {
                RecalcularTotales(fila);
            }
        }

        private void dataGridViewDetallePedido_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewDetallePedido.IsCurrentCellDirty)
                dataGridViewDetallePedido.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void RecalcularTotales(DataGridViewRow fila)
        {
            try
            {
                int cantidadUnidades = Convert.ToInt32(fila.Cells["cantidad_unidad"].Value ?? 0);
                int cantidadBultos = Convert.ToInt32(fila.Cells["cantidad_bultos"].Value ?? 0);
                decimal precioUnitario = Convert.ToDecimal(fila.Cells["precio_unitario"].Value ?? 0);

                int id_producto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                int ID_presentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(id_producto, ID_presentacion);
                if (productoPresentacion == null) return;

                int unidadesPorBulto = productoPresentacion.unidades_bulto;

                // Cálculo seguro del subtotal
                decimal subtotal = precioUnitario * cantidadUnidades + cantidadBultos * unidadesPorBulto * precioUnitario;
                fila.Cells["subtotal"].Value = Math.Round(subtotal, 2);

                //Intentar aplicar descuento
                try
                {
                    decimal descuento = Convert.ToDecimal(fila.Cells["descuento"].Value ?? 0);
                    if (descuento < 0) throw new Exception();

                    decimal total = subtotal - (subtotal * descuento / 100);
                    fila.Cells["total"].Value = Math.Round(total, 2);
                }
                catch
                {
                    fila.Cells["total"].Value = "Error";
                    MessageBox.Show("El valor de descuento no es válido. Se omitió el cálculo del total.", "Descuento inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                fila.Cells["subtotal"].Value = "Error";
                fila.Cells["total"].Value = "Error";
                MessageBox.Show("No se pudo calcular el subtotal. Verificá que los valores ingresados sean válidos.", "Error de cálculo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewDetallePedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y control en las columnas de cantidad y descuento
            if (dataGridViewDetallePedido.CurrentCell != null &&
                (dataGridViewDetallePedido.CurrentCell.OwningColumn.Name == "cantidad_unidad" ||
                 dataGridViewDetallePedido.CurrentCell.OwningColumn.Name == "cantidad_bultos" ||
                 dataGridViewDetallePedido.CurrentCell.OwningColumn.Name == "descuento"))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                    errorProvider1.SetError(dataGridViewDetallePedido, "Solo se permiten números.");
                }
                else
                {
                    errorProvider1.SetError(dataGridViewDetallePedido, "");
                }
            }
        }

        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            cancelarModificacion = true;
            //limpiar datos del pedido
            lblNombre.Text = "Cliente: ";
            lblTelefono.Text = "Teléfono: ";
            lblDireccion.Text = "Dirección: ";
            lblVendedor.Text = "Vendedor: ";
            lblNumeroPedido.Text = "Pedido N°: ";
            dataGridViewDetallePedido.Rows.Clear();
            txtBuscarCliente.Clear();
            textBoxDNI.Clear();
            textBoxCodigo.Clear();
            CargarTodosLosClientesActivos();
            CargarTodosLosProductosActivosConStock();
            cancelarModificacion = false;
        }
        private bool ValidarDatosPedido()
        {
            // Validar fecha
            if (dateTimePicker1.Value.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha de entrega no debe ser anterior a la actual.");
                return false;
            }
            // Validar cliente seleccionado
            if (dgvResultados.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.");
                return false;
            }
            // Validar que haya productos en el pedido
            if (dataGridViewDetallePedido.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto al pedido.");
                return false;
            }
            // Validar cantidades en cada fila
            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;
                int cantidadUnidad = 0;
                int cantidadBultos = 0;

                if (fila.Cells["cantidad_unidad"].Value != null)
                    int.TryParse(fila.Cells["cantidad_unidad"].Value.ToString(), out cantidadUnidad);
                if (fila.Cells["cantidad_bultos"].Value != null)
                    int.TryParse(fila.Cells["cantidad_bultos"].Value.ToString(), out cantidadBultos);
                if (cantidadUnidad <= 0 && cantidadBultos <= 0)
                {
                    MessageBox.Show("Cada producto debe tener al menos una cantidad en unidades o bultos.", "Validación de cantidades", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private decimal CalcularTotalPedido()
        {
            decimal sumaTotal = 0;
            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.Cells["total"].Value != null)
                {
                    decimal valor;
                    if (decimal.TryParse(fila.Cells["total"].Value.ToString(), out valor))
                    {
                        sumaTotal += valor;
                    }
                }
            }
            return sumaTotal;
        }
        private List<DETALLE_PEDIDO> CrearDetallesDesdeGrid(int idPedido)
        {
            var detalles = new List<DETALLE_PEDIDO>();

            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;

                int id_producto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                int id_presentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                int cantidadUnidades = Convert.ToInt32(fila.Cells["cantidad_unidad"].Value);
                int cantidadBultos = Convert.ToInt32(fila.Cells["cantidad_bultos"].Value);

                var producto = productoLogica.ObtenerProductoPorId(id_producto);
                var presentacion = productoLogica.ObtenerPresentacionPorId(id_presentacion);
                var producto_Presentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(id_producto, id_presentacion);
                var stock = productoLogica.ObtenerStockPorProductoYPresentacion(id_producto, id_presentacion);

                if (stock == null || (cantidadUnidades + cantidadBultos * producto_Presentacion.unidades_bulto > stock.stock_actual))
                {
                    return null; // Stock insuficiente o inexistente
                }

                var detalle = new DETALLE_PEDIDO
                {
                    id_pedido = idPedido,
                    id_producto = id_producto,
                    ID_presentacion = id_presentacion,
                    ID_detalle_pedido = Convert.ToInt32(fila.Cells["ID_detalle_pedido"].Value),
                    cantidad = cantidadUnidades,
                    precio_unitario = Convert.ToDecimal(fila.Cells["precio_unitario"].Value),
                    descuento = Convert.ToDecimal(fila.Cells["descuento"].Value),
                    cantidad_bultos = cantidadBultos
                };

                detalles.Add(detalle);
            }

            return detalles.Count > 0 ? detalles : null;
        }


       
        private void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            if (!ValidarDatosPedido()) return;

            if (dgvResultados.CurrentCell == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int filaIndex = dgvResultados.CurrentCell.RowIndex;
            int idCliente = Convert.ToInt32(dgvResultados.Rows[filaIndex].Cells["id_cliente"].Value);
            DateTime fechaActual = DateTime.Now;
            decimal total = CalcularTotalPedido();
            string vendedor = ObtenerUsuarioActual();

            // Validar detalles antes de crear el pedido
            var detallesTemporales = CrearDetallesDesdeGrid(-1); // Usamos -1 como ID temporal
            if (detallesTemporales == null)
            {
                MessageBox.Show("No se puede crear el pedido. Hay productos con stock insuficiente o faltante.", "Error de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult confirmacion = MessageBox.Show(
                $"¿Desea confirmar la creación del pedido por un total de {total:C}?",
                "Confirmar pedido",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
            {
                MessageBox.Show("Operación cancelada por el usuario.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int estadoPedido;

            //Preguntar si es venta en el local
            DialogResult esVentaEnElLocal = MessageBox.Show(
               "Es Venta en el Local?",
               "Confirmar Entrega Inmediata",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );
            if (esVentaEnElLocal == DialogResult.Yes)
            {
                estadoPedido = 3;
            }
            else
            {
                estadoPedido = 1;
            }

            // Crear pedido
            int idPedidoGenerado = pedidoLogica.CrearPedido(fechaActual, dateTimePicker1.Value, idCliente, estadoPedido, total, vendedor);
            if (idPedidoGenerado == -1)
            {
                string errores = string.Join("\n", pedidoLogica.ErroresValidacion);
                MessageBox.Show("Error al crear el pedido:\n" + errores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Auditoría del pedido
            string usuarioActual = ObtenerUsuarioActual();
            string resumenPedido =
                $"ID Pedido: {idPedidoGenerado}, Fecha: {fechaActual:dd/MM/yyyy}, Entrega: {dateTimePicker1.Value:dd/MM/yyyy}, " +
                $"Cliente ID: {idCliente}, Total: {total:C}, Vendedor: {vendedor}";

            bool registradoPedido = auditoriaLogica.Registrar(
                valorAnterior: "-",
                valorNuevo: resumenPedido,
                nombreAccion: "Alta",
                nombreEntidad: "PEDIDO",
                usuario: usuarioActual
            );

            if (!registradoPedido)
            {
                MessageBox.Show("Pedido creado, pero no se pudo registrar auditoría:\n" +
                    string.Join("\n", auditoriaLogica.ErroresValidacion),
                    "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Asignar ID generado a los detalles
            foreach (var detalle in detallesTemporales)
            {
                detalle.id_pedido = idPedidoGenerado;
            }

            // Guardar detalles
            if (pedidoLogica.GuardarDetalles(detallesTemporales))
            {

                //Auditoria de detalles
                foreach (var detalle in detallesTemporales)
                {
                    string resumenDetalle =
                        $"Pedido ID: {detalle.id_pedido}, Producto: {detalle.id_producto}, Presentación: {detalle.ID_presentacion}, " +
                        $"Cantidad: {detalle.cantidad}, Bultos: {detalle.cantidad_bultos}, Precio: {detalle.precio_unitario:C}, Descuento: {detalle.descuento} %";

                    bool registrado = auditoriaLogica.Registrar(
                        valorAnterior: "-",
                        valorNuevo: resumenDetalle,
                        nombreAccion: "Alta",
                        nombreEntidad: "DETALLE_PEDIDO",
                        usuario: usuarioActual
                    );

                    if (!registrado)
                    {
                        MessageBox.Show("No se pudo registrar auditoría para el detalle:\n" +
                            string.Join("\n", auditoriaLogica.ErroresValidacion),
                            "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //Obtener presentación y calcular unidades totales
                    var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(detalle.id_producto, detalle.ID_presentacion);
                    if (productoPresentacion == null)
                    {
                        MessageBox.Show("No se pudo obtener la presentación del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    int unidadesPorBulto = productoPresentacion.unidades_bulto;
                    int cantidadTotal = (detalle.cantidad ?? 0) + ((detalle.cantidad_bultos ?? 0) * unidadesPorBulto);
                    cantidadTotal *= -1;

                    // Descontar stock
                    bool descontado = pedidoLogica.ActualizarStock(detalle.id_producto, detalle.ID_presentacion, cantidadTotal);
                    if (!descontado)
                    {
                        MessageBox.Show("Error al descontar stock:\n" +
                            string.Join("\n", pedidoLogica.ErroresValidacion),
                            "Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show("Pedido guardado correctamente.");
                CargarTodosLosProductosActivosConStock();
                dataGridViewDetallePedido.Rows.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                string errores = string.Join("\n", pedidoLogica.ErroresValidacion);
                MessageBox.Show("Error al guardar los detalles:\n" + errores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btnModificarPedido_Click(object sender, EventArgs e)
        {
            //abrir formulario clientes
            FormModificarPedidoPreventista formModificarPedidoPreventista = new FormModificarPedidoPreventista();
            formModificarPedidoPreventista.StartPosition = FormStartPosition.CenterScreen;
            FormClosedEventHandler value1 = (s, args) => this.Show();
            FormClosedEventHandler value = value1;
            formModificarPedidoPreventista.FormClosed += value;
            formModificarPedidoPreventista.Show();
            this.Hide();
        }
    }
}
