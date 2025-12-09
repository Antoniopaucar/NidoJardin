using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLServicioAdicional
    {
        public List<clsEntidades.clsServicioAdicional> listar_servicioAdicional()
        {
            clsDAC.clsDacServicioAdicional xSer = new clsDAC.clsDacServicioAdicional();
            List<clsEntidades.clsServicioAdicional> xlista = xSer.ListarServicioAdicional();
            return xlista;
        }

        public void eliminar_servicioAdicional(int xcodigo)
        {
            try
            {
                clsDAC.clsDacServicioAdicional xSer = new clsDAC.clsDacServicioAdicional();
                xSer.EliminarServicioAdicional(xcodigo);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void insertar_servicioAdicional(clsEntidades.clsServicioAdicional xSer)
        {
            try
            {
                clsDAC.clsDacServicioAdicional db = new clsDAC.clsDacServicioAdicional();
                db.InsertarServicioAdicional(xSer);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_servicioAdicional(clsEntidades.clsServicioAdicional xSer)
        {
            try
            {
                clsDAC.clsDacServicioAdicional db = new clsDAC.clsDacServicioAdicional();
                db.ModificarServicioAdicional(xSer);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
    }
}
