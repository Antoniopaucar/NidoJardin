using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsApoderado:clsUsuario
    {
        public string EstadoArchivo { get; set; }
        public string NombreCompleto { get; set; }
        public clsArchivoBase ArchivoBase { get; set; }
        public clsApoderado() { }
        public clsApoderado(
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
            string estado,
            clsArchivoBase archivobase,
            string nombrecompleto
        ) : base(id,tipoDocumento, nombreUsuario, password, passwordII, nombres, apellidoP, apellidoM,
                 documento, fecNacimiento, sexo, distrito, direccion, telefono, email,
                 fecha_bloqueo, ultimo_intento, ultimo_intento_exitoso)
        {
            EstadoArchivo = estado;
            ArchivoBase = archivobase;
            NombreCompleto = nombrecompleto;
        }
    }
}
