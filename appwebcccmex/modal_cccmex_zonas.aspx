<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_zonas.aspx.cs" Inherits="appwebcccmex.modal_cccmex_zonas" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns='http://www.w3.org/1999/xhtml'>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestión de Zonas</title>
    <link href="style/modalpopup.css" rel="stylesheet" />
    <script src="Scripts/cccmex_modal_scripts.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 336px;
        }
    </style>
</head>


<body onload="AjustarRadWindow();">

    <form id="form" runat="server" class="contact_form" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="310px" Height="300px" HorizontalAlign="NotSet">

            <table width="300px">
                <tr>
                    <td valign="top">


                        <asp:Label runat="server" Text="Zona" Font-Bold="True"></asp:Label><br />
                        <telerik:RadTextBox ID="txtZone" runat="server" Skin="Bootstrap" Width="297px" align="center"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtZone" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                        <br />

                        <asp:Label runat="server" Text="Descripción" Font-Bold="True"></asp:Label><br />
                        <telerik:RadTextBox ID="txtDes" runat="server" Skin="Bootstrap" Width="297px" align="center"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldEquipo" ControlToValidate="txtDes" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                        <br />
                        <br />

                        <telerik:RadButton ID="RadButton1" runat="server" Skin="Material" RenderMode="Lightweight" Text="" OnClick="btnGuardar_Click">
                        </telerik:RadButton>

                        <telerik:RadButton ID="RadButton2" runat="server" Skin="Material" RenderMode="Lightweight" Text="Cancelar" OnClick="btnCancel_Click">
                        </telerik:RadButton>
                        <br />
                    </td>
                </tr>
            </table>

            <asp:ValidationSummary ID="RequiredFieldsResumen" runat="server" ShowMessageBox="true" Visible="false"
                ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />
        </telerik:RadAjaxPanel>


        <telerik:RadWindowManager ID="VentanaRad" runat="server" Animation="Resize" Skin="Bootstrap">
        </telerik:RadWindowManager>

        <telerik:RadAjaxManager ID="ManejadorRadAjax" runat="server" EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ManejadorRadAjax">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ManejadorRadAjax" LoadingPanelID="LoadingPanel"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" EnableEmbeddedSkins="false">
        </telerik:RadAjaxLoadingPanel>

    </form>
</body>


</html>
