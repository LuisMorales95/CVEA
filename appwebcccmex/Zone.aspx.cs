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
    public partial class Zone : System.Web.UI.Page
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
            List<BEZona> tempList = new List<BEZona>();
            BLZona buisnessLogic = new BLZona();

            tempList = buisnessLogic.GetZonas();
            gridAlerta.DataSource = tempList;
        }

        protected void gridAlerta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Session["btn"] = null;
            Session["idZona"] = null;
            Int64? _idZona = 0;
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
                        _idZona = convertir.toNInt64(item.GetDataKeyValue("IdZona"));
                        Session["idZona"] = _idZona;
                        string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        return;
                    }
                }
                if (_idZona < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione alguna Zona para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
                    return;
                }
            }
            if (e.CommandName == "btnEliminar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idZona = convertir.toNInt64(item.GetDataKeyValue("IdZona"));
                        Session["tempIdZone"] = _idZona;
                        ManejadorRadWindow.RadConfirm("Seguro que deseas eliminar esta Zona ? ", "confirmCallBackFn", 400, 120, null, "Confirmaciòn");
                        return;
                    }
                }
                if (_idZona < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione alguna zona para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
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
                BLZona logicZona = new BLZona();
                Int64? resultado;
                resultado = logicZona.DeleteZona(convertir.toNInt64(Session["tempIdZone"]));

                if (resultado > 0 && resultado != null)
                {
                    ManejadorRadWindow.RadAlert("La Zona eliminada correctamente.", 350, 100, "ZONA - Informaciòn", "refreshGrid");
                }
                else
                {
                    ManejadorRadWindow.RadAlert("No se elimino ningun registro. !", 350, 100, "ZONA - Informaciòn", null);
                }
            }
        }
    }
}