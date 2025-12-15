using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLGrupoAnual_v
    {
        public List<clsGrupoAnual_v> Listar()
        {
            return new clsDAC.clsDacGrupoAnual_v().ListarGrupoAnual();
        }
    }
}
