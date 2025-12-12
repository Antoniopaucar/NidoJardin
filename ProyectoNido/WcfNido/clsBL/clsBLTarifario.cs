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
    public class clsBLTarifario
    {
        private static clsBLTarifario instance;

        public static clsBLTarifario GetInstance()
        {
            if (instance == null)
                instance = new clsBLTarifario();

            return instance;
        }
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

        public List<clsTarifario> listar_tarifario_1()
        {
            return clsDacTarifario.GetInstance().Listar();
        }

        public bool insertar_tarifario(clsTarifario obj)
        {
            // si Insertar devuelve el Id, consideramos éxito si es > 0
            int nuevoId = clsDacTarifario.GetInstance().Insertar(obj);
            return (nuevoId > 0);
        }

        public bool actualizar_tarifario(clsTarifario obj)
        {
            return clsDacTarifario.GetInstance().Actualizar(obj);
        }

        public bool eliminar_tarifario(int id)
        {
            return clsDacTarifario.GetInstance().Eliminar(id);
        }
    }
}
