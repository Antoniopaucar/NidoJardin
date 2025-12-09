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
    public class clsBLProfesor
    {
        public List<clsEntidades.clsProfesor> listar_profesores()
        {
            clsDAC.clsDacProfesor xpro = new clsDAC.clsDacProfesor();
            List<clsEntidades.clsProfesor> xlistaPros = xpro.listarProfesores();
            return xlistaPros;
        }

        public clsArchivoBase Retornar_Archivo_Profesor(int id, string tipoArchivo)
        {
            try
            {
                clsDAC.clsDacArchivoBase xAb = new clsDAC.clsDacArchivoBase();

                switch (tipoArchivo)
                {
                    case "TituloProfesional":
                        return xAb.RetornarArchivo("Retornar_TituloProfesional_Profesor", "TPNombre", "TPTamanioBytes", "TituloProfesional", id);

                    case "Cv":
                        return xAb.RetornarArchivo("Retornar_Cv_Profesor", "CvNombre", "CvTamanioBytes", "Cv", id);

                    case "EvaluacionPsicologica":
                        return xAb.RetornarArchivo("Retornar_EvaluacionPsicologica_Profesor", "EPSNombre", "EPSTamanioBytes", "EvaluacionPsicologica", id);

                    case "Fotos":
                        return xAb.RetornarArchivo("Retornar_Fotos_Profesor", "FoNombre", "FoTamanioBytes", "Fotos", id);
                    
                    case "VerificacionDomiciliaria":
                        return xAb.RetornarArchivo("Retornar_VerificacionDomiciliaria_Profesor", "VDNombre", "VDTamanioBytes", "VerificacionDomiciliaria", id);

                    default:
                        // opcional: acción por defecto
                        return null;
                }
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
                return null;
            }
        }

        public void eliminar_profesor(int xcodigo)
        {
            try
            {
                clsDAC.clsDacProfesor xPro = new clsDAC.clsDacProfesor();
                xPro.EliminarProfesor(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        //public void insertar_profesor(clsEntidades.clsProfesor xpro)
        //{
        //    try
        //    {
        //        clsDAC.clsDacProfesor db = new clsDAC.clsDacProfesor();
        //        db.InsertarProfesor(xpro);
        //    }
        //    catch (SqlException ex)
        //    {
        //        clsBLError dacError = new clsBLError();
        //        dacError.Control_Sql_Error(ex);
        //    }
        //}

        public void insertar_profesor(clsEntidades.clsProfesor xpro)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();

                    // No se usa transacción porque es un método BL independiente
                    SqlTransaction trx = cn.BeginTransaction();

                    try
                    {
                        clsDAC.clsDacProfesor db = new clsDAC.clsDacProfesor();
                        db.InsertarProfesor(xpro, cn, trx);

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


        public void modificar_profesor(clsEntidades.clsProfesor xpro)
        {
            try
            {
                clsDAC.clsDacProfesor db = new clsDAC.clsDacProfesor();
                db.ModificarProfesor(xpro);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public List<clsProfesorCombo> buscarProfesor(string texto)
        {
            clsDAC.clsDacProfesor dac = new clsDAC.clsDacProfesor();
            return dac.buscarProfesor(texto);
        }
    }
}
