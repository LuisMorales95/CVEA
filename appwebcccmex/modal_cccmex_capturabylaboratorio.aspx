<%@ Page Culture="es-MX" Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_capturabylaboratorio.aspx.cs" Inherits="appwebcccmex.modal_cccmex_capturabylaboratorio" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<% @Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />    <title>Captura de instrumentos</title>  
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
           
   <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server" Width="800px" Height="600px" HorizontalAlign="NotSet" >
       <div class="scrollable">  
            <ul>  
                <li>  
                    <h2><asp:Label ID="nombreCentro" runat="server"  Font-Bold="true" Font-Size="16px"  Text="Instalación"></asp:Label></h2>
                      <hr />
                </li>  
                <li>
                     <label for="instalacion">Pruebas:</label> 
                                <telerik:RadComboBox ID="cmbpruebas" Skin="Bootstrap" runat="server" EmptyMessage="- Selecciona la prueba -" Width="300px"  MarkFirstMatch="true" LoadingMessage="Cargando..." DropDownWidth="300px"></telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cmbpruebas" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>  
                       <hr />
                     <label for="metodo">Método:</label> 
                    <telerik:RadTextBox ID="addmetodo" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Método ASTM Utilizado"/>
                </li>  
                <li> 
                    <table style="width:95%;">
                        <tr>
                            <td>
                                <label for="disp">Dispositivo Temp.:</label> 
                                <telerik:RadTextBox ID="adddistemp" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Dispositivo de temperatura"/>
                            </td>
                            <td>
                                 <label for="ninfo1">No. Informe Temp.:</label> 
                                <telerik:RadTextBox ID="addninfo1" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="No. de informe de calibración"/>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label for="hidro">Hidrometro:</label> 
                                <telerik:RadTextBox ID="addhidro" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Hidrometro"/>
                            </td>
                            <td>
                                 <label for="ninfo2">No. Informe Hidro.:</label> 
                                <telerik:RadTextBox ID="addninfo2" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="No. de informe de calibración"/>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label for="prob">Probeta:</label> 
                                <telerik:RadTextBox ID="addprobeta" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Probeta"/>
                            </td>
                            <td>
                                 <label for="ninfo3">No. Informe Probeta:</label> 
                                <telerik:RadTextBox ID="addninfo3" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="No. de informe de calibración"/>
                            </td>                             
                        </tr>

                         <tr>
                            <td colspan="2">
                                 <hr />
                            </td>                             
                        </tr>

                      
                        <tr>
                            <td>
                                <label for="equipo">Equipo de analisis:</label> 
                                <telerik:RadTextBox ID="addequipoana" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Equipo de analisis"/>
                            </td>
                            <td>
                                 <label for="modmarca">Modelo/Marca:</label> 
                                <telerik:RadTextBox ID="addmodmarca" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Modelo ó Marca"/>
                            </td>                             
                        </tr>

                        <tr>
                            <td>
                                <label for="fecha1">Fecha Verificación:</label>
                                <telerik:RadDatePicker ID="rdpFechaMantto" Skin="Bootstrap" runat="server"
                                    DateInput-EmptyMessage="Fecha Ver. Mantto" MaxDate="01/01/3000"
                                    MinDate="01/01/1000" Width="45%" Height="30px">
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>

                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label for="ninfo4">No. Informe Equipo:</label>
                                <telerik:RadTextBox ID="addninfo4" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="No. de informe de calibración" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                 <hr />
                            </td>                             
                        </tr>

                        <tr>
                            <td>
                                <label for="estandar">Estandar de Verif. Util.:</label> 
                                <telerik:RadTextBox ID="addestandarutil" runat="server" Skin="Bootstrap" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Estandar de verificación utilizado"/>
                            </td>
                            <td>
                                 <label for="fecha2">Fecha Vig:</label> 
                                  <telerik:RadDatePicker ID="rdpFechaVig" Skin="Bootstrap" runat="server"
                                DateInput-EmptyMessage="Fecha Vigencia Estandar" MaxDate="01/01/3000"  
                                MinDate="01/01/1000" Width="45%" Height="30px">
                                <Calendar ID="Calendar2" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                                      <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" />
                     </telerik:RadDatePicker> 
                            </td>                             
                        </tr>

                         <tr>
                            <td colspan="2">
                                 <hr />
                            </td>                             
                        </tr>

                        <tr>
                            <td>
                                <label for="poro">Medida Poro:</label> 
                                <telerik:RadTextBox ID="addporomemb" Skin="Bootstrap" runat="server" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Medida de poro de membraba"/>
                            </td>
                            <td>
                                 <label for="ninfo5">Informe Calibración:</label> 
                                <telerik:RadTextBox ID="addinfocalibrbal" Skin="Bootstrap" runat="server" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Informe de calibración balanza analitica"/>
                            </td>                             
                        </tr>

                         <tr>
                            <td>
                                 <label for="ninfo6">Informe Calibración Tubo:</label> 
                                <telerik:RadTextBox ID="addinfocalibrcannon" Skin="Bootstrap" runat="server" Height="30px" MaxLength="200" Width="50%" EmptyMessage="Informe de calibración de tubo Cannon"/>
      
                            </td>  
                             <td>                                  
                                 <asp:Label id="lblfile" runat="server" Text="Archivo:"></asp:Label>
                                  <telerik:RadAsyncUpload runat="server" ID="fileLaboratorio" Width="50%" MaxFileInputsCount="1" Height="35px" 
                ChunkSize="8000" OnClientValidationFailed="validationFailed" ManualUpload="false" allowedfileextensions=".pdf">                
                </telerik:RadAsyncUpload>
                             </td>                           
                        </tr>

                    </table> 
                </li>                 
               
                <li>                   
                    <div style="text-align:right; margin-right:20px; margin-top:20px;">
                     <telerik:RadButton ID="cmdEjecuta" runat="server" Width="35%" Skin="Material" RenderMode="Lightweight"   Text="Agregar" OnClientClicking="RadConfirm" OnClick="cmdEjecuta_Click">                             
                                </telerik:RadButton>
                        </div>  
                    
                </li>
            </ul>  
        </div>  
       
                           <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" Visible="false" 
                 ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />
                         
  
 </telerik:radajaxpanel>
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
                 return ("La extención no es correcta...");
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
