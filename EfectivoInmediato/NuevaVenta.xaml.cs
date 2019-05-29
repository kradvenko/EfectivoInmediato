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
    /// Lógica de interacción para NuevaVenta.xaml
    /// </summary>
    public partial class NuevaVenta : Window
    {
        MainWindow parent;
        cPrenda prenda;
        float nuevoTotal;

        public NuevaVenta(MainWindow p, cPrenda pre)
        {
            InitializeComponent();
            parent = p;
            prenda = pre;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbNombrePrenda.Text = prenda.Descripcion;
            tbPrecioVenta.Text = prenda.PrecioVentaDisplay;
            tbTotal.Text = prenda.PrecioVentaDisplay;
            nuevoTotal = float.Parse(prenda.PrecioVenta);
        }

        private void Vender(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea vender la prenda?", "ATENCIÓN", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                cVenta v = new cVenta();
                v.IdPrenda = prenda.IdPrenda;
                v.Descuento = tbDescuento.Text;
                v.Subtotal = prenda.PrecioVenta;
                v.Total = nuevoTotal.ToString();
                v.HoraVenta = DateTime.Now.TimeOfDay.ToString();
                v.FechaVenta = DateTime.Now.Date.ToShortDateString();
                v.Estado = "ACTIVO";

                String resultado = cVenta.GuardarVenta(v);

                int id;

                if (int.TryParse(resultado, out id))
                {
                    MessageBox.Show("Se ha realizado la venta.");
                    parent.ActualizarInventarioLista();
                    this.Close();
                }
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TbDescuento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float d = 0;

                if (float.TryParse(tbDescuento.Text, out d))
                {
                    d = float.Parse(tbDescuento.Text);
                    float t = float.Parse(prenda.PrecioVenta);

                    nuevoTotal = t - d;
                    tbTotal.Text = "$ " + nuevoTotal.ToString();
                }
                else
                {
                    MessageBox.Show("No ha ingresado un descuento válido.");
                    tbDescuento.Text = "0";
                    return;
                }
            }
        }
    }
}
