using System;
using System.Collections.Generic;
using clsDAC;
using clsEntidades;
using System.Data.SqlClient;

namespace clsBL
{
    public class clsBLReporteIngresos
    {
        public List<clsReporteIngreso> ListarReporte(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                clsDacReporteIngresos dac = new clsDacReporteIngresos();
                return dac.ListarReporte(idSalon, idDistrito, fechaInicio, fechaFin);
            }
            catch (SqlException ex)
            {
                // En una implementación real, se registraría el error
                throw new Exception("Error al obtener reporte de ingresos", ex);
            }
        }


        public List<clsReporteIngreso> ListarReporteCobranzas(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                clsDacReporteIngresos dac = new clsDacReporteIngresos();
                return dac.ListarReporteCobranzas(idSalon, idDistrito, fechaInicio, fechaFin);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener reporte de cobranzas", ex);
            }
        }
    }
}
