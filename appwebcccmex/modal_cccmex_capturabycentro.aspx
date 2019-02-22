<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_capturabycentro.aspx.cs" Inherits="appwebcccmex.modal_cccmex_capturabycentro" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<% @Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Captura de datos</title>
    <link href="style/modalpopup.css" rel="stylesheet" />
    <style type="text/css">
        .scrollable {
            height: 550px;
            overflow-y: scroll;
        }
   </style>
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
                setTimeout(function () {
                    oWindow.autoSize(true);
                    if ($telerik.isChrome || $telerik.isSafari) {
                        ChromeSafariFix(oWindow);
                    }
                }, 410);
            }

            //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
            function ChromeSafariFix(oWindow) {
                var iframe = oWindow.get_contentFrame();
                var body = iframe.contentWindow.document.body;

                setTimeout(function () {
                    var height = body.height;
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

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="650px"  HorizontalAlign="NotSet">
            <div  class="scrollable">
                <ul>
                    <li>
                        <h2>
                            <asp:Label ID="nombreCentro" runat="server" Font-Bold="true" Font-Size="16px" Text="Centro de trabajo"></asp:Label>
                        </h2>
                        <hr />
                    </li>
                    <li>
                        <table style="width: 100%; border-collapse: collapse;">
                            <tr>
                                <td>
                                    <label for="instalacion">Instalaciones:</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbinstalacion" runat="server" AutoPostBack="true" Skin="Bootstrap" EmptyMessage="- Selecciona la instalación -" Width="300px" MarkFirstMatch="true" LoadingMessage="Cargando..." DropDownWidth="300px" OnSelectedIndexChanged="cmbinstalacion_SelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cmbinstalacion" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="serv">Servicio:</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbservicio" runat="server" AutoPostBack="true" Skin="Bootstrap" EmptyMessage="- Selecciona el servicio -" Width="300px" MarkFirstMatch="true" LoadingMessage="Cargando..." DropDownWidth="300px" OnSelectedIndexChanged="cmbservicio_SelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cmbservicio" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="prod">Productos:</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbproducto" runat="server" AutoPostBack="true" Skin="Bootstrap" EmptyMessage="- Selecciona el producto -" Width="300px" MarkFirstMatch="true" LoadingMessage="Cargando..." DropDownWidth="300px" OnSelectedIndexChanged="cmbproducto_SelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cmbproducto" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="barco">Barco:</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbbarco" runat="server" AutoPostBack="true" Skin="Bootstrap" EmptyMessage="- Selecciona el barco -" Width="300px" MarkFirstMatch="true" LoadingMessage="Cargando..." DropDownWidth="300px"></telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    <hr />
                    </li>

                    <li>
                        <table style="width: 100%; border-collapse: collapse;">
                            <tr>
                                <td>
                                    <label for="ordenserv">Orden de servicio:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addordenservicio" runat="server" Height="35px" MaxLength="20" Width="30%" Skin="Bootstrap" EmptyMessage="Orden servicio"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="addordenservicio" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cantidadinsp">Cantidad inspeccionada:</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="addcantidadinsp" runat="server" EmptyMessage="0.00" Height="35px" Skin="Bootstrap" MinValue="0" NumberFormat-DecimalDigits="3" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator="," Width="30%">
                                        <ReadOnlyStyle HorizontalAlign="Right" />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="addcantidadinsp" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>

                                    <telerik:RadNumericTextBox ID="addpropileno" runat="server" EmptyMessage="Propileno" Height="35px" Skin="Bootstrap" MinValue="0" NumberFormat-DecimalDigits="3" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator="," Width="30%" />
                                    <telerik:RadTextBox ID="addloteturbo" runat="server" Height="35px" MaxLength="20" Width="30%" EmptyMessage="Lote turbosina" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="fecha">Fecha:</label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="rdpFecha" Skin="Bootstrap" runat="server" AutoPostBack="true"
                                        DateInput-EmptyMessage="Fecha Isnpección" MaxDate="01/01/3000"
                                        MinDate="01/01/1000" Width="40%" Height="35px" OnSelectedDateChanged="rdpFecha_SelectedDateChanged">
                                        <Calendar ID="Calendar3" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <telerik:RadTextBox ID="addanio" runat="server" MaxLength="5" Width="20%" Height="35px" Skin="Bootstrap" EmptyMessage="AÑO" ReadOnly="true" />
                                    <telerik:RadTextBox ID="addmes" runat="server" MaxLength="2" Width="20%" Height="35px" Skin="Bootstrap" EnabledStyle-HorizontalAlign="Center" EmptyMessage="MES" ReadOnly="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rdpFecha" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="folcantidad">Folio Cantidad:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addfoliocantidad" runat="server" MaxLength="20" Height="35px" Width="25%" Skin="Bootstrap" EmptyMessage="Fol. Cantidad" ReadOnly="true" />
                                    <telerik:RadTextBox ID="addfoliocantidad02" runat="server" EmptyMessage="Fol. Cantidad" Height="35px" Skin="Bootstrap" MaxLength="150" EnabledStyle-HorizontalAlign="center" Width="30%" OnTextChanged="addfoliocantidad02_TextChanged" AutoPostBack="true" />
                                    <telerik:RadTextBox ID="addfoliocantidad03" runat="server" MaxLength="5" Width="7%" Height="35px" Skin="Bootstrap" EmptyMessage=".." ReadOnly="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="addfoliocantidad02" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="filecantidad">Subir Archivo:</label>
                                </td>
                                <td>
                                    <telerik:RadAsyncUpload runat="server" ID="rAsyncPDF" Width="400px" Skin="Bootstrap" MaxFileInputsCount="1"
                                        ChunkSize="8000" OnClientValidationFailed="validationFailed" ManualUpload="false"
                                        MultipleFileSelection="Disabled" AllowedFileExtensions="pdf" OnFileUploaded="rAsyncPDF_FileUploaded">
                                    </telerik:RadAsyncUpload>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="folcantidad">Folio Calidad:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addfoliocalidad" runat="server" MaxLength="20" Width="25%" Height="35px" Skin="Bootstrap" EmptyMessage="Fol. Calidad" ReadOnly="true" />
                                    <telerik:RadTextBox ID="addfoliocalidad02" runat="server" EmptyMessage="Fol. Calidad" Height="35px" Skin="Bootstrap" MaxLength="150" EnabledStyle-HorizontalAlign="center" Width="30%" OnTextChanged="addfoliocalidad02_TextChanged" AutoPostBack="true" />
                                    <telerik:RadTextBox ID="addfoliocalidad03" runat="server" MaxLength="5" Width="7%" Height="35px" Skin="Bootstrap" EmptyMessage=".." ReadOnly="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="addfoliocalidad02" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="filecantidad">Subir Archivo:</label>
                                </td>
                                <td>
                                    <telerik:RadAsyncUpload runat="server" ID="filecalidad" Skin="Bootstrap" Width="400px" MaxFileInputsCount="4"
                                        ChunkSize="8000" OnClientValidationFailed="validationFailed" ManualUpload="false"
                                        MultipleFileSelection="Disabled" AllowedFileExtensions=".pdf">
                                    </telerik:RadAsyncUpload>
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

            <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" Visible="false"
                ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />


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
