using ArimaERP.EmpleadoClientes;
using ArimaERP.Preventista;
using Capa_Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Entidades;

namespace ArimaERP.Preventista
{
    public partial class FormHistorialPedidos : Form
    {
        ClassClienteLogica clienteLogica = new ClassClienteLogica();
        ClassPedidoLogica pedidoLogica = new ClassPedidoLogica();
        ClassFamiliaLogica familiaLogica = new ClassFamiliaLogica();
        ClassMarcaLogica marcaLogica = new ClassMarcaLogica();
        ClassProveedorLogica proveedorLogica = new ClassProveedorLogica();
        ClassProductoLogica productoLogica = new ClassProductoLogica();
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        ClassAuditoriaLogica auditoriaLogica = new ClassAuditoriaLogica();
        ClassZonaLogica zonaLogica = new ClassZonaLogica();
        ClassPagoLogica pagoLogica = new ClassPagoLogica();
        private string usuarioActual;
        public FormHistorialPedidos()
        {
            InitializeComponent();
            usuarioActual = ObtenerUsuarioActual();
        }
        private string ObtenerUsuarioActual()
        {

            return UsuarioSesion.Nombre;
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void FormHistorialPedidos_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioActual))
            {
                MessageBox.Show("No se ha definido un preventista para listar pedidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                       

            var pedidosConSaldoCero = pedidoLogica.ObtenerPedidosSaldadosPorVendedor(usuarioActual);
            if (pedidosConSaldoCero == null || pedidosConSaldoCero.Count == 0)
            {
                MessageBox.Show("No se encontraron ventas para este preventista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CargarPedidosEnHistorial(pedidosConSaldoCero, dataGridViewHistorial);
        }

        private void CargarPedidosEnHistorial(List<PEDIDO> pedidos, DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("ID Pedido", "Venta(N°Pedido)");
            dgv.Columns.Add("factura", "N° Factura");
            dgv.Columns.Add("Fecha", "Fecha Entrega");
            dgv.Columns.Add("Monto", "Monto");
            dgv.Columns.Add("Cliente", "Cliente");

            foreach (var pedido in pedidos)
            {
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                string detalle = $"Factura Nº {pedido.numero_factura}";
                string fecha = pedido.fecha_entrega.ToString("dd/MM/yyyy");
                string clienteNombre = cliente != null ? $"{cliente.nombre} {cliente.apellido}" : "Desconocido";

                dgv.Rows.Add(
                    pedido.id_pedido,
                    detalle,
                    fecha,
                    pedido.total.ToString("C"),
                    clienteNombre
                );
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = dateTimePicker1.Value.Date;
            DateTime fechaHasta = dateTimePicker2.Value.Date;

            if (string.IsNullOrEmpty(usuarioActual))
            {
                MessageBox.Show("No se encontró un preventista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fechaDesde > fechaHasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pedidosSaldados = pedidoLogica.ObtenerPedidosSaldadosPorVendedorYFechas(usuarioActual, fechaDesde, fechaHasta);

            if (pedidosSaldados == null || pedidosSaldados.Count == 0)
            {
                MessageBox.Show("No se encontraron pedidos saldados en el rango indicado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CargarPedidosEnHistorial(pedidosSaldados, dataGridViewHistorial);
            // Calcular totales
            decimal totalVendido = pedidosSaldados.Sum(p => p.total);
            decimal promedioPorVenta = pedidosSaldados.Count > 0 ? totalVendido / pedidosSaldados.Count : 0;
            decimal comision = totalVendido * 0.03m;
            //MessageBox.Show($"Total: {totalVendido}, Promedio: {promedioPorVenta}, Comisión: {comision}");

            // Mostrar en los labels
            lblTotalVendido.Text = $"Total vendido: $ {totalVendido:N2}";
            lblPromedio.Text = $"Total Promedio x Venta: $ {promedioPorVenta:N2}";
            lblComision.Text = $"Comisión generada (3% del total): $ {comision:N2}";
        }
    }
}
