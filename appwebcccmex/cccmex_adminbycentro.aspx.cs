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

namespace appwebcccmex
{
    public partial class cccmex_adminbycentro : System.Web.UI.Page
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
                oCamposCat = obj.GetBizProducto02(null, null, null, null, _idCentro, null, null, null, null, _fecha, _fecha2,false, 0, 0);
                Session["getCmpCatMovimientoadmin"] = oCamposCat;
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

        //void exportarAccess()
        //{
        //    string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + "\\";
        //    List<capascccmex.metadatos.movproducto> oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];
        //    int del = deleteInspeccionInfo(path);
        //    int ins = insertEmployeeInfo(oCamposCat, path);
        //    if (ins > 0)
        //    {
        //        dounloadXml("Databasecccmex.accdb");
        //    }


        //}

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

        void exportarzip()
        {
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("cccmex_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);

            string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\";

            Int64? centroGrid = 0;
            String  referenciaGrid = "";

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
            try
            {
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
                            zip.AddFile(FileCalidadA, "filesCccmex");

                        if (File.Exists(FileCalidadB))
                            zip.AddFile(FileCalidadB, "filesCccmex");

                        if (File.Exists(FileCalidadC))
                            zip.AddFile(FileCalidadC, "filesCccmex");

                        if (File.Exists(FileCalidadD))
                            zip.AddFile(FileCalidadD, "filesCccmex");

                        if (File.Exists(FileCantidad))
                            zip.AddFile(FileCantidad, "filesCccmex");


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

            }
            catch (Exception ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Exportando archivo Zip", null);
            }
        }

        void ejecutaProceso()
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

                oCamposCat = obj.GetBizProducto02(null, _rrevisado, _rpagado, _prod, _centro, _serv, null, anio, mes, f1, f2,bi, 0, 0);
                Session["getCmpCatMovimientoadmin"] = oCamposCat;
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

        #endregion
              

        #region OTRAS OPERACIONES
        protected void gridCapturas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            bool bit = false;
            Int64? _idreg;
            Int64? _idcentrobygrig;

            if (e.CommandName == "updateGrid")
            {
                ejecutaProceso();
            }

            if (e.CommandName == "filezipGrid")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        exportarzip();
                        bit = true;
                    }
                }
                 if (bit==false)
                windowManager1.RadAlert("Debe seleccionar el registro a descargar..." , 300, 100, "Bajando Zip", null);
            }

            if (e.CommandName == "RowDoubleClick")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        //item = (GridDataItem)e.Item;
                        _idreg = convertir.toNInt64(item.GetDataKeyValue("IdReg"));
                        _idcentrobygrig = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));

                        Session["getIdRegGrid"] = _idreg;
                        Session["idcentrobygrid"] = _idcentrobygrig;
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
                oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCmpCatMovimientoadmin"];
                gridCapturas.DataSource = oCamposCat;
            }
            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            //Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            if (e.Argument == "Rebind")
            {
                gridCapturas.DataSource = new string[0];
                ejecutaProceso();
                //gridCapturas.MasterTableView.SortExpressions.Clear();
                //gridCapturas.MasterTableView.GroupByExpressions.Clear();
                //cargarMovimientos(_idCentro, null, null);
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridCapturas.DataSource = new string[0];
                ejecutaProceso();
                //gridCapturas.MasterTableView.SortExpressions.Clear();
                //gridCapturas.MasterTableView.GroupByExpressions.Clear();
                //gridCapturas.MasterTableView.CurrentPageIndex = gridCapturas.MasterTableView.PageCount - 1;
                //cargarMovimientos(_idCentro, null, null);
            }
        }

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

            RadDatePicker rdp1 = (RadDatePicker)RadPanelBar1.FindItemByValue("info").FindControl("rdpFechaIni");
            RadDatePicker rdp2 = (RadDatePicker)RadPanelBar1.FindItemByValue("info").FindControl("rdpFechaFin");

            RadButton rbt7 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtAnio");
            RadButton rbt8 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtAniomes");
            RadButton rbt9 = (RadButton)RadPanelBar1.FindItemByValue("info").FindControl("rbtnBarco");

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
            ejecutaProceso();
        }

        #endregion

      
    }
}