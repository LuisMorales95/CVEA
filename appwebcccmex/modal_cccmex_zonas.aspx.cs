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
    public partial class modal_cccmex_zonas : System.Web.UI.Page
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
                BLZona buisnessLZona = new BLZona();
                if (Session["btn"].ToString() == "Save")
                {
                    BEZona zona = new BEZona();
                    zona.IdZona = 0;
                    zona.Zona = txtZone.Text;
                    zona.Descripcion = txtDes.Text;
                    int resultado = buisnessLZona.AddZona(zona);
                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Nueva Zona registrado ! </br> Num. Zona : " + resultado, 280, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
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
                    BEZona zona = new BEZona();
                    zona.IdZona = convertir.toNInt64(Session["IdZona"]);
                    zona.Zona = txtZone.Text;
                    zona.Descripcion = txtDes.Text;
                    int result = buisnessLZona.UpdateZona(zona);
                    if (result > 0)
                    {
                        VentanaRad.RadAlert("Zona actualizada ! </br> Num. Zona : " + result, 280, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
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