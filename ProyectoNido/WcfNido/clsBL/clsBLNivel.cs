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
    public class clsBLNivel
    {
        public List<clsEntidades.clsNivel> listar_nivel()
        {
            clsDAC.clsDacNivel xNiv = new clsDAC.clsDacNivel();
            List<clsEntidades.clsNivel> xlistaNivel = xNiv.listarNivel();
            return xlistaNivel;
        }

        public void eliminar_nivel(int xcodigo)
        {
            try
            {
                clsDAC.clsDacNivel xdis = new clsDAC.clsDacNivel();
                xdis.EliminarNivel(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_nivel(clsEntidades.clsNivel xNivel)
        {
            try
            {
                clsDAC.clsDacNivel db = new clsDAC.clsDacNivel();
                db.InsertarNivel(xNivel);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_nivel(clsEntidades.clsNivel xNivel)
        {
            try
            {
                clsDAC.clsDacNivel db = new clsDAC.clsDacNivel();
                db.ModificarNivel(xNivel);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        //---BuscarNivel----
        public List<clsNivel> BuscarNivel(string texto)
        {
            return new clsDacNivel().BuscarNivel(texto);
        }
    }
}
