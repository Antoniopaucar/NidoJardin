using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacDistrito
    {
        public List<clsEntidades.clsDistrito> listarDistritos()
        {
            List<clsEntidades.clsDistrito> lista = new List<clsEntidades.clsDistrito>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_distritos", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsDistrito d = new clsEntidades.clsDistrito();

                            d.Id = Convert.ToInt32(dr["Id_Distrito"]);
                            d.Ubigeo = dr["Ubigeo"].ToString();
                            d.Nombre = dr["Nombre"].ToString();

                            lista.Add(d);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarDistrito(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_distritos", cn))
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

        public void InsertarDistrito(clsEntidades.clsDistrito xDis)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_distritos", cn))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Ubigeo", xDis.Ubigeo);
                        cmd.Parameters.AddWithValue("@Nombre", xDis.Nombre);

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void ModificarDistrito(clsEntidades.clsDistrito xDis)
        {
            try 
            { 
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_distritos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", xDis.Id);
                        cmd.Parameters.AddWithValue("@Ubigeo", xDis.Ubigeo);
                        cmd.Parameters.AddWithValue("@Nombre", xDis.Nombre);

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
