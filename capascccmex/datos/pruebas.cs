using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace capascccmex.datos
{
    public class pruebas
    {
         //Zona de declaraciones ....
        metadatos.pruebas obj = null;
        SqlServer oCon = null;
        List<metadatos.pruebas> list = null;

        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public pruebas()
        {
            obj = new metadatos.pruebas();
        }

        public List<metadatos.pruebas> obtener(List<SqlParameter> filtros = null)
        {
            list = new List<metadatos.pruebas>();

            using (oCon = new SqlServer())
            {

                if (filtros != null)

                    foreach (SqlParameter p in filtros)
                    {
                        oCon.addParameter(p);
                    }
                try
                {
                    using (SqlDataReader drInfo = oCon.executeReader("proc_get" + this.GetType().Name))
                    {
                        while (drInfo.Read())
                        {
                            obj = new metadatos.pruebas()
                            {
                                Idpruebas = convertir.toNInt64(drInfo["idpruebas"]),
                                Pruebas = drInfo["pruebas"].ToString()
                            };

                            list.Add(obj);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _errorMensaje = ex.Message.ToString();
                }

            }

            return list;
        }

    }
}
