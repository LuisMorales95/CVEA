using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BEInstalacion
    {
        private int _idInstalacion;

        public int IdInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

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
