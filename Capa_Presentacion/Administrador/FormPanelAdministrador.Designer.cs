namespace ArimaERP.Administrador
{
    partial class FormPanelAdministrador
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
            this.TLPMAIN = new System.Windows.Forms.TableLayoutPanel();
            this.gbxAdmin = new System.Windows.Forms.GroupBox();
            this.btnAuditoria = new System.Windows.Forms.Button();
            this.Accion3 = new System.Windows.Forms.Button();
            this.Accion2 = new System.Windows.Forms.Button();
            this.Accion1 = new System.Windows.Forms.Button();
            this.gbxMDIProductos = new System.Windows.Forms.GroupBox();
            this.btnProductos = new System.Windows.Forms.Button();
            this.gbxMDIiClientes = new System.Windows.Forms.GroupBox();
            this.btnClientes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TLPHeader = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFECHA = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TLPFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lblDerechos = new System.Windows.Forms.Label();
            this.lblSoporte = new System.Windows.Forms.Label();
            this.TLPMAIN.SuspendLayout();
            this.gbxAdmin.SuspendLayout();
            this.gbxMDIProductos.SuspendLayout();
            this.gbxMDIiClientes.SuspendLayout();
            this.TLPHeader.SuspendLayout();
            this.TLPFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLPMAIN
            // 
            this.TLPMAIN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TLPMAIN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.TLPMAIN.ColumnCount = 2;
            this.TLPMAIN.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPMAIN.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPMAIN.Controls.Add(this.gbxMDIProductos, 1, 0);
            this.TLPMAIN.Controls.Add(this.gbxMDIiClientes, 0, 0);
            this.TLPMAIN.Location = new System.Drawing.Point(13, 171);
            this.TLPMAIN.Margin = new System.Windows.Forms.Padding(0);
            this.TLPMAIN.Name = "TLPMAIN";
            this.TLPMAIN.RowCount = 1;
            this.TLPMAIN.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPMAIN.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLPMAIN.Size = new System.Drawing.Size(1333, 312);
            this.TLPMAIN.TabIndex = 0;
            // 
            // gbxAdmin
            // 
            this.gbxAdmin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxAdmin.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbxAdmin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbxAdmin.Controls.Add(this.btnAuditoria);
            this.gbxAdmin.Controls.Add(this.Accion3);
            this.gbxAdmin.Controls.Add(this.Accion2);
            this.gbxAdmin.Controls.Add(this.Accion1);
            this.gbxAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxAdmin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbxAdmin.Location = new System.Drawing.Point(434, 497);
            this.gbxAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.gbxAdmin.Name = "gbxAdmin";
            this.gbxAdmin.Padding = new System.Windows.Forms.Padding(4);
            this.gbxAdmin.Size = new System.Drawing.Size(476, 268);
            this.gbxAdmin.TabIndex = 4;
            this.gbxAdmin.TabStop = false;
            this.gbxAdmin.Text = "Funciones Administrador";
            this.gbxAdmin.Enter += new System.EventHandler(this.gbxAdmin_Enter);
            // 
            // btnAuditoria
            // 
            this.btnAuditoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.btnAuditoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuditoria.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAuditoria.Location = new System.Drawing.Point(49, 189);
            this.btnAuditoria.Margin = new System.Windows.Forms.Padding(4);
            this.btnAuditoria.Name = "btnAuditoria";
            this.btnAuditoria.Size = new System.Drawing.Size(385, 43);
            this.btnAuditoria.TabIndex = 4;
            this.btnAuditoria.Text = "Auditoria";
            this.btnAuditoria.UseVisualStyleBackColor = false;
            this.btnAuditoria.Click += new System.EventHandler(this.btnAuditoria_Click);
            // 
            // Accion3
            // 
            this.Accion3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.Accion3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Accion3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Accion3.Location = new System.Drawing.Point(49, 138);
            this.Accion3.Margin = new System.Windows.Forms.Padding(4);
            this.Accion3.Name = "Accion3";
            this.Accion3.Size = new System.Drawing.Size(385, 43);
            this.Accion3.TabIndex = 3;
            this.Accion3.Text = "Reportes";
            this.Accion3.UseVisualStyleBackColor = false;
            this.Accion3.Click += new System.EventHandler(this.Accion3_Click);
            // 
            // Accion2
            // 
            this.Accion2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.Accion2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Accion2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Accion2.Location = new System.Drawing.Point(49, 87);
            this.Accion2.Margin = new System.Windows.Forms.Padding(4);
            this.Accion2.Name = "Accion2";
            this.Accion2.Size = new System.Drawing.Size(385, 43);
            this.Accion2.TabIndex = 1;
            this.Accion2.Text = "Gestion de Roles";
            this.Accion2.UseVisualStyleBackColor = false;
            this.Accion2.Click += new System.EventHandler(this.Accion2_Click);
            // 
            // Accion1
            // 
            this.Accion1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.Accion1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Accion1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Accion1.Location = new System.Drawing.Point(49, 37);
            this.Accion1.Margin = new System.Windows.Forms.Padding(4);
            this.Accion1.Name = "Accion1";
            this.Accion1.Size = new System.Drawing.Size(385, 43);
            this.Accion1.TabIndex = 0;
            this.Accion1.Text = "ABM Usuarios";
            this.Accion1.UseVisualStyleBackColor = false;
            this.Accion1.Click += new System.EventHandler(this.Accion1_Click);
            // 
            // gbxMDIProductos
            // 
            this.gbxMDIProductos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxMDIProductos.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbxMDIProductos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbxMDIProductos.Controls.Add(this.btnProductos);
            this.gbxMDIProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxMDIProductos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbxMDIProductos.Location = new System.Drawing.Point(761, 43);
            this.gbxMDIProductos.Margin = new System.Windows.Forms.Padding(4);
            this.gbxMDIProductos.Name = "gbxMDIProductos";
            this.gbxMDIProductos.Padding = new System.Windows.Forms.Padding(4);
            this.gbxMDIProductos.Size = new System.Drawing.Size(476, 226);
            this.gbxMDIProductos.TabIndex = 3;
            this.gbxMDIProductos.TabStop = false;
            this.gbxMDIProductos.Text = "Ir a Sección Productos";
            // 
            // btnProductos
            // 
            this.btnProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnProductos.Location = new System.Drawing.Point(136, 76);
            this.btnProductos.Margin = new System.Windows.Forms.Padding(4);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(203, 75);
            this.btnProductos.TabIndex = 1;
            this.btnProductos.Text = "Acceder";
            this.btnProductos.UseVisualStyleBackColor = false;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // gbxMDIiClientes
            // 
            this.gbxMDIiClientes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxMDIiClientes.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbxMDIiClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbxMDIiClientes.Controls.Add(this.btnClientes);
            this.gbxMDIiClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxMDIiClientes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbxMDIiClientes.Location = new System.Drawing.Point(95, 43);
            this.gbxMDIiClientes.Margin = new System.Windows.Forms.Padding(4);
            this.gbxMDIiClientes.Name = "gbxMDIiClientes";
            this.gbxMDIiClientes.Padding = new System.Windows.Forms.Padding(4);
            this.gbxMDIiClientes.Size = new System.Drawing.Size(476, 226);
            this.gbxMDIiClientes.TabIndex = 2;
            this.gbxMDIiClientes.TabStop = false;
            this.gbxMDIiClientes.Text = "Ir a Sección Clientes";
            // 
            // btnClientes
            // 
            this.btnClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClientes.Location = new System.Drawing.Point(135, 80);
            this.btnClientes.Margin = new System.Windows.Forms.Padding(4);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(203, 75);
            this.btnClientes.TabIndex = 0;
            this.btnClientes.Text = "Acceder";
            this.btnClientes.UseVisualStyleBackColor = false;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(453, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "Administración ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TLPHeader
            // 
            this.TLPHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TLPHeader.ColumnCount = 2;
            this.TLPHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.51039F));
            this.TLPHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.48961F));
            this.TLPHeader.Controls.Add(this.label2, 0, 1);
            this.TLPHeader.Controls.Add(this.lblFECHA, 1, 1);
            this.TLPHeader.Controls.Add(this.button1, 0, 0);
            this.TLPHeader.Controls.Add(this.label3, 1, 0);
            this.TLPHeader.Location = new System.Drawing.Point(0, 0);
            this.TLPHeader.Margin = new System.Windows.Forms.Padding(4);
            this.TLPHeader.Name = "TLPHeader";
            this.TLPHeader.RowCount = 2;
            this.TLPHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPHeader.Size = new System.Drawing.Size(1348, 90);
            this.TLPHeader.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(351, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bienvenido, Administrador ";
            // 
            // lblFECHA
            // 
            this.lblFECHA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFECHA.AutoSize = true;
            this.lblFECHA.BackColor = System.Drawing.Color.Transparent;
            this.lblFECHA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFECHA.Location = new System.Drawing.Point(663, 49);
            this.lblFECHA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFECHA.Name = "lblFECHA";
            this.lblFECHA.Size = new System.Drawing.Size(622, 36);
            this.lblFECHA.TabIndex = 2;
            this.lblFECHA.Text = "Martes, 16 de septiembre de 2025 - 10:34 AM";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(25, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(25, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "Volver al Login";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(767, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(414, 42);
            this.label3.TabIndex = 1;
            this.label3.Text = "Optimizando tu gestión cada día";
            // 
            // TLPFooter
            // 
            this.TLPFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TLPFooter.ColumnCount = 2;
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.81328F));
            this.TLPFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.18672F));
            this.TLPFooter.Controls.Add(this.lblDerechos, 0, 0);
            this.TLPFooter.Controls.Add(this.lblSoporte, 1, 0);
            this.TLPFooter.Location = new System.Drawing.Point(0, 791);
            this.TLPFooter.Margin = new System.Windows.Forms.Padding(4);
            this.TLPFooter.Name = "TLPFooter";
            this.TLPFooter.RowCount = 1;
            this.TLPFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.TLPFooter.Size = new System.Drawing.Size(1346, 49);
            this.TLPFooter.TabIndex = 5;
            // 
            // lblDerechos
            // 
            this.lblDerechos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDerechos.AutoSize = true;
            this.lblDerechos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDerechos.Location = new System.Drawing.Point(70, 12);
            this.lblDerechos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDerechos.Name = "lblDerechos";
            this.lblDerechos.Size = new System.Drawing.Size(462, 25);
            this.lblDerechos.TabIndex = 3;
            this.lblDerechos.Text = "© 2025 ArimaERP - Todos los derechos reservados";
            // 
            // lblSoporte
            // 
            this.lblSoporte.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSoporte.AutoSize = true;
            this.lblSoporte.BackColor = System.Drawing.Color.Transparent;
            this.lblSoporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoporte.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSoporte.Location = new System.Drawing.Point(712, 12);
            this.lblSoporte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoporte.Name = "lblSoporte";
            this.lblSoporte.Size = new System.Drawing.Size(524, 25);
            this.lblSoporte.TabIndex = 4;
            this.lblSoporte.Text = "Soporte técnico: soporte@arimaerp.com | (011) 4444-5555";
            // 
            // FormPanelAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1361, 838);
            this.Controls.Add(this.TLPFooter);
            this.Controls.Add(this.TLPHeader);
            this.Controls.Add(this.gbxAdmin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TLPMAIN);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormPanelAdministrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADMINISTRADOR";
            this.Load += new System.EventHandler(this.FormPanelAdministrador_Load);
            this.TLPMAIN.ResumeLayout(false);
            this.gbxAdmin.ResumeLayout(false);
            this.gbxMDIProductos.ResumeLayout(false);
            this.gbxMDIiClientes.ResumeLayout(false);
            this.TLPHeader.ResumeLayout(false);
            this.TLPHeader.PerformLayout();
            this.TLPFooter.ResumeLayout(false);
            this.TLPFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPMAIN;
        private System.Windows.Forms.GroupBox gbxMDIProductos;
        private System.Windows.Forms.GroupBox gbxMDIiClientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxAdmin;
        private System.Windows.Forms.Button btnAuditoria;
        private System.Windows.Forms.Button Accion3;
        private System.Windows.Forms.Button Accion2;
        private System.Windows.Forms.Button Accion1;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.TableLayoutPanel TLPHeader;
        private System.Windows.Forms.TableLayoutPanel TLPFooter;
        private System.Windows.Forms.Label lblDerechos;
        private System.Windows.Forms.Label lblSoporte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFECHA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}