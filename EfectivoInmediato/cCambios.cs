using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cCambios
    {
        public cCambios()
        {
            
        }

        public static String EjecutarCambios()
        {
            String resultado;
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "IF (EXISTS (SELECT * " +
                        "FROM INFORMATION_SCHEMA.TABLES " +
                        "WHERE TABLE_SCHEMA = 'EfectivoInmediato' " +
                        "AND  TABLE_NAME = 'Ventas')) " +
                            "BEGIN " +
                                "SELECT* " +
                                "FROM Refrendos " +
                            "END " +
                        "ELSE " +
                            "BEGIN " +
                                "SELECT * " +
                                "FROM Clientes " +
                            "END " +
                        "", con))
                    {
                        con.Open();

                        resultado = myCMD.ExecuteScalar().ToString();

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
