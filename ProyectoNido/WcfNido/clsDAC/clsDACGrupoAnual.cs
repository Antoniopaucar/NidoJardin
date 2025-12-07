using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using clsEntidades;
using System.Configuration;

namespace clsDAC
{
    public class clsDACGrupoAnual
    {
        public List<GrupoAnualDetalle> ListarGruposPorDocente(int idUsuario)
        {
            List<GrupoAnualDetalle> lista = new List<GrupoAnualDetalle>();
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarGruposPorDocente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            GrupoAnualDetalle grupo = new GrupoAnualDetalle
                            {
                                Id_GrupoAnual = Convert.ToInt32(dr["Id_GrupoAnual"]),
                                Nivel = dr["Nivel"].ToString(),
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
