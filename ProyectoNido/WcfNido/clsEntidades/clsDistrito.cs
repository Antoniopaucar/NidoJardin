using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsDistrito
    {
        public int Id { get; set; }
        public string Ubigeo { get; set; }
        public string Nombre { get; set; }

        public clsDistrito() { }

        public clsDistrito(int id, string ubigeo, string nombre)
        {
            Id = id;
            Ubigeo = ubigeo;
            Nombre = nombre;
        }
    }
}
