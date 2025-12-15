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
    public class clsDacGrupoAnual_v
    {
        public List<clsGrupoAnual_v> ListarGrupoAnual()
        {
            var lista = new List<clsGrupoAnual_v>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_grupoAnual", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new clsGrupoAnual_v
                            {
                                Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]),
                                Id_Salon = Convert.ToInt32(dr["Id_Salon"]),
                                NombreSalon = dr["NombreSalon"].ToString(),
                                Aforo = Convert.ToInt32(dr["Aforo"]),
                                Id_Profesor = Convert.ToInt32(dr["Id_Profesor"]),
                                NombreProfesor = dr["NombreProfesor"].ToString(),
                                Id_Nivel = Convert.ToInt32(dr["Id_Nivel"]),
                                NombreNivel = dr["NombreNivel"].ToString(),
                                Periodo = Convert.ToInt16(dr["Periodo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
