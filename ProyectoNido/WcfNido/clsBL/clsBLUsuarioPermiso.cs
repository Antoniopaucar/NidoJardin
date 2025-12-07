using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLUsuarioPermiso
    {
        public List<clsEntidades.clsUsuarioPermiso> listar_usuario_permiso()
        {
            clsDAC.clsDacUsuarioPermiso xUp = new clsDAC.clsDacUsuarioPermiso();
            List<clsEntidades.clsUsuarioPermiso> xlistaUp = xUp.listarUsuarioPermiso();
            return xlistaUp;
        }

        public void eliminar_usuario_permiso(int id_user, int id_permiso)
        {
            try
            {
                clsDAC.clsDacUsuarioPermiso xUp = new clsDAC.clsDacUsuarioPermiso();
                xUp.EliminarUsuarioPermiso(id_user, id_permiso);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_usuario_permiso(clsEntidades.clsUsuarioPermiso xUp)
        {

            try
            {
                clsDAC.clsDacUsuarioPermiso db = new clsDAC.clsDacUsuarioPermiso();
                db.InsertarUsuarioPermiso(xUp);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
