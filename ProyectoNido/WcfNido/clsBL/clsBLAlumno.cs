using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLAlumno
    {
        public List<clsEntidades.clsAlumno> listar_alumnos()
        {
            clsDAC.clsDacAlumno xal = new clsDAC.clsDacAlumno();
            List<clsEntidades.clsAlumno> xlistaAl = xal.listarAlumnos();
            return xlistaAl;
        }

        public List<clsEntidades.clsAlumno> listarAlumnosActivos()
        {
            clsDAC.clsDacAlumno xal = new clsDAC.clsDacAlumno();
            return xal.listarAlumnosActivos();
        }

        public clsArchivoBase Retornar_Archivo_Alumno(int id,string tipoArchivo)
        {
            try
            {
                clsDAC.clsDacArchivoBase xAb = new clsDAC.clsDacArchivoBase();

                switch (tipoArchivo)
                {
                    case "Fotos":
                        return xAb.RetornarArchivo("Retornar_Fotos_Alumno", "FoNombre", "FoTamanioBytes", "Fotos", id);

                    case "CopiaDni":
                        return xAb.RetornarArchivo("Retornar_CopiaDni_Alumno", "CDNombre", "CDTamanioBytes", "CopiaDni", id);

                    case "Permiso":
                        return xAb.RetornarArchivo("Retornar_PermisoPublicidad_Alumno", "PPNombre", "PPTamanioBytes", "PermisoPublicidad", id);

                    case "Carnet":
                        return xAb.RetornarArchivo("Retornar_CarnetSeguro_Alumno", "CSNombre", "CSTamanioBytes", "CarnetSeguro", id);

                    default:
                        // opcional: acción por defecto
                        return null;
                }
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
                return null;
            }
        }

        public void eliminar_alumno(int xcodigo)
        {
            try
            {
                clsDAC.clsDacAlumno xAl = new clsDAC.clsDacAlumno();
                xAl.EliminarAlumno(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_alumno(clsEntidades.clsAlumno xal)
        {
            try
            {
                clsDAC.clsDacAlumno db = new clsDAC.clsDacAlumno();
                db.InsertarAlumno(xal);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_alumno(clsEntidades.clsAlumno xal)
        {
            try
            {
                clsDAC.clsDacAlumno db = new clsDAC.clsDacAlumno();
                db.ModificarAlumno(xal);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        private clsDacAlumno oDac = new clsDacAlumno();

        public List<clsAlumno> listar_alumnos_Combo()
        {
            return oDac.listar_alumnos_Combo();
        }

        /// <summary>
        /// Lista alumnos matriculados en un grupo anual específico
        /// </summary>
        public List<clsEntidades.clsAlumno> ListarAlumnosPorGrupoAnual(int idGrupoAnual)
        {
            try
            {
                clsDAC.clsDacAlumno dac = new clsDAC.clsDacAlumno();
                return dac.ListarAlumnosPorGrupoAnual(idGrupoAnual);
            }
            catch (SqlException ex)
            {
                clsBLError err = new clsBLError();
                err.Control_Sql_Error(ex);
                return new List<clsEntidades.clsAlumno>();
            }
        }

        /// <summary>
        /// Lista alumnos inscritos en un grupo de servicio específico
        /// </summary>
        public List<clsEntidades.clsAlumno> ListarAlumnosPorGrupoServicio(int idGrupoServicio)
        {
            try
            {
                clsDAC.clsDacAlumno dac = new clsDAC.clsDacAlumno();
                return dac.ListarAlumnosPorGrupoServicio(idGrupoServicio);
            }
            catch (SqlException ex)
            {
                clsBLError err = new clsBLError();
                err.Control_Sql_Error(ex);
                return new List<clsEntidades.clsAlumno>();
            }
        }

        /// <summary>
        /// Lista alumnos (hijos) de un apoderado específico
        /// </summary>
        public List<clsEntidades.clsAlumno> ListarAlumnosPorApoderado(int idApoderado)
        {
            try
            {
                clsDAC.clsDacAlumno dac = new clsDAC.clsDacAlumno();
                return dac.ListarAlumnosPorApoderado(idApoderado);
            }
            catch (SqlException ex)
            {
                clsBLError err = new clsBLError();
                err.Control_Sql_Error(ex);
                return new List<clsEntidades.clsAlumno>();
            }
        }

        public List<clsAlumnoCombo> buscarAlumno(string texto)
        {
            clsDacAlumno dac = new clsDacAlumno();
            return dac.buscarAlumno(texto);
        }

    }
}
