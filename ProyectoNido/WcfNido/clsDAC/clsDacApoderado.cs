using clsEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacApoderado
    {
        public List<clsEntidades.clsApoderado> listarApoderados()
        {
            List<clsEntidades.clsApoderado> lista = new List<clsEntidades.clsApoderado>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_apoderados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsApoderado a = new clsEntidades.clsApoderado();
                            a.Distrito = new clsEntidades.clsDistrito();
                            a.TipoDocumento = new clsEntidades.clsTipoDocumento();

                            a.Id = Convert.ToInt32(dr["Id_Apoderado"]);
                            a.NombreUsuario = dr["NombreUsuario"].ToString();
                            a.Nombres = dr["Nombres"].ToString();
                            a.ApellidoPaterno = dr["ApPaterno"].ToString();
                            a.ApellidoMaterno = dr["ApMaterno"].ToString();
                            a.Documento = dr["Documento"].ToString();
                            a.EstadoArchivo = dr["EstadoArchivo"].ToString();
                            a.NombreCompleto = dr["NombreCompleto"].ToString();

                            a.TipoDocumento.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                            a.TipoDocumento.Nombre = dr["NombreTipoDocumento"].ToString();
                            a.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
                            a.Sexo = dr["Sexo"].ToString();
                            a.Distrito.Id = Convert.IsDBNull(dr["Id_Distrito"])
                            ? 0
                            : Convert.ToInt32(dr["Id_Distrito"]);
                            a.Distrito.Nombre = dr["NombreDistrito"].ToString();
                            a.Direccion = dr["Direccion"].ToString();
                            a.Telefono = dr["Telefono"].ToString();
                            a.Email = dr["Email"].ToString();
                            a.Activo = Convert.ToBoolean(dr["Activo"]);
                            a.Intentos = Convert.ToInt32(dr["Intentos"]);
                            a.Bloqueado = Convert.ToBoolean(dr["Bloqueado"]);
                            a.FechaBloqueo = dr.IsDBNull(dr.GetOrdinal("FechaBloqueo")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaBloqueo"));
                            a.UltimoIntento = dr.IsDBNull(dr.GetOrdinal("UltimoIntento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoIntento"));
                            a.UltimoLoginExitoso = dr.IsDBNull(dr.GetOrdinal("UltimoLoginExitoso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoLoginExitoso"));
                            a.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));

                            lista.Add(a);
                        }
                    }
                }
            }

            return lista;
        }

        //public clsApoderado RetornarCopiaDniApoderado(int id)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand("Retornar_Copia_Dni_Apoderado", cn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@Id", id);
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    dr.Read();

        //                    clsEntidades.clsApoderado a = new clsEntidades.clsApoderado();
        //                    a.ArchivoBase = new clsArchivoBase();

        //                    a.ArchivoBase.TamanioBytes = Convert.ToInt32(dr["TamanioBytes"]);
        //                    a.ArchivoBase.NombreArchivo= dr["NombreArchivo"].ToString();
        //                    a.ArchivoBase.Archivo = dr["CopiaDni"] != DBNull.Value
        //                            ? (byte[])dr["CopiaDni"]
        //                            : null;

        //                    return a;
                            
        //                }
        //            }
        //        }

               
        //    }
        //    catch (ArgumentException)
        //    {
        //        throw;
        //    }
        //}

        public void EliminarApoderado(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_apoderados", cn))
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

        public void InsertarApoderado(clsApoderado xAp, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("insertar_apoderados", cn, trx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Apoderado", xAp.Id);
                    cmd.Parameters.AddWithValue("@TamanioBytes", xAp.ArchivoBase.TamanioBytes);
                    cmd.Parameters.AddWithValue("@NombreArchivo", xAp.ArchivoBase.NombreArchivo);
                    cmd.Parameters.Add("@CopiaDni", SqlDbType.VarBinary).Value =
                        (object)xAp.ArchivoBase.Archivo ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (ArgumentException) 
            {
                throw;
            }
        }


        //public void InsertarApoderado(clsEntidades.clsApoderado xAp)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand("insertar_apoderados", cn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@Id_Apoderado", xAp.Id);
        //                cmd.Parameters.AddWithValue("@TamanioBytes", xAp.ArchivoBase.TamanioBytes);
        //                cmd.Parameters.AddWithValue("@NombreArchivo", xAp.ArchivoBase.NombreArchivo);
        //                cmd.Parameters.Add("@CopiaDni", SqlDbType.VarBinary).Value = (object)xAp.ArchivoBase.Archivo ?? DBNull.Value;

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (ArgumentException)
        //    {
        //        throw;
        //    }
        //}

        public void ModificarApoderado(clsEntidades.clsApoderado xAp)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_apoderados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Apoderado", xAp.Id);
                        cmd.Parameters.AddWithValue("@TamanioBytes", xAp.ArchivoBase.TamanioBytes);
                        cmd.Parameters.AddWithValue("@NombreArchivo", xAp.ArchivoBase.NombreArchivo);
                        cmd.Parameters.Add("@CopiaDni", SqlDbType.VarBinary).Value = (object)xAp.ArchivoBase.Archivo ?? DBNull.Value;

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
