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

namespace ArimaERP.EmpleadoClientes
{
    public partial class MDIClientes : Form
    {
        ClassEmpleadoLogica classEmpleadoLogica = new ClassEmpleadoLogica();
        public MDIClientes()
        {
            InitializeComponent();            

        }

        private void MDIClientes_Load(object sender, EventArgs e)
        {
            // Obtener el área de trabajo del monitor principal (sin la barra de tareas)
            Rectangle areaTrabajo = Screen.PrimaryScreen.WorkingArea;

            // Establecer el tamaño y posición del formulario
            this.Location = areaTrabajo.Location;
            this.Size = areaTrabajo.Size;

            lblFecha.Text = lblFecha.Text + DateTime.Now.ToString("dd/MM/yyyy");
            lblHora.Text = lblHora.Text + DateTime.Now.ToString("HH:mm:ss");
            //Obtener usuario actual
            string usuarioActual = ObtenerUsuarioActual();
            //obtener nombre de empleado de tabla Empleado partir de nombre_usuario que corresponde al usuario actual
            Empleado empleado = new ClassEmpleadoLogica().ObtenerEmpleadoPorNombreUsuario(usuarioActual);
            if (empleado != null)
            {
                lblNombreUsuario.Text = $"Vendedor: {empleado.nombre} {empleado.apellido}";
            }
        }
        private string ObtenerUsuarioActual()
        {
            // Aquí debes implementar la lógica para obtener el nombre del usuario actual
            return UsuarioSesion.Nombre; // Ejemplo: retorna el nombre del usuario desde una clase estática de sesión
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            pnlClientes.Visible = true;
            pnlPedidos.Visible = false;
            pnlPreventistas.Visible = false;
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            pnlClientes.Visible = false;
            pnlPedidos.Visible = true;
            pnlPreventistas.Visible = false;
        }

        private void btnPreventista_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            pnlPedidos.Visible = false;
            pnlClientes.Visible = false;
            pnlPreventistas.Visible = true;
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();

            // Instanciar el formulario
            FormCliente formCliente = new FormCliente();

            // Configurar como control embebido
            formCliente.TopLevel = false;
            formCliente.FormBorderStyle = FormBorderStyle.None;
            formCliente.Dock = DockStyle.Fill;

            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formCliente);
            formCliente.Show();
        }

        private void btnModificacion_Click_1(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormModificacionCliente formModificacionCliente = new FormModificacionCliente();
            // Configurar como control embebido
            formModificacionCliente.TopLevel = false;
            formModificacionCliente.FormBorderStyle = FormBorderStyle.None;
            formModificacionCliente.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formModificacionCliente);
            formModificacionCliente.Show();
        }
               

        private void btnBaja_Click(object sender, EventArgs e)
        {
            {
                // Limpiar el panel antes de agregar nuevo contenido
                pnlVistaMenuSecundario.Controls.Clear();
                // Instanciar el formulario
                FormBajaCliente formBajaCliente = new FormBajaCliente();
                // Configurar como control embebido
                formBajaCliente.TopLevel = false;
                formBajaCliente.FormBorderStyle = FormBorderStyle.None;
                formBajaCliente.Dock = DockStyle.Fill;
                // Agregar al panel y mostrar
                pnlVistaMenuSecundario.Controls.Add(formBajaCliente);
                formBajaCliente.Show();
            }
        }

        private void btnCrearPedido_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormCrearPedido formCrearPedido = new FormCrearPedido();
            // Configurar como control embebido
            formCrearPedido.TopLevel = false;
            formCrearPedido.FormBorderStyle = FormBorderStyle.None;
            formCrearPedido.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formCrearPedido);
            formCrearPedido.Show();
        }

        private void btnCobros_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            //ocultar paneles de secciones
            pnlPedidos.Visible = false;
            pnlClientes.Visible = false;
            pnlPreventistas.Visible = false;
            // Instanciar el formulario
            FormPagos formCobros = new FormPagos();
            // Configurar como control embebido
            formCobros.TopLevel = false;
            formCobros.FormBorderStyle = FormBorderStyle.None;
            formCobros.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formCobros);
            formCobros.Show();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //volver al login
            this.Close();
         
        }
                

        private void btnModificarPedido_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormModificarPedido formModificarPedido = new FormModificarPedido();
            // Configurar como control embebido
            formModificarPedido.TopLevel = false;
            formModificarPedido.FormBorderStyle = FormBorderStyle.None;
            formModificarPedido.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formModificarPedido);
            formModificarPedido.Show();

        }

        private void btnCancPedido_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormCancelarPedido formCancelarPedido = new FormCancelarPedido();
            // Configurar como control embebido
            formCancelarPedido.TopLevel = false;
            formCancelarPedido.FormBorderStyle = FormBorderStyle.None;
            formCancelarPedido.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formCancelarPedido);
            formCancelarPedido.Show();
        }

        private void btnAltaPrevent_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormAltaPreventista formAltaPreventista = new FormAltaPreventista();
            // Configurar como control embebido
            formAltaPreventista.TopLevel = false;
            formAltaPreventista.FormBorderStyle = FormBorderStyle.None;
            formAltaPreventista.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formAltaPreventista);
            formAltaPreventista.Show();
        }

        private void btnHistorialPreventistas_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormHistorialPreventista formHistorialPreventistas = new FormHistorialPreventista();
            // Configurar como control embebido
            formHistorialPreventistas.TopLevel = false;
            formHistorialPreventistas.FormBorderStyle = FormBorderStyle.None;
            formHistorialPreventistas.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formHistorialPreventistas);
            formHistorialPreventistas.Show();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            // Limpiar el panel antes de agregar nuevo contenido
            pnlVistaMenuSecundario.Controls.Clear();
            // Instanciar el formulario
            FormClienteHistorial formHistorialClientes = new FormClienteHistorial();
            // Configurar como control embebido
            formHistorialClientes.TopLevel = false;
            formHistorialClientes.FormBorderStyle = FormBorderStyle.None;
            formHistorialClientes.Dock = DockStyle.Fill;
            // Agregar al panel y mostrar
            pnlVistaMenuSecundario.Controls.Add(formHistorialClientes);
            formHistorialClientes.Show();
        }
        

    }
}
