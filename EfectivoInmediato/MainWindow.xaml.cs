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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<cPrestamo> prestamos = new ObservableCollection<cPrestamo>();

        public MainWindow()
        {
            InitializeComponent();
            cPrestamo c = new cPrestamo();
            c.NombreCliente = "TEST";
            prestamos.Add(c);
            dgPrestamos.ItemsSource = prestamos;
            dgClientes.ItemsSource = prestamos;
        }

        private void MostrarGrid(String Opcion)
        {
            switch (Opcion)
            {
                case "VerPrestamos":
                    LimpiarGrids();
                    gPrestamos.Visibility = Visibility.Visible;
                    break;
                case "VerClientes":
                    LimpiarGrids();
                    gClientes.Visibility = Visibility.Visible;
                    break;
            }
        }
        
        private void LimpiarGrids()
        {
            gPrestamos.Visibility = Visibility.Collapsed;
            gClientes.Visibility = Visibility.Collapsed;
        }

        private void VerPrestamos(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerPrestamos");
        }

        private void VerClientes(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerClientes");
        }

        private void VerConfiguracion(object sender, RoutedEventArgs e)
        {
            Configuracion conf = new Configuracion();
            conf.ShowDialog();
        }

        private void NuevoCliente(object sender, RoutedEventArgs e)
        {
            NuevoCliente nuevo = new NuevoCliente();
            nuevo.ShowDialog();
        }

        private void NuevoPrestamo(object sender, RoutedEventArgs e)
        {
            NuevoPrestamo prestamo = new NuevoPrestamo();
            prestamo.ShowDialog();
        }
    }
}
