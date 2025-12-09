using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsGrupoServicioDetalle
    {
        public int Id_GrupoServicio { get; set; }
        public string Servicio { get; set; }
        public string Tipo { get; set; }
        public string Salon { get; set; }
        public int Aforo { get; set; }
        public int Periodo { get; set; }
        public int TotalAlumnos { get; set; }
    }
}

