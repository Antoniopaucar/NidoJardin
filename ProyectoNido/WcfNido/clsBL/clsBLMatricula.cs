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
    public class clsBLMatricula
    {
        // Listado para la grilla (join con Alumno, Grupo, Tarifario)
        public List<clsEntidades.clsMatricula> Nido_Matricula_Listar(char? estado = null)
        {
            try
            {
                var dac = new clsDAC.clsDacMatricula();
                return dac.Nido_Matricula_Listar(estado);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return new List<clsEntidades.clsMatricula>();
            }
        }

        // Listar con filtros (consulta más técnica)
        public List<clsEntidades.clsMatricula> Matricula_Listar(
            int? idMatricula = null,
            int? idAlumno = null,
            int? idGrupoAnual = null,
            char? estado = null)
        {
            try
            {
                var dac = new clsDAC.clsDacMatricula();
                return dac.Matricula_Listar(idMatricula, idAlumno, idGrupoAnual, estado);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return new List<clsEntidades.clsMatricula>();
            }
        }

        // Obtener una sola matrícula
        public clsEntidades.clsMatricula Matricula_Obtener(int idMatricula)
        {
            try
            {
                var dac = new clsDAC.clsDacMatricula();
                return dac.Matricula_Obtener(idMatricula);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return null;
            }
        }

        // Insertar matrícula (crea automáticamente sus cuotas)
        public int Matricula_Insertar(clsEntidades.clsMatricula mat)
        {
            try
            {
                var dac = new clsDAC.clsDacMatricula();
                return dac.Matricula_Insertar(mat);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return 0;
            }
        }

        // Actualizar matrícula
        public bool Matricula_Actualizar(clsEntidades.clsMatricula mat)
        {
            try
            {
                var dac = new clsDAC.clsDacMatricula();
                return dac.Matricula_Actualizar(mat);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return false;
            }
        }

        // Cambiar estado (anular / reactivar matrícula)
        public bool Matricula_CambiarEstado(int idMatricula, char estado)
        {
            try
            {
                var dac = new clsDAC.clsDacMatricula();
                return dac.Matricula_CambiarEstado(idMatricula, estado);
            }
            catch (SqlException ex)
            {
                var err = new clsBLError();
                err.Control_Sql_Error(ex);
                return false;
            }
        }
    }
}
