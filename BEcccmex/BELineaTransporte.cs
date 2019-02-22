using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
   public class BELineaTransporte
    {
        private Int64? _idLineaTransporte;
        private string _lineaTransporte;
        private string _descripcion;

        public long? IdLineaTransporte { get => _idLineaTransporte; set => _idLineaTransporte = value; }
        public string LineaTransporte { get => _lineaTransporte; set => _lineaTransporte = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
    }
}
