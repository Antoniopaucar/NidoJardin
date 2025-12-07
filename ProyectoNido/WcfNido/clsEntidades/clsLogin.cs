using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsLogin
    {
        //public string Usuario { get; set; }
        //public string clave { get; set; }
        //public clsLogin() { }

        public string Usuario { get; set; }
        public string clave { get; set; }

        // Campos adicionales que puede devolver el SP
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public int Id_Usuario { get; set; }
        public int? Id_Rol { get; set; }
        public int? Id_Permiso { get; set; }

        public clsLogin() { }

    }
}
