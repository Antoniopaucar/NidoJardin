using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    /// <summary>
    /// Entidad para mostrar ofertas de grupos de servicio a apoderados
    /// Incluye información del docente y cupos disponibles
    /// </summary>
    public class clsGrupoServicioOferta
    {
        public int Id_GrupoServicio { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public decimal Costo { get; set; }
        public string Salon { get; set; }
        public int Aforo { get; set; }
        public int Periodo { get; set; }
        public int TotalAlumnos { get; set; }
        public string NombreDocente { get; set; }
        public string ApellidoPaternoDocente { get; set; }
        public string ApellidoMaternoDocente { get; set; }
        
        // Propiedad calculada para cupos disponibles
        public int CuposDisponibles 
        { 
            get { return Aforo - TotalAlumnos; } 
        }
        
        // Propiedad calculada para nombre completo del docente
        public string NombreCompletoDocente
        {
            get 
            { 
                return $"{NombreDocente} {ApellidoPaternoDocente} {ApellidoMaternoDocente}".Trim(); 
            }
        }
    }
}

