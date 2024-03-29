﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cPrenda
    {
        public String IdPrenda { get; set; }
        public String IdDepartamento { get; set; }
        public String IdCliente { get; set; }
        public String IdCategoriaArticulo { get; set; }
        public String TipoPrenda { get; set; }
        public String Descripcion { get; set; }
        //Variables para tipo de prenda "Articulos"
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public String Serie { get; set; }
        //Variables para tipo de prenda "Joyas" Tipo Metal
        public String IdTipoMetal { get; set; }
        public String TipoMetal { get; set; }
        public String PesoMetal { get; set; }
        public String Pureza { get; set; }
        public String ObservacionesMetal { get; set; }
        //Variables para tipo de prenda "Joyas" Tipo Piedra
        public String IdTipoPiedra { get; set; }
        public String TipoPiedra { get; set; }
        public String ColorPiedra { get; set; }
        public String ClaridadOPureza { get; set; }
        public String CorteOTalla { get; set; }
        public String PesoPiedra { get; set; }
        public String ObservacionesPiedra { get; set; }
        //Variables para tipo de prenda "Vehículo"
        public String IdTipoVehiculo { get; set; }
        public String IdMarcaVehiculo { get; set; }
        public String ModeloVehiculo { get; set; }
        public String AnioVehiculo { get; set; }
        public String Kilometraje { get; set; }
        public String NumeroSerieVehiculo { get; set; }
        public String Placas { get; set; }
        public String ColorVehiculo { get; set; }
        //Más variables
        public String IdUbicacionAlmacen { get; set; }
        public String UbicacionAlmacen { get; set; }
        public String Observaciones { get; set; }
        public String Avaluo { get; set; }
        public String Prestamo { get; set; }
        public String PrestamoDisplay { get; set; }
        //Para venta
        public String EnVenta { get; set; }
        public String Vendida { get; set; }
        public String PrecioVenta { get; set; }
        public String Enajenado { get; set; }
        public String PrecioVentaDisplay { get; set; }
        //Para inventario
        public String Contrato { get; set; }
        public String Maximo { get; set; }
        public String DePrestamo { get; set; }

        //Imagenes
        public String Imagen1 { get; set; }
        public String Imagen2 { get; set; }
        public String Imagen3 { get; set; }
        public String Imagen4 { get; set; }
        public String Imagen5 { get; set; }
        public String Imagen6 { get; set; }
        public String Imagen7 { get; set; }
        public String Imagen8 { get; set; }
        public String Imagen9 { get; set; }

        public cPrenda()
        {

        }

        public static String GuardarPrenda(String IdDepartamento, String IdCliente, String IdCategoriaArticulo, String TipoPrenda, String Descripcion, String Marca, String Modelo, String Serie, String IdTipoMetal, String PesoMetal, String Pureza, String ObservacionesMetal, String IdTipoPiedra, String ColorPiedra, String ClaridadOPureza, String CorteOTalla, String PesoPiedra, String ObservacionesPiedra, String IdTipoVehiculo, String IdMarcaVehiculo, String ModeloVehiculo, String AnioVehiculo, String Kilometraje, String NumeroSerieVehiculo, String Placas, String ColorVehiculo, String UbicacionAlmacen, String Observaciones, String Avaluo, String Prestamo, String EnVenta)
        {
            String resultado = "0";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Prendas (IdDepartamento, IdCliente, IdCategoriaArticulo, TipoPrenda, Descripcion, Marca, Modelo, Serie, IdTipoMetal, PesoMetal, Pureza, ObservacionesMetal, IdTipoPiedra, ColorPiedra, ClaridadOPureza, CorteOTalla, PesoPiedra, ObservacionesPiedra, IdTipoVehiculo, IdMarcaVehiculo, ModeloVehiculo, AnioVehiculo, Kilometraje, NumeroSerieVehiculo, Placas, ColorVehiculo, UbicacionAlmacen, Observaciones, Avaluo, Prestamo, EnVenta, PrecioVenta, Vendida, Enajenado) " +
                        "OUTPUT INSERTED.IdPrenda " +
                        "VALUES (@IdDepartamento, @IdCliente, @IdCategoriaArticulo, @TipoPrenda, @Descripcion, @Marca, @Modelo, @Serie, @IdTipoMetal, @PesoMetal, @Pureza, @ObservacionesMetal, @IdTipoPiedra, @ColorPiedra, @ClaridadOPureza, @CorteOTalla, @PesoPiedra, @ObservacionesPiedra, @IdTipoVehiculo, @IdMarcaVehiculo, @ModeloVehiculo, @AnioVehiculo, @Kilometraje, @NumeroSerieVehiculo, @Placas, @ColorVehiculo, @UbicacionAlmacen, @Observaciones, @Avaluo, @Prestamo, @EnVenta, @Avaluo, 'NO', 'NO')" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
                        myCMD.Parameters.AddWithValue("@IdCliente", IdCliente);
                        myCMD.Parameters.AddWithValue("@IdCategoriaArticulo", IdCategoriaArticulo);
                        myCMD.Parameters.AddWithValue("@TipoPrenda", TipoPrenda);
                        myCMD.Parameters.AddWithValue("@Descripcion", Descripcion);
                        myCMD.Parameters.AddWithValue("@Marca", Marca);
                        myCMD.Parameters.AddWithValue("@Modelo", Modelo);
                        myCMD.Parameters.AddWithValue("@Serie", Serie);
                        myCMD.Parameters.AddWithValue("@IdTipoMetal", IdTipoMetal);
                        myCMD.Parameters.AddWithValue("@PesoMetal", PesoMetal);
                        myCMD.Parameters.AddWithValue("@Pureza", Pureza);
                        myCMD.Parameters.AddWithValue("@ObservacionesMetal", ObservacionesMetal);
                        myCMD.Parameters.AddWithValue("@IdTipoPiedra", IdTipoPiedra);
                        myCMD.Parameters.AddWithValue("@ColorPiedra", ColorPiedra);
                        myCMD.Parameters.AddWithValue("@ClaridadOPureza", ClaridadOPureza);
                        myCMD.Parameters.AddWithValue("@CorteOTalla", CorteOTalla);
                        myCMD.Parameters.AddWithValue("@PesoPiedra", PesoPiedra);
                        myCMD.Parameters.AddWithValue("@ObservacionesPiedra", ObservacionesPiedra);
                        myCMD.Parameters.AddWithValue("@IdTipoVehiculo", IdTipoVehiculo);
                        myCMD.Parameters.AddWithValue("@IdMarcaVehiculo", IdMarcaVehiculo);
                        myCMD.Parameters.AddWithValue("@ModeloVehiculo", ModeloVehiculo);
                        myCMD.Parameters.AddWithValue("@AnioVehiculo", AnioVehiculo);
                        myCMD.Parameters.AddWithValue("@Kilometraje", Kilometraje);
                        myCMD.Parameters.AddWithValue("@NumeroSerieVehiculo", NumeroSerieVehiculo);
                        myCMD.Parameters.AddWithValue("@Placas", Placas);
                        myCMD.Parameters.AddWithValue("@ColorVehiculo", ColorVehiculo);
                        myCMD.Parameters.AddWithValue("@UbicacionAlmacen", UbicacionAlmacen);
                        myCMD.Parameters.AddWithValue("@Observaciones", Observaciones);
                        myCMD.Parameters.AddWithValue("@Avaluo", Avaluo);
                        myCMD.Parameters.AddWithValue("@Prestamo", Prestamo);
                        myCMD.Parameters.AddWithValue("@EnVenta", EnVenta);

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

        public static cPrenda ObtenerPrendaId(String IdPrenda)
        {
            cPrenda prenda = new cPrenda();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM Prendas " +
                        "WHERE IdPrenda = @IdPrenda " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", IdPrenda);

                        SqlDataReader r = myCMD.ExecuteReader();

                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                prenda.IdPrenda = r["IdPrenda"].ToString();
                                prenda.IdDepartamento = r["IdDepartamento"].ToString();
                                prenda.IdCliente = r["IdCliente"].ToString();
                                prenda.IdCategoriaArticulo = r["IdCategoriaArticulo"].ToString();
                                prenda.TipoPrenda = r["TipoPrenda"].ToString();
                                prenda.Descripcion = r["Descripcion"].ToString();
                                prenda.Marca = r["Marca"].ToString();
                                prenda.Modelo = r["Modelo"].ToString();
                                prenda.Serie = r["Serie"].ToString();
                                prenda.IdTipoMetal = r["IdTipoMetal"].ToString();
                                prenda.PesoMetal = r["PesoMetal"].ToString();
                                prenda.Pureza = r["Pureza"].ToString();
                                prenda.ObservacionesMetal = r["ObservacionesMetal"].ToString();
                                prenda.IdTipoPiedra = r["IdTipoPiedra"].ToString();
                                prenda.ColorPiedra = r["ColorPiedra"].ToString();
                                prenda.ClaridadOPureza = r["ClaridadOPureza"].ToString();
                                prenda.CorteOTalla = r["CorteOTalla"].ToString();
                                prenda.PesoPiedra = r["PesoPiedra"].ToString();
                                prenda.ObservacionesPiedra = r["ObservacionesPiedra"].ToString();
                                prenda.IdTipoVehiculo = r["IdTipoVehiculo"].ToString();
                                prenda.IdMarcaVehiculo = r["IdMarcaVehiculo"].ToString();
                                prenda.ModeloVehiculo = r["ModeloVehiculo"].ToString();
                                prenda.AnioVehiculo = r["AnioVehiculo"].ToString();
                                prenda.Kilometraje = r["Kilometraje"].ToString();
                                prenda.NumeroSerieVehiculo = r["NumeroSerieVehiculo"].ToString();
                                prenda.Placas = r["Placas"].ToString();
                                prenda.ColorVehiculo = r["ColorVehiculo"].ToString();
                                prenda.UbicacionAlmacen = r["UbicacionAlmacen"].ToString();
                                prenda.Observaciones = r["Observaciones"].ToString();
                                prenda.Avaluo = r["Avaluo"].ToString();
                                prenda.Prestamo = r["Prestamo"].ToString();
                                prenda.Enajenado = r["Enajenado"].ToString();
                                prenda.PrestamoDisplay = "$ " + prenda.Prestamo;
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                
            }

            return prenda;
        }

        public static cPrenda ObtenerPrendaIdPrestamo(String IdPrestamo)
        {
            cPrenda prenda = new cPrenda();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Prendas.* " +
                        "FROM Prendas " +
                        "INNER JOIN Prestamos " +
                        "ON Prestamos.IdPrenda = Prendas.IdPrenda " +
                        "WHERE Prestamos.IdPrestamo = @IdPrestamo " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);

                        SqlDataReader r = myCMD.ExecuteReader();

                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                prenda.IdPrenda = r["IdPrenda"].ToString();
                                prenda.IdDepartamento = r["IdDepartamento"].ToString();
                                prenda.IdCliente = r["IdCliente"].ToString();
                                prenda.IdCategoriaArticulo = r["IdCategoriaArticulo"].ToString();
                                prenda.TipoPrenda = r["TipoPrenda"].ToString();
                                prenda.Descripcion = r["Descripcion"].ToString();
                                prenda.Marca = r["Marca"].ToString();
                                prenda.Modelo = r["Modelo"].ToString();
                                prenda.Serie = r["Serie"].ToString();
                                prenda.IdTipoMetal = r["IdTipoMetal"].ToString();
                                prenda.PesoMetal = r["PesoMetal"].ToString();
                                prenda.Pureza = r["Pureza"].ToString();
                                prenda.ObservacionesMetal = r["ObservacionesMetal"].ToString();
                                prenda.IdTipoPiedra = r["IdTipoPiedra"].ToString();
                                prenda.ColorPiedra = r["ColorPiedra"].ToString();
                                prenda.ClaridadOPureza = r["ClaridadOPureza"].ToString();
                                prenda.CorteOTalla = r["CorteOTalla"].ToString();
                                prenda.PesoPiedra = r["PesoPiedra"].ToString();
                                prenda.ObservacionesPiedra = r["ObservacionesPiedra"].ToString();
                                prenda.IdTipoVehiculo = r["IdTipoVehiculo"].ToString();
                                prenda.IdMarcaVehiculo = r["IdMarcaVehiculo"].ToString();
                                prenda.ModeloVehiculo = r["ModeloVehiculo"].ToString();
                                prenda.AnioVehiculo = r["AnioVehiculo"].ToString();
                                prenda.Kilometraje = r["Kilometraje"].ToString();
                                prenda.NumeroSerieVehiculo = r["NumeroSerieVehiculo"].ToString();
                                prenda.Placas = r["Placas"].ToString();
                                prenda.ColorVehiculo = r["ColorVehiculo"].ToString();
                                prenda.UbicacionAlmacen = r["UbicacionAlmacen"].ToString();
                                prenda.Observaciones = r["Observaciones"].ToString();
                                prenda.Avaluo = r["Avaluo"].ToString();
                                prenda.Prestamo = r["Prestamo"].ToString();
                                prenda.Enajenado = r["Enajenado"].ToString();
                                prenda.PrestamoDisplay = "$ " + prenda.Prestamo;
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return prenda;
        }

        public static ObservableCollection<cPrenda> ObtenerPrendasVenta(String Busqueda)
        {
            ObservableCollection<cPrenda> prendas = new ObservableCollection<cPrenda>();
            cPrenda prenda = new cPrenda();

            String BusquedaDesc = "%" + Busqueda + "%";
            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Prendas.*, Prestamos.Contrato, (((Intereses.Financiamiento + Intereses.Almacenaje + Intereses.Administracion) * 1.16) * 2 / 100 * Prendas.Prestamo + Prendas.Prestamo) AS Maximo " +
                        "FROM Prendas " +
                        //"WHERE EnVenta = 'SI' AND Vendida = 'NO'" +
                        "FULL JOIN Prestamos " +
                        "ON Prestamos.IdPrenda = Prendas.IdPrenda " +
                        "INNER JOIN Departamentos " +
                        "ON Departamentos.IdDepartamento = Prendas.IdDepartamento " +
                        "INNER JOIN Intereses " +
                        "ON Intereses.IdDepartamento = Departamentos.IdDepartamento " +
                        //"WHERE Vendida = 'NO' AND Prestamos.Estado = 'ACTIVO' AND Prestamos.Contrato LIKE @IdContrato" +
                        "WHERE (Prestamos.Contrato LIKE @Busqueda OR Prendas.Descripcion LIKE @BusquedaDesc) " +
                        "AND (Prendas.Vendida = 'NO') " +
                        "AND (Prestamos.Estado = 'ACTIVO')" +
                        "ORDER BY EnVenta" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@Busqueda", Busqueda);
                        myCMD.Parameters.AddWithValue("@BusquedaDesc", BusquedaDesc);

                        SqlDataReader r = myCMD.ExecuteReader();

                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                prenda = new cPrenda();

                                prenda.IdPrenda = r["IdPrenda"].ToString();
                                prenda.IdDepartamento = r["IdDepartamento"].ToString();
                                prenda.IdCliente = r["IdCliente"].ToString();
                                prenda.IdCategoriaArticulo = r["IdCategoriaArticulo"].ToString();
                                prenda.TipoPrenda = r["TipoPrenda"].ToString();
                                prenda.Descripcion = r["Descripcion"].ToString();
                                prenda.Marca = r["Marca"].ToString();
                                prenda.Modelo = r["Modelo"].ToString();
                                prenda.Serie = r["Serie"].ToString();
                                prenda.IdTipoMetal = r["IdTipoMetal"].ToString();
                                prenda.PesoMetal = r["PesoMetal"].ToString();
                                prenda.Pureza = r["Pureza"].ToString();
                                prenda.ObservacionesMetal = r["ObservacionesMetal"].ToString();
                                prenda.IdTipoPiedra = r["IdTipoPiedra"].ToString();
                                prenda.ColorPiedra = r["ColorPiedra"].ToString();
                                prenda.ClaridadOPureza = r["ClaridadOPureza"].ToString();
                                prenda.CorteOTalla = r["CorteOTalla"].ToString();
                                prenda.PesoPiedra = r["PesoPiedra"].ToString();
                                prenda.ObservacionesPiedra = r["ObservacionesPiedra"].ToString();
                                prenda.IdTipoVehiculo = r["IdTipoVehiculo"].ToString();
                                prenda.IdMarcaVehiculo = r["IdMarcaVehiculo"].ToString();
                                prenda.ModeloVehiculo = r["ModeloVehiculo"].ToString();
                                prenda.AnioVehiculo = r["AnioVehiculo"].ToString();
                                prenda.Kilometraje = r["Kilometraje"].ToString();
                                prenda.NumeroSerieVehiculo = r["NumeroSerieVehiculo"].ToString();
                                prenda.Placas = r["Placas"].ToString();
                                prenda.ColorVehiculo = r["ColorVehiculo"].ToString();
                                prenda.UbicacionAlmacen = r["UbicacionAlmacen"].ToString();
                                prenda.Observaciones = r["Observaciones"].ToString();
                                prenda.Avaluo = r["Avaluo"].ToString();
                                prenda.Prestamo = r["Prestamo"].ToString();
                                prenda.PrestamoDisplay = "$ " + prenda.Prestamo;
                                prenda.EnVenta = r["EnVenta"].ToString();
                                prenda.Vendida = r["Vendida"].ToString();
                                prenda.PrecioVenta = r["PrecioVenta"].ToString();
                                prenda.Enajenado = r["Enajenado"].ToString();
                                prenda.PrecioVentaDisplay = "$ " + prenda.PrecioVenta;
                                prenda.Contrato = r["Contrato"].ToString();
                                prenda.Maximo = r["Maximo"].ToString();
                                prenda.DePrestamo = "SI";

                                prendas.Add(prenda);
                            }
                        }

                        con.Close();
                    }

                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Prendas.* " +
                        "FROM Prendas " +
                        "WHERE Prendas.Descripcion LIKE @Busqueda AND IdCliente = 0" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@Busqueda", Busqueda);

                        SqlDataReader r = myCMD.ExecuteReader();

                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                prenda = new cPrenda();

                                prenda.IdPrenda = r["IdPrenda"].ToString();
                                prenda.IdDepartamento = r["IdDepartamento"].ToString();
                                prenda.IdCliente = r["IdCliente"].ToString();
                                prenda.IdCategoriaArticulo = r["IdCategoriaArticulo"].ToString();
                                prenda.TipoPrenda = r["TipoPrenda"].ToString();
                                prenda.Descripcion = r["Descripcion"].ToString();
                                prenda.Marca = r["Marca"].ToString();
                                prenda.Modelo = r["Modelo"].ToString();
                                prenda.Serie = r["Serie"].ToString();
                                prenda.IdTipoMetal = r["IdTipoMetal"].ToString();
                                prenda.PesoMetal = r["PesoMetal"].ToString();
                                prenda.Pureza = r["Pureza"].ToString();
                                prenda.ObservacionesMetal = r["ObservacionesMetal"].ToString();
                                prenda.IdTipoPiedra = r["IdTipoPiedra"].ToString();
                                prenda.ColorPiedra = r["ColorPiedra"].ToString();
                                prenda.ClaridadOPureza = r["ClaridadOPureza"].ToString();
                                prenda.CorteOTalla = r["CorteOTalla"].ToString();
                                prenda.PesoPiedra = r["PesoPiedra"].ToString();
                                prenda.ObservacionesPiedra = r["ObservacionesPiedra"].ToString();
                                prenda.IdTipoVehiculo = r["IdTipoVehiculo"].ToString();
                                prenda.IdMarcaVehiculo = r["IdMarcaVehiculo"].ToString();
                                prenda.ModeloVehiculo = r["ModeloVehiculo"].ToString();
                                prenda.AnioVehiculo = r["AnioVehiculo"].ToString();
                                prenda.Kilometraje = r["Kilometraje"].ToString();
                                prenda.NumeroSerieVehiculo = r["NumeroSerieVehiculo"].ToString();
                                prenda.Placas = r["Placas"].ToString();
                                prenda.ColorVehiculo = r["ColorVehiculo"].ToString();
                                prenda.UbicacionAlmacen = r["UbicacionAlmacen"].ToString();
                                prenda.Observaciones = r["Observaciones"].ToString();
                                prenda.Avaluo = r["Avaluo"].ToString();
                                prenda.Prestamo = r["Prestamo"].ToString();
                                prenda.PrestamoDisplay = "$ " + prenda.Prestamo;
                                prenda.EnVenta = r["EnVenta"].ToString();
                                prenda.Vendida = r["Vendida"].ToString();
                                prenda.PrecioVenta = r["PrecioVenta"].ToString();
                                prenda.Enajenado = r["Enajenado"].ToString();
                                prenda.PrecioVentaDisplay = "$ " + prenda.PrecioVenta;
                                prenda.DePrestamo = "NO";
                                //prenda.Contrato = r["Contrato"].ToString();
                                //prenda.Maximo = r["Maximo"].ToString();

                                prendas.Add(prenda);
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return prendas;
        }

        public static ObservableCollection<cPrenda> ObtenerPrendasVendidas(String Fecha)
        {
            ObservableCollection<cPrenda> prendas = new ObservableCollection<cPrenda>();
            cPrenda prenda = new cPrenda();

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT Prendas.* " +
                        "FROM Prendas " +
                        "INNER JOIN Ventas " +
                        "ON Ventas.IdPrenda = Prendas.IdPrenda " +
                        "WHERE Vendida = 'SI' AND Ventas.FechaVenta = @Fecha" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@Fecha", Fecha);

                        SqlDataReader r = myCMD.ExecuteReader();

                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                prenda = new cPrenda();

                                prenda.IdPrenda = r["IdPrenda"].ToString();
                                prenda.IdDepartamento = r["IdDepartamento"].ToString();
                                prenda.IdCliente = r["IdCliente"].ToString();
                                prenda.IdCategoriaArticulo = r["IdCategoriaArticulo"].ToString();
                                prenda.TipoPrenda = r["TipoPrenda"].ToString();
                                prenda.Descripcion = r["Descripcion"].ToString();
                                prenda.Marca = r["Marca"].ToString();
                                prenda.Modelo = r["Modelo"].ToString();
                                prenda.Serie = r["Serie"].ToString();
                                prenda.IdTipoMetal = r["IdTipoMetal"].ToString();
                                prenda.PesoMetal = r["PesoMetal"].ToString();
                                prenda.Pureza = r["Pureza"].ToString();
                                prenda.ObservacionesMetal = r["ObservacionesMetal"].ToString();
                                prenda.IdTipoPiedra = r["IdTipoPiedra"].ToString();
                                prenda.ColorPiedra = r["ColorPiedra"].ToString();
                                prenda.ClaridadOPureza = r["ClaridadOPureza"].ToString();
                                prenda.CorteOTalla = r["CorteOTalla"].ToString();
                                prenda.PesoPiedra = r["PesoPiedra"].ToString();
                                prenda.ObservacionesPiedra = r["ObservacionesPiedra"].ToString();
                                prenda.IdTipoVehiculo = r["IdTipoVehiculo"].ToString();
                                prenda.IdMarcaVehiculo = r["IdMarcaVehiculo"].ToString();
                                prenda.ModeloVehiculo = r["ModeloVehiculo"].ToString();
                                prenda.AnioVehiculo = r["AnioVehiculo"].ToString();
                                prenda.Kilometraje = r["Kilometraje"].ToString();
                                prenda.NumeroSerieVehiculo = r["NumeroSerieVehiculo"].ToString();
                                prenda.Placas = r["Placas"].ToString();
                                prenda.ColorVehiculo = r["ColorVehiculo"].ToString();
                                prenda.UbicacionAlmacen = r["UbicacionAlmacen"].ToString();
                                prenda.Observaciones = r["Observaciones"].ToString();
                                prenda.Avaluo = r["Avaluo"].ToString();
                                prenda.Prestamo = r["Prestamo"].ToString();
                                prenda.PrestamoDisplay = "$ " + prenda.Prestamo;
                                prenda.EnVenta = r["EnVenta"].ToString();
                                prenda.Vendida = r["Vendida"].ToString();
                                prenda.PrecioVenta = r["PrecioVenta"].ToString();
                                prenda.Enajenado = r["Enajenado"].ToString();
                                prenda.PrecioVentaDisplay = "$ " + prenda.PrecioVenta;
                                prenda.DePrestamo = "";

                                prendas.Add(prenda);
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return prendas;
        }

        public static String EnajenarPrenda(String IdPrenda)
        {
            String r = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Prendas SET Enajenado = 'SI', EnVenta = 'SI', PrecioVenta = Avaluo, Vendida = 'NO' " +
                        "WHERE IdPrenda = @IdPrenda " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", IdPrenda);

                        myCMD.ExecuteNonQuery();

                        r = "OK";

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                r = exc.Message;
            }

            return r;
        }

        public static String ActualizarPrecioVenta(String IdPrenda, String Precio)
        {
            String r = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Prendas SET PrecioVenta = @PrecioVenta " +
                        "WHERE IdPrenda = @IdPrenda " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", IdPrenda);
                        myCMD.Parameters.AddWithValue("@PrecioVenta", Precio);

                        myCMD.ExecuteNonQuery();

                        r = "OK";

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                r = exc.Message;
            }

            return r;
        }

        public static String ActualizarImagenPrenda(String IdPrenda, String RutaImagen, String NumeroImagen)
        {
            String r = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Prendas SET RutaImagen" + NumeroImagen + " = @RutaImagen " +
                        "WHERE IdPrenda = @IdPrenda " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", IdPrenda);
                        myCMD.Parameters.AddWithValue("@RutaImagen", RutaImagen);

                        myCMD.ExecuteNonQuery();

                        r = "OK";

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                r = exc.Message;
            }

            return r;
        }

        public static String[] ObtenerImagenesPrenda(String IdPrenda)
        {
            String[] imgs = new string[9] { "", "", "", "", "", "", "", "", "" };

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "SELECT * " +
                        "FROM Prendas " +
                        "WHERE IdPrenda = @IdPrenda " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", IdPrenda);

                        SqlDataReader r = myCMD.ExecuteReader();

                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                imgs[0] = r["RutaImagen1"].ToString();
                                imgs[1] = r["RutaImagen2"].ToString();
                                imgs[2] = r["RutaImagen3"].ToString();
                                imgs[3] = r["RutaImagen4"].ToString();
                                imgs[4] = r["RutaImagen5"].ToString();
                                imgs[5] = r["RutaImagen6"].ToString();
                                imgs[6] = r["RutaImagen7"].ToString();
                                imgs[7] = r["RutaImagen8"].ToString();
                                imgs[8] = r["RutaImagen9"].ToString();
                            }
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {

            }

            return imgs;
        }

        public static String ActualizarPrendaEnajenada(String IdPrenda, String Estado)
        {
            String r = "";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "UPDATE Prendas SET Enajenado = @Estado, EnVenta = @Estado " +
                        "WHERE IdPrenda = @IdPrenda " +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdPrenda", IdPrenda);
                        myCMD.Parameters.AddWithValue("@Estado", Estado);

                        myCMD.ExecuteNonQuery();

                        r = "OK";

                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                r = exc.Message;
            }

            return r;
        }
    }
}
