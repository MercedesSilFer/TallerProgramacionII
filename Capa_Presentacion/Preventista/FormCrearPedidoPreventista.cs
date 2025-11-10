using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArimaERP.Preventista
{
    public partial class FormCrearPedidoPreventista : Form
    {
        public FormCrearPedidoPreventista()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //abrir formulario ModalSeleccionarCliente
            ModalSeleccionarCliente modalSeleccionarCliente = new ModalSeleccionarCliente();
            //centrar formulario
            modalSeleccionarCliente.StartPosition = FormStartPosition.CenterParent;
            modalSeleccionarCliente.ShowDialog();
        }
    }
}
