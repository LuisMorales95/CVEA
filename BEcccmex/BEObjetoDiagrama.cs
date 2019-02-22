using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BEObjetoDiagrama
    {


        private int _idObjetoDiagrama;

        public int idObjetoDiagrama
        {
            get { return _idObjetoDiagrama; }
            set { _idObjetoDiagrama = value; }
        }


        private int _idEquipo;

        public int idEquipo
        {
            get { return _idEquipo; }
            set { _idEquipo = value; }
        }

        private int _idEvento;

        public int idEvento
        {
            get { return _idEvento; }
            set { _idEvento = value; }
        }
        private string _tipoEvento;

        public string tipoEvento
        {
            get { return _tipoEvento; }
            set { _tipoEvento = value; }
        }

        private string _fechaEvento;

        public string fechaEvento
        {
            get { return _fechaEvento; }
            set { _fechaEvento = value; }
        }

        private string _fechaPreEvento;

        public string fechaPreEvento
        {
            get { return _fechaPreEvento; }
            set { _fechaPreEvento = value; }
        }
        private string _fechaPostEvento;

        public string fechaPostEvento
        {
            get { return _fechaPostEvento; }
            set { _fechaPostEvento = value; }
        }
        private int _idDiagrama;

        public int idDiagrama
        {
            get { return _idDiagrama; }
            set { _idDiagrama = value; }
        }



        private string _nombreEquipo;

        public string nombreEquipo
        {
            get { return _nombreEquipo; }
            set { _nombreEquipo = value; }
        }



        private string _nombreDiagrama;

        public string nombreDiagrama
        {
            get { return _nombreDiagrama; }
            set { _nombreDiagrama = value; }
        }

        private string _responsable;

        public string responsable
        {
            get { return _responsable; }
            set { _responsable = value; }
        }
        private string _mailResp;

        public string mailResp
        {
            get { return _mailResp; }
            set { _mailResp = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        private string _detalle;

        public string detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }

        private string _color;

        public string color
        {
            get { return _color; }
            set { _color = value; }
        }



        private int _objetoX;

        public int objetoX
        {
            get { return _objetoX; }
            set { _objetoX= value; }
        }


        private int _objetoY;

        public int objetoY
        {
            get { return _objetoY; }
            set { _objetoY = value; }
        }

        private int _objetoW;

        public int objetoW
        {
            get { return _objetoW; }
            set { _objetoW = value; }
        }

        private int _objetoH;

        public int objetoH
        {
            get { return _objetoH; }
            set { _objetoH = value; }
        }


        private string _avisoTexto;

        public string avisoTexto
        {
            get { return _avisoTexto; }
            set { _avisoTexto = value; }
        }


        private int _avisoX;

        public int avisoX
        {
            get { return _avisoX; }
            set { _avisoX = value; }
        }


        private int _avisoY;
        public int avisoY
        {
            get { return _avisoY; }
            set { _avisoY = value; }
        }

        private int _idcentro;
        public int idcentro
        {
            get { return _idcentro; }
            set { _idcentro = value; }
        }
        private int _idinst;
        public int idinst
        {
            get { return _idinst; }
            set { _idinst = value; }
        }
        private string _instalacion;

        public string instalacion
        {
            get { return _instalacion; }
            set { _instalacion = value; }
        }
    }
}
