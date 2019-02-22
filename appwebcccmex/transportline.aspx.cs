using System;
using System.Collections.Generic;
using System.Web.UI;
using BLcccmex;
using Telerik.Web.UI;
using capascccmex;
using BEcccmex;

namespace appwebcccmex
{
    public partial class transportline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    gridAlerta.ClientSettings.Scrolling.AllowScroll = true;
                    LoadZonas();
                }
                else
                {
                    Response.Redirect("~/Account/outSession.aspx");
                }
            }
        }

        private void LoadZonas()
        {
            List<BELineaTransporte> tempList = new List<BELineaTransporte>();
            BLLineaTransporte buisnessLogic = new BLLineaTransporte();

            tempList = buisnessLogic.GetLineaTransportes();
            gridAlerta.DataSource = tempList;
        }

        protected void gridAlerta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Session["btn"] = null;
            Session["idLineaTransporte"] = null;
            Int64? _idLineaTransporte = 0;
            if (e.CommandName == "btnAgregar")
            {
                Session["btn"] = "Save";
                string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                return;
            }
            if (e.CommandName == "DobleClick" || e.CommandName == "btnActualizar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        Session["btn"] = "Update";
                        _idLineaTransporte = convertir.toNInt64(item.GetDataKeyValue("IdLineaTransporte"));
                        Session["idLineaTransporte"] = _idLineaTransporte;
                        string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        return;
                    }
                }
                if (_idLineaTransporte < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione alguna Linea de Transporte para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
                    return;
                }
            }
            if (e.CommandName == "btnEliminar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idLineaTransporte = convertir.toNInt64(item.GetDataKeyValue("IdLineaTransporte"));
                        Session["tempIdLineaTransporte"] = _idLineaTransporte;
                        ManejadorRadWindow.RadConfirm("Seguro que deseas eliminar esta Linea de Transporte? ", "confirmCallBackFn", 400, 120, null, "Confirmaciòn");
                        return;
                    }
                }
                if (_idLineaTransporte < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione algun Linea de Transporte para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
                    return;
                }

            }
        }
        protected void gridAlerta_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                LoadZonas();
            }
            else
            {
                Response.Redirect("~/Account/outSession.aspx");
            }
        }

        protected void ManejadorRadAjax_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                gridAlerta.Rebind();
            }

            if (e.Argument == "Eliminar")
            {
                BLLineaTransporte logicLineaTransporte = new BLLineaTransporte();
                Int64? resultado;
                resultado = logicLineaTransporte.deleteLineaTransporte(convertir.toNInt64(Session["tempIdLineaTransporte"]));

                if (resultado > 0 && resultado != null)
                {
                    ManejadorRadWindow.RadAlert("La Linea de Transporte eliminada correctamente.", 350, 100, "ZONA - Informaciòn", "refreshGrid");
                }
                else
                {
                    ManejadorRadWindow.RadAlert("No se elimino ningun registro. !", 350, 100, "Linea de Transporte - Informaciòn", null);
                }
            }
        }
    }
}