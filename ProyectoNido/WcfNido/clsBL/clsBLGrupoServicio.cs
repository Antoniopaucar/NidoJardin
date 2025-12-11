using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsEntidades;
using clsDAC;

namespace clsBL
{
    public class clsBLGrupoServicio
    {
        public List<clsGrupoServicioDetalle> listar_grupos_servicio_por_docente(int idUsuario)
        {
            clsDACGrupoServicio dac = new clsDACGrupoServicio();
            return dac.ListarGruposServicioPorDocente(idUsuario);
        }

        /// <summary>
        /// Lista todas las ofertas de grupos de servicio del periodo actual para apoderados
        /// </summary>
        public List<clsGrupoServicioOferta> listar_ofertas_grupo_servicio()
        {
            clsDACGrupoServicio dac = new clsDACGrupoServicio();
            return dac.ListarOfertasGrupoServicio();
        }
    }
}

