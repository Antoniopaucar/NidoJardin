using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsServicioAdicional
    {
        public int Id_ServicioAdicional { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public char Tipo { get; set; }
        public decimal Costo { get; set; }

        public clsServicioAdicional() { }

        // Constructor con todos los campos excepto el ID (útil al insertar)
        public clsServicioAdicional(string nombre, string descripcion, char tipo, decimal costo)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Tipo = tipo;
            Costo = costo;
        }

        // Constructor completo (útil al listar o modificar)
        public clsServicioAdicional(int id, string nombre, string descripcion, char tipo, decimal costo)
        {
            Id_ServicioAdicional = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Tipo = tipo;
            Costo = costo;
        }
    }
}
