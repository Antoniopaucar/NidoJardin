using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfNido
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        //-----------------Login----------------------
        [OperationContract]
        List<clsEntidades.clsLogin> ValidarUsuario(clsEntidades.clsLogin login);

        //-----------------USUARIOS--------------------

        [OperationContract]
        List<clsEntidades.clsUsuario> GetUsuario();

        [OperationContract]
        void DelUsuarios(int Codigo);
        [OperationContract]
        void InsUsuarios(clsEntidades.clsUsuario Usuario);
        [OperationContract]
        void ModUsuario(clsEntidades.clsUsuario User);

        //----------------COMUNICADOS---------------------------------------------------

        [OperationContract]
        List<clsEntidades.clsComunicado> GetComunicado(int idUsuario);

        [OperationContract]
        void MarcarComunicadoVisto(int idComunicado, int idUsuario);

        [OperationContract]
        void DelComunicado(int Codigo);
        [OperationContract]
        void InsComunicado(clsEntidades.clsComunicado Usuario);
        [OperationContract]
        void ModComunicado(clsEntidades.clsComunicado User);

        [OperationContract]
        List<clsEntidades.clsComunicado> GetComunicadoPorRolUsuario(int idUsuario);

        //------------------------ DISTRITO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsDistrito> GetDistrito();

        [OperationContract]
        void DelDistrito(int Codigo);
        [OperationContract]
        void InsDistrito(clsEntidades.clsDistrito Distrito);
        [OperationContract]
        void ModDistrito(clsEntidades.clsDistrito Distrito);
        //------------------------ NIVEL ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsNivel> GetNivel();

        [OperationContract]
        void DelNivel(int Codigo);
        [OperationContract]
        void InsNivel(clsEntidades.clsNivel nivel);
        [OperationContract]
        void ModNivel(clsEntidades.clsNivel nivel);
        //------------------------ SALON ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsSalon> GetSalon();

        [OperationContract]
        void DelSalon(int Codigo);
        [OperationContract]
        void InsSalon(clsEntidades.clsSalon salon);
        [OperationContract]
        void ModSalon(clsEntidades.clsSalon salon);
        //----listar salon por nombre 
        [OperationContract]
        List<clsSalonCombo> BuscarSalon(string texto);


        //------------------------ ROL ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsRol> GetRol();

        [OperationContract]
        void DelRol(int Codigo);
        [OperationContract]
        void InsRol(clsEntidades.clsRol rol);
        [OperationContract]
        void ModRol(clsEntidades.clsRol rol);

        //------------------------ PERMISO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsPermiso> GetPermiso();

        [OperationContract]
        void DelPermiso(int Codigo);
        [OperationContract]
        void InsPermiso(clsEntidades.clsPermiso per);
        [OperationContract]
        void ModPermiso(clsEntidades.clsPermiso per);
        //------------------------ USUARIO ROL ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsUsuarioRol> GetUsuarioRol();

        [OperationContract]
        void DelUsuarioRol(int id_user,int id_rol);
        [OperationContract]
        void InsUsuarioRol(clsUsuarioRol xUsr, clsProfesor xPro, clsApoderado xApo);

        //------------------------ ROL PERMISO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsRolPermiso> GetRolPermiso();

        [OperationContract]
        void DelRolPermiso(int id_rol, int id_permiso);
        [OperationContract]
        void InsRolPermiso(clsEntidades.clsRolPermiso xRp);

        //------------------------ USUARIO PERMISO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsUsuarioPermiso> GetUsuarioPermiso();

        [OperationContract]
        void DelUsuarioPermiso(int id_user, int id_permiso);
        [OperationContract]
        void InsUsuarioPermiso(clsEntidades.clsUsuarioPermiso xUp);

        //------------------------ APODERADO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsApoderado> GetApoderado();

        [OperationContract]
        void DelApoderado(int Codigo);
        [OperationContract]
        void InsApoderado(clsEntidades.clsApoderado apo);
        [OperationContract]
        void ModApoderado(clsEntidades.clsApoderado apo);
        [OperationContract]
        clsArchivoBase RetArchivoApoderado(int Codigo);
        //------------------------ PROFESOR----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsProfesor> GetProfesor();

        [OperationContract]
        void DelProfesor(int Codigo);
        [OperationContract]
        void InsProfesor(clsEntidades.clsProfesor profe);
        [OperationContract]
        void ModProfesor(clsEntidades.clsProfesor profe);
        [OperationContract]
        clsArchivoBase RetArchivoProfesor(int Codigo,string archivo);
        //---metodo para el buscarporfesor
        [OperationContract]
        List<clsProfesorCombo> buscarProfesor(string texto);


        //------------------------ ALUMNO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsAlumno> GetAlumno();

        [OperationContract]
        void DelAlumno(int Codigo);
        [OperationContract]
        void InsAlumno(clsEntidades.clsAlumno alu);
        [OperationContract]
        void ModAlumno(clsEntidades.clsAlumno alu);
        [OperationContract]
        clsArchivoBase RetArchivoAlumno(int Codigo,string tipoArch);

        [OperationContract]
        List<clsAlumno> listarAlumnos_Combo();

        //------------------------ TIPO DOCUMENTO ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsTipoDocumento> GetTipoDocumento();

        [OperationContract]
        void DelTipoDocumento(int Codigo);
        [OperationContract]
        void InsTipoDocumento(clsEntidades.clsTipoDocumento tipodoc);
        [OperationContract]
        void ModTipoDocumento(clsEntidades.clsTipoDocumento tipodoc);
        //------------------------ DOCENTE DATOS ----------------------------------------------
        //[OperationContract]
        //void ActualizarDatosDocente(int idUsuario, string nombres, string apPaterno, string apMaterno,
        //    string dni, DateTime? fechaNacimiento, string sexo, string direccion, string email,
        //    DateTime? fechaIngreso, string tituloProfesional, string cv, string evaluacionPsicologica,
        //    string fotos, string verificacionDomiciliaria);

        //[OperationContract]
        //clsEntidades.clsUsuario ObtenerDatosDocente(int idUsuario);

        //------------------------ GRUPO ANUAL ----------------------------------------------
        [OperationContract]
        List<clsEntidades.clsGrupoAnualDetalle> ListarGruposPorDocente(int idUsuario);

        //------------------------ GRUPO SERVICIO ----------------------------------------------
        [OperationContract]
        List<clsEntidades.clsGrupoServicioDetalle> ListarGruposServicioPorDocente(int idUsuario);

        [OperationContract]
        List<clsEntidades.clsGrupoAnual> GetGrupoAnual();

        [OperationContract]
        List<clsGrupoAnual> listarGrupoAnual_Combo();

        // TODO: agregue aquí sus operaciones de servicio


        //------------------------ MATRICULA ----------------------------------------------
        // Listado para grilla
        [OperationContract]
        List<clsEntidades.clsMatricula> Nido_Matricula_Listar(string estado);

        // Listar con filtros
        [OperationContract]
        List<clsEntidades.clsMatricula> Matricula_Listar(
            int? idMatricula,
            int? idAlumno,
            int? idGrupoAnual,
            string estado);

        // Obtener una matrícula
        [OperationContract]
        clsEntidades.clsMatricula Matricula_Obtener(int idMatricula);

        // Insertar
        [OperationContract]
        int Matricula_Insertar(clsEntidades.clsMatricula mat);

        // Actualizar
        [OperationContract]
        bool Matricula_Actualizar(clsEntidades.clsMatricula mat);

        // Cambiar estado (anular / reactivar)
        [OperationContract]
        bool Matricula_CambiarEstado(int idMatricula, string estado);

        //------------------------ MATRICULA DETALLE ----------------------------------------------
        [OperationContract]
        List<clsEntidades.clsMatriculaDetalle> MatriculaDetalle_ListarPorMatricula(int idMatricula);

        [OperationContract]
        clsEntidades.clsMatriculaDetalle MatriculaDetalle_Obtener(int idMatriculaDetalle);

        [OperationContract]
        int MatriculaDetalle_Insertar(clsEntidades.clsMatriculaDetalle det);

        [OperationContract]
        void MatriculaDetalle_Actualizar(clsEntidades.clsMatriculaDetalle det);

        [OperationContract]
        void MatriculaDetalle_Eliminar(int idMatriculaDetalle);

        [OperationContract]
        void MatriculaDetalle_Anular(int idMatriculaDetalle);

        [OperationContract]
        void MatriculaDetalle_Reactivar(int idMatriculaDetalle);


        //------------------------ TARIFARIO ----------------------------------------------
        [OperationContract]
        List<clsEntidades.clsTarifario> listar_tarifario();

        [OperationContract]
        List<clsEntidades.clsTarifario> listar_tarifario_combo();

        //------------------------ SERVICIO ADICIONAL ----------------------------------------------

        [OperationContract]
        List<clsEntidades.clsServicioAdicional> GetServicioAdicional();

        [OperationContract]
        void DelServicioAdicional(int Codigo);

        [OperationContract]
        void InsServicioAdicional(clsEntidades.clsServicioAdicional servicio);

        [OperationContract]
        void ModServicioAdicional(clsEntidades.clsServicioAdicional servicio);
        //----listar servicio adicional por nombre
        [OperationContract]
        List<clsServicioAdicional> BuscarServicioAdicional(string texto);



        //-------------------------------- Servicio_Alumno --------------------------------
        [OperationContract]
        List<clsServicioAlumno> GetServicioAlumno();

        [OperationContract]
        void DelServicioAlumno(int Codigo);

        [OperationContract]
        void InsServicioAlumno(clsServicioAlumno servicioAlumno);

        [OperationContract]
        void ModServicioAlumno(clsServicioAlumno servicioAlumno);

        //------------------------ GRUPO SERVICIO_V -------------------------------------

        [OperationContract]
        List<clsGrupoServicio> GetGrupoServicio();

        [OperationContract]
        string InsertarGrupoServicio(clsGrupoServicio obj);

        [OperationContract]
        string ModificarGrupoServicio(clsGrupoServicio obj);

        [OperationContract]
        string EliminarGrupoServicio(int id);

    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
