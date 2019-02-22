using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEcccmex;
using ADcccmex;

namespace BLcccmex
{
    public class BLLineaTransporte
    {
        ADLineaTransporte transporte;

        public BLLineaTransporte()
        {
            this.transporte = new ADLineaTransporte();
        }

        public List<BELineaTransporte> GetLineaTransportes()
        {
            return transporte.GetLineaTransportes();
        }
        public int addLineaTransporte(BELineaTransporte bELineaTransporte)
        {
            return transporte.InsertLineaTransportes(bELineaTransporte);
        }
        public int updateLineaTransporte(BELineaTransporte lineaTransporte)
        {
            return transporte.UpdateLineaTransporte(lineaTransporte);
        }
        public int deleteLineaTransporte(Int64? idLineaTransporte)
        {
            return transporte.DeleteLineaTrasporte(idLineaTransporte);
        }
    }
}
