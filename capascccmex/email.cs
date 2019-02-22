using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace capascccmex
{
    public class email
    {
        private string _mensajeError, _nombreDe, _usuario, _clave, _servidor;

        public string Servidor
        {
            get { return _servidor; }
            set { _servidor = value; }
        }
        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public string NombreDe
        {
            get { return _nombreDe; }
            set { _nombreDe = value; }
        }
        public string MensajeError
        {
            get { return _mensajeError; }
            set { _mensajeError = value; }
        }

        private string _asunto, cuerpoMensaje, _agregarPara, _agregarCC, _agregarArchivoXML, _agregarArchivoPDF;

        public string AgregarArchivoPDF
        {
            get { return _agregarArchivoPDF; }
            set { _agregarArchivoPDF = value; }
        }
        public string AgregarArchivoXML
        {
            get { return _agregarArchivoXML; }
            set { _agregarArchivoXML = value; }
        }
        public string AgregarCC
        {
            get { return _agregarCC; }
            set { _agregarCC = value; }
        }
        public string AgregarPara
        {
            get { return _agregarPara; }
            set { _agregarPara = value; }
        }
        public string CuerpoMensaje
        {
            get { return cuerpoMensaje; }
            set { cuerpoMensaje = value; }
        }
        public string Asunto
        {
            get { return _asunto; }
            set { _asunto = value; }
        }

        private Int32 _puerto;
        public Int32 Puerto
        {
            get { return _puerto; }
            set { _puerto = value; }
        }

        private bool _SSL, _esContenidoHTML, _notificar;
        public bool Notificar
        {
            get { return _notificar; }
            set { _notificar = value; }
        }
        public bool EsContenidoHTML
        {
            get { return _esContenidoHTML; }
            set { _esContenidoHTML = value; }
        }
        public bool SSL
        {
            get { return _SSL; }
            set { _SSL = value; }
        }

        public bool envioMail()
        {
            bool bandera = false;
            try
            {
                System.Net.Mail.MailMessage Correo = new System.Net.Mail.MailMessage();
                Correo.From = new System.Net.Mail.MailAddress(_usuario, _nombreDe);
                MailAddress replytoaddr = new MailAddress(_usuario);

                if (_notificar == true) { Correo.To.Add(_usuario); }
                Correo.To.Add(_agregarPara);
                if (_agregarCC.Length > 0) { Correo.CC.Add(_agregarCC); }
                Correo.Subject = _asunto.ToString();
                Correo.Body = cuerpoMensaje.ToString();
                Correo.IsBodyHtml = _esContenidoHTML;
                Correo.Priority = System.Net.Mail.MailPriority.High;

                Correo.SubjectEncoding = System.Text.Encoding.UTF8;
                Correo.BodyEncoding = System.Text.Encoding.UTF8;

                //Notificación
                Correo.Headers.Add("Disposition-Notification-To", _usuario);
                //Correo.ReplyTo = replytoaddr;
                Correo.ReplyTo = new System.Net.Mail.MailAddress(_usuario);

                System.Net.Mail.SmtpClient SMTP = new System.Net.Mail.SmtpClient();


                SMTP.Host = _servidor;
                SMTP.Port = _puerto;

                SMTP.Credentials = new NetworkCredential(_usuario, _clave);

                SMTP.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                SMTP.EnableSsl = _SSL;


                //Archivos adjuntos
                System.Net.Mail.Attachment attachXML = new System.Net.Mail.Attachment(_agregarArchivoXML);
                System.Net.Mail.Attachment attachPDF = new System.Net.Mail.Attachment(_agregarArchivoPDF);

                Correo.Attachments.Add(attachXML);
                Correo.Attachments.Add(attachPDF);

                SMTP.Send(Correo);
                bandera = true;
            }
            catch (Exception ex)
            {
                _mensajeError = ex.Message.ToString();
            }
            return bandera;
        }
    }
}
