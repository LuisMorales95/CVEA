using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.biz
{
    public class mov_producto
    {
        public List<metadatos.movproducto> GetBizProducto(Int64? _idReg, String _estatusRev, String _estatusPag, Int64? _idProd, Int64? _idCentro, String _idServicio, Int64? _idBarco, Int16? _anio, Int16? _mes, DateTime? fecha, DateTime? fecha2, int maximumRows, int startRowIndex)
        {
            datos.mov_producto obj = new datos.mov_producto();
            List<metadatos.movproducto> listaObjs = new List<metadatos.movproducto>();

            List<SqlParameter> campos = new List<SqlParameter>();

            campos.Add(new SqlParameter("vidreg", _idReg));
            campos.Add(new SqlParameter("vestatus_revisado", _estatusRev));
            campos.Add(new SqlParameter("vestatus_pagado", _estatusPag));
            campos.Add(new SqlParameter("vidproducto", _idProd));
            campos.Add(new SqlParameter("vidcentro", _idCentro));
            campos.Add(new SqlParameter("vidservicio", _idServicio));
            campos.Add(new SqlParameter("vidbarco", _idBarco));
            campos.Add(new SqlParameter("vanio", _anio));
            campos.Add(new SqlParameter("vmes", _mes));
            campos.Add(new SqlParameter("vfecha", fecha));
            campos.Add(new SqlParameter("vfecha2", fecha2));

            listaObjs = obj.obtener(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.movproducto> GetBizProducto02(Int64? _idReg, String _estatusRev, String _estatusPag, Int64? _idProd, Int64? _idCentro, String _idServicio, Int64? _idBarco, Int16? _anio, Int16? _mes, DateTime? fecha, DateTime? fecha2, bool? barcoimp,int maximumRows, int startRowIndex)
        {
            datos.mov_producto obj = new datos.mov_producto();
            List<metadatos.movproducto> listaObjs = new List<metadatos.movproducto>();

            List<SqlParameter> campos = new List<SqlParameter>();

            campos.Add(new SqlParameter("vidreg", _idReg));
            campos.Add(new SqlParameter("vestatus_revisado", _estatusRev));
            campos.Add(new SqlParameter("vestatus_pagado", _estatusPag));
            campos.Add(new SqlParameter("vidproducto", _idProd));
            campos.Add(new SqlParameter("vidcentro", _idCentro));
            campos.Add(new SqlParameter("vidservicio", _idServicio));
            campos.Add(new SqlParameter("vidbarco", _idBarco));
            campos.Add(new SqlParameter("vanio", _anio));
            campos.Add(new SqlParameter("vmes", _mes));
            campos.Add(new SqlParameter("vfecha", fecha));
            campos.Add(new SqlParameter("vfecha2", fecha2));

            campos.Add(new SqlParameter("@importacion", barcoimp));

            listaObjs = obj.obtenerGerencia(campos);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.movproducto> GetBizAcumuladoCentro(String sprocedure, Int16? _anio, Int16? _mes)
        {
            datos.mov_producto obj = new datos.mov_producto();
            List<metadatos.movproducto> listaObjs = new List<metadatos.movproducto>();

            List<SqlParameter> campos = new List<SqlParameter>();
                      
            campos.Add(new SqlParameter("@anio", _anio));
            campos.Add(new SqlParameter("@mes", _mes));
           
            listaObjs = obj.obtenerAcumuladoByCentro(campos,sprocedure);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.movproducto> GetBizAcumuladoServicio(String sprocedure, Int16? _anio, Int16? _mes)
        {
            datos.mov_producto obj = new datos.mov_producto();
            List<metadatos.movproducto> listaObjs = new List<metadatos.movproducto>();

            List<SqlParameter> campos = new List<SqlParameter>();

            campos.Add(new SqlParameter("@anio", _anio));
            campos.Add(new SqlParameter("@mes", _mes));

            listaObjs = obj.obtenerAcumuladoByServicio(campos, sprocedure);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.movproducto> GetBizAcumuladoCentroOS(String sprocedure, String _ordenServicio)
        {
            datos.mov_producto obj = new datos.mov_producto();
            List<metadatos.movproducto> listaObjs = new List<metadatos.movproducto>();

            List<SqlParameter> campos = new List<SqlParameter>();

            campos.Add(new SqlParameter("@ordenServicio", _ordenServicio));
          

            listaObjs = obj.obtenerAcumuladoByCentro(campos, sprocedure);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }

        public List<metadatos.movproducto> GetBizAcumuladoServicioOS(String sprocedure, String ordenServicio)
        {
            datos.mov_producto obj = new datos.mov_producto();
            List<metadatos.movproducto> listaObjs = new List<metadatos.movproducto>();

            List<SqlParameter> campos = new List<SqlParameter>();

            campos.Add(new SqlParameter("@ordenServicio", ordenServicio));
          

            listaObjs = obj.obtenerAcumuladoByServicio(campos, sprocedure);
            //totalRegistros = listaObjs.Count();

            return listaObjs;
        }
    }
}
