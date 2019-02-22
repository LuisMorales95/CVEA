using ADcccmex;
using BEcccmex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLcccmex
{
   public  class BLDiagrama
    {
       private int totalDiagrama = 0;

        public List<BEDiagrama> GetDiagrama(int idInstalacion)
        {
            ADDiagrama obj = new ADDiagrama();
            List<BEDiagrama> listaDiagrama = new List<BEDiagrama>();
            listaDiagrama = obj.GetDiagrama(idInstalacion, 0);

            totalDiagrama = listaDiagrama.Count();

            return listaDiagrama;
        }

        public int TotalDiagrama(int idCentro)
        {
            return totalDiagrama;
        }

    }
}
