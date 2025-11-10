using ArimaERP.EmpleadoClientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArimaERP.Preventista;

namespace ArimaERP.Preventista
{
    public partial class FormHistorialPedidos : Form
    {
        public FormHistorialPedidos()
        {
            InitializeComponent();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            // Crear instancia del formulario hijo que querés abrir
            FormCrearPedidoPreventista formCargar = new FormCrearPedidoPreventista();

            // Acceder al formulario padre MDI y llamar a AbrirFormEnPanel
            MDIPreventista mdi = this.MdiParent as MDIPreventista;
            if (mdi != null)
            {
                mdi.AbrirFormEnPanel(formCargar);
            }
        }
    }
}
