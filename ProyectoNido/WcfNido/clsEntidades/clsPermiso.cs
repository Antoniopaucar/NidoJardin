using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsPermiso
    {
        public int Id { get; set; }
        public string NombrePermiso { get; set; }
        public string Descripcion { get; set; }
        public clsPermiso() { }
        public clsPermiso(int id, string nombre, string descripcion)
        {
            Id = id;
            NombrePermiso = nombre;
            Descripcion = descripcion;
        }
    }
}
