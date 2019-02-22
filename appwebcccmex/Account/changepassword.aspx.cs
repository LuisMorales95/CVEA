using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;
using System.Linq;

namespace appwebcccmex.Account
{
    public partial class changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                lblid.Text = Session["userNameApp"].ToString();
            }

            else
                Response.Redirect("~/Account/outSession.aspx");
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string pass = CurrentPassword.Text.ToString().Trim();
                string passold = Session["userPassApp"].ToString();

                string newpass = NewPassword.Text.ToString().Trim();

                if (pass.CompareTo(passold) == 0)
                {
                    String param = actualizaCat(newpass);

                    if (param.CompareTo("F") == 0)
                    {
                        windowManager1.RadAlert("Error:" + Session["error_Reporte"].ToString(), 250, 150, "Cambio de contraseña", null);
                    }
                    else
                    {
                        windowManager1.RadAlert("Contraseña actualiza con éxito", 250, 150, "Cambio de contraseña", null);
                        Response.Redirect("~/Account/outSession.aspx");
                    }

                }
                else
                {
                    windowManager1.RadAlert("La contraña no coincide con la anterior ..., favor de verificar...", 250, 150, "Cambio de contraseña", null);
                }

            }
        }

        String actualizaCat(string pass)
        {
            String error = "F";

            Int64? _idUsr = convertir.toNInt64(Session["getIdusuario"]);

            capascccmex.datos.usuarioweb obj = new capascccmex.datos.usuarioweb();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("viappid", _idUsr));
            campos.Add(new SqlParameter("viapppwd", capascccmex.utilerias.EncryptKey(pass)));

            error = obj.actualizarpass(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }
    }
}