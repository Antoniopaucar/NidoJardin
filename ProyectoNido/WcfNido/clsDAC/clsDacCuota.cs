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
    public class clsDacCuota
    {
        private static clsDacCuota instance;

        public static clsDacCuota GetInstance()
        {
            if (instance == null)
                instance = new clsDacCuota();
            return instance;
        }

        // INSERTAR
        public int Insertar(clsCuota obj)
        {
            SqlConnection cn = null;
            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Cuota_Insertar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Tarifario", obj.Id_Tarifario);
                cmd.Parameters.AddWithValue("@NroCuota", obj.NroCuota);
                cmd.Parameters.AddWithValue("@FechaPagoSugerido", obj.FechaPagoSugerido);
                cmd.Parameters.AddWithValue("@Monto", (object)obj.Monto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Descuento", (object)obj.Descuento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Adicional", (object)obj.Adicional ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreCuota", obj.NombreCuota);

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally { if (cn != null) cn.Close(); }
        }

        // ACTUALIZAR
        public bool Actualizar(clsCuota obj)
        {
            SqlConnection cn = null;
            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Cuota_Actualizar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Cuota", obj.Id_Cuota);
                cmd.Parameters.AddWithValue("@Id_Tarifario", obj.Id_Tarifario);
                cmd.Parameters.AddWithValue("@NroCuota", obj.NroCuota);
                cmd.Parameters.AddWithValue("@FechaPagoSugerido", obj.FechaPagoSugerido);
                cmd.Parameters.AddWithValue("@Monto", (object)obj.Monto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Descuento", (object)obj.Descuento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Adicional", (object)obj.Adicional ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreCuota", obj.NombreCuota);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { if (cn != null) cn.Close(); }
        }

        // ELIMINAR
        public bool Eliminar(int idCuota)
        {
            SqlConnection cn = null;
            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Cuota_Eliminar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Cuota", idCuota);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            finally { if (cn != null) cn.Close(); }
        }

        // LISTAR POR TARIFARIO (o todos si idTarifario = 0 / null)
        public List<clsCuota> Listar(int? idTarifario = null)
        {
            List<clsCuota> lista = new List<clsCuota>();
            SqlConnection cn = null;

            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Cuota_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (idTarifario.HasValue)
                    cmd.Parameters.AddWithValue("@Id_Tarifario", idTarifario.Value);
                else
                    cmd.Parameters.AddWithValue("@Id_Tarifario", DBNull.Value);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    clsCuota c = new clsCuota
                    {
                        Id_Cuota = Convert.ToInt32(dr["Id_Cuota"]),
                        Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                        NroCuota = Convert.ToInt32(dr["NroCuota"]),
                        FechaPagoSugerido = Convert.ToDateTime(dr["FechaPagoSugerido"]),
                        Monto = dr["Monto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Monto"]),
                        Descuento = dr["Descuento"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Descuento"]),
                        Adicional = dr["Adicional"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Adicional"]),
                        NombreCuota = dr["NombreCuota"].ToString()
                    };

                    lista.Add(c);
                }
            }
            finally { if (cn != null) cn.Close(); }

            return lista;
        }
    }
}
