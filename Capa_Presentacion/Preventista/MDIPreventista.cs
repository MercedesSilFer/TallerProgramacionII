using Capa_Entidades;
using Capa_Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArimaERP.Preventista
{
    public partial class MDIPreventista : Form
    {
        ClassEmpleadoLogica classEmpleadoLogica = new ClassEmpleadoLogica();
        ClassZonaLogica zonaLogica = new ClassZonaLogica();
        public MDIPreventista()
        {
            InitializeComponent();
            
        }

        private void MDIProductos_Load(object sender, EventArgs e)
        {
            //cargar fecha en lblFecha
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblRol.Text = "Preventista";
            //Obtener usuario actual
            string usuarioActual = ObtenerUsuarioActual();
            int id_zona = zonaLogica.BuscarZonaPorPreventista(usuarioActual);
            var zona = zonaLogica.ObtenerZonaPorId(id_zona);
            lblZona.Text = $"Zona: {zona.nombre}";
            //obtener nombre de empleado de tabla Empleado partir de nombre_usuario que corresponde al usuario actual
            Empleado empleado = new ClassEmpleadoLogica().ObtenerEmpleadoPorNombreUsuario(usuarioActual);
            if (empleado != null)
            {
                lblNombre.Text = $"{empleado.nombre} {empleado.apellido}";
            }
        }
        
         private string ObtenerUsuarioActual()
        {
            
            return UsuarioSesion.Nombre; 
        }

        public void AbrirFormEnPanel(Form formHijo)
        {
            // Si ya hay controles dentro del panel, los limpio
            pnlContent.Controls.Clear();

            // Configuro el form hijo
            formHijo.TopLevel = false;            // No será ventana independiente
            formHijo.FormBorderStyle = FormBorderStyle.None; // Sin bordes
            formHijo.Dock = DockStyle.Fill;       // Que ocupe todo el panel

            // Lo agrego al panel
            pnlContent.Controls.Add(formHijo);
            pnlContent.Tag = formHijo;

            // Muestro el formulario
            formHijo.Show();
        }



        private void lblRol_Click(object sender, EventArgs e)
        {

        }

        private void btnAlerta_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormHistorialPedidos());
        }

        private void btnABM_Click(object sender, EventArgs e)
        {

            //AbrirFormEnPanel(new FormHistorialPedidos());

            //abrir crear pedido en panel pnlContent
            AbrirFormEnPanel(new FormCrearPedido());
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {

        }

        private void btnFMP_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormAltaCliente());
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormRutas());
        }
       

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();
          
        }

    }
}
