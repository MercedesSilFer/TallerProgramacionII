using Capa_Entidades;
using Capa_Logica;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArimaERP.EmpleadoClientes
{
    public partial class FormPagos : Form
    {
        ClassClienteLogica clienteLogica = new ClassClienteLogica();
        ClassEmpleadoLogica empleadoLogica = new ClassEmpleadoLogica();
        ClassPedidoLogica pedidoLogica = new ClassPedidoLogica();
        ClassPagoLogica pagoLogica = new ClassPagoLogica();
        ClassUsuarioLogica usuarioLogica = new ClassUsuarioLogica();
        public FormPagos()
        {
            InitializeComponent();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            //cerrar
            this.Close();
        }
        private void FormPagos_Load(object sender, EventArgs e)
        {
            
            //crear dataGridviewPedidos con columnas
            dataGridViewPedidos.Columns.Add("id_pedido", "ID_Pedido");
            dataGridViewPedidos.Columns.Add("fecha_entrega", "Fecha de Entrega");
            dataGridViewPedidos.Columns.Add("fecha_vencimiento", "Fecha Vencimiento");
            dataGridViewPedidos.Columns.Add("id_cliente", "ID_Cliente");
            //ocultar columna id_cliente
            dataGridViewPedidos.Columns["id_cliente"].Visible = false;
            dataGridViewPedidos.Columns.Add("nombre_cliente", "Nombre Cliente");
            dataGridViewPedidos.Columns.Add("total", "Total");
            dataGridViewPedidos.Columns.Add("saldo", "Saldo");
            dataGridViewPedidos.Columns.Add("numero_factura", "Factura N°");
            dataGridViewPedidos.Columns.Add("vendedor", "Vendedor");
            //ocultar columna vendedor
            dataGridViewPedidos.Columns["vendedor"].Visible = false;
            dataGridViewPedidos.Columns.Add("nombre_vendedor", "Vendedor");
            //Agregar botón pagar
            DataGridViewButtonColumn btnPagar = new DataGridViewButtonColumn();
            btnPagar.HeaderText = "Generar Pago";
            btnPagar.Name = "pagar";
            btnPagar.Text = "Pagar";
            btnPagar.UseColumnTextForButtonValue = true;
            dataGridViewPedidos.Columns.Add(btnPagar);
            dataGridViewPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPedidos.MultiSelect = false;
            dataGridViewPedidos.AllowUserToAddRows = false;
            //NO permitir modificar el ancho de las filas
            dataGridViewPedidos.AllowUserToResizeRows = false;
            //agregar btnVerPagos
            DataGridViewButtonColumn btnVerPagos = new DataGridViewButtonColumn();
            btnVerPagos.HeaderText = "Pagos";
            btnVerPagos.Name = "btnVerPagos";
            btnVerPagos.Text = "Ver Pagos";
            btnVerPagos.UseColumnTextForButtonValue = true;
            dataGridViewPedidos.Columns.Add(btnVerPagos);
            //cargar zonas en comboBoxClienteZona
            var zonas = clienteLogica.ObtenerZonas();
            comboBoxClienteZona.DataSource = zonas;
            comboBoxClienteZona.DisplayMember = "nombre";
            comboBoxClienteZona.ValueMember = "id_zona";
            comboBoxClienteZona.SelectedIndex = -1; // No seleccionar nada al inicio
            //cargar metodos de pago en comboBoxMetodoPago desde ClassPagoLogica            
            var metodosPago = pagoLogica.ObtenerMetodosPago();
            comboBoxMetodoPago.DataSource = metodosPago;
            comboBoxMetodoPago.DisplayMember = "descripcion";
            comboBoxMetodoPago.ValueMember = "id_metodo";
            comboBoxMetodoPago.SelectedIndex = -1; // No seleccionar nada al inicio
            //Cargar todos los pedidos en el dataGridViewPedidos
            List<PEDIDO> todosLosPedidos = pedidoLogica.ObtenerTodosLosPedidos();
            CargarPedidosEnDataGridView(todosLosPedidos);
            //Crear columnas de dataGridViewDetallePagos
            dataGridViewDetallePagos.Columns.Add("id_pago", "ID_Pago");
            dataGridViewDetallePagos.Columns.Add("monto", "Monto");
            dataGridViewDetallePagos.Columns.Add("fecha", "Fecha de Pago");
            dataGridViewDetallePagos.Columns.Add("id_metodo", "ID_Metodo");
            dataGridViewDetallePagos.Columns.Add("metodo_pago", "Método de Pago");
            dataGridViewDetallePagos.Columns.Add("id_cliente", "ID_Cliente");
            dataGridViewDetallePagos.Columns.Add("nombre_cliente", "Nombre Cliente");
            //Ocultar columnas id_metodo e id_cliente
            dataGridViewDetallePagos.Columns["id_metodo"].Visible = false;
            dataGridViewDetallePagos.Columns["id_cliente"].Visible = false;
            dataGridViewDetallePagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDetallePagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDetallePagos.MultiSelect = false;
            dataGridViewDetallePagos.AllowUserToAddRows = false;
            //NO permitir modificar el ancho de las filas
            dataGridViewDetallePagos.AllowUserToResizeRows = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limpiar datGridViewDetallePagos
            dataGridViewDetallePagos.Rows.Clear();
            //Limpiar textBoxCliente
            textBoxCliente.Text = "";
            //Limpiar txtMonto
            txtMonto.Text = "";
            //Limpiar txtIdCliente
            txtIdCliente.Text = "";
            //Eliminar selección de comboBoxMetodoPago
            comboBoxMetodoPago.SelectedIndex = -1;
        }

        private void btnRegistrarNuevo_Click(object sender, EventArgs e)
        {
            // Validar campo Monto
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                errorProvider1.SetError(txtMonto, "El campo Monto es obligatorio.");
                return;
            }
            errorProvider1.SetError(txtMonto, "");

            // Validar campo Cliente
            if (string.IsNullOrWhiteSpace(textBoxCliente.Text))
            {
                errorProvider1.SetError(textBoxCliente, "El campo Cliente es obligatorio.");
                return;
            }
            errorProvider1.SetError(textBoxCliente, "");

            // Validar método de pago
            if (comboBoxMetodoPago.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBoxMetodoPago, "Debe seleccionar un método de pago.");
                return;
            }
            errorProvider1.SetError(comboBoxMetodoPago, "");

            // Obtener datos del pedido seleccionado
            if (dataGridViewPedidos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dataGridViewPedidos.CurrentRow;
            int idPedido = Convert.ToInt32(fila.Cells["id_pedido"].Value);
            //Obtener PEDIDO por id_pedido
            var pedido = pedidoLogica.ObtenerPedidoPorId(idPedido);

            decimal saldo;
            // Obtener el saldo actual del pedido desde base de datos
            var ultimoPago = pagoLogica.ObtenerUltimoPedidoPagoPorIdPedido(idPedido);
            if (ultimoPago != null)
            {
                saldo = ultimoPago.saldo;
                //Si el saldo es 0.00, el pedido se encuentra completamente abonado
                if (saldo == 0m)
                {
                    MessageBox.Show("El pedido ya se encuentra completamente abonado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Salir del flujo de pago
                }

            }
            else
            {
                // No hay registros → usar el total del pedido como saldo inicial
                saldo = pedido.total;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("El monto debe ser un número mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (monto > saldo)
            {
                MessageBox.Show("El monto ingresado no puede ser mayor al saldo del pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener datos del cliente y método de pago
            int id_cliente = Convert.ToInt32(txtIdCliente.Text);
            int id_metodo = Convert.ToInt32(comboBoxMetodoPago.SelectedValue);
            DateTime fecha = DateTime.Today;

            // Registrar el pago
            int idPagoGenerado = pagoLogica.CrearNuevoPago(monto, fecha, id_metodo, id_cliente);
            if (idPagoGenerado == -1)
            {
                MessageBox.Show("Error al registrar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Registrar en pedido_pago con saldo actualizado
            decimal nuevoSaldo = saldo - monto;
            bool registrado = pagoLogica.CrearNuevopedido_pago(idPedido, idPagoGenerado, nuevoSaldo);
            if (!registrado)
            {
                MessageBox.Show("Error al registrar el pago en el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ajustar cuenta corriente si el cliente es confiable
            var cliente = clienteLogica.ObtenerClientePorId(id_cliente);
            if (cliente != null && cliente.confiable)
            {
                bool actualizado = clienteLogica.AjustarSaldo(id_cliente, -monto);
                if (!actualizado)
                {
                    MessageBox.Show("No se pudo actualizar el saldo de la cuenta corriente del cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            MessageBox.Show("Pago registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refrescar grillas
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
            btnLimpiar.PerformClick();
        }


        private void txtBuscarPedidosCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = txtBuscarPedidosCliente.Text.Trim();

                if (string.IsNullOrEmpty(textoBusqueda))
                {
                    MessageBox.Show("Debe ingresar un DNI o Email para buscar.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? idCliente = pedidoLogica.ObtenerIdClientePorTexto(textoBusqueda);

                dataGridViewPedidos.Rows.Clear();

                if (idCliente.HasValue)
                {
                    List<PEDIDO> pedidos = pedidoLogica.ObtenerPedidosPorIdCliente(idCliente.Value);
                    CargarPedidosEnDataGridView(pedidos);
                }
                else
                {
                    MessageBox.Show("No se encontró ningún cliente con ese DNI o Email.", "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Función para sumar días hábiles
        private DateTime SumarDiasHabiles(DateTime fecha, int diasHabiles)
        {
            int diasSumados = 0;
            DateTime resultado = fecha;
            while (diasSumados < diasHabiles)
            {
                resultado = resultado.AddDays(1);
                if (resultado.DayOfWeek != DayOfWeek.Saturday && resultado.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasSumados++;
                }
            }
            return resultado;
        }

        private void txtMontoMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permitir el ingreso de numeros y un punto decimal únicamente
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(txtMontoMaximo, "Solo se permiten números y un punto decimal.");
            }
            else if (e.KeyChar == '.' && txtMontoMaximo.Text.Contains('.'))
            {
                e.Handled = true; // No permitir más de un punto decimal
                errorProvider1.SetError(txtMontoMaximo, "Solo se permite un punto decimal.");
            }
            else
            {
                errorProvider1.SetError(txtMontoMaximo, ""); // Limpiar el error si la entrada es válida
            }
        }

        private void textBoxNumeroFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ingresar solo números y no mas de 10 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxNumeroFactura, "Ingrese solo números.");
            }
            else if (
                textBoxNumeroFactura.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(textBoxNumeroFactura, "No puede ingresar más de 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(textBoxNumeroFactura, "");
            }
        }

        private void textBoxNumeroFactura_KeyDown(object sender, KeyEventArgs e)
        {
            //Obtener pedido por número de factura al presionar Enter
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = textBoxNumeroFactura.Text.Trim();
                if (int.TryParse(textoBusqueda, out int numeroFactura))
                {
                    // Limpiar el DataGrid antes de cargar nuevos datos
                    dataGridViewPedidos.Rows.Clear();
                    // Obtener el pedido por número de factura
                    PEDIDO pedido = pedidoLogica.ObtenerPedidoPorNumeroFactura(numeroFactura);
                    if (pedido != null)
                    {
                        //Obtener CLIENTE por pedido.id_cliente
                        var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                        string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                        //Obtener VENDEDOR por pedido.vendedor
                        var empleado = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(pedido.vendedor);
                        string nombreVendedor = $"{empleado.nombre} {empleado.apellido}";
                        //Si el cliente no es confiable la fecha vencimiento es igual a la fecha_entrega si no la fecha vencimiento es igual a la fecha_entrega mas 5 días hábiles
                        DateTime fecha_vencimiento;
                        DateTime.TryParse(pedido.fecha_entrega.ToString("dd/MM/yyyy"), out fecha_vencimiento);
                        decimal saldo;
                        /*Obtener el valor del saldo de la tabla pedido_pago para este id_pedido, si no se encuentra ningún registro para ese pedido
                         el saldo es igual al total del pedido*/

                        if (cliente.confiable)
                        {
                            fecha_vencimiento = pedido.fecha_entrega;
                            saldo = pagoLogica.ObtenerSaldoActual(pedido.id_pedido);
                            if (saldo == 0)
                                saldo = pedido.total;
                        }
                        else
                        {
                            fecha_vencimiento = SumarDiasHabiles(pedido.fecha_entrega, 5);
                            saldo = pedido.total;
                        }

                        dataGridViewPedidos.Rows.Add(
                            pedido.id_pedido,
                            pedido.fecha_entrega.ToString("dd/MM/yyyy"),
                            fecha_vencimiento.ToString("dd/MM/yyyy"),
                            pedido.id_cliente,
                            nombreCompleto,
                            pedido.total.ToString("C"),
                            saldo.ToString("C"),
                            pedido.numero_factura,
                            pedido.vendedor,
                            nombreVendedor
                        );
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún pedido con ese número de factura.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un número de factura válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtMontoMaximo_KeyDown(object sender, KeyEventArgs e)
        {
            //filtrar pedidos por monto máximo al presionar Enter
            if (e.KeyCode == Keys.Enter)
            {
                dataGridViewPedidos.Rows.Clear();
                string textoBusqueda = txtMontoMaximo.Text.Trim();
                if (decimal.TryParse(textoBusqueda, out decimal montoMaximo))
                {
                    // Limpiar el DataGrid antes de cargar nuevos datos
                    dataGridViewPedidos.Rows.Clear();
                    // Obtener los pedidos por monto máximo
                    List<PEDIDO> pedidosPorMonto = pedidoLogica.ObtenerPedidosPorMontoMaximo(montoMaximo);
                    CargarPedidosEnDataGridView(pedidosPorMonto);
                }
                else
                {
                    MessageBox.Show("Ingrese un monto válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void CargarPedidosEnDataGridView(List<PEDIDO> pedidos)
        {
            dataGridViewPedidos.Rows.Clear();

            foreach (var pedido in pedidos)
            {
                //Obtener CLIENTE por pedido.id_cliente
                var cliente = clienteLogica.ObtenerClientePorId(pedido.id_cliente);
                string nombreCompleto = $"{cliente.nombre} {cliente.apellido}";
                //Si el cliente no es confiable la fecha vencimiento es igual a la fecha_entrega si no la fecha vencimiento es igual a la fecha_entrega mas 5 días hábiles
                DateTime fecha_vencimiento;
                DateTime.TryParse(pedido.fecha_entrega.ToString("dd/MM/yyyy"), out fecha_vencimiento);


                if (cliente.confiable)
                {
                    fecha_vencimiento = pedido.fecha_entrega;
                }

                else
                {
                    fecha_vencimiento = SumarDiasHabiles(pedido.fecha_entrega, 5);
                }

                decimal saldo;

                // Obtener el saldo actual del pedido desde base de datos
                var ultimoPago = pagoLogica.ObtenerUltimoPedidoPagoPorIdPedido(pedido.id_pedido);
                if (ultimoPago != null)
                {
                    saldo = ultimoPago.saldo;                 

                }
                else
                {
                    // No hay registros → usar el total del pedido como saldo
                    saldo = pedido.total;
                }

                //Obtener VENDEDOR por pedido.vendedor
                var empleado = empleadoLogica.ObtenerEmpleadoPorNombreUsuario(pedido.vendedor);
                string nombreVendedor = $"{empleado.nombre} {empleado.apellido}";
                dataGridViewPedidos.Rows.Add(
                    pedido.id_pedido,
                            pedido.fecha_entrega.ToString("dd/MM/yyyy"),
                            fecha_vencimiento.ToString("dd/MM/yyyy"),
                            pedido.id_cliente,
                            nombreCompleto,
                            pedido.total,
                            saldo,
                            pedido.numero_factura,
                            pedido.vendedor,
                            nombreVendedor
                );
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permitir el ingreso de numeros y un punto decimal únicamente
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                errorProvider1.SetError(txtMontoMaximo, "Solo se permiten números y un punto decimal.");
            }
            else if (e.KeyChar == '.' && txtMontoMaximo.Text.Contains('.'))
            {
                e.Handled = true; // No permitir más de un punto decimal
                errorProvider1.SetError(txtMontoMaximo, "Solo se permite un punto decimal.");
            }
            else
            {
                errorProvider1.SetError(txtMontoMaximo, ""); // Limpiar el error si la entrada es válida
            }
        }

        private void btnFechaEntrega_Click(object sender, EventArgs e)
        {
            //Obtener todos los pedidos con la fecha de entrega indicada en dateTimePicker1
            DateTime fechaSeleccionada = dateTimePicker1.Value.Date;
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaEntrega(fechaSeleccionada);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void btnCreacion_Click(object sender, EventArgs e)
        {
            //Obtener todos los pedidos con la fecha de creación indicada en dateTimePicker4
            DateTime fechaSeleccionada = dateTimePicker4.Value.Date;
            List<PEDIDO> pedidosPorFecha = pedidoLogica.ObtenerPedidosPorFechaCreacion(fechaSeleccionada);
            CargarPedidosEnDataGridView(pedidosPorFecha);
        }

        private void comboBoxClienteZona_SelectedIndexChanged(object sender, EventArgs e)
        {

            //filtrar clientes por zona seleccionada
            if (comboBoxClienteZona.SelectedValue is int idZona && idZona > 0)
            {
                dataGridViewPedidos.Rows.Clear();
                //Obtener todos los clientes de la zona seleccionada
                List<CLIENTE> clientesEnZona = clienteLogica.ClientesPorZona(idZona);

                foreach (var cliente in clientesEnZona)
                {
                    List<PEDIDO> pedidosDelCliente = pedidoLogica.ObtenerPedidosPorIdCliente(cliente.id_cliente);
                    //cargar pedidos en dataGridViewModificarPedidos
                    CargarPedidosEnDataGridView(pedidosDelCliente);
                }
                comboBoxClienteZona.SelectedIndex = -1; //resetear comboBox después de filtrar
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Permitir solo números en el campo DNI y no mas de 8 caracteres
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtDNI, "Ingrese solo números.");
            }
            else if (
                txtDNI.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtDNI, "No puede ingresar más de 8 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtDNI, "");

            }
        }

        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            //Por el DNI buscar empleado y validar con nombre_usuario que sea un usuario de id_rol = 5 al presionar Enter.Luego buscar los pedidos de ese empleado
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = txtDNI.Text.Trim();
                //convertir textoBusqueda a int
                int dniBusqueda = int.Parse(textoBusqueda);
                // Obtener el empleado por DNI
                Empleado empleado = empleadoLogica.ObtenerEmpleadoPorDNI(dniBusqueda);
                //Obtener Usuario por nombre_usuario del empleado
                if (empleado != null)
                {
                    var usuario = usuarioLogica.ObtenerUsuarioPorNombre(empleado.nombre_usuario);
                    if (usuario != null)
                    {
                        if (usuario.id_rol != 5)
                        {
                            MessageBox.Show("El empleado no es un preventista.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún usuario asociado al empleado.", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún empleado con ese DNI.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Limpiar el DataGrid antes de cargar nuevos datos
                dataGridViewPedidos.Rows.Clear();
                {
                    // Obtener los pedidos del empleado
                    List<PEDIDO> pedidos = pedidoLogica.ObtenerPedidosPorVendedor(empleado.nombre_usuario);
                    CargarPedidosEnDataGridView(pedidos);
                }

            }
        }

        private void dataGridViewPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Cuando se presiona el botón btnVerPagos cargar los pagos del id_pedido desde tabla pedido_pago de base de datos, en dataGridViewDetallePagos
            if (e.RowIndex >= 0 && dataGridViewPedidos.Columns[e.ColumnIndex].Name == "btnVerPagos")
            {
                // Obtener el id_pedido de la fila seleccionada
                int idPedido = Convert.ToInt32(dataGridViewPedidos.Rows[e.RowIndex].Cells["id_pedido"].Value);
                // Limpiar el DataGrid antes de cargar nuevos datos
                dataGridViewDetallePagos.Rows.Clear();
                // Obtener los registros de pedido_pago asociados al pedido
                List<pedido_pago> pagosDelPedido = pagoLogica.ObtenerPedidoPagosPorIdPedido(idPedido);
                foreach (var pedidoPago in pagosDelPedido)
                {
                    // Obtener el pago completo por id_pago
                    var pago = pagoLogica.ObtenerPagoPorId(pedidoPago.id_pago);
                    if (pago != null)
                    {
                        // Obtener CLIENTE por pago.id_cliente
                        var cliente = clienteLogica.ObtenerClientePorId(pago.id_cliente);
                        string nombreCompleto = cliente != null ? $"{cliente.nombre} {cliente.apellido}" : "";
                        // Obtener METODO DE PAGO por pago.id_metodo
                        var metodoPago = pagoLogica.ObtenerMetodoPagoPorId(pago.id_metodo);
                        string descripcionMetodo = metodoPago != null ? metodoPago.descripcion : "";
                        dataGridViewDetallePagos.Rows.Add(
                            pago.id_pago,
                            pago.monto,
                            pago.fecha.ToString("dd/MM/yyyy"),
                            pago.id_metodo,
                            descripcionMetodo,
                            pago.id_cliente,
                            nombreCompleto
                        );
                    }
                }
            }
            else if (e.RowIndex >= 0 && dataGridViewPedidos.Columns[e.ColumnIndex].Name == "pagar")
            {
                /*Si el cliente no es confiable el monto a pagar tiene que ser el saldo total  del pedido, cargar en el txtMonto y no permitir editar txtMonto,
                 y si es confiable puede abonar una valor menor al total del pedido, también cargar el saldo en el txtMonto y permitir editar txtMonto */

                // Obtener el id_pedido de la fila seleccionada
                int idPedido = Convert.ToInt32(dataGridViewPedidos.Rows[e.RowIndex].Cells["id_pedido"].Value);
                //Obtener PEDIDO por id_pedido
                var pedido = pedidoLogica.ObtenerPedidoPorId(idPedido);
                // Obtener el cliente asociado al pedido
                int idCliente = Convert.ToInt32(dataGridViewPedidos.Rows[e.RowIndex].Cells["id_cliente"].Value);
         
                //Cargar id_cliente en txtIdCliente
                txtIdCliente.Text = idCliente.ToString();
                var cliente = clienteLogica.ObtenerClientePorId(idCliente);
                //Mostrar nombre del cliente en textBoxCliente
                textBoxCliente.Text = $" {cliente.nombre} {cliente.apellido}";                       
                               
                decimal saldo;
               
                // Obtener el saldo actual del pedido desde base de datos
                var ultimoPago = pagoLogica.ObtenerUltimoPedidoPagoPorIdPedido(idPedido);
                if (ultimoPago != null)
                {
                    saldo= ultimoPago.saldo;
                    //Si el saldo es 0.00, el pedido se encuentra completamente abonado
                    if (saldo == 0m)
                    {
                        MessageBox.Show("El pedido ya se encuentra completamente abonado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // Salir del flujo de pago
                    }

                }
                else
                {
                    // No hay registros → usar el total del pedido como saldo inicial
                    saldo = pedido.total;
                }
                
                txtMonto.Text = saldo.ToString();
                //Si el cliente es confiable puede editar txtMonto
                if (cliente.confiable)
                {
                    txtMonto.ReadOnly= false;
                }
                else
                {
                    txtMonto.ReadOnly = true;
                }
             
            }
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            CargarPedidosEnDataGridView(pedidoLogica.ObtenerTodosLosPedidos());
        }
        
    } 
}
