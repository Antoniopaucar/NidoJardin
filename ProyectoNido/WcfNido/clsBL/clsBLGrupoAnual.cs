using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLGrupoAnual
    {
        public List<clsGrupoAnualDetalle> ListarGruposPorDocente(int idUsuario)
        {
            clsDACGrupoAnual dac = new clsDACGrupoAnual();
            return dac.ListarGruposPorDocente(idUsuario);
        }


        //------------------------------------------------
        public List<clsEntidades.clsGrupoAnual> listar_grupoanual()
        {
            List<clsEntidades.clsGrupoAnual> lista = null;

            try
            {
                clsDAC.clsDACGrupoAnual db = new clsDAC.clsDACGrupoAnual();
                lista = db.ListarGrupoAnual();
            }
            catch (SqlException ex)
            {
                clsBLError err = new clsBLError();
                err.Control_Sql_Error(ex);
            }

            return lista;
        }

        //------------------------------------------------
        private clsDACGrupoAnual oDac = new clsDACGrupoAnual();

        public List<clsGrupoAnual> listar_grupo_anual_combo()
        {
            return oDac.listar_grupo_anual_combo();
        }
    }
}
