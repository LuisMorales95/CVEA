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
    public partial class cccmex_situacionoperativa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    
                   
                    Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
                    nameCentro.Text = Session["nameCentroActual"].ToString();                    
                    cargarMovimientos(_idCentro, null);
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
                Session["getCamposCatOperatividad"] = oCamposCat;
                gridCapturas.DataSource = oCamposCat.OrderByDescending(x => x.Fecha); 
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

        void cargarMovimientosByDia()
        {
            List<capascccmex.metadatos.operatividad> oCamposCat = new List<capascccmex.metadatos.operatividad>();

            try
            {
                //values.DataTime >= fromDate && values.DataTime <= toDate
                DateTime? _fechaHoy = DateTime.Now;

                oCamposCat = (List<capascccmex.metadatos.operatividad>)Session["getCamposCatOperatividad"];

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
                gridCapturas.AllowPaging = false;

                oCamposCat = (List<capascccmex.metadatos.operatividad>)Session["getCamposCatOperatividad"];

                //var listaGenerica = from lc in oCamposCat
                //                    where lc.Fecha.Value.Year == _fechaHoy.Value.Year && lc.Fecha.Value.Day == _fechaHoy.Value.Day && lc.Fecha.Value.Month == _fechaHoy.Value.Month
                //                    select lc;

                gridCapturas.DataSource = oCamposCat.Where(x => string.Format("{0:dd/MM/yyyy}", x.Fecha.Value) == string.Format("{0:dd/MM/yyyy}", _fechaHoy)).OrderByDescending(y=>y.IdOperatividad); //listaGenerica.OrderByDescending(x => x.Fecha);
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

        String eliminarCat()
        {
            String error = "F";
            Int64? _idcampo = convertir.toNInt64(Session["getIdOperaGrid"]);

            capascccmex.datos.operatividad obj = new capascccmex.datos.operatividad();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("@vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@idoperatividad", _idcampo));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }
        #endregion

        #region EVENTOS
        protected void gridCapturas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {

                List<capascccmex.metadatos.operatividad> oCamposCat = new List<capascccmex.metadatos.operatividad>();
                oCamposCat = (List<capascccmex.metadatos.operatividad>)Session["getCamposCatOperatividad"];
                gridCapturas.DataSource = oCamposCat;
                //gridCapturas.DataBind();
            }
            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void gridCapturas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Int64? _idCampo = 0;
            bool bit = false;
            if (e.CommandName == "addGrid")
            {
                Session["opOpera"] = 1;
                string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);               
            }
            else if (e.CommandName == "deleteGrid")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IdOperatividad"));
                        Session["getIdOperaGrid"] = _idCampo;
                        string script = "function f(){callConfirm(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        bit = true;
                        break;
                    }

                }
                if (bit == false)
                    windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
            }
            else if (e.CommandName == "editGrid")
            {

                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IdOperatividad").ToString());
                       
                        Session["opOpera"] = 2;
                        Session["getIdOperaGrid"] = _idCampo.ToString();                    

                        string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }
                }
                if (bit == false)
                    windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
            }
        }

        protected void rbtTodas_Click(object sender, EventArgs e)
        {
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            cargarMovimientos(_idCentro, null);
        }

        protected void rbtHoy_Click(object sender, EventArgs e)
        {
            cargarMovimientosByDia();
        }

        protected void rdpFechaIni_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            cargarMovimientosByDiaChange();
            cambiarEncabezado();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            if (e.Argument == "Rebind")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                cargarMovimientos(_idCentro, null);
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                gridCapturas.MasterTableView.CurrentPageIndex = gridCapturas.MasterTableView.PageCount - 1;
                cargarMovimientos(_idCentro, null);
            }

            if (e.Argument.ToString() == "oka")
            {
                string param = eliminarCat();
                string _error = Session["error_Reporte"].ToString();
                if (param.CompareTo("F") == 0 && _error.CompareTo("ok") == 1)
                {
                    windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Captura", null);
                }
                else
                {
                    //_idEmpresa = Convert.ToInt64(Session["getIdEmpresaGrid"]);
                    windowManager1.RadAlert("Registro eliminado con éxito...", 450, 200, "Eliminando Captura", null);
                    gridCapturas.MasterTableView.SortExpressions.Clear();
                    gridCapturas.MasterTableView.GroupByExpressions.Clear();
                    cargarMovimientos(_idCentro, null);
                }
            }
        }

        void cambiarEncabezado()
        {
            DateTime fecha=rdpFechaIni.SelectedDate.Value;
            int diaAnterior=Convert.ToInt32( fecha.Day)-1;
            foreach (GridColumn col in gridCapturas.Columns)
            {
                if (col.UniqueName == "Cantidad_dia_anterior")
                    col.HeaderText = string.Format("CANTIDAD INSPECCIONADA {0:00}/{1:MM/yyyy}",diaAnterior,fecha);
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

       
    }
}