using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;


namespace appwebcccmex.catalogos
{
    public partial class cccmex_centros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    Session["opTipo"] = 1;
                    cargarcentros();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region EVENTOS DE PANTALLAS
        
        protected void cmdcentro_Click(object sender, EventArgs e)
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
                        if (param.CompareTo("F")==0)
                        {
                            windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Agregando Centro", null);
                        }
                        else
                        {
                            windowManager1.RadAlert("Centro ingresado con éxito...", 300, 200, "Agregando Centro", null);
                        }
                    }
                    else if (tipoOp == 2)//actualizar
                    {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando Centro", null);
                        }
                        else
                        {
                            windowManager1.RadAlert("Centro actualizado con éxito...", 300, 200, "actualizando Centro", null);
                        }
                    }
                    limpiarRegistros();
                  addcentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                  addidCentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                  
                }
                catch (SqlException ex)
                {
                    windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Centros de trabajos", null);

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
            Int64? _idCampo = null;
            string _nombre = "";
            bool bit = false;

          if (e.CommandName == "updGrid")
            {
                cargarcentros();
                limpiarRegistros();
                bit = true;
            }
            else if (e.CommandName == "editGrid")
            {
                foreach (GridDataItem item in gridcccmex.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));
                        _nombre = item.GetDataKeyValue("Centro").ToString();
                        addcentro.Text = _nombre.ToString();
                        addidCentro.Text = _idCampo.ToString();
                        addidCentro.ReadOnly = true;

                        cmdcentro.Text = "Actualizar";                      
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
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));
                        Session["getIdCentroGrid"] = _idCampo;
                        string script = "function f(){callConfirm(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }

                }

            }

          else if (e.CommandName == "addGrid")
          {
              foreach (GridDataItem item in gridcccmex.MasterTableView.Items)
              {
                  if (item.Selected == true)
                  {
                      _idCampo = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));
                      _nombre = item.GetDataKeyValue("Centro").ToString();
                      Session["getidgridcentroactual"] = _idCampo;
                      Session["getgridcentroactual"] = _nombre;
                      string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                      bit = true;
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
                    windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Centro", null);
                }
                else
                {
                    windowManager1.RadAlert("Centro eliminado con éxito...", 300, 150, "Eliminando Centro", null);
                    gridcccmex.MasterTableView.SortExpressions.Clear();
                    gridcccmex.MasterTableView.GroupByExpressions.Clear();
                    cargarcentros();
                }
            }
        }

        #endregion

        #region EVENTOS DE OPERACIONES

        void limpiarRegistros()
        {
            Session["opTipo"] = 1;
            addidCentro.ReadOnly = false;
            addidCentro.Text = "";
            addcentro.Text = "";
           cmdcentro.Text = "Agregar";
           //addidCentro.Focus();
           cargarcentros();
        }
        void cargarcentros()
        {
            List<capascccmex.metadatos.centro> oCamposCat = new List<capascccmex.metadatos.centro>();
            capascccmex.biz.centro obj = new capascccmex.biz.centro();
       
            try
            {
                oCamposCat = obj.GetBizCentro(null, 0, 0);
                Session["getCamposCat"] = oCamposCat;
                    gridcccmex.DataSource = oCamposCat;
                    gridcccmex.DataBind();
                    gridcccmex.Rebind();               
                    //----------------------------------------

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de centros", null);
            }
        }

        String eliminarCat()
        {
            String error = "F";
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroGrid"]);

            capascccmex.datos.centro obj = new capascccmex.datos.centro();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidcentro", _idCentro));
            
            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        String AgregarCat()
        {
            String error = "F";
            Int64? _idcentro = convertir.toNInt64(addidCentro.Text);
            String _centro = addcentro.Text.ToString().Trim().ToUpper();

            capascccmex.datos.centro obj = new capascccmex.datos.centro();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            //campos.Add(new SqlParameter("vidcentro", _idcentro));
            campos.Add(new SqlParameter("vcentro", _centro));
            
            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        String actualizaCat()
        {
            String error = "F";

            Int64? _idcentro = convertir.toNInt64(addidCentro.Text);
            String _centro = addcentro.Text.ToString().Trim().ToUpper();

            capascccmex.datos.centro obj = new capascccmex.datos.centro();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidcentro", _idcentro));
            campos.Add(new SqlParameter("vcentro", _centro));

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        #endregion

        //protected void gridcccmex_SelectedIndexChanged(object sender, EventArgs e)
        //{         
        //    GridDataItem selectedItem = (GridDataItem)gridcccmex.SelectedItems[0];
        //    Int64? _idCampo = convertir.toNInt64(selectedItem.GetDataKeyValue("IdCentro"));
        //    string _nombre = selectedItem.GetDataKeyValue("Centro").ToString();
        //    Session["getidcampo"] = _idCampo;
        //    Session["getnombrecampo"] = _nombre;

        //    addcentro.Text = _nombre.ToString();
        //    addidCentro.Text = _idCampo.ToString();
        //    addidCentro.ReadOnly = true;

        //    cmdcentro.Text = "Actualizar";
        //    //Session["getIdCentroGrid"] = _idCampo;
        //    Session["opTipo"] = 2;

        //}

    }
}