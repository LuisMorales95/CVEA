

namespace ADcccmex
{using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using BEcccmex;
    public class ADObjetoDiagrama
    {
        public List<BEObjetoDiagrama> GetObjetoDiagrama(int idEquipo, int idDiagrama, int idObjetoDiagrama)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEObjetoDiagrama> listaObjetoDiagrama = new List<BEObjetoDiagrama>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("catastro.OBJETODIAGRAMAGET");
            if (idEquipo > 0)
                db.AddInParameter(dbc, "@IDEQUIPO", System.Data.DbType.Int16, idEquipo);
            if (idDiagrama > 0)
                db.AddInParameter(dbc, "IDDIAGRAMA", System.Data.DbType.Int16, idDiagrama);
            if (idObjetoDiagrama > 0)
                db.AddInParameter(dbc, "IDOBJETODIAGRMA", System.Data.DbType.Int16, idObjetoDiagrama);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEObjetoDiagrama objObjetoDiagrama = new BEObjetoDiagrama();
                objObjetoDiagrama.idEquipo= Convert.ToInt32(dr["IDEQUIPO"]);
                objObjetoDiagrama.idDiagrama = Convert.ToInt32(dr["IDDIAGRAMA"]);
                objObjetoDiagrama.idObjetoDiagrama= Convert.ToInt32(dr["IDOBJETODIAGRMA"]);
                objObjetoDiagrama.nombre = dr["NOMBRE"].ToString();
                objObjetoDiagrama.descripcion = dr["DESCRIPCION"].ToString();
                objObjetoDiagrama.detalle = dr["DETALLE"].ToString();
                objObjetoDiagrama.color = dr["COLOR"].ToString();
                objObjetoDiagrama.objetoX = Convert.ToInt32(dr["OBJETOX"]);
                objObjetoDiagrama.objetoY = Convert.ToInt32(dr["OBJETOY"]);
                objObjetoDiagrama.objetoW = Convert.ToInt32(dr["OBJETOW"]);
                objObjetoDiagrama.objetoH = Convert.ToInt32(dr["OBJETOH"]);
                objObjetoDiagrama.avisoTexto = dr["AVISOTEXTO"].ToString();
                objObjetoDiagrama.avisoX = Convert.ToInt32(dr["AVISOX"]);
                objObjetoDiagrama.avisoY = Convert.ToInt32(dr["AVISOY"]);



                listaObjetoDiagrama.Add(objObjetoDiagrama);
            }

            return listaObjetoDiagrama;
        }


    }
}
