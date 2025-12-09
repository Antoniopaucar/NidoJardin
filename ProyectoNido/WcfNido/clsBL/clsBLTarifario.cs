using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLTarifario
    {
        public List<clsEntidades.clsTarifario> listar_tarifario_combo()
        {
            List<clsEntidades.clsTarifario> lista = null;
            try
            {
                clsDAC.clsDacTarifario db = new clsDAC.clsDacTarifario();
                lista = db.listar_tarifario_combo();
            }
            catch (SqlException ex)
            {
                clsBLError err = new clsBLError();
                err.Control_Sql_Error(ex);
            }
            return lista;
        }
        public List<clsEntidades.clsTarifario> listar_tarifario()
        {
            List<clsEntidades.clsTarifario> lista = null;
            try
            {
                clsDAC.clsDacTarifario db = new clsDAC.clsDacTarifario();
                lista = db.listar_tarifario();
            }
            catch (SqlException ex)
            {
                clsBLError err = new clsBLError();
                err.Control_Sql_Error(ex);
            }
            return lista;
        }
    }
}
