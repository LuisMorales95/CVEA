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
    public partial class modal_cccmex_capturabycentro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
                    //Session["nameCentroActual"] = "Pajaritos";
                    //Session["getIdCentroUsr"] = 1;
                    //Session["getIdusuario"] = 1;
                    Session["letra"] = "X";
                    //lblCentroActual.Text = Session["getcentroactual"].ToString();
                    //lblidcentro.Text = Session["getidcentroactual"].ToString();
                    //cargarcentros();
                    inicializa();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
          
        }

        #region Metodos de operacion
        void inicializa()
        {
            int _idCentro = convertir.toInt32(Session["getIdCentroUsr"]);
            cargarcentrosByInstalaciones(_idCentro);
            cargarBarco();
            cargarProducto();
            cargarServicio();

            nombreCentro.Text = Session["nameCentroActual"].ToString().Trim().ToUpper();
            addloteturbo.Visible = false;
            addpropileno.Visible = false;
            rdpFecha.SelectedDate = DateTime.Now;
            rAsyncPDF.Enabled = false;
            filecalidad.Enabled = false;
            cmbbarco.Enabled = false;
            //addanio.Text = rdpFecha.SelectedDate.Value.Year.ToString();
            //addmes.Text = rdpFecha.SelectedDate.Value.Month.ToString();
        }

        void cargarBarco()
        {
            List<capascccmex.metadatos.barco> oCamposCat = new List<capascccmex.metadatos.barco>();
            capascccmex.biz.barco obj = new capascccmex.biz.barco();

            try
            {
                Dictionary<Int64?, string> dictionarycat = new Dictionary<Int64?, string>();
                oCamposCat = obj.GetBizBarco(null, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dictionarycat.Add(convertir.toNInt64(item.IdBarco), (string)item.Barco);
                }

                cmbbarco.DataSource = dictionarycat;
               cmbbarco.DataTextField = "Value";
               cmbbarco.DataValueField = "Key";
               cmbbarco.DataBind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de barco", null);
            }
        }

        void cargarProducto()
        {
            List<capascccmex.metadatos.producto> oCamposCat = new List<capascccmex.metadatos.producto>();
            capascccmex.biz.producto obj = new capascccmex.biz.producto();

            try
            {
                Dictionary<Int64?, string> dictionarycat1 = new Dictionary<Int64?, string>();
                oCamposCat = obj.GetBizProducto(null, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dictionarycat1.Add(convertir.toNInt64(item.IdProducto), (string)item.Producto);
                }

                cmbproducto.DataSource = dictionarycat1;
               cmbproducto.DataTextField = "Value";
               cmbproducto.DataValueField = "Key";
               cmbproducto.DataBind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de productos", null);
            }
        }

        void cargarServicio()
        {
           
            List<capascccmex.metadatos.servicio> oCamposCat = new List<capascccmex.metadatos.servicio>();
            capascccmex.biz.servicio obj = new capascccmex.biz.servicio();

            try
            {
                Dictionary<string, string> dictionarycat2 = new Dictionary<string, string>();
                oCamposCat = obj.GetBizServicio(null, 0, 0);
                foreach (var item in oCamposCat)
                {
                    dictionarycat2.Add(item.IdServicio.ToString().Trim(), item.Servicio.ToString().Trim());
                }
                cmbservicio.DataSource = dictionarycat2;
                cmbservicio.DataTextField = "Value";
                cmbservicio.DataValueField = "Key";
                cmbservicio.DataBind();
                //----------------------------------------

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando información de Servicio", null);
            }
        }
        void cargarcentrosByInstalaciones(int _idcentro)
        {
            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();

            try
            {


                Dictionary<int, string> dictionarycat3 = new Dictionary<int, string>();
                oCamposCat = obj.GetBizInstalaciones(_idcentro, 0, 0);
                //----------------------------------------
                foreach (var item in oCamposCat)
                {
                    dictionarycat3.Add(convertir.toInt32(item.IdInst), (string)item.Nombre);
                }

                cmbinstalacion.DataSource = dictionarycat3;
               cmbinstalacion.DataTextField = "Value";
               cmbinstalacion.DataValueField = "Key";
               cmbinstalacion.DataBind();

            }
            catch (SqlException ex)
            {
                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Cargando instalaciones", null);
            }
        }

        void generaFolio(string  folio, int tipo=0)
        {
            try
            {

            string letra="X";
            string inst = "CAN";// cmbinstalacion.SelectedValue.ToString();
            string inst2 = "CAL";// cmbinstalacion.SelectedValue.ToString();
            if (Session["letra"] != null)
                letra = Session["letra"].ToString();
            if (Session["instal1"] != null)
                inst = Session["instal1"].ToString();
            if (Session["instal2"] != null)
                inst2 = Session["instal2"].ToString();

            string anio = string.Format("{0:yy}", rdpFecha.SelectedDate);
            string mes = string.Format("{0:MM}", rdpFecha.SelectedDate);
            StringBuilder sbcod = new StringBuilder();
            StringBuilder sbcod2 = new StringBuilder();


            sbcod.Append(inst);
            sbcod.Append(letra);
            sbcod.Append(mes.Length == 1 ? "0" + mes : mes);
            sbcod.Append(anio);
            sbcod.Append(folio);
            //-------------------
            sbcod2.Append(inst2);
            sbcod2.Append(letra);
            sbcod2.Append(mes.Length == 1 ? "0" + mes : mes);
            sbcod2.Append(anio);
            sbcod2.Append(folio);

                if(tipo==1)
                    addfoliocalidad.Text=sbcod2.ToString();
                else if (tipo == 2)
                    addfoliocantidad.Text = sbcod.ToString();
                else
                {
                    addfoliocalidad.Text = sbcod2.ToString();
                    addfoliocantidad.Text = sbcod.ToString();
                }

            }
            catch (Exception ex)
            {

                convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                throw;
            }

        }

        String AgregarCat()
        {
            String error = "F";

            String _reffol = string.Format("{0}{1}|{2}{3}", addfoliocalidad.Text.Trim().ToString(), addfoliocalidad03.Text.Trim().ToString(), addfoliocantidad.Text.Trim().ToString(), addfoliocantidad03.Text.Trim().ToString());
            decimal? _propileno = addpropileno.Text.ToString().Length == 0 ? 0 : convertir.toNDecimal(addpropileno.Text);

            Int64? idbarco = cmbbarco.SelectedValue.Length > 0 ? convertir.toNInt64(cmbbarco.SelectedValue) : 0;

            capascccmex.datos.mov_producto obj = new capascccmex.datos.mov_producto();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr",System.Data.SqlDbType.Char,1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vestatus_revisado", "N"));
            campos.Add(new SqlParameter("vestatus_pagado", "P"));

            campos.Add(new SqlParameter("vorden_servicio", addordenservicio.Text.Trim().ToUpper().ToString()));
            campos.Add(new SqlParameter("vidproducto", convertir.toNInt64(cmbproducto.SelectedValue)));
            campos.Add(new SqlParameter("vidcentro", convertir.toNInt64(Session["getIdCentroUsr"])));
            campos.Add(new SqlParameter("vidinst", convertir.toNInt64(cmbinstalacion.SelectedValue)));
            campos.Add(new SqlParameter("vidservicio", cmbservicio.SelectedValue.ToString()));
            campos.Add(new SqlParameter("vidbarco", convertir.toNInt64(idbarco)));
            campos.Add(new SqlParameter("vcant_insp_mezcla", convertir.toNDecimal(addcantidadinsp.Text)));
            campos.Add(new SqlParameter("vpropileno", _propileno));
            campos.Add(new SqlParameter("vanio", Convert.ToInt32(addanio.Text)));
            campos.Add(new SqlParameter("vmes", Convert.ToInt16(addmes.Text)));
            campos.Add(new SqlParameter("vfecha", string.Format("{0:yyyy-MM-dd}", rdpFecha.SelectedDate.Value)));
            campos.Add(new SqlParameter("vfolio_cert_cant", addfoliocantidad02.Text.ToString().Trim()));
            campos.Add(new SqlParameter("vfolio_cert_calidad", addfoliocalidad02.Text.ToString().Trim()));
            campos.Add(new SqlParameter("vlote_turbosina", addloteturbo.Text.ToString().Trim()));
            campos.Add(new SqlParameter("vreferencia_folio", _reffol));
            campos.Add(new SqlParameter("vcreate_iappid", convertir.toNInt64(Session["getIdusuario"])));
            campos.Add(new SqlParameter("vlast_update_iappid", convertir.toNInt64(Session["getIdusuario"])));


            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        #endregion

        #region Eventos de pantalla

        void deleteFile()
        {
            string newfilename = "";
            int unicode = 65;          
            string caracter = "";
            
            //Session["getFilePdfCAN"] = newfilename.ToString();

            for (int i = 1; i <= 4; i++)
            {
                caracter = char.ConvertFromUtf32(unicode);
                filecalidad.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString();
                newfilename = string.Format("{0}{1}_{2}{3}", addfoliocalidad.Text.Trim().ToString(), addfoliocalidad03.Text.Trim().ToString(), caracter.ToString(), ".pdf");

                if (System.IO.File.Exists(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename)))
                    System.IO.File.Delete(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename));
                unicode++;
            }
        }

        void subirArchivoCALIDAD()
        {
            string pathDirectoryPadre = "";
            string newfilename = "";
            int unicode = 65;
            bool bit = false;

            if (addfoliocalidad02.Text.Length > 0 && addfoliocalidad03.Text.Length > 0)
                bit = true;
            if (bit == true)
            {
                //Verificar la subida de archivos
                deleteFile();
                pathDirectoryPadre = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\" + Session["getIdCentroUsr"].ToString();

                if (!Directory.Exists(pathDirectoryPadre))
                {
                    System.IO.Directory.CreateDirectory(pathDirectoryPadre.ToString());
                }
                foreach (Telerik.Web.UI.UploadedFile f in filecalidad.UploadedFiles)
                {

                    string caracter = char.ConvertFromUtf32(unicode);
                    filecalidad.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString();
                    newfilename = string.Format("{0}{1}_{2}{3}", addfoliocalidad.Text.Trim().ToString(), addfoliocalidad03.Text.Trim().ToString(), caracter.ToString(), ".pdf");
                    Session["getFilePdfCAN"] = newfilename.ToString();
                    if (!File.Exists(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename)))
                    {

                        f.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename), true);
                        unicode++;
                    }
                }
            }
        }

        //protected void filecalidad_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        //{
        //    string pathDirectoryPadre = "";
        //    string newfilename="";
        //    int unicode = 65;
        //    bool bit = false;

        //      if( addfoliocalidad02.Text.Length>0 && addfoliocalidad03.Text.Length>0)
        //          bit=true;
        //    if(bit==true)
        //   {
        //        //Verificar la subida de archivos
          
        //       pathDirectoryPadre = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\" + Session["getIdCentroUsr"].ToString();

        //       if (!Directory.Exists(pathDirectoryPadre))
        //       {
        //           System.IO.Directory.CreateDirectory(pathDirectoryPadre.ToString());
        //       }
        //       foreach (Telerik.Web.UI.UploadedFile f in filecalidad.UploadedFiles)
        //       {
                   
        //           string caracter = char.ConvertFromUtf32(unicode);
        //           filecalidad.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString();
        //           newfilename = string.Format("{0}{1}_{2}{3}", addfoliocalidad.Text.Trim().ToString(), addfoliocalidad03.Text.Trim().ToString(),caracter.ToString(), e.File.GetExtension());
        //           Session["getFilePdfCAN"] = newfilename.ToString();
        //           if(!File.Exists(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename)))
        //           {
                  
        //           f.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename),true);
        //           unicode++;
        //           }
        //       }
            
                
               

            
                 
        //    //e.File.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename));
        //   }
        //}



        protected void rAsyncPDF_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
              bool bit = false;

              if( addfoliocantidad02.Text.Length>0 && addfoliocantidad03.Text.Length>0)
                  bit=true;
            if(bit==true)
           {
               //string pathDirectoryPadre = HttpContext.Current.ApplicationInstance.Server.MapPath("~/reportes") + "\\" + Session["getIdAgencia"].ToString();
                string pathDirectoryPadre = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\" + Session["getIdCentroUsr"].ToString();
                //rAsyncPDF.TargetFolder = "~/reportes" + "\\" + Session["getIdAgencia"].ToString(); //+"\\" + Session["getIdTramite"] + e.File.GetExtension(); ;
                rAsyncPDF.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString(); //+"\\" + Session["getIdTramite"] + e.File.GetExtension();

                if (!Directory.Exists(pathDirectoryPadre))
                {
                    System.IO.Directory.CreateDirectory(pathDirectoryPadre.ToString());
                    //System.IO.Directory.CreateDirectory(pathDirectoryHijo.ToString());
                }
                string newfilename = string.Format("{0}{1}{2}",addfoliocantidad.Text.Trim().ToString(),  addfoliocantidad03.Text.Trim().ToString(),  e.File.GetExtension());
                Session["getFilePdfCAL"] = newfilename.ToString();

                if (System.IO.File.Exists(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename)))
                    System.IO.File.Delete(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename));
                
                e.File.SaveAs(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename));
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
                    String param = AgregarCat();
                    subirArchivoCALIDAD();

                    if (param.CompareTo("F") == 0)
                    {
                        RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Captura del día", null);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("Captura ingresada con éxito...", 300, 200, "Captura del día", null);
                    }

                    string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                }
                catch (SqlException ex)
                {
                    convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Captura del día", null);

                }
            }
            else
            {
            #region parametrosColor

                if (cmbinstalacion.SelectedValue != null && cmbinstalacion.SelectedValue.Length > 0)
                    cmbinstalacion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                //cmbinstalacion.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    cmbinstalacion.BackColor = System.Drawing.ColorTranslator.FromHtml("#b94a48");
                   // cmbinstalacion.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                if (cmbservicio.Text.Length > 0)
                    cmbservicio.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                else
                    cmbservicio.BackColor = System.Drawing.ColorTranslator.FromHtml("#b94a48");

                if (cmbproducto.Text.Length > 0)
                    cmbproducto.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                else
                    cmbproducto.BackColor = System.Drawing.ColorTranslator.FromHtml("#b94a48");
                               
                if (addordenservicio.Text.Length > 0)
                    addordenservicio.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addordenservicio.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                if (addcantidadinsp.Text.Length > 0)
                    addcantidadinsp.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addcantidadinsp.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                

                if (rdpFecha.SelectedDate.HasValue)
                    rdpFecha.BackColor = System.Drawing.ColorTranslator.FromHtml("#468847");
                else
                    rdpFecha.BackColor = System.Drawing.ColorTranslator.FromHtml("#b94a48");

                if (addfoliocantidad02.Text.Length > 0)
                    addfoliocantidad02.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addfoliocantidad02.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");

                if (addfoliocalidad02.Text.Length > 0)
                    addfoliocalidad02.Attributes.Add("style", "border-color: #468847;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
                else
                    addfoliocalidad02.Attributes.Add("style", "border-color:#b94a48;  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);");
        #endregion
                RadWindowManager1.RadAlert("Existen registros obligatorios, favor de corregir ", 300, 100, "Captura por centro", null);
            }
        }
        
        protected void cmbinstalacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbinstalacion.SelectedValue.Length > 0)
            {
                if (rdpFecha.SelectedDate.HasValue)
                {
                    addanio.Text = rdpFecha.SelectedDate.Value.Year.ToString();
                    addmes.Text = rdpFecha.SelectedDate.Value.Month.ToString();
                }

                string codprod = cmbproducto.SelectedValue.ToString();
                string codinstal = cmbinstalacion.SelectedValue.ToString();
                if (codprod.Length > 0)
                    TAR_REF(codprod, codinstal);
               

                generaFolio("");
            }
        }

        void TAR_REF(string producto, string instal)
        {
            Session["instal1"] = null;
            Session["instal2"] = null;
            if (producto.CompareTo("33006") == 0)//Turbosina 
            {
            switch (instal)
            {
                case "602": //Cadereyta
                    Session["instal1"] = instal.ToString();
                    Session["instal2"] = "312".ToString();
                break;
                case "612": //Madero
                Session["instal1"] = instal.ToString();
                Session["instal2"] = "311".ToString();
                break;
                case "693": //Tula
                Session["instal1"] = instal.ToString();
                Session["instal2"] = "303".ToString();
                break;
                default: //No existe coincidencias
                     Session["instal1"] = instal.ToString();
                     Session["instal2"] = instal.ToString();
                break;
            }
            }
            else if (producto.CompareTo("11508") == 0 || producto.CompareTo("51037") == 0)//11508-Propileno grado quimico, 51037-grado refineria 
            {
                switch (instal)
                {
                    case "672": //Minatitlan
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = "331".ToString();
                        break;
                    case "652": //Salamanca
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = "302".ToString();
                        break;
                    case "693": //Tula
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = "303".ToString();
                        break;
                    case "602": //Cadereyta
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = "312".ToString();
                        break;
                    case "681": //Salina cruz
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = "332".ToString();
                        break;
                    default: //No existe coincidencias
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = instal.ToString();
                        break;
                }
            }
            else if (producto.CompareTo("51038") == 0)//Propano 
            {
                switch (instal)
                {
                    case "612": //Madero
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = "311".ToString();
                        break;
                    default: //No existe coincidencias
                        Session["instal1"] = instal.ToString();
                        Session["instal2"] = instal.ToString();
                        break;
                }
            }
            else
            {
                Session["instal1"] = instal.ToString();
                Session["instal2"] = instal.ToString();
            }
        }

        protected void cmbproducto_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string letra = "";

            if (cmbproducto.SelectedValue.Length > 0)
            {
                if (rdpFecha.SelectedDate.HasValue)
                {
                    addanio.Text = rdpFecha.SelectedDate.Value.Year.ToString();
                    addmes.Text = rdpFecha.SelectedDate.Value.Month.ToString();
                }

                addpropileno.Visible = false;
                //addloteturbo.Visible = false; 

                string codprod = cmbproducto.SelectedValue.ToString();
                string codinstal = cmbinstalacion.SelectedValue.ToString();
                if (codprod.CompareTo("51038") == 0)//propano
                {
                    addpropileno.Visible = true;
                    //addloteturbo.Visible = false;
                    letra = "P";
                }
                else if (codprod.CompareTo("33006") == 0)//Turbosina   
                {
                    letra = "T";
                    addpropileno.Visible = false;
                    //addloteturbo.Visible = true;
                }

                else if (codprod.CompareTo("11508") == 0)//Propileno                 
                {
                    addpropileno.Visible = true;
                    //addloteturbo.Visible = false;
                    letra = "P";
                }

                else if (codprod.CompareTo("51037") == 0)//Propileno                  
                {
                    addpropileno.Visible = true;
                    //addloteturbo.Visible = false;
                    letra = "P";
                }
                else
                    letra = "X";
                Session["letra"] = letra.ToString();
                if (codinstal.Length>0)
                TAR_REF(codprod, codinstal);
                else
                    RadWindowManager1.RadAlert("Debe seleccionar una instalación" , 200, 90, "Generando folio", null);

                generaFolio("");
            }
        }

        protected void rdpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (rdpFecha.SelectedDate.HasValue)
            {
                addanio.Text = rdpFecha.SelectedDate.Value.Year.ToString();
                addmes.Text = rdpFecha.SelectedDate.Value.Month.ToString();
            }
            generaFolio("");
        }

        protected void addfoliocantidad02_TextChanged(object sender, EventArgs e)
        {
            string lcant = "", fol = "";

            if (addfoliocantidad02.Text.Length > 0)
            {
                fol = addfoliocantidad02.Text.ToString();
                if (cmbproducto.SelectedValue.Length > 0)
                {

                    string codprod = cmbproducto.SelectedValue.ToString();
                    if (codprod.CompareTo("51038") == 0)//propano                   
                        lcant = "V";

                    else if (codprod.CompareTo("33006") == 0)//Turbosina                    
                        lcant = "BV";

                    else if (codprod.CompareTo("11508") == 0)//Propileno                 
                        lcant = "V";

                    else if (codprod.CompareTo("51037") == 0)//Propileno                  
                        lcant = "V";
                    else
                        lcant = "SYS";

                    generaFolio(fol, 2);
                    addfoliocantidad03.Text = lcant.ToString();
                   
                    
                }
            }
        }

        protected void addfoliocalidad02_TextChanged(object sender, EventArgs e)
        {
            string lcal = "", fol = "";

            if (addfoliocalidad02.Text.Length > 0)
            {
                fol = addfoliocalidad02.Text.ToString();
                if (cmbproducto.SelectedValue.Length > 0)
                {

                    string codprod = cmbproducto.SelectedValue.ToString();
                    if (codprod.CompareTo("51038") == 0)//propano                   
                        lcal = "P";

                    else if (codprod.CompareTo("33006") == 0)//Turbosina                    
                        lcal = "C";

                    else if (codprod.CompareTo("11508") == 0)//Propileno                 
                        lcal = "P";

                    else if (codprod.CompareTo("51037") == 0)//Propileno                  
                        lcal = "P";
                    else
                        lcal = "SYS";

                    generaFolio(fol, 1);
                    addfoliocalidad03.Text = lcal.ToString();
                    filecalidad.Enabled = true;
                    rAsyncPDF.Enabled = true;
                }
            }
        }

        protected void cmbservicio_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbservicio.SelectedValue.Length > 0)
            {
                string servicio = cmbservicio.SelectedValue.ToString();
                if (servicio.CompareTo("BT") == 0)
                    cmbbarco.Enabled = true;
                else
                    cmbbarco.Enabled = false;
            }
        }

        #endregion

    }
}