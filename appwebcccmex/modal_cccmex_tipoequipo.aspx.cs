using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BEcccmex;
using BLcccmex;
using capascccmex;

namespace appwebcccmex
{
    public partial class modal_cccmex_tipoequipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    RadButton1.Text = Session["btn"].ToString();
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");
            }


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            RequiredFieldsResumen.ValidationGroup = "get";
            Page.Validate("get");
            if (Page.IsValid)
            {
                BLTipoEquipo buisnessLTipoEquipo = new BLTipoEquipo();
                if (Session["btn"].ToString() == "Save")
                {
                    BETipoEquipo tipoEquipo = new BETipoEquipo();
                    tipoEquipo.IdTipoEquipo = 0;
                    tipoEquipo.Codigo = txtCodigo.Text;
                    tipoEquipo.TipoEquipo = txtTipoEquipo.Text;
                    tipoEquipo.Descripcion = txtDes.Text;
                    int resultado = buisnessLTipoEquipo.addTipoEquipo(tipoEquipo);
                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Nuevo Tipo de Equipo registrado ! </br> TipoEquipo Num: " + resultado, 280, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se agrego ningun tipo de equipo. Favor de contactar con su Administrador de sistemas", 280, 300, "Eventos - Informaciòn", null);
                        return;
                    }
                }
                if (Session["btn"].ToString() == "Update")
                {
                    BETipoEquipo tipoEquipo = new BETipoEquipo();
                    tipoEquipo.IdTipoEquipo = convertir.toNInt64(Session["idTipoEquipo"]);
                    tipoEquipo.Codigo = txtCodigo.Text;
                    tipoEquipo.TipoEquipo = txtTipoEquipo.Text;
                    tipoEquipo.Descripcion = txtDes.Text;
                    int result = buisnessLTipoEquipo.updateTipoEquipo(tipoEquipo);
                    if (result > 0)
                    {
                        VentanaRad.RadAlert("Tipo de Equipo actualizado ! </br> TipoEquipo Num: " + result, 280, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se agrego ningun zona. Favor de contactar con su Administrador de sistemas", 280, 300, "Eventos - Informaciòn", null);
                        return;
                    }

                }
            }
            else
            {
                VentanaRad.RadAlert("Existen campos obligatorios, favor de verificar ", 400, 100, "Equipos - Validación", null);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
    }
}