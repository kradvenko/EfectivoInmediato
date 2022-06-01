using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cContrato
    {
        public String IdContrato { get; set; }
        public String NumeroContrato { get; set; }
        public String NumeroContratoEsp { get; set; }
        public String Esp { get; set; }

        public cContrato()
        {

        }

        public static cContrato ObtenerContrato()
        {
            cContrato c = new cContrato();
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM Contratos " +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                c.IdContrato = reader["IdContrato"].ToString();
                                c.NumeroContrato = reader["NumeroContrato"].ToString();
                                c.NumeroContratoEsp = reader["NumeroContratoEsp"].ToString();
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

            return c;
        }

        public static String ActualizarNumeroContrato(String NumeroContrato)
        {
            String resultado = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(" " +
                        "UPDATE Contratos " +
                        "SET NumeroContrato = @NumeroContrato " +
                        "", con))
                    {
                        con.Open();

                        comm.Parameters.AddWithValue("@NumeroContrato", NumeroContrato);

                        comm.ExecuteNonQuery();

                        resultado = "OK";

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

        public static String ActualizarNumeroContratoEsp(String NumeroContrato)
        {
            String resultado = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(" " +
                        "UPDATE Contratos " +
                        "SET NumeroContratoEsp = @NumeroContrato " +
                        "", con))
                    {
                        con.Open();

                        comm.Parameters.AddWithValue("@NumeroContrato", NumeroContrato);

                        comm.ExecuteNonQuery();

                        resultado = "OK";

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
