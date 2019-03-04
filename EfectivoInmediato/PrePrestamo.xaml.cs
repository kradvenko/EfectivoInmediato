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
    /// Lógica de interacción para wPrePrestamo.xaml
    /// </summary>
    public partial class PrePrestamo : Window
    {
        public cPrenda prenda;
        public cInteres interes;
        public ObservableCollection<cPago> pagos;

        public PrePrestamo(cPrenda p)
        {
            InitializeComponent();
            prenda = p;
            pagos = new ObservableCollection<cPago>();
            CargarIntereses();
        }

        public void CargarIntereses()
        {
            interes = cInteres.ObtenerInteresDepartamento(prenda.IdDepartamento);

            for (int i = 1; i <= int.Parse(interes.Plazo) + 1; i++)
            {
                cPago p = new cPago();
                if (i == 1)
                {
                    p.Periodo = i.ToString();
                    p.Importe = prenda.Prestamo;

                    if (interes.ReclamoAnticipadoMetodo == "Interes")
                    {
                        p.Intereses = ((float.Parse(interes.ReclamoAnticipadoCantidad) / 100) * (float.Parse(prenda.Prestamo))).ToString();
                        p.Almacenaje = ( 0.01 * (float.Parse(prenda.Prestamo))).ToString();
                        p.IVA = (((float.Parse(p.Intereses) + float.Parse(p.Almacenaje)) * (float.Parse(interes.IVA) / 100)).ToString());
                        p.TotalDesempeno = (float.Parse(p.Importe) + float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.TotalRefrendo = (float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.FechaPago = DateTime.Now.AddDays(int.Parse(interes.ReclamoAnticipadoDias)).ToShortDateString();
                    }
                }
                
                pagos.Add(p);
            }

            dgPagos.ItemsSource = pagos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
