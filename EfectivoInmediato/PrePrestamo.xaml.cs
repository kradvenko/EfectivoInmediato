using Microsoft.Office.Interop.Excel;
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
    public partial class PrePrestamo : System.Windows.Window
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

                p.IdPrestamo = IdPrestamo;

                CrearBoletaExcel(p);
            }
        }

        public void CrearBoletaExcel(cPrestamo p)
        {
            try
            {
                object m = Type.Missing;

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                Workbook wb = app.Workbooks.Open(
                    @"C:\EfectivoInmediato\Boleta2M.xlsx",
                    m, false, m, m, m, m, m, m, m, m, m, m, m, m);

                Worksheet ws = (Worksheet)wb.ActiveSheet;

                Range r = (Range)ws.UsedRange;

                bool success;

                //Se escribe el nombre de la sucursal.
                success = (bool)r.Replace(
                    @"\sucursal\",
                    "MATRIZ",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el horario de la sucursal.
                success = (bool)r.Replace(
                    @"\hor\",
                    "9:00 a.m. A 7:00 p.m.",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe la fecha del documento.
                success = (bool)r.Replace(
                    @"\fecha\",
                    DateTime.Now.ToShortDateString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el número de contrato.
                success = (bool)r.Replace(
                    @"\contrato\",
                    p.IdPrestamo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el nombre del cliente.
                success = (bool)r.Replace(
                    @"\cliente\",
                    cliente.NombreCompleto,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el tipo de idenficación del cliente.
                success = (bool)r.Replace(
                    @"\tipoIDCli\",
                    cliente.TipoIdentificacion,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el número de idenficación del cliente.
                success = (bool)r.Replace(
                    @"\numIDCli\",
                    cliente.ClaveIdentificacion,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el domicilio del cliente.
                success = (bool)r.Replace(
                    @"\domCli\",
                    cliente.Domicilio,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el teléfono del cliente.
                success = (bool)r.Replace(
                    @"\telCli\",
                    cliente.Telefono1,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);
                /*
                //Se escribe el correo electrónico del cliente.
                success = (bool)r.Replace(
                    @"\emailCliente\",
                    cliente.CorreoElectronico,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);
                    */
                // Se escribe el nombre del cotitular del cliente.
                success = (bool)r.Replace(
                    @"\cotitular\",
                    cliente.NombreCotitular,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el CAT.
                success = (bool)r.Replace(
                    @"\cat\",
                    (float.Parse(interes.CalcularCAT()) / 100).ToString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe la cantidad para desempeñar.
                success = (bool)r.Replace(
                    @"\desemp\",
                    pagos[pagos.Count-1].Importe,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el porcentaje de almacenaje.
                success = (bool)r.Replace(
                    @"\pAlmcnj\",
                    (float.Parse(interes.Almacenaje) / 100).ToString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe la cantidad por reposición de contrato.
                success = (bool)r.Replace(
                    @"\repos\",
                    "$ 15.00",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe la cantidad por reclamo extemporaneo.
                success = (bool)r.Replace(
                    @"\intExtempo\",
                    (float.Parse(interes.ReclamoExtemporaneoCantidad) / 100).ToString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el porcentaje de gastos de administración.
                success = (bool)r.Replace(
                    @"\pAdmon\",
                    interes.Administracion,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe la fecha de vencimiento.
                success = (bool)r.Replace(
                    @"\fecVen\",
                    pagos[pagos.Count - 1].FechaPago,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el interes final.
                success = (bool)r.Replace(
                    @"\finan\",
                    pagos[pagos.Count - 1].Intereses,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el almacenaje final.
                success = (bool)r.Replace(
                    @"\almcnj\",
                    pagos[pagos.Count - 1].Almacenaje,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el IVA final.
                success = (bool)r.Replace(
                    @"\iva\",
                    pagos[pagos.Count - 1].IVA,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el refrendo final.
                success = (bool)r.Replace(
                    @"\refren\",
                    pagos[pagos.Count - 1].TotalRefrendo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe nombre de la prenda.
                success = (bool)r.Replace(
                    @"\prenda1\",
                    prenda.Descripcion,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el avalúo de la prenda.
                success = (bool)r.Replace(
                    @"\ava1\",
                    prenda.Avaluo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                // Se escribe el préstamo de la prenda.
                success = (bool)r.Replace(
                    @"\pres1\",
                    prenda.Prestamo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el avalúo de la prenda.
                success = (bool)r.Replace(
                    @"\avaluoTotal\",
                    prenda.Avaluo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el prestamo de la prenda.
                success = (bool)r.Replace(
                    @"\pres\",
                    prenda.Prestamo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el pago mínimo.
                success = (bool)r.Replace(
                    @"\pres\",
                    interes.PagoMinimo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escriben las observaciones.
                success = (bool)r.Replace(
                    @"\observ1\",
                    "",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el número del contrato con PROFECO.
                success = (bool)r.Replace(
                    @"\contratoProfeco\",
                    "8361-2018",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el domicilio del establecimiento.
                success = (bool)r.Replace(
                    @"\domProp\",
                    "GIRASOL Num. 15 COL. CIUDAD INDUSTRIAL.",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el teléfono del establecimiento.
                success = (bool)r.Replace(
                    @"\telProp\",
                    "311 160 8815",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el correo del establecimiento.
                success = (bool)r.Replace(
                    @"\emailProp\",
                    "-",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe la dirección de la página del establecimiento.
                success = (bool)r.Replace(
                    @"\paginaProp\",
                    "-",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                // Se escribe el nombre de la ciudad.
                success = (bool)r.Replace(
                    @"\ciudad\",
                    "TEPIC, NAYARIT",
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                // Se escribe el día.
                success = (bool)r.Replace(
                    @"\dia\",
                    DateTime.Today.Day.ToString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                // Se escribe el mes.
                success = (bool)r.Replace(
                    @"\mes\",
                    DateTime.Today.ToString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                // Se escribe el año.
                success = (bool)r.Replace(
                    @"\año\",
                    DateTime.Today.Year.ToString(),
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                app.CalculateFull();

                wb.SaveAs(@"C:\EfectivoInmediato\Boletas\Boleta_" + p.IdPrestamo);
                app.Quit();
                app = null;
            }
            catch (Exception exc)
            {

            }
        }
    }
}
