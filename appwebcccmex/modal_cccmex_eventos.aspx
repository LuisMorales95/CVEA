<%@ Page Culture="es-MX" Title="" Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_eventos.aspx.cs" Inherits="appwebcccmex.Diagramas2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<% @Import Namespace="System.Globalization" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns='http://www.w3.org/1999/xhtml'>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>     
    <title>Gestiòn de eventos</title>  
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
                     
   <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server" Width="630px" Height="400px" HorizontalAlign="NotSet">

         <table width="700px">
        <tr>
            <td valign="top">
            <asp:Label runat="server" Text="Centro" Font-Bold="True"></asp:Label><br />

            <telerik:RadComboBox  ID="cmbcentro" runat="server" OnItemsRequested="cmbcentro_ItemsRequested" EnableLoadOnDemand="true" OnSelectedIndexChanged="Cmbcentro_SelectedIndexChanged"
             AutoPostBack="true" EmptyMessage="- Seleccione el centro -" Width="300px" MarkFirstMatch="true" Skin="Bootstrap" ></telerik:RadComboBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldCentro" ControlToValidate="cmbcentro" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>                      
             <br/>

            <asp:Label runat="server" Text="Instalación" Font-Bold="True"></asp:Label><br />
            <telerik:RadComboBox ID="cmbInstalacion" runat="server" AutoPostBack="true" OnItemsRequested="cmbInstalacion_ItemsRequested" EnableLoadOnDemand="true" OnSelectedIndexChanged="cmbInstalacion_SelectedIndexChanged"
              EmptyMessage="- Selecciona la instalación -" Width="300px" MarkFirstMatch="true" Skin="Bootstrap" ></telerik:RadComboBox>
             <asp:RequiredFieldValidator ID="RequiredFieldInstalacion" ControlToValidate="cmbInstalacion" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>  
             <br />

            <asp:Label runat="server" Text="Equipo" Font-Bold="True"></asp:Label><br />

            <telerik:RadComboBox ID="cmbEquipo" runat="server" AutoPostBack="true" OnItemsRequested="cmbEquipo_ItemsRequested" EnableLoadOnDemand="true" OnSelectedIndexChanged="cmbEquipo_SelectedIndexChanged" 
              EmptyMessage="- Selecciona la instalación -" Width="300px" MarkFirstMatch="true" Skin="Bootstrap" ></telerik:RadComboBox>
             <asp:RequiredFieldValidator ID="RequiredFieldEquipo" ControlToValidate="cmbEquipo" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>  
             <br />
            
            <asp:Label runat="server" Visible ="false" Text="Detalle del Equipo"></asp:Label><br />
            
            <asp:Label runat="server" Visible ="false"  ID="lbinfoeequipo" Width="268px"></asp:Label>
            
            </td>            

            <td class="auto-style1">
            <asp:Label runat="server" Text="Tipo Evento" Font-Bold="True"></asp:Label><br />
            <telerik:RadComboBox ID="cboTipoEvento" Runat="server" Width="300px" Skin="Bootstrap">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Mantenimiento" Value="Mantenimiento" />
                    <telerik:RadComboBoxItem runat="server" Text="Calibracion" Value="Calibracion" />
                </Items>
            </telerik:RadComboBox>
             <asp:RequiredFieldValidator ID="RequiredFieldTipoEvento" ControlToValidate="cboTipoEvento" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>  
            <br/>

            <asp:Label runat="server" Text="Evento" Font-Bold="True"></asp:Label><br />
            <telerik:RadTextBox ID="txtEvento" Width="301px" runat="server" TextMode="MultiLine" Height="73px" Skin="Bootstrap"></telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldEvento" ControlToValidate="txtEvento" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>  
            <br/>

            <asp:Label runat="server" Text="Fecha evento" Font-Bold="True"></asp:Label><br />
                <telerik:RadDatePicker ID="txtFecha" Runat="server" Width="297px" WrapperTableCaption="">
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldFecha" ControlToValidate="txtFecha" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>            
                <br/>
             <asp:Label runat="server" Text="Comentarios sobre el Evento" Font-Bold="True"></asp:Label>
                <br />
            <telerik:RadTextBox ID="txtComentario" Width="298px" runat="server" TextMode="MultiLine" Height="73px" Skin="Bootstrap"></telerik:RadTextBox>
            
                <asp:Label runat="server" Text="Vigencia (Duración)" Font-Bold="True" Visible="false"></asp:Label><br />
                <telerik:RadTextBox ID="txtVigencia" Runat="server" Text="30" Skin="Bootstrap" Width="297px" Visible="false"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldVigencia" ControlToValidate="txtVigencia" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
                <br />
            
                <asp:Label runat="server" Text="Pre-Alarma" Visible="false" Font-Bold="True"></asp:Label><br />
                <telerik:RadTextBox ID="txtPreAlarma" Visible="false" Text="30" Runat="server" Skin="Bootstrap" Width="297px"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldPreAlarma" ControlToValidate="txtPreAlarma" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
                <br />
            <asp:Label runat="server" Text="Post-Alarma" Visible="false" Font-Bold="True"></asp:Label><br />
                <telerik:RadTextBox ID="txtPostAlarma" Visible="false" Text="30" Runat="server" Skin="Bootstrap" Width="297px">
                </telerik:RadTextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldPostAlarma" ControlToValidate="txtPostAlarma" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
              
                <br />
                <telerik:RadButton ID="btnGuadar" runat="server" Skin="Material" RenderMode="Lightweight" Text="Guardar" OnClick="btnGuardar_Click">
                </telerik:RadButton>
                 <telerik:RadButton ID="btnHistorial" runat="server" Visible="false" Skin="Material" RenderMode="Lightweight" Text="Archivar" OnClick="btnHistorial_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancel" runat="server" Skin="Material" RenderMode="Lightweight" Text="Cancelar" OnClick="btnCancel_Click">
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

