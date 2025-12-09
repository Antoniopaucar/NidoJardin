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
    }
}

