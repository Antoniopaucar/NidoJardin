using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBL_Movil
    {
        clsDAC_Movil dac = new clsDAC_Movil();

        public List<ComunicadoMovil> ListarComunicados(int idUsuario)
        {
            return dac.ListarComunicadosApoderado(idUsuario);
        }
        public E_MovLogin LoginApoderado(string usuarioODocumento, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuarioODocumento) || string.IsNullOrWhiteSpace(clave))
                return null;

            var lista = dac.LoginApoderado(usuarioODocumento.Trim(), clave.Trim());
            var user = lista.FirstOrDefault();

            if (user == null) return null;

            // validación extra por seguridad:
            if (!user.Activo || user.Bloqueado) return null;

            // debe ser Apoderado
            if (user.Id_Rol != 3) return null;

            return user;
        }

        //LISTAR HIJOS POR APODERADO
        public List<clsAlumno> ListarHijosPorApoderado(int idApoderado)
        {
            // validación simple
            if (idApoderado <= 0)
                return new List<clsAlumno>();

            return dac.ListarHijosPorApoderado(idApoderado);
        }

        //OBTENER MATRICULA POR ALUMNO
        // Obtener matricula actual por alumno
        public clsMatricula ObtenerMatriculaActual(int idAlumno)
        {
            if (idAlumno <= 0)
                return new clsMatricula { Id_Matricula = 0 };

            return dac.ObtenerMatriculaActual(idAlumno);
        }

        //RESUMEN DE PAGOS POR MATRICULA
        public clsResumenCuotas ResumenCuotasPorMatricula(int idMatricula)
        {
            if (idMatricula <= 0)
                return new clsResumenCuotas { Total = 0, Pagado = 0, Pendiente = 0 };

            return dac.ResumenCuotasPorMatricula(idMatricula);
        }

        //LISTAR DETALLE DE CUOTAS POR MATRICULA
        public List<clsMatriculaDetalle> ListarCuotasPorMatricula(int idMatricula)
        {
            if (idMatricula <= 0)
                return new List<clsMatriculaDetalle>();

            return dac.ListarCuotasPorMatricula(idMatricula);
        }
    }
}
