using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsConexion
    {
        private static clsConexion instance = null;
        private readonly string connectionString;

        // Constructor privado
        private clsConexion()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
        }

        // Patrón Singleton
        public static clsConexion getInstance()
        {
            if (instance == null)
                instance = new clsConexion();

            return instance;
        }

        // Devuelve una nueva conexión cerrada (el método que la llame la abrirá)
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
