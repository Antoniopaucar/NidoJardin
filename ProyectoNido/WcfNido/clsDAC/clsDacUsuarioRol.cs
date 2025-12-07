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
    public class clsDacUsuarioRol
    {
        public List<clsEntidades.clsUsuarioRol> listarUsuarioRol()
        {
            List<clsEntidades.clsUsuarioRol> lista = new List<clsEntidades.clsUsuarioRol>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_usuario_rol", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsUsuarioRol ur = new clsEntidades.clsUsuarioRol();
                            ur.Usuario = new clsEntidades.clsUsuario();
                            ur.Rol = new clsEntidades.clsRol();

                            ur.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                            ur.Usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            ur.Rol.Id = Convert.ToInt32(dr["Id_Rol"]);
                            ur.Rol.NombreRol = dr["NombreRol"].ToString();

                            lista.Add(ur);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarUsuarioRol(int id_user,int id_rol)
        {
            try 
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_usuario_rol", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Usuario", id_user);
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

        public void InsertarUsuarioRol(clsUsuarioRol xUr, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("insertar_usuario_rol", cn, trx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Usuario", xUr.Usuario.Id);
                    cmd.Parameters.AddWithValue("@Id_Rol", xUr.Rol.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        //public void InsertarUsuarioRol(clsEntidades.clsUsuarioRol xUr)
        //{
        //    try 
        //    {
        //        using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand("insertar_usuario_rol", cn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@Id_Usuario", xUr.Usuario.Id);
        //                cmd.Parameters.AddWithValue("@Id_Rol", xUr.Rol.Id);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (ArgumentException)
        //    {
        //        throw;
        //    }
        //}

    }
}
