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
        List<clsEntidades.GrupoAnualDetalle> ListarGruposPorDocente(int idUsuario);

        // TODO: agregue aquí sus operaciones de servicio
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
