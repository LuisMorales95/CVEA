using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BEEquipo
    {
        private Int64? _idCentro;

        public long? IdCentro
        {
            get
            {
                return _idCentro;
            }

            set
            {
                _idCentro = value;
            }
        }

        private String _centro;

        public string Centro
        {
            get
            {
                return _centro;
            }

            set
            {
                _centro = value;
            }
        }

        private Int64? _idInstalacion;

        public long? IdInstalacion
        {
            get
            {
                return _idInstalacion;
            }

            set
            {
                _idInstalacion = value;
            }
        }

        private string _instalacion;

        public string Instalacion
        {
            get
            {
                return _instalacion;
            }

            set
            {
                _instalacion = value;
            }
        }

        private Int64? _idEquipo;

        public Int64? idEquipo
        {
            get { return _idEquipo; }
            set { _idEquipo = value; }
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


        private string _tag;

        public string tag
        {
            get { return _tag; }
            set { _tag= value; }
        }

        private string _detalle;

        public string detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }

        
    }
}
