using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using capascccmex;
using System.Text;
using System.IO;

namespace appwebcccmex
{
    public partial class modal_cccmex_situacionoperativa : System.Web.UI.Page
    {
        List<capascccmex.metadatos.operatividad> lstCat = new List<capascccmex.metadatos.operatividad>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

                    if (Session["opOpera"] != null)
                    {
                        //Session["opTipo"] = 1;
                        nombreCentro.Text = Session["nameCentroActual"].ToString();                       
                        Int16 tipoOp = Convert.ToInt16(Session["opOpera"]);
                        if (tipoOp == 1)//alta
                        {
                           cmdEjecuta.Text = "Agregar Registro";                          
                        }
                        else if (tipoOp == 2)//actualiza
                        {
                            cmdEjecuta.Text = "Actualizar Registro";
                            lstCat = (List<capascccmex.metadatos.operatividad>)Session["getCamposCatOperatividad"];
                            cargarInfoOperatividad(lstCat);
                        }

                    }
                }
                else
                    Response.Redirect("~/Account/outSession.aspx");

            }
        }

        #region Metodos de operacion
        void inicializa()
        {
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            nombreCentro.Text = Session["nameCentroActual"].ToString().Trim().ToUpper();
           
            rdpFecha.SelectedDate = DateTime.Now;
            cambiarEncabezado();
        }

        void cambiarEncabezado()
        {
            if (rdpFecha.SelectedDate.HasValue)
            {
                DateTime fecha = rdpFecha.SelectedDate.Value;
                int diaAnterior = Convert.ToInt32(fecha.Day) - 1;

                lbl1.Text = string.Format("Cantidad inspeccionada {0:00}/{1:MM/yyyy}", diaAnterior, fecha);
                lbl2.Text = string.Format("Unidades inspeccionadas de 00:00 hrs a 08:30 ({0:dd/MM/yyyy})", fecha);
                lbl3.Text = string.Format("Unidades pendientes de inspeccionar siendo las 8:30 ({0:dd/MM/yyyy})", fecha);
                lbl4.Text = string.Format("Unidades inspeccionadas de las 08:30 a las 16:00 hrs ({0:dd/MM/yyyy})", fecha);
                lbl5.Text = string.Format("Unidades pendientes de inspeccionar a las 16:00 hrs ({0:dd/MM/yyyy})", fecha);
                Label4.Text = string.Format("Cantidad facturada el día {0:00}/{1:MM/yyyy}", diaAnterior, fecha);
                Label6.Text = string.Format("Cantidad facturada el día {0:dd/MM/yyyy} hasta las 16:00 PM", fecha);
                lbl9.Text = string.Format("Equipos rechazados {0:00}/{1:MM/yyyy}", diaAnterior, fecha);
            }
            
        }

        private void cargarInfoOperatividad(List<capascccmex.metadatos.operatividad> _lstOp)
        {
            Int64? _idOP = convertir.toNInt64(Session["getIdOperaGrid"]);
            nombreCentro.Text = Session["nameCentroActual"].ToString();
            var getUsr = from oUsr in _lstOp
                         where oUsr.IdOperatividad == _idOP
                         select oUsr;

            //lblEmpresa.Text = Session["getNombreEmpresa"].ToString();

            foreach (var iop in getUsr)
            {
                rdpFecha.SelectedDate = Convert.ToDateTime(iop.Fecha);
                addCantidad_dia_anterio.Text = iop.Cantidad_dia_anterior.ToString();
                addUnidad_inspeccionada.Text = iop.Unidad_inspeccionada.ToString();
                addUnidad_pendiente.Text = iop.Unidad_pendiente.ToString();
                addUnidad_inspeccionada_hrs.Text = iop.Unidad_inspeccionada_hora.ToString();
                addUnidad_pendiente_hrs.Text = iop.Unidad_pendiente_hora.ToString();
                addtanque_servicio_muestreo.Text = iop.Tanque_servicio.ToString();
                addproblemas_inspeccion.Text = iop.Problema_inspeccion.ToString();
                addotras_observaciones.Text = iop.Otras_observaciones.ToString();
                addcantidad_facturada.Text = iop.Cantidad_facturada.ToString();
                addcantidad_facturada2.Text = iop.Cantidad_facturada2.ToString();
                addEquiposRechazados.Text = iop.Equipos_Rechazados.ToString(); 

            }

            cambiarEncabezado();
        }
  
        Int64? AgregarCat()
        {
            Int64? error = 0;
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            DateTime? fecha = convertir.toNDateTime(rdpFecha.SelectedDate);
            string cmp1 = addCantidad_dia_anterio.Text.ToString().Trim().ToUpper();
            string cmp2 = addUnidad_inspeccionada.Text.ToString().Trim().ToUpper();
            string cmp3 = addUnidad_pendiente.Text.ToString().Trim().ToUpper();
            string cmp4 = addUnidad_inspeccionada_hrs.Text.ToString().Trim().ToUpper();
            string cmp5 = addUnidad_pendiente_hrs.Text.ToString().Trim().ToUpper();
            string cmp6 = addtanque_servicio_muestreo.Text.ToString().Trim().ToUpper();
            string cmp7 = addproblemas_inspeccion.Text.ToString().Trim().ToUpper();
            string cmp8 = addotras_observaciones.Text.ToString().Trim().ToUpper();
            string cmp9 = addcantidad_facturada.Text.ToString().Trim().ToUpper();
            string cmp10 = addcantidad_facturada2.Text.ToString().Trim().ToUpper();
            string cmp11 = addEquiposRechazados.Text.ToString().Trim().ToUpper();

            capascccmex.datos.operatividad obj = new capascccmex.datos.operatividad();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("@idoperatividad", System.Data.SqlDbType.BigInt));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@idcentro", _idCentro));
            campos.Add(new SqlParameter("@cantidad_dia_anterior", cmp1));
            campos.Add(new SqlParameter("@unidad_inspeccionada", cmp2));
            campos.Add(new SqlParameter("@unidad_pendiente", cmp3));
            campos.Add(new SqlParameter("@unidad_inspeccionada_hora", cmp4));
            campos.Add(new SqlParameter("@unidad_pendiente_hora", cmp5));
            campos.Add(new SqlParameter("@tanque_servicio", cmp6));
            campos.Add(new SqlParameter("@problema_inspeccion", cmp7));

            campos.Add(new SqlParameter("@otras_observaciones", cmp8));
            campos.Add(new SqlParameter("@cantidad_facturada", cmp9));
            campos.Add(new SqlParameter("@cantidad_facturada2", cmp10));
            campos.Add(new SqlParameter("@fecha", string.Format("{0:yyyy-MM-dd}", rdpFecha.SelectedDate.Value)));
            campos.Add(new SqlParameter("@equipo_rechazado", cmp11));

            error = obj.agregar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();
            return error;
        }

        String actualizaCat()
        {
            String error = "F";

            Int64? _idOP = convertir.toNInt64(Session["getIdOperaGrid"]);
            Int64? _idCentro = convertir.toNInt64(Session["getIdCentroUsr"]);
            DateTime? fecha = convertir.toNDateTime(rdpFecha.SelectedDate);
            string cmp1 = addCantidad_dia_anterio.Text.ToString().Trim().ToUpper();
            string cmp2 = addUnidad_inspeccionada.Text.ToString().Trim().ToUpper();
            string cmp3 = addUnidad_pendiente.Text.ToString().Trim().ToUpper();
            string cmp4 = addUnidad_inspeccionada_hrs.Text.ToString().Trim().ToUpper();
            string cmp5 = addUnidad_pendiente_hrs.Text.ToString().Trim().ToUpper();
            string cmp6 = addtanque_servicio_muestreo.Text.ToString().Trim().ToUpper();
            string cmp7 = addproblemas_inspeccion.Text.ToString().Trim().ToUpper();
            string cmp8 = addotras_observaciones.Text.ToString().Trim().ToUpper();
            string cmp9 = addcantidad_facturada.Text.ToString().Trim().ToUpper();
            string cmp10 = addcantidad_facturada2.Text.ToString().Trim().ToUpper();
            string cmp11 = addEquiposRechazados.Text.ToString().Trim().ToUpper();


            capascccmex.datos.operatividad obj = new capascccmex.datos.operatividad();
            List<SqlParameter> campos = new List<SqlParameter>();
            campos.Add(new SqlParameter("vr", System.Data.SqlDbType.Char, 1));
            campos[0].Direction = System.Data.ParameterDirection.Output;

            campos.Add(new SqlParameter("@idoperatividad", _idOP));
            campos.Add(new SqlParameter("@idcentro", _idCentro));
            campos.Add(new SqlParameter("@cantidad_dia_anterior", cmp1));
            campos.Add(new SqlParameter("@unidad_inspeccionada", cmp2));
            campos.Add(new SqlParameter("@unidad_pendiente", cmp3));
            campos.Add(new SqlParameter("@unidad_inspeccionada_hora", cmp4));
            campos.Add(new SqlParameter("@unidad_pendiente_hora", cmp5));
            campos.Add(new SqlParameter("@tanque_servicio", cmp6));
            campos.Add(new SqlParameter("@problema_inspeccion", cmp7));

            campos.Add(new SqlParameter("@otras_observaciones", cmp8));
            campos.Add(new SqlParameter("@cantidad_facturada", cmp9));
            campos.Add(new SqlParameter("@cantidad_facturada2", cmp10));
            campos.Add(new SqlParameter("@fecha", string.Format("{0:yyyy-MM-dd}", rdpFecha.SelectedDate.Value)));
            campos.Add(new SqlParameter("@equipo_rechazado", cmp11));
         
            error = obj.actualizar(campos);
            Session["error_Reporte"] = obj.ErrorMensaje.ToString();

            return error;
        }

        #endregion
        protected void rdpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            cambiarEncabezado();
        }

        protected void cmdEjecuta_Click(object sender, EventArgs e)
        {
            if (rdpFecha.SelectedDate.HasValue)
            {
                try
                {

                    Int16 tipoOp = Convert.ToInt16(Session["opOpera"]);
                    if (tipoOp == 1)//alta
                    {
                        Int64? param = AgregarCat();
                        if (param > 0)
                        {
                            RadWindowManager1.RadAlert("Operación ingresada con éxito...", 300, 200, "Agregando Operación", null);
                            
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Agregando Operación", null);
                        }
                    }
                    else if (tipoOp == 2)//actualizar
                    {
                        String param2 = actualizaCat();
                        if (param2.CompareTo("F") == 0)
                        {
                            RadWindowManager1.RadAlert("Se genero el Siguiente Error: " + Session["error_Reporte"].ToString() + ", Favor de verificar con el Administrador de sistemas...", 450, 300, "Actualizando usuarios", null);
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Operación actualizada con éxito...", 300, 200, "actualizando Operación", null);
                        }
                    }

                    string script = "function f(){CloseAndRebind(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                catch (SqlException ex)
                {
                    RadWindowManager1.RadAlert("Error: " + ex.Message.ToString(), 300, 100, "Operación", null);

                }

            }
            else
                RadWindowManager1.RadAlert("Debe seleccionar la fecha para realizar la operación", 300, 100, "Cargando operatividad", null);
        }
    }
}