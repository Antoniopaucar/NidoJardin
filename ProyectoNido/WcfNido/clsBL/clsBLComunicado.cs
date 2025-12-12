using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLComunicado
    {
        public List<clsEntidades.clsComunicado> listar_comunicados(int idUsuario)
        {
            clsDAC.clsDacComunicado xcomunicados = new clsDAC.clsDacComunicado();
            List<clsEntidades.clsComunicado> xlistacomunicados = xcomunicados.listarComunicados(idUsuario);
            return xlistacomunicados;
        }

        public void marcar_comunicado_visto(int idComunicado, int idUsuario)
        {
            try
            {
                clsDAC.clsDacComunicado db = new clsDAC.clsDacComunicado();
                db.MarcarComunicadoVisto(idComunicado, idUsuario);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void eliminar_comunicado(int xcodigo)
        {
            try
            {
                clsDAC.clsDacComunicado xcoms = new clsDAC.clsDacComunicado();
                xcoms.EliminarComunicado(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_comunicado(clsEntidades.clsComunicado xcom)
        {

            try
            {
                clsDAC.clsDacComunicado db = new clsDAC.clsDacComunicado();
                db.InsertarComunicado(xcom);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_comunicado(clsEntidades.clsComunicado xCom)
        {
            try
            {
                clsDAC.clsDacComunicado db = new clsDAC.clsDacComunicado();
                db.ModificarComunicado(xCom);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        /// <summary>
        /// Lista comunicados dirigidos a los roles del usuario (para vista de docente/apoderado)
        /// </summary>
        public List<clsEntidades.clsComunicado> listar_comunicados_por_rol_usuario(int idUsuario)
        {
            clsDAC.clsDacComunicado xcomunicados = new clsDAC.clsDacComunicado();
            List<clsEntidades.clsComunicado> xlistacomunicados = xcomunicados.ListarComunicadosPorRolUsuario(idUsuario);
            return xlistacomunicados;
        }

        /// <summary>
        /// Lista comunicados dirigidos específicamente al rol PROFESOR
        /// </summary>
        public List<clsEntidades.clsComunicado> listar_comunicados_por_rol_usuario_profesor(int idUsuario)
        {
            clsDAC.clsDacComunicado xcomunicados = new clsDAC.clsDacComunicado();
            List<clsEntidades.clsComunicado> xlistacomunicados = xcomunicados.ListarComunicadosPorRolUsuarioProfesor(idUsuario);
            return xlistacomunicados;
        }

        /// <summary>
        /// Lista comunicados dirigidos específicamente al rol APODERADO
        /// </summary>
        public List<clsEntidades.clsComunicado> listar_comunicados_por_rol_usuario_apoderado(int idUsuario)
        {
            clsDAC.clsDacComunicado xcomunicados = new clsDAC.clsDacComunicado();
            List<clsEntidades.clsComunicado> xlistacomunicados = xcomunicados.ListarComunicadosPorRolUsuarioApoderado(idUsuario);
            return xlistacomunicados;
        }
    }
}
