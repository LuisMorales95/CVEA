using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADcccmex;
using BEcccmex;

namespace BLcccmex
{
    public class BLTipoEquipo
    {
        ADTipoEquipo tipoEquipo;

        public BLTipoEquipo()
        {
            this.tipoEquipo = new ADTipoEquipo();
        }

        public List<BETipoEquipo> GetTipoEquipo()
        {
            return tipoEquipo.getTipoEquipos();
        }
        public int addTipoEquipo(BETipoEquipo bETipoEquipo)
        {
            return tipoEquipo.AddTipoEquipo(bETipoEquipo);
        }
        public int updateTipoEquipo(BETipoEquipo bETipoEquipo)
        {
            return tipoEquipo.UpdateTipoEquipo(bETipoEquipo);
        }
        public int deleteTipoEquipo(Int64? idTipoEquipo)
        {
            return tipoEquipo.DeleteTipoEquipo(idTipoEquipo);
        }
    }
}