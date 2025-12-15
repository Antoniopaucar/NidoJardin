using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacServicioAlumno
    {
        // =====================================================
        // ============== LISTAR ===============================
        // =====================================================
        public List<clsServicioAlumno> ListarServicioAlumno()
        {
            List<clsServicioAlumno> lista = new List<clsServicioAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_ServicioAlumno", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsServicioAlumno x = new clsServicioAlumno();

                            x.Id_ServicioAlumno = Convert.ToInt32(dr["Id_ServicioAlumno"]);
                            x.Id_GrupoServicio = Convert.ToInt32(dr["Id_GrupoServicio"]);
                            x.Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]);

                            x.FechaInicio = dr["FechaInicio"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaInicio"])
                                : (DateTime?)null;

                            x.FechaFinal = dr["FechaFinal"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaFinal"])
                                : (DateTime?)null;

                            x.FechaPago = dr["FechaPago"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaPago"])
                                : (DateTime?)null;

                            x.HoraInicio = dr["HoraInicio"] != DBNull.Value
                                ? (TimeSpan?)dr["HoraInicio"]
                                : (TimeSpan?)null;

                            x.HoraFinal = dr["HoraFinal"] != DBNull.Value
                                ? (TimeSpan?)dr["HoraFinal"]
                                : (TimeSpan?)null;

                            x.Monto = dr["Monto"] != DBNull.Value
                                ? Convert.ToDecimal(dr["Monto"])
                                : (decimal?)null;

                            // Campos adicionales del JOIN
                            x.NombreAlumno = dr["NombreAlumno"].ToString();
                            x.NombreGrupo = dr["NombreGrupo"].ToString();

                            lista.Add(x);
                        }
                    }
                }
            }

            return lista;
        }

        // =====================================================
        // ============== INSERTAR =============================
        // =====================================================
        public void InsertarServicioAlumno(clsServicioAlumno x)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_ServicioAlumno", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_GrupoServicio", x.Id_GrupoServicio);
                    cmd.Parameters.AddWithValue("@Id_Alumno", x.Id_Alumno);

                    cmd.Parameters.AddWithValue("@FechaInicio",
                        x.FechaInicio.HasValue ? (object)x.FechaInicio.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@FechaFinal",
                        x.FechaFinal.HasValue ? (object)x.FechaFinal.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@FechaPago",
                        x.FechaPago.HasValue ? (object)x.FechaPago.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@HoraInicio",
                        x.HoraInicio.HasValue ? (object)x.HoraInicio.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@HoraFinal",
                        x.HoraFinal.HasValue ? (object)x.HoraFinal.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Monto",
                        x.Monto.HasValue ? (object)x.Monto.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =====================================================
        // ============== MODIFICAR ============================
        // =====================================================
        public void ModificarServicioAlumno(clsServicioAlumno x)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_ServicioAlumno", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_ServicioAlumno", x.Id_ServicioAlumno);
                    cmd.Parameters.AddWithValue("@Id_GrupoServicio", x.Id_GrupoServicio);
                    cmd.Parameters.AddWithValue("@Id_Alumno", x.Id_Alumno);

                    cmd.Parameters.AddWithValue("@FechaInicio",
                        x.FechaInicio.HasValue ? (object)x.FechaInicio.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@FechaFinal",
                        x.FechaFinal.HasValue ? (object)x.FechaFinal.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@FechaPago",
                        x.FechaPago.HasValue ? (object)x.FechaPago.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@HoraInicio",
                        x.HoraInicio.HasValue ? (object)x.HoraInicio.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@HoraFinal",
                        x.HoraFinal.HasValue ? (object)x.HoraFinal.Value : DBNull.Value);

                    cmd.Parameters.AddWithValue("@Monto",
                        x.Monto.HasValue ? (object)x.Monto.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =====================================================
        // ============== ELIMINAR =============================
        // =====================================================
        public void EliminarServicioAlumno(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_ServicioAlumno", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_ServicioAlumno", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =====================================================
        // ============== LISTAR POR ALUMNO (para aplicación web) ===============================
        // =====================================================
        public List<clsServicioAlumno> ListarServicioAlumnoPorAlumno(int idAlumno)
        {
            List<clsServicioAlumno> lista = new List<clsServicioAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_ServicioAlumnoPorAlumno", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Alumno", idAlumno);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsServicioAlumno x = new clsServicioAlumno();

                            x.Id_ServicioAlumno = Convert.ToInt32(dr["Id_ServicioAlumno"]);
                            x.Id_GrupoServicio = Convert.ToInt32(dr["Id_GrupoServicio"]);
                            x.Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]);

                            x.FechaInicio = dr["FechaInicio"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaInicio"])
                                : (DateTime?)null;

                            x.FechaFinal = dr["FechaFinal"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaFinal"])
                                : (DateTime?)null;

                            x.FechaPago = dr["FechaPago"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaPago"])
                                : (DateTime?)null;

                            x.HoraInicio = dr["HoraInicio"] != DBNull.Value
                                ? (TimeSpan?)dr["HoraInicio"]
                                : (TimeSpan?)null;

                            x.HoraFinal = dr["HoraFinal"] != DBNull.Value
                                ? (TimeSpan?)dr["HoraFinal"]
                                : (TimeSpan?)null;

                            x.Monto = dr["Monto"] != DBNull.Value
                                ? Convert.ToDecimal(dr["Monto"])
                                : (decimal?)null;

                            // Campos adicionales del JOIN
                            x.NombreAlumno = dr["NombreAlumno"]?.ToString() ?? "";
                            x.NombreGrupo = dr["NombreGrupo"]?.ToString() ?? "";

                            lista.Add(x);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
