using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsError
    {
        public int Id { get; set; }
        public int IdError { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public clsError() { }
        public clsError(int id, int idError, string descripcion, string categoria)
        {
            Id = id;
            IdError = idError;
            Descripcion = descripcion;
            Categoria = categoria;
        }
    }
}
