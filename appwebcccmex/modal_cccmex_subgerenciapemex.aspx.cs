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
using Telerik.Web.UI;


namespace appwebcccmex
{
    public partial class modal_cccmex_subgerenciapemex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                    Int64? _idRrg = convertir.toNInt64(Session["getIdRegGrid"]);
                    lblctrlreg.Text = _idRrg.ToString();
                    loadcmbsEstatus(cmbrevisado, 1);
                    loadcmbsEstatus(cmbestatuspago, 2);
                    loadRegActual();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }


        #region EVENTOS Y METODOS
        void loadcmbsEstatus(RadComboBox telerik,int tipo)
        {
          try
            {
                Dictionary<char, string> dcat = new Dictionary<char, string>();

                if (tipo == 1)
                {
                    dcat.Add('N', "Sin Revisar");
                    dcat.Add('S', "Revisado - No se puede modificar");
                    dcat.Add('T', "En trámite");
                    dcat.Add('C', "Cancelado");
                }
                else
                {
                    dcat.Add('A', "Aceptado");
                    dcat.Add('P', "Pendiente");
                }

                telerik.DataSource = dcat;
                telerik.DataTextField = "Value";
                telerik.DataValueField = "Key";
                telerik.DataBind();

            }
            catch (Exception ex)
            {
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando instalaciones", null);
               
        }
    }

        void loadRegActual()
        {
            Int64? _idRrg = convertir.toNInt64(Session["getIdRegGrid"]);
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            oCamposCat = (List <capascccmex.metadatos.movproducto>) Session["getCamposCatMovimiento"];

            var getReg = from oReg in oCamposCat
                         where oReg.IdReg == _idRrg
                         select oReg;

            Label lblorderservicio = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblorderservicio");
            Label lblidproducto = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblidproducto");
            Label lblproducto = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblproducto");

            Label lblidcentro = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblidcentro");
            Label lblcentro = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblcentro");

            Label lblidservicio = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblidservicio");
            Label lblservicio = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblservicio");

            Label lblbarco = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblBarco");
            Label lblexp = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblexp");

            Label lblmezcla = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblmezcla"); 

            Label lblpropileno_turbosina = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblpropileno_turbosina");
            Label lblreg_propileno_turbosina = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblreg_propileno_turbosina");

            Label lblanio_mes = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblanio_mes");


            Label lblfecha = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblfecha");

            Label lblfolcertcantidad_file = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblfolcertcantidad_file");
            Label lblfolcertcalidad_file = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblfolcertcalidad_file");

            bool bImp = false;

            foreach (var iReg in getReg)
            {
                cmbrevisado.SelectedValue = iReg.Estatus_revisado.ToString();
                cmbestatuspago.SelectedValue = iReg.Estatus_pagado.ToString();

                lblctrlregProd.Text = iReg.Idregbyprod.ToString();

                lblorderservicio.Text = iReg.Orden_servicio.ToString().Trim().ToUpper();
                lblidproducto.Text = iReg.IdProducto.ToString();
                lblproducto.Text = iReg.NombreProducto.ToString().Trim().ToUpper();

                lblidcentro.Text = iReg.IdCentro.ToString();
                lblcentro.Text = iReg.NombreCentro.ToString().Trim().ToUpper();

                lblidservicio.Text = iReg.IdServicio.ToString();
                lblservicio.Text = iReg.NombreServicio.ToString().Trim().ToUpper();

                lblmezcla.Text = string.Format("{0:#,#0.000}", iReg.Cant_insp_mezcla);

                lblbarco.Text = iReg.NombreBarco.ToString();
                bImp = Convert.ToBoolean(iReg.BarcoImp);
                lblexp.Text = bImp == true ? "Importación" : "";

                if (iReg.IdProducto.ToString().CompareTo("33006") == 0)
                {
                    lblpropileno_turbosina.Text = "Lote";
                    lblreg_propileno_turbosina.Text = iReg.Lote_turbosina;
                }
                else
                {
                    lblpropileno_turbosina.Text = "Propileno";
                    lblreg_propileno_turbosina.Text = string.Format("{0:#,#0.000}", iReg.Propileno);
                }

                string[] words=iReg.Referencia_folio.Split('|');

                lblanio_mes.Text = string.Format("{0:yyyy | MM}", iReg.Fecha);
                lblfecha.Text = string.Format("{0:dd/MM/yyyy}", iReg.Fecha);
                lblfolcertcantidad_file.Text = string.Format("{0} | {1}", iReg.Folio_cert_cant_aux, words[1]);
                lblfolcertcalidad_file.Text = string.Format("{0} | {1}", iReg.Folio_cert_calidad_aux, words[0]);

                addComent.Text = iReg.Comentarios.ToString();

                //Solo personal de pmx puede modificar el registro...
                if (iReg.Estatus_revisado.CompareTo("S") == 0 && iReg.Estatus_pagado.CompareTo("A")==0)
                    cmdEjecuta.Enabled = false;
                else
                    cmdEjecuta.Enabled = true;
            }
        }

        String actualizaCat()
        {
            //VERICAR QUE USUARIO GENERO LA OPERACIÓN DE CAMBIO ...
            String error = "F";

            Int64? _idRrg = convertir.toNInt64(Session["getIdRegGrid"]);

            capascccmex.datos.mov_producto obj = new capascccmex.datos.mov_producto();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidreg", _idRrg));
            campos.Add(new SqlParameter("vestatus_revisado", cmbrevisado.SelectedValue.ToString() ));
            campos.Add(new SqlParameter("vestatus_pagado", cmbestatuspago.SelectedValue.ToString()));
            campos.Add(new SqlParameter("@comentarios", addComent.Text.ToString().Trim().ToUpper()));
            
            error = obj.actualizarByPemex(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        protected void cmdEjecuta_Click(object sender, EventArgs e)
        {
            valResumen.ValidationGroup = "get";
            Page.Validate("get");
            if (Page.IsValid)
            {
                try
                {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando Inspección", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Inspección actualizada con éxito...", 300, 200, "Actualizando Inspección", null);
                        }
                    

                    string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                catch (SqlException ex)
                {
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Inspección", null);

                }
            }
            else
                RadWindowManager1.RadAlert("Error: Debe selección los estatus, Revisión y Pago", 300, 100, "Inspección", null);
        }
        #endregion
    }
}