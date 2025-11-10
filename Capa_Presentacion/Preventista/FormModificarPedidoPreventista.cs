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
using Font = iTextSharp.text.Font;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ArimaERP.Preventista
{
    public partial class FormModificarPedidoPreventista : Form
    {
        ClassClienteLogica clienteLogica = new ClassClienteLogica();
        ClassPedidoLogica pedidoLogica = new ClassPedidoLogica();
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        ClassUsuarioLogica usuarioLogica = new ClassUsuarioLogica();
        ClassProductoLogica productoLogica = new ClassProductoLogica();
        ClassMarcaLogica marcaLogica = new ClassMarcaLogica();
        ClassFamiliaLogica familiaLogica = new ClassFamiliaLogica();
        ClassAuditoriaLogica auditoriaLogica = new ClassAuditoriaLogica();
        ClassZonaLogica zonaLogica = new ClassZonaLogica();
        private string usuarioActual;
        public FormModificarPedidoPreventista()
        {
            InitializeComponent();
            usuarioActual = ObtenerUsuarioActual();
        }
        private string ObtenerUsuarioActual()
        {

            return UsuarioSesion.Nombre;
        }
      

        private void txtBuscarCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = txtBuscarCliente.Text.Trim();
                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    MessageBox.Show("Ingrese un DNI o Email para buscar.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
                // Obtener el ID del cliente
                int? idCliente = pedidoLogica.ObtenerIdClientePorTextoYZona(textoBusqueda, id_zona);                             
               

                // Limpiar el DataGrid antes de cargar nuevos datos
                dataGridViewModificarPedidos.Rows.Clear();

                if (idCliente.HasValue)
                {
                    // Obtener los pedidos del cliente
                    List<PEDIDO> pedidos = pedidoLogica.ObtenerPedidosPorIdCliente(idCliente.Value);
                    if (pedidos == null || pedidos.Count == 0)
                    {
                        MessageBox.Show("El cliente no tiene pedidos registrados.", "Sin pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    foreach (var pedido in pedidos)
                    {
                        //Obtener CLIENTE por pedido.id_cliente
                        var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                        string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";

                        //Obtener VENDEDOR por pedido.vendedor
                        var empleado = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(pedido.vendedor);
                        string nombreVendedor = $"{empleado.nombre} {empleado.apellido}";

                        dataGridViewModificarPedidos.Rows.Add(
                            pedido.id_pedido,
                            pedido.fecha_creacion.ToString("dd/MM/yyyy"),
                            pedido.fecha_entrega.ToString("dd/MM/yyyy"),
                            pedido.id_cliente,
                            nombreCompleto,
                            pedido.id_estado,
                            pedido.total.ToString("C"),
                            pedido.numero_factura,
                            pedido.vendedor,
                            nombreVendedor
                        );
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún cliente con ese DNI o Email.", "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void FormModificarPedidoPreventista_Load(object sender, EventArgs e)
        {
            //crear dataGridviewModificarPedidos con columnas
            dataGridViewModificarPedidos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12);
            dataGridViewModificarPedidos.Columns.Add("id_pedido", "ID_Pedido");
            dataGridViewModificarPedidos.Columns.Add("fecha_creacion", "Fecha Creación");
            dataGridViewModificarPedidos.Columns.Add("fecha_entrega", "Fecha de Entrega");
            dataGridViewModificarPedidos.Columns.Add("id_cliente", "ID_Cliente");
            //ocultar columna id_cliente
            dataGridViewModificarPedidos.Columns["id_cliente"].Visible = false;
            dataGridViewModificarPedidos.Columns.Add("nombre_cliente", "Nombre Cliente");
            dataGridViewModificarPedidos.Columns.Add("estado", "Estado");
            dataGridViewModificarPedidos.Columns.Add("id_estado", "ID_Estado");
            //ocultar columna id_estado
            dataGridViewModificarPedidos.Columns["id_estado"].Visible = false;
            dataGridViewModificarPedidos.Columns.Add("total", "Total");
            dataGridViewModificarPedidos.Columns.Add("numero_factura", "Factura N°");
            dataGridViewModificarPedidos.Columns.Add("vendedor", "Vendedor");
            //ocultar columna vendedor
            dataGridViewModificarPedidos.Columns["vendedor"].Visible = false;
            dataGridViewModificarPedidos.Columns.Add("nombre_vendedor", "Vendedor");
            //Agregar botón de ver detalles
            DataGridViewButtonColumn btnVerDetalles = new DataGridViewButtonColumn();
            btnVerDetalles.HeaderText = "Detalles";
            btnVerDetalles.Name = "btnVerDetalles";
            btnVerDetalles.Text = "Ver";
            btnVerDetalles.UseColumnTextForButtonValue = true;
            dataGridViewModificarPedidos.Columns.Add(btnVerDetalles);
            dataGridViewModificarPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewModificarPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewModificarPedidos.MultiSelect = false;
            dataGridViewModificarPedidos.AllowUserToAddRows = false;
            //NO permitir modificar el ancho de las filas
            dataGridViewModificarPedidos.AllowUserToResizeRows = false;
            
            dataGridViewModificarPedidos.Columns["fecha_entrega"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["estado"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["total"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["numero_factura"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["nombre_cliente"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["nombre_vendedor"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["id_pedido"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["fecha_creacion"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["id_cliente"].ReadOnly = true;
            dataGridViewModificarPedidos.Columns["vendedor"].ReadOnly = true;
            //agregar btnGenerarFactura
            DataGridViewButtonColumn btnFactura = new DataGridViewButtonColumn();
            btnFactura.HeaderText = "Factura";
            btnFactura.Name = "btnFactura";
            btnFactura.Text = "Generar";
            btnFactura.UseColumnTextForButtonValue = false;
            dataGridViewModificarPedidos.Columns.Add(btnFactura);
            //agregar btnEliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Name = "eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            dataGridViewModificarPedidos.Columns.Add(btnEliminar);

            //cargar dataGridViewDetallePedido con columnas
            dataGridViewDetallePedido.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12);
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
            //No permitir modificar el ancho de las filas
            dataGridViewDetallePedido.AllowUserToResizeRows = false;
            //agregar boton de eliminar detalle
            DataGridViewButtonColumn btnEliminarDetalle = new DataGridViewButtonColumn();
            btnEliminarDetalle.HeaderText = "Acción";
            btnEliminarDetalle.Text = "Eliminar";
            btnEliminarDetalle.Name = "btnEliminar";
            btnEliminarDetalle.UseColumnTextForButtonValue = true;
            dataGridViewDetallePedido.Columns.Add(btnEliminarDetalle);
            dataGridViewDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDetallePedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDetallePedido.ReadOnly = false;
            dataGridViewDetallePedido.MultiSelect = false;
            dataGridViewDetallePedido.Columns["id_producto"].Visible = false;
            dataGridViewDetallePedido.Columns["ID_presentacion"].Visible = false;
            dataGridViewDetallePedido.Columns["total"].Visible = false;
            dataGridViewDetallePedido.AllowUserToAddRows = false;
            
            var zonas = clienteLogica.ObtenerZonas();
    
            //cargar estados en comboBoxBuscarPorEstado
            var estados = pedidoLogica.ObtenerEstadosPedido();
            comboBoxBuscarPorEstado.DataSource = estados;
            comboBoxBuscarPorEstado.DisplayMember = "descripcion";
            comboBoxBuscarPorEstado.ValueMember = "id_estado";
            comboBoxBuscarPorEstado.SelectedIndex = -1; // No seleccionar nada al inicio
            //cargar estados en comboBoxEstados
            var estadoPedido = pedidoLogica.ObtenerEstadosPedido();
            comboBoxEstados.DataSource = estadoPedido;
            comboBoxEstados.DisplayMember = "descripcion";
            comboBoxEstados.ValueMember = "id_estado";
            comboBoxEstados.SelectedIndex = -1; // No seleccionar nada al inicio

            //cargar todos los pedidos en dataGridViewModificarPedidos
            int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
            List<PEDIDO> todosLosPedidos = pedidoLogica.ObtenerPedidosPorZona(id_zona);
            CargarPedidosEnDataGridView(todosLosPedidos);
            //cargar dataGridViewProductos con columnas
            dataGridViewProductos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12);
            dataGridViewProductos.Columns.Add("nombre", "Nombre");
            dataGridViewProductos.Columns.Add("presentacion", "Presentación");
            dataGridViewProductos.Columns.Add("unidades_bultos", "Unidades x Bulto");
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
            dataGridViewDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProductos.MultiSelect = false;
            dataGridViewProductos.ReadOnly = true;
            dataGridViewProductos.AllowUserToAddRows = false;
            //No permitir modificar el ancho de las filas
            dataGridViewProductos.AllowUserToResizeRows = false;
            //cargar todos los productos activos con stock en dataGridViewProductos
            CargarTodosLosProductosActivosConStock();
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
        }

        private void btnTodosLosPedidos_Click(object sender, EventArgs e)
        {
            int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerPedidosPorZona(id_zona));
        }

        private void textBoxNumeroFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ingresar solo números y no mas de 10 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxNumeroFactura, "Ingrese solo números.");
            }
            else if (
                textBoxNumeroFactura.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxNumeroFactura, "No puede ingresar más de 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxNumeroFactura, "");
            }
        }

        private void txtMontoMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Permitir control keys (como Backspace)
            if (char.IsControl(e.KeyChar))
                return;

            // Permitir solo dígitos y un punto decimal
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                errorProvider1.SetError(tb, "Ingrese solo números y un punto decimal.");
                return;
            }

            // Evitar más de un punto decimal
            if (e.KeyChar == '.' && tb.Text.Contains('.'))
            {
                e.Handled = true;
                errorProvider1.SetError(tb, "Solo se permite un punto decimal.");
                return;
            }

            // Simular el texto final si se permite el carácter
            string textoFinal = tb.Text.Insert(tb.SelectionStart, e.KeyChar.ToString());

            // Validar formato decimal
            if (decimal.TryParse(textoFinal, out decimal monto))
            {
                // Validar máximo permitido por decimal(8,2)
                if (monto > 999999.99m)
                {
                    e.Handled = true;
                    errorProvider1.SetError(tb, "El monto no puede superar 999999.99.");
                    return;
                }

                // Validar cantidad de decimales
                int indexPunto = textoFinal.IndexOf('.');
                if (indexPunto >= 0 && textoFinal.Length - indexPunto - 1 > 2)
                {
                    e.Handled = true;
                    errorProvider1.SetError(tb, "Solo se permiten dos decimales.");
                    return;
                }

                errorProvider1.SetError(tb, ""); // Sin errores
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(tb, "Formato inválido.");
            }
        }

        private void dataGridViewModificarPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBoxEstados.SelectedIndex = -1;
            //visualizar fecha actual
            dateTimePicker2.Value = DateTime.Now;
            if (e.RowIndex < 0) return;
            var fila = dataGridViewModificarPedidos.Rows[e.RowIndex];
            // Botón Ver Detalles
            int idPedido = Convert.ToInt32(fila.Cells["id_pedido"].Value);
            var pedido = pedidoLogica.ObtenerPedidoPorId(idPedido);
            
            if (e.ColumnIndex == dataGridViewModificarPedidos.Columns["btnVerDetalles"].Index)
            {
                //cargar estado del pedido en el comboBoxEstados
                if (pedido.id_pedido > 0)
                {
                    comboBoxEstados.SelectedValue = pedido.id_estado;
                    dateTimePicker2.Value = pedido.fecha_entrega;

                }
                List<DETALLE_PEDIDO> detallesPedido = pedidoLogica.ObtenerDetallesPedido(idPedido);
                dataGridViewDetallePedido.Rows.Clear();
                foreach (var detalle in detallesPedido)
                {
                    var producto = productoLogica.ObtenerProductoPorId(detalle.id_producto);
                    var presentacion = productoLogica.ObtenerPresentacionPorId(detalle.ID_presentacion);
                    //Obtener producto_presentacion para obtener unidades_bulto
                    var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(producto.id_producto, presentacion.ID_presentacion);
                    int unidadesPorBulto = productoPresentacion.unidades_bulto;
                    // Reemplaza la línea problemática en dataGridViewModificarPedidos_CellContentClick
                    decimal subtotal = (detalle.precio_unitario * (detalle.cantidad ?? 0)) + ((detalle.cantidad_bultos ?? 0) * unidadesPorBulto * detalle.precio_unitario);
                    decimal total = subtotal - (subtotal * detalle.descuento / 100);

                    dataGridViewDetallePedido.Rows.Add(
                        detalle.ID_detalle_pedido,
                        detalle.id_producto,
                        detalle.ID_presentacion,
                        producto.nombre,
                        presentacion.descripcion,
                        detalle.cantidad,
                        detalle.cantidad_bultos,
                        detalle.precio_unitario,
                        subtotal,
                        detalle.descuento,
                        total

                    );
                }
                bool esCancelado = pedido.id_estado == 4;
                bool esEntregado = pedido.id_estado == 3;

                // Cancelado: todo deshabilitado
                // Entregado: solo comboBox habilitado
                // Otros: todo habilitado

                comboBoxEstados.Enabled = true; // el preventista no puede modificar estado del pedido
                dateTimePicker2.Enabled = true;// el preventista no puede reasignar fecha de entrega
                btnModificarPedido.Enabled = !(esCancelado) || !(esEntregado);

                // Estilo visual del DataGridView
                if (esCancelado || esEntregado)
                {
                    dataGridViewDetallePedido.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else
                {
                    dataGridViewDetallePedido.DefaultCellStyle.BackColor = Color.White;
                }

                // Desactivar edición en celdas si está cancelado o entregado
                foreach (DataGridViewColumn col in dataGridViewDetallePedido.Columns)
                {
                    if (col.Name == "cantidad_unidad" || col.Name == "cantidad_bultos" || col.Name == "descuento")
                    {
                        col.ReadOnly = esCancelado || esEntregado;
                    }
                }
                dataGridViewDetallePedido.Columns["btnEliminar"].Visible = !(esCancelado || esEntregado);



            }
            // Botón Eliminar
            else if (e.ColumnIndex == dataGridViewModificarPedidos.Columns["eliminar"].Index)
            {
                //Eliminar pedido solo si esta en estado "Pendiente", "En Preparación" o "Retrasado" y no tiene factura generada
                if (pedido.id_estado != 1 && pedido.id_estado != 2 && pedido.id_estado != 5)
                {
                    MessageBox.Show("Solo se pueden eliminar pedidos en estado 'Pendiente' o 'En Preparación' o 'Retrasado'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este pedido?", "Confirmar Eliminación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Obtener los detalles del pedido
                    var detalles = pedidoLogica.ObtenerDetallesPedido(idPedido); // Devuelve lista de objetos con id_producto, ID_presentacion

                    //  Restaurar el stock
                    foreach (var detalle in detalles)
                    {
                        //Obtener producto_presentacion
                        var producto_pres = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(detalle.id_producto, detalle.ID_presentacion);

                        //Obtener cantidad
                        int cantidad;
                        cantidad = (detalle.cantidad ?? 0) + ((detalle.cantidad_bultos ?? 0) * producto_pres.unidades_bulto);

                        // Sumamos la cantidad al stock actual
                        bool actualizado = pedidoLogica.ActualizarStock(detalle.id_producto, detalle.ID_presentacion, cantidad);
                        if (!actualizado)
                        {
                            MessageBox.Show(string.Join("\n", pedidoLogica.ErroresValidacion), "Error al actualizar stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    //  Eliminar los detalles del pedido
                    if (!pedidoLogica.EliminarDetallesPedido(idPedido))
                    {
                        MessageBox.Show(string.Join("\n", pedidoLogica.ErroresValidacion), "Error al eliminar detalles del pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Eliminar el pedido
                    if (!pedidoLogica.EliminarPedido(idPedido))
                    {
                        MessageBox.Show(string.Join("\n", pedidoLogica.ErroresValidacion), "Error al eliminar el pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {

                        MessageBox.Show("Pedido eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
                        CargarPedidosEnDataGridView(pedidoLogica.ObtenerPedidosPorZona(id_zona));
                        CargarTodosLosProductosActivosConStock();
                        dataGridViewDetallePedido.Rows.Clear();
                    }
                }
            }
            //Botón VER o GENERAR
            else if (dataGridViewModificarPedidos.Columns[e.ColumnIndex].Name == "btnFactura")                                   
            
            // 1. Al hacer clic en el botón de factura, obtener el estado y el número de factura del pedido seleccionado.
            // 2. Si el pedido ya tiene número de factura, mostrar la factura existente.
                           
            {
                var filaActual = dataGridViewModificarPedidos.Rows[e.RowIndex];
                var numeroFactura = filaActual.Cells["numero_factura"].Value?.ToString();
                var idEstado = Convert.ToInt32(filaActual.Cells["id_estado"].Value);

                if (!string.IsNullOrEmpty(numeroFactura))
                {
                    VerFactura(numeroFactura);
                    return;
                }                              
               
            }

        }
        private void VerFactura(string numeroFactura)
        {
            // Buscar primero la factura ANULADA
            string rutaFacturaAnulada = Path.Combine(Application.StartupPath, $"Factura_{numeroFactura}_ANULADA.pdf");
            string rutaFacturaNormal = Path.Combine(Application.StartupPath, $"Factura_{numeroFactura}.pdf");

            if (File.Exists(rutaFacturaAnulada))
            {
                System.Diagnostics.Process.Start(rutaFacturaAnulada);
            }
            else if (File.Exists(rutaFacturaNormal))
            {
                System.Diagnostics.Process.Start(rutaFacturaNormal);
            }
            else
            {
                MessageBox.Show("El archivo de la factura no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancelarModificacion_Click(object sender, EventArgs e)
        {
            //limpiar dataGridViewDetallePedido
            dataGridViewDetallePedido.Rows.Clear();
            //limpiar comboBoxEstados
            comboBoxEstados.SelectedIndex = -1;
            //visualizar fecha actual
            dateTimePicker2.Value = DateTime.Now;
            //visualizar todos los pedidos en dataGridViewModificarPedidos
            int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerPedidosPorZona(id_zona));
            //visualizar todos los productos activos y con stock en dataGridViewProductos
            CargarTodosLosProductosActivosConStock();
            //Habilitar botones y controles
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
                        pp.unidades_bulto,
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
        private void CargarPedidosEnDataGridView(List<PEDIDO> pedidos)
        {
            dataGridViewModificarPedidos.Rows.Clear();

            foreach (var pedido in pedidos)
            {
                //Obtener CLIENTE por pedido.id_cliente
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                //Obtener VENDEDOR por pedido.vendedor
                var empleado = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(pedido.vendedor);
                var estadoPedido = pedidoLogica.ObtenerEstadosPedido().FirstOrDefault(e => e.id_estado == pedido.id_estado);
                string nombreVendedor = $"{empleado.nombre} {empleado.apellido}";
                dataGridViewModificarPedidos.Rows.Add(
                    pedido.id_pedido,
                    pedido.fecha_creacion.ToString("dd/MM/yyyy"),
                    pedido.fecha_entrega.ToString("dd/MM/yyyy"),
                    pedido.id_cliente,
                    nombreCompleto,
                    estadoPedido.descripcion,
                    pedido.id_estado,
                    pedido.total.ToString("C"),
                    pedido.numero_factura,
                    pedido.vendedor,
                    nombreVendedor
                );
            }
        }

        private void comboBoxBuscarPorEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            if (comboBoxBuscarPorEstado.SelectedValue is int idEstadoSeleccionado && idEstadoSeleccionado > 0)
            {
                dataGridViewModificarPedidos.Rows.Clear();

                // Obtener pedidos por estado Y preventista actual
                List<PEDIDO> pedidosFiltrados = pedidoLogica.ObtenerPedidosPorEstadoYVendedor(idEstadoSeleccionado, usuarioActual);

                if (pedidosFiltrados == null || pedidosFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron pedidos con ese estado para el preventista.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    CargarPedidosEnDataGridView(pedidosFiltrados);
                }

                comboBoxBuscarPorEstado.SelectedIndex = -1; // resetear comboBox
            }
        }

    }
}

