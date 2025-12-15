using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsProfesor:clsUsuario
    {
        public DateTime? FechaIngreso {  get; set; }
        public string NombreCompleto { get; set; }
        public string EstadoTituloProfesional { get; set; }
        public string EstadoCv { get; set; }
        public string EstadoEvaluacionPsicologica { get; set; }
        public string EstadoFotos { get; set; }
        public string EstadoVerificacionDomiciliaria { get; set; }
        public clsArchivoBase TituloProfesional { get; set; }
        public clsArchivoBase Cv { get; set; }
        public clsArchivoBase EvaluacionPsicologica { get; set; }
        public clsArchivoBase Fotos { get; set; }
        public clsArchivoBase VerificacionDomiciliaria { get; set; }
        public clsProfesor() { }
        public clsProfesor(
            int id,
            clsTipoDocumento tipoDocumento,
            string nombreUsuario,
            string password,
            string passwordII,
            string nombres,
            string apellidoP,
            string apellidoM,
            string documento,
            DateTime? fecNacimiento,
            string sexo,
            clsDistrito distrito,
            string direccion,
            string telefono,
            string email,
            DateTime? fecha_bloqueo,
            DateTime? ultimo_intento,
            DateTime? ultimo_intento_exitoso,
            DateTime? fecha_ingreso,
            string estadotitulo,
            string estadocv,
            string estadoevaluacion,
            string estadofotos,
            string estadoverificacion,
            clsArchivoBase titulo,
            clsArchivoBase cv,
            clsArchivoBase evaluacion,
            clsArchivoBase fotos,
            clsArchivoBase verificacion
        ) : base(id, tipoDocumento, nombreUsuario, password, passwordII, nombres, apellidoP, apellidoM,
                 documento, fecNacimiento, sexo, distrito, direccion, telefono, email,
                 fecha_bloqueo, ultimo_intento, ultimo_intento_exitoso)
        {
            FechaIngreso = fecha_ingreso;
            EstadoTituloProfesional=estadotitulo;
            EstadoCv = estadocv;
            EstadoEvaluacionPsicologica=estadoevaluacion;
            EstadoFotos = estadofotos;
            EstadoVerificacionDomiciliaria=estadoevaluacion;
            TituloProfesional=titulo;
            Cv = cv;
            EvaluacionPsicologica = evaluacion;
            Fotos = fotos;
            VerificacionDomiciliaria = verificacion;
        }
    }
}
