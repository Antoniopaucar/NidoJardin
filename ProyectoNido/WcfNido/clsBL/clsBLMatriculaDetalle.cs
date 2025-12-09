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
    public class clsBLMatriculaDetalle
    {
        public List<clsEntidades.clsMatriculaDetalle> listar_por_matricula(int idMatricula)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                return db.ListarPorMatricula(idMatricula);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return new List<clsEntidades.clsMatriculaDetalle>();
            }
        }

        public clsEntidades.clsMatriculaDetalle obtener(int idDetalle)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                return db.Obtener(idDetalle);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return null;
            }
        }

        public int insertar(clsEntidades.clsMatriculaDetalle det)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                return db.Insertar(det);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return 0;
            }
        }

        public void actualizar(clsEntidades.clsMatriculaDetalle det)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                db.Actualizar(det);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
            }
        }

        public void eliminar(int idDetalle)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                db.Eliminar(idDetalle);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
            }
        }
        public void anular(int idDetalle)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                db.Anular(idDetalle);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
            }
        }

        public void reactivar(int idDetalle)
        {
            try
            {
                var db = new clsDAC.clsDacMatriculaDetalle();
                db.Reactivar(idDetalle);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
            }
        }
    }
}
