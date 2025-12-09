using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsGrupoAnualDetalle
    {
        public int Id_GrupoAnual { get; set; }
        public string Nivel { get; set; }
        public string Salon { get; set; }
        public int Aforo { get; set; }
        public int Periodo { get; set; }
        public int TotalAlumnos { get; set; }

        // Nuevas propiedades
        public string NombreGrupo { get; set; }
    }
}
