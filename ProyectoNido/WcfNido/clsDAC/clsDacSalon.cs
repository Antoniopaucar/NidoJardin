using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacSalon
    {
        public List<clsEntidades.clsSalon> listarSalon()
        {
            List<clsEntidades.clsSalon> lista = new List<clsEntidades.clsSalon>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_salon", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsSalon s = new clsEntidades.clsSalon();

                            s.Id = Convert.ToInt32(dr["Id_Salon"]);
                            s.Nombre = dr["Nombre"].ToString();
                            s.Aforo = Convert.ToInt32(dr["Aforo"].ToString());
                            s.Dimensiones = dr["Dimensiones"].ToString();
                            s.Activo = Convert.ToBoolean(dr["Activo"]);

                            lista.Add(s);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarSalon(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_salon", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarSalon(clsEntidades.clsSalon xSal)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_salon", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", xSal.Nombre);
                    cmd.Parameters.AddWithValue("@Aforo", xSal.Aforo);
                    cmd.Parameters.AddWithValue("@Dimensiones", xSal.Dimensiones);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarSalon(clsEntidades.clsSalon xSal)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_salon", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", xSal.Id);
                    cmd.Parameters.AddWithValue("@Nombre", xSal.Nombre);
                    cmd.Parameters.AddWithValue("@Aforo", xSal.Aforo);
                    cmd.Parameters.AddWithValue("@Dimensiones", xSal.Dimensiones);
                    cmd.Parameters.AddWithValue("@Activo", xSal.Activo);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
