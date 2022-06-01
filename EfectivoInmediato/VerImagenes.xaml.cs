using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para VerImagenes.xaml
    /// </summary>
    public partial class VerImagenes : Window
    {
        String[] Imagenes = new string[9] { "", "", "", "", "", "", "", "", "" };
        String IdPrenda;
        public VerImagenes(String IdPrenda)
        {
            InitializeComponent();
            this.IdPrenda = IdPrenda;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Imagenes = cPrenda.ObtenerImagenesPrenda(IdPrenda);
            UXBotonesImagen();
        }

        private void UXBotonesImagen()
        {
            if (Imagenes[0] != "")
            {
                i01.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[1] != "")
            {
                i02.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[2] != "")
            {
                i03.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[3] != "")
            {
                i04.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[4] != "")
            {
                i05.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[5] != "")
            {
                i06.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[6] != "")
            {
                i07.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[7] != "")
            {
                i08.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
            if (Imagenes[8] != "")
            {
                i09.Style = (Style)FindResource("ButtonStyleSecondaryBarHighlight");
            }
        }

        private void MostrarImagen(object sender, RoutedEventArgs e)
        {
            String por = sender.ToString();

            if (por.Contains("1"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[0], 0);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("2"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[1], 1);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("3"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[2], 2);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("4"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[3], 3);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("5"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[4], 4);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("6"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[5], 5);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("7"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[6], 6);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("8"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[7], 7);
                VerImagen.ShowDialog();
            }
            else if (por.Contains("9"))
            {
                VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR EXISTENTE", Imagenes[8], 8);
                VerImagen.ShowDialog();
            }
        }

        public void GuardarRutaImagen(int Indice, String RutaImagen)
        {
            try
            {
                String ruta = @"C:\Praiz\Imagenes Prendas\Prenda_" + IdPrenda;

                System.IO.Directory.CreateDirectory(ruta);

                String Extension = System.IO.Path.GetExtension(RutaImagen);

                String ArchivoImagen = "Imagen" + (Indice + 1).ToString() + Extension;

                string destFile = System.IO.Path.Combine(ruta, ArchivoImagen);

                System.IO.File.Copy(RutaImagen, destFile, true);

                cPrenda.ActualizarImagenPrenda(IdPrenda, destFile, (Indice + 1).ToString());

                Imagenes[Indice] = destFile;

                UXBotonesImagen();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
