using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movil_Servicio_DB_Nido.Util
{
    public class AppState
    {
        public static int IdAlumnoSeleccionado { get; set; } = 0;
        public static string NombreAlumnoSeleccionado { get; set; } = "";

        public static int IdMatriculaActual { get; set; } = 0;
    }
}
