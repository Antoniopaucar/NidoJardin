using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using clsEntidades;

namespace clsDAC
{
    public class clsDacReporteIngresos
    {
        public List<clsReporteIngreso> ListarReporte(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<clsReporteIngreso> lista = new List<clsReporteIngreso>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("sp_ReporteIngresos_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdSalon", (object)idSalon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdDistrito", (object)idDistrito ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicio", (object)fechaInicio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaFin", (object)fechaFin ?? DBNull.Value);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        clsReporteIngreso r = new clsReporteIngreso
                        {
                            FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaPago"]),
                            Salon = dr["Salon"].ToString(),
                            Distrito = dr["Distrito"] == DBNull.Value ? "" : dr["Distrito"].ToString(),
                            Alumno = dr["Alumno"].ToString(),
                            Concepto = dr["Concepto"].ToString(),
                            Monto = Convert.ToDecimal(dr["Monto"]),
                            EstadoPago = dr["EstadoPago"].ToString(),
                            EstadoDescripcion = dr["EstadoDescripcion"].ToString(),
                            TipoOrigen = dr["TipoOrigen"].ToString()
                        };
                        lista.Add(r);
                    }
                }
            }
            return lista;
        }


        public List<clsReporteIngreso> ListarReporteCobranzas(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<clsReporteIngreso> lista = new List<clsReporteIngreso>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("sp_ReporteCobranzas_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdSalon", (object)idSalon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdDistrito", (object)idDistrito ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicio", (object)fechaInicio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaFin", (object)fechaFin ?? DBNull.Value);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        clsReporteIngreso r = new clsReporteIngreso
                        {
                            FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaPago"]),
                            Salon = dr["Salon"].ToString(),
                            Distrito = dr["Distrito"] == DBNull.Value ? "" : dr["Distrito"].ToString(),
                            Alumno = dr["Alumno"].ToString(),
                            Concepto = dr["Concepto"].ToString(),
                            Monto = Convert.ToDecimal(dr["Monto"]),
                            EstadoPago = dr["EstadoPago"].ToString(),
                            EstadoDescripcion = dr["EstadoDescripcion"].ToString(),
                            TipoOrigen = dr["TipoOrigen"].ToString()
                        };
                        lista.Add(r);
                    }
                }
            }
            return lista;
        }
    }
}
