using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using BEcccmex;
using capascccmex;

namespace ADcccmex
{
   public class ADEvento
    {
        public List<BEEvento> GetEvento(int idEquipo, int idEvento)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEEvento> listaEvento = new List<BEEvento>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("dbo.EVENTOGET");
            
            if (idEvento > 0)
                db.AddInParameter(dbc, "IDEVENTO", System.Data.DbType.Int16, idEvento);
            if (idEquipo > 0)
                db.AddInParameter(dbc, "IDEQUIPO", System.Data.DbType.Int16, idEquipo);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEEvento objEvento = new BEEvento();
                objEvento.idEvento= convertir.toNInt64(dr["IDEVENTO"]);
                objEvento.idEquipo = convertir.toNInt64(dr["IDEQUIPO"]);
                objEvento.evento = dr["EVENTO"].ToString();
                objEvento.tipoEvento = dr["TIPOEVENTO"].ToString();
                objEvento.prealarma = convertir.toNInt64(dr["PREALARMA"]);
                objEvento.fechaEvento = Convert.ToDateTime(dr["FECHAEVENTO"].ToString());
                objEvento.vigencia = dr["VIGENCIA"].ToString();
                objEvento.postAlarma = convertir.toNInt64(dr["POSTALARMA"]);
                objEvento.observacion = dr["OBSERVACION"].ToString();
                listaEvento.Add(objEvento);
            }

            return listaEvento;
        }


        public Boolean GetVerificaEnvioNotificacion(String fecha, int idcentro)
        {
            Boolean resultado = false;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEEvento> listaEvento = new List<BEEvento>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("dbo.ENVIONOTIFICACIONGET");

            db.AddInParameter(dbc, "FechaEnvio", System.Data.DbType.String, fecha);
            db.AddInParameter(dbc, "idcentro", System.Data.DbType.Int32, idcentro);
            DataSet ds = db.ExecuteDataSet(dbc);

            if (ds.Tables[0].Rows.Count > 0)
                resultado = true;
            else
                resultado = false;


            return resultado;
            
        }


        public int DelEvento(Int64? idEvento)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.EVENTODEL");

                db.AddOutParameter(dbc, "RETURNVAL", System.Data.DbType.Int64, 4);
                db.AddInParameter(dbc, "IDEVENTO", System.Data.DbType.Int64, idEvento);
                // db.AddInParameter(dbc, "error", System.Data.DbType.Int16, 0);
                //the return value is the number of rows affected by the command. 
                //For all other types of statements, the return value is -1.
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }


        public int AddEvento(BEEvento objProp)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.EVENTOADD");
                //output
                //db.AddInParameter(dbc, "IDENTIDADFEDERAL", System.Data.DbType.Int16, objProp.identidadfederal);
                db.AddOutParameter(dbc, "IDEVENTO", System.Data.DbType.Int64, 8);
                db.AddInParameter(dbc, "IDEQUIPO", System.Data.DbType.Int64, objProp.idEquipo);
                db.AddInParameter(dbc, "EVENTO", System.Data.DbType.String, objProp.evento);
                db.AddInParameter(dbc, "TIPOEVENTO", System.Data.DbType.String, objProp.tipoEvento);
                db.AddInParameter(dbc, "PREALARMA", System.Data.DbType.Int32, objProp.prealarma);
                db.AddInParameter(dbc, "FECHAEVENTO", System.Data.DbType.Date, objProp.fechaEvento);
                db.AddInParameter(dbc, "VIGENCIA", System.Data.DbType.String, objProp.vigencia);
                db.AddInParameter(dbc, "POSTALARMA", System.Data.DbType.Int32, objProp.postAlarma);
                db.AddInParameter(dbc, "OBSERVACION", System.Data.DbType.String, objProp.observacion);
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "IDEVENTO"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }


        public int AddEventoHistorico(int idEvento, int idEquipo, string fechaReal,int iduser)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.EVENTOHISTORIALADD");
                //output
                db.AddOutParameter(dbc, "IDEVENTOHISTORICO", System.Data.DbType.Int32, 8);
                db.AddInParameter(dbc, "IDEVENTO", System.Data.DbType.Int32, idEvento);
                db.AddInParameter(dbc, "IDEQUIPO", System.Data.DbType.Int32, idEquipo);
                db.AddInParameter(dbc, "FECHAREALIZACION", System.Data.DbType.String, fechaReal);
                db.AddInParameter(dbc, "IDUSUARIO", System.Data.DbType.Int32, iduser);
                
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "IDEVENTOHISTORICO"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }
        public int AddEnvioNotificacionEvento(String FechaEvento, int idcentro)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.ENVIONOTIFICACIONADD");
                //output
                db.AddOutParameter(dbc, "IDENVIONOTIFICACION", System.Data.DbType.Int32,4);
                db.AddInParameter(dbc, "FechaEnvio", System.Data.DbType.String, FechaEvento);
                db.AddInParameter(dbc, "idcentro", System.Data.DbType.Int32, idcentro);

                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "IDENVIONOTIFICACION"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }

        public int UpdateEnvioNotificacionEvento(DateTime FechaEvento, int idcentro, Boolean status)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.ENVIONOTIFICACIONUPDATE");
                //output
                db.AddOutParameter(dbc, "RETURNVAL", System.Data.DbType.Int64, 4);
                db.AddInParameter(dbc, "FechaEnvio", System.Data.DbType.DateTime, FechaEvento);
                db.AddInParameter(dbc, "estado", System.Data.DbType.Boolean, status);
                db.AddInParameter(dbc, "idcentro", System.Data.DbType.Int32, idcentro);
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }
        public int UpdateEvento(BEEvento objProp)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.EVENTOUPDATE");
              
                db.AddOutParameter(dbc, "RETURNVAL", System.Data.DbType.Int64, 4);
                db.AddInParameter(dbc, "IDEVENTO", System.Data.DbType.Int16, objProp.idEvento);
                db.AddInParameter(dbc, "IDEQUIPO", System.Data.DbType.Int16, objProp.idEquipo);
                db.AddInParameter(dbc, "EVENTO", System.Data.DbType.String, objProp.evento);
                db.AddInParameter(dbc, "TIPOEVENTO", System.Data.DbType.String, objProp.tipoEvento);
                db.AddInParameter(dbc, "PREALARMA", System.Data.DbType.Int16, objProp.prealarma);
                db.AddInParameter(dbc, "FECHAEVENTO", System.Data.DbType.DateTime, objProp.fechaEvento);
                db.AddInParameter(dbc, "VIGENCIA", System.Data.DbType.String, objProp.vigencia);
                db.AddInParameter(dbc, "POSTALARMA", System.Data.DbType.Int16, objProp.postAlarma);
                db.AddInParameter(dbc, "OBSERVACION", System.Data.DbType.String, objProp.observacion);
                //db.AddInParameter(dbc, "APELLIDO_MATERNO", System.Data.DbType.String, objProp.APELLIDO_MATERNO);
               // db.AddInParameter(dbc, "MODIFIEDBY", System.Data.DbType.Int16, objProp.ModifiedBY);
                // db.AddInParameter(dbc, "ACTIVO", System.Data.DbType.Boolean, objProp.Activo);

                //the return value is the number of rows affected by the command. 
                //For all other types of statements, the return value is -1.
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }



    }
    
}
