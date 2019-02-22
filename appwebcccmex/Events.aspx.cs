using capascccmex;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace appwebcccmex
{
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    //Session["getIdCentroUsr"] = 1;
                    int _idCentro = convertir.toInt32(Session["getIdCentroUsr"]);
                    gridAlerta.ClientSettings.Scrolling.AllowScroll = true;

                    Cargarcentros(_idCentro);

                }
                else
                {
                    Response.Redirect("~/Account/outSession.aspx");
                }

            }
        }

        protected void Cargarcentros(int idCentro)
        {
            List<capascccmex.metadatos.centro> oCamposCat = new List<capascccmex.metadatos.centro>();
            capascccmex.biz.centro obj = new capascccmex.biz.centro();
            Dictionary<Int64?, string> dcat = new Dictionary<Int64?, string>();

            bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            bool pemex = Convert.ToBoolean(Session["prmPemex"]);
            if (adm == true || pemex == true)
                oCamposCat = obj.GetBizCentro(null, 0, 0);
            else
                oCamposCat = obj.GetBizCentro(idCentro, 0, 0);
            Session["tempCatCentros"] = oCamposCat;
            //----------------------------------------
            foreach (var item in oCamposCat)
            {
                dcat.Add(convertir.toInt32(item.IdCentro), (string)item.Centro);
            }

            cmbcentro.DataSource = dcat;
            cmbcentro.DataTextField = "Value";
            cmbcentro.DataValueField = "Key";
            cmbcentro.DataBind();
        }

        protected void CargarInstalacionesbyCentro(int _idcentro)
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            bool adm = Convert.ToBoolean(Session["prmAdmin"]);

            Dictionary<int, string> dInst = new Dictionary<int, string>();
            oCamposCat = obj.GetInstalacionDiagrama(_idcentro);


            Session["getCmpCatInstalaciones"] = oCamposCat;

            foreach (var item in oCamposCat)
            {
                dInst.Add(convertir.toInt16(item.IdInst), (string)item.Nombre);
            }

            cmbInstalacion.Text = "";
            cmbInstalacion.DataSource = dInst;
            cmbInstalacion.DataTextField = "Value";
            cmbInstalacion.DataValueField = "Key";
            cmbInstalacion.DataBind();
        }


        BLcccmex.BLEventoEquipo obj;

        protected void CargarEventos(Int64? _idCentro, Int64? _idInstalacion)
        {

            List<BEcccmex.BEEventoEquipo> oCamposCat = new List<BEcccmex.BEEventoEquipo>();
            obj = new BLcccmex.BLEventoEquipo();
            try
            {

                oCamposCat = obj.GetEvento(_idCentro, _idInstalacion);
                gridAlerta.DataSource = oCamposCat;
                Session["tempCatEventos"] = oCamposCat;
            }
            catch
            {
                //Implementación del codigo a ejecutarse en caso de alguna excepcion
                ManejadorRadWindow.RadAlert("Error: ", 300, 100, "Error al cargar", null);
            }

        }

        protected void cmbcentro_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbcentro.SelectedValue.Length > 0)
            {
                cmbInstalacion.SelectedValue = null;
                cmbInstalacion.Text = null;
            }

            gridAlerta.Rebind();
        }

        protected void cmbInstalacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            gridAlerta.Rebind();
        }


        protected void gridAlerta_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Int64? _idCentro = convertir.toNInt64(cmbcentro.SelectedValue);
                Int64? _idInstalacion = convertir.toNInt64(cmbInstalacion.SelectedValue);
                CargarEventos(_idCentro, _idInstalacion);
            }
            else
            {
                Response.Redirect("~/Account/outSession.aspx");
            }
        }


        protected void cmbInstalacion_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (cmbcentro.SelectedValue.Length > 0)
            {
                int _idCentro = convertir.toInt32(cmbcentro.SelectedValue);
                CargarInstalacionesbyCentro(_idCentro);
            }
        }

        BLcccmex.BLEvento objEvento = new BLcccmex.BLEvento();
        Int64? idEvento = 0;

        protected void gridAlerta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Inicializamos el evento actual 
            Session["tempIdEvento"] = null;
            //Inicializamos el tipo de operaciòn
            Session["tempOpEvento"] = null;
            Int64? _idEvento = 0;
            //Operaciòn para actualizar
            if (e.CommandName == "DobleClick" || e.CommandName == "btnActualizar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idEvento = convertir.toNInt64(item.GetDataKeyValue("IdEvento"));

                        Session["tempIdEvento"] = _idEvento;
                        Session["tempOpEvento"] = "Actualizar";
                        string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        return;
                    }

                }
                if (_idEvento < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione algun evento para </br> proceder con la operaciòn !", 350, 100, "Eventos - Informaciòn", null);
                    return;
                }
            }
            //Operaciòn para agregar
            if (e.CommandName == "btnAgregar")
            {
                Int64? _centro = convertir.toNInt64(cmbcentro.SelectedValue);

                Int64? _instalacion = convertir.toNInt64(cmbInstalacion.SelectedValue);

                Session["getIdCentroEvento"] = _centro;
                Session["getIdInstalacionEvento"] = _instalacion;
                Session["tempOpEvento"] = "Agregar";
                string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                return;
            }
            //Operaciòn para eliminar
            if (e.CommandName == "btnEliminar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        idEvento = convertir.toNInt64(item.GetDataKeyValue("IdEvento"));

                        Session["tempIdEvento"] = idEvento;
                        ManejadorRadWindow.RadConfirm("Seguro que deseas eliminar este evento ? ", "confirmCallBackFn", 400, 120, null, "Confirmaciòn");
                        return;
                    }
                }
                if (_idEvento < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione algun evento para </br> proceder con la operaciòn !", 350, 100, "Eventos - Informaciòn", null);
                    return;
                }

            }
            //Operaciòn para agregar
            if (e.CommandName == "btnArchivar")
            {
                int resultado = 0;
                Int64? _centro = convertir.toNInt64(cmbcentro.SelectedValue);

                Int64? _instalacion = convertir.toNInt64(cmbInstalacion.SelectedValue);
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        int idEvento = convertir.toInt32(item.GetDataKeyValue("IdEvento"));
                        int idEquipo = convertir.toInt32(item.GetDataKeyValue("IdEquipo"));
                        string fechaevento = item["FechaEvento"].ToString();
                        BLcccmex.BLEvento objbl = new BLcccmex.BLEvento();
                        resultado = objbl.AddEventoHistorico(idEvento, idEquipo, fechaevento, 1);

                        //string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        return;
                    }
                }
            }

        }

        protected void ManejadorRadAjax_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                gridAlerta.Rebind();
            }

            if (e.Argument == "Eliminar")
            {

                Int64? resultado;

                resultado = objEvento.DelEvento(convertir.toNInt64(Session["tempIdEvento"]));

                if (resultado > 0 && resultado != null)
                {
                    ManejadorRadWindow.RadAlert("El evento eliminado correctamente.", 350, 100, "Eventos - Informaciòn", "refreshGrid");
                }
                else
                {
                    ManejadorRadWindow.RadAlert("No se elimino ningun registro. !", 350, 100, "Eventos - Informaciòn", null);
                }
            }

        }
    }
}