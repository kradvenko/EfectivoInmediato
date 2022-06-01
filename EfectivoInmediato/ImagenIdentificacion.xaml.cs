using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Lógica de interacción para ImagenIdentificacion.xaml
    /// </summary>
    public partial class ImagenIdentificacion : Window
    {
        private cCliente ClienteElegido;
        private String RutaFrente;
        private String RutaAtras;
        private String ArchivoFrente;
        private String ArchivoAtras;

        private NuevoCliente Parent;
        public ImagenIdentificacion(cCliente ClienteElegido)
        {
            InitializeComponent();
            this.ClienteElegido = ClienteElegido;
        }
        public ImagenIdentificacion(String Frente, String Atras, NuevoCliente Parent)
        {
            InitializeComponent();
            RutaFrente = "";
            RutaAtras = "";
            ArchivoFrente = "";
            ArchivoAtras = "";
            RutaFrente = Frente;
            RutaAtras = Atras;
            this.Parent = Parent;
            this.ClienteElegido = null;
        }

        private void CargarFrente(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagenes (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                imgFrente.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                RutaFrente = openFileDialog.FileName;
                ArchivoFrente = openFileDialog.SafeFileName;
            }
        }

        private void VerFrente(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ClienteElegido.RutaImagenFrente))
            {
                Process.Start("explorer.exe", ClienteElegido.RutaImagenFrente);
            }
        }

        private void CargarAtras(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagenes (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                imgAtras.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                RutaAtras = openFileDialog.FileName;
                ArchivoAtras = openFileDialog.SafeFileName;
            }
        }

        private void VerAtras(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ClienteElegido.RutaImagenAtras))
            {
                Process.Start("explorer.exe", ClienteElegido.RutaImagenAtras);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ClienteElegido != null)
            {
                RutaFrente = "";
                RutaAtras = "";
                ArchivoFrente = "";
                ArchivoAtras = "";

                if (ClienteElegido.RutaImagenFrente.Length > 0)
                {
                    imgFrente.Source = new BitmapImage(new Uri(ClienteElegido.RutaImagenFrente));
                }
                if (ClienteElegido.RutaImagenAtras.Length > 0)
                {
                    imgAtras.Source = new BitmapImage(new Uri(ClienteElegido.RutaImagenAtras));
                }
            }
            else
            {
                if (RutaFrente != "")
                {
                    imgFrente.Source = new BitmapImage(new Uri(RutaFrente));
                }
                if (RutaAtras != "")
                {
                    imgAtras.Source = new BitmapImage(new Uri(RutaAtras));
                }
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Guardar(object sender, RoutedEventArgs e)
        {
            if (ClienteElegido != null)
            {
                bool actualizado = false;
                if (RutaFrente != "")
                {
                    String ruta = @"C:\Efectivo Inmediato\Identificaciones\" + ClienteElegido.NombreCompleto;

                    System.IO.Directory.CreateDirectory(ruta);

                    string destFile = System.IO.Path.Combine(ruta, ArchivoFrente);

                    System.IO.File.Copy(RutaFrente, destFile, true);

                    cCliente.ActualizarImagenFrenteCliente(ClienteElegido.IdCliente, RutaFrente);

                    ClienteElegido.RutaImagenFrente = RutaFrente;

                    actualizado = true;
                }

                if (RutaAtras != "")
                {
                    String ruta = @"C:\Efectivo Inmediato\Identificaciones\" + ClienteElegido.NombreCompleto;

                    System.IO.Directory.CreateDirectory(ruta);

                    string destFile = System.IO.Path.Combine(ruta, ArchivoAtras);

                    System.IO.File.Copy(RutaAtras, destFile, true);

                    cCliente.ActualizarImagenAtrasCliente(ClienteElegido.IdCliente, RutaAtras);

                    ClienteElegido.RutaImagenAtras = RutaAtras;

                    actualizado = true;
                }

                if (actualizado)
                {
                    MessageBox.Show("Se ha actualizado la imagen de la identificación.");
                    this.Close();
                }
            }
            else
            {
                if (RutaFrente != "" && RutaAtras != "")
                {
                    Parent.GuardarRutasImagen(RutaFrente, RutaAtras);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No ha elegido ambas imagenes.");
                }
            }
        }
    }
}
