using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class usuarioweb
    {
        private String errorRegistros = "";
        public List<metadatos.usuarioweb> GetBizUsuarios(Int64? _idUsuarioWeb, int maximumRows, int startRowIndex)
        {
            datos.usuarioweb obj = new datos.usuarioweb();
            List<metadatos.usuarioweb> listaObjs = new List<metadatos.usuarioweb>();

            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("viappid", _idUsuarioWeb));

            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.usuarioweb> GetBizUsuariosByLogin(String login, int maximumRows, int startRowIndex)
        {

            datos.usuarioweb obj = new datos.usuarioweb();
            List<metadatos.usuarioweb> listaObjs = new List<metadatos.usuarioweb>();

          
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("viapplogin", login));            
            listaObjs = obj.obtenerByLogin(campos);           
            errorRegistros = obj.ErrorMensaje.ToString();

          
            return listaObjs;
        }

        public String ErrorRegistros()
        {
            return errorRegistros;
        }
    }
}
