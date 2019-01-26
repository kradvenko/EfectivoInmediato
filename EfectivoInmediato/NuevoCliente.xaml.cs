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
    /// Lógica de interacción para NuevoCliente.xaml
    /// </summary>
    public partial class NuevoCliente : Window
    {
        NuevoPrestamo parentPrestamo;
        MainWindow parentMain;
        ObservableCollection<String> identificaciones;
        String IdClienteInsertado;

        public NuevoCliente()
        {
            InitializeComponent();
            identificaciones = new ObservableCollection<String>();
            identificaciones.Add("INE");
            identificaciones.Add("Pasaporte");
            identificaciones.Add("CURP");
            identificaciones.Add("Licencia para conducir");
            cbTipoIdentificacion.ItemsSource = identificaciones;
            cbTipoIdentificacion.SelectedIndex = 0;
        }

        public NuevoCliente(NuevoPrestamo p)
        {
            InitializeComponent();
            identificaciones = new ObservableCollection<String>();
            identificaciones.Add("INE");
            identificaciones.Add("Pasaporte");
            identificaciones.Add("CURP");
            identificaciones.Add("Licencia para conducir");
            cbTipoIdentificacion.ItemsSource = identificaciones;
            cbTipoIdentificacion.SelectedIndex = 0;
            parentPrestamo = p;
        }

        public NuevoCliente(MainWindow p)
        {
            InitializeComponent();
            identificaciones = new ObservableCollection<String>();
            identificaciones.Add("INE");
            identificaciones.Add("Pasaporte");
            identificaciones.Add("CURP");
            identificaciones.Add("Licencia para conducir");
            cbTipoIdentificacion.ItemsSource = identificaciones;
            cbTipoIdentificacion.SelectedIndex = 0;
            parentMain = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GuardarCliente(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito un nombre de cliente.");
                tbNombre.Focus(); 
                return;
            }
            if (cbTipoIdentificacion.Text.Length == 0)
            {
                MessageBox.Show("No ha elegido el tipo de identificación.");
                cbTipoIdentificacion.Focus();
                return;
            }
            if (tbClaveIdentificacion.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito la clave de la identificación.");
                tbClaveIdentificacion.Focus();
                return;
            }

            String resultado = cCliente.GuardarCliente(tbNombre.Text, tbApPaterno.Text, tbApMaterno.Text, cbTipoIdentificacion.Text, tbClaveIdentificacion.Text, tbDomicilio.Text, tbColonia.Text, tbCiudad.Text, tbEstado.Text, tbTelefono1.Text, tbTelefono2.Text, tbCorreoElectronico.Text, tbFechaNacimiento.Text, tbOcupacion.Text, tbNombreCotitular.Text, tbDomicilioCotitular.Text);
            if (resultado != "0")
            {
                int x;
                if (int.TryParse(resultado, out x))
                {
                    MessageBox.Show("Se ha guardado el cliente.");
                    IdClienteInsertado = resultado;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resultado);
                }
                
            }
            else
            {
                MessageBox.Show(resultado);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (parentPrestamo != null)
            {
                parentPrestamo.RecargarClientes(IdClienteInsertado);
            }
            else if (parentMain != null)
            {
                parentMain.RecargarClientes();
            }
        }
    }
}
