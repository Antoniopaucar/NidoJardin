using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsServicioAlumno
    {
        public int Id_ServicioAlumno { get; set; }

        public int Id_GrupoServicio { get; set; }
        public int Id_Alumno { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime? FechaPago { get; set; }

        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFinal { get; set; }

        public decimal? Monto { get; set; }

        // Campos adicionales SOLO para mostrar en la interfaz (JOIN del SP)
        public string NombreAlumno { get; set; }
        public string NombreGrupo { get; set; }

        public clsServicioAlumno()
        {
            // Valores iniciales opcionales si deseas
        }

        public clsServicioAlumno(int id_ServicioAlumno, int id_GrupoServicio, int id_Alumno,
            DateTime? fechaInicio, DateTime? fechaFinal, DateTime? fechaPago,
            TimeSpan? horaInicio, TimeSpan? horaFinal, decimal? monto)
        {
            Id_ServicioAlumno = id_ServicioAlumno;
            Id_GrupoServicio = id_GrupoServicio;
            Id_Alumno = id_Alumno;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            FechaPago = fechaPago;
            HoraInicio = horaInicio;
            HoraFinal = horaFinal;
            Monto = monto;
        }
    }
}

