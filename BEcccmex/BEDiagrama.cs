using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
   public  class BEDiagrama
    {

        private int _idDiagrama;

        public int idDiagrama
        {
            get { return _idDiagrama; }
            set { _idDiagrama = value; }
        }


        private int _idCentro;

        public int idCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }
        private int _idInstalacion;

        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        private string _nombreCentro;

        public string nombreCentro
        {
            get { return _nombreCentro; }
            set { _nombreCentro = value; }
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


        private string _archivo;

        public string archivo
        {
            get { return _archivo; }
            set { _archivo = value; }
        }

       


    }
}
