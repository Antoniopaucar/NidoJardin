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
        // ======================================
        //  1) LISTA RESUMEN PARA LA GRILLA
        //     Usa Nido_Matricula_Listar
        // ======================================
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

        // ======================================
        //  2) LISTAR CON FILTROS (para consultas)
        //     Usa Matricula_Listar
        // ======================================
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

        // ======================================
        //  3) OBTENER UNA MATRÍCULA POR ID
        //     (usa Matricula_Listar internamente)
        // ======================================
        public clsEntidades.clsMatricula Matricula_Obtener(int idMatricula)
        {
            var lista = Matricula_Listar(idMatricula, null, null, null);
            if (lista.Count > 0)
                return lista[0];
            return null;
        }

        // ======================================
        //  4) INSERTAR MATRÍCULA
        //     Usa Matricula_Insertar
        //     Devuelve el Id generado
        // ======================================
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

        // ======================================
        //  5) ACTUALIZAR MATRÍCULA
        //     Usa Matricula_Actualizar
        // ======================================
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
    }
}
