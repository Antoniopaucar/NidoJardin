using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsAlumno
    {
        public int Id { get; set; }
        public clsApoderado Apoderado { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public clsTipoDocumento TipoDocumento { get; set; }
        public string Documento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public bool Activo { get; set; }
        public string EstadoFotos { get; set; }
        public string EstadoCopiaDni { get; set; }
        public string EstadoPermisoPublicidad { get; set; }
        public string EstadoCarnetSeguro { get; set; }
        public clsArchivoBase Fotos { get; set; }
        public clsArchivoBase CopiaDni { get; set; }
        public clsArchivoBase PermisoPublicidad { get; set; }
        public clsArchivoBase CarnetSeguro { get; set; }

        public string NombreCompleto { get; set; }

        public clsAlumno() { }
        public clsAlumno(int id, clsApoderado apoderado, string nombres, string apellidoPaterno, string apellidoMaterno, 
            clsTipoDocumento tipoDocumento, string documento, DateTime? fechaNacimiento, string sexo, 
            string estadoFotos, string estadoCopiaDni, string estadoPermisoPublicidad, string estadoCarnetSeguro, 
            clsArchivoBase fotos,clsArchivoBase copiadni,clsArchivoBase permiso,clsArchivoBase carnet)
        {
            Id = id;
            Apoderado = apoderado;
            Nombres = nombres;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            TipoDocumento = tipoDocumento;
            Documento = documento;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
            Activo = true;
            EstadoFotos = estadoFotos;
            EstadoCopiaDni = estadoCopiaDni;
            EstadoPermisoPublicidad = estadoPermisoPublicidad;
            EstadoCarnetSeguro = estadoCarnetSeguro;
            Fotos = fotos;
            CopiaDni = copiadni;
            PermisoPublicidad = permiso;
            CarnetSeguro = carnet;
        }
    }
}
