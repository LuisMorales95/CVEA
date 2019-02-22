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
    public class ADEventoEquipo
    {

        public List<BEcccmex.BEEventoEquipo> GetEventoEquipo(Int64? idCentro, Int64? idInstalacion)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEEventoEquipo> listaEventoEquipo = new List<BEEventoEquipo>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("dbo.proc_getevento_equipo");

           
            if (idCentro > 0)
                db.AddInParameter(dbc, "IDCENTRO", System.Data.DbType.Int64, idCentro);
            if (idInstalacion > 0)
                db.AddInParameter(dbc, "IDINSTALACION", System.Data.DbType.Int64, idInstalacion);

            DataSet ds = db.ExecuteDataSet(dbc);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEEventoEquipo objEventoEquipo = new BEEventoEquipo();
                //Almacenamos informacion del evento
                objEventoEquipo.IdEvento = convertir.toNInt64(dr["IDEVENTO"]);
                objEventoEquipo.IdEquipo = convertir.toNInt64(dr["IDEQUIPO"]);
                objEventoEquipo.Evento = dr["EVENTO"].ToString();
                objEventoEquipo.TipoAlarma = dr["TIPOEVENTO"].ToString();
                objEventoEquipo.Prealarma = convertir.toNInt64(dr["PREALARMA"]);
                objEventoEquipo.FechaEvento = Convert.ToDateTime(dr["FECHAEVENTO"]).ToString("dd/MM/yyyy");
                objEventoEquipo.Vigencia = dr["VIGENCIA"].ToString();
                objEventoEquipo.PostAlarma = convertir.toNInt64(dr["POSTALARMA"] ?? 0);
                objEventoEquipo.Observacion = dr["OBSERVACION"].ToString();
                //Almacenamos informacion del equipo
                objEventoEquipo.IdEquipo = convertir.toNInt64(dr["IDEQUIPO"]);
                objEventoEquipo.Equipo = dr["EQUIPO"].ToString();
                //Almacenamos informacion de la instalacion
                objEventoEquipo.IdInstalacion = convertir.toNInt64(dr["IDINST"]);
                objEventoEquipo.Instalacion = dr["INSTALACION"].ToString();
                //Almacenamos informacion del centro
                objEventoEquipo.IdCentro = convertir.toNInt64(dr["IDCENTRO"]);
                objEventoEquipo.Centro = dr["CENTRO"].ToString();

                listaEventoEquipo.Add(objEventoEquipo);
            }

            return listaEventoEquipo;
        }
    }
}
