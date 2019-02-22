using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
   public  class operatividad
    {
       public List<metadatos.operatividad> GetBizOperatividad(Int64? _idCentro,DateTime? _fecha )
       {
           datos.operatividad obj = new datos.operatividad();
           List<metadatos.operatividad> listaObjs = new List<metadatos.operatividad>();

           List<SqlParameter> campos = new List<SqlParameter>();
           campos.Add(new SqlParameter("@idcentro", _idCentro));
           campos.Add(new SqlParameter("@fecha", _fecha));

           listaObjs = obj.obtener(campos);
           //totalRegistros = listaObjs.Count();

           return listaObjs;
       }
    }
}
