<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cccmex_modalUsuarios.aspx.cs" Inherits="appwebcccmex.catalogos.cccmex_modalUsuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Instalaciones de centros</title>
    <link href="../style/modalpopup.css" rel="stylesheet" />
</head>
<body onload="AdjustRadWidow();">
    <form id="form1" runat="server" method="post" class="contact_form">
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
                setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 800);
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

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="600px" Height="430px" HorizontalAlign="NotSet">


            <div>
                <ul>
                    <li>
                        <h2>
                            <asp:Label ID="lblCentroActual" runat="server" Text="!!"> </asp:Label></h2>
                        <hr />
                    </li>
                    <li>
                        <table>
                            <tr>
                                <td>
                                    <label for="user">Usuario:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="addUser" Skin="Bootstrap" MaxLength="20" LabelWidth="64px" ValidationGroup="add" Height="35px" Width="50%" EmptyMessage="User Name" />
                                    <asp:RequiredFieldValidator ID="valReqAddUsuario" ControlToValidate="addUser" runat="server" ErrorMessage="El usuario es requerido" ValidationGroup="add" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="pwd">Password:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="addPwd" Skin="Bootstrap" MaxLength="40" EmptyMessage="Contraseña" LabelWidth="64px" ValidationGroup="add" Height="35px" Width="50%" ForeColor="Red" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="addPwd" runat="server" ErrorMessage="La contraseña del usuario es requerida" ValidationGroup="add" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="nombre">Nombre Completo:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="addNombre" Skin="Bootstrap" MaxLength="200" EmptyMessage="Nombre completo" LabelWidth="64px" ValidationGroup="add" Height="35px" Width="85%" />
                                    <asp:RequiredFieldValidator ID="valReqAddNombre" ControlToValidate="addNombre" runat="server" ErrorMessage="El nombre del usaurio es requerido" ValidationGroup="add" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="mail">Correo Electrónico:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="addCorreo" Skin="Bootstrap" EmptyMessage="Correo Electrónico" Height="35px" Width="85%" MaxLength="200" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="add" runat="server" Display="Dynamic" ControlToValidate="addCorreo" ErrorMessage="Correo electronico invalido, favor de corregir" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="priv">Privilegios:</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbActivo" runat="server" CssClass="form-check" Text="Coordinador" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="ckbPemex" runat="server" CssClass="form-check" Text="SubGerencia" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="ckbAdmin" runat="server" CssClass="form-check" Text="Administrador" />
                                </td>
                            </tr>
                        </table>

                    </li>
                    <li>
                        <label for="btn"></label>
                        <telerik:RadButton ID="cmdedit" runat="server" Text="Agregar" Skin="Material" RenderMode="Lightweight" OnClientClicking="RadConfirm" OnClick="cmdedit_Click" />
                    </li>


                </ul>

            </div>


            <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true"
                ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />


        </telerik:RadAjaxPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
        </telerik:RadWindowManager>
    </form>
</body>
</html>
