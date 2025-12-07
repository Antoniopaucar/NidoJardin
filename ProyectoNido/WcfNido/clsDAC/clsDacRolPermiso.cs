using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacRolPermiso
    {
        public List<clsEntidades.clsRolPermiso> listarRolPermiso()
        {
            List<clsEntidades.clsRolPermiso> lista = new List<clsEntidades.clsRolPermiso>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_rol_permiso", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsRolPermiso rp = new clsEntidades.clsRolPermiso();
                            rp.Permiso = new clsEntidades.clsPermiso();
                            rp.Rol = new clsEntidades.clsRol();

                            rp.Permiso.Id = Convert.ToInt32(dr["Id_Permiso"]);
                            rp.Permiso.NombrePermiso = dr["NombrePermiso"].ToString();
                            rp.Rol.Id = Convert.ToInt32(dr["Id_Rol"]);
                            rp.Rol.NombreRol = dr["NombreRol"].ToString();
                            rp.Tipo = dr["Tipo"].ToString();

                            lista.Add(rp);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarRolPermiso(int id_rol,int id_permiso)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_rol_permiso", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Permiso", id_permiso);
                        cmd.Parameters.AddWithValue("@Id_Rol", id_rol);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void InsertarRolPermiso(clsEntidades.clsRolPermiso xRp)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_rol_permiso", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Permiso", xRp.Permiso.Id);
                        cmd.Parameters.AddWithValue("@Id_Rol", xRp.Rol.Id);
                        cmd.Parameters.AddWithValue("@Tipo", xRp.Tipo);

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
