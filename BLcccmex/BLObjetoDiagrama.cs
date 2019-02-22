using ADcccmex;
using BEcccmex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLcccmex
{
    public class BLObjetoDiagrama
    {
        private int totalObjetoDiagrama = 0;

        public List<BEObjetoDiagrama> GetObjetoDiagrama(int idEquipo, int idDiagrama, int maximumRows, int startRowIndex)
        {
            ADObjetoDiagrama obj = new ADObjetoDiagrama();
            List<BEObjetoDiagrama> listaObjetoDiagrama = new List<BEObjetoDiagrama>();
            listaObjetoDiagrama = obj.GetObjetoDiagrama(idEquipo,idDiagrama, 0);

            totalObjetoDiagrama = listaObjetoDiagrama.Count();

            return listaObjetoDiagrama;
        }

        public int TotalObjetoDiagrama(int idEquipo, int idDiagrama)
        {
            return totalObjetoDiagrama;
        }

    }
}
