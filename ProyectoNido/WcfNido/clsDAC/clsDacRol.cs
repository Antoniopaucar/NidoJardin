using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacRol
    {
        public List<clsEntidades.clsRol> listarRol()
        {
            List<clsEntidades.clsRol> lista = new List<clsEntidades.clsRol>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_roles", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsRol r = new clsEntidades.clsRol();

                            r.Id = Convert.ToInt32(dr["Id_Rol"]);
                            r.NombreRol = dr["NombreRol"].ToString();
                            r.Descripcion = dr["Descripcion"].ToString();

                            lista.Add(r);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarRol(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_roles", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarRol(clsEntidades.clsRol xRol)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_roles", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreRol", xRol.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", xRol.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarRol(clsEntidades.clsRol xRol)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_roles", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", xRol.Id);
                    cmd.Parameters.AddWithValue("@NombreRol", xRol.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", xRol.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
