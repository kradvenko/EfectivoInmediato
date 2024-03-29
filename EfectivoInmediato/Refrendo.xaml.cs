﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class Refrendo : System.Windows.Window
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

        public String PagoEspecial;

        public bool FirstInit;

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

            FirstInit = true;

            tbFecha.Text = prestamo.FechaPrestamo;

            dtpFechaRefrendo.SelectedDate = DateTime.Now;

            FirstInit = false;
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
            float cantidadPrestada = float.Parse(prestamo.CantidadPrestada);

            if (refrendos.Count > 0)
            {
                foreach (cRefrendo r in refrendos)
                {
                    if (r.Tipo == "REFRENDO")
                    {
                        prestamo.FechaPrestamo = r.FechaRefrendo;
                    }
                    if (r.Tipo == "ABONO")
                    {
                        cantidadPrestada = cantidadPrestada - float.Parse(r.Refrendo);
                    }
                }
            }

            prestamo.CantidadPrestada = cantidadPrestada.ToString();

            for (int i = 1; i <= int.Parse(interes.Plazo) + 1; i++)
            {
                cPago p = new cPago();
                if (i == 1)
                {
                    p.Periodo = i.ToString();
                    p.Importe = prestamo.CantidadPrestada;

                    if (interes.ReclamoAnticipadoMetodo == "Interes")
                    {
                        p.Intereses = ((float.Parse(interes.ReclamoAnticipadoCantidad) / 100) * (float.Parse(prestamo.CantidadPrestada))).ToString();
                        p.Almacenaje = (0.01 * (float.Parse(prestamo.CantidadPrestada))).ToString();
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
                    p.Importe = prestamo.CantidadPrestada;
                    float financiamiento = float.Parse(interes.Financiamiento);
                    float fPago = financiamiento * (i - 1);
                    int r = i - 1;

                    if (interes.ReclamoAnticipadoMetodo == "Interes")
                    {
                        p.Intereses = ((fPago / 100) * (float.Parse(prestamo.CantidadPrestada))).ToString();
                        p.Almacenaje = (r * (float.Parse(interes.Almacenaje) / 100) * (float.Parse(prestamo.CantidadPrestada))).ToString();
                        p.IVA = (((float.Parse(p.Intereses) + float.Parse(p.Almacenaje)) * (float.Parse(interes.IVA) / 100)).ToString());
                        p.TotalDesempeno = (float.Parse(p.Importe) + float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.TotalRefrendo = (float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                        p.FechaPago = DateTime.Parse(prestamo.FechaPrestamo).AddMonths(r).ToShortDateString();
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
            FechaActual = dtpFechaRefrendo.SelectedDate.Value;
            PagoMinimo = "0";
            PagoLiquidar = "0";
            bool EsPagoNormal = false;

            float financiamiento = 0;
            float almacenaje = 0;
            float administracion = 0;
            float iva = 0;

            financiamiento = float.Parse(interes.Financiamiento);
            almacenaje = float.Parse(interes.Almacenaje);
            administracion = float.Parse(interes.Administracion);
            iva = float.Parse(interes.IVA);

            float interesTotal = financiamiento + almacenaje + administracion;
            interesTotal = interesTotal / 100;

            foreach (cPago item in pagos)
            {
                if (FechaActual <= DateTime.Parse(item.FechaPago))
                {
                    PagoLiquidar = item.TotalDesempeno;
                    PagoMinimo = item.TotalRefrendo;
                    EsPagoNormal = true;
                    break;
                }                
            }

            if (!EsPagoNormal)
            {
                if (pagos.Count > 0)
                {
                    if (DateTime.Now.Date > DateTime.Parse(pagos[pagos.Count - 1].FechaPago).AddDays(int.Parse(interes.DiasDeGracia)))
                    {
                        double DiasPasados = (DateTime.Now.Date - DateTime.Parse(pagos[pagos.Count - 1].FechaPago).AddDays(int.Parse(interes.DiasDeGracia))).TotalDays;
                        DiasPasados = DiasPasados + int.Parse(interes.DiasDeGracia);
                        double Agregado = double.Parse(prestamo.CantidadPrestada) * (0.12/30) * DiasPasados + double.Parse(prestamo.CantidadPrestada) + double.Parse(pagos[pagos.Count - 1].TotalRefrendo);

                        tbxDiasPasados.Text = "Días pasados: " + DiasPasados.ToString();

                        tbxCalculoRefrendo.Text = "Refrendo: $ " + prestamo.CantidadPrestada + " X (" + interesTotal.ToString() + " / 30) X " + DiasPasados.ToString() + " días + $ " + pagos[pagos.Count - 1].TotalRefrendo;

                        tbxCalculoPasado.Text = "Cálculo: $ " + prestamo.CantidadPrestada + " X (" + interesTotal.ToString() + " / 30) X " + DiasPasados.ToString() + " días + $ " + prestamo.CantidadPrestada + " + $ " + pagos[pagos.Count - 1].TotalRefrendo;

                        PagoLiquidar = Agregado.ToString();
                        //PagoMinimo = pagos[pagos.Count - 1].TotalRefrendo;
                        float Min = float.Parse(prestamo.CantidadPrestada) * interesTotal / 30 * float.Parse(DiasPasados.ToString());
                        Min = Min + float.Parse(pagos[pagos.Count - 1].TotalRefrendo);
                        PagoMinimo = Min.ToString();
                    }
                    else
                    {
                        PagoLiquidar = pagos[pagos.Count - 1].TotalDesempeno;
                        PagoMinimo = pagos[pagos.Count - 1].TotalRefrendo;
                    }
                }
            }
            else
            {
                /*
                double DiasPasados = (DateTime.Now.Date - DateTime.Parse(pagos[pagos.Count - 1].FechaPago).AddDays(int.Parse(interes.DiasDeGracia))).TotalDays;
                DiasPasados = DiasPasados + int.Parse(interes.DiasDeGracia);
                double Agregado = double.Parse(prestamo.CantidadPrestada) * (0.12 / 30) * DiasPasados + double.Parse(prestamo.CantidadPrestada) + double.Parse(pagos[pagos.Count - 1].TotalRefrendo);

                tbxDiasPasados.Text = "Días pasados: " + DiasPasados.ToString();
                */

                tbxCalculoPasado.Text = "Cálculo: $ " + prestamo.CantidadPrestada + " + $ " + pagos[pagos.Count - 1].TotalRefrendo;

                //PagoLiquidar = Agregado.ToString();
                //PagoMinimo = pagos[pagos.Count - 1].TotalRefrendo;
            }

            btRefrendoMinimo.Content = "$ " + PagoMinimo;
            btPagoLiquidar.Content = "$ " + PagoLiquidar;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AgregarRefrendoAsync(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea guardar el refrendo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                                String r = cRefrendo.AgregarRefrendo(c);
                                CargarRefrendos();
                                CargarIntereses();
                                CalcularPagoMinimo();

                                await Task.Run(() => CrearBoletaExcel(r, c, "REFRENDO", c.Refrendo, ""));

                                if (MessageBox.Show("Se ha creado el recibo, ¿desea verlo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    System.Diagnostics.Process.Start(ruta);
                                }
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
                                        String r = cRefrendo.AgregarRefrendo(c);
                                        cPrestamo.FinalizarPrestamo(prestamo.IdPrestamo);
                                        int x = 0;
                                        if (int.TryParse(r, out x))
                                        {
                                            MessageBox.Show("Se ha liquidado el préstamo.");
                                            parent.RecargarPrestamos();

                                            await Task.Run(() => CrearBoletaExcel(r, c, "FINAL", c.Refrendo, ""));

                                            if (MessageBox.Show("Se ha creado el recibo, ¿desea verlo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                            {
                                                System.Diagnostics.Process.Start(ruta);
                                            }

                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        //Si la cantidad a refrendar es mayor al refrendo pero menor a la cantidad
                                        //para liquidar se dará la parte al refrendo y el resto a un abono.
                                        cRefrendo c = new cRefrendo();
                                        c.IdPrestamo = prestamo.IdPrestamo;
                                        c.Refrendo = PagoMinimo;
                                        c.FechaRefrendo = dtpFechaRefrendo.SelectedDate.Value.ToShortDateString();
                                        c.Tipo = "REFRENDO";
                                        String r = cRefrendo.AgregarRefrendo(c);

                                        String refrendoEx = c.Refrendo;

                                        float abono = float.Parse(tbRefrendo.Text) - float.Parse(PagoMinimo);
                                        c = new cRefrendo();
                                        c.IdPrestamo = prestamo.IdPrestamo;
                                        c.Refrendo = abono.ToString();
                                        c.FechaRefrendo = dtpFechaRefrendo.SelectedDate.Value.ToShortDateString();
                                        c.Tipo = "ABONO";
                                        cRefrendo.AgregarRefrendo(c);

                                        String abonoEx = c.Refrendo;

                                        CargarRefrendos();
                                        CargarIntereses();
                                        CalcularPagoMinimo();

                                        await Task.Run(() => CrearBoletaExcel(r, c, "ABONO", refrendoEx, abonoEx));

                                        if (MessageBox.Show("Se ha creado el recibo, ¿desea verlo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                        {
                                            System.Diagnostics.Process.Start(ruta);
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
        }

        private void RefrendoMinimo(object sender, RoutedEventArgs e)
        {
            tbRefrendo.Text = PagoMinimo;
        }

        private void ParaLiquidar(object sender, RoutedEventArgs e)
        {
            tbRefrendo.Text = PagoLiquidar;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            parent.RecargarPrestamos();
        }

        public void CrearBoletaExcel(String IdRefrendo, cRefrendo Refrendo, String Tipo, String ImportePago, String Abono)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                object m = Type.Missing;
                Workbook wb = app.Workbooks.Open(
                    @"C:\EfectivoInmediato\ReciboRefrendo.xlsx",
                    m, false, m, m, m, m, m, m, m, m, m, m, m, m);

                Worksheet ws = (Worksheet)wb.ActiveSheet;

                Range r = (Range)ws.UsedRange;

                bool success;

                //Se escribe la fecha del refrendo.
                success = (bool)r.Replace(
                    @"\fecha\",
                    Refrendo.FechaRefrendo,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el número de contrato.
                success = (bool)r.Replace(
                    @"\contrato\",
                    prestamo.Contrato,
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

                //Se escribe el importe del refrendo.
                success = (bool)r.Replace(
                    @"\importe1\",
                    "$ " + ImportePago,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //Se escribe el importe del abono (si aplica).
                success = (bool)r.Replace(
                    @"\importe2\",
                    "$ " + Abono,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                //NUEVOS PAGOS
                cPago p = (cPago)dgPagos.Items[1];
                /*
                */

                //Se escribe el tipo de pago.
                if (Tipo == "REFRENDO")
                {
                    success = (bool)r.Replace(
                        @"\tipo1\",
                        "REFRENDO",
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\tipo2\",
                        "",
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                    @"\imp1\",
                    p.Importe,
                    XlLookAt.xlPart,
                    XlSearchOrder.xlByRows,
                    true, m, m, m);

                    success = (bool)r.Replace(
                        @"\intereses1\",
                        p.Intereses,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\alm1\",
                        p.Almacenaje,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\iva1\",
                        p.IVA,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\desemp1\",
                        p.TotalDesempeno,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\refrendo1\",
                        p.TotalRefrendo,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\fecha1\",
                        p.FechaPago,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    p = (cPago)dgPagos.Items[2];

                    success = (bool)r.Replace(
                        @"\imp2\",
                        p.Importe,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\intereses2\",
                        p.Intereses,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\alm2\",
                        p.Almacenaje,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\iva2\",
                        p.IVA,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\desemp2\",
                        p.TotalDesempeno,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\refrendo2\",
                        p.TotalRefrendo,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\fecha2\",
                        p.FechaPago,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);
                }
                else if (Tipo == "FINAL")
                {
                    success = (bool)r.Replace(
                        @"\tipo1\",
                        "PAGO FINAL",
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\tipo2\",
                        "",
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);
                }
                else if (Tipo == "ABONO")
                {
                    success = (bool)r.Replace(
                        @"\tipo1\",
                        "REFRENDO",
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\tipo2\",
                        "ABONO",
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    //NUEVOS PAGOS
                    p = (cPago)dgPagos.Items[1];

                    success = (bool)r.Replace(
                        @"\imp1\",
                        p.Importe,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\intereses1\",
                        p.Intereses,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\alm1\",
                        p.Almacenaje,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\iva1\",
                        p.IVA,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\desemp1\",
                        p.TotalDesempeno,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\refrendo1\",
                        p.TotalRefrendo,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\fecha1\",
                        p.FechaPago,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    p = (cPago)dgPagos.Items[2];

                    success = (bool)r.Replace(
                        @"\imp2\",
                        p.Importe,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\intereses2\",
                        p.Intereses,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\alm2\",
                        p.Almacenaje,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\iva2\",
                        p.IVA,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\desemp2\",
                        p.TotalDesempeno,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\refrendo2\",
                        p.TotalRefrendo,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);

                    success = (bool)r.Replace(
                        @"\fecha2\",
                        p.FechaPago,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);
                }

                //Se escribe el restante por pagar.
                if (Tipo == "FINAL")
                {
                    success = (bool)r.Replace(
                            @"\restan\",
                            "$ 0",
                            XlLookAt.xlPart,
                            XlSearchOrder.xlByRows,
                            true, m, m, m);
                }
                else
                {
                    success = (bool)r.Replace(
                        @"\restan\",
                        "$ " + PagoLiquidar,
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);
                }

                wb.SaveAs(@"C:\EfectivoInmediato\Refrendos\Refrendo_" + prestamo.IdPrestamo + "_" + IdRefrendo);
                app.Quit();
                //app = null;

                Marshal.ReleaseComObject(app);

                ruta = @"C:\EfectivoInmediato\Refrendos\Refrendo_" + prestamo.IdPrestamo + "_" + IdRefrendo + ".xlsx";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {

            }
        }

        private void DtpFechaRefrendo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!FirstInit)
            {
                CalcularPagoMinimo();
            }
            
        }

        public async void LiquidacionEspecial(String Razon)
        {
            float refrendo;
            if (float.TryParse(tbRefrendo.Text, out refrendo))
            {
                refrendo = float.Parse(tbRefrendo.Text);
                cRefrendo c = new cRefrendo();
                c.IdPrestamo = prestamo.IdPrestamo;
                c.Refrendo = refrendo.ToString();
                c.FechaRefrendo = dtpFechaRefrendo.SelectedDate.Value.ToShortDateString();
                c.Tipo = "FINAL";
                String r = cRefrendo.AgregarRefrendo(c);
                cPrestamo.FinalizarPrestamo(prestamo.IdPrestamo, Razon);
                int x = 0;
                if (int.TryParse(r, out x))
                {
                    MessageBox.Show("Se ha liquidado el préstamo.");
                    parent.RecargarPrestamos();

                    await Task.Run(() => CrearBoletaExcel(r, c, "FINAL", c.Refrendo, ""));

                    if (MessageBox.Show("Se ha creado el recibo, ¿desea verlo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ruta);
                    }

                    this.Close();

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LiquidacionEspecialRazon especial = new LiquidacionEspecialRazon(this);
            especial.ShowDialog();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea guardar el refrendo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (tbRefrendo.Text.Length == 0)
                {
                    MessageBox.Show("No ha escrito una cantidad para el refrendo.");
                    return;
                }
                else
                {
                    float refrendo = float.Parse(tbRefrendo.Text);
                    cRefrendo c = new cRefrendo();
                    c.IdPrestamo = prestamo.IdPrestamo;
                    c.Refrendo = refrendo.ToString();
                    c.FechaRefrendo = dtpFechaRefrendo.SelectedDate.Value.ToShortDateString();
                    c.Tipo = "REFRENDO";
                    String r = cRefrendo.AgregarRefrendo(c);
                    CargarRefrendos();
                    CargarIntereses();
                    CalcularPagoMinimo();

                    await Task.Run(() => CrearBoletaExcel(r, c, "REFRENDO", c.Refrendo, ""));

                    if (MessageBox.Show("Se ha creado el recibo, ¿desea verlo?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ruta);
                    }
                }
            }
        }
    }
}
