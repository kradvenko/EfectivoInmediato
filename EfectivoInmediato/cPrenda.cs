using System;
using System.Collections.Generic;
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
        public String TipoPrenda { get; set; }
        public String Descripcion { get; set; }
        //Variables para tipo de prenda "Articulos"
        public String IdMarca { get; set; }
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

        public cPrenda()
        {

        }

        public static String GuardarPrenda(String IdDepartamento, String TipoPrenda, String Descripcion, String IdMarca, String Modelo, String Serie, String IdTipoMetal, String PesoMetal, String Pureza, String ObservacionesMetal, String IdTipoPiedra, String ColorPiedra, String ClaridadOPureza, String CorteOTalla, String PesoPiedra, String ObservacionesPiedra, String IdTipoVehiculo, String IdMarcaVehiculo, String ModeloVehiculo, String AnioVehiculo, String Kilometraje, String NumeroSerieVehiculo, String Placas, String ColorVehiculo, String UbicacionAlmacen, String Observaciones, String Avaluo, String Prestamo)
        {
            String resultado = "0";

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EfectivoInmediato.Properties.Settings.EfectivoInmediatoConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCMD = new SqlCommand(" " +
                        "INSERT INTO Prendas (IdDepartamento, TipoPrenda, Descripcion, IdMarca, Modelo, Serie, IdTipoMetal, PesoMetal, Pureza, ObservacionesMetal, IdTipoPiedra, ColorPiedra, ClaridadOPureza, CorteOTalla, PesoPiedra, ObservacionesPiedra, IdTipoVehiculo, IdMarcaVehiculo, ModeloVehiculo, AnioVehiculo, Kilometraje, NumeroSerieVehiculo, Placas, ColorVehiculo, UbicacionAlmacen, Observaciones, Avaluo, Prestamo) " +
                        "OUTPUT INSERTED.IdPrenda " +
                        "VALUES (@IdDepartamento, @TipoPrenda, @Descripcion, @IdMarca, @Modelo, @Serie, @IdTipoMetal, @PesoMetal, @Pureza, @ObservacionesMetal, @IdTipoPiedra, @ColorPiedra, @ClaridadOPureza, @CorteOTalla, @PesoPiedr, @ObservacionesPiedra, @IdTipoVehiculo, @IdMarcaVehiculo, @ModeloVehiculo, @AnioVehiculo, @Kilometraje, @NumeroSerieVehiculo, @Placas, @ColorVehiculo, @UbicacionAlmacen, @Observaciones, @Avaluo, @Prestamo)" +
                        "", con))
                    {
                        con.Open();

                        myCMD.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
                        myCMD.Parameters.AddWithValue("@TipoPrenda", TipoPrenda);
                        myCMD.Parameters.AddWithValue("@Descripcion", Descripcion);
                        myCMD.Parameters.AddWithValue("@IdMarca", IdMarca);
                        myCMD.Parameters.AddWithValue("@Modelo", Modelo);
                        myCMD.Parameters.AddWithValue("@Serie", Serie);
                        myCMD.Parameters.AddWithValue("@IdTipoMetal", IdTipoMetal);
                        myCMD.Parameters.AddWithValue("@PesoMetal", PesoMetal);
                        myCMD.Parameters.AddWithValue("@Pureza, ", Pureza);
                        myCMD.Parameters.AddWithValue("@ObservacionesMetal, ", ObservacionesMetal);
                        myCMD.Parameters.AddWithValue("@IdTipoPiedra, ", IdTipoPiedra);
                        myCMD.Parameters.AddWithValue("@ColorPiedra, ", ColorPiedra);
                        myCMD.Parameters.AddWithValue("@ClaridadOPureza, ", ClaridadOPureza);
                        myCMD.Parameters.AddWithValue("@CorteOTalla, ", CorteOTalla);
                        myCMD.Parameters.AddWithValue("@PesoPiedra, ", PesoPiedra);
                        myCMD.Parameters.AddWithValue("@ObservacionesPiedra, ", ObservacionesPiedra);
                        myCMD.Parameters.AddWithValue("@IdTipoVehiculo, ", IdTipoVehiculo);
                        myCMD.Parameters.AddWithValue("@IdMarcaVehiculo, ", IdMarcaVehiculo);
                        myCMD.Parameters.AddWithValue("@ModeloVehiculo, ", ModeloVehiculo);
                        myCMD.Parameters.AddWithValue("@AnioVehiculo, ", AnioVehiculo);
                        myCMD.Parameters.AddWithValue("@Kilometraje, ", Kilometraje);
                        myCMD.Parameters.AddWithValue("@NumeroSerieVehiculo, ", NumeroSerieVehiculo);
                        myCMD.Parameters.AddWithValue("@Placas, ", Placas);
                        myCMD.Parameters.AddWithValue("@ColorVehiculo, ", ColorVehiculo);
                        myCMD.Parameters.AddWithValue("@UbicacionAlmacen, ", UbicacionAlmacen);
                        myCMD.Parameters.AddWithValue("@Observaciones, ", Observaciones);
                        myCMD.Parameters.AddWithValue("@Avaluo, ", Avaluo);
                        myCMD.Parameters.AddWithValue("@Prestamo, ", Prestamo);

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

            return resultado;
        }
    }
}
