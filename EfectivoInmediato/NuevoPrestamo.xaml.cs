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

        public NuevoPrestamo()
        {
            InitializeComponent();
            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";
        }

        private void CbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            
        }

        public void agregarCliente(object sender, RoutedEventArgs e)
        {

        }
    }
}
