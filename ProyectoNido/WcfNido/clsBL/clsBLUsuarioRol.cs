using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace clsBL
{
    public class clsBLUsuarioRol
    {
        public List<clsEntidades.clsUsuarioRol> listar_usuario_rol()
        {
            clsDAC.clsDacUsuarioRol xUr = new clsDAC.clsDacUsuarioRol();
            List<clsEntidades.clsUsuarioRol> xlistaUR = xUr.listarUsuarioRol();
            return xlistaUR;
        }

        public void eliminar_usuario_rol(int id_user,int id_rol)
        {
            try
            {
                clsDAC.clsDacUsuarioRol xUr = new clsDAC.clsDacUsuarioRol();
                xUr.EliminarUsuarioRol(id_user,id_rol);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        //public void insertar_usuario_rol(clsEntidades.clsUsuarioRol xUr)
        //{
        //    try
        //    {
        //        clsDAC.clsDacUsuarioRol db = new clsDAC.clsDacUsuarioRol();
        //        db.InsertarUsuarioRol(xUr);
        //    }
        //    catch (SqlException ex)
        //    {
        //        clsBLError dacError = new clsBLError();
        //        dacError.Control_Sql_Error(ex);
        //    }
        //}

        public void insertar_usuario_rol(clsUsuarioRol xUsr, clsProfesor xPro, clsApoderado xApo)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    SqlTransaction trx = cn.BeginTransaction();

                    try
                    {
                        // 1. Insertar UsuarioRol
                        new clsDacUsuarioRol().InsertarUsuarioRol(xUsr, cn, trx);

                        // 2. Según el Rol, insertar profesor o apoderado
                        switch (xUsr.Rol.Id)
                        {
                            case 2:

                                xPro.Id = xUsr.Usuario.Id;

                                new clsDacProfesor().InsertarProfesor(xPro, cn, trx);
                                break;

                            case 3:

                                xApo.Id = xUsr.Usuario.Id;

                                new clsDacApoderado().InsertarApoderado(xApo, cn, trx);
                                break;
                        }

                        // 3. Todo OK → commit
                        trx.Commit();
                    }
                    catch
                    {
                        // Error → rollback
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
    }
}
