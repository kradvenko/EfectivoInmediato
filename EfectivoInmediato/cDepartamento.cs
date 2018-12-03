using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cDepartamento
    {
        public String IdDepartamento { get; set; }
        public String Departamento { get; set; }

        public cDepartamento()
        {

        }

        public static ObservableCollection<cDepartamento> ObtenerDepartamentos()
        {
            ObservableCollection<cDepartamento> departamentos = new ObservableCollection<cDepartamento>();
            cDepartamento departamento;

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM Departamentos " +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                departamento = new cDepartamento();
                                departamento.IdDepartamento = reader["IdDepartamento"].ToString();
                                departamento.Departamento = reader["Departamento"].ToString();
                                departamentos.Add(departamento);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                departamentos = null;
            }

            return departamentos;
        }

        public static String AgregarDepartamento(String Nombre)
        {
            String resultado = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Departamentos (Departamento, Estatus) " +
                        "VALUES ('" + Nombre + "', 'ACTIVO') " +
                        "", con))
                    {
                        con.Open();

                        int rows = myCMD.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            resultado = "Se ha ingresado el departamento.";
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
