using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLServicioAlumno
    {
        public List<clsEntidades.clsServicioAlumno> listar_ServicioAlumno()
        {
            clsDAC.clsDacServicioAlumno xSer = new clsDAC.clsDacServicioAlumno();
            List<clsEntidades.clsServicioAlumno> xlista = xSer.ListarServicioAlumno();
            return xlista;
        }

        public void eliminar_ServicioAlumno(int xcodigo)
        {
            try
            {
                clsDAC.clsDacServicioAlumno xSer = new clsDAC.clsDacServicioAlumno();
                xSer.EliminarServicioAlumno(xcodigo);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void insertar_ServicioAlumno(clsEntidades.clsServicioAlumno xSer)
        {
            try
            {
                clsDAC.clsDacServicioAlumno db = new clsDAC.clsDacServicioAlumno();
                db.InsertarServicioAlumno(xSer);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void modificar_ServicioAlumno(clsEntidades.clsServicioAlumno xSer)
        {
            try
            {
                clsDAC.clsDacServicioAlumno db = new clsDAC.clsDacServicioAlumno();
                db.ModificarServicioAlumno(xSer);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        // Listar servicios por alumno (para aplicación web)
        public List<clsEntidades.clsServicioAlumno> ListarServicioAlumnoPorAlumno(int idAlumno)
        {
            try
            {
                if (idAlumno <= 0)
                    return new List<clsEntidades.clsServicioAlumno>();

                clsDAC.clsDacServicioAlumno xSer = new clsDAC.clsDacServicioAlumno();
                return xSer.ListarServicioAlumnoPorAlumno(idAlumno);
            }
            catch (Exception)
            {
                return new List<clsEntidades.clsServicioAlumno>();
            }
        }
    }
}
