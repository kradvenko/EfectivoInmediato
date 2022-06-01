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
using System.IO;

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
                    btnVerPrestamos.Style = (Style)FindResource("ButtonStyleTopBarSelected");
                    btnVerClientes.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerInventario.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerReportes.Style = (Style)FindResource("ButtonStyleTopBar");
                    break;
                case "VerClientes":
                    LimpiarGrids();
                    gClientes.Visibility = Visibility.Visible;
                    btnVerPrestamos.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerClientes.Style = (Style)FindResource("ButtonStyleTopBarSelected");
                    btnVerInventario.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerReportes.Style = (Style)FindResource("ButtonStyleTopBar");
                    break;
                case "VerInventario":
                    LimpiarGrids();
                    gInventario.Visibility = Visibility.Visible;
                    btnVerPrestamos.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerClientes.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerInventario.Style = (Style)FindResource("ButtonStyleTopBarSelected");
                    btnVerReportes.Style = (Style)FindResource("ButtonStyleTopBar");
                    break;
                case "VerReportes":
                    LimpiarGrids();
                    gReportes.Visibility = Visibility.Visible;
                    btnVerPrestamos.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerClientes.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerInventario.Style = (Style)FindResource("ButtonStyleTopBar");
                    btnVerReportes.Style = (Style)FindResource("ButtonStyleTopBarSelected");
                    break;
            }
        }

        public void RecargarClientes()
        {
            clientes = cCliente.ObtenerClientes("%");
            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = clientes;
        }
        
        private void LimpiarGrids()
        {
            gPrestamos.Visibility = Visibility.Collapsed;
            gClientes.Visibility = Visibility.Collapsed;
            gInventario.Visibility = Visibility.Collapsed;
            gReportes.Visibility = Visibility.Collapsed;
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
            //cCambios.EjecutarCambios3();
            System.IO.Directory.CreateDirectory(@"C:\EfectivoInmediato\Identificaciones");
            dtpFechaVentas.SelectedDate = DateTime.Now;

            /*cPrestamo c = new cPrestamo();
            c.NombreCliente = "TEST";
            prestamos.Add(c);
            dgPrestamos.ItemsSource = prestamos;
            dgClientes.ItemsSource = prestamos;*/

            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", "30");
            dgPrestamos.ItemsSource = prestamos;

            clientes = cCliente.ObtenerClientes("%");
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

            btnVerPrestamos.Style = (Style)FindResource("ButtonStyleTopBarSelected");
            btnVerClientes.Style = (Style)FindResource("ButtonStyleTopBar");
            btnVerInventario.Style = (Style)FindResource("ButtonStyleTopBar");
            btnVerReportes.Style = (Style)FindResource("ButtonStyleTopBar");
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
            if (e.Key == Key.Enter)
            {
                int dias = 0;
                String Esp = "NO";

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    Esp = "SI";
                }

                if (int.TryParse(tbDiasVencimiento.Text, out dias))
                {
                    if (cbEnVenta.IsChecked == true)
                    {
                        prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", tbDiasVencimiento.Text, "SI", Esp);
                        dgPrestamos.ItemsSource = null;
                        dgPrestamos.ItemsSource = prestamos;
                    }
                    else
                    {
                        prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", tbDiasVencimiento.Text, "NO", Esp);
                        dgPrestamos.ItemsSource = null;
                        dgPrestamos.ItemsSource = prestamos;
                    }

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

            articulos = cPrenda.ObtenerPrendasVenta("%");
            dgInventario.ItemsSource = articulos;

            int totalEnajenados = articulos.Where(x => x.DePrestamo == "SI").Count();
            int totalAdquiridos = articulos.Where(x => x.DePrestamo == "NO").Count();

            lblEnajenadosTotal.Content = "ENAJENADOS: " + totalEnajenados.ToString();
            lblAdquiridosTotal.Content = "ADQUIRIDOS: " + totalAdquiridos.ToString();
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

            int totalEnajenados = articulos.Where(x => x.DePrestamo == "SI").Count();
            int totalAdquiridos = articulos.Where(x => x.DePrestamo == "NO").Count();

            lblEnajenadosTotal.Content = "ENAJENADOS: " + totalEnajenados.ToString();
            lblAdquiridosTotal.Content = "ADQUIRIDOS: " + totalAdquiridos.ToString();

            lblInventarioTotalArticulos.Content = articulos.Count;

            int totalEnVenta = articulos.Where(x => x.EnVenta == "SI").Count();
            int totalDesempeno = articulos.Where(x => x.EnVenta == "NO").Count();

            lblInventarioTotalArticulosEnVenta.Content = totalEnVenta.ToString();
            lblInventarioTotalArticulosPorDesempenar.Content = totalDesempeno.ToString();
        }

        private void BuscarPrendaPorContrato(object sender, RoutedEventArgs e)
        {
            articulos = cPrenda.ObtenerPrendasVenta(tbBusquedaPrendaContrato.Text);
            dgInventario.ItemsSource = articulos;
        }

        private void VerReportes(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerReportes");
        }

        private void GenerarReporteBoletas(object sender, RoutedEventArgs e)
        {
            List<cReporteBoletas> reporteBoletas = new List<cReporteBoletas>();

            String Busqueda = "%";

            if (tbBusquedaNombreFolio.Text.Length > 0)
            {
                Busqueda = tbBusquedaNombreFolio.Text;
            }

            reporteBoletas = cReporteBoletas.ObtenerReporte(Busqueda);

            dgReporte.ItemsSource = null;
            dgReporte.ItemsSource = reporteBoletas;
        }

        private void BuscarCliente(object sender, RoutedEventArgs e)
        {
            String Busqueda = "%" + tbBusquedaCliente.Text + "%";
            clientes = cCliente.ObtenerClientes(Busqueda);
            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = clientes;
        }

        private void VerVentas(object sender, RoutedEventArgs e)
        {
            String Fecha = dtpFechaVentas.SelectedDate.Value.ToShortDateString();
            articulos = cPrenda.ObtenerPrendasVendidas(Fecha);
            dgInventario.ItemsSource = null;
            dgInventario.ItemsSource = articulos;
        }

        private void VerImagenes(object sender, RoutedEventArgs e)
        {
            if (dgInventario.SelectedItem != null)
            {
                cPrenda p = (cPrenda)dgInventario.SelectedItem;
                VerImagenes VerImgs = new VerImagenes(p.IdPrenda);
                VerImgs.ShowDialog();
            }
            else
            {
                MessageBox.Show("No ha elegido una prenda.");
            }
        }

        private void VerBoleta(object sender, RoutedEventArgs e)
        {
            if (dgPrestamos.SelectedItem != null)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    cPrestamo pres = new cPrestamo();

                    pres = (cPrestamo)dgPrestamos.SelectedItem;

                    String curFile = @"C:\EfectivoInmediato\Boletas\Boleta_" + pres.Contrato + ".xlsx";

                    if (File.Exists(curFile))
                    {
                        System.Diagnostics.Process.Start(curFile);
                    }
                    else
                    {
                        MessageBox.Show("No existe la boleta.");
                    }
                }
                else
                {
                    cPrestamo pres = new cPrestamo();

                    pres = (cPrestamo)dgPrestamos.SelectedItem;

                    String curFile = @"C:\EfectivoInmediato\Boletas\Boleta_" + pres.IdPrestamo + ".xlsx";

                    if (File.Exists(curFile))
                    {
                        System.Diagnostics.Process.Start(curFile);
                    }
                    else
                    {
                        MessageBox.Show("No existe la boleta.");
                    }
                }

                
            }
        }

        private void tbBusquedaNombreFolio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<cReporteBoletas> reporteBoletas = new List<cReporteBoletas>();

                String Busqueda = "%";

                if (tbBusquedaNombreFolio.Text.Length > 0)
                {
                    Busqueda = tbBusquedaNombreFolio.Text;
                }

                reporteBoletas = cReporteBoletas.ObtenerReporte(Busqueda);

                dgReporte.ItemsSource = null;
                dgReporte.ItemsSource = reporteBoletas;
            }
        }

        private void tbBusquedaPrestamos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                prestamos = cPrestamo.ObtenerPrestamosClienteFolio(tbBusquedaPrestamos.Text, tbDiasVencimiento.Text);
                dgPrestamos.ItemsSource = null;
                dgPrestamos.ItemsSource = prestamos;
            }
        }

        private void RegresarPrenda(object sender, RoutedEventArgs e)
        {
            if (dgInventario.SelectedItem != null)
            {
                /*
                cPrenda p = (cPrenda)dgInventario.SelectedItem;
                if (p.Apartada == "SI")
                {
                    MessageBox.Show("La prenda está apartada.");
                }
                else
                {
                    if (p.DePrestamo == "NO")
                    {
                        MessageBox.Show("La prenda no proviene de un préstamo.");
                        return;
                    }

                    if (MessageBox.Show("Desea regresar la prenda a los prestamos?", "ATENCION", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        cPrenda.ActualizarPrendaEnajenada(p.IdPrenda, "NO");
                        ActualizarInventarioLista();
                        MessageBox.Show("Se ha regresado la prenda.");
                    }
                }
                */
                cPrenda p = (cPrenda)dgInventario.SelectedItem;
                if (p.DePrestamo == "NO")
                {
                    MessageBox.Show("La prenda no proviene de un préstamo.");
                    return;
                }

                if (MessageBox.Show("Desea regresar la prenda a los prestamos?", "ATENCION", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    cPrenda.ActualizarPrendaEnajenada(p.IdPrenda, "NO");
                    ActualizarInventarioLista();
                    MessageBox.Show("Se ha regresado la prenda.");
                }
            }
            else
            {
                MessageBox.Show("No ha elegido una prenda.");
            }
        }

        private void GenerarReporteBoletasActivos(object sender, RoutedEventArgs e)
        {
            List<cReporteBoletas> reporteBoletas = new List<cReporteBoletas>();            

            reporteBoletas = cReporteBoletas.ObtenerReporteActivos();

            dgReporte.ItemsSource = null;
            dgReporte.ItemsSource = reporteBoletas;
        }
    }
}
