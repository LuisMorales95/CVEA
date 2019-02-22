using capascccmex;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace appwebcccmex.catalogos
{
    public partial class cccmex_equipos : System.Web.UI.Page
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


        BLcccmex.BLEquipo obj;

        protected void CargarEquipos(int _idCentro, int _idInstalacion)
        {

            List<BEcccmex.BEEquipo> oCamposCat = new List<BEcccmex.BEEquipo>();
            obj = new BLcccmex.BLEquipo();
            
                oCamposCat = obj.GetEquipo(_idCentro, _idInstalacion);              
                gridAlerta.DataSource = oCamposCat;
                Session["tempCatEquipos"] = oCamposCat;
            
           

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
                int _idCentro = convertir.toInt16(cmbcentro.SelectedValue);
                int _idInstalacion = convertir.toInt16(cmbInstalacion.SelectedValue);
                CargarEquipos(_idCentro, _idInstalacion);
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

        BLcccmex.BLEquipo objEquipo = new BLcccmex.BLEquipo();
        

        protected void gridAlerta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Inicializamos el Equipo actual 
            Session["tempIdEquipo"] = null;
            //Inicializamos el tipo de operaciòn
            Session["tempOpEquipo"] = null;
            Int64? _idEquipo = 0;
            //Operaciòn para actualizar
            if (e.CommandName == "DobleClick" || e.CommandName == "btnActualizar")
            {
                foreach (GridDataItem item in gridAlerta.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idEquipo = convertir.toNInt64(item.GetDataKeyValue("IdEquipo"));

                        Session["tempIdEquipo"] = _idEquipo;
                        Session["tempOpEquipo"] = "Actualizar";
                        string script = "function f(){AbrirRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        return;
                    }
                    
                }
                if (_idEquipo < 1)
                {
                    ManejadorRadWindow.RadAlert("Por favor seleccione algun Equipo para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
                    return;
                }
            }
            //Operaciòn para agregar
            if (e.CommandName == "btnAgregar")
            {
                Int64? _centro = convertir.toNInt64(cmbcentro.SelectedValue);
                
                Int64? _instalacion = convertir.toNInt64(cmbInstalacion.SelectedValue);

                Session["getIdCentroEquipo"] = _centro;
                Session["getIdInstalacionEquipo"] = _instalacion;
                Session["tempOpEquipo"] = "Agregar";
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
                        _idEquipo = convertir.toNInt64(item.GetDataKeyValue("IdEquipo"));
                            Session["tempIdEquipo"] = _idEquipo;
                            ManejadorRadWindow.RadConfirm("Seguro que deseas eliminar este Equipo ? ", "confirmCallBackFn", 400, 120, null, "Confirmaciòn");
                            return;
                        }
                    }
                    if (_idEquipo < 1)
                    {
                        ManejadorRadWindow.RadAlert("Por favor seleccione algun Equipo para </br> proceder con la operaciòn !", 350, 100, "Equipos - Informaciòn", null);
                        return;
                    }

            }

        }

        protected void ManejadorRadAjax_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                gridAlerta.Rebind();
            }

            if(e.Argument == "Eliminar"){
                
                Int64? resultado;

                resultado = objEquipo.DelEquipo(convertir.toNInt64(Session["tempIdEquipo"]));
                
                if (resultado > 0 && resultado!=null)
                {
                    ManejadorRadWindow.RadAlert("El Equipo eliminado correctamente.", 350, 100, "Equipos - Informaciòn", "refreshGrid");
                }
                else
                {
                    ManejadorRadWindow.RadAlert("No se elimino ningun registro. !", 350, 100, "Equipos - Informaciòn", null);
                }
            }
           
        }

    }
}