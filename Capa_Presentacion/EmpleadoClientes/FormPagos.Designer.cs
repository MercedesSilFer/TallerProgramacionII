namespace ArimaERP.EmpleadoClientes
{
    partial class FormPagos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPagos = new System.Windows.Forms.Label();
            this.btnPedidos = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDetalles = new System.Windows.Forms.Label();
            this.dataGridViewDetallePagos = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRegistrarPago = new System.Windows.Forms.Label();
            this.tableLayoutPanelRegistrarPago = new System.Windows.Forms.TableLayoutPanel();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblMonto = new System.Windows.Forms.Label();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.textBoxCliente = new System.Windows.Forms.TextBox();
            this.comboBoxMetodoPago = new System.Windows.Forms.ComboBox();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnVolver = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBuscarPedidosCliente = new System.Windows.Forms.TextBox();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.textBoxNumeroFactura = new System.Windows.Forms.TextBox();
            this.labelClientes = new System.Windows.Forms.Label();
            this.comboBoxClienteZona = new System.Windows.Forms.ComboBox();
            this.btnFechaEntrega = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblZona = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.MontoMaximo = new System.Windows.Forms.Label();
            this.txtMontoMaximo = new System.Windows.Forms.TextBox();
            this.labelPreventista = new System.Windows.Forms.Label();
            this.btnCreacion = new System.Windows.Forms.Button();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewPedidos = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetallePagos)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanelRegistrarPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.82879F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.17121F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 303F));
            this.tableLayoutPanel1.Controls.Add(this.lblPagos, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPedidos, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1366, 62);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lblPagos
            // 
            this.lblPagos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPagos.AutoSize = true;
            this.lblPagos.Location = new System.Drawing.Point(450, 15);
            this.lblPagos.Name = "lblPagos";
            this.lblPagos.Size = new System.Drawing.Size(309, 31);
            this.lblPagos.TabIndex = 0;
            this.lblPagos.Text = "Consulta y Registro de Pagos";
            this.lblPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPedidos
            // 
            this.btnPedidos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPedidos.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPedidos.Location = new System.Drawing.Point(1119, 9);
            this.btnPedidos.Name = "btnPedidos";
            this.btnPedidos.Size = new System.Drawing.Size(190, 43);
            this.btnPedidos.TabIndex = 1;
            this.btnPedidos.Text = "Todos los Pedidos";
            this.btnPedidos.UseVisualStyleBackColor = true;
            this.btnPedidos.Click += new System.EventHandler(this.btnPedidos_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel6.Controls.Add(this.lblDetalles, 1, 0);
            this.tableLayoutPanel6.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel6.Location = new System.Drawing.Point(22, 300);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1329, 51);
            this.tableLayoutPanel6.TabIndex = 7;
            // 
            // lblDetalles
            // 
            this.lblDetalles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDetalles.AutoSize = true;
            this.lblDetalles.Location = new System.Drawing.Point(571, 10);
            this.lblDetalles.Name = "lblDetalles";
            this.lblDetalles.Size = new System.Drawing.Size(185, 31);
            this.lblDetalles.TabIndex = 0;
            this.lblDetalles.Text = "Detalle de Pagos";
            this.lblDetalles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewDetallePagos
            // 
            this.dataGridViewDetallePagos.AllowUserToResizeColumns = false;
            this.dataGridViewDetallePagos.AllowUserToResizeRows = false;
            this.dataGridViewDetallePagos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDetallePagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDetallePagos.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDetallePagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDetallePagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDetallePagos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDetallePagos.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewDetallePagos.Location = new System.Drawing.Point(23, 360);
            this.dataGridViewDetallePagos.Name = "dataGridViewDetallePagos";
            this.dataGridViewDetallePagos.ReadOnly = true;
            this.dataGridViewDetallePagos.RowHeadersVisible = false;
            this.dataGridViewDetallePagos.RowHeadersWidth = 51;
            this.dataGridViewDetallePagos.RowTemplate.Height = 24;
            this.dataGridViewDetallePagos.Size = new System.Drawing.Size(1328, 131);
            this.dataGridViewDetallePagos.TabIndex = 8;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel7.Controls.Add(this.lblRegistrarPago, 1, 0);
            this.tableLayoutPanel7.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel7.Location = new System.Drawing.Point(23, 502);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1328, 51);
            this.tableLayoutPanel7.TabIndex = 9;
            // 
            // lblRegistrarPago
            // 
            this.lblRegistrarPago.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRegistrarPago.AutoSize = true;
            this.lblRegistrarPago.Location = new System.Drawing.Point(582, 10);
            this.lblRegistrarPago.Name = "lblRegistrarPago";
            this.lblRegistrarPago.Size = new System.Drawing.Size(162, 31);
            this.lblRegistrarPago.TabIndex = 0;
            this.lblRegistrarPago.Text = "Registrar Pago";
            this.lblRegistrarPago.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelRegistrarPago
            // 
            this.tableLayoutPanelRegistrarPago.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelRegistrarPago.ColumnCount = 6;
            this.tableLayoutPanelRegistrarPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanelRegistrarPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.06404F));
            this.tableLayoutPanelRegistrarPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.93596F));
            this.tableLayoutPanelRegistrarPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanelRegistrarPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 306F));
            this.tableLayoutPanelRegistrarPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.lblIdCliente, 1, 1);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.txtMonto, 4, 0);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.lblCliente, 1, 0);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.lblMonto, 3, 0);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.lblMetodoPago, 3, 1);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.btnGuardar, 5, 0);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.textBoxCliente, 2, 0);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.comboBoxMetodoPago, 4, 1);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.txtIdCliente, 2, 1);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.lblFecha, 0, 0);
            this.tableLayoutPanelRegistrarPago.Controls.Add(this.dateTimePicker2, 0, 1);
            this.tableLayoutPanelRegistrarPago.Location = new System.Drawing.Point(22, 565);
            this.tableLayoutPanelRegistrarPago.Name = "tableLayoutPanelRegistrarPago";
            this.tableLayoutPanelRegistrarPago.RowCount = 2;
            this.tableLayoutPanelRegistrarPago.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRegistrarPago.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRegistrarPago.Size = new System.Drawing.Size(1329, 100);
            this.tableLayoutPanelRegistrarPago.TabIndex = 10;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdCliente.Location = new System.Drawing.Point(216, 62);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(88, 25);
            this.lblIdCliente.TabIndex = 12;
            this.lblIdCliente.Text = "ID Cliente";
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonto.Location = new System.Drawing.Point(840, 9);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(243, 31);
            this.txtMonto.TabIndex = 8;
            this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_KeyPress);
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(239, 12);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(65, 25);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente";
            // 
            // lblMonto
            // 
            this.lblMonto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMonto.AutoSize = true;
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.Location = new System.Drawing.Point(768, 12);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(66, 25);
            this.lblMonto.TabIndex = 2;
            this.lblMonto.Text = "Monto";
            // 
            // lblMetodoPago
            // 
            this.lblMetodoPago.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoPago.Location = new System.Drawing.Point(713, 62);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(121, 25);
            this.lblMetodoPago.TabIndex = 3;
            this.lblMetodoPago.Text = "Método Pago";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(1153, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(166, 44);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnRegistrarNuevo_Click);
            // 
            // textBoxCliente
            // 
            this.textBoxCliente.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxCliente.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCliente.Location = new System.Drawing.Point(310, 9);
            this.textBoxCliente.Name = "textBoxCliente";
            this.textBoxCliente.Size = new System.Drawing.Size(346, 31);
            this.textBoxCliente.TabIndex = 6;
            // 
            // comboBoxMetodoPago
            // 
            this.comboBoxMetodoPago.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxMetodoPago.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMetodoPago.FormattingEnabled = true;
            this.comboBoxMetodoPago.Location = new System.Drawing.Point(840, 58);
            this.comboBoxMetodoPago.Name = "comboBoxMetodoPago";
            this.comboBoxMetodoPago.Size = new System.Drawing.Size(241, 33);
            this.comboBoxMetodoPago.TabIndex = 9;
            this.comboBoxMetodoPago.Text = "Seleccione";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtIdCliente.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCliente.Location = new System.Drawing.Point(310, 59);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(101, 31);
            this.txtIdCliente.TabIndex = 11;
            this.txtIdCliente.TabStop = false;
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(49, 12);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(57, 25);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimePicker2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(3, 60);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(150, 30);
            this.dateTimePicker2.TabIndex = 13;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(1036, 679);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(134, 43);
            this.btnVolver.TabIndex = 11;
            this.btnVolver.Text = "Salir";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(353, 679);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(134, 43);
            this.btnLimpiar.TabIndex = 12;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.86091F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.00586F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.42167F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.515373F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.20205F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.81113F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.txtBuscarPedidosCliente, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNumeroFactura, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxNumeroFactura, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelClientes, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxClienteZona, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFechaEntrega, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblZona, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 62);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1366, 39);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // txtBuscarPedidosCliente
            // 
            this.txtBuscarPedidosCliente.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarPedidosCliente.Location = new System.Drawing.Point(174, 3);
            this.txtBuscarPedidosCliente.Name = "txtBuscarPedidosCliente";
            this.txtBuscarPedidosCliente.Size = new System.Drawing.Size(197, 30);
            this.txtBuscarPedidosCliente.TabIndex = 7;
            this.txtBuscarPedidosCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarPedidosCliente_KeyDown);
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroFactura.Location = new System.Drawing.Point(1033, 0);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(157, 39);
            this.lblNumeroFactura.TabIndex = 40;
            this.lblNumeroFactura.Text = "Número de Factura";
            this.lblNumeroFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNumeroFactura
            // 
            this.textBoxNumeroFactura.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumeroFactura.Location = new System.Drawing.Point(1196, 4);
            this.textBoxNumeroFactura.Name = "textBoxNumeroFactura";
            this.textBoxNumeroFactura.Size = new System.Drawing.Size(163, 30);
            this.textBoxNumeroFactura.TabIndex = 3;
            this.textBoxNumeroFactura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNumeroFactura_KeyDown);
            this.textBoxNumeroFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumeroFactura_KeyPress);
            // 
            // labelClientes
            // 
            this.labelClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelClientes.AutoSize = true;
            this.labelClientes.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClientes.Location = new System.Drawing.Point(19, 0);
            this.labelClientes.Name = "labelClientes";
            this.labelClientes.Size = new System.Drawing.Size(149, 39);
            this.labelClientes.TabIndex = 41;
            this.labelClientes.Text = "Ingrese DNI/Email Cliente";
            this.labelClientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxClienteZona
            // 
            this.comboBoxClienteZona.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxClienteZona.FormattingEnabled = true;
            this.comboBoxClienteZona.Location = new System.Drawing.Point(827, 3);
            this.comboBoxClienteZona.Name = "comboBoxClienteZona";
            this.comboBoxClienteZona.Size = new System.Drawing.Size(186, 31);
            this.comboBoxClienteZona.TabIndex = 10;
            this.comboBoxClienteZona.Text = "Seleccione Zona";
            this.comboBoxClienteZona.SelectedIndexChanged += new System.EventHandler(this.comboBoxClienteZona_SelectedIndexChanged);
            // 
            // btnFechaEntrega
            // 
            this.btnFechaEntrega.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFechaEntrega.Location = new System.Drawing.Point(377, 3);
            this.btnFechaEntrega.Name = "btnFechaEntrega";
            this.btnFechaEntrega.Size = new System.Drawing.Size(158, 33);
            this.btnFechaEntrega.TabIndex = 42;
            this.btnFechaEntrega.Text = "Fecha Entrega";
            this.btnFechaEntrega.UseVisualStyleBackColor = true;
            this.btnFechaEntrega.Click += new System.EventHandler(this.btnFechaEntrega_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(541, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(191, 30);
            this.dateTimePicker1.TabIndex = 43;
            // 
            // lblZona
            // 
            this.lblZona.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblZona.AutoSize = true;
            this.lblZona.Location = new System.Drawing.Point(760, 11);
            this.lblZona.Name = "lblZona";
            this.lblZona.Size = new System.Drawing.Size(38, 16);
            this.lblZona.TabIndex = 45;
            this.lblZona.Text = "Zona";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel4.ColumnCount = 8;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.93411F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.93265F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.49488F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.58858F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.12884F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.73792F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.txtDNI, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.MontoMaximo, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtMontoMaximo, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelPreventista, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCreacion, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker4, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 101);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1366, 39);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(174, 3);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(197, 30);
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
            this.MontoMaximo.Location = new System.Drawing.Point(1063, 0);
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
            this.txtMontoMaximo.Location = new System.Drawing.Point(1196, 4);
            this.txtMontoMaximo.Name = "txtMontoMaximo";
            this.txtMontoMaximo.Size = new System.Drawing.Size(163, 30);
            this.txtMontoMaximo.TabIndex = 3;
            this.txtMontoMaximo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMontoMaximo_KeyDown);
            this.txtMontoMaximo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoMaximo_KeyPress);
            // 
            // labelPreventista
            // 
            this.labelPreventista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreventista.AutoSize = true;
            this.labelPreventista.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreventista.Location = new System.Drawing.Point(67, 0);
            this.labelPreventista.Name = "labelPreventista";
            this.labelPreventista.Size = new System.Drawing.Size(101, 39);
            this.labelPreventista.TabIndex = 41;
            this.labelPreventista.Text = "Ingrese DNI Preventista";
            this.labelPreventista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCreacion
            // 
            this.btnCreacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreacion.Location = new System.Drawing.Point(378, 3);
            this.btnCreacion.Name = "btnCreacion";
            this.btnCreacion.Size = new System.Drawing.Size(157, 33);
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
            this.dateTimePicker4.Location = new System.Drawing.Point(541, 3);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(192, 30);
            this.dateTimePicker4.TabIndex = 43;
            // 
            // dataGridViewPedidos
            // 
            this.dataGridViewPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPedidos.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPedidos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPedidos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewPedidos.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridViewPedidos.Location = new System.Drawing.Point(23, 146);
            this.dataGridViewPedidos.Name = "dataGridViewPedidos";
            this.dataGridViewPedidos.RowHeadersVisible = false;
            this.dataGridViewPedidos.RowHeadersWidth = 51;
            this.dataGridViewPedidos.RowTemplate.Height = 24;
            this.dataGridViewPedidos.Size = new System.Drawing.Size(1328, 141);
            this.dataGridViewPedidos.TabIndex = 15;
            this.dataGridViewPedidos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPedidos_CellContentClick);
            // 
            // FormPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 731);
            this.Controls.Add(this.dataGridViewPedidos);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.tableLayoutPanelRegistrarPago);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Controls.Add(this.dataGridViewDetallePagos);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormPagos";
            this.Text = "Pagos";
            this.Load += new System.EventHandler(this.FormPagos_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetallePagos)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanelRegistrarPago.ResumeLayout(false);
            this.tableLayoutPanelRegistrarPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPedidos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPagos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblDetalles;
        private System.Windows.Forms.DataGridView dataGridViewDetallePagos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label lblRegistrarPago;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRegistrarPago;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.TextBox textBoxCliente;
        private System.Windows.Forms.ComboBox comboBoxMetodoPago;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtBuscarPedidosCliente;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.TextBox textBoxNumeroFactura;
        private System.Windows.Forms.Label labelClientes;
        private System.Windows.Forms.ComboBox comboBoxClienteZona;
        private System.Windows.Forms.Button btnFechaEntrega;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label MontoMaximo;
        private System.Windows.Forms.TextBox txtMontoMaximo;
        private System.Windows.Forms.Label labelPreventista;
        private System.Windows.Forms.Button btnCreacion;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.DataGridView dataGridViewPedidos;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnPedidos;
    }
}