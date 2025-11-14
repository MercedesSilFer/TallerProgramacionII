using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Capa_Entidades;
using Capa_Entidades.DTOs;
using Capa_Logica;

namespace ArimaERP.EmpleadoProducto
{
    public partial class FormComprar : Form
    {
        private readonly ClassProductoLogica _productoLogica = new ClassProductoLogica();
        private readonly ClassFamiliaLogica _familiaLogica = new ClassFamiliaLogica();
        private readonly ClassMarcaLogica _marcaLogica = new ClassMarcaLogica();
        private readonly ClassCompraLogica _compraLogica = new ClassCompraLogica();
        private readonly CultureInfo _culturaMoneda = CultureInfo.GetCultureInfo("es-AR");
        private int? _marcaActualId;
        private const decimal MargenPrecioVenta = 1.2m;                   // 120%
        private const decimal FactorCostoDesdeVenta = 1m / MargenPrecioVenta; //

        public FormComprar()
        {
            InitializeComponent();

            Load += FormComprar_Load;
            btnFiltrar.Click += BtnFiltrar_Click;
            button4.Click += BtnLimpiarFiltros_Click;
            button5.Click += BtnBuscarPorNombre_Click;
            btnCarrito.Click += BtnCarrito_Click;
            btnConfirmar.Click += btnConfirmar_Click;
            btnGuardar.Click += btnGuardar_Click;
            dgvProductos.CellContentClick += DgvProductos_CellContentClick;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            cbxMarca.SelectedIndexChanged += CbxMarca_SelectedIndexChanged;
        }

        private void FormComprar_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private void InicializarControles()
        {
            dgvProductos.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;

            if (dataGridView1.Columns.Contains("Producto"))
            {
                dataGridView1.Columns["Producto"].ReadOnly = true;
            }

            if (dataGridView1.Columns.Contains("Total"))
            {
                dataGridView1.Columns["Total"].ReadOnly = true;
            }

            CargarFamilias();
            CargarMarcas();
            ActualizarEstadoFiltros(true);
            ActualizarLabelTotal(0m);
            CargarProductos();
        }

        private void CargarFamilias()
        {
            List<FAMILIA> familias = new List<FAMILIA>();

            familias = _familiaLogica.ObtenerTodasLasFamilias() ?? new List<FAMILIA>();

            if (!familias.Any() && _familiaLogica.ErroresValidacion.Any())
            {
                MostrarErrores("Error al cargar familias", _familiaLogica.ErroresValidacion);
            }

            var familiasConOpcion = new List<FAMILIA>
            {
                new FAMILIA { id_familia = 0, descripcion = "Todas" }
            };

            familiasConOpcion.AddRange(familias.OrderBy(f => f.descripcion));

            cbxFamilia.DataSource = familiasConOpcion;
            cbxFamilia.DisplayMember = nameof(FAMILIA.descripcion);
            cbxFamilia.ValueMember = nameof(FAMILIA.id_familia);
            cbxFamilia.SelectedIndex = 0;
        }

        private void CargarMarcas()
        {
            List<MARCA> marcas = new List<MARCA>();

            marcas = _marcaLogica.ObtenerTodasLasMarcas() ?? new List<MARCA>();

            if (!marcas.Any() && _marcaLogica.ErroresValidacion.Any())
            {
                MostrarErrores("Error al cargar marcas", _marcaLogica.ErroresValidacion);
            }

            var marcasConOpcion = new List<MARCA>
            {
                new MARCA { id_marca = 0, nombre = "Todas" }
            };

            marcasConOpcion.AddRange(marcas.OrderBy(m => m.nombre));

            cbxMarca.DataSource = marcasConOpcion;
            cbxMarca.DisplayMember = nameof(MARCA.nombre);
            cbxMarca.ValueMember = nameof(MARCA.id_marca);
            cbxMarca.SelectedIndex = 0;
            _marcaActualId = ObtenerIdSeleccionado(cbxMarca);
        }

        private void ActualizarEstadoFiltros(bool habilitar)
        {
            cbxFamilia.Enabled = habilitar;
            cbxMarca.Enabled = habilitar;
            btnFiltrar.Enabled = habilitar;
            button5.Enabled = habilitar;
            txtBuscar.Enabled = habilitar;
        }

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void BtnBuscarPorNombre_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cbxFamilia.Items.Count > 0)
            {
                cbxFamilia.SelectedIndex = 0;
            }

            if (cbxMarca.Items.Count > 0)
            {
                cbxMarca.SelectedIndex = 0;
            }

            CargarProductos();
        }

        private void BtnCarrito_Click(object sender, EventArgs e)
        {
            LimpiarCarrito();
        }

        private void BtnHistorialCompras_Click(object sender, EventArgs e)
        {
            var historial = _compraLogica.ObtenerHistorialCompras() ?? new List<CompraHistorialDto>();

            if (_compraLogica.ErroresValidacion.Any())
            {
                MostrarErrores("Error al cargar el historial de compras", _compraLogica.ErroresValidacion);
                return;
            }

            using (var historialForm = new FormHistorialCompras())
            {
                historialForm.CargarHistorial(historial);

                if (!historial.Any())
                {
                    MessageBox.Show(
                        "Aún no hay compras registradas para mostrar.",
                        "Historial vacío",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                historialForm.ShowDialog(this);
            }
        }

        private void LimpiarCarrito()
        {
            dataGridView1.Rows.Clear();
            ActualizarLabelTotal(0m);
        }

        private void CargarProductos()
        {
            string termino = txtBuscar.Text.Trim();
            int? idFamilia = ObtenerIdSeleccionado(cbxFamilia);
            int? idMarca = ObtenerIdSeleccionado(cbxMarca);

            var productos = _productoLogica.BuscarCatalogoProductos(termino, idFamilia, idMarca, null, true);

            if (_productoLogica.ErroresValidacion.Any())
            {
                MostrarErrores("Error al buscar productos", _productoLogica.ErroresValidacion);
                return;
            }

            ActualizarListadoProductos(productos);
        }

        private int? ObtenerIdSeleccionado(ComboBox comboBox)
        {
            if (comboBox.SelectedValue is int id && id != 0)
            {
                return id;
            }

            return null;
        }

        private void CbxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            var marcaSeleccionada = ObtenerIdSeleccionado(cbxMarca);
            bool cambioMarca = _marcaActualId != marcaSeleccionada;
            _marcaActualId = marcaSeleccionada;

            if (cambioMarca && dataGridView1.Rows.Count > 0)
            {
                LimpiarCarrito();
            }

            CargarProductos();
        }

        private void ActualizarListadoProductos(IEnumerable<ProductoCatalogoDto> productos)
        {
            dgvProductos.Rows.Clear();

            var listaProductos = productos?.ToList() ?? new List<ProductoCatalogoDto>();

            foreach (var producto in listaProductos)
            {
                int fila = dgvProductos.Rows.Add(null, producto.Nombre, FormatearMoneda(producto.PrecioLista), producto.StockActual, producto.Familia, producto.Marca);
                dgvProductos.Rows[fila].Tag = producto;
            }

            if (!listaProductos.Any())
            {
                MessageBox.Show(
                    "No se encontraron productos con los filtros aplicados.",
                    "Sin resultados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }



        private void DgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dgvProductos.Columns[e.ColumnIndex].Name != "Agregar")
            {
                return;
            }

            var producto = dgvProductos.Rows[e.RowIndex].Tag as ProductoCatalogoDto;

            if (producto == null)
            {
                return;
            }

            if (ExisteEnCarrito(producto))
            {
                MessageBox.Show(
                    "El producto ya fue agregado al carrito.",
                    "Producto duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int fila = dataGridView1.Rows.Add(producto.Nombre, 0, FormatearMoneda(0m));
            var filaCarrito = dataGridView1.Rows[fila];
            filaCarrito.Tag = producto;

            dataGridView1.CurrentCell = filaCarrito.Cells["Cantidad"];
            dataGridView1.BeginEdit(true);

            ActualizarTotalesCarrito();
        }

        private bool ExisteEnCarrito(ProductoCatalogoDto producto)
        {
            return dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Any(r => r.Tag is ProductoCatalogoDto dto &&
                          dto.IdProducto == producto.IdProducto &&
                          dto.IdPresentacion == producto.IdPresentacion);
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name != "Cantidad")
            {
                return;
            }

            var fila = dataGridView1.Rows[e.RowIndex];

            if (fila.Cells["Cantidad"].Value == null || string.IsNullOrWhiteSpace(fila.Cells["Cantidad"].Value.ToString()))
            {
                fila.Cells["Cantidad"].Value = 0;
            }

            ActualizarTotalFila(fila);
            ActualizarTotalesCarrito();
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name != "Cantidad")
            {
                return;
            }

            string valor = e.FormattedValue?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(valor))
            {
                return;
            }

            if (!int.TryParse(valor, out int cantidad) || cantidad < 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor o igual a cero.",
                    "Cantidad inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell?.OwningColumn?.Name == "Cantidad" && e.Control is TextBox textBox)
            {
                textBox.KeyPress -= CantidadTextBox_KeyPress;
                textBox.KeyPress += CantidadTextBox_KeyPress;
            }
            else if (e.Control is TextBox textBoxGenerico)
            {
                textBoxGenerico.KeyPress -= CantidadTextBox_KeyPress;
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ActualizarTotalFila(DataGridViewRow fila)
        {
            if (!(fila.Tag is ProductoCatalogoDto producto))
            {
                return;
            }

            int cantidad = 0;
            if (fila.Cells["Cantidad"].Value != null)
            {
                int.TryParse(fila.Cells["Cantidad"].Value.ToString(), out cantidad);
            }

            decimal precioCompra = CalcularPrecioCompraDesdeLista(producto.PrecioLista);
            decimal total = precioCompra * cantidad;
            fila.Cells["Total"].Value = FormatearMoneda(total);
        }

        private void ActualizarTotalesCarrito()
        {
            decimal total = dataGridView1.Rows
          .Cast<DataGridViewRow>()
          .Where(r => !r.IsNewRow && r.Tag is ProductoCatalogoDto)
          .Sum(r =>
          {
              var producto = (ProductoCatalogoDto)r.Tag;
              int cantidad = 0;
              if (r.Cells["Cantidad"].Value != null)
              {
                  int.TryParse(r.Cells["Cantidad"].Value.ToString(), out cantidad);
              }

              decimal precioCompra = CalcularPrecioCompraDesdeLista(producto.PrecioLista);
              return precioCompra * cantidad;
          });

            ActualizarLabelTotal(total);
        }

        private void ActualizarLabelTotal(decimal total)
        {
            lblTotal.Text = $"Total: {FormatearMoneda(total)}";
        }

        private string FormatearMoneda(decimal valor)
        {
            return valor.ToString("C2", _culturaMoneda);
        }

        private decimal CalcularPrecioCompraDesdeLista(decimal precioLista)
        {
            // 3 decimales: 0.833
            return Math.Round(precioLista * FactorCostoDesdeVenta, 3, MidpointRounding.AwayFromZero);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var items = ObtenerItemsDelCarrito();

            if (!items.Any())
            {
                MessageBox.Show(
                    "Debe agregar al menos un producto al carrito para confirmar la compra.",
                    "Carrito vacío",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            foreach (var item in items)
            {
                if (item.Cantidad <= 0)
                {
                    MessageBox.Show(
                        "La cantidad a comprar de un producto no puede ser cero.",
                        "Cantidad inválida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    SeleccionarFilaConProducto(item.Producto);
                    return;
                }
            }

            var marcasEnCarrito = items
                .Select(i => i.Producto.IdMarca)
                .Distinct()
                .ToList();

            if (marcasEnCarrito.Count > 1)
            {
                MessageBox.Show(
                    "Todos los productos del carrito deben pertenecer a la misma marca para confirmar la compra.",
                    "Marcas diferentes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!marcasEnCarrito.Any())
            {
                MessageBox.Show(
                    "No se pudo determinar la marca asociada a los productos seleccionados.",
                    "Marca no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int idMarcaCompra = marcasEnCarrito.First();

            var detalles = new List<detalle_compra>();
            decimal totalCompra = 0m;

            foreach (var item in items)
            {
                decimal precioLista = item.Producto.PrecioLista;
                decimal precioCompra = CalcularPrecioCompraDesdeLista(precioLista); // 0.833 × lista

                var detalle = new detalle_compra
                {
                    cantidad_bulto = item.Cantidad,
                    precio_unitario = precioCompra,                 // se guarda el 0.833 del precio lista
                    id_producto = item.Producto.IdProducto,
                    ID_presentacion = item.Producto.IdPresentacion
                };

                detalles.Add(detalle);
                totalCompra += precioCompra * item.Cantidad;        // el monto de la compra también al costo
            }

            int nro = _compraLogica.ObtenerSiguienteNumeroFactura();

            var nuevaCompra = new compra
            {
                fecha = DateTime.Now,
                monto = totalCompra,
                nro_factura = nro,
                id_proveedor = 5
            };

            bool resultadoCompra = _compraLogica.RegistrarCompra(nuevaCompra, detalles);

            if (!resultadoCompra)
            {
                MostrarErrores("Error al registrar la compra", _compraLogica.ErroresValidacion);
                return;
            }

            MessageBox.Show(
                "La compra se confirmó correctamente y el stock fue actualizado.",
                "Compra confirmada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarCarrito();
            CargarProductos();
        }
        private void SeleccionarFilaConProducto(ProductoCatalogoDto producto)
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.IsNewRow)
                {
                    continue;
                }

                if (fila.Tag is ProductoCatalogoDto dto &&
                    dto.IdProducto == producto.IdProducto &&
                    dto.IdPresentacion == producto.IdPresentacion)
                {
                    dataGridView1.CurrentCell = fila.Cells["Cantidad"];
                    dataGridView1.BeginEdit(true);
                    break;
                }
            }
        }

        private List<ItemCarrito> ObtenerItemsDelCarrito()
        {
            var items = new List<ItemCarrito>();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.IsNewRow || !(fila.Tag is ProductoCatalogoDto producto))
                {
                    continue;
                }

                int cantidad = 0;
                if (fila.Cells["Cantidad"].Value != null)
                {
                    int.TryParse(fila.Cells["Cantidad"].Value.ToString(), out cantidad);
                }

                items.Add(new ItemCarrito(producto, cantidad));
            }

            return items;
        }

        private void MostrarErrores(string titulo, IEnumerable<string> errores)
        {
            var mensajes = (errores ?? Enumerable.Empty<string>()).ToList();

            if (!mensajes.Any())
            {
                mensajes.Add("Se produjo un error inesperado.");
            }

            MessageBox.Show(
                string.Join(Environment.NewLine, mensajes),
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private class ItemCarrito
        {
            public ItemCarrito(ProductoCatalogoDto producto, int cantidad)
            {
                Producto = producto;
                Cantidad = cantidad;
            }

            public ProductoCatalogoDto Producto { get; }
            public int Cantidad { get; }
        }

        private void TLPFooter_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var historial = _compraLogica.ObtenerHistorialCompras() ?? new List<CompraHistorialDto>();

            if (_compraLogica.ErroresValidacion.Any())
            {
                MostrarErrores("Error al cargar el historial de compras", _compraLogica.ErroresValidacion);
                return;
            }

            using (var historialForm = new FormHistorialCompras())
            {
                historialForm.CargarHistorial(historial);

                if (!historial.Any())
                {
                    MessageBox.Show(
                        "Aún no hay compras registradas para mostrar.",
                        "Historial vacío",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                historialForm.ShowDialog(this);
            }
        }

        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}
