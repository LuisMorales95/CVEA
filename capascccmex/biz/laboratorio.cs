using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
   public class laboratorio
    {
       public List<metadatos.laboratorio> GetBizLaboratorio(Int64? _idInst, int maximumRows, int startRowIndex)
       {
           datos.laboratorio obj = new datos.laboratorio();
           List<metadatos.laboratorio> listaObjs = new List<metadatos.laboratorio>();

           List<SqlParameter> campos = new List<SqlParameter>();
           campos.Add(new SqlParameter("vidinst", _idInst));

           listaObjs = obj.obtener(campos);
           //totalRegistros = listaObjs.Count();

           return listaObjs;
       }
    }
}
