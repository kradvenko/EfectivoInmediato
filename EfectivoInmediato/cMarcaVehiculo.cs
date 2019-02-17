using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cMarcaVehiculo
    {
        public String IdMarcaVehiculo { get; set; }
        public String Marca { get; set; }

        public cMarcaVehiculo()
        {

        }

        public static ObservableCollection<cMarcaVehiculo> ObtenerMarcasVehiculo()
        {
            ObservableCollection<cMarcaVehiculo> marcas = new ObservableCollection<cMarcaVehiculo>();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM MarcasVehiculo " +
                        "ORDER BY Marca " +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cMarcaVehiculo marca = new cMarcaVehiculo();
                                marca.IdMarcaVehiculo = reader["IdMarcaVehiculo"].ToString();
                                marca.Marca = reader["Marca"].ToString();
                                marcas.Add(marca);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                marcas = null;
            }

            return marcas;
        }

        public static String AgregarMarcaVehiculo(String Marca)
        {
            String resultado = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(" " +
                        "INSERT INTO MarcasVehiculo (Marca) " +
                        "VALUES (@Marca) " +
                        "", con))
                    {
                        con.Open();

                        comm.Parameters.AddWithValue("@Marca", Marca);

                        int rows = comm.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            resultado = "Se ha ingresado la marca del vehículo.";
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
