using clsBL;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI.WebControls;

namespace WcfNido
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        //----------------LOGIN----------------------------
        public List<clsLogin> ValidarUsuario(clsLogin login)
        {
            clsBL.clsBLLogin bl = new clsBL.clsBLLogin();
            return bl.validarusuario(login);
        }


        //------------------USUARIO----------------------------
        public List<clsUsuario> GetUsuario()
        {
            clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
            return xbl.listar_usuarios();
        }

        public void InsUsuarios(clsUsuario Usuario)
        {
            try
            {
                clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
                xbl.insertar_usuario(Usuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModUsuario(clsUsuario User)
        {
            try
            {
                clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
                xbl.modificar_usuario(User);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void DelUsuarios(int Codigo)
        {
            try
            {
                clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
                xbl.eliminar_usuario(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        //------------------ COMUNICADO ----------------------------
        public List<clsComunicado> GetComunicado(int idUsuario)
        {

            clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
            return xbl.listar_comunicados(idUsuario);
        }

        public void MarcarComunicadoVisto(int idComunicado, int idUsuario)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                xbl.marcar_comunicado_visto(idComunicado, idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void DelComunicado(int Codigo)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                xbl.eliminar_comunicado(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsComunicado(clsComunicado Usuario)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                xbl.insertar_comunicado(Usuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModComunicado(clsComunicado User)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                xbl.modificar_comunicado(User);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsComunicado> GetComunicadoPorRolUsuario(int idUsuario)
        {
            clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
            return xbl.listar_comunicados_por_rol_usuario(idUsuario);
        }

        //----------------------------- DISTRITO ---------------------------------------

        public List<clsDistrito> GetDistrito()
        {
            clsBL.clsBLDistrito xbl = new clsBL.clsBLDistrito();
            return xbl.listar_distritos();
        }

        public void DelDistrito(int Codigo)
        {
            try
            {
                clsBL.clsBLDistrito xbl = new clsBL.clsBLDistrito();
                xbl.eliminar_distrito(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsDistrito(clsDistrito Distrito)
        {
            try
            {
                clsBL.clsBLDistrito xbl = new clsBL.clsBLDistrito();
                xbl.insertar_distrito(Distrito);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModDistrito(clsDistrito Distrito)
        {
            try
            {
                clsBL.clsBLDistrito xbl = new clsBL.clsBLDistrito();
                xbl.modificar_distrito(Distrito);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //----------------------------- TIPO DOCUMENTO ------------------------------
        public List<clsTipoDocumento> GetTipoDocumento()
        {
            clsBL.clsBLTipoDocumento xbl = new clsBL.clsBLTipoDocumento();
            return xbl.listar_tipo_documentos();
        }

        public void DelTipoDocumento(int Codigo)
        {
            try
            {
                clsBL.clsBLTipoDocumento xbl = new clsBL.clsBLTipoDocumento();
                xbl.eliminar_tipo_documento(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsTipoDocumento(clsTipoDocumento tipodoc)
        {
            try
            {
                clsBL.clsBLTipoDocumento xbl = new clsBL.clsBLTipoDocumento();
                xbl.insertar_tipo_documento(tipodoc);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModTipoDocumento(clsTipoDocumento tipodoc)
        {
            try
            {
                clsBL.clsBLTipoDocumento xbl = new clsBL.clsBLTipoDocumento();
                xbl.modificar_tipo_documento(tipodoc);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //----------------------------- NIVEL ---------------------------------------
        public List<clsNivel> GetNivel()
        {
            clsBL.clsBLNivel xbl = new clsBL.clsBLNivel();
            return xbl.listar_nivel();
        }

        public void DelNivel(int Codigo)
        {
            try
            {
                clsBL.clsBLNivel xbl = new clsBL.clsBLNivel();
                xbl.eliminar_nivel(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsNivel(clsNivel nivel)
        {
            try
            {
                clsBL.clsBLNivel xbl = new clsBL.clsBLNivel();
                xbl.insertar_nivel(nivel);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModNivel(clsNivel nivel)
        {
            try
            {
                clsBL.clsBLNivel xbl = new clsBL.clsBLNivel();
                xbl.modificar_nivel(nivel);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //----------------------------- SALON ---------------------------------------
        public List<clsSalon> GetSalon()
        {
            clsBL.clsBLSalon xbl = new clsBL.clsBLSalon();
            return xbl.listar_salon();
        }

        public void DelSalon(int Codigo)
        {
            try
            {
                clsBL.clsBLSalon xbl = new clsBL.clsBLSalon();
                xbl.eliminar_salon(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsSalon(clsSalon salon)
        {
            try
            {
                clsBL.clsBLSalon xbl = new clsBL.clsBLSalon();
                xbl.insertar_salon(salon);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModSalon(clsSalon salon)
        {
            try
            {
                clsBL.clsBLSalon xbl = new clsBL.clsBLSalon();
                xbl.modificar_salon(salon);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //------------------------- ROL -----------------------------
        public List<clsRol> GetRol()
        {
            clsBL.clsBLRol xbl = new clsBL.clsBLRol();
            return xbl.listar_roles();
        }

        public void DelRol(int Codigo)
        {
            try
            {
                clsBL.clsBLRol xbl = new clsBL.clsBLRol();
                xbl.eliminar_rol(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsRol(clsRol rol)
        {
            try
            {
                clsBL.clsBLRol xbl = new clsBL.clsBLRol();
                xbl.insertar_rol(rol);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModRol(clsRol rol)
        {
            try
            {
                clsBL.clsBLRol xbl = new clsBL.clsBLRol();
                xbl.modificar_rol(rol);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //---------------------------- PERMISO ---------------------------------
        public List<clsPermiso> GetPermiso()
        {
            clsBL.clsBLPermiso xbl = new clsBL.clsBLPermiso();
            return xbl.listar_permisos();
        }

        public void DelPermiso(int Codigo)
        {
            try
            {
                clsBL.clsBLPermiso xbl = new clsBL.clsBLPermiso();
                xbl.eliminar_permiso(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsPermiso(clsPermiso per)
        {
            try
            {
                clsBL.clsBLPermiso xbl = new clsBL.clsBLPermiso();
                xbl.insertar_permiso(per);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModPermiso(clsPermiso per)
        {
            try
            {
                clsBL.clsBLPermiso xbl = new clsBL.clsBLPermiso();
                xbl.modificar_permiso(per);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        // --------------------------- USUARIO ROL -----------------------------------
        public List<clsUsuarioRol> GetUsuarioRol()
        {
            clsBL.clsBLUsuarioRol xbl = new clsBL.clsBLUsuarioRol();
            return xbl.listar_usuario_rol();
        }

        public void DelUsuarioRol(int id_user, int id_rol)
        {
            try
            {
                clsBL.clsBLUsuarioRol xbl = new clsBL.clsBLUsuarioRol();
                xbl.eliminar_usuario_rol(id_user,id_rol);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsUsuarioRol(clsUsuarioRol xUsr, clsProfesor xPro, clsApoderado xApo)
        {
            try
            {
                clsBLUsuarioRol bl = new clsBLUsuarioRol();
                bl.insertar_usuario_rol(xUsr, xPro, xApo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        // --------------------------- ROL PERMISO -----------------------------------
        public List<clsRolPermiso> GetRolPermiso()
        {
            clsBL.clsBLRolPermiso xbl = new clsBL.clsBLRolPermiso();
            return xbl.listar_rol_permiso();
        }

        public void DelRolPermiso(int id_rol, int id_permiso)
        {
            try
            {
                clsBL.clsBLRolPermiso xbl = new clsBL.clsBLRolPermiso();
                xbl.eliminar_rol_permiso(id_rol,id_permiso);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsRolPermiso(clsRolPermiso xRp)
        {
            try
            {
                clsBL.clsBLRolPermiso xbl = new clsBL.clsBLRolPermiso();
                xbl.insertar_rol_permiso(xRp);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //----------------------- USUARIO PERMISO --------------------------------
        public List<clsUsuarioPermiso> GetUsuarioPermiso()
        {
            clsBL.clsBLUsuarioPermiso xbl = new clsBL.clsBLUsuarioPermiso();
            return xbl.listar_usuario_permiso();
        }
        public void DelUsuarioPermiso(int id_user, int id_permiso)
        {
            try
            {
                clsBL.clsBLUsuarioPermiso xbl = new clsBL.clsBLUsuarioPermiso();
                xbl.eliminar_usuario_permiso(id_user, id_permiso);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsUsuarioPermiso(clsUsuarioPermiso xUp)
        {
            try
            {
                clsBL.clsBLUsuarioPermiso xbl = new clsBL.clsBLUsuarioPermiso();
                xbl.insertar_usuario_permiso(xUp);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //---------------------------- APODERADO -------------------------------------------------
        public List<clsApoderado> GetApoderado()
        {
            clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
            return xbl.listar_apoderados();
        }

        public void DelApoderado(int Codigo)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                xbl.eliminar_apoderado(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsApoderado(clsApoderado apo)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                xbl.insertar_apoderado(apo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModApoderado(clsApoderado apo)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                xbl.modificar_apoderado(apo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public clsArchivoBase RetArchivoApoderado(int Codigo)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                return xbl.Retornar_Archivo_Apoderado(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //----------------------------- PROFESOR --------------------------------------------------
        public List<clsProfesor> GetProfesor()
        {
            clsBL.clsBLProfesor xbl = new clsBL.clsBLProfesor();
            return xbl.listar_profesores();
        }

        public void DelProfesor(int Codigo)
        {
            try
            {
                clsBL.clsBLProfesor xbl = new clsBL.clsBLProfesor();
                xbl.eliminar_profesor(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsProfesor(clsProfesor profe)
        {
            try
            {
                clsBL.clsBLProfesor xbl = new clsBL.clsBLProfesor();
                xbl.insertar_profesor(profe);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModProfesor(clsProfesor profe)
        {
            try
            {
                clsBL.clsBLProfesor xbl = new clsBL.clsBLProfesor();
                xbl.modificar_profesor(profe);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public clsArchivoBase RetArchivoProfesor(int Codigo, string archivo)
        {
            try
            {
                clsBL.clsBLProfesor xbl = new clsBL.clsBLProfesor();
                return xbl.Retornar_Archivo_Profesor(Codigo, archivo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        //----------------------------- ALUMNO ---------------------------------------------------
        public List<clsAlumno> GetAlumno()
        {
            clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
            return xbl.listar_alumnos();
        }

        public void DelAlumno(int Codigo)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                xbl.eliminar_alumno(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void InsAlumno(clsAlumno alu)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                xbl.insertar_alumno(alu);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModAlumno(clsAlumno alu)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                xbl.modificar_alumno(alu);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public clsArchivoBase RetArchivoAlumno(int Codigo, string tipoArch)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                return xbl.Retornar_Archivo_Alumno(Codigo, tipoArch);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        //---------------------------- ACTUALIZAR DATOS DOCENTE ---------------------------------
        //public void ActualizarDatosDocente(int idUsuario, string nombres, string apPaterno, string apMaterno,
        //    string dni, DateTime? fechaNacimiento, string sexo, string direccion, string email,
        //    DateTime? fechaIngreso, string tituloProfesional, string cv, string evaluacionPsicologica,
        //    string fotos, string verificacionDomiciliaria)
        //{
        //    try
        //    {
        //        clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
        //        xbl.actualizar_datos_docente(idUsuario, nombres, apPaterno, apMaterno, dni, fechaNacimiento,
        //            sexo, direccion, email, fechaIngreso, tituloProfesional, cv, evaluacionPsicologica,
        //            fotos, verificacionDomiciliaria);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new FaultException(ex.Message);
        //    }
        //}

        //public clsUsuario ObtenerDatosDocente(int idUsuario)
        //{
        //    try
        //    {
        //        clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
        //        return xbl.obtener_datos_docente(idUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new FaultException(ex.Message);
        //    }
        //}

        //---------------------------- GRUPO ANUAL ---------------------------------
        public List<clsGrupoAnualDetalle> ListarGruposPorDocente(int idUsuario)
        {
            try
            {
                clsBL.clsBLGrupoAnual xbl = new clsBL.clsBLGrupoAnual();
                return xbl.ListarGruposPorDocente(idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        //---------------------------- GRUPO SERVICIO ---------------------------------
        public List<clsGrupoServicioDetalle> ListarGruposServicioPorDocente(int idUsuario)
        {
            try
            {
                clsBL.clsBLGrupoServicio xbl = new clsBL.clsBLGrupoServicio();
                return xbl.listar_grupos_servicio_por_docente(idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

    }
}
