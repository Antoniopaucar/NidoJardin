using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsGrupoAnual
    {
        public int Id_GrupoAnual { get; set; }
        public int Id_Salon { get; set; }
        public int Id_Profesor { get; set; }
        public int Id_Nivel { get; set; }
        public short Periodo { get; set; }

        // Mostrar en el Combo
        public string NombreGrupo { get; set; }  

        // Mostrar en listas o en formularios
        public string Descripcion { get; set; }   
    }
}
