using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace clsDAC
{
    public class clsDacUsuario
    {
        public List<clsEntidades.clsUsuario> listarUsuarios()
        {
            List<clsEntidades.clsUsuario> lista = new List<clsEntidades.clsUsuario>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_usuarios", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsUsuario u = new clsEntidades.clsUsuario();
                            u.Distrito = new clsEntidades.clsDistrito();
                            u.TipoDocumento = new clsEntidades.clsTipoDocumento();

                            u.Id = Convert.ToInt32(dr["Id"]);
                            u.TipoDocumento.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                            u.TipoDocumento.Nombre = dr["NombreTipoDocumento"].ToString();
                            u.NombreUsuario =dr["NombreUsuario"].ToString();
                            u.Clave = dr["Clave"].ToString();
                            u.Nombres = dr["Nombres"].ToString();
                            u.ApellidoPaterno = dr["ApPaterno"].ToString();
                            u.ApellidoMaterno = dr["ApMaterno"].ToString();
                            u.Documento = dr["Documento"].ToString();
                            u.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
                            u.Sexo = dr["Sexo"].ToString();
                            u.Distrito.Id = Convert.IsDBNull(dr["Id_Distrito"])
                            ? 0
                            : Convert.ToInt32(dr["Id_Distrito"]);
                            u.Distrito.Nombre = dr["NombreDistrito"].ToString();
                            u.Direccion = dr["Direccion"].ToString();
                            u.Telefono = dr["Telefono"].ToString();
                            u.Email = dr["Email"].ToString();
                            u.Activo = Convert.ToBoolean(dr["Activo"]);
                            u.Intentos = Convert.ToInt32(dr["Intentos"]);
                            u.Bloqueado = Convert.ToBoolean(dr["Bloqueado"]);
                            u.FechaBloqueo = dr.IsDBNull(dr.GetOrdinal("FechaBloqueo")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaBloqueo"));
                            u.UltimoIntento = dr.IsDBNull(dr.GetOrdinal("UltimoIntento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoIntento"));
                            u.UltimoLoginExitoso = dr.IsDBNull(dr.GetOrdinal("UltimoLoginExitoso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoLoginExitoso"));
                            u.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));

                            lista.Add(u);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarUsuario(int id)
        {
            try 
            { 
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_usuarios", cn))
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

        public void InsertarUsuario(clsEntidades.clsUsuario xusr)
        {
            try 
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_usuarios", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_TipoDocumento", xusr.TipoDocumento.Id);
                        cmd.Parameters.AddWithValue("@NombreUsuario",xusr.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", clsUtilidades.clsUtiles.ObtenerSha256(xusr.Clave));
                        cmd.Parameters.AddWithValue("@Nombres", xusr.Nombres);
                        cmd.Parameters.AddWithValue("@ApPaterno", xusr.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApMaterno", xusr.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Documento", xusr.Documento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", xusr.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Sexo", xusr.Sexo);

                        cmd.Parameters.AddWithValue(
                            "@Id_Distrito",
                            xusr.Distrito == null ? (object)DBNull.Value : xusr.Distrito.Id
                        );

                        cmd.Parameters.AddWithValue("@Direccion", xusr.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", xusr.Telefono);
                        cmd.Parameters.AddWithValue("@Email", xusr.Email);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void ModificarUsuario(clsEntidades.clsUsuario xusr)
        {
            try 
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_usuarios", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", xusr.Id);
                        cmd.Parameters.AddWithValue("@Id_TipoDocumento", xusr.TipoDocumento.Id);
                        cmd.Parameters.AddWithValue("@NombreUsuario", xusr.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", clsUtilidades.clsUtiles.ObtenerSha256(xusr.Clave));
                        cmd.Parameters.AddWithValue("@Nombres", xusr.Nombres);
                        cmd.Parameters.AddWithValue("@ApPaterno", xusr.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApMaterno", xusr.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Documento", xusr.Documento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", xusr.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Sexo", xusr.Sexo);

                        cmd.Parameters.AddWithValue(
                            "@Id_Distrito",
                            xusr.Distrito == null ? (object)DBNull.Value : xusr.Distrito.Id
                        );

                        cmd.Parameters.AddWithValue("@Direccion", xusr.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", xusr.Telefono);
                        cmd.Parameters.AddWithValue("@Email", xusr.Email);
                        cmd.Parameters.AddWithValue("@Activo", xusr.Activo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        // Nuevo método: Modificar usuario sin actualizar contraseña (para apoderado)
        // Usa el mismo SP pero pasa NULL para la clave si está vacía
        public void ModificarUsuarioSinClave(clsEntidades.clsUsuario xusr)
        {
            try 
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_usuarios", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", xusr.Id);
                        cmd.Parameters.AddWithValue("@Id_TipoDocumento", xusr.TipoDocumento.Id);
                        cmd.Parameters.AddWithValue("@NombreUsuario", xusr.NombreUsuario);
                        
                        // Pasar NULL para la clave para que el SP mantenga la clave actual
                        // Nota: Esto requiere que el SP maneje NULL correctamente
                        cmd.Parameters.AddWithValue("@Clave", DBNull.Value);
                        
                        cmd.Parameters.AddWithValue("@Nombres", xusr.Nombres);
                        cmd.Parameters.AddWithValue("@ApPaterno", xusr.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApMaterno", xusr.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Documento", xusr.Documento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", xusr.FechaNacimiento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Sexo", xusr.Sexo ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue(
                            "@Id_Distrito",
                            xusr.Distrito == null ? (object)DBNull.Value : xusr.Distrito.Id
                        );

                        cmd.Parameters.AddWithValue("@Direccion", xusr.Direccion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefono", xusr.Telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", xusr.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Activo", xusr.Activo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al modificar usuario sin clave: {ex.Message}", ex);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        //public void ActualizarDatosDocente(int idUsuario, string nombres, string apPaterno, string apMaterno,
        //    string dni, DateTime? fechaNacimiento, string sexo, string direccion, string email,
        //    DateTime? fechaIngreso, string tituloProfesional, string cv, string evaluacionPsicologica,
        //    string fotos, string verificacionDomiciliaria)
        //{
        //    using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
        //    {
        //        cn.Open();
        //        using (SqlCommand cmd = new SqlCommand("actualizar_datos_docente", cn))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);
        //            cmd.Parameters.AddWithValue("@Nombres", nombres ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ApPaterno", apPaterno ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ApMaterno", apMaterno ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Dni", dni ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento.HasValue ? (object)fechaNacimiento.Value : DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Sexo", sexo ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Direccion", direccion ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso.HasValue ? (object)fechaIngreso.Value : DBNull.Value);
        //            cmd.Parameters.AddWithValue("@TituloProfesional", tituloProfesional ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Cv", cv ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@EvaluacionPsicologica", evaluacionPsicologica ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@Fotos", fotos ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@VerificacionDomiciliaria", verificacionDomiciliaria ?? (object)DBNull.Value);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public clsEntidades.clsUsuario ObtenerDatosDocente(int idUsuario)
        //{
        //    clsEntidades.clsUsuario usuario = null;

        //    using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
        //    {
        //        cn.Open();
        //        using (SqlCommand cmd = new SqlCommand("obtener_datos_profesor_por_usuario", cn))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                if (dr.Read())
        //                {
        //                    usuario = new clsEntidades.clsUsuario();
                            
        //                    usuario.Id = Convert.ToInt32(dr["Id"]);
        //                    usuario.NombreUsuario = dr["NombreUsuario"].ToString();
        //                    usuario.Nombres = dr["Nombres"].ToString();
        //                    usuario.ApellidoPaterno = dr["ApPaterno"].ToString();
        //                    usuario.ApellidoMaterno = dr["ApMaterno"].ToString();
        //                    usuario.Dni = dr.IsDBNull(dr.GetOrdinal("Dni")) ? null : dr["Dni"].ToString();
        //                    usuario.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
        //                    usuario.Sexo = dr.IsDBNull(dr.GetOrdinal("Sexo")) ? null : dr["Sexo"].ToString();
        //                    usuario.Direccion = dr.IsDBNull(dr.GetOrdinal("Direccion")) ? null : dr["Direccion"].ToString();
        //                    usuario.Email = dr.IsDBNull(dr.GetOrdinal("Email")) ? null : dr["Email"].ToString();
                            
        //                    // Campos de Profesor
        //                    usuario.FechaIngreso = dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaIngreso"));
        //                    usuario.TituloProfesional = dr.IsDBNull(dr.GetOrdinal("TituloProfesional")) ? null : dr["TituloProfesional"].ToString();
        //                    usuario.Cv = dr.IsDBNull(dr.GetOrdinal("Cv")) ? null : dr["Cv"].ToString();
        //                    usuario.EvaluacionPsicologica = dr.IsDBNull(dr.GetOrdinal("EvaluacionPsicologica")) ? null : dr["EvaluacionPsicologica"].ToString();
        //                    usuario.Fotos = dr.IsDBNull(dr.GetOrdinal("Fotos")) ? null : dr["Fotos"].ToString();
        //                    usuario.VerificacionDomiciliaria = dr.IsDBNull(dr.GetOrdinal("VerificacionDomiciliaria")) ? null : dr["VerificacionDomiciliaria"].ToString();
        //                }
        //            }
        //        }
        //    }

        //    return usuario;
        //}
    }
}
