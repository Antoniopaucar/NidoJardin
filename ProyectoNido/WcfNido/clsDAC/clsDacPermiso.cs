using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacPermiso
    {
        public List<clsEntidades.clsPermiso> listarPermiso()
        {
            List<clsEntidades.clsPermiso> lista = new List<clsEntidades.clsPermiso>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_permisos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsPermiso p = new clsEntidades.clsPermiso();

                            p.Id = Convert.ToInt32(dr["Id_Permiso"]);
                            p.NombrePermiso = dr["NombrePermiso"].ToString();
                            p.Descripcion = dr["Descripcion"].ToString();

                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarPermiso(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_permisos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarPermiso(clsEntidades.clsPermiso xPer)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_permisos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombrePermiso", xPer.NombrePermiso);
                    cmd.Parameters.AddWithValue("@Descripcion", xPer.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarPermiso(clsEntidades.clsPermiso xPer)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_permisos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", xPer.Id);
                    cmd.Parameters.AddWithValue("@NombrePermiso", xPer.NombrePermiso);
                    cmd.Parameters.AddWithValue("@Descripcion", xPer.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
