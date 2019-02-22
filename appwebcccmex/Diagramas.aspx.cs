using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using capascccmex;
using System.Drawing;
using BLcccmex;
using BEcccmex;
using System.Text;
namespace appwebcccmex
{
    public partial class Diagramas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    //Session["getIdCentroUsr"] = 1;
                    int _idCentro = Convert.ToInt16(Session["getIdCentroUsr"]);
                    
                    nameCentro.Text = Session["nameCentroActual"].ToString();
                    

                    Cargarcentros(_idCentro);
                    InstalacionesbyCentro(_idCentro);

                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }


        void Cargarcentros(int idCentro)
        {
            List<capascccmex.metadatos.centro> oCamposCat = new List<capascccmex.metadatos.centro>();
            capascccmex.biz.centro obj = new capascccmex.biz.centro();
            Dictionary<Int64?, string> dcat = new Dictionary<Int64?, string>();

            bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            bool pemex = Convert.ToBoolean(Session["prmPemex"]);
            if (adm == true || pemex == true)                
                oCamposCat = obj.GetBizCentro(null, 0, 0);
            else
                oCamposCat = obj.GetBizCentro(idCentro, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dcat.Add(convertir.toNInt64(item.IdCentro), (string)item.Centro);
                }

                cmbcentro.DataSource = dcat;
                cmbcentro.DataTextField = "Value";
                cmbcentro.DataValueField = "Key";
                cmbcentro.DataBind();
                //cmbcentro.SelectedValue = idCentro.ToString();
                //cmbcentro.Enabled = false;
        }
        void InstalacionesbyCentro(int _idcentro)
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            //if (adm == true) _idcentro = null;

            Dictionary<Int16, string> dInst = new Dictionary<Int16, string>();
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
        protected void Cmbcentro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbcentro.SelectedValue.Length > 0)
            {

                int _idCentro = convertir.toInt32(cmbcentro.SelectedValue);
                InstalacionesbyCentro(_idCentro);
            }
        }

        protected void cmbInstalacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbInstalacion.SelectedValue.Length > 0)
            {

                int _idInst = convertir.toInt16(cmbInstalacion.SelectedValue);
                Session["getNameInstalacion"] = cmbInstalacion.Text.ToString().Trim().ToUpper();
                Session["getIDInstalacion"] = _idInst;
                BLcccmex.BLDiagrama objbl = new BLcccmex.BLDiagrama();
                BEcccmex.BEDiagrama objBE = new BEcccmex.BEDiagrama();
                List<BEcccmex.BEDiagrama> lista = objbl.GetDiagrama(_idInst);

                if (lista.Count > 0)
                {
                    Session["ObjDiagrama"] = lista;
                    CargarDiagrama(lista[0].archivo,_idInst);
                }
            }
        }

        protected void CargarObjetos(int idInstalacion){

            List<BEObjetoDiagrama> oCamposCat = new List<BEObjetoDiagrama>();
            BLEventoObjeto obj = new BLEventoObjeto();

            oCamposCat = obj.GetObjetoDiagrama(idInstalacion,0);
            if (oCamposCat.Count > 0)
            {
                Session["ObjDiagrama"] = oCamposCat;
                
            }
            StringBuilder sb = new StringBuilder("objetos = new Array();");

            sb.AppendLine();
            //----------------------------------------
            foreach (var item in oCamposCat)
            {
                sb.AppendFormat("objetos.push(new Objeto({0},{1},{2},{3},{4},{5},{6}));", item.objetoX, item.objetoY, item.objetoW, item.objetoH, item.idEvento, "'" + item.color + "'", "'" + item.tipoEvento + "'");              
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "obj", sb.ToString(), true);

        
        }


        protected void btnServerSide_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction2", "myFunction2();", true);
        }

        void CargarDiagrama(String diagrama, int inst)
        {

            CargarObjetos(inst);
            String jsfunc = "CargarDiagrama('" + diagrama + "');";
            //ScriptManager.RegisterStartupScript(this, GetType(), "CargarDiagrama", "CargarDiagrama(" + diagrama + ");", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CargarDiagrama", jsfunc, true);

        }
    }
}