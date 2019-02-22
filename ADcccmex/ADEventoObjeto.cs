using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using BEcccmex;
using System.Net.Mail;
using System.Net;

namespace ADcccmex
{
    public class ADEventoObjeto
    {
        public List<BEObjetoDiagrama> GetObjetoDiagrama(int idInstalacion, int idevento)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEObjetoDiagrama> listaObjetoDiagrama = new List<BEObjetoDiagrama>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("proc_geteventoobjeto");
            if (idInstalacion > 0)
                db.AddInParameter(dbc, "IDINSTALACION", System.Data.DbType.Int16, idInstalacion);
            if (idevento > 0)
                db.AddInParameter(dbc, "IDEVENTO", System.Data.DbType.Int16, idevento);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEObjetoDiagrama obj = new BEObjetoDiagrama();
                       
                obj.objetoX = Convert.ToInt32(dr["OBJETOX"]);
                obj.objetoY = Convert.ToInt32(dr["OBJETOY"]);
                obj.objetoW = Convert.ToInt32(dr["OBJETOW"]);
                obj.objetoH = Convert.ToInt32(dr["OBJETOH"]);
                obj.color = Convert.ToString(dr["COLOR"]);
                obj.idEvento = Convert.ToInt32(dr["IDEVENTO"]);
                obj.tipoEvento = Convert.ToString(dr["TIPOEVENTO"]);
                obj.fechaEvento = Convert.ToDateTime(dr["FECHAEVENTO"]).ToString("dd/MM/yyyy");
                obj.fechaPostEvento = Convert.ToString(dr["POSTALARMA_FC"]);
                obj.fechaPreEvento = Convert.ToString(dr["PREALARMA_FC"]);
                obj.nombreEquipo = Convert.ToString(dr["EQUIPO"]);
                obj.idcentro = Convert.ToInt32(dr["idcentro"]);
                obj.idinst = Convert.ToInt32(dr["idinst"]);
                obj.instalacion = Convert.ToString(dr["INSTALACION"]);
                obj.responsable = Convert.ToString(dr["NOMBRE"]);
                obj.mailResp = Convert.ToString(dr["CORREO"]);
                listaObjetoDiagrama.Add(obj);
            }

            return listaObjetoDiagrama;
        }
        public List<BEObjetoDiagrama> GetEventoDiagrama(int idInstalacion, int idevento)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEObjetoDiagrama> listaObjetoDiagrama = new List<BEObjetoDiagrama>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("proc_geteventodiagrama");
            if (idInstalacion > 0)
                db.AddInParameter(dbc, "IDINSTALACION", System.Data.DbType.Int16, idInstalacion);
            if (idevento > 0)
                db.AddInParameter(dbc, "IDEVENTO", System.Data.DbType.Int16, idevento);
            DataSet ds = db.ExecuteDataSet(dbc);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BEObjetoDiagrama obj = new BEObjetoDiagrama();

                obj.objetoX = Convert.ToInt32(dr["OBJETOX"]);
                obj.objetoY = Convert.ToInt32(dr["OBJETOY"]);
                obj.objetoW = Convert.ToInt32(dr["OBJETOW"]);
                obj.objetoH = Convert.ToInt32(dr["OBJETOH"]);
                obj.color = Convert.ToString(dr["COLOR"]);
                obj.idEvento = Convert.ToInt32(dr["IDEVENTO"]);
                obj.tipoEvento = Convert.ToString(dr["TIPOEVENTO"]);
                obj.fechaEvento = Convert.ToString(dr["FECHAEVENTO"]);
                obj.fechaPostEvento = Convert.ToString(dr["POSTALARMA_FC"]);
                obj.fechaPreEvento = Convert.ToString(dr["PREALARMA_FC"]);
                obj.nombreEquipo = Convert.ToString(dr["EQUIPO"]);
                obj.idcentro = Convert.ToInt32(dr["idcentro"]);
                obj.idinst = Convert.ToInt32(dr["idinst"]);
                obj.instalacion = Convert.ToString(dr["INSTALACION"]);
                obj.responsable = Convert.ToString(dr["NOMBRE"]);
                obj.mailResp = Convert.ToString(dr["CORREO"]);
                listaObjetoDiagrama.Add(obj);
            }

            return listaObjetoDiagrama;
        }

        public int EnviarCorreoxInstalacion(int idInstalacion, string instalacion)
        {
            string contenido = "";
            string titulo = "Alerta de Equipos - " + instalacion;
            string eventos = "";
            string mailRespon = "";
            string color;
            int cont = 0;
            int res = 0;
            int activo= 0;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();
            try
            {

            
            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("proc_geteventoobjeto");
            if (idInstalacion > 0)
                db.AddInParameter(dbc, "IDINSTALACION", System.Data.DbType.Int16, idInstalacion);

            DataSet ds = db.ExecuteDataSet(dbc);
            contenido = "<h2>Alerta de Equipos</h2><br>" +
            "<p> En la instalacion: " + instalacion + " <br> " +
                " Tiene eventos de : </p><p>";

           
                
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                activo = Convert.ToInt32(dr["activo"]);
                if (activo==0)
                    return -1;
                color = Convert.ToString(dr["COLOR"]);
                if (color != "GREEN")
                {
                    eventos = eventos +
                    
                    Convert.ToString(dr["TIPOEVENTO"]) + " del equipo " +
                    "<B>" + Convert.ToString(dr["EQUIPO"]) + "</B>" + " en la fecha: " + Convert.ToDateTime(dr["FECHAEVENTO"]).ToShortDateString() + "</p>";
                    cont++;
                    mailRespon = Convert.ToString(dr["CORREO"]);
                }
            }
            if (cont>0)
            {
                contenido = contenido + eventos;
                enviarEmail(mailRespon, titulo, contenido);

                }
            }
            catch (Exception)
            {

                throw;
            }
            return res;
        }

        private Boolean enviarEmail(string mailResponsable, string titulo, string contenido)
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
                correo.To.Add(mailResponsable);
                correo.CC.Add(emailCoordinador);
                correo.Subject = titulo;
                correo.Body = contenido;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient objsmtp = new SmtpClient(EmailSmtp, puerto);
                //objsmtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod..Network;
                objsmtp.UseDefaultCredentials = false;
                objsmtp.EnableSsl = vSsl;
                objsmtp.Credentials = new NetworkCredential(EmailCccmex, pwd);
                
                objsmtp.Host = EmailSmtp;
                objsmtp.Port = puerto;
                
                objsmtp.Send(correo);
                estado = true;
            }
            catch (Exception ex)
            {
                string xy = ex.Message.ToString();
                estado = false;
                throw;
            }
            correo.Dispose();
            return estado;
        }
        public int EnviarCorreoPorEvento(int idcentro)
        {
            int r = 0;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.CreateDefault();

            List<BEObjetoDiagrama> listaObjetoDiagrama = new List<BEObjetoDiagrama>();

            //IDPropietario= 0, significa que quiero todo el catalogo completo
            DbCommand dbc = db.GetStoredProcCommand("EnviarCorreoxResponsable");
            if (idcentro > 0)
                db.AddInParameter(dbc, "idcentro", System.Data.DbType.Int16, idcentro);
            db.AddOutParameter(dbc, "RETURNVAL", System.Data.DbType.Int64, 8);

            r = db.ExecuteNonQuery(dbc);
            if (r >= 0)
                r = Convert.ToInt32(db.GetParameterValue(dbc, "RETURNVAL"));

            return r;
        }
    }
}
