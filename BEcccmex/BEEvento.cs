using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BEEvento
    {


        private Int64? _idEvento;

        public Int64? idEvento
        {
            get { return _idEvento; }
            set { _idEvento = value; }
        }


        private Int64? _idEquipo;

        public Int64? idEquipo
        {
            get { return _idEquipo; }
            set { _idEquipo = value; }
        }


        private string _evento;

        public string evento
        {
            get { return _evento; }
            set { _evento = value; }
        }


        private string _tipoEvento;

        public string tipoEvento
        {
            get { return _tipoEvento; }
            set { _tipoEvento = value; }
        }


        private Int64? _prealarma;

        public Int64? prealarma
        {
            get { return _prealarma; }
            set { _prealarma = value; }
        }


        private DateTime _fechaEvento;

        public DateTime fechaEvento
        {
            get { return _fechaEvento; }
            set { _fechaEvento= value; }
        }

       

         private string _vigencia;

        public string vigencia
        {
            get { return _vigencia; }
            set { _vigencia= value; }
        }

        private Int64? _postAlarma;

        public Int64? postAlarma
        {
            get { return _postAlarma; }
            set { _postAlarma= value; }
        }

        private string _observacion;

        public string observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }




    }

    }

