using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class ComunicadoMovil
    {
        public int Id_Comunicado { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Visto { get; set; }
        public string EstadoTexto { get; set; }
    }
}
