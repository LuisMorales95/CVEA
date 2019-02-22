using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class centro
    {
        public List<metadatos.centro> GetBizCentro(Int64? _idCentro, int maximumRows, int startRowIndex)
        {
            datos.centro obj = new datos.centro();
            List<metadatos.centro> listaObjs = new List<metadatos.centro>();

            List<SqlParameter> campos = new List<SqlParameter>();
            if (_idCentro > 0)
            campos.Add(new SqlParameter("vidcentro", _idCentro));

            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }
    }
}
