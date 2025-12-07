using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsUsuarioRol
    {
        public clsUsuario Usuario { get; set; }
        public clsRol Rol { get; set; }
        public clsUsuarioRol() { }
        public clsUsuarioRol(clsUsuario usuario, clsRol rol)
        {
            Usuario = usuario;
            Rol = rol;
        }

    }
}
