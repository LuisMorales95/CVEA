<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_situacionoperativa.aspx.cs" Inherits="appwebcccmex.modal_cccmex_situacionoperativa" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Captura de datos</title>
    <link href="style/modalpopup.css" rel="stylesheet" />
</head>
<body onload="AdjustRadWidow();">
    <form id="form1" runat="server" class="contact_form" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" />
        <style type="text/css">
            .scrollable {
                height: 550px;
                overflow-y: scroll;
            }
        </style>
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function AdjustRadWidow() {
                var oWindow = GetRadWindow();
                setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 710);
            }

            //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
            function ChromeSafariFix(oWindow) {
                var iframe = oWindow.get_contentFrame();
                var body = iframe.contentWindow.document.body;

                setTimeout(function () {
                    var height = body.scrollHeight;
                    var width = body.scrollWidth;

                    var iframeBounds = $telerik.getBounds(iframe);
                    var heightDelta = height - iframeBounds.height;
                    var widthDelta = width - iframeBounds.width;

                    if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
                    if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
                    oWindow.center();

                }, 310);
            }

            //RadConfirm
            function RadConfirm(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                var text = "Esta seguro?";
                radconfirm(text, callBackFunction, 300, 160, null, "Confirmación");
                args.set_cancel(true);
            }

            //Regresar a parent ...
            function CloseAndRebind(args) { GetRadWindow().BrowserWindow.refreshGrid(args); GetRadWindow().close(); }



        </script>

        <%--  <div id="body" style="width:600px;">--%>

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="650px" Height="650px" HorizontalAlign="NotSet">

            <div class="scrollable">
                <ul>
                    <li>
                        <h2>
                            <asp:Label ID="nombreCentro" runat="server" Font-Bold="true" Font-Size="16px" Text="Centro de trabajo"></asp:Label></h2>
                        <hr />
                    </li>

                    <li>
                        <table style="width: 99%; border-collapse: collapse;">

                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label5" runat="server" Text="Fecha de Captura:" /><br />
                                    <telerik:RadDatePicker ID="rdpFecha" runat="server" Skin="Bootstrap" AutoPostBack="true"
                                        DateInput-EmptyMessage="Fecha Captura" MaxDate="01/01/3000"
                                        MinDate="01/01/1000" Width="30%" Height="35px" OnSelectedDateChanged="rdpFecha_SelectedDateChanged">
                                        <Calendar ID="Calendar3" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbl9" runat="server" Text="Equipos rechazados:" /><br />
                                    <telerik:RadTextBox ID="addEquiposRechazados" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Equipos rechazadis día anterior" />

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl1" runat="server" Text="Cantidad Inspeccionada:" /><br />
                                    <telerik:RadTextBox ID="addCantidad_dia_anterio" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Cantidad día anterio" />
                                </td>
                                <td>
                                    <asp:Label ID="lbl2" runat="server" Text="Unidad Inspeccionada:" /><br />
                                    <telerik:RadTextBox ID="addUnidad_inspeccionada" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Unidad Inspeccionada" />
                                </td>

                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl3" runat="server" Text="Unidad Pendiente:" /><br />
                                    <telerik:RadTextBox ID="addUnidad_pendiente" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Unidad Pendiente" />
                                </td>
                                <td>
                                    <asp:Label ID="lbl4" runat="server" Text="Unidad Inspeccionada Hrs:" /><br />
                                    <telerik:RadTextBox ID="addUnidad_inspeccionada_hrs" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Unidad Inspeccionada Hrs" />
                                </td>

                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl5" runat="server" Text="Unidad Pendiente hrs:" /><br />
                                    <telerik:RadTextBox ID="addUnidad_pendiente_hrs" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Unidad Pendiente Hrs" />
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Tanques en servicio y muestreo:" /><br />
                                    <telerik:RadTextBox ID="addtanque_servicio_muestreo" runat="server" Skin="Bootstrap" MaxLength="300" Width="95%" Height="45px" TextMode="MultiLine" EmptyMessage="Tanques en servicio y muestreo" />
                                </td>

                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Problemas en la inspección:" /><br />
                                    <telerik:RadTextBox ID="addproblemas_inspeccion" runat="server" Skin="Bootstrap" MaxLength="300" TextMode="MultiLine" Height="45px" Width="95%" EmptyMessage="Problemas en la inspección" />
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Otras observaciones:" /><br />
                                    <telerik:RadTextBox ID="addotras_observaciones" runat="server" Skin="Bootstrap" MaxLength="512" Width="95%" Height="35px" EmptyMessage="Otras observaciones" />
                                </td>

                            </tr>

                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label4" runat="server" Text="Cantidad Facturada Ayer:" /><br />
                                    <telerik:RadTextBox ID="addcantidad_facturada" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Cantidad facturada Ayer" />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label6" runat="server" Text="Cantidad Facturada Hoy:" /><br />
                                    <telerik:RadTextBox ID="addcantidad_facturada2" runat="server" Skin="Bootstrap" MaxLength="200" Width="95%" Height="35px" EmptyMessage="Cantidad facturada Hoy" />
                                </td>
                            </tr>

                        </table>


                    </li>
                    <li>
                        <div style="text-align: right; margin-right: 20px;">
                            <telerik:RadButton ID="cmdEjecuta" runat="server" Width="35%" Skin="Material" RenderMode="Lightweight" Text="Agregar" OnClientClicking="RadConfirm" OnClick="cmdEjecuta_Click">
                            </telerik:RadButton>
                        </div>

                    </li>
                </ul>
            </div>



        </telerik:RadAjaxPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
        </telerik:RadWindowManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
            <AjaxSettings>

                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxManager1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" EnableEmbeddedSkins="false">
        </telerik:RadAjaxLoadingPanel>

    </form>
</body>
</html>
