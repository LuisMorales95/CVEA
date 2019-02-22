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
    public partial class accumulatedbyorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    inicializa();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        void inicializa()
        {
            gridcentro.Visible = false;
            gridServicio.Visible = false;
            loadOrdenServicios();
            //RadGrid1.MasterTableView.Caption = "Title: ABC Name: XYZ";
        }

        protected void cmdejecuta_Click(object sender, EventArgs e)
        {
            bool prod = convertir.toBoolean(rbproducto.Checked);
            bool acum = convertir.toBoolean(rbacumulado.Checked);
            if (cmbordenserv.Text.Length > 0)
            {

                String ordenServicio = cmbordenserv.Text.ToString();
                string titulo = "";

                gridServicio.DataSource = null;
                gridcentro.DataSource = null;
                gridcentro.Visible = false;
                gridServicio.Visible = false;
                //RadGrid1.MasterTableView.Caption = "Title: ABC Name: XYZ";

                if (prod == true) //propileno
                {
                    if (acum == true) //centro
                    {
                        gridServicio.Visible = true;
                        titulo = "ORDEN: " + ordenServicio + " PROPILENO, ACUMULADO POR SERVICIO";
                        cargarMovimientosByServicio(ordenServicio, 1, titulo);
                    }
                    else if (acum == false)//servicio
                    {
                        gridcentro.Visible = true;
                        titulo = "ORDEN: " + ordenServicio + " PROPILENO, ACUMULADO POR CENTRO";
                        cargarMovimientosByCentro(ordenServicio, 1, titulo);

                    }
                }
                else if (prod == false)//Turbosina
                {
                    if (acum == true) //servicio
                    {
                        gridServicio.Visible = true;
                        titulo = "ORDEN: " + ordenServicio + " TURBOSINA, ACUMULADO POR SERVICIO";
                        cargarMovimientosByServicio(ordenServicio, 2, titulo);
                    }
                    else if (acum == false)//centro
                    {
                        gridcentro.Visible = true;
                        titulo = "ORDEN: " + ordenServicio + " TURBOSINA, ACUMULADO POR CENTRO";
                        cargarMovimientosByCentro(ordenServicio, 2, titulo);
                    }
                }
            }
            else
                windowManager1.RadAlert("Favor de seleccionar la Orden de Servicio", 300, 100, "Validando", null);

        }

        #region EVENTOS DE OPERACIONES POR CENTROS

        Decimal regresaVolumenOrden(String ordenServicio)
        {
            Decimal volumen = 0;
            List<capascccmex.metadatos.orden_servicio> oCamposCat = new List<capascccmex.metadatos.orden_servicio>();
            oCamposCat = (List<capascccmex.metadatos.orden_servicio>)Session["getCamposCatOrdenServicioRep"];
            var getReg = from oReg in oCamposCat
                         where oReg.Orden_servicio == ordenServicio
                         select oReg;

            foreach (var ioc in getReg)
            {
                volumen = ioc.Volumen;
            }

            return volumen;
        }
        void cargarMovimientosByCentro(String ordenServicio, int producto, string title)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();
            //1:propileno, 2:turbosina
            string sproc = producto == 1 ? "dbo.proc_getOrdenServicioByCentroServiciop" : "dbo.proc_getOrdenServicioByCentroServiciot";
            try
            {
                oCamposCat = obj.GetBizAcumuladoCentroOS(sproc, ordenServicio);
                var sum = oCamposCat.Select(c => c.Cant_insp_mezcla).Sum();
                gridcentro.MasterTableView.Caption = title + string.Format(", RESTANTE: {0:##,##0.000} - {1:##,##0.000} = {2:##,##0.000}", regresaVolumenOrden(ordenServicio), sum, (regresaVolumenOrden(ordenServicio) - sum));
                gridcentro.DataSource = oCamposCat;
                gridcentro.DataBind();
                gridcentro.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error acum centro: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando acumulado por centro", null);
            }
        }

        void loadOrdenServicios()
        {
            List<capascccmex.metadatos.orden_servicio> oCamposCat = new List<capascccmex.metadatos.orden_servicio>();
            capascccmex.biz.orden_servicio obj = new capascccmex.biz.orden_servicio();
            Dictionary<String, String> dcat = new Dictionary<String, String>();

            try
            {
                oCamposCat = obj.GetBizOrdenServicio();
                Session["getCamposCatOrdenServicioRep"] = oCamposCat;
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dcat.Add((string)item.Orden_servicio, (string)item.Orden_servicio);
                }

                cmbordenserv.DataSource = dcat;
                cmbordenserv.DataTextField = "Value";
                cmbordenserv.DataValueField = "Key";
                cmbordenserv.DataBind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando orden servicio", null);
            }
        }

        protected void gridcentro_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
            {

                gridcentro.ExportSettings.ExportOnlyData = true;
                gridcentro.ExportSettings.IgnorePaging = true;
                gridcentro.ExportSettings.OpenInNewWindow = true;

                //gridFacturas.MasterTableView.ExportToCSV();
                gridcentro.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
        }

        #endregion

        #region EVENTOS DE OPERACIONES POR SERVICIO

        void cargarMovimientosByServicio(String ordenServicio, int producto, string title)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();
            //1:propileno, 2:turbosina
            string sproc = producto == 1 ? "dbo.proc_getOrdenServicioByServicioP" : "dbo.proc_getOrdenServicioByServicioT";
            try
            {
                oCamposCat = obj.GetBizAcumuladoServicioOS(sproc, ordenServicio);
                var sum = oCamposCat.Select(c => c.Cant_insp_mezcla).Sum();
                gridServicio.MasterTableView.Caption = title + string.Format(", RESTANTE: {0:##,##0.000} - {1:##,##0.000} = {2:##,##0.000}", regresaVolumenOrden(ordenServicio), sum, (regresaVolumenOrden(ordenServicio) - sum));
                gridServicio.DataSource = oCamposCat;
                gridServicio.DataBind();
                gridServicio.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error acum serv: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando acumulado por servicio", null);
            }
        }

        protected void gridServicio_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
            {

                gridServicio.ExportSettings.ExportOnlyData = true;
                gridServicio.ExportSettings.IgnorePaging = true;
                gridServicio.ExportSettings.OpenInNewWindow = true;

                //gridFacturas.MasterTableView.ExportToCSV();
                gridServicio.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
        }

        protected void gridcentro_ExportCellFormatting(object sender, ExportCellFormattingEventArgs e)
        {
            GridDataItem item = e.Cell.Parent as GridDataItem;
            if (item.ItemType == GridItemType.AlternatingItem)
            {
                item.Style["background-color"] = "#1A79A7";
            }
            else
                item.Style["background-color"] = "#EEEEEE";

            item.Style["border-style"] = "solid";
            item.Style["border-color"] = "#808080";
        }
        #endregion


    }
}