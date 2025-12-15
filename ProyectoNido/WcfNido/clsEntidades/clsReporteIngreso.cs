using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsReporteIngreso
    {
        public DateTime? FechaPago { get; set; }
        public string Salon { get; set; }
        public string Distrito { get; set; }
        public string Alumno { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string EstadoPago { get; set; }
        public string EstadoDescripcion { get; set; }
        public string TipoOrigen { get; set; } // 'Pension' o 'Servicio'

        public clsReporteIngreso() { }
    }
}
