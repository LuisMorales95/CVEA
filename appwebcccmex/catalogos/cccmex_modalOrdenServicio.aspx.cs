    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using capascccmex;
using System.Text;
using System.IO;

namespace appwebcccmex.catalogos
{
    public partial class cccmex_modalOrdenServicio : System.Web.UI.Page
    {
        List<capascccmex.metadatos.orden_servicio> lstCat = new List<capascccmex.metadatos.orden_servicio>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                    if (Session["opOServ"] != null)
                    {
                        loadmeses();
                        Int16 tipoOp = Convert.ToInt16(Session["opOServ"]);
                        if (tipoOp == 1)//alta
                        {
                            cmdEjecuta.Text = "Agregar Registro";
                          
                            addanio.Text = DateTime.Now.Year.ToString();
                        }
                        else if (tipoOp == 2)//actualiza
                        {
                            cmdEjecuta.Text = "Actualizar Registro";
                            lstCat = (List<capascccmex.metadatos.orden_servicio>)Session["getCamposCatOrdenServicio"];
                            cargarInfoOrden(lstCat);
                        }

                    }
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        private void cargarInfoOrden(List<capascccmex.metadatos.orden_servicio> _lstOp)
        {
            Int32 _idOP = Convert.ToInt32(Session["getIdOrdenServGrid"]);
          
            var getUsr = from oUsr in _lstOp
                         where oUsr.Idorden == _idOP
                         select oUsr;

            //lblEmpresa.Text = Session["getNombreEmpresa"].ToString();

            foreach (var ios in getUsr)
            {
                addordenservicio.Text = ios.Orden_servicio.ToString();
                addvolumen.Text = ios.Volumen.ToString();
                cmbmes.SelectedValue = ios.Mes.ToString();
               addanio.Text = ios.Anio.ToString();

            }

           
        }

        void loadmeses()
        {
            Dictionary<int, string> cmbCampos = new Dictionary<int, string>();
            cmbCampos.Add((int)1, (string)"ENERO");
            cmbCampos.Add((int)2, (string)"FEBRERO");

            cmbCampos.Add((int)3, (string)"MARZO");
            cmbCampos.Add((int)4, (string)"ABRIL");
            cmbCampos.Add((int)5, (string)"MAYO");
            cmbCampos.Add((int)6, (string)"JUNIO");
            cmbCampos.Add((int)7, (string)"JULIO");
            cmbCampos.Add((int)8, (string)"AGOSTO");
            cmbCampos.Add((int)9, (string)"SEPTIEMBRE");
            cmbCampos.Add((int)10, (string)"OCTUBRE");
            cmbCampos.Add((int)11, (string)"NOVIMEBRE");
            cmbCampos.Add((int)12, (string)"DICIEMBRE");

            cmbmes.DataSource = cmbCampos;
            cmbmes.DataTextField = "Value";
            cmbmes.DataValueField = "Key";
            cmbmes.DataBind();
        }

        String AgregarCat()
        {
            String error = "";
       
            string cmp1 = addordenservicio.Text.ToString().Trim().ToUpper();
            decimal cmp2 = Convert.ToDecimal(addvolumen.Text);
            Int16 cmp3 = Convert.ToInt16(cmbmes.SelectedValue);
            Int16 cmp4 = Convert.ToInt16(addanio.Text);


            capascccmex.datos.orden_servicio obj = new capascccmex.datos.orden_servicio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("@vr", System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@orden_servicio", cmp1));
            campos.Add(new SqlParameter("@volumen", cmp2));
            campos.Add(new SqlParameter("@mes", cmp3));
            campos.Add(new SqlParameter("@anio", cmp4));
        
            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        String actualizaCat()
        {
            String error = "F";

            Int32 _idOServ = Convert.ToInt32(Session["getIdOrdenServGrid"]);
            string cmp1 = addordenservicio.Text.ToString().Trim().ToUpper();
            decimal cmp2 = Convert.ToDecimal(addvolumen.Text);
            Int16 cmp3 = Convert.ToInt16(cmbmes.SelectedValue);
            Int16 cmp4 = Convert.ToInt16(addanio.Text);


            capascccmex.datos.orden_servicio obj = new capascccmex.datos.orden_servicio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("@vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@idorden", _idOServ));
            campos.Add(new SqlParameter("@orden_servicio", cmp1));
            campos.Add(new SqlParameter("@volumen", cmp2));
            campos.Add(new SqlParameter("@mes", cmp3));
            campos.Add(new SqlParameter("@anio", cmp4));
        

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        protected void cmdEjecuta_Click(object sender, EventArgs e)
        {
            valResumen.ValidationGroup = "add";
            Page.Validate("add");
            if (Page.IsValid)
            {
                try
                {

                    Int16 tipoOp = Convert.ToInt16(Session["opOServ"]);
                    if (tipoOp == 1)//alta
                    {
                        string  param = AgregarCat();
                        if (param.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Orden ingresada con éxito...", 300, 200, "Agregando Orden Servicio", null);

                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Agregando Orden Servicio", null);
                        }
                    }
                    else if (tipoOp == 2)//actualizar
                    {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando Orden Servicio", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Operación actualizada con éxito...", 300, 200, "actualizando Orden Servicio", null);
                        }
                    }

                    string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                catch (SqlException ex)
                {
                    convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Operación", null);

                }

            }
            else
            {
                #region parametrosColor

                if (cmbmes.SelectedValue != null && cmbmes.SelectedValue.Length > 0)
                    cmbmes.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");              
                else
                    cmbmes.BackColor = System.Drawing.ColorTranslator.FromHtml("#b94a48");
              

                if (addordenservicio.Text.Length > 0)
                    addordenservicio.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addordenservicio.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                if (addvolumen.Text.Length > 0)
                    addvolumen.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addvolumen.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                if (addanio.Text.Length > 0)
                    addanio.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addanio.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                #endregion
                RadWindowManager1.RadAlert("Existen registros obligatorios, favor de corregir ", 300, 100, "Captura de orden de servicio", null);
            }
        }
    }
}