using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsSalon
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Aforo { get; set; }
        public string Dimensiones { get; set; }
        public bool Activo { get; set; }
        public clsSalon() { }
        public clsSalon(int id, string nombre, int aforo, string dimension)
        {
            Id = id;
            Nombre = nombre;
            Aforo = aforo;
            Dimensiones = dimension;
            Activo = true;
        }
    }
}
