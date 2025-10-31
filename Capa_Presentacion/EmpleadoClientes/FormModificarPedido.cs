using Capa_Entidades;
using Capa_Logica;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = iTextSharp.text.Font;

namespace ArimaERP.EmpleadoClientes
{
    public partial class FormModificarPedido : Form
    {
        ClassClienteLogica clienteLogica = new ClassClienteLogica();
        ClassPedidoLogica pedidoLogica = new ClassPedidoLogica();
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        ClassUsuarioLogica usuarioLogica = new ClassUsuarioLogica();
        ClassProductoLogica productoLogica = new ClassProductoLogica();
        ClassMarcaLogica marcaLogica = new ClassMarcaLogica();
        ClassFamiliaLogica familiaLogica = new ClassFamiliaLogica();
        public FormModificarPedido()
        {
            InitializeComponent();
        }
      

        
        private void textBoxNumeroPedido_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
            if (e.ColumnIndex == dataGridViewModificarPedidos.Columns["Column12"].Index)
            {
                int idPedido = Convert.ToInt32(fila.Cells["id_pedido"].Value);
                //cargar estado del pedido en el comboBoxEstados
                if (idPedido > 0)
                {
                    int idEstado = Convert.ToInt32(fila.Cells["id_estado"].Value);
                    comboBoxEstados.SelectedValue = idEstado;
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
            }
            // Botón Eliminar
            else if (e.ColumnIndex == dataGridViewModificarPedidos.Columns["eliminar"].Index)
            {
                //Eliminar pedido solo si esta en estado "Pendiente" o "En Proceso" y no tiene factura generada
                //Obtener id_estado del pedido
                var idEstado = Convert.ToInt32(fila.Cells["id_estado"].Value);
                var numeroFactura = fila.Cells["numero_factura"].Value?.ToString();

                if (idEstado != 1 && idEstado != 2 && idEstado != 5)
                {
                    MessageBox.Show("Solo se pueden eliminar pedidos en estado 'Pendiente' o 'En Preparación' o 'Retrasado'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(numeroFactura))
                {
                    MessageBox.Show("No se pueden eliminar pedidos con factura generada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este pedido?", "Confirmar Eliminación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int idPedido = Convert.ToInt32(fila.Cells["id_pedido"].Value);
                    bool eliminado = pedidoLogica.EliminarPedido(idPedido);

                    if (eliminado)
                    {
                        MessageBox.Show("Pedido eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //Botón VER o GENERAR
            else if (dataGridViewModificarPedidos.Columns[e.ColumnIndex].Name == "btnFactura")

            // Botón Generar Factura solo si esta en estado "Entregado"               
            // Pseudocódigo detallado para mejorar la estructura de la generación de factura:
            // 1. Al hacer clic en el botón de factura, obtener el estado y el número de factura del pedido seleccionado.
            // 2. Si el pedido ya tiene número de factura, mostrar la factura existente.
            // 3. Si el pedido NO tiene número de factura:
            //    a. Verificar que el estado sea "Entregado" (id_estado == 3).
            //    b. Si NO es "Entregado", mostrar mensaje de error y salir.
            //    c. Si es "Entregado", generar el número de factura, asignarlo y mostrar la factura.                
            {
                var filaActual = dataGridViewModificarPedidos.Rows[e.RowIndex];
                var numeroFactura = filaActual.Cells["numero_factura"].Value?.ToString();
                var idEstado = Convert.ToInt32(filaActual.Cells["id_estado"].Value);
                var idPedido = Convert.ToInt32(filaActual.Cells["id_pedido"].Value);

                if (!string.IsNullOrEmpty(numeroFactura))
                {
                    VerFactura(numeroFactura);
                    return;
                }

                // Solo permitir generar factura si el estado es "Entregado" (id_estado == 3)
                if (idEstado != 3)
                {
                    MessageBox.Show("Solo se puede generar la factura si el pedido está en estado 'Entregado'.", "Estado incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var detallesPedido = pedidoLogica.ObtenerDetallesPedido(idPedido);
                int nuevoNumeroFactura = pedidoLogica.GenerarNumeroFactura(idPedido);
                MessageBox.Show($"Número de factura asignado: {nuevoNumeroFactura}");
                var pedidoGenerado = pedidoLogica.ObtenerPedidoPorId(idPedido);
                filaActual.Cells["numero_factura"].Value = nuevoNumeroFactura;
                GenerarComprobanteFactura(pedidoGenerado, detallesPedido);
                dataGridViewModificarPedidos.InvalidateCell(filaActual.Cells["btnFactura"]);
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

        private void GenerarComprobanteFactura(PEDIDO pedido, List<DETALLE_PEDIDO> detalles)
        {
            string nombreArchivo = pedido.id_estado == 4 ? $"Factura_{pedido.numero_factura}_ANULADA.pdf" : $"Factura_{pedido.numero_factura}.pdf";

            string rutaArchivo = Path.Combine(Application.StartupPath, nombreArchivo);
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();
            // Verificar si el pedido está cancelado
            var estadoPedido = pedido.id_estado;
            bool esCancelado = estadoPedido == 4;
            if (esCancelado)
            {
                // Agregar leyenda "ANULADO" en rojo y centrado
                Paragraph anulacion = new Paragraph("ANULADO", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30, BaseColor.RED));
                anulacion.Alignment = Element.ALIGN_CENTER;
                doc.Add(anulacion);

                // Espacio visual
                doc.Add(new Paragraph("\n\n"));
            }
            var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
            var vendedor = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(pedido.vendedor);
            // Encabezado
            doc.Add(new Paragraph($"Razón Social: Distribuidora J.K.     Factura N° 2025-{pedido.numero_factura}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)));
            doc.Add(new Paragraph("CUIT: 30-71234567-8"));
            doc.Add(new Paragraph("I.V.A Responsable Inscripto"));
            doc.Add(new Paragraph("Dirección Comercial: Av. Libertad 1450,Corrientes Capital, Corrientes, Argentina"));
            doc.Add(new Paragraph("Teléfono: +54 9 (379) 4456789"));
            doc.Add(new Paragraph("Sucursal: Corrientes Centro"));
            doc.Add(new Paragraph("Inscripción AFIP: Nº 123456789 – Fecha de alta: 12/03/2010"));
            doc.Add(new Paragraph("--------------------------------------------------"));
            //Obtener fecha actual
            DateTime fechaActual = DateTime.Now;
            doc.Add(new Paragraph($"Fecha: {fechaActual.ToShortDateString()}"));
            doc.Add(new Paragraph("--------------------------------------------------"));
            doc.Add(new Paragraph($"Cliente: {cliente.nombre} {cliente.apellido}"));
            doc.Add(new Paragraph($"DNI: {cliente.dni}"));
            doc.Add(new Paragraph($"Dirección: {cliente.calle} {cliente.numero}, {cliente.ciudad}, {cliente.provincia}, CP: {cliente.cod_postal}"));
            doc.Add(new Paragraph($"Condición frente al IVA: {cliente.condicion_frenteIVA}"));
            doc.Add(new Paragraph("--------------------------------------------------"));
            doc.Add(new Paragraph($"Vendedor: {vendedor.nombre} {vendedor.apellido}"));
            doc.Add(new Paragraph("--------------------------------------------------"));
            // Tabla de detalles
            PdfPTable tabla = new PdfPTable(8);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(new float[] { 2f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f });
            Font fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
            string[] encabezados = { "Producto", "Presentación", "Cant. Unidades", "Cant. Bultos", "Precio Unitario", "Subtotal", "Descuento", "Total Producto" };
            foreach (string titulo in encabezados)
            {
                PdfPCell celda = new PdfPCell(new Phrase(titulo, fontHeader));
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                celda.Padding = 3;
                tabla.AddCell(celda);
            }

            foreach (var detalle in detalles)
            {
                var producto = productoLogica.ObtenerProductoPorId(detalle.id_producto);
                var presentacion = productoLogica.ObtenerPresentacionPorId(detalle.ID_presentacion);
                var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(producto.id_producto, presentacion.ID_presentacion);
                decimal subtotal = (detalle.cantidad ?? 0 + (detalle.cantidad_bultos ?? 0)) * detalle.precio_unitario;
                decimal totalProducto = subtotal - detalle.descuento;
                Font fontDetalle = FontFactory.GetFont(FontFactory.HELVETICA, 9); // mismo tamaño que fontHeader
                tabla.AddCell(new Phrase(producto.nombre.ToString(), fontDetalle));
                tabla.AddCell(new Phrase($"{presentacion.descripcion}\n{productoPresentacion.unidades_bulto} unidades/bulto", fontDetalle));
                tabla.AddCell(new Phrase((detalle.cantidad ?? 0).ToString(), fontDetalle));
                tabla.AddCell(new Phrase((detalle.cantidad_bultos ?? 0).ToString(), fontDetalle));
                tabla.AddCell(new Phrase(detalle.precio_unitario.ToString("C"), fontDetalle));
                tabla.AddCell(new Phrase(subtotal.ToString("C"), fontDetalle));
                tabla.AddCell(new Phrase(detalle.descuento.ToString("C"), fontDetalle));
                tabla.AddCell(new Phrase(totalProducto.ToString("C"), fontDetalle));
            }
            doc.Add(tabla);
            // Total
            doc.Add(new Paragraph("--------------------------------------------------"));
            doc.Add(new Paragraph($"Total:      {pedido.total.ToString("C")}"));
            doc.Close();
            // Mostrar el PDF
            System.Diagnostics.Process.Start(rutaArchivo);
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
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
            //visualizar todos los productos activos y con stock en dataGridViewProductos
            CargarTodosLosProductosActivosConStock();
        }

        private void FormModificarPedido_Load(object sender, EventArgs e)
        {
            //crear dataGridviewModificarPedidos con columnas
            dataGridViewModificarPedidos.Columns.Add("id_pedido", "ID_Pedido");
            dataGridViewModificarPedidos.Columns.Add("fecha_creacion", "Fecha Creación");
            dataGridViewModificarPedidos.Columns.Add("fecha_entrega", "Fecha de Entrega");
            dataGridViewModificarPedidos.Columns.Add("id_cliente", "ID_Cliente");
            //ocultar columna id_cliente
            dataGridViewModificarPedidos.Columns["id_cliente"].Visible = false;
            dataGridViewModificarPedidos.Columns.Add("nombre_cliente", "Nombre Cliente");
            dataGridViewModificarPedidos.Columns.Add("estado", "Estado");
            dataGridViewModificarPedidos.Columns["estado"].ReadOnly = false;
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
            btnVerDetalles.Name = "Column12";
            btnVerDetalles.Text = "Ver";
            btnVerDetalles.UseColumnTextForButtonValue = true;
            dataGridViewModificarPedidos.Columns.Add(btnVerDetalles);
            dataGridViewModificarPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewModificarPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewModificarPedidos.MultiSelect = false;
            dataGridViewModificarPedidos.AllowUserToAddRows = false;
            //NO permitir modificar el ancho de las filas
            dataGridViewModificarPedidos.AllowUserToResizeRows = false;
            //Permitir solo la edición de la columna fecha_entrega y comboEstado
            dataGridViewModificarPedidos.Columns["fecha_entrega"].ReadOnly = false;
            dataGridViewModificarPedidos.Columns["estado"].ReadOnly = false;
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
            //cargar zonas en comboBoxClienteZona
            var zonas = clienteLogica.ObtenerZonas();
            comboBoxClienteZona.DataSource = zonas;
            comboBoxClienteZona.DisplayMember = "nombre";
            comboBoxClienteZona.ValueMember = "id_zona";
            comboBoxClienteZona.SelectedIndex = -1; // No seleccionar nada al inicio
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
            List<PEDIDO> todosLosPedidos = pedidoLogica.ObtenerTodosLosPedidos();
            CargarPedidosEnDataGridView(todosLosPedidos);
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
        
        private void txtBuscarCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = txtBuscarCliente.Text.Trim();
                // Obtener el ID del cliente
                int? idCliente = pedidoLogica.ObtenerIdClientePorTexto(textoBusqueda);

                // Limpiar el DataGrid antes de cargar nuevos datos
                dataGridViewModificarPedidos.Rows.Clear();

                if (idCliente.HasValue)
                {
                    // Obtener los pedidos del cliente
                    List<PEDIDO> pedidos = pedidoLogica.ObtenerPedidosPorIdCliente(idCliente.Value);
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
        //cargar lista de pedidos en datagridviewModificarPedidos a partir de una lista de PEDIDO
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

        private void comboBoxClienteZona_SelectedIndexChanged(object sender, EventArgs e)
        {

            //filtrar clientes por zona seleccionada
            if (comboBoxClienteZona.SelectedValue is int idZona && idZona > 0)
            {
                dataGridViewModificarPedidos.Rows.Clear();
                //Obtener todos los clientes de la zona seleccionada
                List<CLIENTE> clientesEnZona = clienteLogica.ClientesPorZona(idZona);

                foreach (var cliente in clientesEnZona)
                {
                    List<PEDIDO> pedidosDelCliente = pedidoLogica.ObtenerPedidosPorIdCliente(cliente.id_cliente);
                    //cargar pedidos en dataGridViewModificarPedidos
                    CargarPedidosEnDataGridView(pedidosDelCliente);
                }
                comboBoxClienteZona.SelectedIndex = -1; //resetear comboBox después de filtrar
            }
        }

        private void comboBoxBuscarPorEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtrar pedidos por estado seleccionado
            if (comboBoxBuscarPorEstado.SelectedValue is int idEstadoSeleccionado && idEstadoSeleccionado > 0)
            {
                dataGridViewModificarPedidos.Rows.Clear();
                //Obtener todos los pedidos
                List<PEDIDO> todosLosPedidos = pedidoLogica.ObtenerPedidosPorEstado(idEstadoSeleccionado);
                CargarPedidosEnDataGridView(todosLosPedidos);
                comboBoxBuscarPorEstado.SelectedIndex = -1; //resetear comboBox después de filtrar
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //filtrar pedidos por fecha de entrega
            DateTime fechaSeleccionada = dateTimePicker1.Value.Date;
            //Obtener todos los pedidos
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaEntrega(fechaSeleccionada);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            //filtrar pedidos por fecha de creación
            DateTime fechaSeleccionada = dateTimePicker4.Value.Date;
            //Obtener todos los pedidos
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaCreacion(fechaSeleccionada);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void textBoxNumeroFactura_KeyDown(object sender, KeyEventArgs e)
        {
            //Obtener pedido por número de factura al presionar Enter
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = textBoxNumeroFactura.Text.Trim();
                if (int.TryParse(textoBusqueda, out int numeroFactura))
                {
                    // Limpiar el DataGrid antes de cargar nuevos datos
                    dataGridViewModificarPedidos.Rows.Clear();
                    // Obtener el pedido por número de factura
                    PEDIDO pedido = pedidoLogica.ObtenerPedidoPorNumeroFactura(numeroFactura);
                    if (pedido != null)
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
                    else
                    {
                        MessageBox.Show("No se encontró ningún pedido con ese número de factura.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un número de factura válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtMontoMaximo_KeyDown(object sender, KeyEventArgs e)
        {
            //filtrar pedidos por monto máximo al presionar Enter
            if (e.KeyCode == Keys.Enter)
            {
                dataGridViewModificarPedidos.Rows.Clear();
                string textoBusqueda = txtMontoMaximo.Text.Trim();
                if (decimal.TryParse(textoBusqueda, out decimal montoMaximo))
                {
                    // Limpiar el DataGrid antes de cargar nuevos datos
                    dataGridViewModificarPedidos.Rows.Clear();
                    // Obtener los pedidos por monto máximo
                    List<PEDIDO> pedidosPorMonto = pedidoLogica.ObtenerPedidosPorMontoMaximo(montoMaximo);
                    CargarPedidosEnDataGridView(pedidosPorMonto);
                }
                else
                {
                    MessageBox.Show("Ingrese un monto válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Obtener todos los pedidos con la fecha de entrega indicada en dateTimePicker1
            DateTime fechaSeleccionada = dateTimePicker1.Value.Date;
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaEntrega(fechaSeleccionada);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void btnCreacion_Click(object sender, EventArgs e)
        {
            //Obtener todos los pedidos con la fecha de creación indicada en dateTimePicker4
            DateTime fechaSeleccionada = dateTimePicker4.Value.Date;
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaCreacion(fechaSeleccionada);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Permitir solo números y no más de 8 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtDNI, "Ingrese solo números.");
            }
            else if (
                txtDNI.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtDNI, "No puede ingresar más de 8 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtDNI, "");
            }
        }

        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            //Por el DNI buscar empleado y validar con nombre_usuario que sea un usuario de id_rol = 5 al presionar Enter.Luego buscar los pedidos de ese empleado
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = txtDNI.Text.Trim();
                //convertir textoBusqueda a int
                int dniBusqueda = int.Parse(textoBusqueda);
                // Obtener el empleado por DNI
                Empleado empleado = empleadoLogica.ObtenerEmpleadoPorDNI(dniBusqueda);
                //Obtener Usuario por nombre_usuario del empleado
                if (empleado != null)
                {
                    var usuario = usuarioLogica.ObtenerUsuarioPorNombre(empleado.nombre_usuario);
                    if (usuario != null)
                    {
                        if (usuario.id_rol != 5)
                        {
                            MessageBox.Show("El empleado no es un preventista.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún usuario asociado al empleado.", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún empleado con ese DNI .", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Limpiar el DataGrid antes de cargar nuevos datos
                dataGridViewModificarPedidos.Rows.Clear();
                {
                    // Obtener los pedidos del empleado
                    List<PEDIDO> pedidos = pedidoLogica.ObtenerPedidosPorVendedor(empleado.nombre_usuario);
                    CargarPedidosEnDataGridView(pedidos);
                }

            }

        }

        private void dataGridViewModificarPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewModificarPedidos.Columns[e.ColumnIndex].Name == "btnFactura")
            {
                var numeroFactura = dataGridViewModificarPedidos.Rows[e.RowIndex].Cells["numero_factura"].Value?.ToString();

                e.Value = string.IsNullOrEmpty(numeroFactura) ? "GENERAR" : "VER";
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

        private void btnTodosLosPedidos_Click(object sender, EventArgs e)
        {
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
        }

        private void btnModificarPedido_Click(object sender, EventArgs e)
        {
            // Paso 1: Validaciones previas
            if (dataGridViewModificarPedidos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un pedido para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Si el pedido seleccionado está en estado 'Cancelado' no permitir modificar
            var filaPedidoSeleccionado = dataGridViewModificarPedidos.CurrentRow;
            int estadoPedidoSeleccionado = Convert.ToInt32(filaPedidoSeleccionado.Cells["id_estado"].Value);
            if (estadoPedidoSeleccionado == 4)
            {
                MessageBox.Show("No se pueden modificar pedidos en estado 'Cancelado'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar fecha de entrega
            DateTime fechaEntrega = dateTimePicker2.Value.Date;
            if (fechaEntrega <= DateTime.Today)
            {
                MessageBox.Show("La fecha de entrega debe ser posterior a la actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que haya al menos un detalle
            if (dataGridViewDetallePedido.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto al pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar cantidades en cada detalle
            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;
                int cantidadUnidad = 0, cantidadBultos = 0;
                int.TryParse(fila.Cells["cantidad_unidad"].Value?.ToString(), out cantidadUnidad);
                int.TryParse(fila.Cells["cantidad_bultos"].Value?.ToString(), out cantidadBultos);
                if (cantidadUnidad <= 0 && cantidadBultos <= 0)
                {
                    MessageBox.Show("Cada producto debe tener al menos una cantidad en unidades o bultos mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Validar estado seleccionado
            if (comboBoxEstados.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado para el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener datos del pedido seleccionado
            var filaPedido = dataGridViewModificarPedidos.CurrentRow;
            int idPedido = Convert.ToInt32(filaPedido.Cells["id_pedido"].Value);
            DateTime fechaCreacion = DateTime.ParseExact(filaPedido.Cells["fecha_creacion"].Value.ToString(), "dd/MM/yyyy", null);
            int idCliente = Convert.ToInt32(filaPedido.Cells["id_cliente"].Value);
            //Obtener CLIENTE por id_cliente 
            var cliente = clienteLogica.ObtenerClientePorId(idCliente);


            int idEstadoAnterior = Convert.ToInt32(filaPedido.Cells["id_estado"].Value);
            int idEstadoNuevo = Convert.ToInt32(comboBoxEstados.SelectedValue);
            string vendedor = filaPedido.Cells["vendedor"].Value?.ToString();
            int? numeroFacturaAnterior = null;
            if (filaPedido.Cells["numero_factura"].Value != null && int.TryParse(filaPedido.Cells["numero_factura"].Value.ToString(), out int nf))
                numeroFacturaAnterior = nf;

            // Validar transición de estados
            if (idEstadoAnterior == 3 && (idEstadoNuevo != 4 && idEstadoNuevo != 3))
            {
                MessageBox.Show("Un pedido en estado 'Entregado' solo puede pasar a 'Cancelado'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Paso 2: Verificar stock de cada producto
            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;
                int idProducto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                int idPresentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                int cantidadUnidad = 0, cantidadBultos = 0;
                int.TryParse(fila.Cells["cantidad_unidad"].Value?.ToString(), out cantidadUnidad);
                int.TryParse(fila.Cells["cantidad_bultos"].Value?.ToString(), out cantidadBultos);

                var productoPresentacion = productoLogica.ObtenerProductoPresentacionPorProductoYPresentacion(idProducto, idPresentacion);
                int unidadesPorBulto = productoPresentacion.unidades_bulto;
                int cantidadTotal = cantidadUnidad + (cantidadBultos * unidadesPorBulto);

                var stock = productoLogica.ObtenerStockPorProductoYPresentacion(idProducto, idPresentacion);
                if (stock == null || stock.stock_actual < cantidadTotal)
                {
                    MessageBox.Show($"Stock insuficiente para el producto '{fila.Cells["nombre"].Value}' ({cantidadTotal} solicitado, {stock?.stock_actual ?? 0} disponible).", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Paso 3: Eliminar todos los detalles anteriores y asignar los nuevos desde el DataGrid
            // Eliminar todos los detalles anteriores asociados al pedido
            pedidoLogica.EliminarDetallesPedido(idPedido);

            // Preparar lista de nuevos detalles a guardar
            var detallesNuevos = new List<DETALLE_PEDIDO>();
            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;

                int idProducto = Convert.ToInt32(fila.Cells["id_producto"].Value);
                int idPresentacion = Convert.ToInt32(fila.Cells["ID_presentacion"].Value);
                int ID_detalle_pedido = Convert.ToInt32(fila.Cells["ID_detalle_pedido"].Value);
                int cantidadUnidad = 0, cantidadBultos = 0;
                int.TryParse(fila.Cells["cantidad_unidad"].Value?.ToString(), out cantidadUnidad);
                int.TryParse(fila.Cells["cantidad_bultos"].Value?.ToString(), out cantidadBultos);
                decimal precioUnitario = Convert.ToDecimal(fila.Cells["precio_unitario"].Value);
                decimal descuento = 0;
                decimal.TryParse(fila.Cells["descuento"].Value?.ToString(), out descuento);

                detallesNuevos.Add(new DETALLE_PEDIDO
                {
                    id_pedido = idPedido,
                    id_producto = idProducto,
                    ID_presentacion = idPresentacion,
                    ID_detalle_pedido = ID_detalle_pedido,
                    cantidad = cantidadUnidad,
                    cantidad_bultos = cantidadBultos,
                    precio_unitario = precioUnitario,
                    descuento = descuento
                });
            }

            // Paso 4: Calcular nuevo total
            decimal totalPedido = 0;
            foreach (DataGridViewRow fila in dataGridViewDetallePedido.Rows)
            {
                if (fila.IsNewRow) continue;
                decimal subtotal = 0, descuento = 0;
                decimal.TryParse(fila.Cells["subtotal"].Value?.ToString(), out subtotal);
                decimal.TryParse(fila.Cells["descuento"].Value?.ToString(), out descuento);
                totalPedido += subtotal - (subtotal * descuento / 100);
            }
            // Paso extra: actualizar cuenta corriente si corresponde
            if (cliente.confiable)
            {
                decimal totalAnterior = Convert.ToDecimal(filaPedido.Cells["total"].Value);

                if (idEstadoAnterior != 3 && idEstadoNuevo == 3)
                {
                    // Primera vez que se entrega → sumar total completo
                    bool actualizado = clienteLogica.SumarSaldoPorPedidoEntregado(idCliente, totalPedido);
                    if (!actualizado)
                    {
                        MessageBox.Show("No se pudo actualizar el saldo de la cuenta corriente del cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (idEstadoAnterior == 3 && idEstadoNuevo == 3)
                {
                    // Ya estaba entregado → actualizar solo si el total cambió
                    decimal diferencia = totalPedido - totalAnterior;
                    if (diferencia != 0)
                    {
                        bool actualizado = clienteLogica.AjustarSaldo(idCliente, diferencia);
                        if (!actualizado)
                        {
                            MessageBox.Show("No se pudo ajustar el saldo de la cuenta corriente del cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }

            // Paso 5: Actualizar pedido
            int? nuevoNumeroFactura = numeroFacturaAnterior;
            bool generarFacturaAnulada = false;
            bool generarNuevaFactura = false;

            // Si pasa a Cancelado y tenía factura, generar comprobante ANULADO
            if (idEstadoNuevo == 4 && numeroFacturaAnterior.HasValue)
            {
                generarFacturaAnulada = true;
            }
            // Si pasa de Cancelado a Entregado, generar nueva factura
            if (idEstadoAnterior == 4 && idEstadoNuevo == 3)
            {
                nuevoNumeroFactura = pedidoLogica.GenerarNumeroFactura(idPedido);
                generarNuevaFactura = true;
            }

            // Actualizar el pedido
            var pedidoModificado = pedidoLogica.ModificarPedido(
                idPedido,
                fechaCreacion,
                fechaEntrega,
                idCliente,
                idEstadoNuevo,
                totalPedido,
                nuevoNumeroFactura ?? 0,
                vendedor
            );

            // Paso 6: Guardar los nuevos detalles
            if (pedidoLogica.GuardarDetalles(detallesNuevos))
            {
                // Detalles guardados correctamente
                MessageBox.Show("Detalles del pedido guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al guardar los detalles del pedido: " + string.Join("\n", pedidoLogica.ErroresValidacion), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ;

            
                // Paso 7: Generar comprobante si corresponde
                if (generarFacturaAnulada && pedidoModificado != null && pedidoModificado.numero_factura.HasValue)
                {
                    var detalles = pedidoLogica.ObtenerDetallesPedido(idPedido);
                    GenerarComprobanteFactura(pedidoModificado, detalles);
                }
                if (generarNuevaFactura && pedidoModificado != null && pedidoModificado.numero_factura.HasValue)
                {
                    var detalles = pedidoLogica.ObtenerDetallesPedido(idPedido);
                    GenerarComprobanteFactura(pedidoModificado, detalles);
                }

                MessageBox.Show("Pedido modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Refrescar la grilla
                CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
                //Cargar todos los productos activos con stock en datagrid
                CargarTodosLosProductosActivosConStock();
            }
        
           
        private void dataGridViewProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //si se presiona el btnAgregarProducto 
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

        private void dataGridViewDetallePedido_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewDetallePedido.IsCurrentCellDirty)
            {
                dataGridViewDetallePedido.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
    }
}

