using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
    public class instalaciones
    {
         //Zona de declaraciones ....
        metadatos.instalaciones obj = null;
        SqlServer oCon = null;       
        List<metadatos.instalaciones> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public instalaciones()
        {
            obj = new metadatos.instalaciones();
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

        public List<metadatos.instalaciones> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.instalaciones>();

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
                            obj = new metadatos.instalaciones()
                            {
                                IdInst = Convert.ToInt32(drInfo["idinst"]),
                                IdCentro = Convert.ToInt32(drInfo["idcentro"]),
                                Nombre = drInfo["nombre"].ToString()                                
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

        public List<metadatos.instalaciones> getInstalacionDiagrama(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.instalaciones>();

            using (oCon = new SqlServer())
            {
                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader("GetInstalacionDiagrama"))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.instalaciones()
                            {
                                IdInst = Convert.ToInt32(drInfo["IDINSTALACION"]),
                                IdCentro = Convert.ToInt32(drInfo["idcentro"]),
                                Nombre = drInfo["NOMBRE"].ToString()
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
        public List<metadatos.instalaciones> getInstalacionNotificacion()
        {
            list = new List<metadatos.instalaciones>();

            using (oCon = new SqlServer())
            {

                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader("InstalacionNotificacion"))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.instalaciones()
                            {
                                IdInst = Convert.ToInt32(drInfo["idinst"]),
                                IdCentro = Convert.ToInt32(drInfo["idcentro"]),
                                Nombre = drInfo["nombre"].ToString()
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
