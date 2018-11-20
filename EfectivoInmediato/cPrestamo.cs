using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cPrestamo
    {
        public int IdPrestamo { get; set; }
        public int IdCliente { get; set; }
        public int IdPrenda { get; set; }
        public int IdContrato { get; set; }
        public int IdPeriodo { get; set; }
        public int IdDepartamento { get; set; }
        public String NombreCliente { get; set; }
        public String NombrePrenda { get; set; }
        public float CantidadPrestada { get; set; }
        public float Saldo { get; set; }
        public String FechaPrestamo { get; set; }
        public String FechaVencimiento { get; set; }
        public int Refrendos { get; set; }
        public String Periodo { get; set; }
        public String Departamento { get; set; }
        public String Estado { get; set; }
    }
}
