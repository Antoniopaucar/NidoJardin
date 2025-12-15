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
    public class clsDacProfesor
    {
        public List<clsEntidades.clsProfesor> listarProfesores()
        {
            List<clsEntidades.clsProfesor> lista = new List<clsEntidades.clsProfesor>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_profesores", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsProfesor p = new clsEntidades.clsProfesor();
                            p.Distrito = new clsEntidades.clsDistrito();
                            p.TipoDocumento = new clsEntidades.clsTipoDocumento();

                            p.Id = Convert.ToInt32(dr["Id_Profesor"]);
                            p.NombreUsuario = dr["NombreUsuario"].ToString();
                            p.Nombres = dr["Nombres"].ToString();
                            p.ApellidoPaterno = dr["ApPaterno"].ToString();
                            p.ApellidoMaterno = dr["ApMaterno"].ToString();
                            p.Documento = dr["Documento"].ToString();

                            p.FechaIngreso = dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaIngreso"));
                            p.EstadoTituloProfesional = dr["EstadoTituloProfesional"].ToString();
                            p.EstadoCv= dr["EstadoCv"].ToString();
                            p.EstadoEvaluacionPsicologica =dr["EstadoEvaluacionPsicologica"].ToString();
                            p.EstadoFotos= dr["EstadoFotos"].ToString();
                            p.EstadoVerificacionDomiciliaria= dr["EstadoVerificacionDomiciliaria"].ToString();

                            p.TipoDocumento.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                            p.TipoDocumento.Nombre = dr["NombreTipoDocumento"].ToString();
                            p.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
                            p.Sexo = dr["Sexo"].ToString();
                            p.Distrito.Id = Convert.IsDBNull(dr["Id_Distrito"])
                            ? 0
                            : Convert.ToInt32(dr["Id_Distrito"]);
                            p.Distrito.Nombre = dr["NombreDistrito"].ToString();
                            p.Direccion = dr["Direccion"].ToString();
                            p.Telefono = dr["Telefono"].ToString();
                            p.Email = dr["Email"].ToString();
                            p.Activo = Convert.ToBoolean(dr["Activo"]);
                            p.Intentos = Convert.ToInt32(dr["Intentos"]);
                            p.Bloqueado = Convert.ToBoolean(dr["Bloqueado"]);
                            p.FechaBloqueo = dr.IsDBNull(dr.GetOrdinal("FechaBloqueo")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaBloqueo"));
                            p.UltimoIntento = dr.IsDBNull(dr.GetOrdinal("UltimoIntento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoIntento"));
                            p.UltimoLoginExitoso = dr.IsDBNull(dr.GetOrdinal("UltimoLoginExitoso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoLoginExitoso"));
                            p.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));

                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }

        public List<clsEntidades.clsProfesor> listarProfesoresActivos()
        {
            List<clsEntidades.clsProfesor> lista = new List<clsEntidades.clsProfesor>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarProfesoresActivos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsProfesor p = new clsEntidades.clsProfesor();
                            p.Distrito = new clsEntidades.clsDistrito();
                            p.TipoDocumento = new clsEntidades.clsTipoDocumento();

                            p.Id = Convert.ToInt32(dr["Id_Profesor"]);
                            p.NombreUsuario = dr["NombreUsuario"].ToString();
                            p.Nombres = dr["Nombres"].ToString();
                            p.ApellidoPaterno = dr["ApPaterno"].ToString();
                            p.ApellidoMaterno = dr["ApMaterno"].ToString();
                            p.Documento = dr["Documento"].ToString();

                            p.FechaIngreso = dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaIngreso"));
                            
                            // Mapeo los estados "calculados" por el SP
                            // p.EstadoTituloProfesional = dr["EstadoTituloProfesional"].ToString();
                            // p.EstadoCv= dr["EstadoCv"].ToString();
                            // p.EstadoEvaluacionPsicologica =dr["EstadoEvaluacionPsicologica"].ToString();
                            // p.EstadoFotos= dr["EstadoFotos"].ToString();
                            // p.EstadoVerificacionDomiciliaria= dr["EstadoVerificacionDomiciliaria"].ToString();

                            p.TipoDocumento.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                            p.TipoDocumento.Nombre = dr["NombreTipoDocumento"].ToString();
                            p.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
                            p.Sexo = dr["Sexo"].ToString();
                            p.Distrito.Id = Convert.IsDBNull(dr["Id_Distrito"])
                            ? 0
                            : Convert.ToInt32(dr["Id_Distrito"]);
                            p.Distrito.Nombre = dr["NombreDistrito"].ToString();
                            p.Direccion = dr["Direccion"].ToString();
                            p.Telefono = dr["Telefono"].ToString();
                            p.Email = dr["Email"].ToString();
                            p.Activo = Convert.ToBoolean(dr["Activo"]);
                            p.Intentos = Convert.ToInt32(dr["Intentos"]);
                            p.Bloqueado = Convert.ToBoolean(dr["Bloqueado"]);
                            p.FechaBloqueo = dr.IsDBNull(dr.GetOrdinal("FechaBloqueo")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaBloqueo"));
                            p.UltimoIntento = dr.IsDBNull(dr.GetOrdinal("UltimoIntento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoIntento"));
                            p.UltimoLoginExitoso = dr.IsDBNull(dr.GetOrdinal("UltimoLoginExitoso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoLoginExitoso"));
                            p.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));

                            // Calculo NombreCompleto
                            p.NombreCompleto = p.Nombres + " " + p.ApellidoPaterno + " " + p.ApellidoMaterno;

                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarProfesor(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_profesores", cn))
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

        public void InsertarProfesor(clsProfesor xPro, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("insertar_profesores", cn, trx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_Profesor", xPro.Id);
                    cmd.Parameters.AddWithValue("@FechaIngreso", xPro.FechaIngreso);

                    cmd.Parameters.AddWithValue("@TPTamanioBytes", xPro.TituloProfesional.TamanioBytes);
                    cmd.Parameters.AddWithValue("@TPNombre", xPro.TituloProfesional.NombreArchivo);
                    cmd.Parameters.Add("@TituloProfesional", SqlDbType.VarBinary).Value =
                        (object)xPro.TituloProfesional.Archivo ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@CvTamanioBytes", xPro.Cv.TamanioBytes);
                    cmd.Parameters.AddWithValue("@CvNombre", xPro.Cv.NombreArchivo);
                    cmd.Parameters.Add("@Cv", SqlDbType.VarBinary).Value =
                        (object)xPro.Cv.Archivo ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@EPSTamanioBytes", xPro.EvaluacionPsicologica.TamanioBytes);
                    cmd.Parameters.AddWithValue("@EPSNombre", xPro.EvaluacionPsicologica.NombreArchivo);
                    cmd.Parameters.Add("@EvaluacionPsicologica", SqlDbType.VarBinary).Value =
                        (object)xPro.EvaluacionPsicologica.Archivo ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@FoTamanioBytes", xPro.Fotos.TamanioBytes);
                    cmd.Parameters.AddWithValue("@FoNombre", xPro.Fotos.NombreArchivo);
                    cmd.Parameters.Add("@Fotos", SqlDbType.VarBinary).Value =
                        (object)xPro.Fotos.Archivo ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@VDTamanioBytes", xPro.VerificacionDomiciliaria.TamanioBytes);
                    cmd.Parameters.AddWithValue("@VDNombre", xPro.VerificacionDomiciliaria.NombreArchivo);
                    cmd.Parameters.Add("@VerificacionDomiciliaria", SqlDbType.VarBinary).Value =
                        (object)xPro.VerificacionDomiciliaria.Archivo ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }


        //public void InsertarProfesor(clsEntidades.clsProfesor xPro)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand("insertar_profesores", cn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@Id_Profesor", xPro.Id);
        //                cmd.Parameters.AddWithValue("@FechaIngreso", xPro.FechaIngreso);

        //                cmd.Parameters.AddWithValue("@TPTamanioBytes", xPro.TituloProfesional.TamanioBytes);
        //                cmd.Parameters.AddWithValue("@TPNombre", xPro.TituloProfesional.NombreArchivo);
        //                cmd.Parameters.Add("@TituloProfesional", SqlDbType.VarBinary).Value = (object)xPro.TituloProfesional.Archivo ?? DBNull.Value;

        //                cmd.Parameters.AddWithValue("@CvTamanioBytes", xPro.Cv.TamanioBytes);
        //                cmd.Parameters.AddWithValue("@CvNombre", xPro.Cv.NombreArchivo);
        //                cmd.Parameters.Add("@Cv", SqlDbType.VarBinary).Value = (object)xPro.Cv.Archivo ?? DBNull.Value;

        //                cmd.Parameters.AddWithValue("@EPSTamanioBytes", xPro.EvaluacionPsicologica.TamanioBytes);
        //                cmd.Parameters.AddWithValue("@EPSNombre", xPro.EvaluacionPsicologica.NombreArchivo);
        //                cmd.Parameters.Add("@EvaluacionPsicologica", SqlDbType.VarBinary).Value = (object)xPro.EvaluacionPsicologica.Archivo ?? DBNull.Value;

        //                cmd.Parameters.AddWithValue("@FoTamanioBytes", xPro.Fotos.TamanioBytes);
        //                cmd.Parameters.AddWithValue("@FoNombre", xPro.Fotos.NombreArchivo);
        //                cmd.Parameters.Add("@Fotos", SqlDbType.VarBinary).Value = (object)xPro.Fotos.Archivo ?? DBNull.Value;

        //                cmd.Parameters.AddWithValue("@VDTamanioBytes", xPro.VerificacionDomiciliaria.TamanioBytes);
        //                cmd.Parameters.AddWithValue("@VDNombre", xPro.EvaluacionPsicologica.NombreArchivo);
        //                cmd.Parameters.Add("@VerificacionDomiciliaria", SqlDbType.VarBinary).Value = (object)xPro.EvaluacionPsicologica.Archivo ?? DBNull.Value;

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (ArgumentException)
        //    {
        //        throw;
        //    }
        //}

        public void ModificarProfesor(clsEntidades.clsProfesor xPro)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_profesores", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Profesor", xPro.Id);
                        cmd.Parameters.AddWithValue("@FechaIngreso", xPro.FechaIngreso);

                        cmd.Parameters.AddWithValue("@TPTamanioBytes", xPro.TituloProfesional.TamanioBytes);
                        cmd.Parameters.AddWithValue("@TPNombre", xPro.TituloProfesional.NombreArchivo);
                        cmd.Parameters.Add("@TituloProfesional", SqlDbType.VarBinary).Value = (object)xPro.TituloProfesional.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@CvTamanioBytes", xPro.Cv.TamanioBytes);
                        cmd.Parameters.AddWithValue("@CvNombre", xPro.Cv.NombreArchivo);
                        cmd.Parameters.Add("@Cv", SqlDbType.VarBinary).Value = (object)xPro.Cv.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@EPSTamanioBytes", xPro.EvaluacionPsicologica.TamanioBytes);
                        cmd.Parameters.AddWithValue("@EPSNombre", xPro.EvaluacionPsicologica.NombreArchivo);
                        cmd.Parameters.Add("@EvaluacionPsicologica", SqlDbType.VarBinary).Value = (object)xPro.EvaluacionPsicologica.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@FoTamanioBytes", xPro.Fotos.TamanioBytes);
                        cmd.Parameters.AddWithValue("@FoNombre", xPro.Fotos.NombreArchivo);
                        cmd.Parameters.Add("@Fotos", SqlDbType.VarBinary).Value = (object)xPro.Fotos.Archivo ?? DBNull.Value;

                        cmd.Parameters.AddWithValue("@VDTamanioBytes", xPro.VerificacionDomiciliaria.TamanioBytes);
                        cmd.Parameters.AddWithValue("@VDNombre", xPro.VerificacionDomiciliaria.NombreArchivo);
                        cmd.Parameters.Add("@VerificacionDomiciliaria", SqlDbType.VarBinary).Value = (object)xPro.VerificacionDomiciliaria.Archivo ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public List<clsProfesorCombo> buscarProfesor(string texto)
        {
            List<clsProfesorCombo> lista = new List<clsProfesorCombo>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("buscar_profesor", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Texto", texto ?? string.Empty);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new clsProfesorCombo
                        {
                            Id_Profesor = Convert.ToInt32(dr["Id_Profesor"]),
                            NombreCompleto = dr["NombreCompleto"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

    }
}
