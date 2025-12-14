using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsGrupoServicio
    {
        public int Id_GrupoServicio { get; set; }
        public int Id_Salon { get; set; }
        public int Id_Profesor { get; set; }
        public int Id_ServicioAdicional { get; set; }
        public short Periodo { get; set; }
        public string NombreSalon { get; set; }
        public string NombreProfesor { get; set; }
        public string NombreServicio { get; set; }

        public clsGrupoServicio() { }

        public clsGrupoServicio(int id_GrupoServicio, int id_Salon, int id_Profesor, int id_ServicioAdicional, short periodo)
        {
            Id_GrupoServicio = id_GrupoServicio;
            Id_Salon = id_Salon;
            Id_Profesor = id_Profesor;
            Id_ServicioAdicional = id_ServicioAdicional;
            Periodo = periodo;
        }
    }
}
