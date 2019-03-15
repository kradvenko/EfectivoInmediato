using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cPrestamo
    {
        public String IdPrestamo { get; set; }
        public String IdPrestamoPadre { get; set; }
        public String IdCliente { get; set; }
        public String IdPrenda { get; set; }
        public String Contrato { get; set; }
        public String NombreCliente { get; set; }
        public String NombrePrenda { get; set; }
        public String CantidadPrestada { get; set; }
        public String FechaPrestamo { get; set; }
        public String FechaVencimiento { get; set; }
        public String Estado { get; set; }

        public cPrestamo()
        {

        }

        public static String AgregarPrestamo(cPrestamo p)
        {
            String respuesta = "0";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Prestamos (IdPrestamoPadre, IdCliente, IdPrenda, Contrato, CantidadPrestada, FechaPrestamo, FechaVencimiento, Estado) " +
                        "OUTPUT INSERTED.IdPrestamo " +
                        "VALUES (@IdPrestamoPadre, @IdCliente, @IdPrenda, @Contrato, @CantidadPrestada, @FechaPrestamo, @FechaVencimiento, @Estado)" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrestamoPadre", p.IdPrestamoPadre);
                        myCMD.Parameters.AddWithValue("@IdCliente", p.IdCliente);
                        myCMD.Parameters.AddWithValue("@IdPrenda", p.IdPrenda);
                        myCMD.Parameters.AddWithValue("@Contrato", p.Contrato);
                        myCMD.Parameters.AddWithValue("@CantidadPrestada", p.CantidadPrestada);
                        myCMD.Parameters.AddWithValue("@FechaPrestamo", p.FechaPrestamo);
                        myCMD.Parameters.AddWithValue("@FechaVencimiento", p.FechaVencimiento);
                        myCMD.Parameters.AddWithValue("@Estado", p.Estado);

                        respuesta = myCMD.ExecuteScalar().ToString();

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                respuesta = "0";
            }

            return respuesta;
        }
    }
}
