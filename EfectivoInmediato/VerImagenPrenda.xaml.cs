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
    /// Lógica de interacción para VerImagenPrenda.xaml
    /// </summary>
    public partial class VerImagenPrenda : Window
    {
        private String ArchivoImagen;

        NuevoArticulo Parent;
        VerImagenes ParentI;
        private String Modo;
        private String RutaImagen;
        private int Indice;
        public VerImagenPrenda(NuevoArticulo Parent, String Modo, String RutaImagen, int Indice)
        {
            InitializeComponent();
            this.Parent = Parent;
            this.Modo = Modo;
            this.RutaImagen = RutaImagen;
            this.Indice = Indice;
        }

        public VerImagenPrenda(VerImagenes Parent, String Modo, String RutaImagen, int Indice)
        {
            InitializeComponent();
            this.ParentI = Parent;
            this.Modo = Modo;
            this.RutaImagen = RutaImagen;
            this.Indice = Indice;
        }

        private void CargarFrente(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagenes (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imgImagenPrenda.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                RutaImagen = openFileDialog.FileName;
                ArchivoImagen = openFileDialog.SafeFileName;
            }
        }

        private void VerFrente(object sender, RoutedEventArgs e)
        {
            if (File.Exists(RutaImagen))
            {
                Process.Start("explorer.exe", RutaImagen);
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Modo == "NUEVO")
            {

            }
            else if (Modo == "MODIFICAR NUEVO")
            {
                imgImagenPrenda.Source = new BitmapImage(new Uri(RutaImagen));
            }
            else if (Modo == "MODIFICAR EXISTENTE")
            {
                if (RutaImagen != "")
                {
                    imgImagenPrenda.Source = new BitmapImage(new Uri(RutaImagen));
                }
            }
        }

        private void Guardar(object sender, RoutedEventArgs e)
        {
            if (Modo == "NUEVO")
            {
                Parent.GuardarRutaImagen(Indice, RutaImagen);
                this.Close();
            }
            else if (Modo == "MODIFICAR NUEVO")
            {
                Parent.GuardarRutaImagen(Indice, RutaImagen);
                this.Close();
            }
            else if (Modo == "MODIFICAR EXISTENTE")
            {
                ParentI.GuardarRutaImagen(Indice, RutaImagen);
                this.Close();
            }
        }
    }
}
