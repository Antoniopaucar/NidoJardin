using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacComunicado
    {
        public List<clsEntidades.clsComunicado> listarComunicados(int idUsuario)
        {
            List<clsEntidades.clsComunicado> lista = new List<clsEntidades.clsComunicado>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_comunicados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsComunicado c = new clsEntidades.clsComunicado();
                            c.Usuario = new clsEntidades.clsUsuario();
                            c.Rol= new clsEntidades.clsRol();

                            c.Id = Convert.ToInt32(dr["Id_Comunicado"]);
                            c.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                            c.Usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            c.Rol.Id = Convert.ToInt32(dr["Id_Rol"]);
                            c.Rol.NombreRol = dr["NombreRol"].ToString();
                            c.Nombre = dr["Nombre"].ToString();
                            c.Descripcion = dr["Descripcion"].ToString();
                            c.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                            c.FechaFinal = dr.IsDBNull(dr.GetOrdinal("FechaFinal")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaFinal"));

                            lista.Add(c);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarComunicado(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_comunicados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void InsertarComunicado(clsEntidades.clsComunicado xcom)
        {
            try
            { 
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_comunicados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Usuario", xcom.Usuario.Id);
                        cmd.Parameters.AddWithValue("@Id_Rol", xcom.Rol.Id);
                        cmd.Parameters.AddWithValue("@Nombre", xcom.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", xcom.Descripcion);
                        cmd.Parameters.AddWithValue("@FechaFinal", xcom.FechaFinal);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void ModificarComunicado(clsEntidades.clsComunicado xcom)
        {
            try
            { 
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_comunicados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", xcom.Id);
                        cmd.Parameters.AddWithValue("@Id_Usuario", xcom.Usuario.Id);
                        cmd.Parameters.AddWithValue("@Id_Rol", xcom.Rol.Id);
                        cmd.Parameters.AddWithValue("@Nombre", xcom.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", xcom.Descripcion);
                        cmd.Parameters.AddWithValue("@FechaFinal", xcom.FechaFinal);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        public void MarcarComunicadoVisto(int idComunicado, int idUsuario)
        {
            try
            { 
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_MarcarComunicadoVisto", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Comunicado", idComunicado);
                        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista comunicados dirigidos a los roles que tiene asignado un usuario
        /// Requiere el SP: listar_comunicados_por_rol_usuario
        /// </summary>
        public List<clsEntidades.clsComunicado> ListarComunicadosPorRolUsuario(int idUsuario)
        {
            List<clsEntidades.clsComunicado> lista = new List<clsEntidades.clsComunicado>();

            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("listar_comunicados_por_rol_usuario", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsEntidades.clsComunicado c = new clsEntidades.clsComunicado();
                                c.Usuario = new clsEntidades.clsUsuario();
                                c.Rol = new clsEntidades.clsRol();

                                c.Id = Convert.ToInt32(dr["Id_Comunicado"]);
                                c.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                                c.Usuario.NombreUsuario = dr["NombreUsuario"] != DBNull.Value ? dr["NombreUsuario"].ToString() : string.Empty;
                                c.Usuario.Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty;
                                c.Usuario.ApellidoPaterno = dr["ApPaterno"] != DBNull.Value ? dr["ApPaterno"].ToString() : string.Empty;
                                c.Rol.Id = Convert.ToInt32(dr["Id_Rol"]);
                                c.Rol.NombreRol = dr["NombreRol"] != DBNull.Value ? dr["NombreRol"].ToString() : string.Empty;
                                c.Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty;
                                c.Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty;
                                c.FechaCreacion = dr.IsDBNull(dr.GetOrdinal("FechaCreacion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                                c.FechaFinal = dr.IsDBNull(dr.GetOrdinal("FechaFinal")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaFinal"));
                                
                                // Manejar el campo Visto de forma segura
                                int vistoOrdinal = dr.GetOrdinal("Visto");
                                c.Visto = dr.IsDBNull(vistoOrdinal) ? false : Convert.ToBoolean(dr[vistoOrdinal]);

                                lista.Add(c);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar comunicados por rol de usuario: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar comunicados: {ex.Message}", ex);
            }

            return lista;
        }

        /// <summary>
        /// Lista comunicados dirigidos específicamente al rol PROFESOR
        /// Requiere el SP: listar_comunicados_por_rol_usuario_Profesor
        /// </summary>
        public List<clsEntidades.clsComunicado> ListarComunicadosPorRolUsuarioProfesor(int idUsuario)
        {
            List<clsEntidades.clsComunicado> lista = new List<clsEntidades.clsComunicado>();

            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("listar_comunicados_por_rol_usuario_Profesor", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsEntidades.clsComunicado c = new clsEntidades.clsComunicado();
                                c.Usuario = new clsEntidades.clsUsuario();
                                c.Rol = new clsEntidades.clsRol();

                                c.Id = Convert.ToInt32(dr["Id_Comunicado"]);
                                c.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                                c.Usuario.NombreUsuario = dr["NombreUsuario"] != DBNull.Value ? dr["NombreUsuario"].ToString() : string.Empty;
                                c.Usuario.Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty;
                                c.Usuario.ApellidoPaterno = dr["ApPaterno"] != DBNull.Value ? dr["ApPaterno"].ToString() : string.Empty;
                                c.Rol.Id = Convert.ToInt32(dr["Id_Rol"]);
                                c.Rol.NombreRol = dr["NombreRol"] != DBNull.Value ? dr["NombreRol"].ToString() : string.Empty;
                                c.Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty;
                                c.Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty;
                                c.FechaCreacion = dr.IsDBNull(dr.GetOrdinal("FechaCreacion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                                c.FechaFinal = dr.IsDBNull(dr.GetOrdinal("FechaFinal")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaFinal"));
                                
                                // Manejar el campo Visto de forma segura
                                int vistoOrdinal = dr.GetOrdinal("Visto");
                                c.Visto = dr.IsDBNull(vistoOrdinal) ? false : Convert.ToBoolean(dr[vistoOrdinal]);

                                lista.Add(c);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar comunicados por rol de profesor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar comunicados de profesor: {ex.Message}", ex);
            }

            return lista;
        }

        /// <summary>
        /// Lista comunicados dirigidos específicamente al rol APODERADO
        /// Requiere el SP: listar_comunicados_por_rol_usuario_Apoderado
        /// </summary>
        public List<clsEntidades.clsComunicado> ListarComunicadosPorRolUsuarioApoderado(int idUsuario)
        {
            List<clsEntidades.clsComunicado> lista = new List<clsEntidades.clsComunicado>();

            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("listar_comunicados_por_rol_usuario_Apoderado", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                clsEntidades.clsComunicado c = new clsEntidades.clsComunicado();
                                c.Usuario = new clsEntidades.clsUsuario();
                                c.Rol = new clsEntidades.clsRol();

                                c.Id = Convert.ToInt32(dr["Id_Comunicado"]);
                                c.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                                c.Usuario.NombreUsuario = dr["NombreUsuario"] != DBNull.Value ? dr["NombreUsuario"].ToString() : string.Empty;
                                c.Usuario.Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty;
                                c.Usuario.ApellidoPaterno = dr["ApPaterno"] != DBNull.Value ? dr["ApPaterno"].ToString() : string.Empty;
                                c.Rol.Id = Convert.ToInt32(dr["Id_Rol"]);
                                c.Rol.NombreRol = dr["NombreRol"] != DBNull.Value ? dr["NombreRol"].ToString() : string.Empty;
                                c.Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty;
                                c.Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty;
                                c.FechaCreacion = dr.IsDBNull(dr.GetOrdinal("FechaCreacion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                                c.FechaFinal = dr.IsDBNull(dr.GetOrdinal("FechaFinal")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaFinal"));
                                
                                // Manejar el campo Visto de forma segura
                                int vistoOrdinal = dr.GetOrdinal("Visto");
                                c.Visto = dr.IsDBNull(vistoOrdinal) ? false : Convert.ToBoolean(dr[vistoOrdinal]);

                                lista.Add(c);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar comunicados por rol de apoderado: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al listar comunicados de apoderado: {ex.Message}", ex);
            }

            return lista;
        }
    }
}
