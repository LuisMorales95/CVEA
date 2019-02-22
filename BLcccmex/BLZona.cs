using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEcccmex;
using ADcccmex;

namespace BLcccmex
{
    public class BLZona
    {
        private ADZona reqzona;

        public BLZona()
        {
            this.reqzona = new ADZona();
        }

        public List<BEZona> GetZonas()
        {
            return reqzona.GetZonas();
        }
        public int AddZona(BEZona zona)
        {
            return reqzona.InsertZona(zona);
        }
        public int UpdateZona(BEZona zona)
        {
            return reqzona.UpdateEquipo(zona);
        }
        public int DeleteZona(Int64? idZona)
        {
            return reqzona.DeleteZone(idZona);
        }
    }
}
