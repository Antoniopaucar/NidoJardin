using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLServicioAlumno_v
    {
        private readonly clsDacServicioAlumno_v dac = new clsDacServicioAlumno_v();

        // LISTAR
        public List<clsServicioAlumno_v> listar_servicioAlumno()
        {
            return dac.ListarServicioAlumno();
        }

        // INSERTAR
        public string insertar_servicioAlumno(clsServicioAlumno_v obj)
        {
            // Validaciones mínimas (antes de ir a BD)
            if (obj == null) return "Datos inválidos.";
            if (obj.Id_GrupoServicio <= 0) return "Seleccione un GrupoServicio.";
            if (obj.Id_Alumno <= 0) return "Seleccione un Alumno.";
            if (obj.Monto < 0) return "El monto no puede ser negativo.";

            if (obj.FechaFinal.HasValue && obj.FechaFinal.Value.Date < obj.FechaInicio.Date)
                return "FechaFinal no puede ser menor que FechaInicio.";

            if (obj.HoraInicio.HasValue && obj.HoraFinal.HasValue && obj.HoraFinal.Value <= obj.HoraInicio.Value)
                return "HoraFinal debe ser mayor que HoraInicio.";

            // La validación de AFORO se hace en el SP (regla crítica)
            return dac.InsertarServicioAlumno(obj);
        }

        // MODIFICAR
        public string modificar_servicioAlumno(clsServicioAlumno_v obj)
        {
            if (obj == null) return "Datos inválidos.";
            if (obj.Id_ServicioAlumno <= 0) return "Seleccione un registro primero.";
            if (obj.Id_GrupoServicio <= 0) return "Seleccione un GrupoServicio.";
            if (obj.Id_Alumno <= 0) return "Seleccione un Alumno.";
            if (obj.Monto < 0) return "El monto no puede ser negativo.";

            if (obj.FechaFinal.HasValue && obj.FechaFinal.Value.Date < obj.FechaInicio.Date)
                return "FechaFinal no puede ser menor que FechaInicio.";

            if (obj.HoraInicio.HasValue && obj.HoraFinal.HasValue && obj.HoraFinal.Value <= obj.HoraInicio.Value)
                return "HoraFinal debe ser mayor que HoraInicio.";

            return dac.ModificarServicioAlumno(obj);
        }

        // ELIMINAR
        public string eliminar_servicioAlumno(int id)
        {
            if (id <= 0) return "Id inválido.";
            return dac.EliminarServicioAlumno(id);
        }
    }
}
