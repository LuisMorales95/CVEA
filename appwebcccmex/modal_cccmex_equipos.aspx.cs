using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using capascccmex;
using System.Drawing;

namespace appwebcccmex
{
    public partial class modal_cccmex_equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    CargarCentros();
                    CargarEquipo();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");
            }
        }

        protected void CargarCentros()
        {
            List<capascccmex.metadatos.centro> oCamposCat = new List<capascccmex.metadatos.centro>();
            capascccmex.biz.centro obj = new capascccmex.biz.centro();
            Dictionary<int, string> dcat = new Dictionary<int, string>();

            //RadComboBox cmbcat = (RadComboBox)RadPanelBar1.FindItemByValue("info").FindControl("cmbcentro");

            oCamposCat = obj.GetBizCentro(null, 0, 0);
            //----------------------------------------
            foreach (var item in oCamposCat)
            {
                dcat.Add(convertir.toInt16(item.IdCentro), (string)item.Centro);
            }

            cmbcentro.DataSource = dcat;
            cmbcentro.DataTextField = "Value";
            cmbcentro.DataValueField = "Key";
            cmbcentro.DataBind();

        }
        protected void Cmbcentro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbInstalacion.DataSource = null;
            cmbInstalacion.Text = "";
            cmbInstalacion.SelectedValue = null;
       
        }
        protected void InstalacionesbyCentro(int _idcentro)
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            //if (adm == true) _idcentro = null;

            Dictionary<int, string> dInst = new Dictionary<int, string>();
            oCamposCat = obj.GetInstalacionDiagrama(_idcentro);
            //----------------------------------------
            foreach (var item in oCamposCat)
            {
                dInst.Add(convertir.toInt16(item.IdInst), (string)item.Nombre);
            }

            cmbInstalacion.DataSource = dInst;
            cmbInstalacion.DataTextField = "Value";
            cmbInstalacion.DataValueField = "Key";
            cmbInstalacion.DataBind();
        }
              
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            RequiredFieldsResumen.ValidationGroup = "get";
            Page.Validate("get");
            if (Page.IsValid)
            {
                BLcccmex.BLEquipo objbl = new BLcccmex.BLEquipo();
                int resultado = 0;

                if (Session["tempOpEquipo"].ToString() == "Agregar")
                {

                    resultado = objbl.AddEquipo(convertir.toInt32(cmbInstalacion.SelectedValue), txtEquipo.Text, txtDescripcion.Text,
                        txtTag.Text, txtDetalle.Text);
                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Nuevo equipo registrado ! </br> Num. Equipo : " + resultado, 400, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        Session["tempIdEquipo"] = null;
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se agrego ningun equipo. Favor de contactar con su Administrador de sistemas", 450, 300, "Eventos - Informaciòn", null);
                        return;
                    }
                }

               if (Session["tempOpEquipo"].ToString() == "Actualizar")
                {
                    int idEquipo = int.Parse(Session["tempIdEquipo"].ToString());
                    resultado = objbl.UpdateEquipo(convertir.toInt32(cmbInstalacion.SelectedValue),idEquipo, txtEquipo.Text, txtDescripcion.Text,
                        txtTag.Text, txtDetalle.Text);

                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Se actualizo correctamente el equipo. ! </br> Num. Equipo : " + Session["tempIdEquipo"].ToString(), 400, 120, "Confirmación - Registro de Equipo", "CloseAndRebind");
                        Session["tempIdEquipo"] = null;
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se actualizo ningun equipo. Favor de contactar con su Administrador de sistemas", 450, 300, "Equipos - Informaciòn", null);
                        return;
                    }
                }

            }
            else
            {
                MostrarCamposInvalidados();
                VentanaRad.RadAlert("Existen campos obligatorios, favor de verificar ", 400, 100, "Equipos - Validación", null);
            }
            
        }

        protected void cmbcentro_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            CargarCentros();           
        }

        protected void cmbInstalacion_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (cmbcentro.SelectedValue.Length > 0)
            {
                int _idCentro = Convert.ToInt16(cmbcentro.SelectedValue);
                InstalacionesbyCentro(_idCentro);
            }
        }   
        
        protected void MostrarCamposInvalidados()
        {
            #region parametrosColor

            String defInvalidStyle = "border - color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);";
            String defValidStyle = "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);";
            //Falta verificar validacion para combos 
            if (cmbcentro.SelectedValue != null && cmbcentro.SelectedValue.Length > 0)
                cmbcentro.Attributes.Add("style", defValidStyle);
            else
                cmbcentro.Attributes.Add("style", defInvalidStyle);

            if (cmbInstalacion.SelectedValue != null && cmbInstalacion.SelectedValue.Length > 0)
                cmbInstalacion.Attributes.Add("style", defValidStyle);
            else
                cmbInstalacion.Attributes.Add("style", defInvalidStyle);

            if (String.IsNullOrEmpty(txtEquipo.Text))
                txtEquipo.Attributes.Add("style", defValidStyle);
            else
                txtEquipo.Attributes.Add("style", defInvalidStyle);

            if (String.IsNullOrEmpty(txtDescripcion.Text))
                txtDescripcion.Attributes.Add("style", defValidStyle);
            else
                txtDescripcion.Attributes.Add("style", defInvalidStyle);

            if (String.IsNullOrEmpty(txtTag.Text))
                txtTag.Attributes.Add("style", defValidStyle);
            else
                txtTag.Attributes.Add("style", defInvalidStyle);

            if (String.IsNullOrEmpty(txtDetalle.Text))
                txtDetalle.Attributes.Add("style", defValidStyle);
            else
                txtDetalle.Attributes.Add("style", defInvalidStyle);

            #endregion
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        void CargarEquipo()
        {
            if (Session["tempIdEquipo"] != null) { 

            Int64? _idEquipo = convertir.toInt32(Session["tempIdEquipo"]);

            List<BEcccmex.BEEquipo> oCamposCat = new List<BEcccmex.BEEquipo>();
            oCamposCat = (List<BEcccmex.BEEquipo>)Session["tempCatEquipos"];

            var getEquipos = from oEquipos in oCamposCat
                             where oEquipos.idEquipo == _idEquipo
                             select oEquipos;
           
            foreach (var iEquipo in getEquipos)
            {
                cmbcentro.SelectedValue = iEquipo.IdCentro.ToString();
                    txtEquipo.Text = iEquipo.nombre;
                    txtDescripcion.Text = iEquipo.descripcion;
                    txtDetalle.Text = iEquipo.detalle;
                    txtTag.Text = iEquipo.tag;

                InstalacionesbyCentro(convertir.toInt32(cmbcentro.SelectedValue));

                cmbInstalacion.SelectedValue = iEquipo.IdInstalacion.ToString();
                
            }
            cmbcentro.Enabled = false;
            cmbInstalacion.Enabled = false;
            }
        }


    }
}