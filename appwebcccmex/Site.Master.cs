using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace appwebcccmex
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        //private string _antiXsrfTokenValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    try
                    {
                        RadRibbonBar1.Visible = true;
                        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                        //if (Session["getIdCentroUsr"] == null)
                        //    Response.Redirect("~/Account/outSession.aspx");

                        bool adm = Convert.ToBoolean(Session["prmAdmin"]);
                        bool pemex = Convert.ToBoolean(Session["prmPemex"]);
                        //lblFooter.Text = Session["userNameApp"].ToString().ToUpper() + " | " + @"© 2014 CCC - MEX".ToUpper() + " | " + DateTime.Now.ToString("dd-MMMM-yyyy").ToUpper();

                        //foreach (Telerik.Web.UI.RadToolBarButton btn in mainToolBar.Items)
                        //{
                        //    //btn.Value
                        //    if (btn.Text == "Save Filter")
                        //    {
                        //        btn.Visible = false;
                        //    }
                        //}

                        //operaciones
                        RadRibbonBar1.Tabs[0].Groups[0].Items[0].Visible = false;//captura
                        RadRibbonBar1.Tabs[0].Groups[0].Items[1].Visible = false;//laboratorio
                        RadRibbonBar1.Tabs[0].Groups[0].Items[2].Visible = false;//situación op.

                        //Consulta
                        RadRibbonBar1.Tabs[0].Groups[1].Items[0].Visible = false;//Centros
                        RadRibbonBar1.Tabs[0].Groups[1].Items[1].Visible = false;//Pemex
                        RadRibbonBar1.Tabs[0].Groups[1].Items[2].Visible = false;//Acumulado
                        RadRibbonBar1.Tabs[0].Groups[1].Items[3].Visible = false;//situación exp
                        RadRibbonBar1.Tabs[0].Groups[1].Items[4].Visible = false;//Acumulado orden servicio

                        //Diagramas
                        RadRibbonBar1.Tabs[0].Groups[2].Items[0].Visible = false;//Diagrama centros
                        

                        //Catalogos
                        RadRibbonBar1.Tabs[1].Groups[0].Items[0].Visible = false;//Centro**
                        RadRibbonBar1.Tabs[1].Groups[0].Items[1].Visible = false;//Producto
                        RadRibbonBar1.Tabs[1].Groups[0].Items[2].Visible = false;//Servicios**
                        RadRibbonBar1.Tabs[1].Groups[0].Items[3].Visible = false;//Barco**
                        RadRibbonBar1.Tabs[1].Groups[0].Items[4].Visible = false;//Acumulado**
                        RadRibbonBar1.Tabs[1].Groups[0].Items[5].Visible = false;
                        RadRibbonBar1.Tabs[1].Groups[0].Items[6].Visible = false;
                        //Seguridad
                        RadRibbonBar1.Tabs[2].Groups[0].Items[0].Visible = false;//Agregar**
                        RadRibbonBar1.Tabs[2].Groups[0].Items[1].Visible = false;//Contraseña**

                        if (adm == true)
                        {
                            //operaciones
                            RadRibbonBar1.Tabs[0].Groups[0].Items[0].Visible = true;//captura
                            RadRibbonBar1.Tabs[0].Groups[0].Items[1].Visible = true;//laboratorio
                            RadRibbonBar1.Tabs[0].Groups[0].Items[2].Visible = true;//situación op.
                            //Consulta
                            RadRibbonBar1.Tabs[0].Groups[1].Items[0].Visible = true;//Centros
                            RadRibbonBar1.Tabs[0].Groups[1].Items[1].Visible = true;//Pemex
                            RadRibbonBar1.Tabs[0].Groups[1].Items[2].Visible = true;//Acumulado
                            RadRibbonBar1.Tabs[0].Groups[1].Items[3].Visible = true;//situación exp
                            RadRibbonBar1.Tabs[0].Groups[1].Items[4].Visible = true;//Acumulado orden servicio
                            
                            //Diagramas
                            RadRibbonBar1.Tabs[0].Groups[2].Items[0].Visible = true;//Diagrama centros
                            
                            //Catalogos
                            RadRibbonBar1.Tabs[1].Groups[0].Items[0].Visible = true;//Centro**
                            RadRibbonBar1.Tabs[1].Groups[0].Items[1].Visible = true;//Producto
                            RadRibbonBar1.Tabs[1].Groups[0].Items[2].Visible = true;//Servicios**
                            RadRibbonBar1.Tabs[1].Groups[0].Items[3].Visible = true;//Barco**
                            RadRibbonBar1.Tabs[1].Groups[0].Items[4].Visible = true;//Acumulado**
                            RadRibbonBar1.Tabs[1].Groups[0].Items[5].Visible = true;//Equipos**
                            RadRibbonBar1.Tabs[1].Groups[0].Items[6].Visible = true;//eventos**
                            //Seguridad
                            RadRibbonBar1.Tabs[2].Groups[0].Items[0].Visible = true;//Agregar**
                            RadRibbonBar1.Tabs[2].Groups[0].Items[1].Visible = true;//Contraseña**
                        }
                        else if (pemex == true)
                        {
                            RadRibbonBar1.Tabs[1].Visible = false;    //Catalogos
                            RadRibbonBar1.Tabs[0].Groups[1].Items[0].Visible = false;//Centros
                            RadRibbonBar1.Tabs[0].Groups[0].Visible = false;//captura

                            RadRibbonBar1.Tabs[0].Groups[1].Items[1].Visible = true;//Pemex
                            RadRibbonBar1.Tabs[0].Groups[1].Items[2].Visible = true;//Acumulado
                            RadRibbonBar1.Tabs[0].Groups[1].Items[3].Visible = true;//situación exp
                            RadRibbonBar1.Tabs[0].Groups[1].Items[4].Visible = true;//Acumulado orden servicio
                            
                            RadRibbonBar1.Tabs[2].Groups[0].Items[1].Visible = true;//Contraseña**
                            RadRibbonBar1.Tabs[0].Groups[2].Items[0].Visible = true;//Diagrama centros

                            //RadRibbonBar1.Tabs[2].Groups[0].Items[0].Visible = false;//Agregar**
                            //RadRibbonBar1.Tabs[1].Visible = false;
                            //RadRibbonBar1.Tabs[0].Groups[1].Visible = false;
                            //RadRibbonBar1.Tabs[0].Groups[2].Visible = false;
                        }
                        else
                        {
                            //operaciones
                            RadRibbonBar1.Tabs[0].Groups[0].Items[0].Visible = true;//captura
                            RadRibbonBar1.Tabs[0].Groups[0].Items[1].Visible = true;//laboratorio
                            RadRibbonBar1.Tabs[0].Groups[0].Items[2].Visible = true;//situación op.
                            RadRibbonBar1.Tabs[1].Groups[0].Items[5].Visible = true;//Equipos**
                            RadRibbonBar1.Tabs[1].Groups[0].Items[6].Visible = true;//eventos**
                            RadRibbonBar1.Tabs[2].Groups[0].Items[1].Visible = true;//Contraseña**  

                            RadRibbonBar1.Tabs[2].Groups[0].Items[0].Visible = false;//Agregar**
                            
                            RadRibbonBar1.Tabs[0].Groups[1].Visible = false;
                            RadRibbonBar1.Tabs[0].Groups[2].Visible = true;

                            RadRibbonBar1.Tabs[0].Groups[2].Items[0].Visible = true;//Diagrama centros
                        }


                    }
                    catch (Exception)
                    {
                        //Response.Redirect("~/Account/login.aspx");
                    }
                }
                else
                    RadRibbonBar1.Visible = false;

            }

        }

     

        protected void RadRibbonBar1_ButtonClick(object sender, Telerik.Web.UI.RibbonBarButtonClickEventArgs e)
        {
            string urlCatalogo = e.Button.Value;
            string txtBtn = e.Button.Text.ToString().ToUpper().Trim();
            Response.Redirect(urlCatalogo);

        }
    }
}