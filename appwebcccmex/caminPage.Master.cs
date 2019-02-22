using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace appwebcccmex
{
    public partial class caminPage : System.Web.UI.MasterPage
    {

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated && Session["getIdusuario"]!=null)
                {
                    RadMenu1.Items[6].Visible = false; //LogOut
                    try
                    {
                        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                        bool adm = Convert.ToBoolean(Session["prmAdmin"]);
                        bool pemex = Convert.ToBoolean(Session["prmPemex"]);

                        //Catalogos
                        RadMenu1.Items[1].Items[0].Visible = false; //centro
                        RadMenu1.Items[1].Items[1].Visible = false; //producto
                        RadMenu1.Items[1].Items[2].Visible = false; //servicio
                        RadMenu1.Items[1].Items[3].Visible = false; //barcos
                        RadMenu1.Items[1].Items[4].Visible = false; //orden de  servicio
                        RadMenu1.Items[1].Items[5].Visible = false; //equipo
                        RadMenu1.Items[1].Items[6].Visible = false; //eventos
                        RadMenu1.Items[1].Items[7].Visible = false; //linea transporte
                        RadMenu1.Items[1].Items[8].Visible = false; //tipo equipo
                        RadMenu1.Items[1].Items[9].Visible = false; //zona
                        RadMenu1.Items[1].Visible = false; //Catalogos

                        //Operaciones
                        RadMenu1.Items[2].Items[0].Items[0].Visible = false; //Centro de trabajo
                        RadMenu1.Items[2].Items[0].Items[1].Visible = false; //Laboratorio
                        RadMenu1.Items[2].Items[0].Items[2].Visible = false; //Situacion Operativa
                        RadMenu1.Items[2].Items[0].Visible = false; //Capturas
                        RadMenu1.Items[2].Items[1].Items[0].Visible = false; //Diagramas Centro
                        RadMenu1.Items[2].Items[1].Visible = false; //Diagramas
                        RadMenu1.Items[2].Visible = false; //Operaciones

                        //Turbosina
                        RadMenu1.Items[3].Items[0].Visible = false; //Certificación y calidad
                        RadMenu1.Items[3].Items[1].Visible = false; //Línea de transporte
                        RadMenu1.Items[3].Items[2].Visible = false; //Muellas
                        RadMenu1.Items[3].Items[3].Visible = false; //Tipo equipo
                        RadMenu1.Items[3].Items[4].Visible = false; //Buque Tanque
                        RadMenu1.Items[3].Items[5].Visible = false; //Graficas
                        RadMenu1.Items[3].Items[6].Visible = false; //Consultas
                        RadMenu1.Items[3].Visible = false; //turbosina

                        //Consultas
                        RadMenu1.Items[4].Items[0].Visible = false; //Centros
                        RadMenu1.Items[4].Items[0].Visible = false; //Subgerencia
                        RadMenu1.Items[4].Items[0].Visible = false; //Acumulado
                        RadMenu1.Items[4].Items[0].Visible = false; //Situacion operativa
                        RadMenu1.Items[4].Items[0].Visible = false; //Acumulado orden servicio
                        RadMenu1.Items[4].Visible = false; //Consultas

                        //Seguridad
                        RadMenu1.Items[5].Items[0].Visible = false; //Usuarios
                        RadMenu1.Items[5].Items[1].Visible = false; //Contraseña
                        RadMenu1.Items[5].Visible = false; //Seguridad
                        

                        if (adm == true)
                        {
                            //Catalogos
                            RadMenu1.Items[1].Items[0].Visible = true; //centro
                            RadMenu1.Items[1].Items[1].Visible = true; //producto
                            RadMenu1.Items[1].Items[2].Visible = true; //servicio
                            RadMenu1.Items[1].Items[3].Visible = true; //barcos
                            RadMenu1.Items[1].Items[4].Visible = true; //orden de  servicio
                            RadMenu1.Items[1].Items[5].Visible = true; //equipo
                            RadMenu1.Items[1].Items[6].Visible = true; //eventos
                            RadMenu1.Items[1].Items[7].Visible = true; //linea transporte
                            RadMenu1.Items[1].Items[8].Visible = true; //tipo equipo
                            RadMenu1.Items[1].Items[9].Visible = true; //zona
                            RadMenu1.Items[1].Visible = true; //Catalogos

                            //Operaciones
                            RadMenu1.Items[2].Items[0].Items[0].Visible = true; //Centro de trabajo
                            RadMenu1.Items[2].Items[0].Items[1].Visible = true; //Laboratorio
                            RadMenu1.Items[2].Items[0].Items[2].Visible = true; //Situacion Operativa
                            RadMenu1.Items[2].Items[0].Visible = true; //Capturas
                            RadMenu1.Items[2].Items[1].Items[0].Visible = true; //Diagramas Centro
                            RadMenu1.Items[2].Items[1].Visible = true; //Diagramas
                            RadMenu1.Items[2].Visible = true; //Operaciones

                            //Turbosina
                            RadMenu1.Items[3].Items[0].Visible = true; //Certificación y calidad
                            RadMenu1.Items[3].Items[1].Visible = true; //Línea de transporte
                            RadMenu1.Items[3].Items[2].Visible = true; //Muellas
                            RadMenu1.Items[3].Items[3].Visible = true; //Tipo equipo
                            RadMenu1.Items[3].Items[4].Visible = true; //Buque Tanque
                            RadMenu1.Items[3].Items[5].Visible = true; //Graficas
                            RadMenu1.Items[3].Items[6].Visible = true; //Consultas
                            RadMenu1.Items[3].Visible = true; //turbosina

                            //Consultas
                            RadMenu1.Items[4].Items[0].Visible = true; //Centros
                            RadMenu1.Items[4].Items[1].Visible = true; //Subgerencia
                            RadMenu1.Items[4].Items[2].Visible = true; //Acumulado
                            RadMenu1.Items[4].Items[3].Visible = true; //Situacion operativa
                            RadMenu1.Items[4].Items[4].Visible = true; //Acumulado orden servicio
                            RadMenu1.Items[4].Visible = true; //Consultas

                            //Seguridad
                            RadMenu1.Items[5].Items[0].Visible = true; //Usuarios
                            RadMenu1.Items[5].Items[1].Visible = true; //Contraseña
                            RadMenu1.Items[5].Visible = true; //Seguridad
                        }
                        else if (pemex == true)
                        {
                            RadMenu1.Items[4].Visible = true; //Consultas
                            RadMenu1.Items[4].Items[0].Visible = true; //Subgerencia
                            RadMenu1.Items[4].Items[1].Visible = true; //Subgerencia
                            RadMenu1.Items[4].Items[2].Visible = true; //Acumulado
                            RadMenu1.Items[4].Items[3].Visible = true; //Situacion operativa
                            RadMenu1.Items[4].Items[4].Visible = true; //Acumulado orden servicio

                            RadMenu1.Items[5].Visible = true; //Seguridad
                            RadMenu1.Items[5].Items[1].Visible = true; //Contraseña

                        }
                        else
                        {
                            //Operaciones
                            RadMenu1.Items[2].Items[0].Items[0].Visible = true; //Centro de trabajo
                            RadMenu1.Items[2].Items[0].Items[1].Visible = true; //Laboratorio
                            RadMenu1.Items[2].Items[0].Items[2].Visible = true; //Situacion Operativa
                            RadMenu1.Items[2].Items[0].Visible = true; //Capturas
                            RadMenu1.Items[2].Items[1].Items[0].Visible = true; //Diagramas Centro
                            RadMenu1.Items[2].Items[1].Visible = true; //Diagramas
                            RadMenu1.Items[2].Visible = true; //Operaciones


                            RadMenu1.Items[1].Items[5].Visible = true; //equipo
                            RadMenu1.Items[1].Items[6].Visible = true; //eventos
                            RadMenu1.Items[1].Visible = true; //Catalogos


                            RadMenu1.Items[5].Items[0].Visible = true; //Usuarios
                            RadMenu1.Items[5].Items[1].Visible = true; //Contraseña
                            RadMenu1.Items[5].Visible = true; //Seguridad

                        }


                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/Account/MigratedLogin.aspx");
                    }
                }
                else
                {
                    //Catalogos
                    RadMenu1.Items[1].Items[0].Visible = false; //centro
                    RadMenu1.Items[1].Items[1].Visible = false; //producto
                    RadMenu1.Items[1].Items[2].Visible = false; //servicio
                    RadMenu1.Items[1].Items[3].Visible = false; //barcos
                    RadMenu1.Items[1].Items[4].Visible = false; //orden de  servicio
                    RadMenu1.Items[1].Items[5].Visible = false; //equipo
                    RadMenu1.Items[1].Items[6].Visible = true; //eventos
                    RadMenu1.Items[1].Visible = false; //Catalogos

                    //Operaciones
                    RadMenu1.Items[2].Items[0].Items[0].Visible = false; //Centro de trabajo
                    RadMenu1.Items[2].Items[0].Items[1].Visible = false; //Laboratorio
                    RadMenu1.Items[2].Items[0].Items[2].Visible = false; //Situacion Operativa
                    RadMenu1.Items[2].Items[0].Visible = false; //Capturas
                    RadMenu1.Items[2].Items[1].Items[0].Visible = false; //Diagramas Centro
                    RadMenu1.Items[2].Items[1].Visible = false; //Diagramas
                    RadMenu1.Items[2].Visible = false; //Operaciones

                    //Turbosina
                    RadMenu1.Items[3].Items[0].Visible = false; //Certificación y calidad
                    RadMenu1.Items[3].Items[1].Visible = false; //Línea de transporte
                    RadMenu1.Items[3].Items[2].Visible = false; //Muellas
                    RadMenu1.Items[3].Items[3].Visible = false; //Tipo equipo
                    RadMenu1.Items[3].Items[4].Visible = false; //Buque Tanque
                    RadMenu1.Items[3].Items[5].Visible = false; //Graficas
                    RadMenu1.Items[3].Items[6].Visible = false; //Consultas
                    RadMenu1.Items[3].Visible = false; //turbosina

                    //Consultas
                    RadMenu1.Items[4].Items[0].Visible = false; //Centros
                    RadMenu1.Items[4].Items[0].Visible = false; //Subgerencia
                    RadMenu1.Items[4].Items[0].Visible = false; //Acumulado
                    RadMenu1.Items[4].Items[0].Visible = false; //Situacion operativa
                    RadMenu1.Items[4].Items[0].Visible = false; //Acumulado orden servicio
                    RadMenu1.Items[4].Visible = false; //Consultas

                    //Seguridad
                    RadMenu1.Items[5].Items[0].Visible = false; //Usuarios
                    RadMenu1.Items[5].Items[1].Visible = false; //Contraseña
                    RadMenu1.Items[5].Visible = false; //Seguridad

                    RadMenu1.Items[7].Visible = false; //LogIn
                }
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