using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using capascccmex;
using System.Drawing;
using BEcccmex;

namespace appwebcccmex
{
    public partial class modal_cccmex_infoevento : System.Web.UI.Page
    {
        int idevento;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    idevento = Convert.ToInt16(this.Request["EventoID"]);
                    MostrarDatos(idevento);
                    Session["idevento"] = idevento;
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");
            }
        }




              
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int idevento = Convert.ToInt16(Session["idevento"].ToString());
            BLcccmex.BLEventoObjeto objbl = new BLcccmex.BLEventoObjeto();
            int r = objbl.EnviarCorreoEvento(idevento,"");

            if (r==0)
                windowManager1.RadAlert("El Correo fue Enviado con Exito", 300, 100, "Envio de Correo", null);
            else
                windowManager1.RadAlert("Correo no Enviado", 300, 100, "Envio de Correo", null);
            
        }



 


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        void MostrarDatos(int idevento)
        {



            List<BEObjetoDiagrama> objDiags = new List<BEObjetoDiagrama>();
            objDiags = (List<BEObjetoDiagrama>)Session["ObjDiagrama"];

            var getInfo = from objetos in objDiags
                          where objetos.idEvento == idevento
                          select objetos;

            foreach (var info in getInfo)
            {
                Instalacion.Text = info.instalacion;
                FechaEvento.Text = info.fechaEvento;
                TipoEvento.Text = info.tipoEvento;
                  
                    Equipo.Text = info.nombreEquipo;
                    this.Responsable.Text = info.responsable;
                
            }

        }


    }
}