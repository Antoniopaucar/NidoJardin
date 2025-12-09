using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsMatricula
    {
        public int Id_Matricula { get; set; }
        public int Id_Alumno { get; set; }
        public int Id_GrupoAnual { get; set; }
        public int Id_Tarifario { get; set; }
        public string Codigo { get; set; }

        // campos de solo lectura / consulta (no los necesitas para insertar,
        // pero te servirán cuando hagas el Listar / Consultar)
        public System.DateTime FechaMatricula { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }

        // 🔹 Propiedades que usa el SP Nido_Matricula_Listar:
        public string AlumnoNombre { get; set; }      // a.Nombres + ...
        public string GrupoNombre { get; set; }       // CONCAT(n.Nombre, ' - ', g.Periodo)
        public string NombreTarifario { get; set; }   // t.Nombre

        public clsMatricula() { }
    }
}
