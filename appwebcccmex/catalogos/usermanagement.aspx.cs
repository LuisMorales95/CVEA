using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;
using System.Linq;

namespace appwebcccmex.catalogos
{
    public partial class usermanagement : System.Web.UI.Page
    {
        List<capascccmex.metadatos.usuarioweb> oCamposUsr = new List<capascccmex.metadatos.usuarioweb>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {

                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    //if (Session["getIdCentro"] == null)
                    //    Response.Redirect("~/Account/outSession.aspx");


                    loadUsuarios();
                    Session["getNameCentro"] = "PAJARITOS".ToString();
                    Session["getIdCentro"] = "1".ToString();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");
            }
        }


        #region METODOS


        private void loadUsuarios()
        {
            capascccmex.biz.usuarioweb obj = new capascccmex.biz.usuarioweb();
            //bool multiEmp = Convert.ToBoolean(Session["getAgenciaUsr"]);
            //lblEmpresa.Text = "Empresa:".ToString();
            try
            {

                oCamposUsr = obj.GetBizUsuarios(null, 0, 0);
                Session["getCamposUsuarios"] = oCamposUsr;


                gridUsuarios.MasterTableView.Caption = "LISTA DE USUARIOS".ToString();
                gridUsuarios.DataSource = oCamposUsr;
                gridUsuarios.DataBind();
                gridUsuarios.Rebind();
            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando Usuarios", null);

            }
        }

        private void loadUsuariosByCentro(Int64? _idCentro)
        {
            capascccmex.biz.usuarioweb obj = new capascccmex.biz.usuarioweb();
            try
            {


                //oCamposUsr = (List<capascccmex.metadatos.usuarioweb>)Session["getCamposUsuarios"];
                //var getUsr = from oUsr in oCamposUsr
                //             where oUsr.IdCentro == _idCentro
                //             select oUsr;

                oCamposUsr = obj.GetBizUsuarios(null, 0, 0);

                gridUsuarios.MasterTableView.Caption = "LISTA DE USUARIOS".ToString();
                gridUsuarios.DataSource = oCamposUsr.Where(a => a.IdCentro == _idCentro).ToList();
                gridUsuarios.DataBind();
                gridUsuarios.Rebind();
            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando Usuarios", null);

            }
        }
        #endregion

        #region OPERACIONES PANTALLA
        protected void cmbCentro_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                capascccmex.biz.centro obj = new capascccmex.biz.centro();
                List<capascccmex.metadatos.centro> listaCampos = new List<capascccmex.metadatos.centro>();

                listaCampos = obj.GetBizCentro(null, 0, 0);

                RadComboBox rcmb = (RadComboBox)sender;

                foreach (var _row in listaCampos)
                {

                    RadComboBoxItem item = new RadComboBoxItem();
                    //{ 
                    //Text=_row.Nombre,Value=_row.IdPerito.ToString()
                    //};
                    item.Text = _row.Centro;
                    item.Value = _row.IdCentro.ToString();
                    string _centro = _row.Centro;
                    string _idcentro = _row.IdCentro.ToString();
                    //string _rf = _row.RegimenFiscal;

                    item.Attributes.Add("Codigo", _idcentro);
                    item.Attributes.Add("Nombre", _centro);
                    //item.Attributes.Add("RegimenFiscal", _rf);
                    rcmb.Items.Add(item);
                    item.DataBind();
                }

            }
            catch (Exception ex)
            {

                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 400, 100, "Cargando Centros", null);
            }
        }

        protected void cmbCentro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbCentro.SelectedValue.Length > 0)
            {
                Int64? idCentro = convertir.toNInt64(cmbCentro.SelectedValue);
                Session["getIdCentro"] = idCentro;
                Session["getNameCentro"] = cmbCentro.Text.ToString();
                loadUsuariosByCentro(idCentro);
            }

        }

        protected void gridUsuarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                oCamposUsr = (List<capascccmex.metadatos.usuarioweb>)Session["getCamposUsuarios"];
                gridUsuarios.DataSource = oCamposUsr;
            }
            else
                Response.Redirect("~/Account/login.aspx");
        }

        protected void gridUsuarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //GridDataItem item;
            Int64? _idCampo = 0;
            Int64? _idCampo2 = 0;
            string nombre_centro = "";
            bool bit = false;

            if (e.CommandName == "addGrid")
            {
                Session["opUsuarios"] = 1;
                string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                bit = true;
            }

            else if (e.CommandName == "updGrid")
            {
                loadUsuarios();
                bit = true;
            }
            else if (e.CommandName == "delGrid")
            {
                foreach (GridDataItem item in gridUsuarios.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IappId"));
                        Session["getIdUsrGrid"] = _idCampo;
                        string script = "function f(){callConfirm(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        bit = true;
                        break;
                    }

                }
            }

            else if (e.CommandName == "editGrid")
            {

                foreach (GridDataItem item in gridUsuarios.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IappId").ToString());
                        _idCampo2 = convertir.toNInt64(item.GetDataKeyValue("IdCentro").ToString());
                        nombre_centro = item.GetDataKeyValue("Nombre_Centro").ToString();
                        Session["opUsuarios"] = 2;
                        Session["getIdUsuarioGrid"] = _idCampo.ToString();
                        Session["getNameCentro"] = nombre_centro.ToString();
                        Session["getIdCentro"] = _idCampo2.ToString();

                        string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }
                }
            }

            if (bit == false)
                windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
        }

        String eliminarCat()
        {
            String error = "F";
            String _idcampo = Session["getIdUsrGrid"].ToString().Trim().ToUpper();

            capascccmex.datos.usuarioweb obj = new capascccmex.datos.usuarioweb();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("viappid", _idcampo));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //Int64? _idCentro = convertir.toNInt64(Session["getIdCentro"].ToString());
            if (e.Argument == "Rebind")
            {
                gridUsuarios.MasterTableView.SortExpressions.Clear();
                gridUsuarios.MasterTableView.GroupByExpressions.Clear();
                loadUsuarios();
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridUsuarios.MasterTableView.SortExpressions.Clear();
                gridUsuarios.MasterTableView.GroupByExpressions.Clear();
                gridUsuarios.MasterTableView.CurrentPageIndex = gridUsuarios.MasterTableView.PageCount - 1;
                loadUsuarios();
            }

            if (e.Argument.ToString() == "oka")
            {
                string param = eliminarCat();
                string _error = Session["error_Reporte"].ToString();
                if (param.CompareTo("F") == 0 && _error.CompareTo("ok") == 1)
                {
                    windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Usuarios", null);
                }
                else
                {
                    //_idEmpresa = Convert.ToInt64(Session["getIdEmpresaGrid"]);
                    windowManager1.RadAlert("Usuario eliminado con éxito...", 450, 200, "Eliminando Usuarios", null);
                    gridUsuarios.MasterTableView.SortExpressions.Clear();
                    gridUsuarios.MasterTableView.GroupByExpressions.Clear();
                    loadUsuarios();
                }
            }
        }

        #endregion
    }
}