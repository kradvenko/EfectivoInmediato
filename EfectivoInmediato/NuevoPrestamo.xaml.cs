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
        ObservableCollection<cDepartamento> departamentos;
        ObservableCollection<cCliente> clientes;
        ObservableCollection<String> tipoPrendas;
        ObservableCollection<cPrenda> prendas;

        public NuevoPrestamo()
        {
            InitializeComponent();
            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";

            clientes = cCliente.ObtenerClientes();
            cbClientes.ItemsSource = clientes;
            cbClientes.DisplayMemberPath = "NombreCompleto";
            cbClientes.SelectedValuePath = "IdCliente";

            tipoPrendas = new ObservableCollection<string>();
            tipoPrendas.Add("Artículo");
            tipoPrendas.Add("Joya");
            tipoPrendas.Add("Vehículo");
            cbTipoPrendas.ItemsSource = tipoPrendas;

            prendas = new ObservableCollection<cPrenda>();
            dgPrendas.ItemsSource = prendas;

        }

        private void CbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            
        }

        public void agregarCliente(object sender, RoutedEventArgs e)
        {

        }
    }
}
