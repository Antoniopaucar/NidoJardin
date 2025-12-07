using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsRol
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
        public clsRol() { }
        public clsRol(int id, string nombre, string descripcion)
        {
            Id = id;
            NombreRol = nombre;
            Descripcion = descripcion;
        }
    }
}
