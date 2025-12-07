using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsRolPermiso
    {
        public clsRol Rol {  get; set; }
        public clsPermiso Permiso { get; set; }
        public string Tipo { get; set; }
        public clsRolPermiso() { }
        public clsRolPermiso(clsRol rol, clsPermiso permiso,string tipo) 
        {
            this.Rol = rol;
            this.Permiso = permiso;
            this.Tipo = tipo;
        }
    }
}
