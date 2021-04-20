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
            String resultado = "OK";
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "IF (NOT EXISTS (SELECT * " +
                        "FROM INFORMATION_SCHEMA.TABLES " +
                        "WHERE TABLE_SCHEMA = 'EfectivoInmediato' " +
                        "AND  TABLE_NAME = 'Ventas')) " +
                            "BEGIN " +
                                "CREATE TABLE[dbo].[Ventas]( " +
                                "[IdVenta][int] IDENTITY(1, 1) NOT NULL, " +
                                "[IdPrenda][int] NULL, " +
                                "[Descuento][float] NULL, " +
                                "[Subtotal][float] NULL, " +
                                "[Total][float] NULL, " + 
                                "[HoraVenta][nvarchar](50) NULL, " +
                                "[FechaVenta][nvarchar](50) NULL, " +
                                "[Estado][nvarchar](50) NULL, " +
                                "CONSTRAINT[PK_Ventas] PRIMARY KEY CLUSTERED " +
                                "( " +
                                "[IdVenta] ASC " + 
                                ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                                ") ON[PRIMARY] " +
                                "ALTER TABLE Prendas ADD EnVenta NVARCHAR(2), Vendida NVARCHAR(2), PrecioVenta FLOAT, Enajenado NVARCHAR(2) " +
                            "END " +
                        "ELSE " +
                            "BEGIN " +
                                "SELECT * " +
                                "FROM Ventas " +
                            "END " +
                        "", con))
                    {
                        con.Open();

                        myCMD.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("UPDATE Prendas SET EnVenta = 'NO', Vendida = 'NO', PrecioVenta = 0, Enajenado = 'NO'", con);

                        cmd2.ExecuteNonQuery();

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

        public static String EjecutarCambios2()
        {
            String resultado = "OK";
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "IF (NOT EXISTS (SELECT * " +
                        "FROM INFORMATION_SCHEMA.COLUMNS " +
                        "WHERE TABLE_NAME = 'Prestamos' " +
                        "AND COLUMN_NAME = 'LiquidacionEspecialRazon')) " +
                            "BEGIN " +
                                "ALTER TABLE Prestamos ADD LiquidacionEspecialRazon NVARCHAR(200) " +
                            "END " +
                        "ELSE " +
                            "BEGIN " +
                                "SELECT * " +
                                "FROM Prestamos " +
                            "END " +
                        "", con))
                    {
                        con.Open();

                        myCMD.ExecuteNonQuery();

                        //SqlCommand cmd2 = new SqlCommand("UPDATE Prestamos SET LiquidacionEspecialRazon = ''", con);

                        //cmd2.ExecuteNonQuery();

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

        public static String EjecutarCambios3()
        {
            String resultado = "OK";
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "IF (NOT EXISTS (SELECT * " +
                        "FROM INFORMATION_SCHEMA.COLUMNS " +
                        "WHERE TABLE_NAME = 'Clientes' " +
                        "AND COLUMN_NAME = 'RutaImagenFrente')) " +
                            "BEGIN " +
                                "ALTER TABLE Clientes " +
                                "ADD RutaImagenFrente NVARCHAR(250)," +
                                "RutaImagenAtras NVARCHAR(250) " +
                            "END " +
                        "ELSE " +
                            "BEGIN " +
                                "SELECT * " +
                                "FROM Clientes " +
                            "END " +
                        "", con))
                    {
                        con.Open();

                        myCMD.ExecuteNonQuery();

                        //SqlCommand cmd2 = new SqlCommand("UPDATE Prestamos SET LiquidacionEspecialRazon = ''", con);

                        //cmd2.ExecuteNonQuery();

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
