using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEcccmex
{
    public class BETipoEquipo
    {
        private Int64? _idTipoEquipo;
        private string _codigo;
        private string _tipoEquipo;
        private string _descripcion;

        public long? IdTipoEquipo { get => _idTipoEquipo; set => _idTipoEquipo = value; }
        public string Codigo { get => _codigo; set => _codigo = value; }
        public string TipoEquipo { get => _tipoEquipo; set => _tipoEquipo = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
    }
}
