using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace appwebcccmex
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //notificacionCorreo();

        }
        private void notificacionCorreo()
        {
            BLcccmex.BLEvento objbl = new BLcccmex.BLEvento();
            Boolean res = false;
            CultureInfo culture = new CultureInfo("en-US");
            int resEnvio = 0;
            string fechaHoy = DateTime.Now.ToString("d", culture);
            try
            {

            List<capascccmex.metadatos.instalaciones> oCamposCat = new List<capascccmex.metadatos.instalaciones>();
            capascccmex.biz.instalaciones obj = new capascccmex.biz.instalaciones();
            oCamposCat = obj.GetInstalaciones();
            foreach (var item in oCamposCat)
            {
                res = objbl.getEnvioNotificacion(fechaHoy, item.IdInst);
                if (!res)
                {
                    BLcccmex.BLEventoObjeto objEventoBL = new BLcccmex.BLEventoObjeto();
                    resEnvio = objEventoBL.EnviarCorreoEvento(item.IdInst, item.Nombre);
                    if (resEnvio >= 0)
                        objbl.AddEnvioNotificacion(fechaHoy, item.IdInst);
                }
            }
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }

}