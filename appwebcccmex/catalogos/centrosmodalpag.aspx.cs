using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;

namespace appwebcccmex.catalogos
{
    public partial class centrosmodalpag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    Session["opTipo"] = 1;
                    lblCentroActual.Text = Session["getgridcentroactual"].ToString();
                    lblidcentro.Text = Session["getidgridcentroactual"].ToString();
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
                        if (param.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Agregando Centro", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Centro ingresado con éxito...", 300, 200, "Agregando Centro", null);
                        }
                    }
                    else if (tipoOp == 2)//actualizar
                    {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando Centro", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Centro actualizado con éxito...", 300, 200, "actualizando Centro", null);
                        }
                    }
                    limpiarRegistros();
                    addcentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                    addidCentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                }
                catch (SqlException ex)
                {
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Centros de trabajos", null);

                }
            }
            else
            {
                if (addcentro.Text.Length > 0)
                    addcentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else if (addidCentro.Text.Length > 0)
                    addidCentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                {
                    addcentro.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                    addidCentro.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                }
            }

        }

        protected void gridcccmex_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int _idCampo = 0;
            //Int64? _idCampo2 = null;
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
                        _idCampo = Convert.ToInt32(item.GetDataKeyValue("IdInst"));
                        //_idCampo2 = convertir.toNInt64(item.GetDataKeyValue("IdCentro"));
                        _nombre = item.GetDataKeyValue("Nombre").ToString();


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
                        _idCampo = convertir.toInt32(item.GetDataKeyValue("IdInst"));
                        String param = eliminarCat(_idCampo);
                        if (param.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Centro", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Centro eliminado con éxito...", 300, 150, "Eliminando Centro", null);
                            gridcccmex.MasterTableView.SortExpressions.Clear();
                            gridcccmex.MasterTableView.GroupByExpressions.Clear();
                            cargarcentros();
                        }

                        bit = true;
                        break;
                    }

                }

            }

            if (bit == false)
                RadWindowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);

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
            int _idCentro = convertir.toInt32(Session["getidgridcentroactual"]);
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            try
            {
                oCamposCat = obj.GetBizInstalaciones(_idCentro, 0, 0);
                Session["getCamposCat"] = oCamposCat;
                gridcccmex.DataSource = oCamposCat;
                gridcccmex.DataBind();
                gridcccmex.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de centros", null);
            }
        }

        String eliminarCat(int idcat)
        {
            String error = "F";
            int _idInstal = convertir.toInt32(idcat);
            int _idCentro = convertir.toInt32(Session["getidgridcentroactual"]);

            capascccmex.datos.instalaciones obj = new capascccmex.datos.instalaciones();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidinst", _idInstal));
            campos.Add(new SqlParameter("vidcentro", _idCentro));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        String AgregarCat()
        {
            String error = "F";
            int _idInstal = convertir.toInt32(addidCentro.Text);
            int _idCentro = convertir.toInt32(lblidcentro.Text);
            String _Nombre = addcentro.Text.ToString().Trim().ToUpper();

            capascccmex.datos.instalaciones obj = new capascccmex.datos.instalaciones();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidinst", _idInstal));
            campos.Add(new SqlParameter("vidcentro", _idCentro));
            campos.Add(new SqlParameter("vnombre", _Nombre));

            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        String actualizaCat()
        {
            String error = "F";

            Int64? _idInstal = convertir.toNInt64(addidCentro.Text);
            Int64? _idCentro = convertir.toNInt64(lblidcentro.Text);
            String _Nombre = addcentro.Text.ToString().Trim().ToUpper();

            capascccmex.datos.instalaciones obj = new capascccmex.datos.instalaciones();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidinst", _idInstal));
            campos.Add(new SqlParameter("vidcentro", _idCentro));
            campos.Add(new SqlParameter("vnombre", _Nombre));

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        #endregion
    }
}