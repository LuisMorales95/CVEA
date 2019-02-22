using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADcccmex;
using BEcccmex;

namespace BLcccmex
{
    public class BLEventoEquipo
    {
        private int totalEventoEquipo = 0;

        public int TotalEventoEquipo
        {
            get { return totalEventoEquipo; }
            set { totalEventoEquipo = value; }
        }

        public List<BEEventoEquipo> GetEvento(Int64? idCentro, Int64? idInstalacion)
        {
            ADEventoEquipo obj = new ADEventoEquipo();
            List<BEEventoEquipo> listaEventoEquipo = new List<BEEventoEquipo>();
            listaEventoEquipo = obj.GetEventoEquipo(idCentro, idInstalacion);

            totalEventoEquipo = listaEventoEquipo.Count();

            return listaEventoEquipo;
        }
    }
}
