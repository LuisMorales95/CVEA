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

namespace appwebcccmex
{
    public partial class modal_cccmex_capturabylaboratorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));


                    nombreCentro.Text = Session["getNameInstalacion"].ToString();
                    cargarpruebas();
                    inicializa();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }


         #region EVENTOS DE OPERACIONES
        void inicializa()
        {
            if (Session["getTipoOp"] != null)
            {
                Int16 tipoOp = Convert.ToInt16(Session["getTipoOp"]);
                if (tipoOp == 1)//alta
                {
                   cmdEjecuta.Text = "Agregar Registro";
                   lblfile.Visible = false;
                   fileLaboratorio.Visible = false;
                }
                else if (tipoOp == 2)//actualiza
                {
                    cmdEjecuta.Text = "Actualizar Archivo";
                    cargarMovimientosByInst();
                    lblfile.Visible = true;
                    fileLaboratorio.Visible = true;
                }
                //else
                //    Response.Redirect("~/Account/outSession.aspx");

            }
        }
        void cargarMovimientosByInst()
        {
            List<capascccmex.metadatos.laboratorio> oCamposCat = new List<capascccmex.metadatos.laboratorio>();

            try
            {
                Int64? _idInst = convertir.toNInt64(Session["getIdInstalacionGrid"]);

                capascccmex.biz.laboratorio obj = new capascccmex.biz.laboratorio();
                oCamposCat = (List<capascccmex.metadatos.laboratorio>)Session["getCamposCatMovimientolab"];
                foreach (var ilab in oCamposCat.Where(x => x.Idlaboratorio == _idInst))
                {
                    cmbpruebas.SelectedValue = ilab.Idprueba.ToString();
                    addmetodo.Text = ilab.Metodo_astm.ToString();
                    adddistemp.Text = ilab.Dispositivo_temp.ToString();
                    addninfo1.Text = ilab.No_inf_calibr_temp.ToString();
                    addhidro.Text = ilab.Hidrometro.ToString();
                    addninfo2.Text = ilab.No_inf_calibr_hid.ToString();
                    addprobeta.Text = ilab.Probeta.ToString();
                    addninfo3.Text = ilab.No_inf_calibr_prob.ToString();
                    addequipoana.Text = ilab.Equipo_analisis.ToString();
                    addmodmarca.Text = ilab.Modelo_marca.ToString();
                    if (ilab.Fecha_calibr_mantto != null)
                    rdpFechaMantto.SelectedDate = Convert.ToDateTime(ilab.Fecha_calibr_mantto);
                    addninfo4.Text = ilab.No_inf_calibr_equipo.ToString();
                    addestandarutil.Text = ilab.Estandar_verif_util.ToString();
                    if (ilab.Fecha_vig_estandar != null)
                    rdpFechaVig.SelectedDate = Convert.ToDateTime(ilab.Fecha_vig_estandar);
                    addporomemb.Text = ilab.Medidor_poro_memb.ToString();
                    addinfocalibrbal.Text = ilab.Inf_calibr_bal_analitica.ToString();
                    addinfocalibrcannon.Text = ilab.Inf_calibr_tubo_cann.ToString();
          
                }

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando instrumento laboratorio", null);
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
                    Int16 tipoOp = Convert.ToInt16(Session["getTipoOp"]);
                    if (tipoOp == 2)//edita
                    {
                        String param = actualizaCat();
                        if (param.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Edita laboratorio", null);
                        }
                        else
                        {
                            //Subir archivo...
                            if (fileLaboratorio.UploadedFiles.Count > 0)
                            {
                                bool bit = subirArchivoLab();

                                if (bit == false)                                  
                                    RadWindowManager1.RadAlert("Error: Al intentar subir el archivo, verifique con el administrador ...", 300, 200, "Edita laboratorio", null);
                            }
                           
                            string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        }
                    else if (tipoOp == 1)//agregar
                    {

                        String param = AgregarCat();                       
                        if (param.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Captura laboratorio", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Captura ingresada con éxito...", 300, 200, "Captura laboratorio", null);
                        }

                        string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }

                }
                catch (SqlException ex)
                {
                    convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Captura laboratorio", null);

                }
            }
            else
            {
                #region parametrosColor

                if (cmbpruebas.SelectedValue != null && cmbpruebas.SelectedValue.Length > 0)
                    cmbpruebas.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                //cmbinstalacion.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    cmbpruebas.BackColor = System.Drawing.ColorTranslator.FromHtml("#b94a48");
                // cmbinstalacion.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

               
                #endregion
                RadWindowManager1.RadAlert("Existen registros obligatorios, favor de corregir ", 300, 100, "Captura laboratorio", null);
            }
        }

        void cargarpruebas()
        {
            List<capascccmex.metadatos.pruebas> oCamposCat = new List<capascccmex.metadatos.pruebas>();
            capascccmex.biz.pruebas obj = new capascccmex.biz.pruebas();

           
            try
            {


                Dictionary<Int64?, string> dInst = new Dictionary<Int64?, string>();
                oCamposCat = obj.GetBizPruebas();
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dInst.Add(convertir.toNInt64(item.Idpruebas), (string)item.Pruebas);
                }

                cmbpruebas.DataSource = dInst;
                cmbpruebas.DataTextField = "Value";
                cmbpruebas.DataValueField = "Key";
                cmbpruebas.DataBind();

            }
            catch (SqlException ex)
            {
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando pruebas", null);
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
            }
        }

        String AgregarCat()
        {
            String error = "F";

            Int64? idprueba = convertir.toNInt64(cmbpruebas.SelectedValue);
            Int64? idinstal = convertir.toNInt64(Session["getIDInstalacion"]);

            DateTime? f1 = rdpFechaMantto.SelectedDate.HasValue == true ? convertir.toNDateTime(rdpFechaMantto.SelectedDate.Value) : null;
            DateTime? f2 = rdpFechaVig.SelectedDate.HasValue == true ? convertir.toNDateTime(rdpFechaVig.SelectedDate.Value) : null;
            //DateTime? f1 = rdpFechaMantto.SelectedDate.Value;
            //DateTime? f2 = rdpFechaVig.SelectedDate.Value;

            capascccmex.datos.laboratorio obj = new capascccmex.datos.laboratorio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidinst", idinstal));
            campos.Add(new SqlParameter("vidprueba", idprueba));

            campos.Add(new SqlParameter("vmetodo_astm",addmetodo.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vdispositivo_temp", adddistemp.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vno_inf_calibr_temp", addninfo1.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vhidrometro", addhidro.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vno_inf_calibr_hidr", addninfo2.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vprobeta", addprobeta.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vno_inf_calibr_prob", addninfo3.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vequipo_analisis", addequipoana.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vmodelo_marca", addmodmarca.Text.Trim().ToString()));

            campos.Add(new SqlParameter("vfecha_calibr_mantto", f1));
            campos.Add(new SqlParameter("vno_inf_calibr_equipo", addninfo4.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vestandar_verif_util", addestandarutil.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vfecha_vig_estandar", f2));
            campos.Add(new SqlParameter("vmedidor_poro_memb", addporomemb.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vinf_calibr_bal_analitica", addinfocalibrbal.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vinf_calibr_tubo_cann", addinfocalibrcannon.Text.Trim().ToString()));
           


            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        bool subirArchivoLab()
        {
            bool bit = false;
            string pathDirectoryPadre = "";
            string newfilename = Session["getIdInstalacionGrid"]+"_LAB.pdf";
           
                //Verificar la subida de archivos
                deleteFile();
                pathDirectoryPadre = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") ;

                if (!Directory.Exists(pathDirectoryPadre))
                {
                    System.IO.Directory.CreateDirectory(pathDirectoryPadre.ToString());
                }
                foreach (Telerik.Web.UI.UploadedFile f in fileLaboratorio.UploadedFiles)
                {   
                    fileLaboratorio.TargetFolder = "~/filecert";                
                    if (!File.Exists(Path.Combine(Server.MapPath(fileLaboratorio.TargetFolder), newfilename)))
                    {
                        f.SaveAs(Path.Combine(Server.MapPath(fileLaboratorio.TargetFolder), newfilename), true);
                        bit = true;
                    }                
                }
                return bit;
        }

        void deleteFile()
        {
            string newfilename = Session["getIdInstalacionGrid"] + "_LAB.pdf";           
               fileLaboratorio.TargetFolder = "~/filecert";
               if (System.IO.File.Exists(Path.Combine(Server.MapPath(fileLaboratorio.TargetFolder), newfilename)))
                   System.IO.File.Delete(Path.Combine(Server.MapPath(fileLaboratorio.TargetFolder), newfilename));
               
            
        }

        String actualizaCat()
        {
            String error = "F";

            Int64? _idLab = convertir.toNInt64(Session["getIdInstalacionGrid"]);
            Int64? idprueba = convertir.toNInt64(cmbpruebas.SelectedValue);
            Int64? idinstal = convertir.toNInt64(Session["getIDInstalacion"]);

            DateTime? f1 = rdpFechaMantto.SelectedDate.HasValue == true ? convertir.toNDateTime(rdpFechaMantto.SelectedDate.Value) : null;
            DateTime? f2 = rdpFechaVig.SelectedDate.HasValue == true ? convertir.toNDateTime(rdpFechaVig.SelectedDate.Value) : null;
            //DateTime? f1 = rdpFechaMantto.SelectedDate.Value;
            //DateTime? f2 = rdpFechaVig.SelectedDate.Value;

            capascccmex.datos.laboratorio obj = new capascccmex.datos.laboratorio();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidlaboratorio", _idLab));
            campos.Add(new SqlParameter("vidinst", idinstal));
            campos.Add(new SqlParameter("vidprueba", idprueba));

            campos.Add(new SqlParameter("vmetodo_astm", addmetodo.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vdispositivo_temp", adddistemp.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vno_inf_calibr_temp", addninfo1.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vhidrometro", addhidro.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vno_inf_calibr_hidr", addninfo2.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vprobeta", addprobeta.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vno_inf_calibr_prob", addninfo3.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vequipo_analisis", addequipoana.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vmodelo_marca", addmodmarca.Text.Trim().ToString()));

            campos.Add(new SqlParameter("vfecha_calibr_mantto", f1));
            campos.Add(new SqlParameter("vno_inf_calibr_equipo", addninfo4.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vestandar_verif_util", addestandarutil.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vfecha_vig_estandar", f2));
            campos.Add(new SqlParameter("vmedidor_poro_memb", addporomemb.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vinf_calibr_bal_analitica", addinfocalibrbal.Text.Trim().ToString()));
            campos.Add(new SqlParameter("vinf_calibr_tubo_cann", addinfocalibrcannon.Text.Trim().ToString()));

            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        #endregion

        

    }
}