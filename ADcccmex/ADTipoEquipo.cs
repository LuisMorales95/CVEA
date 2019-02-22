using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BEcccmex;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ADcccmex
{
    public class ADTipoEquipo
    {
        DatabaseProviderFactory factory;
        Database database;

        public ADTipoEquipo()
        {
            this.factory = new DatabaseProviderFactory();
            this.database = factory.CreateDefault();
        }

        public List<BETipoEquipo> getTipoEquipos()
        {
            List<BETipoEquipo> tipoEquipos = new List<BETipoEquipo>();
            DbCommand dbCommand = this.database.GetStoredProcCommand("proc_gettipoequipo");
            DataSet dataSet = this.database.ExecuteDataSet(dbCommand);

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                BETipoEquipo tipoEquipo = new BETipoEquipo();
                tipoEquipo.IdTipoEquipo = Convert.ToInt64(row["IDTIPOEQUIPO"]);
                tipoEquipo.Codigo = row["CODIGO"].ToString();
                tipoEquipo.TipoEquipo = row["TIPOEQUIPO"].ToString();
                tipoEquipo.Descripcion = row["DESCRIPCION"].ToString();
                tipoEquipos.Add(tipoEquipo);
            }

            return tipoEquipos;
        }
        public int AddTipoEquipo(BETipoEquipo tipoEquipo)
        {
            int resultId = 0;
            try
            {
                DbCommand dbCommand = database.GetStoredProcCommand("dbo.proc_addtipoequipo");
                database.AddOutParameter(dbCommand, "IDTIPOEQUIPO", System.Data.DbType.Int64, 0);
                database.AddInParameter(dbCommand, "CODIGO", System.Data.DbType.String, tipoEquipo.Codigo);
                database.AddInParameter(dbCommand, "TIPOEQUIPO", System.Data.DbType.String, tipoEquipo.TipoEquipo);
                database.AddInParameter(dbCommand, "DESCRIPCION", System.Data.DbType.String, tipoEquipo.Descripcion);
                resultId = database.ExecuteNonQuery(dbCommand);
                resultId = Convert.ToInt32(database.GetParameterValue(dbCommand, "IDTIPOEQUIPO"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultId;
        }
        public int UpdateTipoEquipo(BETipoEquipo tipoEquipo)
        {
            int result = 0;

            try
            {
                DbCommand command = database.GetStoredProcCommand("dbo.proc_updtipoequipo");
                database.AddOutParameter(command, "RETURNVAL", System.Data.DbType.Int64, 0);
                database.AddInParameter(command, "IDTIPOEQUIPO", System.Data.DbType.Int64, tipoEquipo.IdTipoEquipo);
                database.AddInParameter(command, "CODIGO", System.Data.DbType.String, tipoEquipo.Codigo);
                database.AddInParameter(command, "TIPOEQUIPO", System.Data.DbType.String, tipoEquipo.TipoEquipo);
                database.AddInParameter(command, "DESCRIPCION", System.Data.DbType.String, tipoEquipo.Descripcion);
                result = database.ExecuteNonQuery(command);
                result = Convert.ToInt32(database.GetParameterValue(command, "RETURNVAL"));

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }
        public int DeleteTipoEquipo(Int64? IdTipoEquipo)
        {
            int resultId = 0;
            try
            {
                DbCommand dbCommand = database.GetStoredProcCommand("dbo.proc_deltipoequipo");
                database.AddOutParameter(dbCommand, "RETURNVAL", System.Data.DbType.Int64, 0);
                database.AddInParameter(dbCommand, "IDTIPOEQUIPO", System.Data.DbType.Int64, IdTipoEquipo);
                resultId = database.ExecuteNonQuery(dbCommand);
                resultId = Convert.ToInt32(database.GetParameterValue(dbCommand, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultId;
        }
    }
}
