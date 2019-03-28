using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cCategoria
    {
        public String IdCategoria { get; set; }
        public String Categoria { get; set; }

        public cCategoria()
        {

        }

        public static String AgregarCategoriaArticulo(cCategoria c)
        {
            String resultado = "0";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO CategoriasArticulo (Categoria) " +
                        "OUTPUT INSERTED.IdCategoria " +
                        "VALUES (@Categoria)" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@Categoria", c.Categoria);

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

        public static ObservableCollection<cCategoria> ObtenerCategorias()
        {
            ObservableCollection<cCategoria> categorias = new ObservableCollection<cCategoria>();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM CategoriasArticulo " +
                        "ORDER BY Categoria " +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cCategoria c = new cCategoria();
                                c.IdCategoria = reader["IdCategoria"].ToString();
                                c.Categoria = reader["Categoria"].ToString();
                                categorias.Add(c);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return categorias;
        }
    }
}
