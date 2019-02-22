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
using Telerik.Web.UI;

namespace appwebcccmex
{
    public partial class modal_cccmex_editbycentro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                  //  Int64? _idRrg = convertir.toNInt64(Session["getIdReg"]);                   
                    loadRegActual();
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        void loadRegActual()
        {
            Int64? _idRrg = convertir.toNInt64(Session["getIdReg"]);
            List<capascccmex.metadatos.movproducto> oCamposCat = new List<capascccmex.metadatos.movproducto>();
            oCamposCat = (List<capascccmex.metadatos.movproducto>)Session["getCamposCatMovimiento"];

            var getReg = from oReg in oCamposCat
                         where oReg.IdReg == _idRrg
                         select oReg;

        
            foreach (var iReg in getReg)
            {
                idinstal.Text = iReg.IdInst.ToString();
                idservicio.Text = iReg.IdServicio.ToString();
                nombre_servicio.Text = iReg.NombreServicio.ToString();
                idproducto.Text=iReg.IdProducto.ToString();
                nombre_producto.Text=iReg.NombreProducto.ToString();
                idbarco.Text = iReg.IdBarco.ToString();
                nombre_barco.Text = iReg.NombreBarco.ToString();
                lblordenservicio.Text = iReg.Orden_servicio.ToString();
                lblcantidad.Text = string.Format("{0:#,#0.000}", iReg.Cant_insp_mezcla);


               

                string[] words = iReg.Referencia_folio.Split('|');

                addanio.Text = string.Format("{0:yy}", iReg.Fecha);
                addmes.Text = string.Format("{0:MM}", iReg.Fecha);

                addfoliocantidad.Text = string.Format("{0} | {1}", iReg.Folio_cert_cant_aux, words[1]);
                //lblfolcertcantidad_file.Text = string.Format("{0} | {1}", iReg.Folio_cert_cant_aux, words[0]);
                addfoliocalidad.Text = string.Format("{0} | {1}", iReg.Folio_cert_calidad_aux, words[0]);

                //addComent.Text = iReg.Comentarios.ToString();

           
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

        string folioCalidad()
        {

            string[] words = addfoliocalidad.Text.Split('|');
            string fol = words[0].ToString().Trim(); ;
            string letra = "X";    
            string lcal = "";
            string codprod = idproducto.Text.ToString();
            string instal =  idinstal.Text.ToString();

            TAR_REF(codprod, instal);

            if (codprod.CompareTo("51038") == 0)//propano  
            {
                lcal = "P";
                letra = "P";
            }

            else if (codprod.CompareTo("33006") == 0)//Turbosina                    
            {       lcal = "C"; letra = "T";  }
            else if (codprod.CompareTo("11508") == 0)//Propileno                 
            { lcal = "P"; letra = "P";}

            else if (codprod.CompareTo("51037") == 0)//Propileno                  
            { lcal = "P"; letra = "P";}
            else
                lcal = "SYS";


                   
            string inst2 = "CAL";// cmbinstalacion.SelectedValue.ToString();          
            if (Session["instal2"] != null)
                inst2 = Session["instal2"].ToString();

            string anio = addanio.Text.ToString(); ;
            string mes = addmes.Text.ToString();
           
            StringBuilder sbcod = new StringBuilder();
            sbcod.Append(inst2);
            sbcod.Append(letra);
            sbcod.Append(mes.Length == 1 ? "0" + mes : mes);
            sbcod.Append(anio);
            sbcod.Append(fol);
            sbcod.Append(lcal);

            return sbcod.ToString();

        }

        string folioCantidad()
        {

            string[] words = addfoliocantidad.Text.Split('|');
            string fol = words[0].ToString().Trim(); ;
            string letra = "X";
            string lcant = "";
            string codprod = idproducto.Text.ToString();
            string instal = idinstal.Text.ToString();

            TAR_REF(codprod, instal);

            if (codprod.CompareTo("51038") == 0)//propano  
            {
                lcant = "V";
                letra = "P";
            }

            else if (codprod.CompareTo("33006") == 0)//Turbosina                    
            { lcant = "BV"; letra = "T"; }
            else if (codprod.CompareTo("11508") == 0)//Propileno                 
            { lcant = "V"; letra = "P"; }

            else if (codprod.CompareTo("51037") == 0)//Propileno                  
            { lcant = "V"; letra = "P"; }
            



            string inst = "CAN";// cmbinstalacion.SelectedValue.ToString();          
            if (Session["instal1"] != null)
                inst = Session["instal1"].ToString();

            string anio = addanio.Text.ToString(); ;
            string mes = addmes.Text.ToString();

            StringBuilder sbcod = new StringBuilder();
            sbcod.Append(inst);
            sbcod.Append(letra);
            sbcod.Append(mes.Length == 1 ? "0" + mes : mes);
            sbcod.Append(anio);
            sbcod.Append(fol);
            sbcod.Append(lcant);

            return sbcod.ToString();

        }


        //void generaFolio(string folio, int tipo = 0)
        //{
        //    try
        //    {

        //        string letra = "X";
        //        string inst = "CAN";// cmbinstalacion.SelectedValue.ToString();
        //        string inst2 = "CAL";// cmbinstalacion.SelectedValue.ToString();
        //        if (Session["letra"] != null)
        //            letra = Session["letra"].ToString();
        //        if (Session["instal1"] != null)
        //            inst = Session["instal1"].ToString();
        //        if (Session["instal2"] != null)
        //            inst2 = Session["instal2"].ToString();

        //        string anio = addanio.Text.ToString(); ;
        //        string mes = addmes.Text.ToString();
        //        StringBuilder sbcod = new StringBuilder();
        //        StringBuilder sbcod2 = new StringBuilder();


        //        sbcod.Append(inst);
        //        sbcod.Append(letra);
        //        sbcod.Append(mes.Length == 1 ? "0" + mes : mes);
        //        sbcod.Append(anio);
        //        sbcod.Append(folio);
        //        //-------------------
        //        sbcod2.Append(inst2);
        //        sbcod2.Append(letra);
        //        sbcod2.Append(mes.Length == 1 ? "0" + mes : mes);
        //        sbcod2.Append(anio);
        //        sbcod2.Append(folio);

        //        if (tipo == 1)
        //            addfoliocalidad.Text = sbcod2.ToString();
        //        else if (tipo == 2)
        //            addfoliocantidad.Text = sbcod.ToString();
        //        else
        //        {
        //            addfoliocalidad.Text = sbcod2.ToString();
        //            addfoliocantidad.Text = sbcod.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        convertir.log("Error: " + ex.Message.ToString() + ", fecha: " + DateTime.Now.ToString());
        //        throw;
        //    }

        //}

    
        protected void cmdEjecuta_Click(object sender, EventArgs e)
        {

            String param2 = actualizaCat();
            subirArchivoCALIDAD();
            if (param2.CompareTo("F") == 0)
            {
                RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando File", null);
            }
            else
            {
                string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }

        protected void rAsyncPDF_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            bool bit = false;
            string folioCANT = folioCantidad();

            if (folioCANT.ToString().Length > 0)
                bit = true;
            if (bit == true)
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
                string newfilename = string.Format("{0}{1}", folioCANT.Trim().ToString(), e.File.GetExtension());

                if (System.IO.File.Exists(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename)))
                    System.IO.File.Delete(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename));

                e.File.SaveAs(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename),true);
            }
        }

        void deleteFile()
        {
            //string pathDirectoryPadre = "";
            string newfilename = "";
            int unicode = 65;
            //bool bit = false;
            string caracter = "";



            //Eliminar archivos si existen
            string folioCAL = folioCalidad();

            //Session["getFilePdfCAN"] = newfilename.ToString();

            for (int i = 1; i <= 4; i++)
            {
                caracter = char.ConvertFromUtf32(unicode);
                filecalidad.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString();
                newfilename = string.Format("{0}_{1}{2}", folioCAL.Trim().ToString(), caracter.ToString(), ".pdf");

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
            string caracter = "";

            string folioCAL = folioCalidad();
            if (folioCAL.ToString().Length > 0)
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

                    caracter = char.ConvertFromUtf32(unicode);
                    filecalidad.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString();
                    newfilename = string.Format("{0}_{1}{2}", folioCAL.Trim().ToString(), caracter.ToString(), ".pdf");
                    //Session["getFilePdfCAN"] = newfilename.ToString();

                    //if (System.IO.File.Exists(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename)))
                    //    System.IO.File.Delete(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename));

                    if (!File.Exists(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename)))
                    {
                        f.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename), true);
                        unicode++;
                    }
                }

                //e.File.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename));
            }
        }


        //protected void filecalidad_FileUploaded(object sender, FileUploadedEventArgs e)
        //{
        //    string pathDirectoryPadre = "";
        //    string newfilename = "";
        //    int unicode = 65;
        //    bool bit = false;
        //    string caracter = "";

        //    string folioCAL = folioCalidad();
        //    if (folioCAL.ToString().Length > 0)
        //        bit = true;
        //    if (bit == true)
        //    {
        //        //Verificar la subida de archivos
        //        deleteFile();
        //        pathDirectoryPadre = HttpContext.Current.ApplicationInstance.Server.MapPath("~/filecert") + "\\" + Session["getIdCentroUsr"].ToString();

        //        if (!Directory.Exists(pathDirectoryPadre))
        //        {
        //            System.IO.Directory.CreateDirectory(pathDirectoryPadre.ToString());
        //        }

              
              

        //        foreach (Telerik.Web.UI.UploadedFile f in filecalidad.UploadedFiles)
        //        {

        //            caracter = char.ConvertFromUtf32(unicode);
        //            filecalidad.TargetFolder = "~/filecert" + "\\" + Session["getIdCentroUsr"].ToString();
        //            newfilename = string.Format("{0}_{1}{2}", folioCAL.Trim().ToString(), caracter.ToString(), e.File.GetExtension());
        //            //Session["getFilePdfCAN"] = newfilename.ToString();

        //            //if (System.IO.File.Exists(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename)))
        //            //    System.IO.File.Delete(Path.Combine(Server.MapPath(rAsyncPDF.TargetFolder), newfilename));

        //            if (!File.Exists(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename)))
        //            {
        //                f.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename), true);
        //                unicode++;
        //            }
        //        }

        //        //e.File.SaveAs(Path.Combine(Server.MapPath(filecalidad.TargetFolder), newfilename));
        //    }
        //}

        String actualizaCat()
        {
            //VERICAR QUE USUARIO GENERO LA OPERACIÓN DE CAMBIO ...
            String error = "F";

            string referencia = string.Format("{0}|{1}", folioCalidad(), folioCantidad());
            Int64? _idRrg = convertir.toNInt64(Session["getIdReg"]);

            capascccmex.datos.mov_producto obj = new capascccmex.datos.mov_producto();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("vidreg", _idRrg));
            campos.Add(new SqlParameter("referencia_folio", referencia.ToString().Trim()));           

            error = obj.actualizarByFile(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }
    }
}