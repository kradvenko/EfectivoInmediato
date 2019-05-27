using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public String TipoPrenda { get; set; }
        public String DescripcionPrenda { get; set; }
        public String Contrato { get; set; }
        public String NombreCliente { get; set; }
        public String NombrePrenda { get; set; }
        public String CantidadPrestada { get; set; }
        public String FechaPrestamo { get; set; }
        public String FechaVencimiento { get; set; }
        public String Estado { get; set; }
        public String FechaCaptura { get; set; }
        //Variable para fecha próxima de terminación de préstamo
        public String ProximoATerminar { get; set; }

        public cPrestamo()
        {

        }

        public static ObservableCollection<cPrestamo> ObtenerPrestamos(String Estado, String DiasVencimiento = "NO")
        {
            ObservableCollection<cPrestamo> prestamos = new ObservableCollection<cPrestamo>();
            cPrestamo prestamo;

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Prestamos.*, (Clientes.NombreCliente + ' ' + Clientes.ApellidoPaternoCliente + ' ' + Clientes.ApellidoMaternoCliente) As NombreCompleto," +
                        "Prendas.Descripcion, Prendas.TipoPrenda " +
                        "FROM Prestamos " +
                        "INNER JOIN Clientes " +
                        "ON Clientes.IdCliente = Prestamos.IdCliente " +
                        "INNER JOIN Prendas " +
                        "ON Prendas.IdPrenda = Prestamos.IdPrenda " +
                        "WHERE Prestamos.Estado Like @Estado " +
                        "AND Prendas.EnVenta = 'NO'" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@Estado", Estado);

                        SqlDataReader reader = myCMD.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                prestamo = new cPrestamo();
                                prestamo.IdPrestamo = reader["IdPrestamo"].ToString();
                                prestamo.IdPrestamoPadre = reader["IdPrestamoPadre"].ToString();
                                prestamo.IdCliente = reader["IdCliente"].ToString();
                                prestamo.IdPrenda = reader["IdPrenda"].ToString();
                                prestamo.DescripcionPrenda = reader["Descripcion"].ToString();
                                prestamo.TipoPrenda = reader["TipoPrenda"].ToString();
                                prestamo.Contrato = reader["Contrato"].ToString();
                                prestamo.NombreCliente = reader["NombreCompleto"].ToString();
                                prestamo.NombrePrenda = reader["Descripcion"].ToString();
                                prestamo.CantidadPrestada = reader["CantidadPrestada"].ToString();
                                prestamo.FechaPrestamo = reader["FechaPrestamo"].ToString();
                                prestamo.FechaVencimiento = reader["FechaVencimiento"].ToString();
                                prestamo.Estado = reader["Estado"].ToString();
                                prestamo.FechaCaptura = reader["FechaCaptura"].ToString();

                                SqlConnection con2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString);

                                con2.Open();

                                using (SqlCommand commRefrendos = new SqlCommand(" " +
                                    "SELECT * " +
                                    "FROM Refrendos " +
                                    "WHERE IdPrestamo = @IdPrestamo " +
                                    "", con2))
                                {
                                    commRefrendos.Parameters.AddWithValue("@IdPrestamo", prestamo.IdPrestamo);

                                    SqlDataReader readerR = commRefrendos.ExecuteReader();

                                    if (readerR.HasRows)
                                    {
                                        String fecha = "";

                                        while (readerR.Read())
                                        {
                                            fecha = readerR["FechaRefrendo"].ToString();
                                        }

                                        con2.Close();

                                        SqlCommand comIntereses = new SqlCommand("Select Intereses.Periodo, Plazo " +
                                                                            "From Prendas " +
                                                                            "Inner Join Departamentos " +
                                                                            "On Departamentos.IdDepartamento = Prendas.IdDepartamento " +
                                                                            "Inner Join Intereses " +
                                                                            "On Intereses.IdDepartamento = Departamentos.IdDepartamento " +
                                                                            "Where IdPrenda = @IdPrenda", con2);

                                        con2.Open();

                                        comIntereses.Parameters.AddWithValue("@IdPrenda", prestamo.IdPrenda);

                                        SqlDataReader readerI = comIntereses.ExecuteReader();

                                        String plazo = "";

                                        if (readerI.HasRows)
                                        {
                                            while (readerI.Read())
                                            {
                                                plazo = readerI["Plazo"].ToString();
                                            }
                                        }

                                        DateTime fechaVencimiento = DateTime.Parse(fecha);
                                        fechaVencimiento = fechaVencimiento.AddMonths(int.Parse(plazo));

                                        prestamo.FechaVencimiento = fechaVencimiento.ToShortDateString();

                                       con2.Close();
                                    }
                                }

                                if (DiasVencimiento == "NO")
                                {

                                }
                                else
                                {
                                    if (DateTime.Parse(prestamo.FechaVencimiento).AddDays(int.Parse(DiasVencimiento)) <= DateTime.Now)
                                    {
                                        prestamo.ProximoATerminar = "SI";
                                    }
                                    else
                                    {
                                        prestamo.ProximoATerminar = "NO";
                                    }
                                }

                                prestamos.Add(prestamo);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return prestamos;
        }

        public static String AgregarPrestamo(cPrestamo p)
        {
            String respuesta = "0";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Prestamos (IdPrestamoPadre, IdCliente, IdPrenda, Contrato, CantidadPrestada, FechaPrestamo, FechaVencimiento, Estado, FechaCaptura) " +
                        "OUTPUT INSERTED.IdPrestamo " +
                        "VALUES (@IdPrestamoPadre, @IdCliente, @IdPrenda, @Contrato, @CantidadPrestada, @FechaPrestamo, @FechaVencimiento, @Estado, GETDATE())" +
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

        public static String FinalizarPrestamo(String IdPrestamo)
        {
            String respuesta = "OK";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Prestamos SET Estado = 'LIQUIDADO' " +
                        "WHERE IdPrestamo = @IdPrestamo" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);

                        myCMD.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                respuesta = exc.Message;
            }

            return respuesta;
        }
    }
}
