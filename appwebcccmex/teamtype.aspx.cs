using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BEcccmex;
using BLcccmex;
using capascccmex;
using Telerik.Web.UI;


namespace appwebcccmex
{
    public partial class teamtype : System.Web.UI.Page
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
            List<BETipoEquipo> tempList = new List<BETipoEquipo>();
            BLTipoEquipo buisnessLogic = new BLTipoEquipo();

            tempList = buisnessLogic.GetTipoEquipo();
            gridAlerta.DataSource = tempList;
        }

        protected void gridAlerta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Session["btn"] = null;
            Session["idTipoEquipo"] = null;
            Int64? _idTipoEquipo = 0;
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
                        _idTipoEquipo = convertir.toNInt64(item.GetDataKeyValue("IdTipoEquipo"));
                        Session["idTipoEquipo"] = _idTipoEquipo;
                        string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        return;
                    }
                }
                if (_idTipoEquipo < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione alguna Tipo de Equipo para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
                    return;
                }
            }
            if (e.CommandName == "btnEliminar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idTipoEquipo = convertir.toNInt64(item.GetDataKeyValue("IdTipoEquipo"));
                        Session["tempIdTipoEquipo"] = _idTipoEquipo;
                        ManejadorRadWindow.RadConfirm("Seguro que deseas eliminar este Tipo? ", "confirmCallBackFn", 400, 120, null, "Confirmaciòn");
                        return;
                    }
                }
                if (_idTipoEquipo < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione algun Tipo para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
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
                BLTipoEquipo logicZona = new BLTipoEquipo();
                Int64? resultado;
                resultado = logicZona.deleteTipoEquipo(convertir.toNInt64(Session["tempIdTipoEquipo"]));

                if (resultado > 0 && resultado != null)
                {
                    ManejadorRadWindow.RadAlert("El Tipo de Equipo eliminada correctamente.", 350, 100, "ZONA - Informaciòn", "refreshGrid");
                }
                else
                {
                    ManejadorRadWindow.RadAlert("No se elimino ningun registro. !", 350, 100, "ZONA - Informaciòn", null);
                }
            }
        }
    }
}