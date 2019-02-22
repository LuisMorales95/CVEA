using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class orden_servicio
    {
        public List<metadatos.orden_servicio> GetBizOrdenServicio()
        {
            datos.orden_servicio obj = new datos.orden_servicio();
            List<metadatos.orden_servicio> listaObjs = new List<metadatos.orden_servicio>();

            //List<SqlParameter> campos = new List<SqlParameter>();
            //campos.Add(new SqlParameter("vidcentro", _idCentro));

            listaObjs = obj.obtener();
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }
    }
}
