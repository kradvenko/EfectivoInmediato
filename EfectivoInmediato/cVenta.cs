using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfectivoInmediato
{
    public class cVenta
    {
        String IdVenta { get; set; }
        String IdPrenda { get; set; }
        String Descuento { get; set; }
        String Subtotal { get; set; }
        String Total { get; set; }
        String HoraVenta { get; set; }
        String FechaVenta { get; set; }
        String Estado { get; set; }

        public cVenta()
        {

        }

        public String GuardarVenta(cVenta venta)
        {
            String resultado = "OK";



            return resultado;
        }
    }
}
