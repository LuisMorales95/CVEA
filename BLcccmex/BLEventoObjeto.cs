using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADcccmex;
using BEcccmex;

namespace BLcccmex
{
    public class BLEventoObjeto
    {
        private int totalObjetoEvento = 0;

        public List<BEObjetoDiagrama> GetObjetoDiagrama(int idInstalacion, int idevento)
        {
            ADEventoObjeto obj = new ADEventoObjeto();
            List<BEObjetoDiagrama> listaObjetoDiagrama = new List<BEObjetoDiagrama>();
            listaObjetoDiagrama = obj.GetObjetoDiagrama(idInstalacion, idevento);

            totalObjetoEvento = listaObjetoDiagrama.Count();

            return listaObjetoDiagrama;
        }

        public int TotalObjetoEvento(int idEquipo, int idDiagrama)
        {
            return totalObjetoEvento;
        }

        public int EnviarCorreoEvento(int inst, string instalacion)
        {
            int r = 0;
            ADEventoObjeto obj = new ADEventoObjeto();
            try
            {
                r = obj.EnviarCorreoxInstalacion(inst, instalacion);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return r;
        }
    }
}
