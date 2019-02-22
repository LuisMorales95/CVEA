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
        public List<BEEquipo> GetEquipo(int IdCentro, int idInstalacion)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEEquipo> listaEquipo = new List<BEEquipo>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("proc_getequipo");
            db.AddOutParameter(dbc, "@RETURNVAL", System.Data.DbType.Int16, 4);
            if (IdCentro > 0)
                db.AddInParameter(dbc, "@IDCENTRO", System.Data.DbType.Int16, IdCentro);
            if (idInstalacion > 0)
                db.AddInParameter(dbc, "@IDINST", System.Data.DbType.Int16, idInstalacion);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEEquipo objEquipo = new BEEquipo();
                objEquipo.IdCentro = Convert.ToInt32(dr["IDCENTRO"]);
                objEquipo.Centro = dr["CENTRO"].ToString();
                objEquipo.IdInstalacion = Convert.ToInt32(dr["IDINST"]);
                objEquipo.Instalacion = dr["INSTALACION"].ToString();
                objEquipo.idEquipo = Convert.ToInt32(dr["IDEQUIPO"]);
                objEquipo.nombre = dr["EQUIPO"].ToString();
                objEquipo.descripcion = dr["DESCRIPCION"].ToString();
                objEquipo.tag = dr["TAG"].ToString();
                objEquipo.detalle = dr["DETALLE"].ToString();

                listaEquipo.Add(objEquipo);
            }

            return listaEquipo;
        }
        public int DelEquipo(Int64? idEquipo)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.proc_delequipo");

                db.AddOutParameter(dbc, "RETURNVAL", System.Data.DbType.Int64, 4);
                db.AddInParameter(dbc, "IDEQUIPO", System.Data.DbType.Int64, idEquipo);
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "RETURNVAL"));
            }
            catch (Exception ex)
            {
                string xy = ex.Message.ToString();
                r=-2;
            }
            return r;
        }

        public int AddEquipo(BEEquipo objProp)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.proc_addequipo");
                db.AddOutParameter(dbc, "IDEQUIPO", System.Data.DbType.Int64, 8);
                db.AddInParameter(dbc, "IDINST", System.Data.DbType.Int64, objProp.IdInstalacion);
                db.AddInParameter(dbc, "NOMBRE", System.Data.DbType.String, objProp.nombre);
                db.AddInParameter(dbc, "DESCRIPCION", System.Data.DbType.String, objProp.descripcion);
                db.AddInParameter(dbc, "TAG", System.Data.DbType.String, objProp.tag);
                db.AddInParameter(dbc, "DETALLE", System.Data.DbType.String, objProp.detalle);
                r = db.ExecuteNonQuery(dbc);
                r = Convert.ToInt32(db.GetParameterValue(dbc, "IDEQUIPO"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return r;
        }

        public int UpdateEquipo(BEEquipo objProp)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            int r = 0;
            try
            {
                DbCommand dbc = db.GetStoredProcCommand("dbo.proc_updequipo");
                db.AddOutParameter(dbc, "RETURNVAL", System.Data.DbType.Int64, 8);
                db.AddInParameter(dbc, "IDEQUIPO", System.Data.DbType.Int64, objProp.idEquipo);
                db.AddInParameter(dbc, "IDINST", System.Data.DbType.Int64, objProp.IdInstalacion);
                db.AddInParameter(dbc, "NOMBRE", System.Data.DbType.String, objProp.nombre);
                db.AddInParameter(dbc, "DESCRIPCION", System.Data.DbType.String, objProp.descripcion);
                db.AddInParameter(dbc, "TAG", System.Data.DbType.String, objProp.tag);
                db.AddInParameter(dbc, "DETALLE", System.Data.DbType.String, objProp.detalle);
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
