﻿using System;
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
        public String Contrato { get; set; }
        public String NombreCliente { get; set; }
        public String NombrePrenda { get; set; }
        public String CantidadPrestada { get; set; }
        public String FechaPrestamo { get; set; }
        public String FechaVencimiento { get; set; }
        public String Estado { get; set; }

        public cPrestamo()
        {

        }

        public static ObservableCollection<cPrestamo> ObtenerPrestamos()
        {
            ObservableCollection<cPrestamo> prestamos = new ObservableCollection<cPrestamo>();
            cPrestamo prestamo;

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Prestamos.*, (Clientes.NombreCliente + ' ' + Clientes.ApellidoMaternoCliente + ' ' + Clientes.ApellidoMaternoCliente) As NombreCompleto," +
                        "Prendas.Descripcion " +
                        "FROM Prestamos " +
                        "INNER JOIN Clientes " +
                        "ON Clientes.IdCliente = Prestamos.IdCliente " +
                        "INNER JOIN Prendas " +
                        "ON Prendas.IdPrenda = Prestamos.IdPrenda " +
                        "", con))
                    {
                        con.Open();

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
                                prestamo.Contrato = reader["Contrato"].ToString();
                                prestamo.NombreCliente = reader["NombreCompleto"].ToString();
                                prestamo.NombrePrenda = reader["Descripcion"].ToString();
                                prestamo.CantidadPrestada = reader["CantidadPrestada"].ToString();
                                prestamo.FechaPrestamo = reader["FechaPrestamo"].ToString();
                                prestamo.FechaVencimiento = reader["FechaVencimiento"].ToString();
                                prestamo.Estado = reader["Estado"].ToString();
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
                        "INSERT INTO Prestamos (IdPrestamoPadre, IdCliente, IdPrenda, Contrato, CantidadPrestada, FechaPrestamo, FechaVencimiento, Estado) " +
                        "OUTPUT INSERTED.IdPrestamo " +
                        "VALUES (@IdPrestamoPadre, @IdCliente, @IdPrenda, @Contrato, @CantidadPrestada, @FechaPrestamo, @FechaVencimiento, @Estado)" +
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
    }
}
