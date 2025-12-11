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
    public class clsDACGrupoServicio
    {
        public List<clsGrupoServicioDetalle> ListarGruposServicioPorDocente(int idUsuario)
        {
            List<clsGrupoServicioDetalle> lista = new List<clsGrupoServicioDetalle>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarGruposServicioPorDocente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsGrupoServicioDetalle grupo = new clsGrupoServicioDetalle
                            {
                                Id_GrupoServicio = Convert.ToInt32(dr["Id_GrupoServicio"]),
                                Servicio = dr["Servicio"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Salon = dr["Salon"].ToString(),
                                Aforo = Convert.ToInt32(dr["Aforo"]),
                                Periodo = Convert.ToInt32(dr["Periodo"]),
                                TotalAlumnos = Convert.ToInt32(dr["TotalAlumnos"])
                            };
                            lista.Add(grupo);
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Lista todas las ofertas de grupos de servicio del periodo actual
        /// con información del docente para apoderados
        /// </summary>
        public List<clsGrupoServicioOferta> ListarOfertasGrupoServicio()
        {
            List<clsGrupoServicioOferta> lista = new List<clsGrupoServicioOferta>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarOfertasGrupoServicio", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsGrupoServicioOferta oferta = new clsGrupoServicioOferta
                            {
                                Id_GrupoServicio = Convert.ToInt32(dr["Id_GrupoServicio"]),
                                Servicio = dr["Servicio"] != DBNull.Value ? dr["Servicio"].ToString() : string.Empty,
                                Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty,
                                Tipo = dr["Tipo"] != DBNull.Value ? dr["Tipo"].ToString() : string.Empty,
                                Costo = dr["Costo"] != DBNull.Value ? Convert.ToDecimal(dr["Costo"]) : 0,
                                Salon = dr["Salon"] != DBNull.Value ? dr["Salon"].ToString() : string.Empty,
                                Aforo = dr["Aforo"] != DBNull.Value ? Convert.ToInt32(dr["Aforo"]) : 0,
                                Periodo = dr["Periodo"] != DBNull.Value ? Convert.ToInt32(dr["Periodo"]) : 0,
                                TotalAlumnos = dr["TotalAlumnos"] != DBNull.Value ? Convert.ToInt32(dr["TotalAlumnos"]) : 0,
                                NombreDocente = dr["NombreDocente"] != DBNull.Value ? dr["NombreDocente"].ToString() : string.Empty,
                                ApellidoPaternoDocente = dr["ApellidoPaternoDocente"] != DBNull.Value ? dr["ApellidoPaternoDocente"].ToString() : string.Empty,
                                ApellidoMaternoDocente = dr["ApellidoMaternoDocente"] != DBNull.Value ? dr["ApellidoMaternoDocente"].ToString() : string.Empty
                            };
                            lista.Add(oferta);
                        }
                    }
                }
            }
            return lista;
        }
    }
}

