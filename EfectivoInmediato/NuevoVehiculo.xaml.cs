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

        NuevoPrestamo parent;
        String IdCliente;

        public NuevoVehiculo(NuevoPrestamo p, String id)
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

            parent = p;

            IdCliente = id;
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

        private void Guardar(object sender, RoutedEventArgs e)
        {
            if (cbTipo.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido un tipo de vehículo.");
                cbTipo.Focus();
                return;
            }
            if (cbMarca.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido una marca de vehículo.");
                cbTipo.Focus();
                return;
            }
            if (tbAvaluo.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito el avalúo de la prenda.");
                tbAvaluo.Focus();
                return;
            }
            float f;
            if (!float.TryParse(tbAvaluo.Text, out f))
            {
                MessageBox.Show("No ha escrito un avalúo correcto.");
                tbAvaluo.Focus();
                return;
            }
            if (tbPrestamo.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito la cantidad del préstamo.");
                tbPrestamo.Focus();
                return;
            }
            if (!float.TryParse(tbPrestamo.Text, out f))
            {
                MessageBox.Show("No ha escrito una cantidad correcta para el préstamo.");
                tbPrestamo.Focus();
                return;
            }
            //Si no hay parent significa que no viene de un préstamo el ingreso de la prenda y se meterá
            //directamente al inventario para su venta.
            String EnVenta = "NO";
            if (parent == null)
            {
                EnVenta = "SI";
            }

            String idPrenda = cPrenda.GuardarPrenda("0", IdCliente, "0", "VEHICULO", cbTipo.Text + " - " + cbMarca.Text + " " + tbModelo.Text , "-", "-", "-", "0", "-", "-", "-", "0", "-", "-", "-", "-", "-", cbTipo.SelectedValue.ToString(), cbMarca.SelectedValue.ToString(), tbModelo.Text, tbAnio.Text, tbKilometraje.Text, tbNumeroSerie.Text, tbPlacas.Text, tbColor.Text, tbUbicacionAlmacen.Text, tbObservaciones.Text, tbAvaluo.Text, tbPrestamo.Text, EnVenta);
            cPrenda p = cPrenda.ObtenerPrendaId(idPrenda);
            if (parent != null)
            {
                parent.AgregarPrenda(p);
            }
            this.Close();
        }
    }
}
