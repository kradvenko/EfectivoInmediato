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
    /// Lógica de interacción para ModificarCliente.xaml
    /// </summary>
    public partial class ModificarCliente : Window
    {
        MainWindow parentMain;
        ObservableCollection<String> identificaciones;
        cCliente cliente;
        public ModificarCliente(MainWindow p, cCliente c)
        {
            InitializeComponent();
            parentMain = p;
            cliente = c;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            identificaciones = new ObservableCollection<String>();
            identificaciones.Add("INE");
            identificaciones.Add("Pasaporte");
            identificaciones.Add("CURP");
            identificaciones.Add("Licencia para conducir");
            cbTipoIdentificacion.ItemsSource = identificaciones;
            cbTipoIdentificacion.SelectedIndex = 0;

            tbNombre.Text = cliente.NombreCliente;
            tbApPaterno.Text = cliente.ApellidoPaternoCliente;
            tbApMaterno.Text = cliente.ApellidoMaternoCliente;
            cbTipoIdentificacion.Text = cliente.TipoIdentificacion;
            tbClaveIdentificacion.Text = cliente.ClaveIdentificacion;
            tbDomicilio.Text = cliente.Domicilio;
            tbColonia.Text = cliente.Colonia;
            tbCiudad.Text = cliente.Ciudad;
            tbEstado.Text = cliente.Estado;
            tbTelefono1.Text = cliente.Telefono1;
            tbTelefono2.Text = cliente.Telefono2;
            tbCorreoElectronico.Text = cliente.CorreoElectronico;
            tbFechaNacimiento.Text = cliente.FechaNacimiento;
            tbOcupacion.Text = cliente.Ocupacion;
            tbNombreCotitular.Text = cliente.NombreCotitular;
            tbDomicilioCotitular.Text = cliente.DomicilioCotitular;
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

            String resultado = cCliente.ActualizarCliente(cliente.IdCliente, tbNombre.Text, tbApPaterno.Text, tbApMaterno.Text, cbTipoIdentificacion.Text, tbClaveIdentificacion.Text, tbDomicilio.Text, tbColonia.Text, tbCiudad.Text, tbEstado.Text, tbTelefono1.Text, tbTelefono2.Text, tbCorreoElectronico.Text, tbFechaNacimiento.Text, tbOcupacion.Text, tbNombreCotitular.Text, tbDomicilioCotitular.Text);
            if (resultado == "OK")
            {
                MessageBox.Show("Se ha guardado el cliente.");
                parentMain.RecargarClientes();
                this.Close();
            }
            else
            {
                MessageBox.Show(resultado);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ImagenIdentificacion(object sender, RoutedEventArgs e)
        {
            ImagenIdentificacion imagen = new ImagenIdentificacion(cliente);
            imagen.ShowDialog();
        }
    }
}
