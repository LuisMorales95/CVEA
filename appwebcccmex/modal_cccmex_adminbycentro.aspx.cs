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
    public partial class modal_cccmex_adminbycentro : System.Web.UI.Page
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
                    cargarcentrosByInstalaciones();
                    loadRegActual();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }


        #region EVENTOS Y METODOS
     

        void loadRegActual()
        {
            Int64? _idRrg = convertir.toNInt64(Session["getIdRegGrid"]);
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCmpCatMovimientoadmin"];

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

            Label lblmezcla = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblmezcla");

            Label lblpropileno_turbosina = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblpropileno_turbosina");
            Label lblreg_propileno_turbosina = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblreg_propileno_turbosina");

            Label lblanio_mes = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblanio_mes");


            Label lblfecha = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblfecha");

            Label lblfolcertcantidad_file = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblfolcertcantidad_file");
            Label lblfolcertcalidad_file = (Label)RadPanelBar1.FindItemByValue("info").FindControl("lblfolcertcalidad_file");



            foreach (var iReg in getReg)
            {
              cmbinstalacion.SelectedValue = iReg.IdInst.ToString();
              addcantidadinsp.Text = iReg.Cant_insp_mezcla.ToString();
              rdpFecha.SelectedDate = iReg.Fecha;
              addordenservicio.Text = iReg.Orden_servicio.ToString();
              addanio.Text = iReg.Fecha.Value.Year.ToString();
              addmes.Text = iReg.Fecha.Value.Month.ToString();
              addpropileno.Text = iReg.Propileno.ToString();

              lblctrlregProd.Text = iReg.Idregbyprod.ToString();
                lblorderservicio.Text = iReg.Orden_servicio.ToString().Trim().ToUpper();
                lblidproducto.Text = iReg.IdProducto.ToString();
                lblproducto.Text = iReg.NombreProducto.ToString().Trim().ToUpper();

                lblidcentro.Text = iReg.IdCentro.ToString();
                lblcentro.Text = iReg.NombreCentro.ToString().Trim().ToUpper();

                lblidservicio.Text = iReg.IdServicio.ToString();
                lblservicio.Text = iReg.NombreServicio.ToString().Trim().ToUpper();

                lblmezcla.Text = string.Format("{0:#,#0.000}", iReg.Cant_insp_mezcla);

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

                string[] words = iReg.Referencia_folio.Split('|');

                lblanio_mes.Text = string.Format("{0:yyyy | MM}", iReg.Fecha);
                lblfecha.Text = string.Format("{0:dd/MM/yyyy}", iReg.Fecha);
                lblfolcertcantidad_file.Text = string.Format("{0} | {1}", iReg.Folio_cert_cant_aux, words[1]);
                lblfolcertcalidad_file.Text = string.Format("{0} | {1}", iReg.Folio_cert_calidad_aux, words[0]);

                if (iReg.Estatus_revisado.CompareTo("S") == 0 && iReg.Estatus_pagado.CompareTo("A") == 0)
                {
                    cmdEjecuta.Enabled = false;
                    btndel.Enabled = false;
                }
                else
                { 
                    cmdEjecuta.Enabled = true;
                    btndel.Enabled = true;
                }

                cmdEjecuta.Enabled = true;
                btndel.Enabled = true;
            }
        }

        String actualizaCat()
        {
            String error = "F";

            Int64? _idRrg = convertir.toNInt64(Session["getIdRegGrid"]);
            string cmt = "";

            capascccmex.datos.mov_producto obj = new capascccmex.datos.mov_producto();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@vidreg", _idRrg));
            campos.Add(new SqlParameter("@vidinst", convertir.toNInt64( cmbinstalacion.SelectedValue)));
            campos.Add(new SqlParameter("@vcant_insp_mezcla", convertir.toNDecimal(addcantidadinsp.Text)));
            campos.Add(new SqlParameter("@propileno", convertir.toNDecimal(addpropileno.Text)));
            campos.Add(new SqlParameter("@vanio", convertir.toNInt32( rdpFecha.SelectedDate.Value.Year)));
            campos.Add(new SqlParameter("@vmes", convertir.toNInt32( rdpFecha.SelectedDate.Value.Month)));
            //campos.Add(new SqlParameter("@vfecha", rdpFecha.SelectedDate.Value));
            campos.Add(new SqlParameter("@vfecha", string.Format("{0:yyyy-MM-dd}", rdpFecha.SelectedDate.Value)));
            campos.Add(new SqlParameter("@orden_servicio", addordenservicio.Text.ToString().Trim() ));
            campos.Add(new SqlParameter("@comentarios", cmt.ToString()));

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        String eliminarCat()
        {
            String error = "F";
            Int64? _idRrg = convertir.toNInt64(Session["getIdRegGrid"]);

            capascccmex.datos.mov_producto obj = new capascccmex.datos.mov_producto();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidreg", _idRrg));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        void cargarcentrosByInstalaciones()
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            int _idcentro = convertir.toInt32(Session["idcentrobygrid"]);
            //bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            //if (adm == true) _idcentro = null;

            try
            {


                Dictionary<Int64?, string> dInst = new Dictionary<Int64?, string>();
                oCamposCat = obj.GetBizInstalaciones(_idcentro, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dInst.Add(convertir.toNInt64(item.IdInst), (string)item.Nombre);
                }

             cmbinstalacion.DataSource = dInst;
             cmbinstalacion.DataTextField = "Value";
             cmbinstalacion.DataValueField = "Key";
             cmbinstalacion.DataBind();

            }
            catch (SqlException ex)
            {
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando instalaciones", null);
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
            }
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
                RadWindowManager1.RadAlert("Error: Existen campos que no han sido llenados ...", 300, 100, "Inspección", null);
        }

        #endregion

        protected void btndel_Click(object sender, EventArgs e)
        {
            try
            {
                String param2 = eliminarCat();
                if (param2.CompareTo("F") == 0)
                {
                    RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Inspección", null);
                }
                else
                {
                    RadWindowManager1.RadAlert("Inspección elimianda con éxito...", 300, 200, "Eliminando Inspección", null);
                }


                string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (SqlException ex)
            {
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Inspección", null);

            }
        }
    }
}