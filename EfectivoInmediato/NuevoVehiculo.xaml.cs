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
    /// Lógica de interacción para NuevoVehiculo.xaml
    /// </summary>
    public partial class NuevoVehiculo : Window
    {
        ObservableCollection<cTipoVehiculo> tiposVehiculo;
        ObservableCollection<cMarcaVehiculo> marcasVehiculo;

        public NuevoVehiculo()
        {
            InitializeComponent();

            tiposVehiculo = new ObservableCollection<cTipoVehiculo>();
            tiposVehiculo = cTipoVehiculo.ObtenerTiposVehiculo();
            cbTipo.ItemsSource = tiposVehiculo;
            cbTipo.DisplayMemberPath = "Tipo";
            cbTipo.SelectedValuePath = "IdTipoVehiculo";
            cbTipo.SelectedIndex = 0;

            marcasVehiculo = new ObservableCollection<cMarcaVehiculo>();
            marcasVehiculo = cMarcaVehiculo.ObtenerMarcasVehiculo();
            cbMarca.ItemsSource = marcasVehiculo;
            cbMarca.DisplayMemberPath = "Marca";
            cbMarca.SelectedValuePath = "IdMarcaVehiculo";
            cbMarca.SelectedIndex = 0;
        }

        private void agregarTipoVehiculo(object sender, RoutedEventArgs e)
        {
            NuevoTipoVehiculo tipo = new NuevoTipoVehiculo(this);
            tipo.ShowDialog();
        }

        public void ActualizarTiposVehiculo()
        {
            tiposVehiculo = new ObservableCollection<cTipoVehiculo>();
            tiposVehiculo = cTipoVehiculo.ObtenerTiposVehiculo();
            cbTipo.ItemsSource = tiposVehiculo;
            cbTipo.DisplayMemberPath = "Tipo";
            cbTipo.SelectedValuePath = "IdTipoVehiculo";
            cbTipo.SelectedIndex = 0;
        }

        private void agregarMarcaVehiculo(object sender, RoutedEventArgs e)
        {

        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
