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
    public partial class cccmex_acumulados : System.Web.UI.Page
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
            loadmeses();
            addanio.Text = DateTime.Now.Year.ToString();
            //RadGrid1.MasterTableView.Caption = "Title: ABC Name: XYZ";
        }

        protected void cmdejecuta_Click(object sender, EventArgs e)
        {
            bool prod = convertir.toBoolean(rbproducto.Checked);
            bool acum = convertir.toBoolean(rbacumulado.Checked);
            if (addanio.Text.Length > 0 && cmbmes.Text.Length > 0)
            {

                Int16? anio = convertir.toNInt16(addanio.Text);
                Int16? mes = convertir.toNInt16(cmbmes.SelectedValue);
                string mes_nombre = cmbmes.Text.ToString();
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
                        titulo = "MES DE FACTURACIÓN: " + mes_nombre + ", PROPILENO, ACUMULADO POR SERVICIO";
                        cargarMovimientosByServicio(anio, mes, 1, titulo);
                    }
                    else if (acum == false)//servicio
                    {
                        gridcentro.Visible = true;
                        titulo = "MES DE FACTURACIÓN: " + mes_nombre + ", PROPILENO, ACUMULADO POR CENTRO";
                        cargarMovimientosByCentro(anio, mes, 1, titulo);

                    }
                }
                else if (prod == false)//Turbosina
                {
                    if (acum == true) //servicio
                    {
                        gridServicio.Visible = true;
                        titulo = "MES DE FACTURACIÓN: " + mes_nombre + ", TURBOSINA, ACUMULADO POR SERVICIO";
                        cargarMovimientosByServicio(anio, mes, 2, titulo);
                    }
                    else if (acum == false)//centro
                    {
                        gridcentro.Visible = true;
                        titulo = "MES DE FACTURACIÓN: " + mes_nombre + ", TURBOSINA, ACUMULADO POR CENTRO";
                        cargarMovimientosByCentro(anio, mes, 2, titulo);
                    }
                }
            }
            else
                windowManager1.RadAlert("Favor de seleccionar el Año ó Mes", 300, 100, "Validando", null);

        }

        #region EVENTOS DE OPERACIONES POR CENTROS
        void cargarMovimientosByCentro(Int16? anio, Int16? mes, int producto,string title)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();
            //1:propileno, 2:turbosina
            string sproc = producto == 1 ? "dbo.proc_getmov_productoByCentroServiciop" : "dbo.proc_getmov_productoByCentroServiciot";
            try
            {
                oCamposCat = obj.GetBizAcumuladoCentro(sproc,anio,mes);
                var sum = oCamposCat.Select(c => c.Cant_insp_mezcla).Sum();
                gridcentro.MasterTableView.Caption = title + string.Format(", TOTAL: {0:##,##0.000}", sum);
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

        void loadmeses()
        {
            Dictionary<int, string> cmbCampos = new Dictionary<int, string>();
            cmbCampos.Add((int)1, (string)"ENERO");
            cmbCampos.Add((int)2, (string)"FEBRERO");

            cmbCampos.Add((int)3, (string)"MARZO");
            cmbCampos.Add((int)4, (string)"ABRIL");
            cmbCampos.Add((int)5, (string)"MAYO");
            cmbCampos.Add((int)6, (string)"JUNIO");
            cmbCampos.Add((int)7, (string)"JULIO");
            cmbCampos.Add((int)8, (string)"AGOSTO");
            cmbCampos.Add((int)9, (string)"SEPTIEMBRE");
            cmbCampos.Add((int)10, (string)"OCTUBRE");
            cmbCampos.Add((int)11, (string)"NOVIMEBRE");
            cmbCampos.Add((int)12, (string)"DICIEMBRE");

            cmbmes.DataSource = cmbCampos;
            cmbmes.DataTextField = "Value";
            cmbmes.DataValueField = "Key";
            cmbmes.DataBind();
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

        void cargarMovimientosByServicio(Int16? anio, Int16? mes, int producto, string title)
        {
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            capascccmex.biz.mov_producto obj = new capascccmex.biz.mov_producto();
            //1:propileno, 2:turbosina
            string sproc = producto == 1 ? "dbo.proc_getmov_productoByServicioP" : "dbo.proc_getmov_productoByServicioT";
            try
            {
                oCamposCat = obj.GetBizAcumuladoServicio(sproc, anio, mes);
                var sum = oCamposCat.Select(c => c.Cant_insp_mezcla).Sum();
                gridServicio.MasterTableView.Caption = title + string.Format(", TOTAL: {0:##,##0.000}", sum);
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