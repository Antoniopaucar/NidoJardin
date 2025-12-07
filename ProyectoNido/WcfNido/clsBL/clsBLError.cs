using clsDAC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLError
    {
        public void Control_Sql_Error(SqlException ex)
        {
            clsDacError dacError = new clsDacError();
            int idError = ex.Number;

            var errorInfo = dacError.BuscarErrorPorId(idError);

            if (errorInfo != null)
                throw new Exception(errorInfo.Descripcion);

            throw new Exception(ex.Message);
        }

    }
}
