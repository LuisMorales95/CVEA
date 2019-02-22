using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using BEcccmex;

namespace ADcccmex
{
    public class ADEquipo
    {

        public List<BEEquipo> GetEquipoXInstalacion(Int64? IDinstalacion)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEEquipo> listaEquipo = new List<BEEquipo>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("GETEQUIPOXINSTALACION");
            db.AddInParameter(dbc, "@IDINSTALACION", System.Data.DbType.Int16, IDinstalacion);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEEquipo objEquipo = new BEEquipo();
                objEquipo.idEquipo = Convert.ToInt32(dr["IDEQUIPO"]);
                objEquipo.nombre = dr["NOMBRE"].ToString();
                objEquipo.descripcion = dr["DESCRIPCION"].ToString();
                objEquipo.tag = dr["TAG"].ToString();
                objEquipo.detalle = dr["DETALLE"].ToString();

                listaEquipo.Add(objEquipo);
            }

            return listaEquipo;
        }
    }
}
