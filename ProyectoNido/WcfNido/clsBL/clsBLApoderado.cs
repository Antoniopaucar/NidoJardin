using clsDAC;
using clsEntidades;
using clsUtilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLApoderado
    {
        public List<clsEntidades.clsApoderado> listar_apoderados()
        {
            clsDAC.clsDacApoderado xapos = new clsDAC.clsDacApoderado();
            List<clsEntidades.clsApoderado> xlistaApos = xapos.listarApoderados();
            return xlistaApos;
        }

        public clsArchivoBase Retornar_Archivo_Apoderado(int id)
        {
            try
            {
                clsDAC.clsDacArchivoBase xAb = new clsDAC.clsDacArchivoBase();
                return xAb.RetornarArchivo("Retornar_Copia_Dni_Apoderado","NombreArchivo","TamanioBytes","CopiaDni",id);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
                return null;
            }
        }

        public void eliminar_apoderado(int xcodigo)
        {
            try
            {
                clsDAC.clsDacApoderado xApos = new clsDAC.clsDacApoderado();
                xApos.EliminarApoderado(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        //public void insertar_apoderado(clsEntidades.clsApoderado xapo)
        //{
        //    try
        //    {
        //        clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
        //        db.InsertarApoderado(xapo);
        //    }
        //    catch (SqlException ex)
        //    {
        //        clsBLError dacError = new clsBLError();
        //        dacError.Control_Sql_Error(ex);
        //    }
        //}

        public void insertar_apoderado(clsEntidades.clsApoderado xapo)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    SqlTransaction trx = cn.BeginTransaction();

                    try
                    {
                        clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
                        db.InsertarApoderado(xapo, cn, trx);

                        trx.Commit();
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }


        public void modificar_apoderado(clsEntidades.clsApoderado xapo)
        {
            try
            {
                clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
                db.ModificarApoderado(xapo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        // Nuevo método: Modificar apoderado opcionalmente con archivo (para formulario de apoderado)
        public void modificar_apoderado_opcional_archivo(clsEntidades.clsApoderado xapo)
        {
            try
            {
                clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
                db.ModificarApoderadoOpcionalArchivo(xapo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        // Nuevo método: Modificar apoderado usando el nuevo stored procedure para formulario
        public void modificar_apoderado_formulario(clsEntidades.clsUsuario xusuario, clsEntidades.clsApoderado xapo)
        {
            try
            {
                clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
                db.ModificarApoderadoFormulario(xusuario, xapo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
                throw; // Re-lanzar para que WCF lo capture como FaultException
            }
        }
    }
}
