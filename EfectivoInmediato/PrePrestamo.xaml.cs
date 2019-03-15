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
        public cCliente cliente;
        public cPrenda prenda;
        public cInteres interes;
        public ObservableCollection<cPago> pagos;

        public PrePrestamo(cPrenda p, cCliente c)
        {
            InitializeComponent();
            prenda = p;
            pagos = new ObservableCollection<cPago>();
            CargarIntereses();
            cliente = c;
            tbNombreCliente.Text = c.NombreCompleto;
            tbTotalPrestamo.Text = "$ " + prenda.Prestamo;
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
                    else if (interes.ReclamoAnticipadoMetodo == "Monto Fijo")
                    {

                    }
                }
                else
                {
                    p.Periodo = i.ToString();
                    p.Importe = prenda.Prestamo;
                    float financiamiento = float.Parse(interes.Financiamiento);
                    float fPago = financiamiento * (i - 1);
                    int r = i - 1;

                    if (interes.ReclamoAnticipadoMetodo == "Interes")
                    {
                        p.Intereses = ((fPago / 100) * (float.Parse(prenda.Prestamo))).ToString();
                        p.Almacenaje = (r * (float.Parse(interes.Almacenaje)/100) * (float.Parse(prenda.Prestamo))).ToString();
                        p.IVA = (((float.Parse(p.Intereses) + float.Parse(p.Almacenaje)) * (float.Parse(interes.IVA) / 100)).ToString());
                        p.TotalDesempeno = (float.Parse(p.Importe) + float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.TotalRefrendo = (float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.FechaPago = DateTime.Now.AddDays(30 * r).ToShortDateString();
                    }
                    else if (interes.ReclamoAnticipadoMetodo == "Monto Fijo")
                    {

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Atención", "¿Desea guardar el préstamo?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                cPrestamo p = new cPrestamo();
                p.IdPrestamoPadre = "0";
                p.IdCliente = cliente.IdCliente;
                p.IdPrenda = prenda.IdPrenda;
                p.Contrato = "";
                p.CantidadPrestada = prenda.Prestamo;
                p.FechaPrestamo = DateTime.Now.ToString();
                p.FechaVencimiento = pagos[pagos.Count - 1].FechaPago;
                p.Estado = "ACTIVO";

                String IdPrestamo = cPrestamo.AgregarPrestamo(p);

                if (IdPrestamo != "0")
                {
                    MessageBox.Show("Se ha ingresado el préstamo.");
                    this.Close();
                }
            }
        }
    }
}
