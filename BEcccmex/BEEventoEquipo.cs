using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BEEventoEquipo
    {

        private Int64? _idEvento;

        public Int64? IdEvento
        {
            get { return _idEvento; }
            set { _idEvento = value; }
        }
        private Int64? _idEquipo;

        public Int64? IdEquipo
        {
            get { return _idEquipo; }
            set { _idEquipo = value; }
        }
        private String _evento;

        public String Evento
        {
            get { return _evento; }
            set { _evento = value; }
        }
        private String _tipoAlarma;

        public String TipoAlarma
        {
            get { return _tipoAlarma; }
            set { _tipoAlarma = value; }
        }
        private Int64? _prealarma;

        public Int64? Prealarma
        {
            get { return _prealarma; }
            set { _prealarma = value; }
        }
        private String fechaEvento;

        public String FechaEvento
        {
            get { return fechaEvento; }
            set { fechaEvento = value; }
        }
        private String vigencia;

        public String Vigencia
        {
            get { return vigencia; }
            set { vigencia = value; }
        }
        private Int64? _postAlarma;

        public Int64? PostAlarma
        {
            get { return _postAlarma; }
            set { _postAlarma = value; }
        }
        private String _observacion;

        public String Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }
        private Int64? _idCentro;

        public Int64? IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }
        private String _centro;

        public String Centro
        {
            get { return _centro; }
            set { _centro = value; }
        }
        private Int64? _idInstalacion;

        public Int64? IdInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }
        private String _instalacion;

        public String Instalacion
        {
            get { return _instalacion; }
            set { _instalacion = value; }
        }

        private String _equipo;

        public String Equipo
        {
            get { return _equipo; }
            set { _equipo = value; }
        }
        
     
       
   

    }
}
