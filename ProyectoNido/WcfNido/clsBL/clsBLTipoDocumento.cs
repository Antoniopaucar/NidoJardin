using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLTipoDocumento
    {
        public List<clsEntidades.clsTipoDocumento> listar_tipo_documentos()
        {
            clsDAC.clsDacTipoDocumento xtd = new clsDAC.clsDacTipoDocumento();
            List<clsEntidades.clsTipoDocumento> xlistatd = xtd.listarTipoDocumento();
            return xlistatd;
        }

        public void eliminar_tipo_documento(int xcodigo)
        {
            try
            {
                clsDAC.clsDacTipoDocumento xtd = new clsDAC.clsDacTipoDocumento();
                xtd.EliminarTipoDocumento(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_tipo_documento(clsEntidades.clsTipoDocumento xTd)
        {
            try
            {
                clsDAC.clsDacTipoDocumento db = new clsDAC.clsDacTipoDocumento();
                db.InsertarTipoDocumento(xTd);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_tipo_documento(clsEntidades.clsTipoDocumento xTd)
        {
            try
            {
                clsDAC.clsDacTipoDocumento db = new clsDAC.clsDacTipoDocumento();
                db.ModificarTipoDocumento(xTd);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
