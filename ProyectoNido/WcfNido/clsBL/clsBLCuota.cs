using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLCuota
    {
        public List<clsCuota> listar_Cuota(int? idTarifario = null)
        {
            return clsDacCuota.GetInstance().Listar(idTarifario);
        }

        public bool insertar_Cuota(clsCuota obj)
        {
            int nuevoId = clsDacCuota.GetInstance().Insertar(obj);
            return nuevoId > 0;
        }

        public bool actualizar_Cuota(clsCuota obj)
        {
            return clsDacCuota.GetInstance().Actualizar(obj);
        }

        public bool eliminar_Cuota(int idCuota)
        {
            return clsDacCuota.GetInstance().Eliminar(idCuota);
        }
    }
}
