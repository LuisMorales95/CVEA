using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
    public class laboratorio
    {
         //Zona de declaraciones ....
        metadatos.laboratorio obj = null;
        SqlServer oCon = null;       
        List<metadatos.laboratorio> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public laboratorio()
        {
            obj = new metadatos.laboratorio();
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

        public List<metadatos.laboratorio> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.laboratorio>();

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
                            //if (drInfo["fecha_calibr_mantto"] == null)
                            //    {
                            //    drInfo["fecha_calibr_mantto"] = "";
                            //    }
                             

                            obj = new metadatos.laboratorio();
                            
                                obj.Idlaboratorio = convertir.toNInt64(drInfo["idlaboratorio"]);
                                obj.Idinst = convertir.toNInt64(drInfo["idinst"]);
                                obj.Idprueba = convertir.toNInt64(drInfo["idprueba"]);

                                obj.Pruebas = drInfo["pruebas"].ToString();
                                obj.Metodo_astm = drInfo["metodo_astm"].ToString();
                                obj.Dispositivo_temp = drInfo["dispositivo_temp"].ToString();
                                obj.No_inf_calibr_temp = drInfo["no_inf_calibr_temp"].ToString();
                                obj.Hidrometro = drInfo["hidrometro"].ToString();
                                obj.No_inf_calibr_hid = drInfo["no_inf_calibr_hidr"].ToString();
                                obj.Probeta = drInfo["probeta"].ToString();
                                obj.No_inf_calibr_prob = drInfo["no_inf_calibr_prob"].ToString();
                                obj.Equipo_analisis = drInfo["equipo_analisis"].ToString();
                                obj.Modelo_marca = drInfo["modelo_marca"].ToString();

                                if (drInfo["fecha_calibr_mantto"] != DBNull.Value)
                                obj.Fecha_calibr_mantto = Convert.ToDateTime(drInfo["fecha_calibr_mantto"]).ToString("dd/MM/yyyy");
                                if (drInfo["fecha_vig_estandar"] != DBNull.Value)
                                obj.Fecha_vig_estandar = Convert.ToDateTime(drInfo["fecha_vig_estandar"]).ToString("dd/MM/yyyy");
                                obj.No_inf_calibr_equipo = drInfo["no_inf_calibr_equipo"].ToString();
                                obj.Estandar_verif_util = drInfo["estandar_verif_util"].ToString();
                                obj.Medidor_poro_memb = drInfo["medidor_poro_memb"].ToString();
                                obj.Inf_calibr_bal_analitica = drInfo["inf_calibr_bal_analitica"].ToString();
                                obj.Inf_calibr_tubo_cann = drInfo["inf_calibr_tubo_cann"].ToString();

                            

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
                    returnvalue = "F";
                    _errorMensaje = ex.Message.ToString();
                }
            }
            return returnvalue;
        }
    }
}
