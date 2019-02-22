using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace appCfdiWeb.Account
{
    public partial class outSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Session.Clear();
                    FormsAuthentication.SignOut();
                    //se redirecciona al usuario a la pagina de login
                    //Response.Redirect(Request.UrlReferrer.ToString());
                    Response.Redirect("~/Account/MigratedLogin.aspx");
                }
                else
                    Response.Redirect("~/Account/MigratedLogin.aspx");
            }
        }
    }
}