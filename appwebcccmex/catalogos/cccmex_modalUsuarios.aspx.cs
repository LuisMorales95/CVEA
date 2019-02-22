using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;
using System.Linq;

namespace appwebcccmex.catalogos
{
    public partial class cccmex_modalUsuarios : System.Web.UI.Page
    {
        List<capascccmex.metadatos.usuarioweb> lstUsr = new List<capascccmex.metadatos.usuarioweb>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                    if (Session["opUsuarios"] != null)
                    {
                    //Session["opTipo"] = 1;
                    //lblCentroActual.Text = Session["getcentroactual"].ToString();
                    //lblidcentro.Text = Session["getidcentroactual"].ToString();
                    Int16 tipoOp = Convert.ToInt16(Session["opUsuarios"]);
                    if (tipoOp == 1)//alta
                    {
                        cmdedit.Text = "Agregar Registro";
                        if (Session["getNameCentro"] != null)
                            lblCentroActual.Text = Session["getNameCentro"].ToString();
                        else {
                            cmdedit.Enabled = false;
                            lblCentroActual.Text = "No encuentro el centro actual ...";// Session["getcentroactual"].ToString();
                        }
                    }
                    else if (tipoOp == 2)//actualiza
                    {
                        cmdedit.Text = "Actualizar Registro";
                        lstUsr = (List<capascccmex.metadatos.usuarioweb>)Session["getCamposUsuarios"];
                        cargarInfoUsuarios(lstUsr);
                    }

                    }
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }


        #region METODOS

        protected void cmdedit_Click(object sender, EventArgs e)
        {
            valResumen.ValidationGroup = "get";
            Page.Validate("get");
            if (Page.IsValid)
            {
                try
                {

                    Int16 tipoOp = Convert.ToInt16(Session["opUsuarios"]);
                    if (tipoOp == 1)//alta
                    {
                        String param = AgregarCat();
                        if (param.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Agregando usuarios", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Centro ingresado con éxito...", 300, 200, "Agregando usuarios", null);
                        }
                    }
                    else if (tipoOp == 2)//actualizar
                    {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando usuarios", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("usuarios actualizado con éxito...", 300, 200, "actualizando usuarios", null);
                        }
                    }

                    string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                catch (SqlException ex)
                {
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Usuarios", null);

                }
            }
            else
            {
                if (addUser.Text.Length > 0)
                    addUser.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else if (addPwd.Text.Length > 0)
                    addPwd.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else if (addNombre.Text.Length > 0)
                    addNombre.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                else
                {
                    addUser.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                    addPwd.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                    addNombre.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                }
            }
        }

        private void cargarInfoUsuarios(List<capascccmex.metadatos.usuarioweb> _lstUsr)
        {
            Int64? _idUsr = convertir.toNInt64(Session["getIdUsuarioGrid"]);
            lblCentroActual.Text = Session["getNameCentro"].ToString();
            var getUsr = from oUsr in _lstUsr
                         where oUsr.IappId == _idUsr
                         select oUsr;

            //lblEmpresa.Text = Session["getNombreEmpresa"].ToString();

            foreach (var iUsr in getUsr)
            {
                string pwd = utilerias.DecryptKey(iUsr.IappPwd.ToString().Trim());
                addUser.Text = iUsr.IappLogin.ToString().Trim();
                addNombre.Text = iUsr.IappNombre_Completo.ToString().ToUpper().Trim();
                addPwd.Text = pwd.ToString().Trim();
                addCorreo.Text = iUsr.IappCorreo.ToString().ToUpper().Trim();
                ckbActivo.Checked = convertir.toBoolean(iUsr.IappActivo);
                ckbAdmin.Checked = convertir.toBoolean(iUsr.IappAdmin);
                ckbPemex.Checked = convertir.toBoolean(iUsr.IappPemex);
                
            }

        }

       
        String AgregarCat()
        {
            String error = "F";
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentro"]);
            String _user =  addUser.Text.ToString().Trim().ToUpper();
            String _pwd = utilerias.EncryptKey(addPwd.Text.ToString().Trim());
            String _Nombre = addNombre.Text.ToString().Trim().ToUpper();
            String _correo = addCorreo.Text.ToString().Trim().ToUpper();

            bool _activo = Convert.ToBoolean(ckbActivo.Checked);
            bool _admin = Convert.ToBoolean(ckbAdmin.Checked);
            bool _pemex = Convert.ToBoolean(ckbPemex.Checked);



            capascccmex.datos.usuarioweb obj = new capascccmex.datos.usuarioweb();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidcentro", _idCentro));
            campos.Add(new SqlParameter("viapplogin", _user));
            campos.Add(new SqlParameter("viapppwd", _pwd));
            campos.Add(new SqlParameter("viappnombre_completo", _Nombre));
            campos.Add(new SqlParameter("viappcorreo", _correo));
            campos.Add(new SqlParameter("viappactivo", _activo));
            campos.Add(new SqlParameter("viappadmin", _admin));
            campos.Add(new SqlParameter("viapppemex", _pemex));

            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        String actualizaCat()
        {
            String error = "F";

            Int64? _idUsr = convertir.toNInt64(Session["getIdUsuarioGrid"]);
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentro"]);
            String _user = addUser.Text.ToString().Trim().ToUpper();
            String _pwd = utilerias.EncryptKey(addPwd.Text.ToString().Trim());
            String _Nombre = addNombre.Text.ToString().Trim().ToUpper();
            String _correo = addCorreo.Text.ToString().Trim().ToUpper();

            bool _activo = Convert.ToBoolean(ckbActivo.Checked);
            bool _admin = Convert.ToBoolean(ckbAdmin.Checked);
            bool _pemex = Convert.ToBoolean(ckbPemex.Checked);



            capascccmex.datos.usuarioweb obj = new capascccmex.datos.usuarioweb();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("viappid", _idUsr));
            campos.Add(new SqlParameter("vidcentro", _idCentro));
            campos.Add(new SqlParameter("viapplogin", _user));
            campos.Add(new SqlParameter("viapppwd", _pwd));
            campos.Add(new SqlParameter("viappnombre_completo", _Nombre));
            campos.Add(new SqlParameter("viappcorreo", _correo));
            campos.Add(new SqlParameter("viappactivo", _activo));
            campos.Add(new SqlParameter("viappadmin", _admin));
            campos.Add(new SqlParameter("viapppemex", _pemex));

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }
        #endregion

      
    }
}