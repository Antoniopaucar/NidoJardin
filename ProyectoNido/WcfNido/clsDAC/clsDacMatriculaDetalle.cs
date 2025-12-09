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
    public class clsDacMatriculaDetalle
    {
        // LISTAR POR MATRÍCULA
        public List<clsEntidades.clsMatriculaDetalle> ListarPorMatricula(int idMatricula)
        {
            var lista = new List<clsEntidades.clsMatriculaDetalle>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_ListarPorMatricula", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Matricula", idMatricula);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var d = new clsEntidades.clsMatriculaDetalle
                        {
                            Id_MatriculaDetalle = Convert.ToInt32(dr["Id_MatriculaDetalle"]),
                            Id_Matricula = Convert.ToInt32(dr["Id_Matricula"]),
                            Id_Cuota = dr["Id_Cuota"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Id_Cuota"]),
                            NroCuota = dr["NroCuota"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["NroCuota"]),
                            NombreCuota = dr["NombreCuota"].ToString(),
                            FechaVencimiento = dr["FechaVencimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaVencimiento"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Monto = Convert.ToDecimal(dr["Monto"]),
                            Descuento = Convert.ToDecimal(dr["Descuento"]),
                            Adicional = Convert.ToDecimal(dr["Adicional"]),
                            TotalLinea = Convert.ToDecimal(dr["TotalLinea"]),
                            FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaPago"]),
                            EstadoPago = dr["EstadoPago"].ToString(),
                            Observacion = dr["Observacion"].ToString()
                        };
                        lista.Add(d);
                    }
                }
            }

            return lista;
        }

        // OBTENER UNO
        public clsEntidades.clsMatriculaDetalle Obtener(int idMatriculaDetalle)
        {
            clsEntidades.clsMatriculaDetalle d = null;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_Obtener", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_MatriculaDetalle", idMatriculaDetalle);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        d = new clsEntidades.clsMatriculaDetalle
                        {
                            Id_MatriculaDetalle = Convert.ToInt32(dr["Id_MatriculaDetalle"]),
                            Id_Matricula = Convert.ToInt32(dr["Id_Matricula"]),
                            Id_Cuota = dr["Id_Cuota"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Id_Cuota"]),
                            NroCuota = dr["NroCuota"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["NroCuota"]),
                            NombreCuota = dr["NombreCuota"].ToString(),
                            FechaVencimiento = dr["FechaVencimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaVencimiento"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Monto = Convert.ToDecimal(dr["Monto"]),
                            Descuento = Convert.ToDecimal(dr["Descuento"]),
                            Adicional = Convert.ToDecimal(dr["Adicional"]),
                            TotalLinea = Convert.ToDecimal(dr["TotalLinea"]),
                            FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaPago"]),
                            EstadoPago = dr["EstadoPago"].ToString(),
                            Observacion = dr["Observacion"].ToString()
                        };
                    }
                }
            }

            return d;
        }

        // INSERTAR
        public int Insertar(clsEntidades.clsMatriculaDetalle det)
        {
            int idGenerado = 0;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_Insertar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Matricula", det.Id_Matricula);
                cmd.Parameters.AddWithValue("@Id_Cuota", (object)det.Id_Cuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NroCuota", (object)det.NroCuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreCuota", (object)det.NombreCuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaVencimiento", (object)det.FechaVencimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                cmd.Parameters.AddWithValue("@Monto", det.Monto);
                cmd.Parameters.AddWithValue("@Descuento", det.Descuento);
                cmd.Parameters.AddWithValue("@Adicional", det.Adicional);
                cmd.Parameters.AddWithValue("@FechaPago", (object)det.FechaPago ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EstadoPago", det.EstadoPago);
                cmd.Parameters.AddWithValue("@Observacion", (object)det.Observacion ?? DBNull.Value);

                cn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    idGenerado = Convert.ToInt32(result);
            }

            return idGenerado;
        }

        // ACTUALIZAR
        public void Actualizar(clsMatriculaDetalle det)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_Actualizar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_MatriculaDetalle", det.Id_MatriculaDetalle);
                // cmd.Parameters.AddWithValue("@Id_Cuota", (object)det.Id_Cuota ?? DBNull.Value); // ❌ fuera
                cmd.Parameters.AddWithValue("@NroCuota", (object)det.NroCuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreCuota", (object)det.NombreCuota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaVencimiento", (object)det.FechaVencimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                cmd.Parameters.AddWithValue("@Monto", det.Monto);
                cmd.Parameters.AddWithValue("@Descuento", det.Descuento);
                cmd.Parameters.AddWithValue("@Adicional", det.Adicional);
                cmd.Parameters.AddWithValue("@FechaPago", (object)det.FechaPago ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EstadoPago", det.EstadoPago);
                cmd.Parameters.AddWithValue("@Observacion", (object)det.Observacion ?? DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ELIMINAR
        public void Eliminar(int idMatriculaDetalle)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_Eliminar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_MatriculaDetalle", idMatriculaDetalle);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Anular(int idMatriculaDetalle)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_Anular", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_MatriculaDetalle", idMatriculaDetalle);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Reactivar(int idMatriculaDetalle)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("MatriculaDetalle_Reactivar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_MatriculaDetalle", idMatriculaDetalle);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
