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
    /// Lógica de interacción para AgregarPrenda.xaml
    /// </summary>
    public partial class AgregarPrenda : Window
    {
        MainWindow parent;
        ObservableCollection<String> tipoPrendas;

        public AgregarPrenda(MainWindow p)
        {
            InitializeComponent();

            tipoPrendas = new ObservableCollection<string>();
            tipoPrendas.Add("Artículo");
            tipoPrendas.Add("Joya");
            tipoPrendas.Add("Vehículo");
            cbTipoPrendas.ItemsSource = tipoPrendas;
            cbTipoPrendas.SelectedIndex = 0;
            parent = p;
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            if (cbTipoPrendas.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido un tipo de prenda.");
                cbTipoPrendas.Focus();
                return;
            }
            if (cbTipoPrendas.SelectedIndex == 0)
            {
                NuevoArticulo articulo = new NuevoArticulo(null, "0");
                articulo.ShowDialog();
            }
            else if (cbTipoPrendas.SelectedIndex == 1)
            {
                NuevaJoya joya = new NuevaJoya();
                joya.ShowDialog();
            }
            else if (cbTipoPrendas.SelectedIndex == 2)
            {
                NuevoVehiculo vehiculo = new NuevoVehiculo(null, "0");
                vehiculo.ShowDialog();
            }
        }

        private void CbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.ActualizarInventarioLista();
        }
    }
}
