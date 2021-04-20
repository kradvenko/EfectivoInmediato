using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cCliente
    {
        public String IdCliente { get; set; }
        public String NombreCliente { get; set; }
        public String ApellidoPaternoCliente { get; set; }
        public String ApellidoMaternoCliente { get; set; }
        public String NombreCompleto { get; set; }
        public String TipoIdentificacion { get; set; }
        public String ClaveIdentificacion { get; set; }
        public String Domicilio { get; set; }        
        public String Colonia { get; set; }        
        public String Ciudad { get; set; }
        public String Estado { get; set; }
        public String Telefono1 { get; set; }
        public String Telefono2 { get; set; }
        public String CorreoElectronico { get; set; }
        public String FechaNacimiento { get; set; }
        public String Ocupacion { get; set; }
        public String NombreCotitular { get; set; }
        public String DomicilioCotitular { get; set; }
        public String RutaImagenFrente { get; set; }
        public String RutaImagenAtras { get; set; }

        public cCliente()
        {

        }

        public static ObservableCollection<cCliente> ObtenerClientes()
        {
            ObservableCollection<cCliente> clientes = new ObservableCollection<cCliente>();
            cCliente cliente;

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Clientes.*, (NombreCliente + ' ' + ApellidoPaternoCliente + ' ' + ApellidoMaternoCliente) As NombreCompleto " +
                        "FROM Clientes " +
                        "ORDER BY ApellidoPaternoCliente, ApellidoMaternoCliente " +
                        "", con))
                    {
                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cliente = new cCliente();
                                cliente.IdCliente = reader["IdCliente"].ToString();
                                cliente.NombreCliente = reader["NombreCliente"].ToString();
                                cliente.ApellidoPaternoCliente = reader["ApellidoPaternoCliente"].ToString();
                                cliente.ApellidoMaternoCliente = reader["ApellidoMaternoCliente"].ToString();
                                cliente.NombreCompleto = reader["NombreCompleto"].ToString();
                                cliente.TipoIdentificacion = reader["TipoIdentificacion"].ToString();
                                cliente.ClaveIdentificacion = reader["ClaveIdentificacion"].ToString();
                                cliente.Domicilio = reader["Domicilio"].ToString();
                                cliente.Colonia = reader["Colonia"].ToString();
                                cliente.Ciudad = reader["Ciudad"].ToString();
                                cliente.Estado = reader["Estado"].ToString();
                                cliente.Telefono1 = reader["Telefono1"].ToString();
                                cliente.Telefono2 = reader["Telefono2"].ToString();
                                cliente.CorreoElectronico = reader["CorreoElectronico"].ToString();
                                cliente.FechaNacimiento = reader["FechaNacimiento"].ToString();
                                cliente.Ocupacion = reader["Ocupacion"].ToString();
                                cliente.NombreCotitular = reader["NombreCotitular"].ToString();
                                cliente.DomicilioCotitular = reader["DomicilioCotitular"].ToString();
                                cliente.RutaImagenFrente = reader["RutaImagenFrente"].ToString();
                                cliente.RutaImagenAtras = reader["RutaImagenAtras"].ToString();
                                clientes.Add(cliente);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return clientes;
        }

        public static String GuardarCliente(String nombreCliente, String apellidoPaternoCliente, String apellidoMaternoCliente, String identificacion, String claveIdentificacion, String domicilio, String colonia, String ciudad, String estado, String telefono1, String telefono2, String correoElectronico, String fechaNacimiento, String ocupacion, String nombreCotitular, String domicilioCotitular)
        {
            String resultado = "0";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {   
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Clientes (NombreCliente, ApellidoPaternoCliente, ApellidoMaternoCliente, TipoIdentificacion, ClaveIdentificacion, Domicilio, Colonia, Ciudad, Estado, Telefono1, Telefono2, CorreoElectronico, FechaNacimiento, Ocupacion, NombreCotitular, DomicilioCotitular) " +
                        "OUTPUT INSERTED.IdCliente " +
                        "VALUES (@NombreCliente, @ApellidoPaternoCliente, @ApellidoMaternoCliente, @TipoIdentificacion, @ClaveIdentificacion, @Domicilio, @Colonia, @Ciudad, @Estado, @Telefono1, @Telefono2, @CorreoElectronico, @FechaNacimiento, @Ocupacion, @NombreCotitular, @DomicilioCotitular)" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                        myCMD.Parameters.AddWithValue("@ApellidoPaternoCliente", apellidoPaternoCliente);
                        myCMD.Parameters.AddWithValue("@ApellidoMaternoCliente", apellidoMaternoCliente);
                        myCMD.Parameters.AddWithValue("@TipoIdentificacion", identificacion);
                        myCMD.Parameters.AddWithValue("@ClaveIdentificacion", claveIdentificacion);
                        myCMD.Parameters.AddWithValue("@Domicilio", domicilio);
                        myCMD.Parameters.AddWithValue("@Colonia", colonia);
                        myCMD.Parameters.AddWithValue("@Ciudad", ciudad);
                        myCMD.Parameters.AddWithValue("@Estado", estado);
                        myCMD.Parameters.AddWithValue("@Telefono1", telefono1);
                        myCMD.Parameters.AddWithValue("@Telefono2", telefono2);
                        myCMD.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                        myCMD.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        myCMD.Parameters.AddWithValue("@Ocupacion", ocupacion);
                        myCMD.Parameters.AddWithValue("@NombreCotitular", nombreCotitular);
                        myCMD.Parameters.AddWithValue("@DomicilioCotitular", domicilioCotitular);

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

        public static cCliente ObtenerClienteIdPrestamo(String IdPrestamo)
        {
            cCliente cliente = new cCliente();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Clientes.*, (NombreCliente + ' ' + ApellidoPaternoCliente + ' ' + ApellidoMaternoCliente) As NombreCompleto " +
                        "FROM Clientes " +
                        "INNER JOIN Prestamos " +
                        "ON Prestamos.IdCliente = Clientes.IdCliente " +
                        "WHERE Prestamos.IdPrestamo = @IdPrestamo " +
                        "", con))
                    {
                        myCMD.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);

                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cliente = new cCliente();
                                cliente.IdCliente = reader["IdCliente"].ToString();
                                cliente.NombreCliente = reader["NombreCliente"].ToString();
                                cliente.ApellidoPaternoCliente = reader["ApellidoPaternoCliente"].ToString();
                                cliente.ApellidoMaternoCliente = reader["ApellidoMaternoCliente"].ToString();
                                cliente.NombreCompleto = reader["NombreCompleto"].ToString();
                                cliente.TipoIdentificacion = reader["TipoIdentificacion"].ToString();
                                cliente.ClaveIdentificacion = reader["ClaveIdentificacion"].ToString();
                                cliente.Domicilio = reader["Domicilio"].ToString();
                                cliente.Colonia = reader["Colonia"].ToString();
                                cliente.Ciudad = reader["Ciudad"].ToString();
                                cliente.Estado = reader["Estado"].ToString();
                                cliente.Telefono1 = reader["Telefono1"].ToString();
                                cliente.Telefono2 = reader["Telefono2"].ToString();
                                cliente.CorreoElectronico = reader["CorreoElectronico"].ToString();
                                cliente.FechaNacimiento = reader["FechaNacimiento"].ToString();
                                cliente.Ocupacion = reader["Ocupacion"].ToString();
                                cliente.NombreCotitular = reader["NombreCotitular"].ToString();
                                cliente.DomicilioCotitular = reader["DomicilioCotitular"].ToString();
                                cliente.RutaImagenFrente = reader["RutaImagenFrente"].ToString();
                                cliente.RutaImagenAtras = reader["RutaImagenAtras"].ToString();
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return cliente;
        }

        public static String ActualizarCliente(String IdCliente ,String nombreCliente, String apellidoPaternoCliente, String apellidoMaternoCliente, String identificacion, String claveIdentificacion, String domicilio, String colonia, String ciudad, String estado, String telefono1, String telefono2, String correoElectronico, String fechaNacimiento, String ocupacion, String nombreCotitular, String domicilioCotitular)
        {
            String resultado = "OK";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Clientes " +
                        "SET NombreCliente = @NombreCliente, ApellidoPaternoCliente = @ApellidoPaternoCliente, " +
                        "ApellidoMaternoCliente = @ApellidoMaternoCliente, TipoIdentificacion = @TipoIdentificacion, " +
                        "ClaveIdentificacion = @ClaveIdentificacion, Domicilio= @Domicilio, Colonia = @Colonia, Ciudad = @Ciudad, " +
                        "Estado = @Estado, Telefono1 = @Telefono1, Telefono2 = @Telefono2, CorreoElectronico = @CorreoElectronico, " +
                        "FechaNacimiento = @FechaNacimiento, Ocupacion = @Ocupacion, NombreCotitular = @NombreCotitular, DomicilioCotitular = @DomicilioCotitular " +
                        "WHERE IdCliente = @IdCliente " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                        myCMD.Parameters.AddWithValue("@ApellidoPaternoCliente", apellidoPaternoCliente);
                        myCMD.Parameters.AddWithValue("@ApellidoMaternoCliente", apellidoMaternoCliente);
                        myCMD.Parameters.AddWithValue("@TipoIdentificacion", identificacion);
                        myCMD.Parameters.AddWithValue("@ClaveIdentificacion", claveIdentificacion);
                        myCMD.Parameters.AddWithValue("@Domicilio", domicilio);
                        myCMD.Parameters.AddWithValue("@Colonia", colonia);
                        myCMD.Parameters.AddWithValue("@Ciudad", ciudad);
                        myCMD.Parameters.AddWithValue("@Estado", estado);
                        myCMD.Parameters.AddWithValue("@Telefono1", telefono1);
                        myCMD.Parameters.AddWithValue("@Telefono2", telefono2);
                        myCMD.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                        myCMD.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        myCMD.Parameters.AddWithValue("@Ocupacion", ocupacion);
                        myCMD.Parameters.AddWithValue("@NombreCotitular", nombreCotitular);
                        myCMD.Parameters.AddWithValue("@DomicilioCotitular", domicilioCotitular);
                        myCMD.Parameters.AddWithValue("@IdCliente", IdCliente);

                        myCMD.ExecuteNonQuery();

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

        public static cCliente ObtenerClienteId(String IdCliente)
        {
            cCliente cliente = new cCliente();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Clientes.* " +
                        "FROM Clientes " +
                        "WHERE Clientes.IdCliente = @IdCliente " +
                        "", con))
                    {
                        myCMD.Parameters.AddWithValue("@IdCliente", IdCliente);

                        con.Open();

                        SqlDataReader reader = myCMD.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cliente = new cCliente();
                                cliente.IdCliente = reader["IdCliente"].ToString();
                                cliente.NombreCliente = reader["NombreCliente"].ToString();
                                cliente.ApellidoPaternoCliente = reader["ApellidoPaternoCliente"].ToString();
                                cliente.ApellidoMaternoCliente = reader["ApellidoMaternoCliente"].ToString();
                                cliente.NombreCompleto = reader["NombreCompleto"].ToString();
                                cliente.TipoIdentificacion = reader["TipoIdentificacion"].ToString();
                                cliente.ClaveIdentificacion = reader["ClaveIdentificacion"].ToString();
                                cliente.Domicilio = reader["Domicilio"].ToString();
                                cliente.Colonia = reader["Colonia"].ToString();
                                cliente.Ciudad = reader["Ciudad"].ToString();
                                cliente.Estado = reader["Estado"].ToString();
                                cliente.Telefono1 = reader["Telefono1"].ToString();
                                cliente.Telefono2 = reader["Telefono2"].ToString();
                                cliente.CorreoElectronico = reader["CorreoElectronico"].ToString();
                                cliente.FechaNacimiento = reader["FechaNacimiento"].ToString();
                                cliente.Ocupacion = reader["Ocupacion"].ToString();
                                cliente.NombreCotitular = reader["NombreCotitular"].ToString();
                                cliente.DomicilioCotitular = reader["DomicilioCotitular"].ToString();
                                cliente.RutaImagenFrente = reader["RutaImagenFrente"].ToString();
                                cliente.RutaImagenAtras = reader["RutaImagenAtras"].ToString();
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return cliente;
        }

        public static String ActualizarImagenFrenteCliente(String IdCliente, String RutaImagenFrente)
        {
            String resultado = "OK";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Clientes " +
                        "SET RutaImagenFrente = @RutaImagenFrente " +
                        "WHERE IdCliente = @IdCliente " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@RutaImagenFrente", RutaImagenFrente);
                        myCMD.Parameters.AddWithValue("@IdCliente", IdCliente);

                        myCMD.ExecuteNonQuery();

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

        public static String ActualizarImagenAtrasCliente(String IdCliente, String RutaImagenAtras)
        {
            String resultado = "OK";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Clientes " +
                        "SET RutaImagenAtras = @RutaImagenAtras " +
                        "WHERE IdCliente = @IdCliente " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@RutaImagenAtras", RutaImagenAtras);
                        myCMD.Parameters.AddWithValue("@IdCliente", IdCliente);

                        myCMD.ExecuteNonQuery();

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
