using clsUtilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLUsuario
    {
        public List<clsEntidades.clsUsuario> listar_usuarios()
        {
            clsDAC.clsDacUsuario xusuarios = new clsDAC.clsDacUsuario();
            List<clsEntidades.clsUsuario> xlistausuarios = xusuarios.listarUsuarios();
            return xlistausuarios;
        }

        public void eliminar_usuario(int xcodigo)
        {
            try
            {
                clsDAC.clsDacUsuario xusuarios = new clsDAC.clsDacUsuario();
                xusuarios.EliminarUsuario(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_usuario(clsEntidades.clsUsuario xusuario)
        {
            try
            {
                if (xusuario.Clave!= xusuario.ClaveII)
                {
                    throw new ArgumentException("Las contraseñas no coinciden.");
                }

                string errorUsuario = clsUtiles.ValidarUser(xusuario.NombreUsuario);
                string errorClave = clsUtiles.ValidarContrasenia(xusuario.Clave);

                if (errorUsuario != null)
                {
                    throw new ArgumentException(errorUsuario);
                }

                if (errorClave != null)
                {
                    throw new ArgumentException(errorClave);
                }

                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                db.InsertarUsuario(xusuario);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_usuario(clsEntidades.clsUsuario xusuario)
        {
            try
            {
                if (xusuario.Clave != xusuario.ClaveII)
                {
                    throw new ArgumentException("Las contraseñas no coinciden.");
                }

                string errorUsuario = clsUtiles.ValidarUser(xusuario.NombreUsuario);

                if (errorUsuario != null)
                {
                    throw new ArgumentException(errorUsuario);
                }

                if (!string.IsNullOrEmpty(xusuario.Clave))
                {
                    string errorClave = clsUtiles.ValidarContrasenia(xusuario.Clave);

                    if (errorClave != null)
                    {
                        throw new ArgumentException(errorClave);
                    }
                }

                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                db.ModificarUsuario(xusuario);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        //public void actualizar_datos_docente(int idUsuario, string nombres, string apPaterno, string apMaterno,
        //    string dni, DateTime? fechaNacimiento, string sexo, string direccion, string email,
        //    DateTime? fechaIngreso, string tituloProfesional, string cv, string evaluacionPsicologica,
        //    string fotos, string verificacionDomiciliaria)
        //{
        //    try
        //    {
        //        clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
        //        db.ActualizarDatosDocente(idUsuario, nombres, apPaterno, apMaterno, dni, fechaNacimiento,
        //            sexo, direccion, email, fechaIngreso, tituloProfesional, cv, evaluacionPsicologica,
        //            fotos, verificacionDomiciliaria);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error al actualizar los datos del docente: " + ex.Message);
        //    }
        //}

        //public clsEntidades.clsUsuario obtener_datos_docente(int idUsuario)
        //{
        //    try
        //    {
        //        clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
        //        return db.ObtenerDatosDocente(idUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error al obtener los datos del docente: " + ex.Message);
        //    }
        //}
    }
}
