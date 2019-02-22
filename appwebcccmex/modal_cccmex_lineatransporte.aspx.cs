using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLcccmex;
using BEcccmex;
using capascccmex;

namespace appwebcccmex
{
    public partial class modal_cccmex_lineatransporte : System.Web.UI.Page
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
                BLLineaTransporte buisnessLTransporte = new BLLineaTransporte();
                if (Session["btn"].ToString() == "Save")
                {
                    BELineaTransporte lineaTransporte = new BELineaTransporte();
                    lineaTransporte.IdLineaTransporte = 0;
                    lineaTransporte.LineaTransporte = txtLinea.Text;
                    lineaTransporte.Descripcion = txtDes.Text;
                    int resultado = buisnessLTransporte.addLineaTransporte(lineaTransporte);
                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Nueva Linea de Transporte registrado ! </br> Num. Linea de Transporte : " + resultado, 280, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se agrego ningun zona. Favor de contactar con su Administrador de sistemas", 280, 300, "Eventos - Informaciòn", null);
                        return;
                    }
                }
                if (Session["btn"].ToString() == "Update")
                {
                    BELineaTransporte lineaTransporte = new BELineaTransporte();
                    lineaTransporte.IdLineaTransporte = convertir.toNInt64(Session["idLineaTransporte"]);
                    lineaTransporte.LineaTransporte = txtLinea.Text;
                    lineaTransporte.Descripcion = txtDes.Text;
                    int result = buisnessLTransporte.updateLineaTransporte(lineaTransporte);
                    if (result > 0)
                    {
                        VentanaRad.RadAlert("Linea de Transporte actualizada ! </br> Num. Linea de Transporte : " + result, 280, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se agrego ningun Linea de Transporte. Favor de contactar con su Administrador de sistemas", 280, 300, "Eventos - Informaciòn", null);
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