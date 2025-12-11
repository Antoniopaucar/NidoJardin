using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using clsEntidades;

namespace clsDAC
{
    public class clsDACGrupoAnual
    {
        public List<clsGrupoAnualDetalle> ListarGruposPorDocente(int idUsuario)
        {
            List<clsGrupoAnualDetalle> lista = new List<clsGrupoAnualDetalle>();
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ListarGruposPorDocente", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsGrupoAnualDetalle grupo = new clsGrupoAnualDetalle
                                {
                                    Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]),
                                    Nivel = dr["Nivel"] != DBNull.Value ? dr["Nivel"].ToString() : string.Empty,
                                    Salon = dr["Salon"] != DBNull.Value ? dr["Salon"].ToString() : string.Empty,
                                    Aforo = dr["Aforo"] != DBNull.Value ? Convert.ToInt32(dr["Aforo"]) : 0,
                                    Periodo = dr["Periodo"] != DBNull.Value ? Convert.ToInt32(dr["Periodo"]) : 0,
                                    TotalAlumnos = dr["TotalAlumnos"] != DBNull.Value ? Convert.ToInt32(dr["TotalAlumnos"]) : 0
                                };
                                lista.Add(grupo);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar grupos por docente: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar grupos: {ex.Message}", ex);
            }
            return lista;
        }

        //----------------------------clsGrupoAnual------------------------------------
        public List<clsEntidades.clsGrupoAnual> ListarGrupoAnual()
        {
            List<clsEntidades.clsGrupoAnual> lista = new List<clsEntidades.clsGrupoAnual>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("Nido_GrupoAnual_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var x = new clsEntidades.clsGrupoAnual
                        {
                            Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]),
                            Id_Salon = Convert.ToInt32(dr["Id_Salon"]),
                            Id_Profesor = Convert.ToInt32(dr["Id_Profesor"]),
                            Id_Nivel = Convert.ToInt32(dr["Id_Nivel"]),
                            Periodo = Convert.ToInt32(dr["Periodo"]),
                            Descripcion = dr["Descripcion"].ToString()
                        };
                        lista.Add(x);
                    }
                }
            }

            return lista;
        }
        public List<clsGrupoAnual> listar_grupo_anual_combo()
        {
            List<clsGrupoAnual> lista = new List<clsGrupoAnual>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand("listar_grupo_anual_combo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsGrupoAnual obj = new clsGrupoAnual();

                            obj.Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]);
                            obj.NombreGrupo = dr["NombreGrupo"].ToString();

                            // 👇 Solo si más adelante tu SP devuelve estas columnas:
                            if (HasColumn(dr, "Id_Salon") && !dr.IsDBNull(dr.GetOrdinal("Id_Salon")))
                                obj.Id_Salon = Convert.ToInt32(dr["Id_Salon"]);

                            if (HasColumn(dr, "Id_Profesor") && !dr.IsDBNull(dr.GetOrdinal("Id_Profesor")))
                                obj.Id_Profesor = Convert.ToInt32(dr["Id_Profesor"]);

                            if (HasColumn(dr, "Id_Nivel") && !dr.IsDBNull(dr.GetOrdinal("Id_Nivel")))
                                obj.Id_Nivel = Convert.ToInt32(dr["Id_Nivel"]);

                            if (HasColumn(dr, "Periodo") && !dr.IsDBNull(dr.GetOrdinal("Periodo")))
                                obj.Periodo = Convert.ToInt32(dr["Periodo"]);

                            if (HasColumn(dr, "Descripcion") && !dr.IsDBNull(dr.GetOrdinal("Descripcion")))
                                obj.Descripcion = dr["Descripcion"].ToString();

                            lista.Add(obj);
                        }
                    }
                }
            }

            return lista;

        }

        // Utilidad para evitar errores si luego agregas/quitas columnas del SP
        private bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
