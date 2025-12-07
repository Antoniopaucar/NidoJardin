using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsTipoDocumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public int CantidadCaracteres { get; set; }
        public clsTipoDocumento() { }
        public clsTipoDocumento(int id, string nombre, string abreviatura, int cantidadCaracteres)
        {
            Id = id;
            Nombre = nombre;
            Abreviatura = abreviatura;
            CantidadCaracteres = cantidadCaracteres;
        }
    }
}
