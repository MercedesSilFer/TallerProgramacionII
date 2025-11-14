namespace ArimaERP.EmpleadoProducto
{
    partial class FormComprar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TLPBase = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.cbxMarca = new System.Windows.Forms.ComboBox();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.cbxProveedor = new System.Windows.Forms.ComboBox();
            this.cbxFamilia = new System.Windows.Forms.ComboBox();
            this.TLPCentral = new System.Windows.Forms.TableLayoutPanel();
            this.gbxProductos = new System.Windows.Forms.GroupBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.Agregar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbxCarrito = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCarrito = new System.Windows.Forms.Button();
            this.TLPFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TLPBase.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TLPCentral.SuspendLayout();
            this.gbxProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.gbxCarrito.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.TLPFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLPBase
            // 
            this.TLPBase.ColumnCount = 1;
            this.TLPBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPBase.Controls.Add(this.panel2, 0, 0);
            this.TLPBase.Controls.Add(this.groupBox1, 0, 1);
            this.TLPBase.Controls.Add(this.TLPCentral, 0, 2);
            this.TLPBase.Controls.Add(this.TLPFooter, 0, 3);
            this.TLPBase.Controls.Add(this.panel1, 0, 4);
            this.TLPBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPBase.Location = new System.Drawing.Point(0, 0);
            this.TLPBase.Margin = new System.Windows.Forms.Padding(0);
            this.TLPBase.Name = "TLPBase";
            this.TLPBase.RowCount = 5;
            this.TLPBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.TLPBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.TLPBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.TLPBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TLPBase.Size = new System.Drawing.Size(1264, 569);
            this.TLPBase.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.Controls.Add(this.labelTitulo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1264, 60);
            this.panel2.TabIndex = 4;
            // 
            // labelTitulo
            // 
            this.labelTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(513, 9);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(368, 54);
            this.labelTitulo.TabIndex = 8;
            this.labelTitulo.Text = "Comprar productos";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1258, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de busqueda";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Controls.Add(this.lblBuscar, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBuscar, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button5, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFiltrar, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.button4, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblProveedor, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMarca, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxMarca, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFamilia, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxProveedor, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxFamilia, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1252, 73);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(179, 36);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(109, 37);
            this.lblBuscar.TabIndex = 16;
            this.lblBuscar.Text = "Buscar por nombre";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(320, 39);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(139, 34);
            this.txtBuscar.TabIndex = 17;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(478, 39);
            this.button5.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(37, 31);
            this.button5.TabIndex = 22;
            this.button5.Text = "🔍";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFiltrar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(629, 39);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(145, 30);
            this.btnFiltrar.TabIndex = 10;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(787, 39);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 31);
            this.button4.TabIndex = 21;
            this.button4.Text = "Limpiar";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // lblProveedor
            // 
            this.lblProveedor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(164, 0);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(139, 36);
            this.lblProveedor.TabIndex = 5;
            this.lblProveedor.Text = "Proveedor";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProveedor.Visible = false;
            // 
            // lblMarca
            // 
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(345, 0);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(90, 36);
            this.lblMarca.TabIndex = 3;
            this.lblMarca.Text = "Marca";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxMarca
            // 
            this.cbxMarca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMarca.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMarca.FormattingEnabled = true;
            this.cbxMarca.Location = new System.Drawing.Point(485, 3);
            this.cbxMarca.Name = "cbxMarca";
            this.cbxMarca.Size = new System.Drawing.Size(121, 36);
            this.cbxMarca.TabIndex = 8;
            // 
            // lblFamilia
            // 
            this.lblFamilia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(651, 0);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(101, 36);
            this.lblFamilia.TabIndex = 0;
            this.lblFamilia.Text = "Familia";
            this.lblFamilia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxProveedor
            // 
            this.cbxProveedor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProveedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProveedor.FormattingEnabled = true;
            this.cbxProveedor.Location = new System.Drawing.Point(953, 3);
            this.cbxProveedor.Name = "cbxProveedor";
            this.cbxProveedor.Size = new System.Drawing.Size(121, 36);
            this.cbxProveedor.TabIndex = 9;
            this.cbxProveedor.Visible = false;
            this.cbxProveedor.SelectedIndexChanged += new System.EventHandler(this.cbxProveedor_SelectedIndexChanged);
            // 
            // cbxFamilia
            // 
            this.cbxFamilia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFamilia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFamilia.FormattingEnabled = true;
            this.cbxFamilia.Location = new System.Drawing.Point(797, 3);
            this.cbxFamilia.Name = "cbxFamilia";
            this.cbxFamilia.Size = new System.Drawing.Size(121, 36);
            this.cbxFamilia.TabIndex = 6;
            // 
            // TLPCentral
            // 
            this.TLPCentral.ColumnCount = 2;
            this.TLPCentral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.74235F));
            this.TLPCentral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.25765F));
            this.TLPCentral.Controls.Add(this.gbxProductos, 0, 0);
            this.TLPCentral.Controls.Add(this.gbxCarrito, 1, 0);
            this.TLPCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPCentral.Location = new System.Drawing.Point(3, 183);
            this.TLPCentral.Name = "TLPCentral";
            this.TLPCentral.RowCount = 1;
            this.TLPCentral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPCentral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLPCentral.Size = new System.Drawing.Size(1258, 265);
            this.TLPCentral.TabIndex = 1;
            // 
            // gbxProductos
            // 
            this.gbxProductos.Controls.Add(this.dgvProductos);
            this.gbxProductos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxProductos.Location = new System.Drawing.Point(0, 0);
            this.gbxProductos.Margin = new System.Windows.Forms.Padding(0);
            this.gbxProductos.Name = "gbxProductos";
            this.gbxProductos.Size = new System.Drawing.Size(751, 265);
            this.gbxProductos.TabIndex = 0;
            this.gbxProductos.TabStop = false;
            this.gbxProductos.Text = "Productos disponibles";
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Agregar,
            this.Nombre,
            this.Precio,
            this.Stock,
            this.Familia,
            this.Marca});
            this.dgvProductos.Location = new System.Drawing.Point(6, 28);
            this.dgvProductos.Margin = new System.Windows.Forms.Padding(0);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.Size = new System.Drawing.Size(739, 234);
            this.dgvProductos.TabIndex = 0;
            // 
            // Agregar
            // 
            this.Agregar.HeaderText = "Agregar";
            this.Agregar.MinimumWidth = 6;
            this.Agregar.Name = "Agregar";
            this.Agregar.ReadOnly = true;
            this.Agregar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Agregar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Agregar.Text = "Agregar";
            this.Agregar.UseColumnTextForButtonValue = true;
            this.Agregar.Width = 125;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 125;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.MinimumWidth = 6;
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 125;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock";
            this.Stock.MinimumWidth = 6;
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 125;
            // 
            // Familia
            // 
            this.Familia.HeaderText = "Familia";
            this.Familia.MinimumWidth = 6;
            this.Familia.Name = "Familia";
            this.Familia.ReadOnly = true;
            this.Familia.Width = 125;
            // 
            // Marca
            // 
            this.Marca.HeaderText = "Marca";
            this.Marca.MinimumWidth = 6;
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            this.Marca.Width = 125;
            // 
            // gbxCarrito
            // 
            this.gbxCarrito.Controls.Add(this.tableLayoutPanel2);
            this.gbxCarrito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxCarrito.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxCarrito.Location = new System.Drawing.Point(754, 3);
            this.gbxCarrito.Name = "gbxCarrito";
            this.gbxCarrito.Size = new System.Drawing.Size(501, 259);
            this.gbxCarrito.TabIndex = 1;
            this.gbxCarrito.TabStop = false;
            this.gbxCarrito.Text = "Carrito de compras";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCarrito, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.54978F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.45022F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(495, 226);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Producto,
            this.Cantidad,
            this.Total});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(487, 182);
            this.dataGridView1.TabIndex = 0;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.MinimumWidth = 6;
            this.Producto.Name = "Producto";
            this.Producto.Width = 200;
            // 
            // Cantidad
            // 
            this.Cantidad.FillWeight = 150F;
            this.Cantidad.HeaderText = "Cantidad a comprar";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Width = 130;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.MinimumWidth = 6;
            this.Total.Name = "Total";
            this.Total.Width = 110;
            // 
            // btnCarrito
            // 
            this.btnCarrito.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCarrito.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnCarrito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarrito.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarrito.Location = new System.Drawing.Point(158, 191);
            this.btnCarrito.Name = "btnCarrito";
            this.btnCarrito.Size = new System.Drawing.Size(179, 32);
            this.btnCarrito.TabIndex = 22;
            this.btnCarrito.Text = "Limpiar carrito.";
            this.btnCarrito.UseVisualStyleBackColor = false;
            // 
            // TLPFooter
            // 
            this.TLPFooter.ColumnCount = 5;
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.005F));
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66333F));
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66333F));
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66333F));
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.005F));
            this.TLPFooter.Controls.Add(this.lblTotal, 2, 0);
            this.TLPFooter.Controls.Add(this.btnConfirmar, 1, 1);
            this.TLPFooter.Controls.Add(this.btnGuardar, 3, 1);
            this.TLPFooter.Controls.Add(this.btnCancelar, 4, 1);
            this.TLPFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPFooter.Location = new System.Drawing.Point(3, 454);
            this.TLPFooter.Name = "TLPFooter";
            this.TLPFooter.RowCount = 2;
            this.TLPFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPFooter.Size = new System.Drawing.Size(1258, 72);
            this.TLPFooter.TabIndex = 2;
            this.TLPFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.TLPFooter_Paint);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(560, 4);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(134, 28);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "Total: $*******.";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirmar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(323, 39);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(191, 30);
            this.btnConfirmar.TabIndex = 11;
            this.btnConfirmar.Text = "Confirmar Compra";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(758, 39);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(157, 30);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Historial";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(1018, 39);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(163, 30);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 529);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 40);
            this.panel1.TabIndex = 3;
            // 
            // FormComprar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1264, 569);
            this.Controls.Add(this.TLPBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormComprar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormComprar";
            this.TLPBase.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TLPCentral.ResumeLayout(false);
            this.gbxProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.gbxCarrito.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.TLPFooter.ResumeLayout(false);
            this.TLPFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPBase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblFamilia;
        private System.Windows.Forms.ComboBox cbxFamilia;
        private System.Windows.Forms.ComboBox cbxProveedor;
        private System.Windows.Forms.ComboBox cbxMarca;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TableLayoutPanel TLPCentral;
        private System.Windows.Forms.GroupBox gbxProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox gbxCarrito;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCarrito;
        private System.Windows.Forms.TableLayoutPanel TLPFooter;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn Agregar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Familia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marca;
    }
}