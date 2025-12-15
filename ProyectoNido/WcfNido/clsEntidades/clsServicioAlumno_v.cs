using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsServicioAlumno_v
    {
        // ===== Campos reales de la tabla ServicioAlumno =====
        public int Id_ServicioAlumno { get; set; }
        public int Id_GrupoServicio { get; set; }
        public int Id_Alumno { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime? FechaPago { get; set; }

        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFinal { get; set; }

        public decimal Monto { get; set; }

        // ===== Campos extra (para mostrar en la grilla / consultas) =====
        public string NombreAlumno { get; set; }
        public string NombreSalon { get; set; }
        public int Aforo { get; set; }

        public string NombreProfesor { get; set; }
        public string NombreServicio { get; set; }

        public short Periodo { get; set; }      // SQL: smallint
        public string Estado { get; set; }      // ACTIVO / FINALIZADO
    }
}
