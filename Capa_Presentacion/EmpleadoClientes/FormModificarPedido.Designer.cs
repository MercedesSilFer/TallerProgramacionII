namespace ArimaERP.EmpleadoClientes
{
    partial class FormModificarPedido
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTodosLosPedidos = new System.Windows.Forms.Button();
            this.textBoxNumeroFactura = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.labelClientes = new System.Windows.Forms.Label();
            this.btnFechaEntrega = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.MontoMaximo = new System.Windows.Forms.Label();
            this.txtMontoMaximo = new System.Windows.Forms.TextBox();
            this.labelPreventista = new System.Windows.Forms.Label();
            this.comboBoxBuscarPorEstado = new System.Windows.Forms.ComboBox();
            this.btnCreacion = new System.Windows.Forms.Button();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewModificarPedidos = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnModificarPedido = new System.Windows.Forms.Button();
            this.comboBoxEstados = new System.Windows.Forms.ComboBox();
            this.lblEstadoPedido = new System.Windows.Forms.Label();
            this.btnCancelarModificacion = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.lblFechaEntrega = new System.Windows.Forms.Label();
            this.lblDetalles = new System.Windows.Forms.Label();
            this.dataGridViewDetallePedido = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.comboBoxFamilia = new System.Windows.Forms.ComboBox();
            this.comboBoxMarca = new System.Windows.Forms.ComboBox();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.btnVerTodos = new System.Windows.Forms.Button();
            this.dataGridViewProductos = new System.Windows.Forms.DataGridView();
            this.lblZona = new System.Windows.Forms.Label();
            this.comboBoxClienteZona = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModificarPedidos)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetallePedido)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.36964F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.6559F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTodosLosPedidos, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1285, 44);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(515, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = " Modificar Pedidos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTodosLosPedidos
            // 
            this.btnTodosLosPedidos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTodosLosPedidos.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTodosLosPedidos.Location = new System.Drawing.Point(1075, 6);
            this.btnTodosLosPedidos.Name = "btnTodosLosPedidos";
            this.btnTodosLosPedidos.Size = new System.Drawing.Size(191, 32);
            this.btnTodosLosPedidos.TabIndex = 17;
            this.btnTodosLosPedidos.Text = "Todos los Pedidos";
            this.btnTodosLosPedidos.UseVisualStyleBackColor = true;
            this.btnTodosLosPedidos.Click += new System.EventHandler(this.btnTodosLosPedidos_Click);
            // 
            // textBoxNumeroFactura
            // 
            this.textBoxNumeroFactura.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumeroFactura.Location = new System.Drawing.Point(1123, 4);
            this.textBoxNumeroFactura.Name = "textBoxNumeroFactura";
            this.textBoxNumeroFactura.Size = new System.Drawing.Size(159, 30);
            this.textBoxNumeroFactura.TabIndex = 3;
            this.textBoxNumeroFactura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNumeroFactura_KeyDown);
            this.textBoxNumeroFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumeroPedido_KeyPress);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.txtBuscarCliente, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNumeroFactura, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxNumeroFactura, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelClientes, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxClienteZona, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFechaEntrega, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblZona, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 44);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1285, 39);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarCliente.Location = new System.Drawing.Point(163, 3);
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(154, 30);
            this.txtBuscarCliente.TabIndex = 7;
            this.txtBuscarCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarCliente_KeyDown);
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroFactura.Location = new System.Drawing.Point(1020, 0);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(97, 39);
            this.lblNumeroFactura.TabIndex = 40;
            this.lblNumeroFactura.Text = "Número de Factura";
            this.lblNumeroFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelClientes
            // 
            this.labelClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelClientes.AutoSize = true;
            this.labelClientes.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClientes.Location = new System.Drawing.Point(8, 0);
            this.labelClientes.Name = "labelClientes";
            this.labelClientes.Size = new System.Drawing.Size(149, 39);
            this.labelClientes.TabIndex = 41;
            this.labelClientes.Text = "Ingrese DNI/Email Cliente";
            this.labelClientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFechaEntrega
            // 
            this.btnFechaEntrega.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFechaEntrega.Location = new System.Drawing.Point(323, 3);
            this.btnFechaEntrega.Name = "btnFechaEntrega";
            this.btnFechaEntrega.Size = new System.Drawing.Size(154, 33);
            this.btnFechaEntrega.TabIndex = 42;
            this.btnFechaEntrega.Text = "Fecha Entrega";
            this.btnFechaEntrega.UseVisualStyleBackColor = true;
            this.btnFechaEntrega.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(483, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(154, 30);
            this.dateTimePicker1.TabIndex = 43;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel4.ColumnCount = 8;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.lblEstado, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtDNI, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.MontoMaximo, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtMontoMaximo, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelPreventista, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxBuscarPorEstado, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCreacion, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker4, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 83);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1285, 39);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(695, 11);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(50, 16);
            this.lblEstado.TabIndex = 46;
            this.lblEstado.Text = "Estado";
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(163, 3);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(154, 30);
            this.txtDNI.TabIndex = 7;
            this.txtDNI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDNI_KeyDown);
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // MontoMaximo
            // 
            this.MontoMaximo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MontoMaximo.AutoSize = true;
            this.MontoMaximo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MontoMaximo.Location = new System.Drawing.Point(990, 0);
            this.MontoMaximo.Name = "MontoMaximo";
            this.MontoMaximo.Size = new System.Drawing.Size(127, 39);
            this.MontoMaximo.TabIndex = 40;
            this.MontoMaximo.Text = "Monto Máximo";
            this.MontoMaximo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMontoMaximo
            // 
            this.txtMontoMaximo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMontoMaximo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoMaximo.Location = new System.Drawing.Point(1123, 4);
            this.txtMontoMaximo.Name = "txtMontoMaximo";
            this.txtMontoMaximo.Size = new System.Drawing.Size(159, 30);
            this.txtMontoMaximo.TabIndex = 3;
            this.txtMontoMaximo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMontoMaximo_KeyDown);
            this.txtMontoMaximo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox6_KeyPress);
            // 
            // labelPreventista
            // 
            this.labelPreventista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreventista.AutoSize = true;
            this.labelPreventista.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreventista.Location = new System.Drawing.Point(56, 0);
            this.labelPreventista.Name = "labelPreventista";
            this.labelPreventista.Size = new System.Drawing.Size(101, 39);
            this.labelPreventista.TabIndex = 41;
            this.labelPreventista.Text = "Ingrese DNI Preventista";
            this.labelPreventista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxBuscarPorEstado
            // 
            this.comboBoxBuscarPorEstado.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBuscarPorEstado.FormattingEnabled = true;
            this.comboBoxBuscarPorEstado.Items.AddRange(new object[] {
            "Pendiente",
            "En Preparación",
            "Entregado",
            "Cancelado",
            "Retrasado"});
            this.comboBoxBuscarPorEstado.Location = new System.Drawing.Point(803, 3);
            this.comboBoxBuscarPorEstado.Name = "comboBoxBuscarPorEstado";
            this.comboBoxBuscarPorEstado.Size = new System.Drawing.Size(154, 31);
            this.comboBoxBuscarPorEstado.TabIndex = 10;
            this.comboBoxBuscarPorEstado.Text = "Seleccione Estado";
            this.comboBoxBuscarPorEstado.SelectedIndexChanged += new System.EventHandler(this.comboBoxBuscarPorEstado_SelectedIndexChanged);
            // 
            // btnCreacion
            // 
            this.btnCreacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreacion.Location = new System.Drawing.Point(323, 3);
            this.btnCreacion.Name = "btnCreacion";
            this.btnCreacion.Size = new System.Drawing.Size(154, 33);
            this.btnCreacion.TabIndex = 42;
            this.btnCreacion.Text = "Fecha Creación";
            this.btnCreacion.UseVisualStyleBackColor = true;
            this.btnCreacion.Click += new System.EventHandler(this.btnCreacion_Click);
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker4.Location = new System.Drawing.Point(483, 3);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(154, 30);
            this.dateTimePicker4.TabIndex = 43;
            this.dateTimePicker4.ValueChanged += new System.EventHandler(this.dateTimePicker4_ValueChanged);
            // 
            // dataGridViewModificarPedidos
            // 
            this.dataGridViewModificarPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewModificarPedidos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewModificarPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModificarPedidos.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridViewModificarPedidos.Location = new System.Drawing.Point(0, 128);
            this.dataGridViewModificarPedidos.Name = "dataGridViewModificarPedidos";
            this.dataGridViewModificarPedidos.RowHeadersVisible = false;
            this.dataGridViewModificarPedidos.RowHeadersWidth = 51;
            this.dataGridViewModificarPedidos.RowTemplate.Height = 24;
            this.dataGridViewModificarPedidos.Size = new System.Drawing.Size(1282, 141);
            this.dataGridViewModificarPedidos.TabIndex = 4;
            this.dataGridViewModificarPedidos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewModificarPedidos_CellContentClick);
            this.dataGridViewModificarPedidos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewModificarPedidos_CellFormatting);
            this.dataGridViewModificarPedidos.SelectionChanged += new System.EventHandler(this.dataGridViewModificarPedidos_SelectionChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5061F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5061F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.89788F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.20777F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.88215F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel6.Controls.Add(this.btnModificarPedido, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.comboBoxEstados, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblEstadoPedido, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnCancelarModificacion, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.dateTimePicker2, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblFechaEntrega, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblDetalles, 5, 0);
            this.tableLayoutPanel6.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 480);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1285, 45);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // btnModificarPedido
            // 
            this.btnModificarPedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModificarPedido.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarPedido.Location = new System.Drawing.Point(9, 3);
            this.btnModificarPedido.Name = "btnModificarPedido";
            this.btnModificarPedido.Size = new System.Drawing.Size(180, 39);
            this.btnModificarPedido.TabIndex = 14;
            this.btnModificarPedido.Text = "Actualizar Pedido";
            this.btnModificarPedido.UseVisualStyleBackColor = true;
            this.btnModificarPedido.Click += new System.EventHandler(this.btnModificarPedido_Click);
            // 
            // comboBoxEstados
            // 
            this.comboBoxEstados.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxEstados.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEstados.FormattingEnabled = true;
            this.comboBoxEstados.Items.AddRange(new object[] {
            "Pendiente",
            "En Preparación",
            "Entregado",
            "Cancelado",
            "Retrasado"});
            this.comboBoxEstados.Location = new System.Drawing.Point(661, 7);
            this.comboBoxEstados.Name = "comboBoxEstados";
            this.comboBoxEstados.Size = new System.Drawing.Size(169, 31);
            this.comboBoxEstados.TabIndex = 13;
            this.comboBoxEstados.Text = "Seleccione Estado";
            // 
            // lblEstadoPedido
            // 
            this.lblEstadoPedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstadoPedido.AutoSize = true;
            this.lblEstadoPedido.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoPedido.Location = new System.Drawing.Point(537, 0);
            this.lblEstadoPedido.Name = "lblEstadoPedido";
            this.lblEstadoPedido.Size = new System.Drawing.Size(118, 45);
            this.lblEstadoPedido.TabIndex = 12;
            this.lblEstadoPedido.Text = "Estado Pedido";
            this.lblEstadoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelarModificacion
            // 
            this.btnCancelarModificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelarModificacion.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarModificacion.Location = new System.Drawing.Point(1060, 3);
            this.btnCancelarModificacion.Name = "btnCancelarModificacion";
            this.btnCancelarModificacion.Size = new System.Drawing.Size(151, 39);
            this.btnCancelarModificacion.TabIndex = 8;
            this.btnCancelarModificacion.Text = "Cancelar";
            this.btnCancelarModificacion.UseVisualStyleBackColor = true;
            this.btnCancelarModificacion.Click += new System.EventHandler(this.btnCancelarModificacion_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(355, 3);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(154, 31);
            this.dateTimePicker2.TabIndex = 11;
            // 
            // lblFechaEntrega
            // 
            this.lblFechaEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaEntrega.AutoSize = true;
            this.lblFechaEntrega.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaEntrega.Location = new System.Drawing.Point(231, 0);
            this.lblFechaEntrega.Name = "lblFechaEntrega";
            this.lblFechaEntrega.Size = new System.Drawing.Size(118, 45);
            this.lblFechaEntrega.TabIndex = 2;
            this.lblFechaEntrega.Text = "Fecha Entrega";
            this.lblFechaEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetalles
            // 
            this.lblDetalles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDetalles.AutoSize = true;
            this.lblDetalles.Location = new System.Drawing.Point(847, 7);
            this.lblDetalles.Name = "lblDetalles";
            this.lblDetalles.Size = new System.Drawing.Size(195, 31);
            this.lblDetalles.TabIndex = 0;
            this.lblDetalles.Text = "Detalle de Pedido";
            this.lblDetalles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewDetallePedido
            // 
            this.dataGridViewDetallePedido.AllowUserToDeleteRows = false;
            this.dataGridViewDetallePedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDetallePedido.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewDetallePedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetallePedido.Location = new System.Drawing.Point(0, 533);
            this.dataGridViewDetallePedido.Name = "dataGridViewDetallePedido";
            this.dataGridViewDetallePedido.RowHeadersVisible = false;
            this.dataGridViewDetallePedido.RowHeadersWidth = 51;
            this.dataGridViewDetallePedido.RowTemplate.Height = 24;
            this.dataGridViewDetallePedido.Size = new System.Drawing.Size(1285, 79);
            this.dataGridViewDetallePedido.TabIndex = 7;
            this.dataGridViewDetallePedido.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDetallePedido_CellContentClick);
            this.dataGridViewDetallePedido.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDetallePedido_CellValueChanged);
            this.dataGridViewDetallePedido.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewDetallePedido_CurrentCellDirtyStateChanged);
            this.dataGridViewDetallePedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridViewDetallePedido_KeyPress);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.17656F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.50038F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.235913F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.81142F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.489858F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.55222F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.00826F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblMarca, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxFamilia, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxMarca, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxCodigo, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblFamilia, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnVerTodos, 6, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 275);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1285, 50);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Ingrese Código Producto";
            // 
            // lblMarca
            // 
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(761, 17);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(45, 16);
            this.lblMarca.TabIndex = 13;
            this.lblMarca.Text = "Marca";
            // 
            // comboBoxFamilia
            // 
            this.comboBoxFamilia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxFamilia.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFamilia.FormattingEnabled = true;
            this.comboBoxFamilia.Location = new System.Drawing.Point(464, 9);
            this.comboBoxFamilia.Name = "comboBoxFamilia";
            this.comboBoxFamilia.Size = new System.Drawing.Size(262, 31);
            this.comboBoxFamilia.TabIndex = 8;
            this.comboBoxFamilia.Text = "Familia";
            this.comboBoxFamilia.SelectedIndexChanged += new System.EventHandler(this.comboBoxFamilia_SelectedIndexChanged);
            // 
            // comboBoxMarca
            // 
            this.comboBoxMarca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxMarca.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMarca.FormattingEnabled = true;
            this.comboBoxMarca.Location = new System.Drawing.Point(841, 9);
            this.comboBoxMarca.Name = "comboBoxMarca";
            this.comboBoxMarca.Size = new System.Drawing.Size(194, 31);
            this.comboBoxMarca.TabIndex = 10;
            this.comboBoxMarca.Text = "Marca";
            this.comboBoxMarca.SelectedIndexChanged += new System.EventHandler(this.comboBoxMarca_SelectedIndexChanged);
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCodigo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCodigo.Location = new System.Drawing.Point(210, 10);
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.Size = new System.Drawing.Size(156, 30);
            this.textBoxCodigo.TabIndex = 7;
            this.textBoxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCodigo_KeyDown);
            this.textBoxCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCodigo_KeyPress);
            // 
            // lblFamilia
            // 
            this.lblFamilia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(395, 17);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(51, 16);
            this.lblFamilia.TabIndex = 12;
            this.lblFamilia.Text = "Familia";
            // 
            // btnVerTodos
            // 
            this.btnVerTodos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVerTodos.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerTodos.Location = new System.Drawing.Point(1066, 9);
            this.btnVerTodos.Name = "btnVerTodos";
            this.btnVerTodos.Size = new System.Drawing.Size(191, 32);
            this.btnVerTodos.TabIndex = 16;
            this.btnVerTodos.Text = "Todos los Productos";
            this.btnVerTodos.UseVisualStyleBackColor = true;
            this.btnVerTodos.Click += new System.EventHandler(this.btnVerTodos_Click);
            // 
            // dataGridViewProductos
            // 
            this.dataGridViewProductos.AllowUserToAddRows = false;
            this.dataGridViewProductos.AllowUserToDeleteRows = false;
            this.dataGridViewProductos.AllowUserToResizeRows = false;
            this.dataGridViewProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProductos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductos.Location = new System.Drawing.Point(0, 332);
            this.dataGridViewProductos.Name = "dataGridViewProductos";
            this.dataGridViewProductos.ReadOnly = true;
            this.dataGridViewProductos.RowHeadersVisible = false;
            this.dataGridViewProductos.RowHeadersWidth = 51;
            this.dataGridViewProductos.RowTemplate.Height = 24;
            this.dataGridViewProductos.Size = new System.Drawing.Size(1285, 142);
            this.dataGridViewProductos.TabIndex = 10;
            this.dataGridViewProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductos_CellContentClick);
            // 
            // lblZona
            // 
            this.lblZona.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblZona.AutoSize = true;
            this.lblZona.Location = new System.Drawing.Point(701, 11);
            this.lblZona.Name = "lblZona";
            this.lblZona.Size = new System.Drawing.Size(38, 16);
            this.lblZona.TabIndex = 45;
            this.lblZona.Text = "Zona";
            // 
            // comboBoxClienteZona
            // 
            this.comboBoxClienteZona.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxClienteZona.FormattingEnabled = true;
            this.comboBoxClienteZona.Location = new System.Drawing.Point(803, 3);
            this.comboBoxClienteZona.Name = "comboBoxClienteZona";
            this.comboBoxClienteZona.Size = new System.Drawing.Size(154, 31);
            this.comboBoxClienteZona.TabIndex = 10;
            this.comboBoxClienteZona.Text = "Seleccione Zona";
            this.comboBoxClienteZona.SelectedIndexChanged += new System.EventHandler(this.comboBoxClienteZona_SelectedIndexChanged);
            // 
            // FormModificarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 613);
            this.Controls.Add(this.dataGridViewProductos);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.dataGridViewDetallePedido);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.dataGridViewModificarPedidos);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormModificarPedido";
            this.Text = "Modificar  Pedidos";
            this.Load += new System.EventHandler(this.FormModificarPedido_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModificarPedidos)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetallePedido)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.TextBox textBoxNumeroFactura;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.Label labelClientes;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnFechaEntrega;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label MontoMaximo;
        private System.Windows.Forms.TextBox txtMontoMaximo;
        private System.Windows.Forms.Label labelPreventista;
        private System.Windows.Forms.ComboBox comboBoxBuscarPorEstado;
        private System.Windows.Forms.Button btnCreacion;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.DataGridView dataGridViewModificarPedidos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblDetalles;
        private System.Windows.Forms.DataGridView dataGridViewDetallePedido;
        private System.Windows.Forms.Button btnCancelarModificacion;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.ComboBox comboBoxFamilia;
        private System.Windows.Forms.ComboBox comboBoxMarca;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Label lblFamilia;
        private System.Windows.Forms.Button btnVerTodos;
        private System.Windows.Forms.Label lblFechaEntrega;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBoxEstados;
        private System.Windows.Forms.Label lblEstadoPedido;
        private System.Windows.Forms.DataGridView dataGridViewProductos;
        private System.Windows.Forms.Button btnModificarPedido;
        private System.Windows.Forms.Button btnTodosLosPedidos;
        private System.Windows.Forms.ComboBox comboBoxClienteZona;
        private System.Windows.Forms.Label lblZona;
    }
}