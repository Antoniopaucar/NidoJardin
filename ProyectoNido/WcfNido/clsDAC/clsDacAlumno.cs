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

        public List<clsEntidades.clsAlumno> listarAlumnosActivos()
        {
            List<clsEntidades.clsAlumno> lista = new List<clsEntidades.clsAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarAlumnosActivos", cn))
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
                            // En el nuevo SP, NombreCompleto es del Apoderado
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

                            // Construir NombreCompleto del Alumno para la Grilla
                            a.NombreCompleto = a.Nombres + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno;

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

        /// <summary>
        /// Lista alumnos matriculados en un grupo anual específico
        /// </summary>
        public List<clsEntidades.clsAlumno> ListarAlumnosPorGrupoAnual(int idGrupoAnual)
        {
            List<clsEntidades.clsAlumno> lista = new List<clsEntidades.clsAlumno>();
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ListarAlumnosPorGrupoAnual", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_GrupoAnual", idGrupoAnual);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsEntidades.clsAlumno alumno = new clsEntidades.clsAlumno
                                {
                                    Id = Convert.ToInt32(dr["Id_Alumno"]),
                                    Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty,
                                    ApellidoPaterno = dr["ApPaterno"] != DBNull.Value ? dr["ApPaterno"].ToString() : string.Empty,
                                    ApellidoMaterno = dr["ApMaterno"] != DBNull.Value ? dr["ApMaterno"].ToString() : string.Empty,
                                    Documento = dr["Documento"] != DBNull.Value ? dr["Documento"].ToString() : string.Empty,
                                    FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento")),
                                    Sexo = dr["Sexo"] != DBNull.Value ? dr["Sexo"].ToString() : string.Empty
                                };
                                
                                // Construir nombre completo
                                alumno.NombreCompleto = $"{alumno.Nombres} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}".Trim();
                                
                                lista.Add(alumno);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar alumnos por grupo anual: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar alumnos: {ex.Message}", ex);
            }
            return lista;
        }

        /// <summary>
        /// Lista alumnos inscritos en un grupo de servicio específico
        /// </summary>
        public List<clsEntidades.clsAlumno> ListarAlumnosPorGrupoServicio(int idGrupoServicio)
        {
            List<clsEntidades.clsAlumno> lista = new List<clsEntidades.clsAlumno>();
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ListarAlumnosPorGrupoServicio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_GrupoServicio", idGrupoServicio);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsEntidades.clsAlumno alumno = new clsEntidades.clsAlumno
                                {
                                    Id = Convert.ToInt32(dr["Id_Alumno"]),
                                    Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty,
                                    ApellidoPaterno = dr["ApPaterno"] != DBNull.Value ? dr["ApPaterno"].ToString() : string.Empty,
                                    ApellidoMaterno = dr["ApMaterno"] != DBNull.Value ? dr["ApMaterno"].ToString() : string.Empty,
                                    Documento = dr["Documento"] != DBNull.Value ? dr["Documento"].ToString() : string.Empty,
                                    FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento")),
                                    Sexo = dr["Sexo"] != DBNull.Value ? dr["Sexo"].ToString() : string.Empty
                                };
                                
                                // Construir nombre completo
                                alumno.NombreCompleto = $"{alumno.Nombres} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}".Trim();
                                
                                lista.Add(alumno);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar alumnos por grupo de servicio: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar alumnos: {ex.Message}", ex);
            }
            return lista;
        }

        /// <summary>
        /// Lista alumnos (hijos) de un apoderado específico
        /// </summary>
        public List<clsEntidades.clsAlumno> ListarAlumnosPorApoderado(int idApoderado)
        {
            List<clsEntidades.clsAlumno> lista = new List<clsEntidades.clsAlumno>();
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ListarAlumnosPorApoderado", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Apoderado", idApoderado);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsEntidades.clsAlumno alumno = new clsEntidades.clsAlumno
                                {
                                    Id = Convert.ToInt32(dr["Id"]), // Corregido: el SP devuelve "Id" no "Id_Alumno"
                                    Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty,
                                    ApellidoPaterno = dr["ApellidoPaterno"] != DBNull.Value ? dr["ApellidoPaterno"].ToString() : string.Empty, // Corregido: usa alias del SP
                                    ApellidoMaterno = dr["ApellidoMaterno"] != DBNull.Value ? dr["ApellidoMaterno"].ToString() : string.Empty, // Corregido: usa alias del SP
                                    Documento = dr["Documento"] != DBNull.Value ? dr["Documento"].ToString() : string.Empty,
                                    FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento")),
                                    Sexo = dr["Sexo"] != DBNull.Value ? dr["Sexo"].ToString() : string.Empty,
                                    Activo = dr["Activo"] != DBNull.Value ? Convert.ToBoolean(dr["Activo"]) : true
                                };
                                
                                // Inicializar y mapear TipoDocumento
                                alumno.TipoDocumento = new clsTipoDocumento();
                                if (dr["Id_TipoDocumento"] != DBNull.Value)
                                {
                                    alumno.TipoDocumento.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                                    alumno.TipoDocumento.Nombre = dr["NombreTipoDocumento"] != DBNull.Value ? dr["NombreTipoDocumento"].ToString() : string.Empty;
                                }
                                
                                // Construir nombre completo
                                alumno.NombreCompleto = $"{alumno.Nombres} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}".Trim();
                                
                                lista.Add(alumno);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar alumnos por apoderado: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar alumnos: {ex.Message}", ex);
            }
            return lista;
        }

        public List<clsAlumnoCombo> buscarAlumno(string texto)
        {
            var lista = new List<clsAlumnoCombo>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("buscarAlumno", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Texto", SqlDbType.VarChar, 100).Value = texto;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new clsAlumnoCombo
                            {
                                Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Documento = dr["Documento"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }


    }
}
