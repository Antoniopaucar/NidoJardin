using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsGrupoAnual_v
    {
        public int Id_GrupoAnual { get; set; }

        public int Id_Salon { get; set; }
        public string NombreSalon { get; set; }

        public int Aforo { get; set; }

        public int Id_Profesor { get; set; }
        public string NombreProfesor { get; set; }

        public int Id_Nivel { get; set; }
        public string NombreNivel { get; set; }

        public short Periodo { get; set; }
    }
}
