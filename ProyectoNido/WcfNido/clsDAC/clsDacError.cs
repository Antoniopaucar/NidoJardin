using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacError
    {
        public clsEntidades.clsError BuscarErrorPorId(int idError)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("buscar_error_por_id", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdError", idError);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return new clsEntidades.clsError
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    IdError = Convert.ToInt32(dr["IdError"]),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Categoria = dr["Categoria"].ToString()
                                };
                            }
                            else
                            {
                                return null; // No encontrado
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
