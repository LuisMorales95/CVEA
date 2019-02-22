using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BEcccmex;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ADcccmex
{
    public class ADLineaTransporte
    {
        DatabaseProviderFactory factory;
        Database database;

        public ADLineaTransporte()
        {
            this.factory = new DatabaseProviderFactory();
            this.database = factory.CreateDefault();
        }

        public List<BELineaTransporte> GetLineaTransportes()
        {
            List<BELineaTransporte> transportes = new List<BELineaTransporte>();
            DbCommand dbCommand = this.database.GetStoredProcCommand("proc_getlineatransporte");
            DataSet dataSet = this.database.ExecuteDataSet(dbCommand);
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                BELineaTransporte eLineaTransporte = new BELineaTransporte();
                eLineaTransporte.IdLineaTransporte = Convert.ToInt32(dataRow["IDLINEATRANSPORTE"]);
                eLineaTransporte.LineaTransporte = dataRow["LINEATRANSPORTE"].ToString();
                eLineaTransporte.Descripcion = dataRow["DESCRIPCION"].ToString();
                transportes.Add(eLineaTransporte);
            }
            return transportes;
        }
        public int InsertLineaTransportes(BELineaTransporte eLineaTransporte)
        {
            int mResultID = 0;

            try
            {
                DbCommand dbCommand = database.GetStoredProcCommand("proc_addlineatransporte");
                this.database.AddOutParameter(dbCommand, "IDLINEATRANSPORTE", System.Data.DbType.Int64, 0);
                this.database.AddInParameter(dbCommand, "LINEATRANSPORTE", System.Data.DbType.String, eLineaTransporte.LineaTransporte);
                this.database.AddInParameter(dbCommand, "DESCRIPCION", System.Data.DbType.String, eLineaTransporte.Descripcion);
                mResultID = this.database.ExecuteNonQuery(dbCommand);
                mResultID = Convert.ToInt32(this.database.GetParameterValue(dbCommand, "IDLINEATRANSPORTE"));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return mResultID;
        }
        public int DeleteLineaTrasporte(Int64? idLineaTransporte)
        {
            int mResultId = 0;

            try
            {
                DbCommand dbCommand = database.GetStoredProcCommand("proc_dellineatransporte");
                this.database.AddOutParameter(dbCommand, "RETURNVAL", System.Data.DbType.Int64, 0);
                this.database.AddInParameter(dbCommand, "IDLINEATRANSPORTE", System.Data.DbType.Int64, idLineaTransporte);
                mResultId = this.database.ExecuteNonQuery(dbCommand);
                mResultId = Convert.ToInt32(this.database.GetParameterValue(dbCommand, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mResultId;
        }
        public int UpdateLineaTransporte(BELineaTransporte lineaTransporte)
        {
            int mResult = 0;

            try
            {
                DbCommand command = this.database.GetStoredProcCommand("proc_updlineatransporte");
                this.database.AddOutParameter(command, "RETURNVAL", System.Data.DbType.Int64, 0);
                this.database.AddInParameter(command, "IDLINEATRANSPORTE",
                    System.Data.DbType.Int64, lineaTransporte.IdLineaTransporte);
                this.database.AddInParameter(command, "LINEATRANSPORTE",
                    System.Data.DbType.String, lineaTransporte.LineaTransporte);
                this.database.AddInParameter(command, "DESCRIPCION",
                    System.Data.DbType.String, lineaTransporte.Descripcion);
                mResult = database.ExecuteNonQuery(command);
                mResult = Convert.ToInt32(database.GetParameterValue(command, "RETURNVAL"));

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mResult;
        }
    }
}
