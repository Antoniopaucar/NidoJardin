using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLSalon
    {
        public List<clsEntidades.clsSalon> listar_salon()
        {
            clsDAC.clsDacSalon xsalon = new clsDAC.clsDacSalon();
            List<clsEntidades.clsSalon> xlistasalon = xsalon.listarSalon();
            return xlistasalon;
        }

        public void eliminar_salon(int xcodigo)
        {
            try
            {
                clsDAC.clsDacSalon xsal = new clsDAC.clsDacSalon();
                xsal.EliminarSalon(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_salon(clsEntidades.clsSalon xSal)
        {
            try
            {
                clsDAC.clsDacSalon db = new clsDAC.clsDacSalon();
                db.InsertarSalon(xSal);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_salon(clsEntidades.clsSalon xSal)
        {
            try
            {
                clsDAC.clsDacSalon db = new clsDAC.clsDacSalon();
                db.ModificarSalon(xSal);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
