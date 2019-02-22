using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class instalaciones
    {
        public List<metadatos.instalaciones> GetBizInstalaciones(int _idCentro, int maximumRows, int startRowIndex)
        {
            datos.instalaciones obj = new datos.instalaciones();
            List<metadatos.instalaciones> listaObjs = new List<metadatos.instalaciones>();

            List<SqlParameter> campos = new List<SqlParameter>();
            if (_idCentro > 0)
                campos.Add(new SqlParameter("vidcentro", _idCentro));

            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;



        }

        public List<metadatos.instalaciones> GetInstalaciones()
        {
            datos.instalaciones obj = new datos.instalaciones();
            List<metadatos.instalaciones> listaObjs = new List<metadatos.instalaciones>();

            listaObjs = obj.getInstalacionNotificacion();
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.instalaciones> GetInstalacionDiagrama(int idcentro)
        {
            datos.instalaciones obj = new datos.instalaciones();
            List<metadatos.instalaciones> listaObjs = new List<metadatos.instalaciones>();
            List<SqlParameter> campos = new List<SqlParameter>();
            if (idcentro > 0)
                campos.Add(new SqlParameter("vidcentro", idcentro));
            listaObjs = obj.getInstalacionDiagrama(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }
    }
}
