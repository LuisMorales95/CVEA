using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using capascccmex;
using System.Linq;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace appwebcccmex.Account
{
    public partial class MigratedLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!Context.User.Identity.IsAuthenticated)
                {
                    //bool bit = enviarEmail("Correo desde api", "Hola api");
                    //string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                    //string strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                    //string strUrl1 = strUrl + @"filecert\1\698T0115424C_B.pdf";
                    //string strUrl2 = strUrl + @"filecert\1\698T0115424C_A.pdf";
                    ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrl1 + "','_blank')", true);
                    ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrl2 + "','_blank')", true);
                    ////Response.Write("<script type='text/javascript'>window.open('UsageInformation.aspx?arg=" + e.CommandArgument + "','_blank');</script>");
                    //Response.Redirect(strUrl1);
                    //Response.Redirect(strUrl2);
                }
            }
            //RegisterHyperLink.NavigateUrl = "Register";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        protected void cmdSesion_Click(object sender, EventArgs e)
        {
            valResumen.ValidationGroup = "add";
            Page.Validate("add");
            if (Page.IsValid)
            {
                string _UsrName = login1.UserName.ToString().Trim();
                string _UsrPwd = login1.Password.ToString().Trim();

                //convertir.log("usr: " + _UsrName + ", pwd: " + _UsrPwd);

                if (ValidateUser(_UsrName, _UsrPwd))
                {



                    FormsAuthentication.SetAuthCookie(_UsrName, true);
                    Response.Redirect("~/muestrapag.aspx");

                    //bool act = Convert.ToBoolean(Session["prmActivo"]);
                    //if (act)

                    //FormsAuthentication.RedirectFromLoginPage(_UsrName, convertir.toBoolean(login1.RememberMeSet));
                    //else
                    //    windowManager1.RadAlert("El usuario no esta activo, verificar con el adminitrador", 450, 200, "Login", null);
                }
                else
                {
                    windowManager1.RadAlert("Usuario o contraseña no es valido, favor de verificar...", 450, 200, "Login", null);
                    login1.UserName = "";
                    //login1.Password = "";
                    //login1.UserName.Focus();
                }
            }
        }

        private bool ValidateUser(string userName, string passWord)
        {
            string lookupPassword = null;
            // Buscar nombre de usuario no válido.
            // el nombre de usuario no debe ser un valor nulo y debe tener entre 1 y 15 caracteres.
            if ((null == userName) || (0 == userName.Length))//|| (userName.Length > 15)
            {
                convertir.log("[ValidateUser] Input validation of userName failed.");
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                return false;
            }
            try
            {
                //Va el cuerpo del mensaje ...

                List<capascccmex.metadatos.usuarioweb> oCamposUsuarios = new List<capascccmex.metadatos.usuarioweb>();

                capascccmex.biz.usuarioweb obj = new capascccmex.biz.usuarioweb();

                //campos = getUsuarios(userName.ToUpper().Trim().ToString(), utilerias.EncryptKey(passWord.Trim().ToString()));
                lookupPassword = "null";
                //addUser.Text = ficha.ToString();
                oCamposUsuarios = obj.GetBizUsuariosByLogin(userName, 0, 0);

                //windowManager1.RadAlert("Error: " + obj.ErrorRegistros().ToString() + ", campos: " + oCamposUsuarios.Count.ToString(), 450, 200, "Login", null);
                foreach (var item in oCamposUsuarios)
                {
                    Session["getIdusuario"] = item.IappId.ToString().Trim();
                    Session["userNameApp"] = item.IappNombre_Completo.ToString().ToUpper().Trim();
                    lookupPassword = capascccmex.utilerias.DecryptKey(item.IappPwd.ToString().Trim());
                    Session["userPassApp"] = capascccmex.utilerias.DecryptKey(item.IappPwd.ToString().Trim());

                    Session["prmAdmin"] = item.IappAdmin; //administrador
                    Session["prmActivo"] = item.IappActivo; //Usuarios de campo
                    Session["prmPemex"] = item.IappPemex; //administrador
                    Session["getIdCentroUsr"] = item.IdCentro.ToString(); //centro que le corresponde
                    Session["nameCentroActual"] = item.Nombre_Centro.ToString().Trim().ToUpper();
                    //iAppChange
                }

            }
            catch (Exception ex)
            {
                // Agregar aquí un control de errores para la depuración.
                // Este mensaje de error no debería reenviarse al que realiza la llamada.
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
                windowManager1.RadAlert("Error: " + ex.Message.ToString(), 450, 200, "Login", null);
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
            }

            // Si no se encuentra la contraseña, devuelve false.
            if (null == lookupPassword)
            {
                // Para más seguridad, puede escribir aquí los intentos de inicio de sesión con error para el registro de eventos.
                return false;
            }

            // Comparar lookupPassword e introduzca passWord, usando una comparación que distinga mayúsculas y minúsculas.
            //Session["userPassApp"] = "jmatias.2014*".ToString();
            //Session["userFeApp"] = "jmatias".ToString();
            //Session["prmAdmin"] = true;
            //Session["prmActivo"] = true;
            //Session["prmMultiEmpresa"] = true;
            //Session["userNameApp"] = "J. Abrhan Antonio Matías".ToString();
            //Session["prmIdEmpresa"] = 1;
            //Session["prmNameEmpresa"] = "Empresa debug".ToString();
            //return true;
            return (0 == string.Compare(lookupPassword, passWord, false));

        }

        private Boolean enviarEmail(string titulo, string contenido)
        {
            Boolean estado = false;
            MailMessage correo = new MailMessage();
            try
            {
                String EmailCccmex = System.Configuration.ConfigurationManager.AppSettings["EmailCccmex"];
                String pwd = System.Configuration.ConfigurationManager.AppSettings["Emailpwd"];
                String EmailSmtp = System.Configuration.ConfigurationManager.AppSettings["EmailSmtp"];
                int puerto = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                string emailCoordinador = System.Configuration.ConfigurationManager.AppSettings["coordinador"];
                Boolean vSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ssl"]);
                correo.From = new MailAddress(EmailCccmex);
                correo.To.Add(emailCoordinador);
                //correo.CC.Add(emailCoordinador);
                correo.Subject = titulo;
                correo.Body = contenido;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient objsmtp = new SmtpClient(EmailSmtp, puerto);
                //objsmtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //objsmtp.UseDefaultCredentials = false;
                objsmtp.EnableSsl = vSsl;
                objsmtp.Credentials = new NetworkCredential(EmailCccmex, pwd);

                objsmtp.Host = EmailSmtp;
                objsmtp.Port = puerto;

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);


                //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate,X509Chain chain, SslPolicyErrors sslPolicyErrors)
                //{ return true; };

                objsmtp.Send(correo);
                estado = true;
            }
            catch (Exception ex)
            {
                string xy = ex.Message.ToString();
                estado = false;
                //throw;
            }
            correo.Dispose();
            return estado;
        }




        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}