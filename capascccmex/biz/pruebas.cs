using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
   public  class pruebas
    {
       public List<metadatos.pruebas> GetBizPruebas()
       {
           datos.pruebas obj = new datos.pruebas();
           List<metadatos.pruebas> listaObjs = new List<metadatos.pruebas>();

           //List<SqlParameter> campos = new List<SqlParameter>();
           //campos.Add(new SqlParameter("vidbarco", _idBarco));

           listaObjs = obj.obtener();
           //totalRegistros = listaObjs.Count();

           return listaObjs;
       }

    }
}
