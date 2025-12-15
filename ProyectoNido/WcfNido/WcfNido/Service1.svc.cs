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
        //buscar nivel para el modal
        public List<clsNivel> buscarNivel(string texto)
        {
            return new clsBL.clsBLNivel().BuscarNivel(texto);
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

        public List<clsSalonCombo> BuscarSalon(string texto)
        {
            return new clsBLSalon().BuscarSalon(texto);
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

        public void ModUsuarioSinClave(clsUsuario User)
        {
            try
            {
                clsBL.clsBLUsuario xbl = new clsBL.clsBLUsuario();
                xbl.modificar_usuario_sin_clave(User);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModApoderadoOpcionalArchivo(clsApoderado apo)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                xbl.modificar_apoderado_opcional_archivo(apo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModApoderadoFormulario(clsUsuario usuario, clsApoderado apo)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                xbl.modificar_apoderado_formulario(usuario, apo);
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
        //----servicio para buscar a porfesor por nombre
        public List<clsProfesorCombo> buscarProfesor(string texto)
        {
            clsBLProfesor bl = new clsBLProfesor();
            return bl.buscarProfesor(texto);
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

        clsBLAlumno oBLAlumno = new clsBLAlumno();

        public List<clsAlumno> listarAlumnos_Combo()
        {
            return oBLAlumno.listar_alumnos_Combo();
        }

        public List<clsEntidades.clsAlumno> ListarAlumnosPorGrupoAnual(int idGrupoAnual)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                return xbl.ListarAlumnosPorGrupoAnual(idGrupoAnual);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsAlumnoCombo> buscarAlumno(string texto)
        {
            try
            {
                clsBLAlumno bl = new clsBLAlumno();
                return bl.buscarAlumno(texto);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error al buscar alumno: " + ex.Message);
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

        public List<clsGrupoAnual_v> GetGrupoAnual_V()
        {
            return new clsBL.clsBLGrupoAnual_v().Listar();
        }

        public string InsertarGrupoAnual(clsGrupoAnual obj)
        {
            new clsBL.clsBLGrupoAnual().Insertar(obj);
            return "Grupo Anual registrado correctamente.";
        }

        public string ModificarGrupoAnual(clsGrupoAnual obj)
        {
            new clsBL.clsBLGrupoAnual().Modificar(obj);
            return "Grupo Anual actualizado correctamente.";
        }

        public string EliminarGrupoAnual(int id)
        {
            try
            {
                new clsBL.clsBLGrupoAnual().Eliminar(id);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message; // <- tu UI mostrará alert(msg)
            }
        }


        public List<clsEntidades.clsGrupoAnual> GetGrupoAnual()
        {
            clsBL.clsBLGrupoAnual bl = new clsBL.clsBLGrupoAnual();
            return bl.listar_grupoanual();
        }

        clsBLGrupoAnual oBLGrupoAnual = new clsBLGrupoAnual();

        public List<clsGrupoAnual> listarGrupoAnual_Combo()
        {
            return oBLGrupoAnual.listar_grupo_anual_combo();
        }

        //---------------------------- MATRICULA ---------------------------------
        public List<clsEntidades.clsMatricula> Nido_Matricula_Listar(string estado)
        {
            var bl = new clsBL.clsBLMatricula();
            char? cEstado = null;
            if (!string.IsNullOrEmpty(estado))
                cEstado = estado[0];

            return bl.Nido_Matricula_Listar(cEstado);
        }

        public List<clsEntidades.clsMatricula> Matricula_Listar(
            int? idMatricula,
            int? idAlumno,
            int? idGrupoAnual,
            string estado)
        {
            var bl = new clsBL.clsBLMatricula();
            char? cEstado = null;
            if (!string.IsNullOrEmpty(estado))
                cEstado = estado[0];

            return bl.Matricula_Listar(idMatricula, idAlumno, idGrupoAnual, cEstado);
        }

        public clsEntidades.clsMatricula Matricula_Obtener(int idMatricula)
        {
            var bl = new clsBL.clsBLMatricula();
            return bl.Matricula_Obtener(idMatricula);
        }

        public int Matricula_Insertar(clsEntidades.clsMatricula mat)
        {
            var bl = new clsBL.clsBLMatricula();
            return bl.Matricula_Insertar(mat);
        }

        public bool Matricula_Actualizar(clsEntidades.clsMatricula mat)
        {
            var bl = new clsBL.clsBLMatricula();
            return bl.Matricula_Actualizar(mat);
        }

        public bool Matricula_CambiarEstado(int idMatricula, string estado)
        {
            var bl = new clsBL.clsBLMatricula();
            char cEstado = string.IsNullOrEmpty(estado) ? 'A' : estado[0];
            return bl.Matricula_CambiarEstado(idMatricula, cEstado);
        }

        //---------------------------- DETALLE MATRICULA ---------------------------------
        public List<clsEntidades.clsMatriculaDetalle> MatriculaDetalle_ListarPorMatricula(int idMatricula)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            return bl.listar_por_matricula(idMatricula);
        }

        public clsEntidades.clsMatriculaDetalle MatriculaDetalle_Obtener(int idMatriculaDetalle)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            return bl.obtener(idMatriculaDetalle);
        }

        public int MatriculaDetalle_Insertar(clsEntidades.clsMatriculaDetalle det)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            return bl.insertar(det);
        }

        public void MatriculaDetalle_Actualizar(clsEntidades.clsMatriculaDetalle det)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            bl.actualizar(det);
        }

        public void MatriculaDetalle_Eliminar(int idMatriculaDetalle)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            bl.eliminar(idMatriculaDetalle);
        }

        public void MatriculaDetalle_Anular(int idMatriculaDetalle)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            bl.anular(idMatriculaDetalle);
        }

        public void MatriculaDetalle_Reactivar(int idMatriculaDetalle)
        {
            var bl = new clsBL.clsBLMatriculaDetalle();
            bl.reactivar(idMatriculaDetalle);
        }


        //---------------------------- TARIFARIO ---------------------------------
        public List<clsEntidades.clsTarifario> listar_tarifario()
        {
            clsBL.clsBLTarifario bl = new clsBL.clsBLTarifario();
            return bl.listar_tarifario();
        }
        public List<clsEntidades.clsTarifario> listar_tarifario_combo()
        {
            clsBL.clsBLTarifario bl = new clsBL.clsBLTarifario();
            return bl.listar_tarifario_combo();
        }

        // ===== TARIFARIO =====
        public List<clsTarifario> GetTarifario_1()
        {
            clsBL.clsBLTarifario xbl = new clsBL.clsBLTarifario();
            return xbl.listar_tarifario_1();
        }

        public bool InsertarTarifario(clsTarifario obj)
        {
            clsBL.clsBLTarifario xbl = new clsBL.clsBLTarifario();
            return xbl.insertar_tarifario(obj);
        }

        public bool ActualizarTarifario(clsTarifario obj)
        {
            clsBL.clsBLTarifario xbl = new clsBL.clsBLTarifario();
            return xbl.actualizar_tarifario(obj);
        }

        public bool EliminarTarifario(int id)
        {
            clsBL.clsBLTarifario xbl = new clsBL.clsBLTarifario();
            return xbl.eliminar_tarifario(id);
        }



        //-------------------------------- SERVICIO ADICIONAL ---------------------------------------

        public List<clsServicioAdicional> GetServicioAdicional()
        {
            clsBL.clsBLServicioAdicional xbl = new clsBL.clsBLServicioAdicional();
            return xbl.listar_servicioAdicional();
        }

        public void DelServicioAdicional(int Codigo)
        {
            try
            {
                clsBL.clsBLServicioAdicional xbl = new clsBL.clsBLServicioAdicional();
                xbl.eliminar_servicioAdicional(Codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public List<clsServicioAdicional> BuscarServicioAdicional(string texto)
        {
            return new clsBLServicioAdicional().BuscarServicioAdicional(texto);
        }


        public void InsServicioAdicional(clsServicioAdicional servicio)
        {
            try
            {
                clsBL.clsBLServicioAdicional xbl = new clsBL.clsBLServicioAdicional();
                xbl.insertar_servicioAdicional(servicio);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ModServicioAdicional(clsServicioAdicional servicio)
        {
            try
            {
                clsBL.clsBLServicioAdicional xbl = new clsBL.clsBLServicioAdicional();
                xbl.modificar_servicioAdicional(servicio);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        //----------------------------- SERVICIO ALUMNO ---------------------------------------

        //public List<clsServicioAlumno> GetServicioAlumno()
        //{
        //    clsBLServicioAlumno bl = new clsBLServicioAlumno();
        //    return bl.listar_ServicioAlumno();
        //}

        public void DelServicioAlumno(int Codigo)
        {
            clsBLServicioAlumno bl = new clsBLServicioAlumno();
            bl.eliminar_ServicioAlumno(Codigo);
        }

        public void InsServicioAlumno(clsServicioAlumno servicioAlumno)
        {
            clsBLServicioAlumno bl = new clsBLServicioAlumno();
            bl.insertar_ServicioAlumno(servicioAlumno);
        }

        public void ModServicioAlumno(clsServicioAlumno servicioAlumno)
        {
            clsBLServicioAlumno bl = new clsBLServicioAlumno();
            bl.modificar_ServicioAlumno(servicioAlumno);
        }

        //----------------------------- SERVICIO ADICIONAL_V ---------------------------------------
        // ==========================
        // LISTAR
        // ==========================
        public List<clsServicioAlumno_v> GetServicioAlumno()
        {
            try
            {
                clsBLServicioAlumno_v bl = new clsBLServicioAlumno_v();
                return bl.listar_servicioAlumno();
            }
            catch (Exception ex)
            {
                throw new FaultException("Error al listar ServicioAlumno: " + ex.Message);
            }
        }

        // ==========================
        // INSERTAR
        // ==========================
        public string InsertarServicioAlumno(clsServicioAlumno_v obj)
        {
            try
            {
                clsBLServicioAlumno_v bl = new clsBLServicioAlumno_v();
                return bl.insertar_servicioAlumno(obj); // "OK" o mensaje (aforo, duplicado, etc.)
            }
            catch (Exception ex)
            {
                throw new FaultException("Error al insertar ServicioAlumno: " + ex.Message);
            }
        }

        // ==========================
        // MODIFICAR
        // ==========================
        public string ModificarServicioAlumno(clsServicioAlumno_v obj)
        {
            try
            {
                clsBLServicioAlumno_v bl = new clsBLServicioAlumno_v();
                return bl.modificar_servicioAlumno(obj);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error al modificar ServicioAlumno: " + ex.Message);
            }
        }

        // ==========================
        // ELIMINAR
        // ==========================
        public string EliminarServicioAlumno(int id)
        {
            try
            {
                clsBLServicioAlumno_v bl = new clsBLServicioAlumno_v();
                return bl.eliminar_servicioAlumno(id);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error al eliminar ServicioAlumno: " + ex.Message);
            }
        }


        //----------------------------- GRUPO SERVICIO ---------------------------------------
        public List<clsGrupoServicio> GetGrupoServicio()
        {
            clsBL.clsBLGrupoServicio_v xbl = new clsBL.clsBLGrupoServicio_v();
            return xbl.listar_grupoServicio();
        }

        public string InsertarGrupoServicio(clsGrupoServicio obj)
        {
            try
            {
                clsBL.clsBLGrupoServicio_v bl = new clsBL.clsBLGrupoServicio_v();
                bl.insertar_grupoServicio(obj);
                return "OK";
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public string ModificarGrupoServicio(clsGrupoServicio obj)
        {
            try
            {
                clsBL.clsBLGrupoServicio_v bl = new clsBL.clsBLGrupoServicio_v();
                bl.modificar_grupoServicio(obj);
                return "OK";
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public string EliminarGrupoServicio(int id)
        {
            try
            {
                clsBL.clsBLGrupoServicio_v bl = new clsBL.clsBLGrupoServicio_v();
                bl.eliminar_grupoServicio(id);
                return "OK";
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public List<clsGrupoServicio> buscarGrupoServicio(string texto)
        {
            try
            {
                clsBLGrupoServicio_v bl = new clsBLGrupoServicio_v();
                return bl.buscarGrupoServicio(texto);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error al buscar GrupoServicio: " + ex.Message);
            }
        }



        public List<clsComunicado> GetComunicadoPorRolUsuario(int idUsuario)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                return xbl.listar_comunicados_por_rol_usuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsComunicado> GetComunicadoPorRolUsuarioProfesor(int idUsuario)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                return xbl.listar_comunicados_por_rol_usuario_profesor(idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsComunicado> GetComunicadoPorRolUsuarioApoderado(int idUsuario)
        {
            try
            {
                clsBL.clsBLComunicado xbl = new clsBL.clsBLComunicado();
                return xbl.listar_comunicados_por_rol_usuario_apoderado(idUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        List<clsGrupoAnualDetalle> IService1.ListarGruposPorDocente(int idUsuario)
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

        public List<clsEntidades.clsGrupoServicioOferta> ListarOfertasGrupoServicio()
        {
            try
            {
                clsBL.clsBLGrupoServicio xbl = new clsBL.clsBLGrupoServicio();
                return xbl.listar_ofertas_grupo_servicio();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsEntidades.clsAlumno> ListarAlumnosPorGrupoServicio(int idGrupoServicio)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                return xbl.ListarAlumnosPorGrupoServicio(idGrupoServicio);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsEntidades.clsAlumno> ListarAlumnosPorApoderado(int idApoderado)
        {
            try
            {
                clsBL.clsBLAlumno xbl = new clsBL.clsBLAlumno();
                return xbl.ListarAlumnosPorApoderado(idApoderado);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public clsEntidades.clsApoderado ObtenerApoderadoPorId(int idApoderado)
        {
            try
            {
                clsBL.clsBLApoderado xbl = new clsBL.clsBLApoderado();
                var lista = xbl.listar_apoderados();
                return lista.FirstOrDefault(a => a.Id == idApoderado);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        //------------------------ OBTENER ROLES POR IDs -------------------------------------
        public List<clsRol> ObtenerRolesPorIds(int[] idsRoles)
        {
            try
            {
                clsBL.clsBLRol xbl = new clsBL.clsBLRol();
                return xbl.obtener_roles_por_ids(idsRoles);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsCuota> GetCuota(int? idTarifario)
        {
            clsBL.clsBLCuota xbl = new clsBL.clsBLCuota();
            return xbl.listar_Cuota(idTarifario);
        }

        public bool InsertarCuota(clsCuota obj)
        {
            clsBL.clsBLCuota xbl = new clsBL.clsBLCuota();
            return xbl.insertar_Cuota(obj);
        }

        public bool ActualizarCuota(clsCuota obj)
        {
            clsBL.clsBLCuota xbl = new clsBL.clsBLCuota();
            return xbl.actualizar_Cuota(obj);
        }

        public bool EliminarCuota(int idCuota)
        {
            clsBL.clsBLCuota xbl = new clsBL.clsBLCuota();
            return xbl.eliminar_Cuota(idCuota);
        }

        //------------------------- CRONOGRAMA DE PAGOS (APLICACIÓN WEB) ----------------------------------------------
        public clsMatricula ObtenerMatriculaActualPorAlumno(int idAlumno)
        {
            clsBL.clsBLMatricula xbl = new clsBL.clsBLMatricula();
            return xbl.ObtenerMatriculaActualPorAlumno(idAlumno);
        }

        public clsResumenCuotas ResumenCuotasPorMatricula(int idMatricula)
        {
            clsBL.clsBLMatricula xbl = new clsBL.clsBLMatricula();
            return xbl.ResumenCuotasPorMatricula(idMatricula);
        }

        public List<clsMatriculaDetalle> ListarCuotasPorMatricula(int idMatricula)
        {
            clsBL.clsBLMatricula xbl = new clsBL.clsBLMatricula();
            return xbl.ListarCuotasPorMatricula(idMatricula);
        }

        //------------------------- HISTORIAL DE SERVICIOS (APLICACIÓN WEB) ----------------------------------------------
        public List<clsServicioAlumno> ListarServicioAlumnoPorAlumno(int idAlumno)
        {
            clsBL.clsBLServicioAlumno xbl = new clsBL.clsBLServicioAlumno();
            return xbl.ListarServicioAlumnoPorAlumno(idAlumno);
        }

        //------------------------ APARTADO MÓVIL ------------------------------
        //------------------------ OBTENER COMUNICADOS PARA APLICACIÓN MÓVIL ------------------------------
        public List<ComunicadoMovil> Mov_ListarComunicados(int idUsuario)
        {
            return new clsBL_Movil().ListarComunicados(idUsuario);
        }
        // LOGIN APODERADO
        public E_MovLogin Mov_LoginApoderado(string usuarioODocumento, string clave)
        {
            var bl = new clsBL_Movil();
            var user = bl.LoginApoderado(usuarioODocumento, clave);

            // WCF no maneja bien null en algunos casos, devolvemos objeto “vacío”
            if (user == null)
                return new E_MovLogin { Id_Usuario = 0 };

            return user;
        }
        // COMBO LISTAR ALUMNOS POR APODERADO
        public List<clsAlumno> Mov_ListarHijosPorApoderado(int idApoderado)
        {
            clsBL_Movil bl = new clsBL_Movil();
            return bl.ListarHijosPorApoderado(idApoderado);
        }
        // OBTENER MATRICULA POR ALUMNO
        public clsMatricula Mov_ObtenerMatriculaActual(int idAlumno)
        {
            return new clsBL_Movil().ObtenerMatriculaActual(idAlumno);
        }
        // RESUMEN DE PAGOS POR MATRICULA
        public clsResumenCuotas Mov_ResumenCuotas(int idMatricula)
        {
            return new clsBL_Movil().ResumenCuotasPorMatricula(idMatricula);
        }
        // LISTAR DETALLE DE CUOTAS POR MATRICULA
        public List<clsMatriculaDetalle> Mov_ListarCuotas(int idMatricula)
        {
            return new clsBL_Movil().ListarCuotasPorMatricula(idMatricula);
        }
        // LISTAR COMUNICADOS POR USUARIO
        public List<E_Comunicado> mov_Comunicado_Listar_Por_Usuario(int idUsuario)
        {
            return clsBL_Movil.GetInstance().mov_Comunicado_Listar_Por_Usuario(idUsuario);
        }

        // ------------------------ FIN APARTADO MÓVIL ------------------------------

        public List<clsEntidades.clsReporteIngreso> ListarReporteIngresos(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            clsBL.clsBLReporteIngresos bl = new clsBL.clsBLReporteIngresos();
            return bl.ListarReporte(idSalon, idDistrito, fechaInicio, fechaFin);
        }

        public List<clsEntidades.clsReporteIngreso> ListarReporteCobranzas(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            clsBL.clsBLReporteIngresos bl = new clsBL.clsBLReporteIngresos();
            return bl.ListarReporteCobranzas(idSalon, idDistrito, fechaInicio, fechaFin);
        }

        public List<clsEntidades.clsAlumno> ListarAlumnosActivos()
        {
            try
            {
                clsBL.clsBLAlumno bl = new clsBL.clsBLAlumno();
                return bl.listarAlumnosActivos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<clsEntidades.clsProfesor> ListarProfesoresActivos()
        {
            try
            {
                clsBL.clsBLProfesor bl = new clsBL.clsBLProfesor();
                return bl.listarProfesoresActivos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
