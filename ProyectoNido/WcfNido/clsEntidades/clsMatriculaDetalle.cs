using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsMatriculaDetalle
    {
        public int Id_MatriculaDetalle { get; set; }
        public int Id_Matricula { get; set; }
        public int? Id_Cuota { get; set; }
        public int? NroCuota { get; set; }
        public string NombreCuota { get; set; }
        public System.DateTime? FechaVencimiento { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Adicional { get; set; }
        public decimal TotalLinea { get; set; }
        public System.DateTime? FechaPago { get; set; }
        public string EstadoPago { get; set; }  // 'P' pagado, 'A' pendiente, etc.
        public string Observacion { get; set; }

        public clsMatriculaDetalle() { }
    }
}
