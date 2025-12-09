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
    public class clsDacServicioAdicional
    {
        public List<clsEntidades.clsServicioAdicional> ListarServicioAdicional()
        {
            List<clsEntidades.clsServicioAdicional> lista = new List<clsEntidades.clsServicioAdicional>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_servicioAdicional", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsServicioAdicional s = new clsEntidades.clsServicioAdicional();

                            s.Id_ServicioAdicional = Convert.ToInt32(dr["Id_ServicioAdicional"]);
                            s.Nombre = dr["Nombre"].ToString();
                            s.Descripcion = dr["Descripcion"].ToString();
                            s.Tipo = Convert.ToChar(dr["Tipo"]);
                            s.Costo = Convert.ToDecimal(dr["Costo"]);

                            lista.Add(s);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarServicioAdicional(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_servicioAdicional", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_ServicioAdicional", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarServicioAdicional(clsEntidades.clsServicioAdicional xSer)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_servicioAdicional", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", xSer.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", xSer.Descripcion);
                    cmd.Parameters.AddWithValue("@Tipo", xSer.Tipo);
                    cmd.Parameters.AddWithValue("@Costo", xSer.Costo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarServicioAdicional(clsEntidades.clsServicioAdicional xSer)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_servicioAdicional", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_ServicioAdicional", xSer.Id_ServicioAdicional);
                    cmd.Parameters.AddWithValue("@Nombre", xSer.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", xSer.Descripcion);
                    cmd.Parameters.AddWithValue("@Tipo", xSer.Tipo);
                    cmd.Parameters.AddWithValue("@Costo", xSer.Costo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<clsServicioAdicional> BuscarServicioAdicional(string texto)
        {
            List<clsServicioAdicional> lista = new List<clsServicioAdicional>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("buscar_servicio_adicional_nombre", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Texto", texto ?? "");

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new clsServicioAdicional
                        {
                            Id_ServicioAdicional = Convert.ToInt32(dr["Id_ServicioAdicional"]),
                            Nombre = dr["Nombre"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
