using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;


namespace appwebcccmex
{
    public partial class cccmex_capturabylaboratorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    //Session["opTipo"] = 1;
                    //Session["getIdCentroUsr"] = 1;
                    int _idCentro = Convert.ToInt32(Session["getIdCentroUsr"]);
                    nameCentro.Text = Session["nameCentroActual"].ToString();
                    cargarcentrosByInstalaciones(_idCentro);
                    //cargarMovimientos(_idCentro, null, null);
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region EVENTOS DE OPERACIONES
        void cargarcentrosByInstalaciones(int _idcentro)
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            bool adm = Convert.ToBoolean(Session["prmAdmin"]);
            if (adm == true) _idcentro = 0;

            try
            {


                Dictionary<Int64?, string> dInst = new Dictionary<Int64?, string>();
                oCamposCat = obj.GetBizInstalaciones(_idcentro, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dInst.Add(convertir.toNInt64(item.IdInst), (string)item.Nombre);
                }

                cmbInstalacion.DataSource = dInst;
                cmbInstalacion.DataTextField = "Value";
                cmbInstalacion.DataValueField = "Key";
                cmbInstalacion.DataBind();

            }
            catch (SqlException ex)
            {
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando instalaciones", null);
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
            }
        }

        void cargarMovimientosByInst(Int64? _idInst)
        {
            List<capascccmex.metadatos.laboratorio> oCamposCat = new List<capascccmex.metadatos.laboratorio>();

            try
            {
              
                capascccmex.biz.laboratorio obj = new capascccmex.biz.laboratorio();

                oCamposCat = obj.GetBizLaboratorio(_idInst, 0, 0);
                //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("es-mx");
                Session["getCamposCatMovimientolab"] = oCamposCat;
                gridCapturas.DataSource = oCamposCat;
                gridCapturas.DataBind();
                gridCapturas.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando relación laboratorio", null);
            }
        }

        String eliminarCat()
        {
            String error = "F";
            String _idcampo = Session["getIdInstalacionGrid"].ToString().Trim().ToUpper();

            capascccmex.datos.laboratorio obj = new capascccmex.datos.laboratorio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidlaboratorio", _idcampo));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

       
        #endregion

        #region OTRAS OPERACIONES
        protected void gridCapturas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Int64? _idCampo = null;
              
            bool bit = true;

            if (e.CommandName == "pdfArchivos")
            {

                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("Idlaboratorio"));
                        loadarchivospdfs(_idCampo);
                        bit = true;
                        break;
                    }
                }
            }

            if (e.CommandName == "editGrid")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("Idlaboratorio"));
                        Session["getIdInstalacionGrid"] = _idCampo;
                        Session["getTipoOp"] = 2;
                        string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }

                }

            }

            if (e.CommandName == "delGrid")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = convertir.toNInt64(item.GetDataKeyValue("Idlaboratorio"));
                        Session["getIdInstalacionGrid"] = _idCampo;
                        string script = "function f(){callConfirm(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }

                }

            }

            if (e.CommandName == "addGrid")
            {
                if (Session["getIDInstalacion"] != null)
                {
                    Session["getTipoOp"] = 1;
                    string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                    windowManager1.RadAlert("Debe seleccionar la instalación del centro....", 300, 100, "Registros", null);

                bit = true;
            }
            if (bit == false)
                windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
        }


        void loadarchivospdfs(Int64? idReg)
        {
            string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert");
            string archivo = string.Format("{0}/{1}_LAB.pdf", path, idReg.ToString());            

            //List<string> mycollection = new List<string>();
            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string strUrlPage = "";// strUrl + @"filecert\1\698T0115424C_B.pdf";
            StringBuilder sbJs = new StringBuilder();

            if (File.Exists(archivo))
                {
                    strUrlPage = strUrl + string.Format(@"filecert/{0}_LAB.pdf", idReg.ToString());
                    sbJs.AppendLine("window.open('" + strUrlPage + "','_blank');");
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", sbJs.ToString(), true);
        }

        protected void gridCapturas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                List<capascccmex.metadatos.laboratorio> oCamposCat = new List<capascccmex.metadatos.laboratorio>();
                oCamposCat = (List<capascccmex.metadatos.laboratorio>)Session["getCamposCatMovimientolab"];
                gridCapturas.DataSource = oCamposCat;
            }
            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void cmbInstalacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbInstalacion.SelectedValue.Length > 0)
            {

                Int64? _idInst = convertir.toNInt64(cmbInstalacion.SelectedValue);
                Session["getNameInstalacion"] = cmbInstalacion.Text.ToString().Trim().ToUpper();
                Session["getIDInstalacion"] = _idInst;
                cargarMovimientosByInst(_idInst);
            }
        }
     
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            Int64? _idInst = convertir.toNInt64(Session["getIDInstalacion"]);
            //Int64? _idInst2 = convertir.toNInt64(Session["getIDInstalacion"]);
            if (e.Argument == "Rebind")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                cargarMovimientosByInst(_idInst);
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                gridCapturas.MasterTableView.CurrentPageIndex = gridCapturas.MasterTableView.PageCount - 1;
                cargarMovimientosByInst(_idInst);
            }

            if (e.Argument.ToString() == "oka")
            {
                string param = eliminarCat();
                string _error = Session["error_Reporte"].ToString();
                if (param.CompareTo("F") == 0 && _error.CompareTo("ok") == 1)
                {
                    windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Instrumento", null);
                }
                else
                {
                    deleteFile();
                    windowManager1.RadAlert("Instrumento eliminado con éxito...", 450, 200, "Eliminando Instrumento", null);
                    gridCapturas.MasterTableView.SortExpressions.Clear();
                    gridCapturas.MasterTableView.GroupByExpressions.Clear();
                    cargarMovimientosByInst(_idInst);
                }
            }
        }

        void deleteFile()
        {
            string newfilename = Session["getIdInstalacionGrid"].ToString() + "_LAB.pdf";
          string pathDir="~/filecert";
          if (System.IO.File.Exists(Path.Combine(Server.MapPath(pathDir), newfilename)))
              System.IO.File.Delete(Path.Combine(Server.MapPath(pathDir), newfilename));


        }


        #endregion
    }
}