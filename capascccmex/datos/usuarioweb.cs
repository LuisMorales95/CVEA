using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
    public class usuarioweb
    {
        //Zona de declaraciones ....
        metadatos.usuarioweb obj = null;
        SqlServer oCon = null;
        List<metadatos.usuarioweb> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public usuarioweb()
        {
            obj = new metadatos.usuarioweb();
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

        public List<metadatos.usuarioweb> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.usuarioweb>();

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
                            obj = new metadatos.usuarioweb()
                            {
                                IappId = convertir.toNInt64(drInfo["iappid"]),
                                IdCentro = convertir.toNInt64(drInfo["idcentro"]),
                                IappLogin = drInfo["iapplogin"].ToString()  ,
                                IappPwd = drInfo["iapppwd"].ToString(),
                                IappNombre_Completo = drInfo["iappnombre_completo"].ToString(),
                                IappCorreo = drInfo["iappcorreo"].ToString(),
                                IappActivo = Convert.ToBoolean(drInfo["iappactivo"]),
                                IappAdmin = Convert.ToBoolean(drInfo["iappadmin"]),
                                IappPemex = Convert.ToBoolean(drInfo["iapppemex"]),
                                Nombre_Centro = drInfo["centro"].ToString(),
                                Last_update = convertir.toNDateTime(drInfo["last_update"])
                                
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

        public List<metadatos.usuarioweb> obtenerByLogin(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.usuarioweb>();
            _errorMensaje = "ok";
            using (oCon = new SqlServer())
            {

                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader("proc_getusuariowebbylogin"))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.usuarioweb()
                            {
                                IappId = convertir.toNInt64(drInfo["iappid"]),
                                IdCentro = convertir.toNInt64(drInfo["idcentro"]),
                                IappLogin = drInfo["iapplogin"].ToString(),
                                IappPwd = drInfo["iapppwd"].ToString(),
                                IappNombre_Completo = drInfo["iappnombre_completo"].ToString(),
                                IappCorreo = drInfo["iappcorreo"].ToString(),
                                IappActivo = Convert.ToBoolean(drInfo["iappactivo"]),
                                IappAdmin = Convert.ToBoolean(drInfo["iappadmin"]),
                                IappPemex = Convert.ToBoolean(drInfo["iapppemex"]),
                                Nombre_Centro = drInfo["centro"].ToString(),
                                Last_update = convertir.toNDateTime(drInfo["last_update"])

                            };

                            list.Add(obj);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                    convertir.log("Error: " + ex.Message.ToString());
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

        public String actualizarpass(List<SqlParameter> campos)
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
                    oCon.executeNonQuery("proc_updpass" + this.GetType().Name);
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
