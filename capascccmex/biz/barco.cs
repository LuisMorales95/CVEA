using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class barco
    {
        //private int totalRegistros = 0;

        public List<metadatos.barco> GetBizBarco(Int64? _idBarco, int maximumRows, int startRowIndex)
        {
            datos.barco obj = new datos.barco();
            List<metadatos.barco> listaObjs = new List<metadatos.barco>();

            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vidbarco", _idBarco));
            
            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        //public int TotalRegistros()
        //{
        //    return totalRegistros;
        //}
    }
}
