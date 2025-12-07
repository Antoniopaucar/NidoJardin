using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacNivel
    {
        public List<clsEntidades.clsNivel> listarNivel()
        {
            List<clsEntidades.clsNivel> lista = new List<clsEntidades.clsNivel>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_nivel", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsNivel n = new clsEntidades.clsNivel();

                            n.Id = Convert.ToInt32(dr["Id_Nivel"]);
                            n.Nombre = dr["Nombre"].ToString();
                            n.Descripcion = dr["Descripcion"].ToString();

                            lista.Add(n);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarNivel(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_nivel", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarNivel(clsEntidades.clsNivel xNiv)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_nivel", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", xNiv.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", xNiv.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarNivel(clsEntidades.clsNivel xNiv)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_nivel", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", xNiv.Id);
                    cmd.Parameters.AddWithValue("@Nombre", xNiv.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", xNiv.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
