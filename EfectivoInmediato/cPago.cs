using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cPago
    {
        public String IdPago { get; set; }
        public String Periodo { get; set; }
        public String Importe { get; set; }
        public String Intereses { get; set; }
        public String Almacenaje { get; set; }
        public String IVA { get; set; }
        public String TotalDesempeno { get; set; }
        public String TotalRefrendo { get; set; }
        public String FechaPago { get; set; }

        public cPago()
        {

        }
    }
}
