using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacTipoDocumento
    {
        public List<clsEntidades.clsTipoDocumento> listarTipoDocumento()
        {
            List<clsEntidades.clsTipoDocumento> lista = new List<clsEntidades.clsTipoDocumento>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_tipo_documentos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsTipoDocumento td = new clsEntidades.clsTipoDocumento();

                            td.Id = Convert.ToInt32(dr["Id_TipoDocumento"]);
                            td.Nombre = dr["Nombre"].ToString();
                            td.Abreviatura = dr["Abreviatura"].ToString();
                            td.CantidadCaracteres = Convert.ToInt32(dr["CantidadCaracteres"]);

                            lista.Add(td);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarTipoDocumento(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_tipo_documentos", cn))
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

        public void InsertarTipoDocumento(clsEntidades.clsTipoDocumento xTd)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_tipo_documentos", cn))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", xTd.Nombre);
                        cmd.Parameters.AddWithValue("@Abreviatura", xTd.Abreviatura);
                        cmd.Parameters.AddWithValue("@CantidadCaracteres", xTd.CantidadCaracteres);

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void ModificarTipoDocumento(clsEntidades.clsTipoDocumento xTd)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_tipo_documentos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", xTd.Id);
                        cmd.Parameters.AddWithValue("@Nombre", xTd.Nombre);
                        cmd.Parameters.AddWithValue("@Abreviatura", xTd.Abreviatura);
                        cmd.Parameters.AddWithValue("@CantidadCaracteres", xTd.CantidadCaracteres);

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
