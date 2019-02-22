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
    public partial class Diagramas2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                    CargarCentros();
                    CargarEvento();
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
            cmbEquipo.DataSource = null;
            cmbEquipo.Text = "";
            cmbEquipo.SelectedValue = null;
       
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
        List<BEcccmex.BEEquipo> lista;
        protected void EquipoByInstalacion(Int64? _idInstalacion)
        {
            BLcccmex.BLEquipo objbl = new BLcccmex.BLEquipo();
            lista = objbl.GetEquipo(_idInstalacion);
            if (lista.Count > 0)
            {
                Dictionary<int, string> dEquipo = new Dictionary<int, string>();

                foreach (var item in lista)
                {
                    dEquipo.Add(convertir.toInt16(item.idEquipo), (string)item.nombre);
                }
                //  Session["Equipos"] = dEquipo;
                cmbEquipo.DataSource = dEquipo;
                cmbEquipo.DataTextField = "Value";
                cmbEquipo.DataValueField = "Key";
                cmbEquipo.DataBind();
            }
        }
        
        protected void cmbInstalacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
           cmbEquipo.DataSource = null;
            cmbEquipo.Text = "";
            cmbEquipo.SelectedValue = null;
        }
        protected void cmbEquipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
          /* if (cmbEquipo.SelectedValue.Length > 0)
            {
               
                BEcccmex.BEEquipo objBE = new BEcccmex.BEEquipo();
                objBE = lista.Find(x => x.idEquipo == convertir.toNInt64(cmbEquipo.SelectedValue));
                lbinfoeequipo.Text = "This is a test:" + objBE.nombre;
                 
            }*/
        }

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            int resultado = 0;
            string fechaactual = DateTime.Now.ToString();
            VentanaRad.RadPrompt("¿Fecha de Realización del Evento?", "promptCallBackFn", 350, 230, null, "Modificar Evento", fechaactual);

            BLcccmex.BLEvento objbl = new BLcccmex.BLEvento();
            resultado = objbl.AddEventoHistorico(1,1,this.txtComentario.Text,1);

            }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            RequiredFieldsResumen.ValidationGroup = "get";
            Page.Validate("get");
            if (Page.IsValid)
            {

                BEcccmex.BEEvento objBE = new BEcccmex.BEEvento();
                objBE.evento = this.txtEvento.Text;
                objBE.fechaEvento = this.txtFecha.SelectedDate.Value;
                objBE.idEquipo = convertir.toNInt64(this.cmbEquipo.SelectedValue);
                objBE.tipoEvento = this.cboTipoEvento.SelectedValue;
                objBE.vigencia = this.txtVigencia.Text;
                objBE.prealarma = Convert.ToInt16(this.txtPreAlarma.Text);
                objBE.postAlarma = Convert.ToInt16(this.txtPostAlarma.Text);
                objBE.observacion = this.txtComentario.Text;
                BLcccmex.BLEvento objbl = new BLcccmex.BLEvento();

                int resultado = 0;

                if (Session["tempOpEvento"].ToString() == "Agregar")
                {
                   resultado = objbl.AddEvento(objBE);

                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Nuevo evento registrado ! </br> Num. Evento : " + resultado, 400, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        Session["tempIdEvento"] = null;
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se agrego ningun evento. Favor de contactar con su Administrador de sistemas", 450, 300, "Eventos - Informaciòn", null);
                        return;
                    }
                }

                if (Session["tempOpEvento"].ToString() == "Actualizar")
                {
                    objBE.idEvento = convertir.toNInt64(Session["tempIdEvento"]);
                    resultado = objbl.UpdateEvento(convertir.toInt32(Session["tempIdEvento"]), convertir.toInt32(cmbEquipo.SelectedValue), txtEvento.Text,cboTipoEvento.Text, convertir.toInt32(txtPreAlarma.Text),(DateTime) txtFecha.SelectedDate, txtVigencia.Text,
                                                  convertir.toInt32(txtPostAlarma.Text), txtComentario.Text);

                    if (resultado > 0)
                    {
                        VentanaRad.RadAlert("Se actualizo correctamente el evento. ! </br> Num. Evento : " + Session["tempIdEvento"].ToString(), 400, 120, "Confirmación - Registro de Evento", "CloseAndRebind");
                        Session["tempIdEvento"] = null;
                        return;
                    }
                    else
                    {
                        VentanaRad.RadAlert("No se actualizo ningun evento. Favor de contactar con su Administrador de sistemas", 450, 300, "Eventos - Informaciòn", null);
                        return;
                    }
                }

            }
            else
            {
                MostrarCamposInvalidados();
                VentanaRad.RadAlert("Existen campos obligatorios, favor de verificar ", 400, 100, "Eventos - Validación", null);
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

        protected void cmbEquipo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (cmbInstalacion.SelectedValue.Length > 0)
            {
                int _idInstalacion = Convert.ToInt16(cmbInstalacion.SelectedValue);
                EquipoByInstalacion(_idInstalacion);
            }
        }

        protected void MostrarCamposInvalidados()
        {
            #region parametrosColor

               if (cmbcentro.SelectedValue != null && cmbcentro.SelectedValue.Length > 0)
                   cmbcentro.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
            else
                   cmbcentro.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");


            if (cmbInstalacion.SelectedValue != null && cmbInstalacion.SelectedValue.Length > 0)
                cmbInstalacion.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
            else
                cmbInstalacion.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

            if (cmbEquipo.SelectedValue != null && cmbEquipo.SelectedValue.Length > 0)
                cmbEquipo.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
            else
                cmbEquipo.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

              if (txtFecha.SelectedDate.HasValue)
                  txtFecha.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
            else
                  txtFecha.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

             if (cboTipoEvento.SelectedValue != null && cboTipoEvento.SelectedValue.Length > 0)
                 cboTipoEvento.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
            else
                 cboTipoEvento.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

             if (txtEvento.Text.Length>0)
                 txtEvento.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
             else
                 txtEvento.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

             if (txtPostAlarma.Text.Length > 0)
                 txtPostAlarma.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
             else
                 txtPostAlarma.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

             if (txtPreAlarma.Text.Length > 0)
                 txtPreAlarma.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
             else
                 txtPreAlarma.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

             if (txtVigencia.Text.Length > 0)
                 txtVigencia.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
               
             else
                 txtVigencia.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

            #endregion
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        void CargarEvento()
        {
            if (Session["tempIdEvento"] != null) { 

            Int64? _idEvento = convertir.toNInt64(Session["tempIdEvento"]);

            List<BEcccmex.BEEventoEquipo> oCamposCat = new List<BEcccmex.BEEventoEquipo>();
            oCamposCat = (List<BEcccmex.BEEventoEquipo>)Session["tempCatEventos"];

            var getEventos = from oEventos in oCamposCat
                             where oEventos.IdEvento == _idEvento
                             select oEventos;
           
            foreach (var iEvento in getEventos)
            {
                cmbcentro.SelectedValue = iEvento.IdCentro.ToString();
                cmbEquipo.SelectedValue = iEvento.IdEquipo.ToString();
                txtEvento.Text = iEvento.Evento;
                if (iEvento.FechaEvento != null)
                    txtFecha.SelectedDate = Convert.ToDateTime(iEvento.FechaEvento);
                txtPostAlarma.Text = iEvento.PostAlarma.ToString();
                txtPreAlarma.Text = iEvento.Prealarma.ToString();
                txtVigencia.Text = iEvento.Vigencia.ToString();
                txtComentario.Text = iEvento.Observacion;
                InstalacionesbyCentro(Convert.ToInt32(cmbcentro.SelectedValue));

                cmbInstalacion.SelectedValue = iEvento.IdInstalacion.ToString();

                EquipoByInstalacion(Convert.ToInt32(cmbInstalacion.SelectedValue));

                cmbEquipo.SelectedValue = iEvento.IdEquipo.ToString();

            }
            cmbcentro.Enabled = false;
            cmbInstalacion.Enabled = false;
            cmbEquipo.Enabled = false;
            }
        }


    }
}