using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Ionic.Zip;
using System.Text;

namespace appwebcccmex
{
    public partial class submanagementpemex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    //Session["getIdCentroUsr"] = 1;
                    Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);

                    cargarcentros();
                    cargarProducto();
                    cargarServicio();
                    cargarMovimientos(_idCentro, null, null);
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region EVENTOS DE OPERACIONES
        void cargarMovimientos(Int64? _idCentro, DateTime? _fecha, DateTime? _fecha2)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();

            try
            {
                oCamposCat = obj.GetBizProducto02(null, null, null, null, _idCentro, null, null, null, null, _fecha, _fecha2, false, 0, 0);
                Session["getCamposCatMovimiento"] = oCamposCat;
                gridCapturas.DataSource = oCamposCat;
                gridCapturas.DataBind();
                gridCapturas.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando relación de movimientos", null);
            }
        }

        bool existe(List<string> strlist, string variable)
        {
            bool bit = false;
            var result = from file in strlist
                         where file == variable
                         select file;

            foreach (var ir in result)
            {
                bit = true;
            }

            return bit;
        }

        void loadarchivospdfs(Int64? idReg)
        {
            string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\";

            List<capascccmex.metadatos.movproducto> oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];

            string FileCalidad = "";
            string FileCalidadA = "";
            string FileCalidadB = "";
            string FileCalidadC = "";
            string FileCalidadD = "";
            string FileCantidad = "";
            string[] referencia = null;

            //List<string> mycollection = new List<string>();
            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string strUrlPage = "";// strUrl + @"filecert\1\698T0115424C_B.pdf";
            StringBuilder sbJs = new StringBuilder();


            foreach (var info in oCamposCat.Where(x => x.IdReg == idReg))
            {
                referencia = info.Referencia_folio.ToString().Split('|');
                FileCalidad = string.Format("{0}{1}/{2}.pdf", path, info.IdCentro, referencia[0]);
                FileCalidadA = string.Format("{0}{1}/{2}_A.pdf", path, info.IdCentro, referencia[0]);
                FileCalidadB = string.Format("{0}{1}/{2}_B.pdf", path, info.IdCentro, referencia[0]);
                FileCalidadC = string.Format("{0}{1}/{2}_C.pdf", path, info.IdCentro, referencia[0]);
                FileCalidadD = string.Format("{0}{1}/{2}_D.pdf", path, info.IdCentro, referencia[0]);
                FileCantidad = string.Format("{0}{1}/{2}.pdf", path, info.IdCentro, referencia[1]);

                //string strUrlPage1 = strUrl + string.Format(@"filecert/{0}/{1}_A.pdf", info.IdCentro.ToString(), referencia[0]);// @"filecert\1\698T0115424C_B.pdf";
                //string strUrlPage2 = strUrl + string.Format(@"filecert/{0}/{1}_B.pdf", info.IdCentro.ToString(), referencia[0]);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrlPage1 + "','_blank'); window.open('" + strUrlPage1 + "','_blank')", true);

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrlPage + "','_blank')", true);

                //res = mycollection.Where(o => o == FileCalidad);
                if (File.Exists(FileCalidad))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}/{1}.pdf", info.IdCentro.ToString(), referencia[0]);
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");
                }


                if (File.Exists(FileCalidadA))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}/{1}_A.pdf", info.IdCentro.ToString(), referencia[0]);
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");
                }


                if (File.Exists(FileCalidadB))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}/{1}_B.pdf", info.IdCentro.ToString(), referencia[0]);
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");
                }


                if (File.Exists(FileCalidadC))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}/{1}_C.pdf", info.IdCentro.ToString(), referencia[0]);
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");
                }


                if (File.Exists(FileCalidadD))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}/{1}_D.pdf", info.IdCentro.ToString(), referencia[0]);
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");
                }

                if (File.Exists(FileCantidad))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}/{1}.pdf", info.IdCentro.ToString(), referencia[1]);
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");

                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", sbJs.ToString(), true);

            }

        }

        void exportarzip()
        {
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("cccmex_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);

            string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\";
            List<capascccmex.metadatos.movproducto> oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];

            string FileCalidad = "";
            string FileCalidadA = "";
            string FileCalidadB = "";
            string FileCalidadC = "";
            string FileCalidadD = "";
            string FileCantidad = "";
            string[] referencia = null;
            try
            {
                using (ZipFile zip = new ZipFile())
                {

                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    List<string> mycollection = new List<string>();
                    //mycollection.Add("str");
                    //var res = "";
                    //zzip.AddDirectoryByName("filesCccmex");

                    foreach (var info in oCamposCat)
                    {
                        referencia = info.Referencia_folio.ToString().Split('|');
                        FileCalidad = string.Format("{0}{1}/{2}.pdf", path, info.IdCentro, referencia[0]);
                        FileCalidadA = string.Format("{0}{1}/{2}_A.pdf", path, info.IdCentro, referencia[0]);
                        FileCalidadB = string.Format("{0}{1}/{2}_B.pdf", path, info.IdCentro, referencia[0]);
                        FileCalidadC = string.Format("{0}{1}/{2}_C.pdf", path, info.IdCentro, referencia[0]);
                        FileCalidadD = string.Format("{0}{1}/{2}_D.pdf", path, info.IdCentro, referencia[0]);
                        FileCantidad = string.Format("{0}{1}/{2}.pdf", path, info.IdCentro, referencia[1]);

                        //res = mycollection.Where(o => o == FileCalidad);
                        if (File.Exists(FileCalidad) && existe(mycollection, FileCalidad) == false)
                        {
                            zip.AddFile(FileCalidad, string.Format(@"filesCccmex\{0}", info.IdCentro.ToString()));
                            mycollection.Add(FileCalidad);
                        }

                        //res = mycollection.FirstOrDefault(x => x == FileCalidadA);
                        if (File.Exists(FileCalidadA) && existe(mycollection, FileCalidadA) == false)
                        {
                            zip.AddFile(FileCalidadA, string.Format(@"filesCccmex\{0}", info.IdCentro.ToString()));
                            mycollection.Add(FileCalidadA);
                        }

                        //res = mycollection.FirstOrDefault(x => x == FileCalidadB);
                        if (File.Exists(FileCalidadB) && existe(mycollection, FileCalidadB) == false)
                        {
                            zip.AddFile(FileCalidadB, string.Format(@"filesCccmex\{0}", info.IdCentro.ToString()));
                            mycollection.Add(FileCalidadB);
                        }

                        //res = mycollection.FirstOrDefault(x => x == FileCalidadC);
                        if (File.Exists(FileCalidadC) && existe(mycollection, FileCalidadC) == false)
                        {
                            zip.AddFile(FileCalidadC, string.Format(@"filesCccmex\{0}", info.IdCentro.ToString()));
                            mycollection.Add(FileCalidadC);
                        }

                        //res = mycollection.FirstOrDefault(x => x == FileCalidadD);
                        if (File.Exists(FileCalidadD) && existe(mycollection, FileCalidadD) == false)
                        {
                            zip.AddFile(FileCalidadD, string.Format(@"filesCccmex\{0}", info.IdCentro.ToString()));
                            mycollection.Add(FileCalidadD);
                        }

                        if (File.Exists(FileCantidad) && existe(mycollection, FileCantidad) == false)
                        {
                            zip.AddFile(FileCantidad, string.Format(@"filesCccmex\{0}", info.IdCentro.ToString()));
                            mycollection.Add(FileCantidad);
                        }


                    }


                    zip.Save(Response.OutputStream);
                    Response.Flush();
                    Response.End();

                    // string zipName = String.Format("cccmex_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    //byte[] bts = System.IO.File.ReadAllBytes(pathFile);
                    //Response.Clear();
                    //Response.ClearHeaders();
                    //Response.AddHeader("Content-Type", "Application/octet-stream");
                    //Response.AddHeader("Content-Length", bts.Length.ToString());
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + zipName);
                    //Response.BinaryWrite(bts);
                    //Response.Flush();
                    //Response.End();

                }

            }
            catch (Exception ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Exportando archivo Zip", null);
            }
        }

        void exportarAccess()
        {
            try
            {


                string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + "\\";
                List<capascccmex.metadatos.movproducto> oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];
                int del = deleteInspeccionInfo(path);
                int ins = insertEmployeeInfo(oCamposCat, path);
                if (ins > 0)
                {
                    dounloadXml("Databasecccmex.accdb");
                }
            }
            catch (Exception ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Exportando archivo Access", null);
            }

        }


        private void dounloadXml(string archivo)
        {
            string pathFile = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + "\\" + archivo.ToString();
            string filename = archivo.ToString();

            if (File.Exists(pathFile))
            {
                byte[] bts = System.IO.File.ReadAllBytes(pathFile);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bts);
                Response.Flush();
                Response.End();
            }
            else
            {
                windowManager1.RadAlert("Este trámite aun no tiene archivo disponible, favor de verificar...", 300, 150, "Descargando Archivos", null);
            }
        }

        void cargarcentros()
        {
            List<capascccmex.metadatos.centro> oCamposCat = new List<capascccmex.metadatos.centro>();
            capascccmex.biz.centro obj = new capascccmex.biz.centro();
            Dictionary<Int64?, string> dcat = new Dictionary<Int64?, string>();

            RadComboBox cmbcat = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbcentro");
            try
            {
                oCamposCat = obj.GetBizCentro(null, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dcat.Add(convertir.toNInt64(item.IdCentro), (string)item.Centro);
                }

                cmbcat.DataSource = dcat;
                cmbcat.DataTextField = "Value";
                cmbcat.DataValueField = "Key";
                cmbcat.DataBind();

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de centros", null);
            }
        }

        void cargarProducto()
        {
            List<capascccmex.metadatos.producto> oCamposCat = new List<capascccmex.metadatos.producto>();
            capascccmex.biz.producto obj = new capascccmex.biz.producto();

            Dictionary<Int64?, string> dcat = new Dictionary<Int64?, string>();
            RadComboBox cmbcat = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbproducto");
            try
            {
                oCamposCat = obj.GetBizProducto(null, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dcat.Add(convertir.toNInt64(item.IdProducto), (string)item.Producto);
                }

                cmbcat.DataSource = dcat;
                cmbcat.DataTextField = "Value";
                cmbcat.DataValueField = "Key";
                cmbcat.DataBind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de productos", null);
            }
        }

        void cargarServicio()
        {
            List<capascccmex.metadatos.servicio> oCamposCat = new List<capascccmex.metadatos.servicio>();
            capascccmex.biz.servicio obj = new capascccmex.biz.servicio();

            Dictionary<string, string> dcat = new Dictionary<string, string>();
            RadComboBox cmbcat = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbservicio");
            try
            {
                oCamposCat = obj.GetBizServicio(null, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dcat.Add((string)item.IdServicio, (string)item.Servicio);
                }

                cmbcat.DataSource = dcat;
                cmbcat.DataTextField = "Value";
                cmbcat.DataValueField = "Key";
                cmbcat.DataBind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de Servicio", null);
            }
        }
        #endregion

        #region OLEDB
        //Crecte and Return a OleDbConnection obj.
        public int deleteInspeccionInfo(string path)
        {
            string queryString = @"DELETE FROM INSPECCION";
            return Convert.ToInt32(executeQuery(queryString, path));
        }

        public int insertEmployeeInfo(List<capascccmex.metadatos.movproducto> iInfo, string path)
        {
            string[] referencia = null;
            int contador = 0;
            foreach (var info in iInfo.OrderBy(x => x.Idregbyprod))
            {
                string pathfile = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\";
                referencia = info.Referencia_folio.ToString().Split('|');
                string FileCalidad = File.Exists(string.Format(@"{0}{1}\{2}.pdf", pathfile, info.IdCentro, referencia[0])) == true ? string.Format(@"#C:\filesCccmex\{0}\{1}.pdf#", info.IdCentro, referencia[0]) : "@";
                string FileCalidadA = File.Exists(string.Format(@"{0}{1}\{2}_A.pdf", pathfile, info.IdCentro, referencia[0])) == true ? string.Format(@"#C:\filesCccmex\{0}\{1}_A.pdf#", info.IdCentro, referencia[0]) : "@";
                string FileCalidadB = File.Exists(string.Format(@"{0}{1}\{2}_B.pdf", pathfile, info.IdCentro, referencia[0])) == true ? string.Format(@"#C:\filesCccmex\{0}\{1}_B.pdf#", info.IdCentro, referencia[0]) : "@";
                string FileCalidadC = File.Exists(string.Format(@"{0}{1}\{2}_C.pdf", pathfile, info.IdCentro, referencia[0])) == true ? string.Format(@"#C:\filesCccmex\{0}\{1}_C.pdf#", info.IdCentro, referencia[0]) : "@";
                string FileCalidadD = File.Exists(string.Format(@"{0}{1}\{2}_D.pdf", pathfile, info.IdCentro, referencia[0])) == true ? string.Format(@"#C:\filesCccmex\{0}\{1}_D.pdf#", info.IdCentro, referencia[0]) : "@";

                string FileCantidad = File.Exists(string.Format(@"{0}{1}\{2}.pdf", pathfile, info.IdCentro, referencia[1])) == true ? string.Format(@"#C:\filesCccmex\{0}\{1}.pdf#", info.IdCentro, referencia[1]) : "@";

                string bimporta = "";
                if (info.IdBarco > 0)
                    bimporta = info.BarcoImp == true ? "SI" : "NO";

                string queryString = "INSERT INTO INSPECCION (IDPRODUCTO,PRODUCTO,IDCENTRO,CENTRO,IDSERVICIO,servicio,BARCO,CANTIDAD,AÑO,MES,FECHA,NUM_ORDEN_SERVICIO,STATUS,OBSERVACIONES,CERT_CANTIDAD,CERT_CALIDAD,CERT_CALIDAD_A,CERT_CALIDAD_B,CERT_CALIDAD_C,CERT_CALIDAD_D,REGISTRO,FOLIO_CANTIDAD,FOLIO_CALIDAD,propileno,BARCO_IMP) " +
 string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}-{7}',{8},{9},{10},'{11:dd/MM/yyyy}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',{24},'{25}')",
 info.IdProducto, info.NombreProducto,   //0-1
 info.IdInst, info.NombreCentro,       //2-3
 info.IdServicio, info.NombreServicio,    //4-5
 info.IdBarco, info.NombreBarco,
 info.Cant_insp_mezcla, info.Fecha.Value.Year, info.Fecha.Value.Month, string.Format("{0:dd}/{1:MM}/{2:yyyy}", info.Fecha, info.Fecha, info.Fecha), info.Orden_servicio,
 info.Estatus_pagado, info.Comentarios.ToString(), FileCantidad, FileCalidad, FileCalidadA, FileCalidadB, FileCalidadC, FileCalidadD, info.Idregbyprod.ToString(), referencia[1], referencia[0], info.Propileno, bimporta);

                //int rowsAffected = 0;
                //using (OleDbConnection connection = new OleDbConnection(accessConString(path)))
                //{
                //    using (OleDbCommand command = new OleDbCommand(queryString, connection))
                //    {

                //        // string queryString = "INSERT INTO INSPECCION (IDPRODUCTO,PRODUCTO,IDCENTRO,CENTRO,IDSERVICIO,servicio,BARCO,CANTIDAD,AÑO,MES,FECHA,NUM_ORDEN_SERVICIO,STATUS,OBSERVACIONES,CERT_CANTIDAD,CERT_CALIDAD,CERT_CALIDAD_A,CERT_CALIDAD_B,CERT_CALIDAD_C,CERT_CALIDAD_D,REGISTRO,FOLIO_CANTIDAD,FOLIO_CALIDAD,propileno,BARCO_IMP) " +
                //        //string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}-{7}',{8},{9},{10},'{11:dd/MM/yyyy}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',{24},'{25}')",
                //        //info.IdProducto, info.NombreProducto,   //0-1
                //        //info.IdInst, info.NombreCentro,       //2-3
                //        //info.IdServicio,info.NombreServicio,    //4-5
                //        //info.IdBarco, info.NombreBarco,
                //        //info.Cant_insp_mezcla, info.Fecha.Value.Year, info.Fecha.Value.Month, string.Format("{0:dd}/{1:MM}/{2:yyyy}", info.Fecha, info.Fecha,info.Fecha), info.Orden_servicio,
                //        //info.Estatus_pagado, info.Comentarios.ToString(), FileCantidad, FileCalidad, FileCalidadA, FileCalidadB, FileCalidadC, FileCalidadD, info.Idregbyprod.ToString(), referencia[1], referencia[0], info.Propileno, bimporta);
                //        command.Connection.Open();
                //        command.Parameters.AddWithValue("?", info.IdProducto);
                //        command.Parameters.AddWithValue("?", info.NombreProducto);
                //        command.Parameters.AddWithValue("?", info.IdInst);
                //        command.Parameters.AddWithValue("?", info.NombreCentro);
                //        command.Parameters.AddWithValue("?", info.NombreServicio);
                //        command.Parameters.AddWithValue("?", string.Format("{0}-{1}",info.IdBarco.ToString(),info.NombreBarco));
                //        //command.Parameters.AddWithValue("?", info.NombreBarco);
                //        command.Parameters.AddWithValue("?", info.Cant_insp_mezcla);
                //        command.Parameters.AddWithValue("?", info.Fecha.Value.Year);
                //        command.Parameters.AddWithValue("?", info.Fecha.Value.Month);
                //        command.Parameters.AddWithValue("?", string.Format("{0:dd/MM/yyyy}", info.Fecha).ToString());
                //        command.Parameters.AddWithValue("?", info.Orden_servicio);
                //        command.Parameters.AddWithValue("?", info.Estatus_pagado);
                //        command.Parameters.AddWithValue("?", FileCantidad);
                //        command.Parameters.AddWithValue("?", FileCalidad);
                //        command.Parameters.AddWithValue("?", FileCalidadA);
                //        command.Parameters.AddWithValue("?", FileCalidadB);
                //        command.Parameters.AddWithValue("?", FileCalidadC);
                //        command.Parameters.AddWithValue("?", FileCalidadD);
                //        command.Parameters.AddWithValue("?", info.Idregbyprod.ToString());
                //        command.Parameters.AddWithValue("?", referencia[1].ToString());
                //        command.Parameters.AddWithValue("?", referencia[0].ToString());
                //        command.Parameters.AddWithValue("?", info.Propileno);
                //        command.Parameters.AddWithValue("?", bimporta);
                //        //command.Parameters.AddWithValue("?", info.IdProducto);

                //        rowsAffected = command.ExecuteNonQuery();
                //    }
                //}
                contador += executeQuery(queryString, path);
            }
            return contador;
        }
        //public int executeQuery(string queryString,string path,List<capascccmex.metadatos.movproducto> iInfo)
        //{
        //    int rowsAffected = 0;
        //    using (OleDbConnection connection = new OleDbConnection(accessConString(path)))
        //    {
        //        using (OleDbCommand command = new OleDbCommand(queryString, connection))
        //        {

        //            // string queryString = "INSERT INTO INSPECCION (IDPRODUCTO,PRODUCTO,IDCENTRO,CENTRO,IDSERVICIO,servicio,BARCO,CANTIDAD,AÑO,MES,FECHA,NUM_ORDEN_SERVICIO,STATUS,OBSERVACIONES,CERT_CANTIDAD,CERT_CALIDAD,CERT_CALIDAD_A,CERT_CALIDAD_B,CERT_CALIDAD_C,CERT_CALIDAD_D,REGISTRO,FOLIO_CANTIDAD,FOLIO_CALIDAD,propileno,BARCO_IMP) " +
        //            //string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}-{7}',{8},{9},{10},'{11:dd/MM/yyyy}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',{24},'{25}')",
        //            //info.IdProducto, info.NombreProducto,   //0-1
        //            //info.IdInst, info.NombreCentro,       //2-3
        //            //info.IdServicio,info.NombreServicio,    //4-5
        //            //info.IdBarco, info.NombreBarco,
        //            //info.Cant_insp_mezcla, info.Fecha.Value.Year, info.Fecha.Value.Month, string.Format("{0:dd}/{1:MM}/{2:yyyy}", info.Fecha, info.Fecha,info.Fecha), info.Orden_servicio,
        //            //info.Estatus_pagado, info.Comentarios.ToString(), FileCantidad, FileCalidad, FileCalidadA, FileCalidadB, FileCalidadC, FileCalidadD, info.Idregbyprod.ToString(), referencia[1], referencia[0], info.Propileno, bimporta);
        //            command.Connection.Open();
        //            command.Parameters.AddWithValue("?", info.IdProducto);
        //            rowsAffected = command.ExecuteNonQuery();
        //        }
        //    }
        //    return rowsAffected;
        //}

        public int executeQuery(string queryString, string path)
        {
            int rowsAffected = 0;
            using (OleDbConnection connection = new OleDbConnection(accessConString(path)))
            {
                using (OleDbCommand command = new OleDbCommand(queryString, connection))
                {
                    command.Connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        string accessConString(string path)
        {
            String connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                   + @"Data Source=" + path + "Databasecccmex.accdb; Persist Security Info=False;";
            return connectionString;
        }

        #endregion

        #region OTRAS OPERACIONES

        protected void cmdreset_Click(object sender, EventArgs e)
        {
            RadComboBox cmbcat1 = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbservicio");
            RadComboBox cmbcat2 = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbproducto");
            RadComboBox cmbcat3 = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbcentro");

            RadButton rbt1 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtDefault");
            RadButton rbt2 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtRevisado");
            RadButton rbt3 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtTramite");
            RadButton rbt4 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtCancel");
            RadButton rbt5 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtaceptadopag");
            RadButton rbt6 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtarechazadopag");
            RadButton rbt9 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtnBarco");

            RadDatePicker rdp1 = (RadDatePicker)RadPanelBar1.FindItemByValue("info").FindControl("rdpFechaIni");
            RadDatePicker rdp2 = (RadDatePicker)RadPanelBar1.FindItemByValue("info").FindControl("rdpFechaFin");

            RadButton rbt7 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtAnio");
            RadButton rbt8 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtAniomes");

            cmbcat1.ClearSelection();
            cmbcat2.ClearSelection();
            cmbcat3.ClearSelection();

            rbt1.Checked = false;
            rbt2.Checked = false;
            rbt3.Checked = false;
            rbt4.Checked = false;
            rbt5.Checked = false;
            rbt6.Checked = false;
            rbt7.Checked = false;
            rbt8.Checked = false;
            rbt9.Checked = false;

            rdp1.Clear();
            rdp2.Clear();


        }

        protected void cmdejecuta_Click(object sender, EventArgs e)
        {
            try
            {

                RadComboBox cmbcat1 = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbservicio");
                RadComboBox cmbcat2 = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbproducto");
                RadComboBox cmbcat3 = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbcentro");

                RadButton rbt1 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtDefault");
                RadButton rbt2 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtRevisado");
                RadButton rbt3 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtTramite");
                RadButton rbt4 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtCancel");
                RadButton rbt5 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtaceptadopag");
                RadButton rbt6 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtarechazadopag");

                RadButton rbtimp = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtnBarco");

                RadDatePicker rdp1 = (RadDatePicker)RadPanelBar1.FindItemByValue("info").FindControl("rdpFechaIni");
                RadDatePicker rdp2 = (RadDatePicker)RadPanelBar1.FindItemByValue("info").FindControl("rdpFechaFin");

                RadButton rbt7 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtAnio");
                RadButton rbt8 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtAniomes");

                Int64? _centro = cmbcat3.SelectedValue.Length > 0 ? convertir.toNInt64(cmbcat3.SelectedValue) : null;
                Int64? _prod = cmbcat2.SelectedValue.Length > 0 ? convertir.toNInt64(cmbcat2.SelectedValue) : null;
                String _serv = cmbcat1.SelectedValue.Length > 0 ? cmbcat1.SelectedValue.ToString() : null;
                String _rpagado = null;
                String _rrevisado = null;

                if (rbt1.Checked == true)
                    _rrevisado = "N";
                else if (rbt2.Checked == true)
                    _rrevisado = "S";
                else if (rbt3.Checked == true)
                    _rrevisado = "T";
                else if (rbt4.Checked == true)
                    _rrevisado = "C";
                else
                    _rrevisado = null;

                if (rbt5.Checked == true)
                    _rpagado = "A";
                else if (rbt6.Checked == true)
                    _rpagado = "P";
                else
                    _rpagado = null;



                DateTime? f1 = rdp1.SelectedDate.HasValue == true ? convertir.toNDateTime(string.Format("{0:dd/MM/yyyy}", rdp1.SelectedDate.Value)) : null;
                DateTime? f2 = rdp2.SelectedDate.HasValue == true ? convertir.toNDateTime(string.Format("{0:dd/MM/yyyy}", rdp2.SelectedDate.Value)) : null;

                bool? bi = Convert.ToBoolean(rbtimp.Checked);
                if (bi == false) bi = null;

                Int16? anio = null;
                Int16? mes = null;
                if (f1.HasValue)
                {

                    anio = rbt7.Checked == true ? convertir.toNInt16(rdp1.SelectedDate.Value.Year) : null;

                    if (rbt8.Checked == true)
                    {
                        anio = convertir.toNInt16(rdp1.SelectedDate.Value.Year);
                        mes = convertir.toNInt16(rdp1.SelectedDate.Value.Month);
                    }

                }

                List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
                capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();

                oCamposCat = obj.GetBizProducto02(null, _rrevisado, _rpagado, _prod, _centro, _serv, null, anio, mes, f1, f2, bi, 0, 0);
                Session["getCamposCatMovimiento"] = oCamposCat;
                gridCapturas.DataSource = oCamposCat;
                gridCapturas.DataBind();
                gridCapturas.Rebind();
                //----------------------------------------

            }
            catch (Exception ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando info", null);
            }
        }

        protected void gridCapturas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Int64? _idreg;
            GridDataItem item2;
            //fileGrid
            if (e.CommandName == "pdfArchivos")
            {
                //Mostrar archivos ...
                //foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                //{
                //    if (item.Selected == true)
                //    {
                item2 = (GridDataItem)e.Item;
                _idreg = convertir.toNInt64(item2.GetDataKeyValue("IdReg"));
                loadarchivospdfs(_idreg);
                //    }
                //}
            }

            if (e.CommandName == "fileGrid")
            {
                exportarAccess();
            }
            if (e.CommandName == "filezipGrid")
            {
                exportarzip();
            }
            //GridDataItem item;

            if (e.CommandName == "RowDoubleClick")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        //item = (GridDataItem)e.Item;
                        _idreg = convertir.toNInt64(item.GetDataKeyValue("IdReg"));
                        Session["getIdRegGrid"] = _idreg;
                        string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        //windowManager1.RadAlert("Se realizo dobleclick: " + _idreg.ToString(), 300, 100, "Probando dobleclick", null);
                    }
                }
            }
        }

        protected void gridCapturas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
                oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];
                gridCapturas.DataSource = oCamposCat;
            }
            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            //RadButton cmbExec = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("cmdejecuta");
            //Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            if (e.Argument == "Rebind")
            {
                //gridCapturas.DataSource = new string[0];
                gridCapturas.DataSource = null;
                cmdejecuta_Click(null, null);
                //gridCapturas.MasterTableView.SortExpressions.Clear();
                //gridCapturas.MasterTableView.GroupByExpressions.Clear();
                //cargarMovimientos(_idCentro, null, null);
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridCapturas.DataSource = null;
                cmdejecuta_Click(null, null);
                //gridCapturas.MasterTableView.SortExpressions.Clear();
                //gridCapturas.MasterTableView.GroupByExpressions.Clear();
                //gridCapturas.MasterTableView.CurrentPageIndex = gridCapturas.MasterTableView.PageCount - 1;
                //cargarMovimientos(_idCentro, null, null);
            }
        }

        #endregion
    }
}