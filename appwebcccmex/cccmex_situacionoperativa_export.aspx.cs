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
    public partial class cccmex_situacionoperativa_export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    cargarMovimientos(null, null);
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region METODOS DE BDS

        void cargarMovimientos(Int64? _idCentro, DateTime? _fecha)
        {
            List<capascccmex.metadatos.operatividad> oCamposCat = new List<capascccmex.metadatos.operatividad>();
            capascccmex.biz.operatividad obj = new capascccmex.biz.operatividad();

            try
            {
                oCamposCat = obj.GetBizOperatividad(_idCentro, _fecha);
                Session["getCamposCatOperatividad2"] = oCamposCat;
                //gridCapturas.DataSource = oCamposCat.OrderByDescending(x => x.Fecha); ;
                //gridCapturas.DataBind();
                //gridCapturas.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando relación de Operatividad", null);
            }
        }

        void cargarMovimientosByDia()
        {
            List<capascccmex.metadatos.operatividad> oCamposCat = new List<capascccmex.metadatos.operatividad>();

            try
            {
                //values.DataTime >= fromDate && values.DataTime <= toDate
                DateTime? _fechaHoy = DateTime.Now;
                cambiarEncabezado(DateTime.Now);
                oCamposCat = (List<capascccmex.metadatos.operatividad>)Session["getCamposCatOperatividad2"];

                //var listaGenerica = from lc in oCamposCat
                //                    where lc.Fecha.Value.Year == _fechaHoy.Value.Year && lc.Fecha.Value.Day == _fechaHoy.Value.Day && lc.Fecha.Value.Month == _fechaHoy.Value.Month
                //                    select lc;

                gridCapturas.DataSource = oCamposCat.Where(x => string.Format("{0:dd/MM/yyyy}",x.Fecha.Value) == string.Format("{0:dd/MM/yyyy}", _fechaHoy));
                gridCapturas.DataBind();
                gridCapturas.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando relación de Operatividad", null);
            }
        }

        void cargarMovimientosByDiaChange()
        {
            List<capascccmex.metadatos.operatividad> oCamposCat = new List<capascccmex.metadatos.operatividad>();

            try
            {
                //values.DataTime >= fromDate && values.DataTime <= toDate
                DateTime? _fechaHoy = DateTime.Now;
                if (rdpFechaIni.SelectedDate.HasValue)
                    _fechaHoy = rdpFechaIni.SelectedDate.Value;
                cambiarEncabezado(rdpFechaIni.SelectedDate.Value);
                oCamposCat = (List<capascccmex.metadatos.operatividad>)Session["getCamposCatOperatividad2"];

                var listaGenerica = from lc in oCamposCat
                                    where lc.Fecha.Value.Year == _fechaHoy.Value.Year && lc.Fecha.Value.Day == _fechaHoy.Value.Day && lc.Fecha.Value.Month == _fechaHoy.Value.Month
                                    select lc;

                gridCapturas.DataSource = listaGenerica.OrderByDescending(x => x.Fecha);
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

      
        #endregion

        #region EVENTOS

        protected void gridCapturas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //string extension = "Xlsx";
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
            {

                //gridCapturas.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML; //(GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), extension);
                //gridCapturas.ExportSettings.Excel.FileExtension = "Xlsx";
                gridCapturas.ExportSettings.ExportOnlyData = true;
                gridCapturas.ExportSettings.IgnorePaging = true;
                gridCapturas.ExportSettings.OpenInNewWindow = true;
                //gridCapturas.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;

                //gridFacturas.MasterTableView.ExportToCSV();
                gridCapturas.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
        }
       
        protected void rbtHoy_Click(object sender, EventArgs e)
        {
            cargarMovimientosByDia();
        }

        protected void rdpFechaIni_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            cargarMovimientosByDiaChange();
            //cambiarEncabezado();
        }
        
        void cambiarEncabezado(DateTime _fecha)
        {
            DateTime fecha = _fecha;// rdpFechaIni.SelectedDate.Value;
            int diaAnterior = Convert.ToInt32(fecha.Day) - 1;
            if (Convert.ToInt32(fecha.Day) == 1)
                diaAnterior = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1);
            //int ultimoDia = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month-1);


            gridCapturas.MasterTableView.Caption = string.Format("SITUACION OPERATIVA POR CENTRO DE TRABAJO {0:dd - MMMM - yyyy}", fecha);

            foreach (GridColumn col in gridCapturas.Columns)
            {
                if (col.UniqueName == "Cantidad_dia_anterior")
                    col.HeaderText = string.Format("CANTIDAD INSPECCIONADA {0:00}/{1:MM/yyyy}", diaAnterior, fecha);
                else if (col.UniqueName == "Unidad_inspeccionada")
                    col.HeaderText = string.Format("UNIDADES INSPECCIONADAS DE 00:00 HRS A 08:30 ({0:dd/MM/yyyy})", fecha);
                else if (col.UniqueName == "Unidad_pendiente")
                    col.HeaderText = string.Format("UNIDADES PENDIENTES DE INSPECIONAR SIENDO LAS 8:30 ({0:dd/MM/yyyy})", fecha);
                else if (col.UniqueName == "Unidad_inspeccionada_hora")
                    col.HeaderText = string.Format("UNIDADES INSPECCIONADAS DE LAS 08:30 A LAS 16:00 HRS ({0:dd/MM/yyyy})", fecha);
                else if (col.UniqueName == "Unidad_pendiente_hora")
                    col.HeaderText = string.Format("UNIDADES PENDIENTES DE INSPECIONAR A LAS 16:00 HRS ({0:dd/MM/yyyy})", fecha);
                else if (col.UniqueName == "Cantidad_facturada")
                    col.HeaderText = string.Format("CANTIDAD FACTURADA EN EL DÍA  {0:00}/{1:MM/yyyy}", diaAnterior, fecha);                    
                else if (col.UniqueName == "Cantidad_facturada2")
                    col.HeaderText = string.Format("CANTIDAD FACTURADA  EL DÍA {0:dd/MM/yyyy} HASTA LAS 16:00 PM", fecha);
                else if (col.UniqueName == "Equipos_Rechazados")
                    col.HeaderText = string.Format("EQUIPOS RECHAZADOS  {0:00}/{1:MM/yyyy}", diaAnterior, fecha);
            }
            gridCapturas.Rebind();
        }

        #endregion

        protected void gridCapturas_ExportCellFormatting(object sender, ExportCellFormattingEventArgs e)
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

    }
}