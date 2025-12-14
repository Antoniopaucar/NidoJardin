using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacGrupoServicio_v
    {


        // Método corregido:
        public List<clsEntidades.clsGrupoServicio> ListarGrupoServicio()
        {
            var lista = new List<clsEntidades.clsGrupoServicio>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_grupoServicio", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        int ordPeriodo = dr.GetOrdinal("Periodo");
                        while (dr.Read())
                        {
                            var gs = new clsEntidades.clsGrupoServicio
                            {
                                Id_GrupoServicio = Convert.ToInt32(dr["Id_GrupoServicio"]),
                                Id_Salon = Convert.ToInt32(dr["Id_Salon"]),
                                Id_Profesor = Convert.ToInt32(dr["Id_Profesor"]),
                                Id_ServicioAdicional = Convert.ToInt32(dr["Id_ServicioAdicional"]),
                                Periodo = dr["Periodo"] == DBNull.Value ? (short)0 : Convert.ToInt16(dr["Periodo"]),
                                NombreSalon = dr["NombreSalon"] == DBNull.Value ? "" : dr["NombreSalon"].ToString(),
                                NombreProfesor = dr["NombreProfesor"] == DBNull.Value ? "" : dr["NombreProfesor"].ToString(),
                                NombreServicio = dr["NombreServicio"] == DBNull.Value ? "" : dr["NombreServicio"].ToString()
                            };

                            lista.Add(gs);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarGrupoServicio(int idGrupoServicio)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_grupoServicio", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_GrupoServicio", idGrupoServicio);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarGrupoServicio(clsEntidades.clsGrupoServicio grupo)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_grupoServicio", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_Salon", grupo.Id_Salon);
                    cmd.Parameters.AddWithValue("@Id_Profesor", grupo.Id_Profesor);
                    cmd.Parameters.AddWithValue("@Id_ServicioAdicional", grupo.Id_ServicioAdicional);
                    cmd.Parameters.Add("@Periodo", SqlDbType.SmallInt).Value = grupo.Periodo;


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarGrupoServicio(clsEntidades.clsGrupoServicio grupo)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_grupoServicio", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_GrupoServicio", grupo.Id_GrupoServicio);
                    cmd.Parameters.AddWithValue("@Id_Salon", grupo.Id_Salon);
                    cmd.Parameters.AddWithValue("@Id_Profesor", grupo.Id_Profesor);
                    cmd.Parameters.AddWithValue("@Id_ServicioAdicional", grupo.Id_ServicioAdicional);
                    cmd.Parameters.Add("@Periodo", SqlDbType.SmallInt).Value = grupo.Periodo;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
