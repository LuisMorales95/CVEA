using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class producto
    {
        public List<metadatos.producto> GetBizProducto(Int64? _idProdcto, int maximumRows, int startRowIndex)
        {
            datos.producto obj = new datos.producto();
            List<metadatos.producto> listaObjs = new List<metadatos.producto>();

            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vidproducto", _idProdcto));

            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }
    }
}
