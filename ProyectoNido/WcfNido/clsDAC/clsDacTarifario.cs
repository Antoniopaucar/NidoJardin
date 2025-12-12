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
    public class clsDacTarifario
    {
        // SINGLETON
        private static clsDacTarifario instance;

        public static clsDacTarifario GetInstance()
        {
            if (instance == null)
                instance = new clsDacTarifario();

            return instance;
        }

        public List<clsEntidades.clsTarifario> listar_tarifario_combo()
        {
            var lista = new List<clsEntidades.clsTarifario>();
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("listar_tarifario_combo", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var t = new clsEntidades.clsTarifario
                        {
                            Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                            Nombre = dr["Nombre"].ToString()
                        };
                        lista.Add(t);
                    }
                }
            }
            return lista;
        }


        public List<clsEntidades.clsTarifario> listar_tarifario()
        {
            var lista = new List<clsEntidades.clsTarifario>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            using (SqlCommand cmd = new SqlCommand("listar_tarifario", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var t = new clsEntidades.clsTarifario
                        {
                            Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                            Tipo = dr["Tipo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Periodo = (byte)Convert.ToInt32(dr["Periodo"]),
                            Valor = Convert.ToDecimal(dr["Valor"])
                        };
                        lista.Add(t);
                    }
                }
            }

            return lista;
        }

        // ============= INSERTAR =============
        public int Insertar(clsTarifario obj)
        {
            SqlConnection cn = null;
            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Tarifario_Insertar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Tipo", obj.Tipo);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object)obj.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Periodo", obj.Periodo);
                cmd.Parameters.AddWithValue("@Valor", obj.Valor);

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally { if (cn != null) cn.Close(); }
        }

        // === ACTUALIZAR ===
        public bool Actualizar(clsTarifario obj)
        {
            SqlConnection cn = null;
            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Tarifario_Actualizar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Tarifario", obj.Id_Tarifario);
                cmd.Parameters.AddWithValue("@Tipo", obj.Tipo);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Periodo", obj.Periodo);
                cmd.Parameters.AddWithValue("@Valor", obj.Valor);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                if (cn != null) cn.Close();
            }
        }

        // === ELIMINAR ===
        public bool Eliminar(int id)
        {
            SqlConnection cn = null;
            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Tarifario_Eliminar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Tarifario", id);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                if (cn != null) cn.Close();
            }
        }

        // === LISTAR ===
        public List<clsTarifario> Listar()
        {
            List<clsTarifario> lista = new List<clsTarifario>();
            SqlConnection cn = null;

            try
            {
                cn = clsConexion.getInstance().GetSqlConnection();
                SqlCommand cmd = new SqlCommand("Tarifario_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new clsTarifario()
                    {
                        Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                        Tipo = dr["Tipo"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Descripcion = dr["Descripcion"].ToString(),
                        Periodo = Convert.ToInt32(dr["Periodo"]),
                        Valor = Convert.ToDecimal(dr["Valor"])
                    });
                }
            }
            finally { if (cn != null) cn.Close(); }

            return lista;
        }
    }
}
