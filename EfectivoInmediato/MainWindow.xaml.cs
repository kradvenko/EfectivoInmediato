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
using System.Deployment.Application;

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<cPrestamo> prestamos = new ObservableCollection<cPrestamo>();
        public ObservableCollection<cCliente> clientes = new ObservableCollection<cCliente>();
        public ObservableCollection<cPrenda> articulos = new ObservableCollection<cPrenda>();

        public MainWindow()
        {
            InitializeComponent();            
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
                case "VerInventario":
                    LimpiarGrids();
                    gInventario.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void RecargarClientes()
        {
            clientes = cCliente.ObtenerClientes();
            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = clientes;
        }
        
        private void LimpiarGrids()
        {
            gPrestamos.Visibility = Visibility.Collapsed;
            gClientes.Visibility = Visibility.Collapsed;
            gInventario.Visibility = Visibility.Collapsed;
        }

        private void VerPrestamos(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerPrestamos");
        }

        private void VerClientes(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerClientes");
        }

        private void VerInventario(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerInventario");
        }

        private void VerConfiguracion(object sender, RoutedEventArgs e)
        {
            Configuracion conf = new Configuracion();
            conf.ShowDialog();
        }

        private void NuevoCliente(object sender, RoutedEventArgs e)
        {
            NuevoCliente nuevo = new NuevoCliente(this);
            nuevo.ShowDialog();
        }

        private void NuevoPrestamo(object sender, RoutedEventArgs e)
        {
            NuevoPrestamo prestamo = new NuevoPrestamo(this);
            prestamo.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cCambios.EjecutarCambios3();
            System.IO.Directory.CreateDirectory(@"C:\EfectivoInmediato\Identificaciones");

            /*cPrestamo c = new cPrestamo();
            c.NombreCliente = "TEST";
            prestamos.Add(c);
            dgPrestamos.ItemsSource = prestamos;
            dgClientes.ItemsSource = prestamos;*/

            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", "30");
            dgPrestamos.ItemsSource = prestamos;

            clientes = cCliente.ObtenerClientes();
            dgClientes.ItemsSource = clientes;            

            string version = null;
            try
            {
                //// get deployment version
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
                //// you cannot read publish version when app isn't installed 
                //// (e.g. during debug)
                version = "No instalado.";
            }

            lblVersion.Content = version;
        }

        public void RecargarPrestamos()
        {
            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", "30");
            dgPrestamos.ItemsSource = null;
            dgPrestamos.ItemsSource = prestamos;
        }

        private void Refrendar(object sender, RoutedEventArgs e)
        {
            if (dgPrestamos.SelectedItem != null)
            {
                cPrenda pren = new cPrenda();
                cPrestamo pres = new cPrestamo();
                cCliente cli = new cCliente();

                pres = (cPrestamo)dgPrestamos.SelectedItem;
                pren = cPrenda.ObtenerPrendaIdPrestamo(pres.IdPrestamo);
                cli = cCliente.ObtenerClienteIdPrestamo(pres.IdPrestamo);
                Refrendo refrendo = new Refrendo(pres, pren, cli, this);
                refrendo.ShowDialog();
            }
            
        }

        private void TbDiasVencimiento_KeyUp(object sender, KeyEventArgs e)
        {
            int dias = 0;

            if (int.TryParse(tbDiasVencimiento.Text, out dias))
            {
                if (cbEnVenta.IsChecked == true)
                {
                    prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", tbDiasVencimiento.Text, "SI");
                    dgPrestamos.ItemsSource = null;
                    dgPrestamos.ItemsSource = prestamos;
                }
                else
                {
                    prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", tbDiasVencimiento.Text);
                    dgPrestamos.ItemsSource = null;
                    dgPrestamos.ItemsSource = prestamos;
                }
                
            }
        }

        private void Finiquitados(object sender, RoutedEventArgs e)
        {
            prestamos = cPrestamo.ObtenerPrestamos("LIQUIDADO");
            dgPrestamos.ItemsSource = null;
            dgPrestamos.ItemsSource = prestamos;
        }

        private void Activos(object sender, RoutedEventArgs e)
        {
            tbDiasVencimiento.Text = "30";
            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", "30");
            dgPrestamos.ItemsSource = null;
            dgPrestamos.ItemsSource = prestamos;
        }

        private void EnajenarPrenda(object sender, RoutedEventArgs e)
        {
            if (dgPrestamos.SelectedItem == null)
            {
                MessageBox.Show("No ha elegido una prenda.");
                return;
            }

            if (MessageBox.Show("¿Desea enajenar la prenda?", "ATENCIÓN", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                cPrenda pren = new cPrenda();
                cPrestamo pres = new cPrestamo();

                pres = (cPrestamo)dgPrestamos.SelectedItem;
                pren = cPrenda.ObtenerPrendaIdPrestamo(pres.IdPrestamo);
                cPrenda.EnajenarPrenda(pren.IdPrenda);

                int dias = 0;

                if (int.TryParse(tbDiasVencimiento.Text, out dias))
                {
                    prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", tbDiasVencimiento.Text);
                    dgPrestamos.ItemsSource = null;
                    dgPrestamos.ItemsSource = prestamos;

                    articulos = cPrenda.ObtenerPrendasVenta("%");
                    dgInventario.ItemsSource = null;
                    dgInventario.ItemsSource = articulos;
                }
            }
        }

        private void EditarProducto(object sender, RoutedEventArgs e)
        {
            if (dgInventario.SelectedItem != null)
            {
                cPrenda p = (cPrenda)dgInventario.SelectedItem;
                PrendaInfo info = new PrendaInfo(p, this);
                info.ShowDialog();
            }
            else
            {
                MessageBox.Show("No ha elegido una prenda.");
            }
        }

        public void ActualizarInventarioLista()
        {
            articulos = cPrenda.ObtenerPrendasVenta("%");
            dgInventario.ItemsSource = null;
            dgInventario.ItemsSource = articulos;
        }

        private void AgregarPrenda(object sender, RoutedEventArgs e)
        {
            AgregarPrenda agregar = new AgregarPrenda(this);
            agregar.ShowDialog();
        }

        private void VenderProducto(object sender, RoutedEventArgs e)
        {
            if (dgInventario.SelectedItem != null)
            {
                cPrenda p = (cPrenda)dgInventario.SelectedItem;
                NuevaVenta venta = new NuevaVenta(this, p);
                venta.ShowDialog();
            }
            else
            {
                MessageBox.Show("No ha elegido una prenda.");
            }
        }

        private void DgPrestamos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dgPrestamos.SelectedItem != null)
                {
                    if (MessageBox.Show("¿Desea eliminar el préstamo por completo?", "ATENCIÓN", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        cPrestamo p = new cPrestamo();
                        p = (cPrestamo)dgPrestamos.SelectedItem;

                        VerificarEliminacion eliminar = new VerificarEliminacion(this, p);

                        eliminar.ShowDialog();
                    }
                }
            }
        }

        private void BuscarPrestamos(object sender, RoutedEventArgs e)
        {
            if (tbBusquedaPrestamos.Text.Length > 0)
            {
                prestamos = cPrestamo.ObtenerPrestamosClienteFolio(tbBusquedaPrestamos.Text, tbDiasVencimiento.Text);
                dgPrestamos.ItemsSource = null;
                dgPrestamos.ItemsSource = prestamos;
            }
        }

        private void ModificarCliente(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem != null)
            {
                cCliente cliente = (cCliente)dgClientes.SelectedItem;
                ModificarCliente modificar = new ModificarCliente(this, cliente);
                modificar.ShowDialog();
            }
        }

        private void GenerarReporteInventario(object sender, RoutedEventArgs e)
        {
            articulos = cPrenda.ObtenerPrendasVenta("%");
            dgInventario.ItemsSource = articulos;
        }

        private void BuscarPrendaPorContrato(object sender, RoutedEventArgs e)
        {
            articulos = cPrenda.ObtenerPrendasVenta(tbBusquedaPrendaContrato.Text);
            dgInventario.ItemsSource = articulos;
        }
    }
}
