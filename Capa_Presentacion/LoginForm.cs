using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Capa_Logica;
using Capa_Entidades;



namespace ArimaERP
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BIngresar_Click(object sender, EventArgs e)
        {

            string nombre = TCorreo.Text.Trim();
            string clave = TContrasena.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UsuarioLogica logica = new UsuarioLogica();
            USUARIO usuario = logica.AutenticarUsuario(nombre, clave);

            if (usuario != null)
            {
                UsuarioSesion.IniciarSesion(usuario.nombre, usuario.id_rol, usuario.rol_descripcion);
                Form mdiForm = null;
                switch (usuario.id_rol)
                {
                    case 5: mdiForm = new Preventista.MDIPreventista();break;
                    case 6: mdiForm = new EmpleadoClientes.MDIClientes(); break;
                    case 7: mdiForm = new EmpleadoProducto.MDIProductos(); break;
                    case 8: mdiForm = new Administrador.FormPanelAdministrador(); break;
                    default:
                        MessageBox.Show("Rol no reconocido."); return;
                }
                mdiForm.StartPosition = FormStartPosition.CenterScreen;
                mdiForm.FormClosed += (s, args) => this.Show(); // Mostrar login al cerrar MDI
                mdiForm.Show();
                TCorreo.Clear();
                TContrasena.Clear();
                this.Hide();           


            }
            else
            {
                MessageBox.Show("Credenciales inválidas o usuario inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            TCorreo.Text = string.Empty;
            TContrasena.Text = string.Empty;
            TCorreo.Focus();
        }

        
    }
}
