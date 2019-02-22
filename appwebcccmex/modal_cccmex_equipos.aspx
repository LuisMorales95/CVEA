<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_equipos.aspx.cs" Inherits="appwebcccmex.modal_cccmex_equipos" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns='http://www.w3.org/1999/xhtml'>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>     
    <title>Gestiòn de equipos</title>  
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
                     
   <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server" Width="650px" Height="342px" HorizontalAlign="NotSet">

         <table width="700px">
        <tr>
            <td valign="top">
            <asp:Label runat="server" Text="Centro" Font-Bold="True"></asp:Label><br />

            <telerik:RadComboBox  ID="cmbcentro" runat="server" OnItemsRequested="cmbcentro_ItemsRequested" EnableLoadOnDemand="true" OnSelectedIndexChanged="Cmbcentro_SelectedIndexChanged"
             AutoPostBack="true" EmptyMessage="- Seleccione el centro -" Width="300px" MarkFirstMatch="true" Skin="Metro" ></telerik:RadComboBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldCentro" ControlToValidate="cmbcentro" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>                      
             <br/>

            <asp:Label runat="server" Text="Instalación" Font-Bold="True"></asp:Label><br />
            <telerik:RadComboBox ID="cmbInstalacion" runat="server" AutoPostBack="true" OnItemsRequested="cmbInstalacion_ItemsRequested" EnableLoadOnDemand="true"
              EmptyMessage="- Selecciona la instalación -" Width="300px" MarkFirstMatch="true" Skin="Metro" ></telerik:RadComboBox>
             <asp:RequiredFieldValidator ID="RequiredFieldInstalacion" ControlToValidate="cmbInstalacion" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>  
             <br />
             
             <asp:Label runat="server" Text="Equipo" Font-Bold="True"></asp:Label><br />
             <telerik:RadTextBox ID="txtEquipo" Runat="server" Skin="Metro" Width="297px"></telerik:RadTextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldEquipo" ControlToValidate="txtEquipo" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
             <br />

            </td>            

            <td class="auto-style1">
            
             <asp:Label runat="server" Text="Tag" Font-Bold="True"></asp:Label><br />
                <telerik:RadTextBox ID="txtTag" Runat="server" Skin="Metro" Width="297px" CssClass="txtcss"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldTag" ControlToValidate="txtTag" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
                <br />

                <asp:Label runat="server" Text="Descripcion" Font-Bold="True"></asp:Label><br />
                <telerik:RadTextBox ID="txtDescripcion" Runat="server" TextMode="MultiLine" Skin="Metro" Width="297px" Height="73"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldDescripcion" ControlToValidate="txtDescripcion" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
                <br />           
                
             <asp:Label runat="server" Text="Detalle" Font-Bold="True"></asp:Label><br />
                <telerik:RadTextBox ID="txtDetalle" Runat="server" Skin="Metro" Width="297px">
                </telerik:RadTextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldDetalle" ControlToValidate="txtDetalle" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>   
               <br />

                <telerik:RadButton ID="btnGuadar" runat="server" Skin="Metro" Text="Guardar" OnClick="btnGuardar_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancel" runat="server" Skin="Metro" Text="Cancelar" OnClick="btnCancel_Click">
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

