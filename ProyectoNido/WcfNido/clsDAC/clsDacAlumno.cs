using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacAlumno
    {
        public List<clsEntidades.clsAlumno> listarAlumnos()
        {
            List<clsEntidades.clsAlumno> lista = new List<clsEntidades.clsAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_alumnos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsAlumno a = new clsEntidades.clsAlumno();
                            a.Apoderado = new clsApoderado();
                            a.TipoDocumento = new clsTipoDocumento();

                            a.Id = Convert.ToInt32(dr["Id_Alumno"]);
                            a.Apoderado.Id = Convert.ToInt32(dr["Id_Apoderado"]);
                            a.Apoderado.NombreCompleto = dr["NombreCompleto"].ToString();
                            a.TipoDocumento.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                            a.TipoDocumento.Nombre = dr["NombreTipoDocumento"].ToString();
                            a.Nombres = dr["Nombres"].ToString();
                            a.ApellidoPaterno = dr["ApPaterno"].ToString();
                            a.ApellidoMaterno = dr["ApMaterno"].ToString();
                            a.Documento = dr["Documento"].ToString();
                            a.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
                            a.Sexo = dr["Sexo"].ToString();
                            a.Activo = Convert.ToBoolean(dr["Activo"]);

                            a.EstadoFotos = dr["EstadoFotos"].ToString();
                            a.EstadoCopiaDni = dr["EstadoCopiaDni"].ToString();
                            a.EstadoPermisoPublicidad = dr["EstadoPermisoPublicidad"].ToString();
                            a.EstadoCarnetSeguro = dr["EstadoCarnetSeguro"].ToString();

                            lista.Add(a);
                        }
                    }
                }
            }
            return lista;
        }

        public void EliminarAlumno(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_alumnos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void InsertarAlumno(clsEntidades.clsAlumno xAl)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_alumnos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Apoderado", xAl.Apoderado.Id);
                        cmd.Parameters.AddWithValue("@Id_TipoDocumento", xAl.TipoDocumento.Id);
                        cmd.Parameters.AddWithValue("@Nombres", xAl.Nombres);
                        cmd.Parameters.AddWithValue("@ApPaterno", xAl.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApMaterno", xAl.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Documento", xAl.Documento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", xAl.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Sexo", xAl.Sexo);

                        cmd.Parameters.AddWithValue("@FoTamanioBytes", xAl.Fotos.TamanioBytes);
                        cmd.Parameters.AddWithValue("@FoNombre", xAl.Fotos.NombreArchivo);
                        cmd.Parameters.Add("@Fotos", SqlDbType.VarBinary).Value = (object)xAl.Fotos.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@CDTamanioBytes", xAl.CopiaDni.TamanioBytes);
                        cmd.Parameters.AddWithValue("@CDNombre", xAl.CopiaDni.NombreArchivo);
                        cmd.Parameters.Add("@CopiaDni", SqlDbType.VarBinary).Value = (object)xAl.CopiaDni.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@PPTamanioBytes", xAl.PermisoPublicidad.TamanioBytes);
                        cmd.Parameters.AddWithValue("@PPNombre", xAl.PermisoPublicidad.NombreArchivo);
                        cmd.Parameters.Add("@PermisoPublicidad", SqlDbType.VarBinary).Value = (object)xAl.PermisoPublicidad.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@CSTamanioBytes", xAl.CarnetSeguro.TamanioBytes);
                        cmd.Parameters.AddWithValue("@CSNombre", xAl.CarnetSeguro.NombreArchivo);
                        cmd.Parameters.Add("@CarnetSeguro", SqlDbType.VarBinary).Value = (object)xAl.CarnetSeguro.Archivo ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void ModificarAlumno(clsEntidades.clsAlumno xAl)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_alumnos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Alumno", xAl.Id);
                        cmd.Parameters.AddWithValue("@Id_Apoderado", xAl.Apoderado.Id);
                        cmd.Parameters.AddWithValue("@Id_TipoDocumento", xAl.TipoDocumento.Id);
                        cmd.Parameters.AddWithValue("@Nombres", xAl.Nombres);
                        cmd.Parameters.AddWithValue("@ApPaterno", xAl.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApMaterno", xAl.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Documento", xAl.Documento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", xAl.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Sexo", xAl.Sexo);
                        cmd.Parameters.AddWithValue("@Activo", xAl.Activo);

                        cmd.Parameters.AddWithValue("@FoTamanioBytes", xAl.Fotos.TamanioBytes);
                        cmd.Parameters.AddWithValue("@FoNombre", xAl.Fotos.NombreArchivo);
                        cmd.Parameters.Add("@Fotos", SqlDbType.VarBinary).Value = (object)xAl.Fotos.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@CDTamanioBytes", xAl.CopiaDni.TamanioBytes);
                        cmd.Parameters.AddWithValue("@CDNombre", xAl.CopiaDni.NombreArchivo);
                        cmd.Parameters.Add("@CopiaDni", SqlDbType.VarBinary).Value = (object)xAl.CopiaDni.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@PPTamanioBytes", xAl.PermisoPublicidad.TamanioBytes);
                        cmd.Parameters.AddWithValue("@PPNombre", xAl.PermisoPublicidad.NombreArchivo);
                        cmd.Parameters.Add("@PermisoPublicidad", SqlDbType.VarBinary).Value = (object)xAl.PermisoPublicidad.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@CSTamanioBytes", xAl.CarnetSeguro.TamanioBytes);
                        cmd.Parameters.AddWithValue("@CSNombre", xAl.CarnetSeguro.NombreArchivo);
                        cmd.Parameters.Add("@CarnetSeguro", SqlDbType.VarBinary).Value = (object)xAl.CarnetSeguro.Archivo ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public List<clsAlumno> listar_alumnos_Combo()
        {
            List<clsAlumno> lista = new List<clsAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand("listar_alumnos_Combo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsAlumno a = new clsAlumno();

                            a.Id = Convert.ToInt32(dr["Id_Alumno"]);
                            a.NombreCompleto = dr["NombreCompleto"].ToString();

                            lista.Add(a);
                        }
                    }
                }
            }

            return lista;
        }

    }
}
