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
    public class clsDacArchivoBase
    {
        public clsArchivoBase RetornarArchivo(
        string storedProcedure,
        string campoNombreArchivo,
        string campoTamanio,
        string campoArchivo,
        int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(storedProcedure, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                                return null;

                            clsArchivoBase archivo = new clsArchivoBase();

                            archivo.NombreArchivo = dr[campoNombreArchivo].ToString();
                            archivo.TamanioBytes = Convert.ToInt32(dr[campoTamanio]);
                            archivo.Archivo = dr[campoArchivo] != DBNull.Value
                                              ? (byte[])dr[campoArchivo]
                                              : null;

                            return archivo;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
