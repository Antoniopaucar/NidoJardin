using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacTarifario
    {
        public List<clsEntidades.clsTarifario> listar_tarifario_combo()
        {
            var lista = new List<clsEntidades.clsTarifario>();
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("listar_tarifario_combo", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var t = new clsEntidades.clsTarifario
                        {
                            Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                            Nombre = dr["Nombre"].ToString()
                        };
                        lista.Add(t);
                    }
                }
            }
            return lista;
        }


        public List<clsEntidades.clsTarifario> listar_tarifario()
        {
            var lista = new List<clsEntidades.clsTarifario>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("listar_tarifario", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var t = new clsEntidades.clsTarifario
                        {
                            Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                            Tipo = dr["Tipo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Periodo = (byte)Convert.ToInt32(dr["Periodo"]),
                            Valor = Convert.ToDecimal(dr["Valor"])
                        };
                        lista.Add(t);
                    }
                }
            }

            return lista;
        }
    }
}
