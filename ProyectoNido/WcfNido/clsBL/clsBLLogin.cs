using clsDAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLLogin
    {
        public List<clsEntidades.clsLogin> validarusuario(clsEntidades.clsLogin login)
        {
            string claveHash = clsUtilidades.clsUtiles.ObtenerSha256(login.clave);
            clsDAC.clsDacLogin dblogin = new clsDAC.clsDacLogin();
            return dblogin.UsuarioExiste(login.Usuario, claveHash);
        }
    }
}
