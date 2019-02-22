<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cccmex_modalcentros.aspx.cs" Inherits="appwebcccmex.catalogos.cccmex_modalcentros" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<% @Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />    <title>Instalaciones de centros</title>        
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
           
   <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server" Width="600px" Height="500px" HorizontalAlign="NotSet">
      
            <div>
                 <ul>
                        <li>
                             <h2><asp:Label ID="lblCentroActual" runat="server" Text="!!" /></h2>
                      <HR />
                            </li>
                        <li>
                             <asp:Label ID="lblidcentro" runat="server" Text="!!" Visible="false"> </asp:Label>                               
                            <label for="codcentro">Código Centro:</label>                          
                               <telerik:RadNumericTextBox ID="addidCentro" runat="server" Height="35px" MaxLength="9" Width="30%" EmptyMessage="Identificador" NumberFormat-GroupSeparator=""  NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>                               
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="addidCentro" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>
                            </li> 
                            <li>                              
                                 <label for="codcentro">Centro De Trabajo - Instalacion:</label> 
                               <telerik:RadTextBox ID="addcentro" runat="server" Height="35px" MaxLength="180" Width="60%" EmptyMessage="Centro de trabajo"></telerik:RadTextBox>                               
                                 <asp:RequiredFieldValidator ID="valReqaddcentro" ControlToValidate="addcentro" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>  
                            </li> 
                     
                     <li>
                            <telerik:RadButton ID="cmdcentro" runat="server" Text="Agregar" Height="35" Width="30%" OnClientClicking="RadConfirm" OnClick="cmdcentro_Click">
                             
                                </telerik:RadButton>  
                           
                      </li>                        
                    
                     <li>
             
                                   <telerik:RadGrid ID="gridcccmex" runat="server" Width="95%"  Height="250px" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridcccmex_ItemCommand" OnNeedDataSource="gridcccmex_NeedDataSource" PageSize="5">
                                       <PagerStyle Mode="NumericPages" />
                                       <mastertableview allowautomaticinserts="false" autogeneratecolumns="False" caption="LISTADO DE INSTALACIONES" clientdatakeynames="IdInst,IdCentro,Nombre" commanditemdisplay="Top" datakeynames="IdInst" font-size="13px">
                                           <commanditemtemplate>
                                               <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true">
                                                   <Items>
                                                       <telerik:RadToolBarButton CommandName="updGrid" ImageUrl="../Images/Update.png" Text="Actualizar">
                                                       </telerik:RadToolBarButton>
                                                       <telerik:RadToolBarButton CommandName="editGrid" ImageUrl="../Images/edit-29.png" Text="Editar">
                                                       </telerik:RadToolBarButton>
                                                       <telerik:RadToolBarButton CommandName="delGrid" ImageUrl="../Images/delete.png" Text="Eliminar">
                                                       </telerik:RadToolBarButton>
                                                       
                                                   </Items>
                                               </telerik:RadToolBar>
                                           </commanditemtemplate>
                                           <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                           <rowindicatorcolumn filtercontrolalttext="Filter RowIndicator column">
                                           </rowindicatorcolumn>
                                           <expandcollapsecolumn created="True" filtercontrolalttext="Filter ExpandColumn column" visible="True">
                                           </expandcollapsecolumn>
                                           <Columns>
                                               <telerik:GridBoundColumn DataField="IdInst" HeaderStyle-Width="20%" DataType="System.Int64" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center">
                                                   <ItemStyle HorizontalAlign="Center"/>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                               </telerik:GridBoundColumn>

                                                 <telerik:GridBoundColumn DataField="IdCentro" HeaderStyle-Width="20%" DataType="System.Int64" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center" Visible="false">                                                 
                                               </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn DataField="Nombre" DataType="System.String" HeaderText="CENTRO" UniqueName="Nombre">
                                               </telerik:GridBoundColumn>                                              
                                           </Columns>
                                           <editformsettings>
                                               <editcolumn filtercontrolalttext="Filter EditCommandColumn column">
                                               </editcolumn>
                                           </editformsettings>
                                           <PagerStyle PageSizeControlType="RadComboBox" />
                                       </mastertableview>
                                       <clientsettings>
                                           <Selecting AllowRowSelect="true" />
                                           <Scrolling AllowScroll="True" FrozenColumnsCount="2" SaveScrollPosition="true" UseStaticHeaders="True" />
                                       </clientsettings>
                                       <filtermenu enableimagesprites="False">
                                       </filtermenu>
                                   </telerik:RadGrid>
                          
                              </li>                        
                      </ul>           
                </div>
          

         
                           <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" 
                 ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error"                                     
                 HeaderText="Datos requeridos favor de corregir" />
                         
                               
    
 </telerik:radajaxpanel>

                  
  <%--  </div>--%>
   <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize">
    </telerik:RadWindowManager>

      

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
      <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="18px">Procesando ... </asp:Label>
     </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
