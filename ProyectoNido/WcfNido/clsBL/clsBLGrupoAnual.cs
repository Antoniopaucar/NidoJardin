using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsEntidades;
using clsDAC;

namespace clsBL
{
    public class clsBLGrupoAnual
    {
        public List<clsGrupoAnualDetalle> ListarGruposPorDocente(int idUsuario)
        {
            clsDACGrupoAnual dac = new clsDACGrupoAnual();
            return dac.ListarGruposPorDocente(idUsuario);
        }
    }
}
