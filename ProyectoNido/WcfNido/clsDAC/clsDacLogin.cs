using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacLogin
    {
        public List<clsEntidades.clsLogin> UsuarioExiste(string usuario, string clave)
        {
            List<clsEntidades.clsLogin> lista = new List<clsEntidades.clsLogin>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("ValidarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario);
                    cmd.Parameters.AddWithValue("@Clave", clave);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var login = new clsEntidades.clsLogin
                            {
                                Usuario = usuario,
                                Exito = Convert.ToBoolean(dr["Exito"]),
                                Mensaje = dr["Mensaje"].ToString(),
                                Id_Usuario = dr["Id_Usuario"] != DBNull.Value ? Convert.ToInt32(dr["Id_Usuario"]) : 0,
                                Id_Rol = dr["Id_Rol"] != DBNull.Value ? Convert.ToInt32(dr["Id_Rol"]) : (int?)null,
                                Id_Permiso = dr["Id_Permiso"] != DBNull.Value ? Convert.ToInt32(dr["Id_Permiso"]) : (int?)null
                            };
                            lista.Add(login);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
