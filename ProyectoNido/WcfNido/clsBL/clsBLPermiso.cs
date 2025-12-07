using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLPermiso
    {
        public List<clsEntidades.clsPermiso> listar_permisos()
        {
            clsDAC.clsDacPermiso xPer = new clsDAC.clsDacPermiso();
            List<clsEntidades.clsPermiso> xlistaPer = xPer.listarPermiso();
            return xlistaPer;
        }

        public void eliminar_permiso(int xcodigo)
        {
            try
            {
                clsDAC.clsDacPermiso xPer = new clsDAC.clsDacPermiso();
                xPer.EliminarPermiso(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_permiso(clsEntidades.clsPermiso xPer)
        {
            try
            {
                clsDAC.clsDacPermiso db = new clsDAC.clsDacPermiso();
                db.InsertarPermiso(xPer);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_permiso(clsEntidades.clsPermiso xPer)
        {
            try
            {
                clsDAC.clsDacPermiso db = new clsDAC.clsDacPermiso();
                db.ModificarPermiso(xPer);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
