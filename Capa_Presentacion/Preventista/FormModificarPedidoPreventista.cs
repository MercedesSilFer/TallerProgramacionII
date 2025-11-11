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
                        var estadoPedido = pedidoLogica.ObtenerEstadoPorId(pedido.id_estado);

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
                            estadoPedido.descripcion,
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
            string usuarioActual = ObtenerUsuarioActual();
            //obtener nombre de empleado de tabla Empleado partir de nombre_usuario que corresponde al usuario actual
            Empleado empleado = new ClassEmpleadoLogica().ObtenerEmpleadoPorNombreUsuario(usuarioActual);
            if (empleado != null)
            {
                lblNombre.Text = $"{empleado.nombre} {empleado.apellido}";
            }
            //crear dataGridviewModificarPedidos con columnas
            dataGridViewModificarPedidos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12);
            dataGridViewModificarPedidos.Columns.Add("id_pedido", "ID_Pedido");
            dataGridViewModificarPedidos.Columns.Add("fecha_creacion", "Fecha Creación");
            dataGridViewModificarPedidos.Columns.Add("fecha_entrega", "Fecha de Entrega");
            dataGridViewModificarPedidos.Columns.Add("id_cliente", "ID_Cliente");
            //ocultar columna id_cliente
            dataGridViewModificarPedidos.Columns["id_cliente"].Visible = false;
            dataGridViewModificarPedidos.Columns.Add("nombre_cliente", "Nombre Cliente");
            dataGridViewModificarPedidos.Columns.Add("id_estado", "ID_Estado");
            dataGridViewModificarPedidos.Columns.Add("estado", "Estado");            
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
                    comboBoxEstados.Enabled = false;
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

                // Cancelado y entregado todo deshabilitado
                               

                comboBoxEstados.Enabled = false; // el preventista no puede modificar estado del pedido
                dateTimePicker2.Enabled = !(esCancelado || esEntregado);
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
            //Botón VER 
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
                var estadoPedido = pedidoLogica.ObtenerEstadoPorId(pedido.id_estado);
                string nombreVendedor = $"{empleado.nombre} {empleado.apellido}";
                dataGridViewModificarPedidos.Rows.Add(
                    pedido.id_pedido,
                    pedido.fecha_creacion.ToString("dd/MM/yyyy"),
                    pedido.fecha_entrega.ToString("dd/MM/yyyy"),
                    pedido.id_cliente,
                    nombreCompleto,                    
                    pedido.id_estado,
                    estadoPedido.descripcion,
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

        private void txtMontoMaximo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBoxNumeroFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = textBoxNumeroFactura.Text.Trim();

                if (int.TryParse(textoBusqueda, out int numeroFactura))
                {
                    dataGridViewModificarPedidos.Rows.Clear();

                    // Buscar pedido por número de factura y preventista
                    PEDIDO pedido = pedidoLogica.ObtenerPedidoPorNumeroFacturaYVendedor(numeroFactura, usuarioActual);

                    if (pedido != null)
                    {
                        var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                        string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";

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
                    else
                    {
                        MessageBox.Show("No se encontró ningún pedido con ese número de factura para este preventista.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un número de factura válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dataGridViewModificarPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewModificarPedidos.Columns[e.ColumnIndex].Name == "btnFactura")
            {
                var numeroFactura = dataGridViewModificarPedidos.Rows[e.RowIndex].Cells["numero_factura"].Value?.ToString();

                e.Value = string.IsNullOrEmpty(numeroFactura) ? " " : "VER";
                e.FormattingApplied = true;
            }
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
            dataGridViewProductos.Rows.Clear();
            CargarTodosLosProductosActivosConStock();
        }

        private void btnFechaEntrega_Click(object sender, EventArgs e)
        {
            //Obtener todos los pedidos con la fecha de entrega indicada en dateTimePicker1
            DateTime fechaSeleccionada = dateTimePicker1.Value.Date;
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaEntregaYPreventista(fechaSeleccionada, usuarioActual);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void btnCreacion_Click(object sender, EventArgs e)
        {
            //Obtener todos los pedidos con la fecha de creación indicada en dateTimePicker4
            DateTime fechaSeleccionada = dateTimePicker4.Value.Date;
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaCreacionYPreventista(fechaSeleccionada, usuarioActual);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void dataGridViewProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //si se presiona el btnAgregarProducto 
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewProductos.Columns["btnAgregarProducto"].Index)
            {
                // Paso 1: Validaciones previas
                var filaPedido = dataGridViewModificarPedidos.CurrentRow;
                if (filaPedido == null)
                {
                    MessageBox.Show("Debe seleccionar un pedido para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPedidoSeleccionado = Convert.ToInt32(filaPedido.Cells["id_pedido"].Value);
                var pedido = pedidoLogica.ObtenerPedidoPorId(idPedidoSeleccionado);

                if (pedido.id_estado == 4 || pedido.id_estado == 3)
                {
                    MessageBox.Show("No se pueden modificar pedidos en estado 'Entregado' o 'Cancelado'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCancelarModificacion.PerformClick();
                    return;
                }
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

        private void dataGridViewDetallePedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dataGridViewDetallePedido.Rows[e.RowIndex];

            if (fila.Cells["cantidad_unidad"].Value == null ||
                fila.Cells["cantidad_bultos"].Value == null ||
                fila.Cells["descuento"].Value == null) return;
            // Validar que los valores sean numéricos mayores o iguales a cero
            int cantidadUnidades;
            int cantidadBultos;
            decimal descuento;
            if (!int.TryParse(fila.Cells["cantidad_unidad"].Value.ToString(), out cantidadUnidades) || cantidadUnidades < 0)
            {
                //
                MessageBox.Show("La cantidad de unidades debe ser un número entero positivo.");
                fila.Cells["cantidad_unidad"].Value = 0;

                return;
            }

            if (!int.TryParse(fila.Cells["cantidad_bultos"].Value.ToString(), out cantidadBultos) || cantidadBultos < 0)
            {
                MessageBox.Show("La cantidad de bultos debe ser un número entero positivo.");
                fila.Cells["cantidad_bultos"].Value = 0;
                return;
            }
            descuento = Convert.ToDecimal(fila.Cells["descuento"].Value);
            decimal precioUnitario = Convert.ToDecimal(fila.Cells["precio_unitario"].Value);
            int id_producto = Convert.ToInt32(fila.Cells["id_producto"].Value);
            int ID_presentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
            var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(id_producto, ID_presentacion);
            if (productoPresentacion == null) return;

            int unidadesPorBulto = productoPresentacion.unidades_bulto;
            decimal subtotal = precioUnitario * cantidadUnidades + cantidadBultos * unidadesPorBulto * precioUnitario;
            decimal total = subtotal - (subtotal * descuento / 100);

            fila.Cells["subtotal"].Value = subtotal;
            fila.Cells["total"].Value = total;
        }

        private void dataGridViewDetallePedido_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewDetallePedido.IsCurrentCellDirty)
            {
                dataGridViewDetallePedido.CommitEdit(DataGridViewDataErrorContexts.Commit);
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

        private void btnModificarPedido_Click(object sender, EventArgs e)
        {
            // Paso 1: Validaciones previas
            var filaPedido = dataGridViewModificarPedidos.CurrentRow;
            if (filaPedido == null)
            {
                MessageBox.Show("Debe seleccionar un pedido para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPedidoSeleccionado = Convert.ToInt32(filaPedido.Cells["id_pedido"].Value);
            var pedido = pedidoLogica.ObtenerPedidoPorId(idPedidoSeleccionado);

            if (pedido.id_estado == 4 || pedido.id_estado == 3)
            {
                MessageBox.Show("No se pueden modificar pedidos en estado 'Entregado' o 'Cancelado'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnCancelarModificacion.PerformClick();
                return;
            }

            int idEstadoAnterior = pedido.id_estado;
            int idEstadoNuevo = Convert.ToInt32(comboBoxEstados.SelectedValue);

            

            // Validar fecha de entrega
            DateTime fechaEntrega = dateTimePicker2.Value.Date;
            if (dateTimePicker2.Enabled && fechaEntrega < DateTime.Today)
            {
                MessageBox.Show("La fecha de entrega no debe ser anterior a la actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker2.Value = DateTime.Now;
                return;
            }

            if (dataGridViewDetallePedido.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto al pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            //Datos actuales del pedido
            DateTime fechaCreacion = pedido.fecha_creacion;
            int idCliente = pedido.id_cliente;
            var cliente = clienteLogica.ObtenerClientePorId(idCliente);
            string vendedor = pedido.vendedor;
            int? numeroFacturaAnterior = pedido.numero_factura;

            var detallesAnteriores = pedidoLogica.ObtenerDetallesPedido(idPedidoSeleccionado);

            bool pedidoEntregadoYCancelado = idEstadoAnterior == 3 && idEstadoNuevo == 4;

            // Validar stock futuro solo si el pedido no está siendo cancelado
            if (!pedidoEntregadoYCancelado)
            {
                foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
                {
                    if (fila.IsNewRow) continue;

                    int idProducto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                    int idPresentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                    int cantidadUnidad = 0, cantidadBultos = 0;
                    decimal descuento = 0;

                    if (!int.TryParse(fila.Cells["cantidad_unidad"].Value?.ToString(), out cantidadUnidad) || cantidadUnidad < 0 ||
                        !int.TryParse(fila.Cells["cantidad_bultos"].Value?.ToString(), out cantidadBultos) || cantidadBultos < 0)
                    {
                        MessageBox.Show("Las cantidades deben ser valores numéricos mayores o iguales a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cantidadUnidad == 0 && cantidadBultos == 0)
                    {
                        MessageBox.Show("Cada producto debe tener al menos una cantidad en unidades o bultos mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(fila.Cells["descuento"].Value?.ToString(), out descuento) || descuento < 0 || descuento > 100)
                    {
                        MessageBox.Show("El descuento debe ser un número entre 0 y 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(idProducto, idPresentacion);
                    int unidadesPorBulto = productoPresentacion.unidades_bulto;
                    int cantidadSolicitada = cantidadUnidad + (cantidadBultos * unidadesPorBulto);

                    var stock = productoLogica.ObtenerStockPorProductoYPresentacion(idProducto, idPresentacion);
                    int stockActual = stock?.stock_actual ?? 0;

                    var detalleAnterior = detallesAnteriores.FirstOrDefault(d =>
                        d.id_producto == idProducto && d.ID_presentacion == idPresentacion);

                    int cantidadAnterior = 0;
                    if (detalleAnterior != null)
                    {
                        int unidadesAnteriores = detalleAnterior.cantidad ?? 0;
                        int bultosAnteriores = detalleAnterior.cantidad_bultos ?? 0;
                        cantidadAnterior = unidadesAnteriores + (bultosAnteriores * unidadesPorBulto);
                    }

                    int stockFuturo = stockActual + cantidadAnterior;

                    if (stockFuturo < cantidadSolicitada)
                    {
                        MessageBox.Show($"Stock insuficiente para el producto '{fila.Cells["nombre"].Value}' ({cantidadSolicitada} solicitado, {stockFuturo} disponible tras reposición).", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // Reposición de stock si corresponde
            if (!pedidoEntregadoYCancelado)
            {
                foreach (var detalle in detallesAnteriores)
                {
                    var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(detalle.id_producto, detalle.ID_presentacion);
                    int unidadesPorBulto = productoPresentacion.unidades_bulto;
                    int cantidadTotal = (detalle.cantidad ?? 0) + ((detalle.cantidad_bultos ?? 0) * unidadesPorBulto);
                    // Mensaje de prueba para verificar cada detalle
                    /*MessageBox.Show(
                        $"Producto: {detalle.id_producto}, Presentación: {detalle.ID_presentacion}\n" +
                        $"Unidades: {detalle.cantidad ?? 0}, Bultos: {detalle.cantidad_bultos ?? 0}, Unidades por bulto: {unidadesPorBulto}\n" +
                        $"Cantidad total a reponer: {cantidadTotal}",
                        "Detalle de reposición",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    */
                    bool repuesto = pedidoLogica.ActualizarStock(detalle.id_producto, detalle.ID_presentacion, cantidadTotal);
                    if (!repuesto)
                    {
                        MessageBox.Show("Error al reponer stock:\n" + string.Join("\n", productoLogica.ErroresValidacion), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        //Mostrar stock
                        var stock = productoLogica.ObtenerStockPorProductoYPresentacion(detalle.id_producto, detalle.ID_presentacion);
                        /*if (stock != null)
                        {
                            MessageBox.Show(
                                $"Stock actualizado para Producto {detalle.id_producto}, Presentación {detalle.ID_presentacion}:\n" +
                                $"Stock actual: {stock.stock_actual}",
                                "Stock después de reposición",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        else
                        {
                            MessageBox.Show("No se pudo obtener el stock actualizado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }*/
                    }
                }
            }

            //Auditoria
            foreach (var detalle in detallesAnteriores)
            {
                string resumenDetalleEliminado =
                    $"Pedido ID: {detalle.id_pedido}, Producto: {detalle.id_producto}, Presentación: {detalle.ID_presentacion}, " +
                    $"Cantidad: {detalle.cantidad}, Bultos: {detalle.cantidad_bultos}, Precio: {detalle.precio_unitario:C}, Descuento: {detalle.descuento:P}";

                bool registradoEliminado = auditoriaLogica.Registrar(
                    valorAnterior: resumenDetalleEliminado,
                    valorNuevo: "-",
                    nombreAccion: "Baja",
                    nombreEntidad: "DETALLE_PEDIDO",
                    usuario: ObtenerUsuarioActual()
                );

                if (!registradoEliminado)
                {
                    MessageBox.Show("No se pudo registrar auditoría para el detalle eliminado:\n" +
                        string.Join("\n", auditoriaLogica.ErroresValidacion),
                        "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }



            if (!pedidoEntregadoYCancelado)
            {
                pedidoLogica.EliminarDetallesPedido(idPedidoSeleccionado);
            }

            var detallesNuevos = new List<DETALLE_PEDIDO>();
            if (!pedidoEntregadoYCancelado)
            {
                foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
                {
                    if (fila.IsNewRow) continue;

                    int idProducto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                    int idPresentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                    int ID_detalle_pedido = Convert.ToInt32(fila.Cells["ID_detalle_pedido"].Value);
                    int cantidadUnidad = Convert.ToInt32(fila.Cells["cantidad_unidad"].Value);
                    int cantidadBultos = Convert.ToInt32(fila.Cells["cantidad_bultos"].Value);
                    decimal precioUnitario = Convert.ToDecimal(fila.Cells["precio_unitario"].Value);
                    decimal descuento = Convert.ToDecimal(fila.Cells["descuento"].Value);

                    detallesNuevos.Add(new DETALLE_PEDIDO
                    {
                        id_pedido = idPedidoSeleccionado,
                        id_producto = idProducto,
                        ID_presentacion = idPresentacion,
                        ID_detalle_pedido = ID_detalle_pedido,
                        cantidad = cantidadUnidad,
                        cantidad_bultos = cantidadBultos,
                        precio_unitario = precioUnitario,
                        descuento = descuento
                    });

                    var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(idProducto, idPresentacion);
                    int unidadesPorBulto = productoPresentacion.unidades_bulto;
                    int cantidadTotal = cantidadUnidad + (cantidadBultos * unidadesPorBulto);

                    /* Mensaje de prueba para verificar cada descuento
                    MessageBox.Show(
                        $"Producto: {idProducto}, Presentación: {idPresentacion}\n" +
                        $"Unidades: {cantidadUnidad}, Bultos: {cantidadBultos}, Unidades por bulto: {unidadesPorBulto}\n" +
                        $"Cantidad total a descontar: {cantidadTotal}",
                        "Detalle de descuento",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );*/

                    bool descontado = pedidoLogica.ActualizarStock(idProducto, idPresentacion, -(cantidadTotal));
                    if (!descontado)
                    {
                        MessageBox.Show("Error al descontar stock:\n" + string.Join("\n", productoLogica.ErroresValidacion), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        var stock = productoLogica.ObtenerStockPorProductoYPresentacion(idProducto, idPresentacion);
                        if (stock != null)
                        {
                            /* MessageBox.Show(
                                 $"Stock actualizado para Producto {idProducto}, Presentación {idPresentacion}:\n" +
                                 $"Stock actual: {stock.stock_actual}",
                                 "Stock después de descuento",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information
                             );*/
                        }
                        else
                        {
                            MessageBox.Show("No se pudo obtener el stock actualizado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }


            // Calcular nuevo total
            decimal totalPedido = 0;

            if (!pedidoEntregadoYCancelado)
            {
                foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
                {
                    if (fila.IsNewRow) continue;

                    decimal subtotal = 0, descuento = 0;

                    var valorSubtotal = fila.Cells["subtotal"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(valorSubtotal) || !decimal.TryParse(valorSubtotal, out subtotal) || subtotal < 0)
                    {
                        MessageBox.Show("El subtotal debe ser un número válido mayor o igual a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var valorDescuento = fila.Cells["descuento"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(valorDescuento))
                    {
                        if (!decimal.TryParse(valorDescuento, out descuento) || descuento < 0 || descuento > 100)
                        {
                            MessageBox.Show("El descuento debe ser un número entre 0 y 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    totalPedido += subtotal - (subtotal * descuento / 100);
                }
            }

            // Paso 6: Reposición de stock si el pedido entregado fue cancelado
            if (idEstadoAnterior == 3 && idEstadoNuevo == 4)
            {

                foreach (var detalle in detallesAnteriores)
                {
                    var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(detalle.id_producto, detalle.ID_presentacion);
                    int unidadesPorBulto = productoPresentacion.unidades_bulto;
                    int cantidadTotal = (detalle.cantidad ?? 0) + ((detalle.cantidad_bultos ?? 0) * unidadesPorBulto);

                    bool repuesto = pedidoLogica.ActualizarStock(detalle.id_producto, detalle.ID_presentacion, cantidadTotal);
                    if (!repuesto)
                    {
                        MessageBox.Show("Error al reponer stock por cancelación:\n" + string.Join("\n", productoLogica.ErroresValidacion), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }


            if (!pedidoEntregadoYCancelado)
            {
                // Paso 7: Actualizar pedido
                var pedidoModificado = pedidoLogica.ModificarPedido(
                idPedidoSeleccionado,
                fechaCreacion,
                fechaEntrega,
                idCliente,
                idEstadoNuevo,
                totalPedido,
                numeroFacturaAnterior,
                vendedor
                );
                //Auditorias
                if (pedidoModificado != null)
                {
                    string usuarioActual = ObtenerUsuarioActual();

                    string resumenAnterior =
                        $"Estado: {idEstadoAnterior}, Fecha Entrega: {pedido.fecha_entrega:dd/MM/yyyy}, Total: {pedido.total:C}";

                    string resumenNuevo =
                        $"Estado: {idEstadoNuevo}, Fecha Entrega: {fechaEntrega:dd/MM/yyyy}, Total: {totalPedido:C}";

                    bool registradoPedido = auditoriaLogica.Registrar(
                        valorAnterior: resumenAnterior,
                        valorNuevo: resumenNuevo,
                        nombreAccion: "Modificacion",
                        nombreEntidad: "PEDIDO",
                        usuario: usuarioActual
                    );

                    if (!registradoPedido)
                    {
                        MessageBox.Show("Pedido modificado, pero no se pudo registrar auditoría:\n" +
                            string.Join("\n", auditoriaLogica.ErroresValidacion),
                            "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                var pedidoModificado = pedidoLogica.ModificarPedido(
                    idPedidoSeleccionado,
                    pedido.fecha_creacion,
                    pedido.fecha_entrega,
                    pedido.id_cliente,
                    idEstadoNuevo,
                    pedido.total,
                    pedido.numero_factura,
                    pedido.vendedor
                    );
                if (pedidoModificado != null)
                {
                    string usuarioActual = ObtenerUsuarioActual();

                    string resumenAnterior =
                        $"Estado: {idEstadoAnterior}, Fecha Entrega: {pedido.fecha_entrega:dd/MM/yyyy}, Total: {pedido.total:C}";

                    string resumenNuevo =
                        $"Estado: {idEstadoNuevo}, Fecha Entrega: {fechaEntrega:dd/MM/yyyy}, Total: {totalPedido:C}";

                    bool registradoPedido = auditoriaLogica.Registrar(
                        valorAnterior: resumenAnterior,
                        valorNuevo: resumenNuevo,
                        nombreAccion: "Modificacion",
                        nombreEntidad: "PEDIDO",
                        usuario: usuarioActual
                    );

                    if (!registradoPedido)
                    {
                        MessageBox.Show("Pedido modificado, pero no se pudo registrar auditoría:\n" +
                            string.Join("\n", auditoriaLogica.ErroresValidacion),
                            "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }

            // Paso 8: Guardar los nuevos detalles si corresponde
            if (!pedidoEntregadoYCancelado)
            {

                if (pedidoLogica.GuardarDetalles(detallesNuevos))
                {
                    foreach (var detalle in detallesNuevos)
                    {
                        string resumenDetalle =
                            $"Pedido ID: {detalle.id_pedido}, Producto: {detalle.id_producto}, Presentación: {detalle.ID_presentacion}, " +
                            $"Cantidad: {detalle.cantidad}, Bultos: {detalle.cantidad_bultos}, Precio: {detalle.precio_unitario:C}, Descuento: {detalle.descuento:P}";

                        bool registradoDetalle = auditoriaLogica.Registrar(
                            valorAnterior: "-",
                            valorNuevo: resumenDetalle,
                            nombreAccion: "Alta",
                            nombreEntidad: "DETALLE_PEDIDO",
                            usuario: ObtenerUsuarioActual()
                        );

                        if (!registradoDetalle)
                        {
                            MessageBox.Show("Detalle guardado, pero no se pudo registrar auditoría:\n" +
                                string.Join("\n", auditoriaLogica.ErroresValidacion),
                                "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //MessageBox.Show("Detalles del pedido guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Error al guardar los detalles del pedido: " + string.Join("\n", pedidoLogica.ErroresValidacion), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            var pedidoActualizado = pedidoLogica.ObtenerPedidoPorId(idPedidoSeleccionado);
           
            // Actualizar cuenta corriente si corresponde
            if (cliente.confiable)
            {
                decimal totalAnterior = pedido.total;

                bool pasoDeNoEntregadoAEntregado =
                    pedidoActualizado.id_estado == 3 &&
                    (pedido.id_estado == 1 || pedido.id_estado == 2 || pedido.id_estado == 5);

                bool pasoDeEntregadoACancelado =
                    pedido.id_estado == 3 && pedidoActualizado.id_estado == 4;

                // MessageBox.Show($"Estado anterior: {idEstadoAnterior}, nuevo: {idEstadoNuevo}");


                if (pasoDeNoEntregadoAEntregado)
                {
                    //MessageBox.Show($"Monto a sumar: {pedidoActualizado.total}");

                    bool sumado = clienteLogica.SumarSaldoPorPedidoEntregado(idCliente, pedidoActualizado.total);
                    if (!sumado)
                    {
                        string errores = string.Join("\n", clienteLogica.ErroresValidacion);
                        MessageBox.Show("No se pudo sumar el nuevo saldo a la cuenta corriente:\n" + errores, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (pasoDeEntregadoACancelado)
                {
                    bool restado = clienteLogica.SumarSaldoPorPedidoEntregado(idCliente, -pedidoActualizado.total);
                    if (!restado)
                    {
                        string errores = string.Join("\n", clienteLogica.ErroresValidacion);
                        MessageBox.Show("No se pudo restar el saldo de la cuenta corriente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            //Finalizar
            MessageBox.Show("Pedido modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
            CargarTodosLosProductosActivosConStock();
            dataGridViewDetallePedido.Rows.Clear();
        }

        private void dataGridViewModificarPedidos_SelectionChanged(object sender, EventArgs e)
        {
            // Limpiar detalles si hay una fila seleccionada
            if (dataGridViewModificarPedidos.SelectedRows.Count > 0)
            {
                dataGridViewDetallePedido.Rows.Clear();
                comboBoxEstados.SelectedIndex = -1;
                dateTimePicker2.Value = DateTime.Now;

                //desactivar controles hasta que se presione btnVerDetalles
                comboBoxEstados.Enabled = false;
                dateTimePicker2.Enabled = false;
                btnModificarPedido.Enabled = false;
                dataGridViewDetallePedido.DefaultCellStyle.BackColor = Color.White;
            }
        }
    }
    }


