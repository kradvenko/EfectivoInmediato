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
    /// Lógica de interacción para PrendaInfo.xaml
    /// </summary>
    public partial class PrendaInfo : Window
    {
        cPrenda prenda;
        MainWindow parent;

        public PrendaInfo(cPrenda p, MainWindow pa)
        {
            InitializeComponent();
            prenda = p;
            parent = pa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float precio;

            if (float.TryParse(tbPrecio.Text, out precio))
            {
                cPrenda.ActualizarPrecioVenta(prenda.IdPrenda, tbPrecio.Text);
                parent.ActualizarInventarioLista();
                this.Close();
            }
            else
            {
                MessageBox.Show("No ha ingresado un precio de venta correcto.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbPrecio.Text = prenda.PrecioVenta;
        }
    }
}
