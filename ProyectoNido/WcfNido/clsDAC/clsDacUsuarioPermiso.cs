using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacUsuarioPermiso
    {
        public List<clsEntidades.clsUsuarioPermiso> listarUsuarioPermiso()
        {
            List<clsEntidades.clsUsuarioPermiso> lista = new List<clsEntidades.clsUsuarioPermiso>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_usuario_permiso", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsUsuarioPermiso up = new clsEntidades.clsUsuarioPermiso();
                            up.Usuario = new clsEntidades.clsUsuario();
                            up.Permiso = new clsEntidades.clsPermiso();

                            up.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                            up.Usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            up.Permiso.Id = Convert.ToInt32(dr["Id_Permiso"]);
                            up.Permiso.NombrePermiso = dr["NombrePermiso"].ToString();
                            up.Tipo = dr["TipoPermiso"].ToString();

                            lista.Add(up);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarUsuarioPermiso(int id_user, int id_permiso)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_usuario_permiso", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Usuario", id_user);
                        cmd.Parameters.AddWithValue("@Id_Permiso", id_permiso);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void InsertarUsuarioPermiso(clsEntidades.clsUsuarioPermiso xUp)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_usuario_permiso", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Usuario", xUp.Usuario.Id);
                        cmd.Parameters.AddWithValue("@Id_Permiso", xUp.Permiso.Id);
                        cmd.Parameters.AddWithValue("@Tipo", xUp.Tipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
    }
}
