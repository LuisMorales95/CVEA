<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_editbycentro.aspx.cs" Inherits="appwebcccmex.modal_cccmex_editbycentro" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<% @Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Edición de datos</title>
    <link href="style/modalpopup.css" rel="stylesheet" />
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

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="650px" Height="710px" HorizontalAlign="NotSet">

            <div>
                <ul>
                    <li>
                        <table style="width: 99%; border-collapse: collapse;">
                            <tr>
                                <td>
                                    <h2>
                                        <asp:Label ID="nombreCentro" runat="server" Font-Bold="true" Font-Size="16px" Text="Centro de trabajo"></asp:Label></h2>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="idinstal" runat="server" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="idservicio" runat="server" />
                                    <asp:Label ID="nombre_servicio" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="idproducto" runat="server" />
                                    <asp:Label ID="nombre_producto" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="idbarco" runat="server" />
                                    <asp:Label ID="nombre_barco" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblordenservicio" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="ordenserv">Orden de servicio:</label>
                                </td>
                                <td>
                                    <label for="ordenserv">Orden de servicio:</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cantidadinsp">Cantidad inspeccionada:</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblcantidad" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="fecha">Fecha:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addanio" runat="server" Height="35px" Skin="Bootstrap" MaxLength="5" Width="10%" EmptyMessage="AÑO" ReadOnly="true" />
                                    <telerik:RadTextBox ID="addmes" runat="server" Height="35px" Skin="Bootstrap" MaxLength="2" Width="10%" EnabledStyle-HorizontalAlign="Center" EmptyMessage="MES" ReadOnly="true" />
                                
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="folcantidad">Folio Cantidad:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addfoliocantidad" runat="server" MaxLength="50" Skin="Bootstrap" Width="40%" EmptyMessage="Fol. Cantidad" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="filecantidad">Subir Archivo:</label>
                                </td>
                                <td>
                                    <telerik:RadAsyncUpload runat="server" ID="rAsyncPDF" Width="45%" Skin="Bootstrap" MaxFileInputsCount="1"
                                        ChunkSize="8000" OnClientValidationFailed="validationFailed" ManualUpload="false" MultipleFileSelection="Disabled" AllowedFileExtensions="pdf" OnFileUploaded="rAsyncPDF_FileUploaded">
                                    </telerik:RadAsyncUpload>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblfile" runat="server" Text="Folio Calidad:" />
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addfoliocalidad" runat="server" MaxLength="20" Skin="Bootstrap" Width="45%" EmptyMessage="Fol. Calidad" ReadOnly="true" />
                                </td>

                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Subir Archivo:" /></td>
                                <td colspan="2" style="border-spacing: 0px; padding: 0px;">
                                    <telerik:RadAsyncUpload runat="server" ID="filecalidad" Width="45%" Skin="Bootstrap" MaxFileInputsCount="4" Height="35px"
                                        ChunkSize="8000" OnClientValidationFailed="validationFailed" ManualUpload="false" MultipleFileSelection="Disabled" AllowedFileExtensions=".pdf">
                                    </telerik:RadAsyncUpload>
                                </td>

                            </tr>
                        </table>


                    </li>
                    <li>
                        <div style="text-align: right; margin-right: 20px;">
                            <telerik:RadButton ID="cmdEjecuta" runat="server" Width="35%" Skin="Material" RenderMode="Lightweight" Height="35px" Text="Actualizar Archivo" OnClick="cmdEjecuta_Click" OnClientClicking="RadConfirm">
                            </telerik:RadButton>
                        </div>

                    </li>
                </ul>
            </div>

            <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" Visible="false"
                ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />


        </telerik:RadAjaxPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize">
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

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                var $ = $telerik.$;
                function validationFailed(radAsyncUpload, args) {
                    var $row = $(args.get_row());
                    var erorMessage = getErrorMessage(radAsyncUpload, args);
                    var span = createError(erorMessage);
                    $row.addClass("ruError");
                    $row.append(span);
                }

                function getErrorMessage(sender, args) {
                    var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                    if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                        if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                            return ("Archivo no soportado.");
                        }
                        else {
                            return ("This file exceeds the maximum allowed size of 500 KB.");
                        }
                    }
                    else {
                        return ("not correct extension.");
                    }
                }
                function createError(erorMessage) {
                    var input = '<span class="ruErrorMessage">' + erorMessage + ' </span>';
                    return input;
                }

            </script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
