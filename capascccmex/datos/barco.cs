using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
    public class barco
    {
         //Zona de declaraciones ....
        metadatos.barco obj = null;
        SqlServer oCon = null;
        List<metadatos.barco> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public barco()
        {
            obj = new metadatos.barco();
        }

        public String agregar(List<SqlParameter> campos)
        {
            String returnvalue = "F";
            using (oCon = new SqlServer())
            {

                foreach (SqlParameter p in campos)
                {
                    oCon.addParameter(p);
                }

                try
                {
                    oCon.executeNonQuery("proc_add" + this.GetType().Name);
                    returnvalue = oCon.getParameter("vr").ToString();
                    _errorMensaje = "";

                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                }
            }
            return returnvalue;
        }

        public List<metadatos.barco> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.barco>();

            using (oCon = new SqlServer())
            {

                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using ( SqlDataReader  drInfo = oCon.executeReader("proc_get" + this.GetType().Name))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.barco()
                            {
                                IdBarco = convertir.toNInt64(drInfo["idbarco"]),
                                Barco = drInfo["barco"].ToString() ,
                                Importacion = convertir.toNBoolean(drInfo["importacion"])
                            };

                            list.Add(obj);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                }

            }

            return list;
        }

        public String actualizar(List<SqlParameter> campos)
        {
            String returnvalue = "";
            using (oCon = new SqlServer())
            {

                foreach (SqlParameter p in campos)
                {
                    oCon.addParameter(p);
                }

                try
                {
                    oCon.executeNonQuery("proc_upd" + this.GetType().Name);
                    returnvalue = oCon.getParameter("vr").ToString();
                    _errorMensaje = "";// oCon.getParameter("@error").ToString();

                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                }
            }
            return returnvalue;
        }

        public String eliminar(List<SqlParameter> campos)
        {
            String returnvalue = "";
            using (oCon = new SqlServer())
            {

                foreach (SqlParameter p in campos)
                {
                    oCon.addParameter(p);
                }

                try
                {
                    oCon.executeNonQuery("proc_del" + this.GetType().Name);
                    returnvalue = oCon.getParameter("vr").ToString();
                    _errorMensaje = "";

                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                }
            }
            return returnvalue;
        }
    }
}
