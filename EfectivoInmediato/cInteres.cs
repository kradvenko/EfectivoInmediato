using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cInteres
    {
        public String IdInteres { get; set; }
        public String IdDepartamento { get; set; }
        public String NombreDepartamento { get; set; }
        public String Periodo { get; set; }
        public String Plazo { get; set; }
        public String Financiamiento { get; set; }
        public String Almacenaje { get; set; }
        public String Administracion { get; set; }
        public String IVA { get; set; }
        public String PagoMinimo { get; set; }
        public String DiasDeGracia { get; set; }
        public String ReclamoAnticipadoMetodo { get; set; }
        public String ReclamoAnticipadoCantidad { get; set; }
        public String ReclamoAnticipadoDias { get; set; }
        public String ReclamoExtemporaneoMetodo { get; set; }
        public String ReclamoExtemporaneoCantidad { get; set; }
        public String ReclamoExtemporaneoDias { get; set; }

        public cInteres()
        {

        }

        public static cInteres ObtenerInteresDepartamento(String IdDepartamento)
        {
            cInteres interes = new cInteres();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM Intereses " +
                        "WHERE IdDepartamento = " + IdDepartamento +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                interes.IdInteres = reader["IdInteres"].ToString();
                                interes.IdDepartamento = reader["IdDepartamento"].ToString();
                                interes.Periodo = reader["Periodo"].ToString();
                                interes.Plazo = reader["Plazo"].ToString();
                                interes.Financiamiento = reader["Financiamiento"].ToString();
                                interes.Almacenaje = reader["Almacenaje"].ToString();
                                interes.Administracion = reader["Administracion"].ToString();
                                interes.IVA = reader["IVA"].ToString();
                                interes.PagoMinimo = reader["PagoMinimo"].ToString();
                                interes.DiasDeGracia = reader["DiasDeGracia"].ToString();
                                interes.ReclamoAnticipadoMetodo = reader["ReclamoAnticipadoMetodo"].ToString();
                                interes.ReclamoAnticipadoCantidad = reader["ReclamoAnticipadoCantidad"].ToString();
                                interes.ReclamoAnticipadoDias = reader["ReclamoAnticipadoDias"].ToString();
                                interes.ReclamoExtemporaneoMetodo = reader["ReclamoExtemporaneoMetodo"].ToString();
                                interes.ReclamoExtemporaneoCantidad = reader["ReclamoExtemporaneoCantidad"].ToString();
                                interes.ReclamoExtemporaneoDias = reader["ReclamoExtemporaneoDias"].ToString();
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                
            }

            return interes;
        }

        public static String GuardarInteres(String IdDepartamento, cInteres Interes)
        {
            String IdInteres = "0";
            bool IsNew = true;

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT *" +
                        "FROM Intereses" +
                        "WHERE IdDepartamento =  " + IdDepartamento +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            IdInteres = reader["IdInteres"].ToString();
                            IsNew = false;
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                IdInteres = exc.Message;
            }

            if (IsNew)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                    {
                        using (SqlCommand myCMD = new SqlCommand(" " +
                            "INSERT INTO Intereses " +
                            "(IdDepartamento, Periodo, Plazo, Financiamiento, Almacenaje, Administracion, IVA, PagoMinimo, DiasDeGracia, ReclamoAnticipadoMetodo, ReclamoAnticipadoCantidad, ReclamoAnticipadoDias, ReclamoExtemporaneoMetodo, ReclamoExtemporaneoCantidad, ReclamoExtemporaneoDias)  " +
                            " OUTPUT Inserted.IdDepartamento " +
                            "VALUES (@IdDepartamento, @Periodo, @Plazo, @Financiamiento, @Almacenaje, @Administracion, @IVA, @PagoMinimo, @DiasDeGracia, @ReclamoAnticipadoMetodo, @ReclamoAnticipadoCantidad, @ReclamoAnticipadoDias, @ReclamoExtemporaneoMetodo, @ReclamoExtemporaneoCantidad, @ReclamoExtemporaneoDias) " +
                            "", con))
                        {
                            con.Open();

                            myCMD.Parameters.AddWithValue("@IdDepartamento", Interes.IdDepartamento);
                            myCMD.Parameters.AddWithValue("@Periodo", Interes.Periodo);
                            myCMD.Parameters.AddWithValue("@Plazo", Interes.Plazo);
                            myCMD.Parameters.AddWithValue("@Financiamiento", Interes.Financiamiento);
                            myCMD.Parameters.AddWithValue("@Almacenaje", Interes.Almacenaje);
                            myCMD.Parameters.AddWithValue("@Administracion", Interes.Administracion);
                            myCMD.Parameters.AddWithValue("@IVA", Interes.IVA);
                            myCMD.Parameters.AddWithValue("@PagoMinimo", Interes.PagoMinimo);
                            myCMD.Parameters.AddWithValue("@DiasDeGracia", Interes.DiasDeGracia);
                            myCMD.Parameters.AddWithValue("@ReclamoAnticipadoMetodo", Interes.ReclamoAnticipadoMetodo);
                            myCMD.Parameters.AddWithValue("@ReclamoAnticipadoCantidad", Interes.ReclamoAnticipadoCantidad);
                            myCMD.Parameters.AddWithValue("@ReclamoAnticipadoDias", Interes.ReclamoAnticipadoDias);
                            myCMD.Parameters.AddWithValue("@ReclamoExtemporaneoMetodo", Interes.ReclamoExtemporaneoMetodo);
                            myCMD.Parameters.AddWithValue("@ReclamoExtemporaneoCantidad", Interes.ReclamoExtemporaneoCantidad);
                            myCMD.Parameters.AddWithValue("@ReclamoExtemporaneoDias", Interes.ReclamoExtemporaneoDias);

                            IdInteres = myCMD.ExecuteScalar().ToString();

                            con.Close();
                        }
                    }
                }
                catch (Exception exc)
                {
                    IdInteres = exc.Message;
                }
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                    {
                        using (SqlCommand myCMD = new SqlCommand(" " +
                            "UPDATE Intereses " +
                            "SET IdDepartamento = @IdDepartamento, Periodo = @Periodo, Plazo = @Plazo, Financiamiento = @Financiamiento, Almacenaje = @Almacenaje, Administracion = @Administracion, IVA = @IVA, PagoMinimo = @PagoMinimo, DiasDeGracia = @DiasDeGracia, ReclamoAnticipadoMetodo = @ReclamoAnticipadoMetodo, ReclamoAnticipadoCantidad = @ReclamoAnticipadoCantidad, ReclamoAnticipadoDias = @ReclamoAnticipadoDias, ReclamoExtemporaneoMetodo = @ReclamoExtemporaneoMetodo, ReclamoExtemporaneoCantidad = @ReclamoExtemporaneoCantidad, ReclamoExtemporaneoDias = @ReclamoExtemporaneoDias)  " +
                            "WHERE IdInteres = @IdInteres " +
                            "", con))
                        {
                            con.Open();

                            myCMD.Parameters.AddWithValue("@IdInteres", IdInteres);
                            myCMD.Parameters.AddWithValue("@IdDepartamento", Interes.IdDepartamento);
                            myCMD.Parameters.AddWithValue("@Periodo", Interes.Periodo);
                            myCMD.Parameters.AddWithValue("@Plazo", Interes.Plazo);
                            myCMD.Parameters.AddWithValue("@Financiamiento", Interes.Financiamiento);
                            myCMD.Parameters.AddWithValue("@Almacenaje", Interes.Almacenaje);
                            myCMD.Parameters.AddWithValue("@Administracion", Interes.Administracion);
                            myCMD.Parameters.AddWithValue("@IVA", Interes.IVA);
                            myCMD.Parameters.AddWithValue("@PagoMinimo", Interes.PagoMinimo);
                            myCMD.Parameters.AddWithValue("@DiasDeGracia", Interes.DiasDeGracia);
                            myCMD.Parameters.AddWithValue("@ReclamoAnticipadoMetodo", Interes.ReclamoAnticipadoMetodo);
                            myCMD.Parameters.AddWithValue("@ReclamoAnticipadoCantidad", Interes.ReclamoAnticipadoCantidad);
                            myCMD.Parameters.AddWithValue("@ReclamoAnticipadoDias", Interes.ReclamoAnticipadoDias);
                            myCMD.Parameters.AddWithValue("@ReclamoExtemporaneoMetodo", Interes.ReclamoExtemporaneoMetodo);
                            myCMD.Parameters.AddWithValue("@ReclamoExtemporaneoCantidad", Interes.ReclamoExtemporaneoCantidad);
                            myCMD.Parameters.AddWithValue("@ReclamoExtemporaneoDias", Interes.ReclamoExtemporaneoDias);

                            myCMD.ExecuteNonQuery();

                            con.Close();
                        }
                    }
                }
                catch (Exception exc)
                {
                    IdInteres = exc.Message;
                }
            }

            return IdInteres;
        }
    }
}
