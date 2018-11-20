using System;
using System.Collections.Generic;
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
    }
}
