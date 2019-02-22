using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
     public class BEZona
    {
        private Int64? _idZona;
        private String _zona;
        private String _descripcion;

        public long? IdZona
        {
            get
            {
                return _idZona;
            }
            set
            {
                _idZona = value;
            }
        }
        public string Zona
        {
            get
            {
                return _zona;
            }
            set
            {
                _zona = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
            }
        }
    }
}
