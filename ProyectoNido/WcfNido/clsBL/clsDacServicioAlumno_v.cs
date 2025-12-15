using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsDacServicioAlumno_v
    {
        // ==========================
        // LISTAR
        // ==========================
        public List<clsServicioAlumno_v> ListarServicioAlumno()
        {
            var lista = new List<clsServicioAlumno_v>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_servicioAlumno", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var obj = new clsServicioAlumno_v
                            {
                                Id_ServicioAlumno = Convert.ToInt32(dr["Id_ServicioAlumno"]),
                                Id_GrupoServicio = Convert.ToInt32(dr["Id_GrupoServicio"]),
                                Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]),

                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFinal = dr["FechaFinal"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaFinal"]),
                                FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaPago"]),

                                HoraInicio = dr["HoraInicio"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)dr["HoraInicio"],
                                HoraFinal = dr["HoraFinal"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)dr["HoraFinal"],

                                Monto = dr["Monto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Monto"])
                            };

                            // Campos extra (si tu SP listar_servicioAlumno los devuelve)
                            if (TieneColumna(dr, "NombreAlumno"))
                                obj.NombreAlumno = dr["NombreAlumno"] == DBNull.Value ? "" : dr["NombreAlumno"].ToString();

                            if (TieneColumna(dr, "NombreSalon"))
                                obj.NombreSalon = dr["NombreSalon"] == DBNull.Value ? "" : dr["NombreSalon"].ToString();

                            if (TieneColumna(dr, "NombreProfesor"))
                                obj.NombreProfesor = dr["NombreProfesor"] == DBNull.Value ? "" : dr["NombreProfesor"].ToString();

                            if (TieneColumna(dr, "NombreServicio"))
                                obj.NombreServicio = dr["NombreServicio"] == DBNull.Value ? "" : dr["NombreServicio"].ToString();

                            if (TieneColumna(dr, "Periodo"))
                                obj.Periodo = dr["Periodo"] == DBNull.Value ? (short)0 : Convert.ToInt16(dr["Periodo"]);

                            if (TieneColumna(dr, "Estado"))
                                obj.Estado = dr["Estado"] == DBNull.Value ? "" : dr["Estado"].ToString();

                            lista.Add(obj);
                        }
                    }
                }
            }

            return lista;
        }

        // ==========================
        // INSERTAR
        // ==========================
        public string InsertarServicioAlumno(clsServicioAlumno_v obj)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_servicioAlumno", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id_GrupoServicio", SqlDbType.Int).Value = obj.Id_GrupoServicio;
                    cmd.Parameters.Add("@Id_Alumno", SqlDbType.Int).Value = obj.Id_Alumno;

                    cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = obj.FechaInicio.Date;

                    cmd.Parameters.Add("@FechaFinal", SqlDbType.Date).Value =
                        obj.FechaFinal.HasValue ? (object)obj.FechaFinal.Value.Date : DBNull.Value;

                    cmd.Parameters.Add("@FechaPago", SqlDbType.Date).Value =
                        obj.FechaPago.HasValue ? (object)obj.FechaPago.Value.Date : DBNull.Value;

                    cmd.Parameters.Add("@HoraInicio", SqlDbType.Time).Value =
                        obj.HoraInicio.HasValue ? (object)obj.HoraInicio.Value : DBNull.Value;

                    cmd.Parameters.Add("@HoraFinal", SqlDbType.Time).Value =
                        obj.HoraFinal.HasValue ? (object)obj.HoraFinal.Value : DBNull.Value;

                    cmd.Parameters.Add("@Monto", SqlDbType.Decimal).Value = obj.Monto;

                    // El SP devuelve: SELECT 'OK' AS Mensaje / o mensaje de error/aforo
                    object result = cmd.ExecuteScalar();
                    return result == null ? "OK" : result.ToString();
                }
            }
        }

        // ==========================
        // MODIFICAR
        // ==========================
        public string ModificarServicioAlumno(clsServicioAlumno_v obj)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_servicioAlumno", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id_ServicioAlumno", SqlDbType.Int).Value = obj.Id_ServicioAlumno;
                    cmd.Parameters.Add("@Id_GrupoServicio", SqlDbType.Int).Value = obj.Id_GrupoServicio;
                    cmd.Parameters.Add("@Id_Alumno", SqlDbType.Int).Value = obj.Id_Alumno;

                    cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = obj.FechaInicio.Date;

                    cmd.Parameters.Add("@FechaFinal", SqlDbType.Date).Value =
                        obj.FechaFinal.HasValue ? (object)obj.FechaFinal.Value.Date : DBNull.Value;

                    cmd.Parameters.Add("@FechaPago", SqlDbType.Date).Value =
                        obj.FechaPago.HasValue ? (object)obj.FechaPago.Value.Date : DBNull.Value;

                    cmd.Parameters.Add("@HoraInicio", SqlDbType.Time).Value =
                        obj.HoraInicio.HasValue ? (object)obj.HoraInicio.Value : DBNull.Value;

                    cmd.Parameters.Add("@HoraFinal", SqlDbType.Time).Value =
                        obj.HoraFinal.HasValue ? (object)obj.HoraFinal.Value : DBNull.Value;

                    cmd.Parameters.Add("@Monto", SqlDbType.Decimal).Value = obj.Monto;

                    object result = cmd.ExecuteScalar();
                    return result == null ? "OK" : result.ToString();
                }
            }
        }

        // ==========================
        // ELIMINAR
        // ==========================
        public string EliminarServicioAlumno(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_servicioAlumno", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id_ServicioAlumno", SqlDbType.Int).Value = id;

                    object result = cmd.ExecuteScalar();
                    return result == null ? "OK" : result.ToString();
                }
            }
        }

        // ==========================
        // UTIL: verificar columnas
        // ==========================
        private bool TieneColumna(SqlDataReader dr, string nombreColumna)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
