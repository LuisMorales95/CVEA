using ADcccmex;
using BEcccmex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLcccmex
{
   public  class BLEquipo
    {

        private int totalEquipo = 0;

        public List<BEEquipo> GetEquipo(Int64? idinst)
        {
            ADEquipo obj = new ADEquipo();
            List<BEEquipo> listaEquipo = new List<BEEquipo>();
            listaEquipo = obj.GetEquipoXInstalacion(idinst);

            totalEquipo = listaEquipo.Count();

            return listaEquipo;
        }
        public List<BEEquipo> GetEquipo(int IdCentro, int idInst)
        {
            ADEquipo obj = new ADEquipo();
            List<BEEquipo> listaEquipo = new List<BEEquipo>();
            listaEquipo = obj.GetEquipo(IdCentro, idInst);

            totalEquipo = listaEquipo.Count();

            return listaEquipo;
        }

        public int DelEquipo(Int64? idEquipo)
        {
            int fila = 0;
            ADEquipo obj = new ADEquipo();
            fila = obj.DelEquipo(idEquipo);
            return fila;
        }

        public int TotalEquipo()
        {
            return totalEquipo;
        }

        public int AddEquipo(int idInstalacion, String equipo,String descripcion,
            String tag, String detalle)
        {

            int valor = 0;
            ADEquipo obj = new ADEquipo();
            BEEquipo oEquipo = new BEEquipo();

            oEquipo.IdInstalacion = idInstalacion;
            oEquipo.nombre = equipo;
            oEquipo.descripcion= descripcion;
            oEquipo.tag = tag;
            oEquipo.detalle = detalle;
            valor = obj.AddEquipo(oEquipo);
            return valor;
        }

        public int UpdateEquipo(int idInstalacion, int idEquipo, String equipo, String descripcion,
            String tag, String detalle)
        {

            int valor = 0;
            ADEquipo obj = new ADEquipo();
            BEEquipo oEquipo = new BEEquipo();

            oEquipo.IdInstalacion = idInstalacion;
            oEquipo.idEquipo = idEquipo;
            oEquipo.nombre = equipo;
            oEquipo.descripcion = descripcion;
            oEquipo.tag = tag;
            oEquipo.detalle = detalle;
            valor = obj.UpdateEquipo(oEquipo);
            return valor;
        }
    }
}
