using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsCuota
    {
        public int Id_Cuota { get; set; }
        public int Id_Tarifario { get; set; }
        public int NroCuota { get; set; }
        public DateTime FechaPagoSugerido { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Adicional { get; set; }
        public string NombreCuota { get; set; }
    }
}
