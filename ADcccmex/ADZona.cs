using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEcccmex;
using capascccmex;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ADcccmex
{
    public class ADZona
    {
        private DatabaseProviderFactory factory;
        private Database database;

        public ADZona()
        {
            this.factory = new DatabaseProviderFactory();
            this.database = this.factory.CreateDefault();
        }

        public List<BEZona> GetZonas()
        {

            List<BEZona> zonas = new List<BEZona>();

            DbCommand dbCommand = database.GetStoredProcCommand("proc_getzona");
            DataSet dataSet = database.ExecuteDataSet(dbCommand);

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                BEZona zonaObject = new BEZona();
                zonaObject.IdZona = Convert.ToInt32(dataRow["IDZONA"]);
                zonaObject.Zona = dataRow["ZONA"].ToString();
                zonaObject.Descripcion = dataRow["DESCRIPCION"].ToString();
                zonas.Add(zonaObject);
            }

            return zonas;
        }
        public int InsertZona(BEZona newZone)
        {
            int resultId = 0;
            try
            {
                DbCommand dbCommand = database.GetStoredProcCommand("dbo.proc_addzona");
                database.AddOutParameter(dbCommand, "IDZONA", System.Data.DbType.Int64, 0);
                database.AddInParameter(dbCommand, "ZONA", System.Data.DbType.String, newZone.Zona);
                database.AddInParameter(dbCommand, "DESCRIPCION", System.Data.DbType.String, newZone.Descripcion);
                resultId = database.ExecuteNonQuery(dbCommand);
                resultId = Convert.ToInt32(database.GetParameterValue(dbCommand, "IDZONA"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultId;
        }
        public int DeleteZone(Int64? idzone)
        {
            int resultId = 0;
            try
            {
                DbCommand dbCommand = database.GetStoredProcCommand("dbo.proc_delzona");
                database.AddOutParameter(dbCommand, "RETURNVAL", System.Data.DbType.Int64, 0);
                database.AddInParameter(dbCommand, "IDZONA", System.Data.DbType.Int64, idzone);
                resultId = database.ExecuteNonQuery(dbCommand);
                resultId = Convert.ToInt32(database.GetParameterValue(dbCommand, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultId;
        }
        public int UpdateEquipo(BEZona zona)
        {
            int result = 0;

            try
            {
                DbCommand command = database.GetStoredProcCommand("dbo.proc_updzona");
                database.AddOutParameter(command, "RETURNVAL", System.Data.DbType.Int64, 0);
                database.AddInParameter(command, "IDZONA", System.Data.DbType.Int64, zona.IdZona);
                database.AddInParameter(command, "ZONA", System.Data.DbType.String, zona.Zona);
                database.AddInParameter(command, "DESCRIPCION", System.Data.DbType.String, zona.Descripcion);
                result = database.ExecuteNonQuery(command);
                result = Convert.ToInt32(database.GetParameterValue(command, "RETURNVAL"));

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }

    }
}
