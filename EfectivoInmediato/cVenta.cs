using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cVenta
    {
        public String IdVenta { get; set; }
        public String IdPrenda { get; set; }
        public String Descuento { get; set; }
        public String Subtotal { get; set; }
        public String Total { get; set; }
        public String HoraVenta { get; set; }
        public String FechaVenta { get; set; }
        public String Estado { get; set; }

        public cVenta()
        {

        }

        public static String GuardarVenta(cVenta venta)
        {
            String resultado = "OK";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Ventas (IdPrenda, Descuento, Subtotal, Total, HoraVenta, FechaVenta, Estado) " +
                        "OUTPUT INSERTED.IdVenta " +
                        "VALUES (@IdPrenda, @Descuento, @Subtotal, @Total, @HoraVenta, @FechaVenta, @Estado)" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", venta.IdPrenda);
                        myCMD.Parameters.AddWithValue("@Descuento", venta.Descuento);
                        myCMD.Parameters.AddWithValue("@Subtotal", venta.Subtotal);
                        myCMD.Parameters.AddWithValue("@Total", venta.Total);
                        myCMD.Parameters.AddWithValue("@HoraVenta", venta.HoraVenta);
                        myCMD.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                        myCMD.Parameters.AddWithValue("@Estado", venta.Estado);

                        resultado = myCMD.ExecuteScalar().ToString();

                        int id;

                        if (int.TryParse(resultado, out id))
                        {
                            SqlCommand cmdUpdate = new SqlCommand("UPDATE Prendas SET Vendida = 'SI' WHERE IdPrenda = @IdPrenda", con);

                            cmdUpdate.Parameters.AddWithValue("@IdPrenda", venta.IdPrenda);

                            cmdUpdate.ExecuteScalar();
                        }
                        else
                        {
                            resultado = "ERROR.";
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                resultado = exc.Message;
            }

            return resultado;
        }
    }
}
