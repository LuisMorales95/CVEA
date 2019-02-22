using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;

namespace appwebcccmex.catalogos
{
    public partial class cccmex_servicios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    Session["opTipo"] = 1;
                    cargarCatalogo();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region EVENTOS DE PANTALLAS

        protected void cmdEjecuta_Click(object sender, EventArgs e)
        {
            valResumen.ValidationGroup = "get";
            Page.Validate("get");
            if (Page.IsValid)
            {
                try
                {

                    Int16 tipoOp = Convert.ToInt16(Session["opTipo"]);
                    if (tipoOp == 1)//alta
                    {
                        String param = AgregarCat();
                        if (param.CompareTo("F") == 0)
                        {
                            windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Agregando Servicio", null);
                        }
                        else
                        {
                            windowManager1.RadAlert("Servicio ingresado con éxito...", 300, 200, "Agregando Servicio", null);
                        }
                    }
                    else if (tipoOp == 2)//actualizar
                    {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando Servicio", null);
                        }
                        else
                        {
                            windowManager1.RadAlert("Servicio actualizado con éxito...", 300, 200, "actualizando Servicio", null);
                        }
                    }
                    limpiarRegistros();
                   addidserv.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                  addserv.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                }
                catch (SqlException ex)
                {
                    windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Servicio", null);

                }
            }
            else
            {

                var content = Form.FindControl("MainContent") as ContentPlaceHolder;
                foreach (BaseValidator validator in Validators)
                {
                    var controlToValidate = content.FindControl(validator.ControlToValidate) as WebControl;
                    controlToValidate.CssClass = null;

                    if (validator.IsValid)
                    {
                        controlToValidate.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                        continue;
                    }
                    else
                    {
                        controlToValidate.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                    }
                }
            }
        }

        protected void gridcccmex_ItemCommand(object sender, GridCommandEventArgs e)
        {
            String _idCampo = null;
            string _nombre = "";
            bool bit = false;

            if (e.CommandName == "updGrid")
            {
                cargarCatalogo();
                limpiarRegistros();
                bit = true;
            }
            else if (e.CommandName == "editGrid")
            {
                foreach (GridDataItem item in gridcccmex.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = item.GetDataKeyValue("IdServicio").ToString();
                        _nombre = item.GetDataKeyValue("Servicio").ToString();
                      addserv.Text = _nombre.ToString();
                      addidserv.Text = _idCampo.ToString();
                      addidserv.ReadOnly = true;

                        cmdEjecuta.Text = "Actualizar";
                        Session["opTipo"] = 2;
                        bit = true;
                        break;
                    }
                }
            }
            else if (e.CommandName == "delGrid")
            {
                foreach (GridDataItem item in gridcccmex.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = item.GetDataKeyValue("IdServicio").ToString();
                        Session["getIdServGrid"] = _idCampo;
                        string script = "function f(){callConfirm(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }

                }

            }

            if (bit == false)
                windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);

        }

        protected void gridcccmex_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                List<capascccmex.metadatos.centro> oCamposCat = new List<capascccmex.metadatos.centro>();
                oCamposCat = (List<capascccmex.metadatos.centro>)Session["getCamposCat"];
                gridcccmex.DataSource = oCamposCat;
            }
            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument.ToString() == "oka")
            {
                String param = eliminarCat();
                if (param.CompareTo("F") == 0)
                {
                    windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Servicio", null);
                }
                else
                {
                    windowManager1.RadAlert("Barco eliminado con éxito...", 300, 150, "Eliminando Servicio", null);
                    gridcccmex.MasterTableView.SortExpressions.Clear();
                    gridcccmex.MasterTableView.GroupByExpressions.Clear();
                    cargarCatalogo();
                }
            }
        }

        #endregion

        #region EVENTOS DE OPERACIONES

        void limpiarRegistros()
        {
            Session["opTipo"] = 1;
         addidserv.ReadOnly = false;
         addidserv.Text = "";
          addserv.Text = "";
            cmdEjecuta.Text = "Agregar";
            //addidCentro.Focus();
            cargarCatalogo();
        }
        void cargarCatalogo()
        {
            List<capascccmex.metadatos.servicio> oCamposCat = new List<capascccmex.metadatos.servicio>();
            capascccmex.biz.servicio obj = new capascccmex.biz.servicio();

            try
            {
                oCamposCat = obj.GetBizServicio(null, 0, 0);
                Session["getCamposCat"] = oCamposCat;
                gridcccmex.DataSource = oCamposCat;
                gridcccmex.DataBind();
                gridcccmex.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de Servicio", null);
            }
        }

        String eliminarCat()
        {
            String error = "F";
            String _idcampo = Session["getIdServGrid"].ToString().Trim().ToUpper();

            capascccmex.datos.servicio obj = new capascccmex.datos.servicio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidservicio", _idcampo));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        String AgregarCat()
        {
            String error = "F";
            String _idcampo = addidserv.Text.ToString().Trim().ToUpper();
            String _campo = addserv.Text.ToString().Trim().ToUpper();

            capascccmex.datos.servicio obj = new capascccmex.datos.servicio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidservicio", _idcampo));
            campos.Add(new SqlParameter("vcservicio", _campo));

            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        String actualizaCat()
        {            
            String error = "F";
            String _idcampo = addidserv.Text.ToString().Trim().ToUpper();
            String _campo = addserv.Text.ToString().Trim().ToUpper();

            capascccmex.datos.servicio obj = new capascccmex.datos.servicio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidservicio", _idcampo));
            campos.Add(new SqlParameter("vservicio", _campo));

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        #endregion
    }
}