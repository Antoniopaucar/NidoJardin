using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLRol
    {
        public List<clsEntidades.clsRol> listar_roles()
        {
            clsDAC.clsDacRol xRol = new clsDAC.clsDacRol();
            List<clsEntidades.clsRol> xlistaRol = xRol.listarRol();
            return xlistaRol;
        }

        public void eliminar_rol(int xcodigo)
        {
            try
            {
                clsDAC.clsDacRol xRol = new clsDAC.clsDacRol();
                xRol.EliminarRol(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_rol(clsEntidades.clsRol xRol)
        {
            try
            {
                clsDAC.clsDacRol db = new clsDAC.clsDacRol();
                db.InsertarRol(xRol);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_rol(clsEntidades.clsRol xRol)
        {
            try
            {
                clsDAC.clsDacRol db = new clsDAC.clsDacRol();
                db.ModificarRol(xRol);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        // Nuevo método: Obtener roles por lista de IDs
        public List<clsEntidades.clsRol> obtener_roles_por_ids(int[] idsRoles)
        {
            try
            {
                clsDAC.clsDacRol xRol = new clsDAC.clsDacRol();
                return xRol.ObtenerRolesPorIds(idsRoles);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
                return new List<clsEntidades.clsRol>();
            }
        }
    }
}
