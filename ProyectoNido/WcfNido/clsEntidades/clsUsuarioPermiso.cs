using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsUsuarioPermiso
    {
        public clsUsuario Usuario { get; set; }
        public clsPermiso Permiso { get; set; }
        public string Tipo { get; set; }
        public clsUsuarioPermiso() { }
        public clsUsuarioPermiso(clsUsuario usuario,clsPermiso permiso,string tipo) 
        {
            Usuario = usuario;
            Permiso = permiso;
            Tipo = tipo;
        }
    }
}
