using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cRefrendo
    {
        public String IdRefrendo { get; set; }
        public String IdPrestamo { get; set; }
        public String Refrendo { get; set; }
        public String FechaRefrendo { get; set; }
        public String FechaCaptura { get; set; }
        public String Tipo { get; set; }

        public cRefrendo()
        {

        }

        public static ObservableCollection<cRefrendo> ObtenerRefrendos(String IdPrestamo)
        {
            ObservableCollection<cRefrendo> refrendos = new ObservableCollection<cRefrendo>();
            cRefrendo refrendo;

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM Refrendos " +
                        "WHERE IdPrestamo = @IdPrestamo " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                refrendo = new cRefrendo();
                                refrendo.IdRefrendo = reader["IdRefrendo"].ToString();
                                refrendo.IdPrestamo = reader["IdPrestamo"].ToString();
                                refrendo.Refrendo = reader["Refrendo"].ToString();
                                refrendo.FechaRefrendo = reader["FechaRefrendo"].ToString();
                                refrendo.FechaCaptura = reader["FechaCaptura"].ToString();
                                refrendo.Tipo = reader["Tipo"].ToString();
                                refrendos.Add(refrendo);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                refrendos = null;
            }

            return refrendos;
        }

        public static String AgregarRefrendo(cRefrendo c)
        {
            String resultado = "OK";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Refrendos (IdPrestamo, Refrendo, FechaRefrendo, FechaCaptura, Tipo) " +
                        "OUTPUT INSERTED.IdRefrendo " +
                        "VALUES (@IdPrestamo, @Refrendo, @FechaRefrendo, GETDATE(), @Tipo) " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrestamo", c.IdPrestamo);
                        myCMD.Parameters.AddWithValue("@Refrendo", c.Refrendo);
                        myCMD.Parameters.AddWithValue("@FechaRefrendo", c.FechaRefrendo);
                        myCMD.Parameters.AddWithValue("@Tipo", c.Tipo);

                        resultado = myCMD.ExecuteScalar().ToString();
                        
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                resultado = "0";
            }

            return resultado;
        }
    }
}
