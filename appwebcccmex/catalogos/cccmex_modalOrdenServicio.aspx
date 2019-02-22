<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cccmex_modalOrdenServicio.aspx.cs" Inherits="appwebcccmex.catalogos.cccmex_modalOrdenServicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<% @Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Captura de datos</title>
    <link href="../style/modalpopup.css" rel="stylesheet" />
</head>
<body onload="AdjustRadWidow();">
    <form id="form1" runat="server" class="contact_form" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" />
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

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="600px" Height="350px" HorizontalAlign="NotSet">
            <div>
                <ul>
                    <li>
                        <h2>
                            <asp:Label ID="nombreEncabezado" runat="server" Font-Bold="true" Font-Size="16px" Text="Acumulado por orden de servicio"></asp:Label></h2>
                        <hr />
                    </li>
                    <li>
                        <table style="width: 95%; border-collapse: collapse;">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label5" runat="server" Text="Orden De Servicio:" /><br />
                                    <telerik:RadNumericTextBox ID="addordenservicio" runat="server" MaxLength="15" EmptyMessage="00000000000" Skin="Bootstrap" MinValue="0" NumberFormat-DecimalDigits="0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator="," Height="35px" Width="40%">
                                        <ReadOnlyStyle HorizontalAlign="Right" />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="addordenservicio" runat="server" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbl9" runat="server" Text="Volumen:" /><br />
                                    <telerik:RadNumericTextBox ID="addvolumen" runat="server" MaxLength="20" EmptyMessage="0.00" MinValue="0" Skin="Bootstrap" CssClass="form-control" NumberFormat-DecimalDigits="3" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator="," Height="35px" Width="40%">
                                        <ReadOnlyStyle HorizontalAlign="Right" />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="addvolumen" runat="server" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl1" runat="server" Text="Mes:" /><br />
                                    <telerik:RadComboBox ID="cmbmes" runat="server" EmptyMessage="- Selecciona el mes -" Skin="Bootstrap" CssClass="form-control" Width="250px" MarkFirstMatch="true"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cmbmes" runat="server" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lbl2" runat="server" Text="Año:" /><br />
                                    <telerik:RadTextBox ID="addanio" runat="server" Width="30%" Height="30px" Skin="Bootstrap" MaxLength="4" EmptyMessage="Año" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cmbmes" runat="server" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" Visible="false"
                                        ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />
                                </td>
                            </tr>

                        </table>
                    </li>
                    <li>
                        <div style="text-align: right; margin-right: 20px;">
                            <telerik:RadButton ID="cmdEjecuta" runat="server" Width="35%" Height="35px" Text="Agregar" Skin="Material" RenderMode="Lightweight" OnClientClicking="RadConfirm" OnClick="cmdEjecuta_Click">
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
