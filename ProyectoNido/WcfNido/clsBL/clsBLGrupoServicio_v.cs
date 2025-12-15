using clsDAC;
using clsEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLGrupoServicio_v
    {
        public List<clsEntidades.clsGrupoServicio> listar_grupoServicio()
        {
            clsDAC.clsDacGrupoServicio_v dac = new clsDAC.clsDacGrupoServicio_v();
            List<clsEntidades.clsGrupoServicio> lista = dac.ListarGrupoServicio();
            return lista;
        }

        public void eliminar_grupoServicio(int idGrupoServicio)
        {
            try
            {
                clsDAC.clsDacGrupoServicio_v dac = new clsDAC.clsDacGrupoServicio_v();
                dac.EliminarGrupoServicio(idGrupoServicio);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void insertar_grupoServicio(clsEntidades.clsGrupoServicio grupo)
        {
            try
            {
                clsDAC.clsDacGrupoServicio_v dac = new clsDAC.clsDacGrupoServicio_v();
                dac.InsertarGrupoServicio(grupo);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void modificar_grupoServicio(clsEntidades.clsGrupoServicio grupo)
        {
            try
            {
                clsDAC.clsDacGrupoServicio_v dac = new clsDAC.clsDacGrupoServicio_v();
                dac.ModificarGrupoServicio(grupo);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<clsGrupoServicio> buscarGrupoServicio(string texto)
        {
            clsDacGrupoServicio_v dac = new clsDacGrupoServicio_v();
            return dac.buscarGrupoServicio(texto);
        }
    }
}
