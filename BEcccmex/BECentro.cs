using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BECentro
    {
        private int _idCentro;

        public int IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
