using ADcccmex;
using BEcccmex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace BLcccmex
{
   public class BLEvento
    {
       private int totalEvento = 0;

        public List<BEEvento> GetEvento(int idEquipo, int idEvento)
        {
            ADEvento obj = new ADEvento();
            List<BEEvento> listaEvento = new List<BEEvento>();
            listaEvento = obj.GetEvento(idEquipo , idEvento);

            totalEvento = listaEvento.Count();

            return listaEvento;
        }

        public int TotalEvento(int idEquipo , int idEvento)
        {
            return totalEvento;
        }



        public int DelEvento(Int64? idEvento)
        {
            int fila = 0;
            ADEvento obj = new ADEvento();
            fila = obj.DelEvento(idEvento);
            return fila;
        }
        //se deberan poner todas las columnas que estan en el radgrid incluso las que estan visibe="false", aunque no se ocupen en los SP
        public int UpdateEvento(int idEvento,int idEquipo ,String evento, String tipoEvento,
            int prealarma, DateTime fechaEvento, string vigencia, int postAlarma, string observacion)
        {

            int fila = 0;
            ADEvento obj = new ADEvento();
            BEEvento oEvento = new BEEvento();

            oEvento.idEvento = idEvento;
            oEvento.idEquipo = idEquipo;
            oEvento.evento = evento;
            oEvento.tipoEvento = tipoEvento;
            oEvento.prealarma = prealarma;
            oEvento.fechaEvento = fechaEvento;
            oEvento.vigencia = vigencia;
            oEvento.postAlarma = postAlarma;
            oEvento.observacion = observacion;


           // oEvento.ModifiedBY = 1;
            fila = obj.UpdateEvento(oEvento);
            return fila;
        }

       public int AddEventoHistorico(int idEvento, int idEquipo, string fechaReal,int iduser)
        {
            int result = 0;
            ADEvento obj = new ADEvento();

            result = obj.AddEventoHistorico(idEvento, idEquipo, fechaReal, iduser);
            return result;
        }
        //se deberan poner todas las columnas que estan en el radgrid incluso las que estan visibe="false", aunque no se ocupen en los SP
        public int AddEvento( int idEquipo, String evento, String tipoEvento,
            int prealarma, DateTime fechaEvento, string vigencia, int postAlarma,string observacion,int idEvento)
        {

            int fila = 0;
            ADEvento obj = new ADEvento();
            BEEvento oEvento = new BEEvento();

            oEvento.idEquipo = idEquipo;
            oEvento.evento = evento;
            oEvento.tipoEvento = tipoEvento;
            oEvento.prealarma = prealarma;
            oEvento.fechaEvento = fechaEvento;
            oEvento.vigencia = vigencia;
            oEvento.postAlarma = postAlarma;
            oEvento.postAlarma = postAlarma;

            //oEvento.CreatedBY = IDUser;
            fila = obj.AddEvento(oEvento);
            return fila;
        }
        public int AddEvento(BEcccmex.BEEvento objEvento)
        {
            BEEvento oEvento = new BEEvento();
            int fila = 0;
            ADEvento obj = new ADEvento();

            oEvento.idEquipo = objEvento.idEquipo;
            oEvento.evento = objEvento.evento; 
            oEvento.tipoEvento = objEvento.tipoEvento; 
            oEvento.prealarma = objEvento.postAlarma; 
            oEvento.fechaEvento = objEvento.fechaEvento; 
            oEvento.vigencia = objEvento.vigencia; 
            oEvento.postAlarma = objEvento.postAlarma;
            oEvento.observacion = objEvento.observacion;
            fila = obj.AddEvento(oEvento);
            return fila;
        }

        public int AddEnvioNotificacion(String fecha, int idcentro)
        {
            int fila = 0;
            ADEvento obj = new ADEvento();
            try
            {
                fila = obj.AddEnvioNotificacionEvento(fecha,idcentro);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return fila;
        }

        public int UpdateEnvioNotificacion(DateTime fecha, bool estatus, int idcentro)
        {
            int fila = 0;
            ADEvento obj = new ADEvento();
            fila = obj.UpdateEnvioNotificacionEvento(fecha,idcentro, estatus);
            return fila;
        }
        public Boolean getEnvioNotificacion(String fecha, int idcentro)
        {
            Boolean res = false;
            ADEvento obj = new ADEvento();
            res = obj.GetVerificaEnvioNotificacion(fecha, idcentro);
            return res;
        }


    }    
}
