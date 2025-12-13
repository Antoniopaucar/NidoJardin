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
    public class clsDAC_Movil
    {
        public List<ComunicadoMovil> ListarComunicadosApoderado(int idUsuario)
        {
            List<ComunicadoMovil> lista = new List<ComunicadoMovil>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("mov_listar_comunicados_apoderado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ComunicadoMovil
                        {
                            Id_Comunicado = dr.GetInt32(dr.GetOrdinal("Id_Comunicado")),
                            Titulo = dr.GetString(dr.GetOrdinal("Titulo")),
                            Descripcion = dr.GetString(dr.GetOrdinal("Descripcion")),
                            FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion")),
                            Visto = dr.GetInt32(dr.GetOrdinal("Visto")) == 1,
                            EstadoTexto = dr.GetString(dr.GetOrdinal("EstadoTexto"))
                        });
                    }
                }
            }
            return lista;
        }
        // LOGIN APODERADO
        public List<E_MovLogin> LoginApoderado(string usuarioODocumento, string clave)
        {
            List<E_MovLogin> lista = new List<E_MovLogin>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("mov_login_apoderado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioODocumento", usuarioODocumento);
                cmd.Parameters.AddWithValue("@Clave", clave);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new E_MovLogin
                        {
                            Id_Usuario = dr.GetInt32(dr.GetOrdinal("Id_Usuario")),
                            NombreUsuario = dr["NombreUsuario"]?.ToString() ?? "",
                            Documento = dr["Documento"]?.ToString() ?? "",
                            NombreCompleto = dr["NombreCompleto"]?.ToString() ?? "",
                            Activo = Convert.ToInt32(dr["Activo"]) == 1,
                            Bloqueado = Convert.ToInt32(dr["Bloqueado"]) == 1,
                            Id_Rol = Convert.ToInt32(dr["Id_Rol"])
                        });
                    }
                }
            }

            return lista;
        }

        // COMBO LISTAR ALUMNOS POR APODERADO
        public List<clsAlumno> ListarHijosPorApoderado(int idApoderado)
        {
            List<clsAlumno> lista = new List<clsAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("mov_listar_hijos_por_apoderado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Apoderado", idApoderado);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new clsAlumno
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            NombreCompleto = dr["NombreCompleto"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        //OBTENER MATRICULA ACTUAL POR ALUMNO
        //Obtener matricula actual por alumno
        public clsMatricula ObtenerMatriculaActual(int idAlumno)
        {
            clsMatricula mat = null;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("mov_obtener_matricula_actual_por_alumno", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Alumno", idAlumno);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        mat = new clsMatricula
                        {
                            Id_Matricula = (int)dr["Id_Matricula"],
                            Id_Alumno = (int)dr["Id_Alumno"],
                            FechaMatricula = (DateTime)dr["FechaMatricula"],
                            Total = (decimal)dr["Total"],
                            Estado = dr["Estado"].ToString()
                        };
                    }
                }
            }

            return mat ?? new clsMatricula { Id_Matricula = 0 };
        }

        //RESUMEN CUOTAS POR MATRICULA

        public clsResumenCuotas ResumenCuotasPorMatricula(int idMatricula)
        {
            clsResumenCuotas res = new clsResumenCuotas();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("mov_resumen_cuotas_por_matricula", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Matricula", idMatricula);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        res.Total = dr["Total"] == DBNull.Value ? 0 : (decimal)dr["Total"];
                        res.Pagado = dr["Pagado"] == DBNull.Value ? 0 : (decimal)dr["Pagado"];
                        res.Pendiente = dr["Pendiente"] == DBNull.Value ? 0 : (decimal)dr["Pendiente"];
                    }
                }
            }

            return res;
        }

        // LISTAR CUOTAS POR MATRICULA
        public List<clsMatriculaDetalle> ListarCuotasPorMatricula(int idMatricula)
        {
            List<clsMatriculaDetalle> lista = new List<clsMatriculaDetalle>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("mov_listar_cuotas_por_matricula", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Matricula", idMatricula);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new clsMatriculaDetalle
                        {
                            Id_MatriculaDetalle = Convert.ToInt32(dr["Id_MatriculaDetalle"]),
                            NroCuota = dr["NroCuota"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NroCuota"]),
                            NombreCuota = dr["NombreCuota"]?.ToString() ?? "",
                            FechaVencimiento = dr["FechaVencimiento"] == DBNull.Value? DateTime.MinValue: Convert.ToDateTime(dr["FechaVencimiento"]),
                            TotalLinea = dr["TotalLinea"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TotalLinea"]),
                            FechaPago = dr["FechaPago"] == DBNull.Value? (DateTime?)null: Convert.ToDateTime(dr["FechaPago"]),
                            EstadoPago = dr["EstadoPago"]?.ToString() ?? "",
                            EstadoTexto = dr["EstadoTexto"]?.ToString() ?? ""
                        });
                    }
                }
            }

            return lista;
        }
    }
}
