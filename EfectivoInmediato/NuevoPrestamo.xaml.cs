using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para NuevoPrestamo.xaml
    /// </summary>
    public partial class NuevoPrestamo : Window
    {
        MainWindow parent;
        ObservableCollection<cDepartamento> departamentos;
        ObservableCollection<cCliente> clientes;
        ObservableCollection<String> tipoPrendas;
        ObservableCollection<cPrenda> prendas;

        cContrato contrato;
        cCliente clienteElegido;

        public NuevoPrestamo(MainWindow p)
        {
            InitializeComponent();

            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";
            cbDepartamento.SelectedIndex = 0;

            clientes = cCliente.ObtenerClientes("%");

            tipoPrendas = new ObservableCollection<string>();
            tipoPrendas.Add("Artículo");
            tipoPrendas.Add("Joya");
            tipoPrendas.Add("Vehículo");
            cbTipoPrendas.ItemsSource = tipoPrendas;
            cbTipoPrendas.SelectedIndex = 0;

            prendas = new ObservableCollection<cPrenda>();
            dgPrendas.ItemsSource = prendas;

            parent = p;

            contrato = new cContrato();
            contrato = cContrato.ObtenerContrato();
            tbNumeroContrato.Text = contrato.NumeroContrato;

            tbClientes.AutoCompleteSource = clientes;
        }

        public void RecargarClientes(String IdCliente)
        {
            clientes = cCliente.ObtenerClientes("%");
            tbClientes.Text = "";
            tbClientes.AutoCompleteSource = null;
            tbClientes.AutoCompleteSource = clientes;
        }

        private void CbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void agregarCliente(object sender, RoutedEventArgs e)
        {
            NuevoCliente cliente = new NuevoCliente(this);
            cliente.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AgregarPrenda(object sender, RoutedEventArgs e)
        {
            if (clienteElegido == null)
            {
                MessageBox.Show("No ha elegido un cliente.");
                return;
            }
            if (cbTipoPrendas.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido un tipo de prenda.");
                cbTipoPrendas.Focus();
                return;
            }
            if (cbTipoPrendas.SelectedIndex == 0)
            {
                NuevoArticulo articulo = new NuevoArticulo(this, clienteElegido.IdCliente);
                articulo.ShowDialog();
             
            }
            else if (cbTipoPrendas.SelectedIndex == 1)
            {
                //NuevaJoya joya = new NuevaJoya();
                //joya.ShowDialog();
            }
            else if (cbTipoPrendas.SelectedIndex == 2)
            {
                NuevoVehiculo vehiculo = new NuevoVehiculo(this, clienteElegido.IdCliente);
                vehiculo.ShowDialog();
            }
        }

        public void AgregarPrenda(cPrenda prenda)
        {
            prendas.Add(prenda);
            dgPrendas.ItemsSource = prendas;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (prendas.Count == 0)
            {
                MessageBox.Show("No hay prendas en el préstamo.");
                return;
            }
            contrato.NumeroContrato = tbNumeroContrato.Text;
            contrato.Esp = "NO";
            foreach (cPrenda prenda in prendas)
            {
                PrePrestamo pre = new PrePrestamo(prenda, clienteElegido, this, contrato);
                pre.ShowDialog();

                int c = int.Parse(tbNumeroContrato.Text);
                c++;
                cContrato.ActualizarNumeroContrato(c.ToString());
                contrato = new cContrato();
                contrato = cContrato.ObtenerContrato();
                tbNumeroContrato.Text = contrato.NumeroContrato;
            }

            LimpiarCampos();
        }

        public void LimpiarCampos()
        {
            tbClientes.Text = "";
            clienteElegido = null;
            cbTipoPrendas.SelectedIndex = 0;
            cbDepartamento.SelectedIndex = 0;
            prendas = new ObservableCollection<cPrenda>();
            dgPrendas.ItemsSource = null;
            dgPrendas.ItemsSource = prendas;
            parent.RecargarPrestamos();
            parent.RecargarClientes();            
        }

        private void TbClientes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbClientes_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (tbClientes.SelectedItem != null)
            {
                clienteElegido = (cCliente)tbClientes.SelectedItem;
            }
        }

        private void GuardarEsp(object sender, RoutedEventArgs e)
        {
            if (prendas.Count == 0)
            {
                MessageBox.Show("No hay prendas en el préstamo.");
                return;
            }
            contrato.NumeroContrato = contrato.NumeroContratoEsp;//tbNumeroContrato.Text;
            contrato.Esp = "SI";
            foreach (cPrenda prenda in prendas)
            {
                PrePrestamo pre = new PrePrestamo(prenda, clienteElegido, this, contrato);
                pre.ShowDialog();

                int c = int.Parse(contrato.NumeroContratoEsp);
                c++;
                cContrato.ActualizarNumeroContratoEsp(c.ToString());
                //contrato = new cContrato();
                //contrato = cContrato.ObtenerContrato();
                //tbNumeroContrato.Text = contrato.NumeroContrato;
            }

            LimpiarCampos();
        }
    }
}
