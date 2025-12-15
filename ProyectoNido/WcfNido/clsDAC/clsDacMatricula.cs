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
    public class clsDacMatricula
    {
        //  1) LISTA RESUMEN PARA LA GRILLA
        public List<clsEntidades.clsMatricula> Nido_Matricula_Listar(char? estado = null)
        {
            var lista = new List<clsEntidades.clsMatricula>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("Nido_Matricula_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (estado.HasValue)
                    cmd.Parameters.AddWithValue("@Estado", estado.Value);
                else
                    cmd.Parameters.AddWithValue("@Estado", DBNull.Value);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var m = new clsEntidades.clsMatricula
                        {
                            Id_Matricula = Convert.ToInt32(dr["Id_Matricula"]),
                            Codigo = dr["Codigo"].ToString(),
                            Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]),
                            AlumnoNombre = dr["AlumnoNombre"].ToString(),
                            Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]),
                            GrupoNombre = dr["GrupoNombre"].ToString(),
                            Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                            NombreTarifario = dr["NombreTarifario"].ToString(),
                            Total = dr["Total"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Total"]),
                            Estado = dr["Estado"].ToString()
                        };

                        lista.Add(m);
                    }
                }
            }

            return lista;
        }


        //  2) LISTAR CON FILTROS (para consultas)
        public List<clsEntidades.clsMatricula> Matricula_Listar(
            int? idMatricula = null,
            int? idAlumno = null,
            int? idGrupoAnual = null,
            char? estado = null)
        {
            var lista = new List<clsEntidades.clsMatricula>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("Matricula_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Matricula",
                    (object)idMatricula ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Alumno",
                    (object)idAlumno ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_GrupoAnual",
                    (object)idGrupoAnual ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado",
                    estado.HasValue ? (object)estado.Value : DBNull.Value);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var m = new clsEntidades.clsMatricula
                        {
                            Id_Matricula = Convert.ToInt32(dr["Id_Matricula"]),
                            Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]),
                            Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]),
                            Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                            Codigo = dr["Codigo"].ToString(),
                            FechaMatricula = (DateTime)(dr["FechaMatricula"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(dr["FechaMatricula"])),
                            SubTotal = dr["SubTotal"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(dr["SubTotal"]),
                            DescuentoTotal = dr["DescuentoTotal"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(dr["DescuentoTotal"]),
                            Total = dr["Total"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(dr["Total"]),
                            Estado = dr["Estado"].ToString(),
                            Observacion = dr["Observacion"].ToString()
                        };

                        lista.Add(m);
                    }
                }
            }

            return lista;
        }

        //  3) OBTENER UNA MATRÍCULA POR ID
        public clsEntidades.clsMatricula Matricula_Obtener(int idMatricula)
        {
            var lista = Matricula_Listar(idMatricula, null, null, null);
            if (lista.Count > 0)
                return lista[0];
            return null;
        }

        //  4) INSERTAR MATRÍCULA

        public int Matricula_Insertar(clsEntidades.clsMatricula mat)
        {
            int idGenerado = 0;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("Matricula_Insertar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // OUTPUT
                SqlParameter pId = new SqlParameter("@Id_Matricula", SqlDbType.Int);
                pId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pId);

                cmd.Parameters.AddWithValue("@Id_Alumno", mat.Id_Alumno);
                cmd.Parameters.AddWithValue("@Id_GrupoAnual", mat.Id_GrupoAnual);
                cmd.Parameters.AddWithValue("@Id_Tarifario", mat.Id_Tarifario);
                cmd.Parameters.AddWithValue("@Codigo", mat.Codigo ?? (object)DBNull.Value);

                cmd.Parameters.AddWithValue("@FechaMatricula",
                    (object)mat.FechaMatricula ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubTotal",
                    (object)mat.SubTotal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DescuentoTotal",
                    (object)mat.DescuentoTotal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Total",
                    (object)mat.Total ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado",
                    string.IsNullOrEmpty(mat.Estado) ? "A" : mat.Estado);
                cmd.Parameters.AddWithValue("@Observacion",
                    (object)mat.Observacion ?? DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();

                if (pId.Value != DBNull.Value)
                    idGenerado = Convert.ToInt32(pId.Value);
            }

            return idGenerado;
        }

        //ACTUALIZAR MATRÍCULA
        public bool Matricula_Actualizar(clsEntidades.clsMatricula mat)
        {
            int filas = 0;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("Matricula_Actualizar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Matricula", mat.Id_Matricula);
                cmd.Parameters.AddWithValue("@Id_Alumno", mat.Id_Alumno);
                cmd.Parameters.AddWithValue("@Id_GrupoAnual", mat.Id_GrupoAnual);
                cmd.Parameters.AddWithValue("@Id_Tarifario", mat.Id_Tarifario);
                cmd.Parameters.AddWithValue("@Codigo", mat.Codigo ?? (object)DBNull.Value);

                cmd.Parameters.AddWithValue("@FechaMatricula",
                    (object)mat.FechaMatricula ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubTotal",
                    (object)mat.SubTotal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DescuentoTotal",
                    (object)mat.DescuentoTotal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Total",
                    (object)mat.Total ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", mat.Estado);
                cmd.Parameters.AddWithValue("@Observacion",
                    (object)mat.Observacion ?? DBNull.Value);

                cn.Open();
                filas = cmd.ExecuteNonQuery();
            }

            return filas > 0;
        }

        // ======================================
        //  6) CAMBIAR ESTADO (Anular / Activar)
        //     Usa Matricula_CambiarEstado
        // ======================================
        public bool Matricula_CambiarEstado(int idMatricula, char estado)
        {
            int filas = 0;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("Matricula_CambiarEstado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Matricula", idMatricula);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cn.Open();
                filas = cmd.ExecuteNonQuery();
            }

            return filas > 0;
        }

        // ======================================
        //  7) OBTENER MATRÍCULA ACTUAL POR ALUMNO (para aplicación web)
        // ======================================
        public clsEntidades.clsMatricula ObtenerMatriculaActualPorAlumno(int idAlumno)
        {
            clsEntidades.clsMatricula mat = null;

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
                        mat = new clsEntidades.clsMatricula
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

            return mat ?? new clsEntidades.clsMatricula { Id_Matricula = 0 };
        }

        // ======================================
        //  8) RESUMEN CUOTAS POR MATRÍCULA (para aplicación web)
        // ======================================
        public clsEntidades.clsResumenCuotas ResumenCuotasPorMatricula(int idMatricula)
        {
            clsEntidades.clsResumenCuotas res = new clsEntidades.clsResumenCuotas();

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

        // ======================================
        //  9) LISTAR CUOTAS POR MATRÍCULA (para aplicación web)
        // ======================================
        public List<clsEntidades.clsMatriculaDetalle> ListarCuotasPorMatricula(int idMatricula)
        {
            List<clsEntidades.clsMatriculaDetalle> lista = new List<clsEntidades.clsMatriculaDetalle>();

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
                        lista.Add(new clsEntidades.clsMatriculaDetalle
                        {
                            Id_MatriculaDetalle = Convert.ToInt32(dr["Id_MatriculaDetalle"]),
                            Id_Matricula = idMatricula, // Usar el parámetro ya que el SP no retorna esta columna
                            NroCuota = dr["NroCuota"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["NroCuota"]),
                            NombreCuota = dr["NombreCuota"]?.ToString() ?? "",
                            FechaVencimiento = dr["FechaVencimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaVencimiento"]),
                            TotalLinea = dr["TotalLinea"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TotalLinea"]),
                            FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaPago"]),
                            EstadoPago = dr["EstadoPago"]?.ToString() ?? "",
                            EstadoTexto = dr["EstadoTexto"]?.ToString() ?? ""
                            // Nota: El SP no retorna: Cantidad, Monto, Descuento, Adicional, Observacion
                            // Estos campos quedarán con sus valores por defecto (0 para decimal/int, "" para string)
                        });
                    }
                }
            }

            return lista;
        }

    }
}
