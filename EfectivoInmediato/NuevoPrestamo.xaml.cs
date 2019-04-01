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

        public NuevoPrestamo(MainWindow p)
        {
            InitializeComponent();

            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";
            cbDepartamento.SelectedIndex = 0;

            clientes = cCliente.ObtenerClientes();
            cbClientes.ItemsSource = clientes;
            cbClientes.DisplayMemberPath = "NombreCompleto";
            cbClientes.SelectedValuePath = "IdCliente";

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
        }

        public void RecargarClientes(String IdCliente)
        {
            clientes = cCliente.ObtenerClientes();
            cbClientes.ItemsSource = clientes;
            cbClientes.DisplayMemberPath = "NombreCompleto";
            cbClientes.SelectedValuePath = "IdCliente";
            cbClientes.SelectedValue = IdCliente;
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
            if (cbTipoPrendas.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido un tipo de prenda.");
                cbTipoPrendas.Focus();
                return;
            }
            if (cbTipoPrendas.SelectedIndex == 0)
            {
                if (cbClientes.SelectedIndex >= 0)
                {
                    NuevoArticulo articulo = new NuevoArticulo(this, cbClientes.SelectedValue.ToString());
                    articulo.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No ha elegido un cliente.");
                    return;
                }
            }
            else if (cbTipoPrendas.SelectedIndex == 1)
            {
                NuevaJoya joya = new NuevaJoya();
                joya.ShowDialog();
            }
            else if (cbTipoPrendas.SelectedIndex == 2)
            {
                NuevoVehiculo vehiculo = new NuevoVehiculo(this, cbClientes.SelectedValue.ToString());
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
            foreach (cPrenda prenda in prendas)
            {
                PrePrestamo pre = new PrePrestamo(prenda, (cCliente)cbClientes.SelectedItem, this, contrato);
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
            cbClientes.SelectedIndex = -1;
            cbTipoPrendas.SelectedIndex = 0;
            cbDepartamento.SelectedIndex = 0;
            prendas = new ObservableCollection<cPrenda>();
            dgPrendas.ItemsSource = null;
            dgPrendas.ItemsSource = prendas;
            parent.RecargarPrestamos();
            parent.RecargarClientes();            
        }
    }
}
