<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_infoevento.aspx.cs" Inherits="appwebcccmex.modal_cccmex_infoevento" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns='http://www.w3.org/1999/xhtml'>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>     
    <title>Información del evento</title>  
    <link href="style/modalpopup.css" rel="stylesheet"/>    
    <script src="Scripts/cccmex_modal_scripts.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 336px;
        }
    </style>
</head>

<body onload="AjustarRadWindow();">

   <form id="form1" runat="server" class="contact_form" method="post">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
                     
   <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server" Width="450px" Height="200px" HorizontalAlign="NotSet">

         <table width="450px">
        <tr>
            <td valign="top">

             <asp:Label ID="Label1" runat="server" Text="Instalación: " Font-Bold="True"></asp:Label>
            <asp:Label ID="Instalacion" runat="server" Text="Instalacion" ForeColor="#0099ff"></asp:Label>
                <br /><br />  


            <asp:Label runat="server" Text="Fecha del Evento: " Font-Bold="True"></asp:Label>
                 <asp:Label ID="FechaEvento" runat="server" Text="FechaEvento" ForeColor="#0099ff"></asp:Label>
              <br /><br />

            <asp:Label runat="server" Text="Tipo de Evento: " Font-Bold="True"></asp:Label>
             <asp:Label ID="TipoEvento" runat="server" Text="TipoEvento" ForeColor="#0099ff"></asp:Label>
             
             <br /><br /> 
            
            <asp:Label runat="server" Text="Equipo: " Font-Bold="True"></asp:Label>
            <asp:Label ID="Equipo" runat="server" Text="Equipo" ForeColor="#0099ff"></asp:Label>

             <br /><br />

            <asp:Label ID="Label2" runat="server" Text="Encargado: " Font-Bold="True"></asp:Label>
            <asp:Label ID="Responsable" runat="server" Text="Encargado" ForeColor="#0099ff"></asp:Label>

             <br /> <br />

                <telerik:RadButton ID="btnEnviar" runat="server" Skin="Silk" Visible="false" Text="Enviar Correo" OnClick="btnEnviar_Click"  >
                </telerik:RadButton>

                <br />
            </td>
        </tr>
        
    </table>
                         
      <asp:ValidationSummary ID="RequiredFieldsResumen" runat="server" ShowMessageBox="true" Visible="false" 
                 ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />
 </telerik:radajaxpanel>
   <telerik:RadWindowManager ID="VentanaRad" runat="server" Animation="Resize">
    </telerik:RadWindowManager>
  <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">
<%--      <Windows>
     <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false"
                NavigateUrl="modal_cccmex_adminbycentro.aspx" Title="Capturas">
       </telerik:RadWindow>           
      </Windows>--%>
      
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

