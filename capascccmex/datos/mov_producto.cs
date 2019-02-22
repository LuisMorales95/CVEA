using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
    public class mov_producto
    {
        //Zona de declaraciones ....
        metadatos.movproducto obj = null;
        SqlServer oCon = null;
        List<metadatos.movproducto> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public mov_producto()
        {
            obj = new metadatos.movproducto();
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

        public List<metadatos.movproducto> obtenerGerencia(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.movproducto>();

            using (oCon = new SqlServer())
            {

                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader("proc_get" + this.GetType().Name))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.movproducto()
                            {
                                IdReg = convertir.toNInt64(drInfo["idreg"]),
                                Estatus_revisado = drInfo["estatus_revisado"].ToString(),
                                Estatus_pagado = drInfo["estatus_pagado"].ToString(),
                                Orden_servicio = drInfo["orden_servicio"].ToString(),
                                IdProducto = convertir.toNInt64(drInfo["idproducto"]),
                                IdCentro = convertir.toNInt64(drInfo["idcentro"]),
                                IdInst = convertir.toNInt64(drInfo["idinst"]),
                                IdServicio = drInfo["idservicio"].ToString(),
                                IdBarco = convertir.toNInt64(drInfo["idbarco"]),
                                Cant_insp_mezcla =  convertir.toNDecimal(drInfo["cant_insp_mezcla"]),
                                Propileno = convertir.toNDecimal(drInfo["propileno"]),
                                NombreProducto = drInfo["nombreProducto"].ToString(),
                                NombreCentro = drInfo["nombreCentro"].ToString(),
                                NombreServicio = drInfo["nombreServicio"].ToString(),
                                Folio_cert_calidad = drInfo["referencia_folio"].ToString().Split(new char[]{'|'},2)[0],
                                Folio_cert_cant = drInfo["referencia_folio"].ToString().Split(new char[] { '|' }, 2)[1],
                                Fecha = convertir.toNDateTime(drInfo["fecha"]),
                                Referencia_folio = drInfo["referencia_folio"].ToString(),
                                Folio_cert_calidad_aux = drInfo["folio_cert_calidad"].ToString(),
                                Folio_cert_cant_aux = drInfo["folio_cert_cant"].ToString(),
                                NombreBarco = drInfo["nombreBarco"].ToString(),
                                Comentarios = drInfo["comentarios"].ToString(),
                                BarcoImp=Convert.ToBoolean(drInfo["barcos_importacion"]),
                                Idregbyprod = convertir.toNInt64(drInfo["idregbyprod"])
                                
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

        public List<metadatos.movproducto> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.movproducto>();

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
                            obj = new metadatos.movproducto()
                            {
                                IdReg = convertir.toNInt64( drInfo["idreg"]),
                                Estatus_revisado = drInfo["estatus_revisado"].ToString(),
                                Estatus_pagado = drInfo["estatus_pagado"].ToString(),
                                Orden_servicio = drInfo["orden_servicio"].ToString(),
                                IdProducto = convertir.toNInt64(drInfo["idproducto"]),
                                IdCentro = convertir.toNInt64(drInfo["idcentro"]),
                                IdInst = convertir.toNInt64(drInfo["idinst"]),
                                IdServicio = drInfo["idservicio"].ToString(),
                                IdBarco = convertir.toNInt64(drInfo["idbarco"]),                                
                                Cant_insp_mezcla = convertir.toNDecimal(drInfo["cant_insp_mezcla"]),
                                Propileno = convertir.toNDecimal(drInfo["propileno"]),
                                NombreProducto = drInfo["nombreProducto"].ToString(),
                                NombreCentro = drInfo["nombreCentro"].ToString(),
                                NombreServicio = drInfo["nombreServicio"].ToString(),
                                Folio_cert_calidad = drInfo["folio_cert_calidad"].ToString(),
                                Folio_cert_cant = drInfo["folio_cert_cant"].ToString(),
                                Fecha = convertir.toNDateTime(drInfo["fecha"]),
                                Referencia_folio = drInfo["referencia_folio"].ToString(),
                                NombreBarco = drInfo["nombreBarco"].ToString(),
                                Comentarios = drInfo["comentarios"].ToString(),
                                Folio_cert_calidad_aux = drInfo["folio_cert_calidad"].ToString(),
                                Folio_cert_cant_aux = drInfo["folio_cert_cant"].ToString(),
                                BarcoImp=Convert.ToBoolean(drInfo["barcos_importacion"]),
                                Idregbyprod = convertir.toNInt64(drInfo["idregbyprod"])
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
        
        public String actualizarByPemex(List<SqlParameter> campos)
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
                    oCon.executeNonQuery("proc_updmov_productoByPemex");
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

        public String actualizarByFile(List<SqlParameter> campos)
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
                    oCon.executeNonQuery("proc_updmov_productoByFile");
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

        public List<metadatos.movproducto> obtenerAcumuladoByCentro(List<SqlParameter> filtros = null,string sprocedure="")
        {
            list = new List<metadatos.movproducto>();

            using (oCon = new SqlServer())
            {

                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader(sprocedure))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.movproducto()
                            {
                               
                                IdInst = convertir.toNInt64(drInfo["idinst"]),
                                IdServicio = drInfo["idservicio"].ToString(),
                                NombreServicio = drInfo["servicio"].ToString(),
                                Cant_insp_mezcla = convertir.toNDecimal(drInfo["Cantidad"]),
                                Mes = convertir.toNInt16(drInfo["mes"]),
                                Year = convertir.toNInt16(drInfo["anio"]),                             
                                NombreCentro = drInfo["nombre"].ToString()                              

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

        public List<metadatos.movproducto> obtenerAcumuladoByServicio(List<SqlParameter> filtros = null, string sprocedure = "")
        {
            list = new List<metadatos.movproducto>();

            using (oCon = new SqlServer())
            {

                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader(sprocedure))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.movproducto()
                            {

                               
                                IdServicio = drInfo["idservicio"].ToString(),
                                NombreServicio = drInfo["servicio"].ToString(),
                                Cant_insp_mezcla = convertir.toNDecimal(drInfo["Cantidad"]),
                                Mes = convertir.toNInt16(drInfo["mes"]),
                                Year = convertir.toNInt16(drInfo["anio"]),
                             

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
    }
}
