using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
   public class operatividad
    {
       //Zona de declaraciones ....
        metadatos.operatividad obj = null;
        SqlServer oCon = null;
        List<metadatos.operatividad> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public operatividad()
        {
            obj = new metadatos.operatividad();
        }

        public Int64? agregar(List<SqlParameter> campos)
        {
            Int64? returnvalue =0;
            using (oCon = new SqlServer())
            {

                foreach (SqlParameter p in campos)
                {
                    oCon.addParameter(p);
                }

                try
                {
                    oCon.executeNonQuery("proc_add" + this.GetType().Name);
                    returnvalue = convertir.toNInt64( oCon.getParameter("@idoperatividad"));
                    _errorMensaje = "";

                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                }
            }
            return returnvalue;
        }

        public List<metadatos.operatividad> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.operatividad>();

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
                            obj = new metadatos.operatividad()
                            {
                                IdOperatividad = convertir.toNInt64(drInfo["idoperatividad"]),
                                IdCentro = convertir.toNInt64(drInfo["idcentro"]),
                                
                                Cantidad_dia_anterior = drInfo["cantidad_dia_anterior"].ToString(),
                                Unidad_inspeccionada = drInfo["unidad_inspeccionada"].ToString(),
                                Unidad_pendiente = drInfo["unidad_pendiente"].ToString(),
                                Unidad_inspeccionada_hora = drInfo["unidad_inspeccionada_hora"].ToString(),
                                Unidad_pendiente_hora = drInfo["unidad_pendiente_hora"].ToString(),
                                Tanque_servicio = drInfo["tanque_servicio"].ToString(),
                                Problema_inspeccion = drInfo["problema_inspeccion"].ToString(),
                                Otras_observaciones = drInfo["otras_observaciones"].ToString(),
                                Cantidad_facturada = drInfo["cantidad_facturada"].ToString(),
                                Cantidad_facturada2 = drInfo["cantidad_facturada2"].ToString(),
                                Centro_centro = drInfo["centro_centro"].ToString(),
                                Equipos_Rechazados = drInfo["equipo_rechazado"].ToString(),
                                Fecha = convertir.toNDateTime(drInfo["fecha"])
                                
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
                    returnvalue = oCon.getParameter("@vr").ToString();
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
