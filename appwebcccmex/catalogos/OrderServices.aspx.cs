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
using Ionic.Zip;
using System.Text;

namespace appwebcccmex.catalogos
{
    public partial class OrderServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                    cargarMovimientos();
                }
                else Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region METODOS DE BDS

        void cargarMovimientos()
        {
            List<capascccmex.metadatos.orden_servicio> oCamposCat = new List<capascccmex.metadatos.orden_servicio>();
            capascccmex.biz.orden_servicio obj = new capascccmex.biz.orden_servicio();

            try
            {
                oCamposCat = obj.GetBizOrdenServicio();
                Session["getCamposCatOrdenServicio"] = oCamposCat;
                gridCapturas.DataSource = oCamposCat.OrderByDescending(x => x.Fecha); ;
                gridCapturas.DataBind();
                gridCapturas.Rebind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando orden servicio", null);
            }
        }

        String eliminarCat()
        {
            String error = "F";
            Int32 _idcampo = Convert.ToInt32(Session["getIdOrdenServGrid"]);

            capascccmex.datos.orden_servicio obj = new capascccmex.datos.orden_servicio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("@vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@idorden", _idcampo));

            error = obj.eliminar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }
        #endregion

        #region EVENTOS


        protected void gridCapturas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Int32 _idCampo = 0;
            bool bit = false;
            if (e.CommandName == "addGrid")
            {
                Session["opOServ"] = 1;
                string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            else if (e.CommandName == "deleteGrid")
            {
                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = Convert.ToInt32(item.GetDataKeyValue("Idorden"));
                        Session["getIdOrdenServGrid"] = _idCampo;
                        string script = "function f(){callConfirm(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        bit = true;
                        break;
                    }

                }
                if (bit == false)
                    windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
            }
            else if (e.CommandName == "editGrid")
            {

                foreach (GridDataItem item in gridCapturas.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        _idCampo = Convert.ToInt32(item.GetDataKeyValue("Idorden"));

                        Session["opOServ"] = 2;
                        Session["getIdOrdenServGrid"] = _idCampo.ToString();

                        string script = "function f(){openRadWindow(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        bit = true;
                        break;
                    }
                }
                if (bit == false)
                    windowManager1.RadAlert("Debe seleccionar un registro....", 300, 100, "Registros", null);
            }
        }


        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            if (e.Argument == "Rebind")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                cargarMovimientos();
            }
            else if (e.Argument == "RebindAndNavigate")
            {
                gridCapturas.MasterTableView.SortExpressions.Clear();
                gridCapturas.MasterTableView.GroupByExpressions.Clear();
                gridCapturas.MasterTableView.CurrentPageIndex = gridCapturas.MasterTableView.PageCount - 1;
                cargarMovimientos();
            }

            if (e.Argument.ToString() == "oka")
            {
                string param = eliminarCat();
                string _error = Session["error_Reporte"].ToString();
                if (param.CompareTo("F") == 0 && _error.CompareTo("ok") == 1)
                {
                    windowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Eliminando Orden de Servicio", null);
                }
                else
                {
                    //_idEmpresa = Convert.ToInt64(Session["getIdEmpresaGrid"]);
                    windowManager1.RadAlert("Registro eliminado con éxito...", 450, 200, "Eliminando Orden de Servicio", null);
                    gridCapturas.MasterTableView.SortExpressions.Clear();
                    gridCapturas.MasterTableView.GroupByExpressions.Clear();
                    cargarMovimientos();
                }
            }
        }

        #endregion
    }
}