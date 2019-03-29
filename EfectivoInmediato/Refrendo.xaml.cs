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
    /// Lógica de interacción para Refrendo.xaml
    /// </summary>
    public partial class Refrendo : Window
    {
        public MainWindow parent;
        public cPrestamo prestamo;
        public cCliente cliente;
        public cPrenda prenda;
        public cInteres interes;
        public ObservableCollection<cPago> pagos;
        public ObservableCollection<cRefrendo> refrendos;
        public String ruta;
        public String PagoMinimo;
        public String PagoLiquidar;
        public DateTime FechaActual;

        public Refrendo(cPrestamo pre, cPrenda p, cCliente c, MainWindow pa)
        {
            InitializeComponent();

            prestamo = pre;
            prenda = p;
            pagos = new ObservableCollection<cPago>();

            cliente = c;
            tbNombreCliente.Text = c.NombreCompleto;
            tbTotalPrestamo.Text = "$ " + prenda.Prestamo;
            parent = pa;

            tbFecha.Text = prestamo.FechaPrestamo;

            dtpFechaRefrendo.SelectedDate = DateTime.Now;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarRefrendos();
            CargarIntereses();
            CalcularPagoMinimo();
            tbRefrendo.Focus();
        }

        private void CargarRefrendos()
        {
            refrendos = cRefrendo.ObtenerRefrendos(prestamo.IdPrestamo);
            dgRefrendos.ItemsSource = refrendos;
        }

        public void CargarIntereses()
        {
            interes = cInteres.ObtenerInteresDepartamento(prenda.IdDepartamento);
            pagos = null;
            pagos = new ObservableCollection<cPago>();

            if (refrendos.Count > 0)
            {
                foreach (cRefrendo r in refrendos)
                {
                    if (r.Tipo == "REFRENDO")
                    {
                        prestamo.FechaPrestamo = r.FechaRefrendo;
                    }
                }
            }

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
                        p.Almacenaje = (0.01 * (float.Parse(prenda.Prestamo))).ToString();
                        p.IVA = (((float.Parse(p.Intereses) + float.Parse(p.Almacenaje)) * (float.Parse(interes.IVA) / 100)).ToString());
                        p.TotalDesempeno = (float.Parse(p.Importe) + float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.TotalRefrendo = (float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.FechaPago = DateTime.Parse(prestamo.FechaPrestamo).AddDays(int.Parse(interes.ReclamoAnticipadoDias)).ToShortDateString();
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
                        p.Almacenaje = (r * (float.Parse(interes.Almacenaje) / 100) * (float.Parse(prenda.Prestamo))).ToString();
                        p.IVA = (((float.Parse(p.Intereses) + float.Parse(p.Almacenaje)) * (float.Parse(interes.IVA) / 100)).ToString());
                        p.TotalDesempeno = (float.Parse(p.Importe) + float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.TotalRefrendo = (float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.FechaPago = DateTime.Parse(prestamo.FechaPrestamo).AddDays(30 * r).ToShortDateString();
                    }
                    else if (interes.ReclamoAnticipadoMetodo == "Monto Fijo")
                    {

                    }
                }

                pagos.Add(p);
            }

            dgPagos.ItemsSource = pagos;
        }

        private void CalcularPagoMinimo()
        {
            FechaActual = DateTime.Now;

            foreach (cPago item in pagos)
            {
                if (FechaActual <= DateTime.Parse(item.FechaPago))
                {
                    PagoLiquidar = item.TotalDesempeno;
                    PagoMinimo = item.TotalRefrendo;
                    break;
                }
            }

            btRefrendoMinimo.Content = "$ " + PagoMinimo;
            btPagoLiquidar.Content = "$ " + PagoLiquidar;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AgregarRefrendo(object sender, RoutedEventArgs e)
        {
            if (tbRefrendo.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito una cantidad para el refrendo.");
                return;
            }
            else
            {
                float refrendo;
                if (float.TryParse(tbRefrendo.Text, out refrendo))
                {
                    refrendo = float.Parse(tbRefrendo.Text);
                    if (refrendo < float.Parse(PagoMinimo))
                    {
                        MessageBox.Show("El pago ingresado es menor al refrendo mínimo.");
                        return;
                    }
                    else
                    {
                        if (refrendo == float.Parse(PagoMinimo))
                        {
                            cRefrendo c = new cRefrendo();
                            c.IdPrestamo = prestamo.IdPrestamo;
                            c.Refrendo = refrendo.ToString();
                            c.FechaRefrendo = dtpFechaRefrendo.SelectedDate.Value.ToShortDateString();
                            c.Tipo = "REFRENDO";
                            cRefrendo.AgregarRefrendo(c);
                            CargarRefrendos();
                            CargarIntereses();
                            CalcularPagoMinimo();
                        }
                        else 
                        {
                            if (refrendo > float.Parse(PagoLiquidar))
                            {
                                MessageBox.Show("No puede pagar más de la cantidad establecida como pago para liquidar.");
                                return;
                            }
                            else
                            {
                                if (refrendo == float.Parse(PagoLiquidar))
                                {
                                    cRefrendo c = new cRefrendo();
                                    c.IdPrestamo = prestamo.IdPrestamo;
                                    c.Refrendo = refrendo.ToString();
                                    c.FechaRefrendo = dtpFechaRefrendo.SelectedDate.Value.ToShortDateString();
                                    c.Tipo = "FINAL";
                                    cRefrendo.AgregarRefrendo(c);
                                    String r = cPrestamo.FinalizarPrestamo(prestamo.IdPrestamo);
                                    if (r == "OK")
                                    {
                                        MessageBox.Show("Se ha liquidado el préstamo.");
                                        parent.RecargarPrestamos();
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No ha escrito una cantidad correcta para el refrendo.");
                }
            }
        }

        private void RefrendoMinimo(object sender, RoutedEventArgs e)
        {
            tbRefrendo.Text = PagoMinimo;
        }

        private void ParaLiquidar(object sender, RoutedEventArgs e)
        {
            tbRefrendo.Text = PagoLiquidar;
        }
    }
}
