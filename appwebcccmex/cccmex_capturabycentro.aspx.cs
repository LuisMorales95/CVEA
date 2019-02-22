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
using Ionic.Zip;
using System.Text;

namespace appwebcccmex
{
    public partial class cccmex_capturabycentro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    Session["opTipo"] = 1;
                    //Session["getIdCentroUsr"] = 1;
                    int _idCentro = convertir.toInt32(Session["getIdCentroUsr"]);
                    nameCentro.Text = Session["nameCentroActual"].ToString();
                    cargarcentrosByInstalaciones(_idCentro);
                    cargarMovimientos(_idCentro,null,null);
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region EVENTOS DE OPERACIONES
        void cargarcentrosByInstalaciones(int _idcentro)
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            //bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            //if (adm == true) _idcentro = null;

            try
            {
               

                Dictionary<int, string> dInst = new Dictionary<int, string>();
                oCamposCat = obj.GetBizInstalaciones(_idcentro, 0, 0);               
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dInst.Add(convertir.toInt32(item.IdInst), (string)item.Nombre);
                }

               cmbInstalacion.DataSource = dInst;
               cmbInstalacion.DataTextField = "Value";
               cmbInstalacion.DataValueField = "Key";
               cmbInstalacion.DataBind();

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando instalaciones", null);
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
            }
        }

        void cargarMovimientos(Int64? _idCentro,DateTime? _fecha,DateTime? _fecha2)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();

            try
            {
                oCamposCat = obj.GetBizProducto(null,null,null,null,_idCentro,null,null,null,null,_fecha,_fecha2, 0, 0);
                Session["getCamposCatMovimiento"] = oCamposCat;
                gridCapturas.DataSource = oCamposCat.OrderByDescending(x => x.Fecha); ;
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

        void cargarMovimientosByInst(Int64? _idInst)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();           

            try
            {
                //values.DataTime >= fromDate && values.DataTime <= toDate
                oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];

                var listaGenerica = from lc in oCamposCat
                                    where lc.IdInst == _idInst
                                    select lc;

                gridCapturas.DataSource = listaGenerica.OrderByDescending(x=>x.Fecha);
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

        void cargarMovimientosByDia()
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();

            try
            {
                //values.DataTime >= fromDate && values.DataTime <= toDate
                DateTime? _fechaHoy = DateTime.Now;
                oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];

                var listaGenerica = from lc in oCamposCat
                                    where lc.Fecha.Value.Year == _fechaHoy.Value.Year && lc.Fecha.Value.Day == _fechaHoy.Value.Day && lc.Fecha.Value.Month == _fechaHoy.Value.Month
                                    select lc;

                gridCapturas.DataSource = listaGenerica;
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

        void cargarMovimientosByDiaChange()
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();

            try
            {
                //values.DataTime >= fromDate && values.DataTime <= toDate
                DateTime? _fechaHoy = DateTime.Now;
                if (rdpFechaIni.SelectedDate.HasValue)
                    _fechaHoy = rdpFechaIni.SelectedDate.Value;
               
                oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];

                var listaGenerica = from lc in oCamposCat
                                    where lc.Fecha.Value.Year == _fechaHoy.Value.Year && lc.Fecha.Value.Day == _fechaHoy.Value.Day && lc.Fecha.Value.Month == _fechaHoy.Value.Month
                                    select lc;

                gridCapturas.DataSource = listaGenerica.OrderByDescending(x=>x.Fecha);
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

        void exportarzip()
        {
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("cccmex_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);

            string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\";

            Int64? centroGrid = 0;
            String referenciaGrid = "";

            foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
            {
                if (item.Selected == true)
                {
                    referenciaGrid = item.GetDataKeyValue("Referencia_folio").ToString();
                    centroGrid = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));
                }

            }


            string FileCalidad = "";
            string FileCalidadA = "";
            string FileCalidadB = "";
            string FileCalidadC = "";
            string FileCalidadD = "";
            string FileCantidad = "";
            string[] referencia = null;
            //try
            //{
                using (ZipFile zip = new ZipFile())
                {

                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //zzip.AddDirectoryByName("filesCccmex");

                    //foreach (var info in oCamposCat)
                    //{
                    referencia = referenciaGrid.ToString().Split('|');
                    FileCalidad = string.Format("{0}{1}/{2}.pdf", path, centroGrid.ToString(), referencia[0]);
                    FileCalidadA = string.Format("{0}{1}/{2}_A.pdf", path, centroGrid.ToString(), referencia[0]);
                    FileCalidadB = string.Format("{0}{1}/{2}_B.pdf", path, centroGrid.ToString(), referencia[0]);
                    FileCalidadC = string.Format("{0}{1}/{2}_C.pdf", path, centroGrid.ToString(), referencia[0]);
                    FileCalidadD = string.Format("{0}{1}/{2}_D.pdf", path, centroGrid.ToString(), referencia[0]);
                    FileCantidad = string.Format("{0}{1}/{2}.pdf", path, centroGrid.ToString(), referencia[1]);

                    if (File.Exists(FileCalidad))
                        zip.AddFile(FileCalidad, "filesCccmex");

                    if (File.Exists(FileCalidadA))
                        zip.AddFile(FileCalidadA,  "filesCccmex");

                    if (File.Exists(FileCalidadB))
                        zip.AddFile(FileCalidadB, "filesCccmex");

                    if (File.Exists(FileCalidadC))
                        zip.AddFile(FileCalidadC,  "filesCccmex");

                    if (File.Exists(FileCalidadD))
                        zip.AddFile(FileCalidadD, "filesCccmex");

                    if (File.Exists(FileCantidad))
                        zip.AddFile(FileCantidad,  "filesCccmex");


                    //}


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

            //}
            //catch (Exception ex)
            //{
            //    convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
            //    windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Exportando archivo Zip", null);
            //}
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
        #endregion

        #region OTRAS OPERACIONES
        protected void gridCapturas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Int64? _idCampo = null;
            //string _nombre = "";
            //string pathFileCal = "";
            GridDataItem item;
            //String pdfcal = "";
            //String pdfcant = "";
            //String idcentrofile = "";
            //int unicode = 65;
            //string caracter = "";
            //string[] referencia=null;
            bool bit = true;

            if (e.CommandName == "editGrid")
            {
                bit = false;
                foreach (GridDataItem item2 in gridCapturas.MasterTableView.Items)
                {
                    if (item2.Selected == true)
                    {
                        _idCampo = convertir.toNInt64( item2.GetDataKeyValue("IdReg"));
                        Session["getIdReg"] = _idCampo;
                        string script = "function f(){openRadWindow2(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                        
                        bit = true;
                    }
                }
                if (bit == false)
                    windowManager1.RadAlert("Debe seleccionar el registro a editar...", 300, 100, "Editando Archivo", null);
            }

            if (e.CommandName == "filezipGrid")
            {
                foreach (GridDataItem item2 in gridCapturas.MasterTableView.Items)
                {
                    if (item2.Selected == true)
                    {
                        exportarzip();
                        bit = true;
                    }
                }
                if (bit == false)
                    windowManager1.RadAlert("Debe seleccionar el registro a descargar...", 300, 100, "Bajando Zip", null);
            }
            if (e.CommandName == "pdfArchivos")
            {
                //Mostrar archivos ...
                //foreach (GridDataItem item2 in gridCapturas.MasterTableView.Items)
                //{
                //    if (item2.Selected == true)
                //    {
                        item = (GridDataItem)e.Item;
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IdReg"));
                        loadarchivospdfs(_idCampo);
                    //    bit = true;
                    //}
                //}               
            }
            if (bit == false)
                windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
           
            if (e.CommandName == "addGrid")
            {
                //item = (GridDataItem)e.Item;
                //_idCampo = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));
                //Session["getIdCentroGrid"] = _idCampo;
                string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                bit = true;
            }
            if (bit == false)
                windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
        }

        private void dounloadXml(string carpeta, string archivo)
        {
            string pathFile = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\" + carpeta.ToString() + "\\" + archivo.ToString();
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

        protected void gridCapturas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                
                List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
                oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];
               gridCapturas.DataSource = oCamposCat;
               //gridCapturas.DataBind();
            }
            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void cmbInstalacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbInstalacion.SelectedValue.Length > 0)
            {

                Int64? _idInst = convertir.toNInt64(cmbInstalacion.SelectedValue);
                cargarMovimientosByInst(_idInst);
            }
        }

        protected void rbtTodas_Click(object sender, EventArgs e)
        {
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            cargarMovimientos(_idCentro, null, null);
        }

        protected void rbtHoy_Click(object sender, EventArgs e)
        {
            cargarMovimientosByDia();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            if (e.Argument == "Rebind")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                cargarMovimientos(_idCentro, null, null);
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                gridCapturas.MasterTableView.CurrentPageIndex = gridCapturas.MasterTableView.PageCount - 1;
                cargarMovimientos(_idCentro, null, null);
            }

            //if (e.Argument.ToString() == "oka")
            //{
            //    string param = eliminarCat();
            //    string _error = Session["error_Reporte"].ToString();
            //    if (param.CompareTo("F") == 0 && _error.CompareTo("ok") == 1)
            //    {
            //        windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Usuarios", null);
            //    }
            //    else
            //    {
            //        //_idEmpresa = Convert.ToInt64(Session["getIdEmpresaGrid"]);
            //        windowManager1.RadAlert("Usuario eliminado con éxito...", 450, 200, "Eliminando Usuarios", null);
            //        gridUsuarios.MasterTableView.SortExpressions.Clear();
            //        gridUsuarios.MasterTableView.GroupByExpressions.Clear();
            //        loadUsuarios();
            //    }
            //}
        }

        protected void rdpFechaIni_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            cargarMovimientosByDiaChange();
        }
        #endregion

        
    }
}