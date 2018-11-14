using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    class cPrestamo
    {
        public int idPrestamo { get; set; }
        public int idCliente { get; set; }
        public int idPrenda { get; set; }
        public int idContrato { get; set; }
        public int idPeriodo { get; set; }
        public int idDepartamento { get; set; }
        public String nombreCliente { get; set; }
        public String nombrePrenda { get; set; }
        public float cantidadPrestada { get; set; }
        public float saldo { get; set; }
        public String fechaPrestamo { get; set; }
        public String fechaVencimiento { get; set; }
        public int refrendos { get; set; }
        public String periodo { get; set; }
        public String departamento { get; set; }
        public String estado { get; set; }
    }
}
