using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsUsuario
    {
        public int Id { get; set; }
        public clsTipoDocumento TipoDocumento { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string ClaveII { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Documento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public clsDistrito Distrito { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public int Intentos { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime? FechaBloqueo { get; set; } // ? es para que acepte nulos
        public DateTime? UltimoIntento { get; set; }
        public DateTime? UltimoLoginExitoso { get; set; }
        public DateTime FechaCreacion { get; set; }

        public clsUsuario() { }
        public clsUsuario(int id, clsTipoDocumento tipodoc,string nombreUsuario, string password, string passwordII, string nombres, string apellidoP,
            string apellidoM,string documento, DateTime? fecNacimiento,string sexo,clsDistrito distrito,string direccion, string telefono, 
            string email, DateTime? fecha_bloqueo, DateTime? ultimo_intento, DateTime? ultimo_intento_exitoso)
        {
            Id = id;
            TipoDocumento = tipodoc;
            NombreUsuario = nombreUsuario;
            Clave = password;
            ClaveII = passwordII;
            Nombres = nombres;
            ApellidoPaterno = apellidoP;
            ApellidoMaterno = apellidoM;
            Documento = documento;
            FechaNacimiento = fecNacimiento;
            Sexo = sexo;
            Distrito = distrito;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Activo = true;
            Intentos = 0;
            Bloqueado = false;
            FechaBloqueo = fecha_bloqueo;
            UltimoIntento = ultimo_intento;
            UltimoLoginExitoso = ultimo_intento_exitoso;
            FechaCreacion = DateTime.Now;
        }
    }
}
