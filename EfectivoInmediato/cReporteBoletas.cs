using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cReporteBoletas
    {
     
        public String IdPrestamo { get; set; }
        public String IdPrenda { get; set; }
        public String IdDepartamento { get; set; }
        public String Contrato { get; set; }
        public String NombreCliente { get; set; }
        public String Descripcion { get; set; }
        public String CantidadPrestada { get; set; }
        public String Intereses { get; set; }
        public String FechaEmpeno { get; set; }
        public String FechaLiquidacion { get; set; }
        public String Estado { get; set; }
        public String CantidadVenta { get; set; }

        public cReporteBoletas()
        {

        }

        public static List<cReporteBoletas> ObtenerReporte(String Busqueda)
        {
            List<cReporteBoletas> reporte = new List<cReporteBoletas>();
            cReporteBoletas c;
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "Select * " +
                        "From Prestamos " +
                        "Inner Join Clientes " +
                        "On Clientes.IdCliente = Prestamos.IdCliente " +
                        "Inner Join Prendas " +
                        "On Prendas.IdPrenda = Prestamos.IdPrenda " +
                        "WHERE (Prestamos.Contrato LIKE @Busqueda) " +
                        "OR (Clientes.NombreCliente LIKE @Busqueda) " +
                        "ORDER BY Prestamos.IdPrestamo " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("Busqueda", Busqueda);

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                c = new cReporteBoletas();

                                c.IdPrestamo = reader["IdPrestamo"].ToString();
                                c.IdPrenda = reader["IdPrenda"].ToString();
                                c.IdDepartamento = reader["IdDepartamento"].ToString();
                                c.Contrato = reader["Contrato"].ToString();
                                c.NombreCliente = reader["NombreCliente"].ToString() + " " + reader["ApellidoPaternoCliente"].ToString() + " " + reader["ApellidoMaternoCliente"].ToString();
                                c.Descripcion = reader["Descripcion"].ToString() + " " + reader["Marca"].ToString() + " " + reader["Modelo"].ToString();
                                c.CantidadPrestada = reader["CantidadPrestada"].ToString();

                                cInteres interes = cInteres.ObtenerInteresDepartamento(c.IdDepartamento);
                                cPago p = new cPago();
                                p.Importe = c.CantidadPrestada;
                                float financiamiento = float.Parse(interes.Financiamiento);
                                float fPago = financiamiento * (3 - 1);
                                int r = 3 - 1;

                                p.Intereses = ((fPago / 100) * (float.Parse(c.CantidadPrestada))).ToString();
                                p.Almacenaje = (r * (float.Parse(interes.Almacenaje) / 100) * (float.Parse(c.CantidadPrestada))).ToString();
                                p.IVA = (((float.Parse(p.Intereses) + float.Parse(p.Almacenaje)) * (float.Parse(interes.IVA) / 100)).ToString());
                                p.TotalDesempeno = (float.Parse(p.Importe) + float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                                p.TotalRefrendo = (float.Parse(p.Intereses) + float.Parse(p.Almacenaje) + float.Parse(p.IVA)).ToString();
                                //p.FechaPago = DateTime.Parse(c.FechaPrestamo).AddMonths(r).ToShortDateString();

                                c.Intereses = p.TotalRefrendo;

                                c.FechaEmpeno = reader["FechaPrestamo"].ToString();
                                c.FechaLiquidacion = "";
                                c.Estado = reader["Estado"].ToString();
                                c.CantidadVenta = reader["PrecioVenta"].ToString();

                                if (reader["Estado"].ToString() == "LIQUIDADO")
                                {
                                    ObservableCollection<cRefrendo> refrendosLiquidado = cRefrendo.ObtenerRefrendos(c.IdPrestamo);
                                    int s = refrendosLiquidado.Count;

                                    c.FechaLiquidacion = refrendosLiquidado[s - 1].FechaRefrendo;
                                }

                                reporte.Add(c);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                c = null;
            }

            return reporte;
        }
    }
}
