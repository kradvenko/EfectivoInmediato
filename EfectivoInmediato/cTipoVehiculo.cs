using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cTipoVehiculo
    {
        public String IdTipoVehiculo { get; set; }
        public String Tipo { get; set; }

        public cTipoVehiculo()
        {

        }

        public static ObservableCollection<cTipoVehiculo> ObtenerTiposVehiculo()
        {
            ObservableCollection<cTipoVehiculo> tipos = new ObservableCollection<cTipoVehiculo>();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM TiposVehiculo " +
                        "ORDER BY Tipo " +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cTipoVehiculo tipo = new cTipoVehiculo();
                                tipo.IdTipoVehiculo = reader["IdTipoVehiculo"].ToString();
                                tipo.Tipo = reader["Tipo"].ToString();
                                tipos.Add(tipo);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                tipos = null;
            }

            return tipos;
        }

        public static String AgregarTipoVehiculo(String Tipo)
        {
            String resultado = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(" " +
                        "INSERT INTO TiposVehiculo (Tipo) " +
                        "VALUES (@Tipo) " +
                        "", con))
                    {
                        con.Open();

                        comm.Parameters.AddWithValue("@Tipo", Tipo);

                        int rows = comm.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            resultado = "Se ha ingresado el tipo de vehículo.";
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
