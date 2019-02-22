using System;


namespace capascccmex.metadatos
{
    public class orden_servicio
    { 
    Int32 _idorden;

public Int32 Idorden
{
  get { return _idorden; }
  set { _idorden = value; }
}
     Decimal    _volumen;

public Decimal Volumen
{
  get { return _volumen; }
  set { _volumen = value; }
}
       Int32 _mes, _anio;

public Int32 Mes
{
  get { return _mes; }
  set { _mes = value; }
}

public Int32 Anio
{
  get { return _anio; }
  set { _anio = value; }
}
DateTime _fecha;

public DateTime Fecha
{
    get { return _fecha; }
    set { _fecha = value; }
}
String _orden_servicio;

public String Orden_servicio
{
    get { return _orden_servicio; }
    set { _orden_servicio = value; }
}

    }
    public class centro
    {
        int _idCentro;
        public int IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }

        String _centro;
        public String Centro
        {
            get { return _centro; }
            set { _centro = value; }
        }
    }

    public class producto
    {
        Int64? _idProducto;

        public Int64? IdProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }
        String _producto;

        public String Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
    }

    public class servicio
    {
        String _idServicio, _servicio;

        public String IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public String Servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }
    }

    public class barco
    {
        Int64? _idBarco;

        public Int64? IdBarco
        {
            get { return _idBarco; }
            set { _idBarco = value; }
        }
        String _barco;

        public String Barco
        {
            get { return _barco; }
            set { _barco = value; }
        }

        Boolean? importacion;

        public Boolean? Importacion
        {
            get { return importacion; }
            set { importacion = value; }
        }
    }

    public class usuarioweb
    {
        Int64? _iappId, _idCentro;

        public Int64? IappId
        {
            get { return _iappId; }
            set { _iappId = value; }
        }

        public Int64? IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }

        String _iappLogin, _iappPwd, _iappNombre_Completo, _iappCorreo, _nombre_Centro;

        public String Nombre_Centro
        {
            get { return _nombre_Centro; }
            set { _nombre_Centro = value; }
        }

        public String IappLogin
        {
            get { return _iappLogin; }
            set { _iappLogin = value; }
        }

        public String IappPwd
        {
            get { return _iappPwd; }
            set { _iappPwd = value; }
        }

        public String IappNombre_Completo
        {
            get { return _iappNombre_Completo; }
            set { _iappNombre_Completo = value; }
        }

        public String IappCorreo
        {
            get { return _iappCorreo; }
            set { _iappCorreo = value; }
        }

        Boolean _iappActivo, _iappAdmin, _iappPemex;

        public Boolean IappActivo
        {
            get { return _iappActivo; }
            set { _iappActivo = value; }
        }

        public Boolean IappAdmin
        {
            get { return _iappAdmin; }
            set { _iappAdmin = value; }
        }

        public Boolean IappPemex
        {
            get { return _iappPemex; }
            set { _iappPemex = value; }
        }

       

        DateTime? _last_update;

        public DateTime? Last_update
        {
            get { return _last_update; }
            set { _last_update = value; }
        }
    }

    public class movproducto
    {
        Boolean _barcoImp;

        public Boolean BarcoImp
        {
            get { return _barcoImp; }
            set { _barcoImp = value; }
        }

        Int64? _idReg, _idProducto, _idCentro, _idBarco, _create_iappid, _last_update_iappid, _idInst, _idregbyprod;

        public Int64? Idregbyprod
        {
            get { return _idregbyprod; }
            set { _idregbyprod = value; }
        }

        public Int64? IdInst
        {
            get { return _idInst; }
            set { _idInst = value; }
        }
        public Int64? IdReg
        {
            get { return _idReg; }
            set { _idReg = value; }
        }
        public Int64? IdProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }
        public Int64? IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }
        public Int64? IdBarco
        {
            get { return _idBarco; }
            set { _idBarco = value; }
        }
        public Int64? Create_iappid
        {
            get { return _create_iappid; }
            set { _create_iappid = value; }
        }
        public Int64? Last_update_iappid
        {
            get { return _last_update_iappid; }
            set { _last_update_iappid = value; }
        }

        String _estatus_revisado, _estatus_pagado, _orden_servicio, _idServicio, _nombreBarco, _comentarios;

        public String Comentarios
        {
            get { return _comentarios; }
            set { _comentarios = value; }
        }
        public String NombreBarco
        {
            get { return _nombreBarco; }
            set { _nombreBarco = value; }
        }
        public String Estatus_revisado
        {
            get { return _estatus_revisado; }
            set { _estatus_revisado = value; }
        }
        public String Estatus_pagado
        {
            get { return _estatus_pagado; }
            set { _estatus_pagado = value; }
        }
        public String Orden_servicio
        {
            get { return _orden_servicio; }
            set { _orden_servicio = value; }
        }
        public String IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        String _lote_turbosina, _folio_cert_cant, _folio_cert_calidad, _referencia_folio;

        public String Lote_turbosina
        {
            get { return _lote_turbosina; }
            set { _lote_turbosina = value; }
        }
        public String Folio_cert_cant
        {
            get { return _folio_cert_cant; }
            set { _folio_cert_cant = value; }
        }
        public String Folio_cert_calidad
        {
            get { return _folio_cert_calidad; }
            set { _folio_cert_calidad = value; }
        }
        public String Referencia_folio
        {
            get { return _referencia_folio; }
            set { _referencia_folio = value; }
        }

        Decimal? _cant_insp_mezcla, _propileno;

        public Decimal? Cant_insp_mezcla
        {
            get { return _cant_insp_mezcla; }
            set { _cant_insp_mezcla = value; }
        }
        public Decimal? Propileno
        {
            get { return _propileno; }
            set { _propileno = value; }
        }

        Int16? _year, _mes;

        public Int16? Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public Int16? Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }

        DateTime? _fecha;

        public DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        String _nombreProducto, _nombreCentro, _nombreServicio, _pdfCalidad, _pdfCantidad;

        public String PdfCalidad
        {
            get { return _pdfCalidad; }
            set { _pdfCalidad = value; }
        }
        public String PdfCantidad
        {
            get { return _pdfCantidad; }
            set { _pdfCantidad = value; }
        }
        public String NombreProducto
        {
            get { return _nombreProducto; }
            set { _nombreProducto = value; }
        }
        public String NombreCentro
        {
            get { return _nombreCentro; }
            set { _nombreCentro = value; }
        }
        public String NombreServicio
        {
            get { return _nombreServicio; }
            set { _nombreServicio = value; }
        }

        String _folio_cert_cant_aux, _folio_cert_calidad_aux;

        public String Folio_cert_cant_aux
        {
            get { return _folio_cert_cant_aux; }
            set { _folio_cert_cant_aux = value; }
        }
        public String Folio_cert_calidad_aux
        {
            get { return _folio_cert_calidad_aux; }
            set { _folio_cert_calidad_aux = value; }
        }

    }

    public class instalaciones
    {
        int _idInst, _idCentro;

        public int IdInst
        {
            get { return _idInst; }
            set { _idInst = value; }
        }

        public int IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }
        String _nombre;

        public String Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }

    public class laboratorio
    {
        Int64? _idlaboratorio, _idinst, _idprueba;

        public Int64? Idlaboratorio
        {
            get { return _idlaboratorio; }
            set { _idlaboratorio = value; }
        }
        public Int64? Idinst
        {
            get { return _idinst; }
            set { _idinst = value; }
        }
        public Int64? Idprueba
        {
            get { return _idprueba; }
            set { _idprueba = value; }
        }

        String _metodo_astm, _dispositivo_temp, _no_inf_calibr_temp, _hidrometro, _no_inf_calibr_hid, _probeta, _pruebas;

        public String Pruebas
        {
            get { return _pruebas; }
            set { _pruebas = value; }
        }
        public String Metodo_astm
        {
            get { return _metodo_astm; }
            set { _metodo_astm = value; }
        }
        public String Dispositivo_temp
        {
            get { return _dispositivo_temp; }
            set { _dispositivo_temp = value; }
        }
        public String No_inf_calibr_temp
        {
            get { return _no_inf_calibr_temp; }
            set { _no_inf_calibr_temp = value; }
        }
        public String Hidrometro
        {
            get { return _hidrometro; }
            set { _hidrometro = value; }
        }
        public String No_inf_calibr_hid
        {
            get { return _no_inf_calibr_hid; }
            set { _no_inf_calibr_hid = value; }
        }
        public String Probeta
        {
            get { return _probeta; }
            set { _probeta = value; }
        }

        String _no_inf_calibr_prob, _equipo_analisis, _modelo_marca;

        public String No_inf_calibr_prob
        {
            get { return _no_inf_calibr_prob; }
            set { _no_inf_calibr_prob = value; }
        }
        public String Equipo_analisis
        {
            get { return _equipo_analisis; }
            set { _equipo_analisis = value; }
        }
        public String Modelo_marca
        {
            get { return _modelo_marca; }
            set { _modelo_marca = value; }
        }

        String _fecha_calibr_mantto, _fecha_vig_estandar;

        public String Fecha_calibr_mantto
        {
            get { return _fecha_calibr_mantto; }
            set { _fecha_calibr_mantto = value; }
        }
        public String Fecha_vig_estandar
        {
            get { return _fecha_vig_estandar; }
            set { _fecha_vig_estandar = value; }
        }

        String _no_inf_calibr_equipo, _estandar_verif_util, _medidor_poro_memb, _inf_calibr_bal_analitica, inf_calibr_tubo_cann;

        public String No_inf_calibr_equipo
        {
            get { return _no_inf_calibr_equipo; }
            set { _no_inf_calibr_equipo = value; }
        }
        public String Estandar_verif_util
        {
            get { return _estandar_verif_util; }
            set { _estandar_verif_util = value; }
        }
        public String Medidor_poro_memb
        {
            get { return _medidor_poro_memb; }
            set { _medidor_poro_memb = value; }
        }
        public String Inf_calibr_bal_analitica
        {
            get { return _inf_calibr_bal_analitica; }
            set { _inf_calibr_bal_analitica = value; }
        }
        public String Inf_calibr_tubo_cann
        {
            get { return inf_calibr_tubo_cann; }
            set { inf_calibr_tubo_cann = value; }
        }

    }

    public class pruebas
    {
        Int64? _idpruebas;

        public Int64? Idpruebas
        {
            get { return _idpruebas; }
            set { _idpruebas = value; }
        }
        String _pruebas;

        public String Pruebas
        {
            get { return _pruebas; }
            set { _pruebas = value; }
        }
    }

    public class operatividad
    {
        Int64? _idOperatividad, _idCentro;

        public Int64? IdOperatividad
        {
            get { return _idOperatividad; }
            set { _idOperatividad = value; }
        }
        public Int64? IdCentro
        {
            get { return _idCentro; }
            set { _idCentro = value; }
        }

        string _equipos_Rechazados;

        public string Equipos_Rechazados
        {
            get { return _equipos_Rechazados; }
            set { _equipos_Rechazados = value; }
        }
        String _cantidad_dia_anterior, _unidad_inspeccionada, _unidad_pendiente, _unidad_inspeccionada_hora, _unidad_pendiente_hora,
                         _tanque_servicio, _problema_inspeccion, _otras_observaciones, _cantidad_facturada, _centro_centro, _cantidad_facturada2;

        public String Cantidad_facturada2
        {
            get { return _cantidad_facturada2; }
            set { _cantidad_facturada2 = value; }
        }

        public String Centro_centro
        {
            get { return _centro_centro; }
            set { _centro_centro = value; }
        }

        public String Cantidad_dia_anterior
        {
            get { return _cantidad_dia_anterior; }
            set { _cantidad_dia_anterior = value; }
        }
        public String Unidad_inspeccionada
        {
            get { return _unidad_inspeccionada; }
            set { _unidad_inspeccionada = value; }
        }
        public String Unidad_pendiente
        {
            get { return _unidad_pendiente; }
            set { _unidad_pendiente = value; }
        }
        public String Unidad_inspeccionada_hora
        {
            get { return _unidad_inspeccionada_hora; }
            set { _unidad_inspeccionada_hora = value; }
        }
        public String Unidad_pendiente_hora
        {
            get { return _unidad_pendiente_hora; }
            set { _unidad_pendiente_hora = value; }
        }
        public String Tanque_servicio
        {
            get { return _tanque_servicio; }
            set { _tanque_servicio = value; }
        }
        public String Problema_inspeccion
        {
            get { return _problema_inspeccion; }
            set { _problema_inspeccion = value; }
        }
        public String Otras_observaciones
        {
            get { return _otras_observaciones; }
            set { _otras_observaciones = value; }
        }
        public String Cantidad_facturada
        {
            get { return _cantidad_facturada; }
            set { _cantidad_facturada = value; }
        }


        DateTime? _fecha;
        public DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

    }

}
