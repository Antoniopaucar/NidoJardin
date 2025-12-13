using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class E_MovLogin
    {
        public int Id_Usuario { get; set; }
        public string NombreUsuario { get; set; } = "";
        public string Documento { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public int Id_Rol { get; set; }  // debe ser 3
    }
}
