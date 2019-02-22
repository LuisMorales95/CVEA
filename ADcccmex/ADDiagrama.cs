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
   public class ADDiagrama
    {

       public List<BEDiagrama> GetDiagrama(int idinstalacion, int idDiagrama)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEDiagrama> listaDiagrama = new List<BEDiagrama>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("dbo.DIAGRAMAGET");
            if (idinstalacion > 0)
                db.AddInParameter(dbc, "@IDINSTALACION", System.Data.DbType.Int16, idinstalacion);
            if (idDiagrama > 0)
                db.AddInParameter(dbc, "IDDIAGRAMA", System.Data.DbType.Int16, idDiagrama);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEDiagrama objDiagrama = new BEDiagrama();
                objDiagrama.idDiagrama= Convert.ToInt32(dr["IDDIAGRAMA"]);
                objDiagrama.idInstalacion = Convert.ToInt32(dr["IDINSTALACION"]);
                objDiagrama.nombre = dr["NOMBRE"].ToString();
                objDiagrama.descripcion = dr["DESCRIPCION"].ToString();
                objDiagrama.archivo = dr["ARCHIVO"].ToString();
               

                listaDiagrama.Add(objDiagrama);
            }

            return listaDiagrama;
        }

    }
}
