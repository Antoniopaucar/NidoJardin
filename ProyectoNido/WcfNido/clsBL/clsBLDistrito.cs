using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLDistrito
    {
        public List<clsEntidades.clsDistrito> listar_distritos()
        {
            clsDAC.clsDacDistrito xdistritos = new clsDAC.clsDacDistrito();
            List<clsEntidades.clsDistrito> xlistadistritos = xdistritos.listarDistritos();
            return xlistadistritos;
        }

        public void eliminar_distrito(int xcodigo)
        {
            try
            {
                clsDAC.clsDacDistrito xdis = new clsDAC.clsDacDistrito();
                xdis.EliminarDistrito(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_distrito(clsEntidades.clsDistrito xdis)
        {
            try
            {
                clsDAC.clsDacDistrito db = new clsDAC.clsDacDistrito();
                db.InsertarDistrito(xdis);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_distrito(clsEntidades.clsDistrito xDis)
        {
            try
            {
                clsDAC.clsDacDistrito db = new clsDAC.clsDacDistrito();
                db.ModificarDistrito(xDis);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
