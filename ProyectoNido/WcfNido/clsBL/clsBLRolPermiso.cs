using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLRolPermiso
    {
        public List<clsEntidades.clsRolPermiso> listar_rol_permiso()
        {
            clsDAC.clsDacRolPermiso xRp = new clsDAC.clsDacRolPermiso();
            List<clsEntidades.clsRolPermiso> xlistaRp = xRp.listarRolPermiso();
            return xlistaRp;
        }

        public void eliminar_rol_permiso(int id_rol,int id_permiso)
        {
            try
            {
                clsDAC.clsDacRolPermiso xRp = new clsDAC.clsDacRolPermiso();
                xRp.EliminarRolPermiso(id_rol,id_permiso);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_rol_permiso(clsEntidades.clsRolPermiso xRp)
        {

            try
            {
                clsDAC.clsDacRolPermiso db = new clsDAC.clsDacRolPermiso();
                db.InsertarRolPermiso(xRp);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
