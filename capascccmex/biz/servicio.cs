using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class servicio
    {
        public List<metadatos.servicio> GetBizServicio(String _idServicio, int maximumRows, int startRowIndex)
        {
            datos.servicio obj = new datos.servicio();
            List<metadatos.servicio> listaObjs = new List<metadatos.servicio>();

            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vidservicio", _idServicio));

            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }
    }
}
