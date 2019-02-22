<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="centrosmodalpag.aspx.cs" Inherits="appwebcccmex.catalogos.centrosmodalpag" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<% @Import Namespace="System.Globalization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Instalaciones</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <link rel="shortcut icon" href="images/logo.png" type="image/x-icon"/>
    <link rel="icon" href="images/logo.png" type="image/x-icon"/>
 <!-- Required meta tags --> 
  <!-- plugins:css -->
           <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- Custom styles for this template -->
    <link href="../css/full-slider.css" rel="stylesheet"/>

  <link rel="stylesheet" href="../vendors/iconfonts/mdi/css/materialdesignicons.min.css"/>
  <link rel="stylesheet" href="../vendors/css/vendor.bundle.base.css"/>
  <link rel="stylesheet" href="../vendors/css/vendor.bundle.addons.css"/>
  <!-- endinject -->
  <!-- plugin css for this page -->
  <!-- End plugin css for this page -->
  <!-- inject:css -->
  <link rel="stylesheet" href="../css/style.css"/> 
  <link rel="stylesheet" type="text/css" href="../css/telerikmnu.css" />
  <!-- endinject -->


</head>
<body onload="AdjustRadWidow();">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function AdjustRadWidow() {
            var oWindow = GetRadWindow();
            setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 900);
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
                    //initiate the origianal postback again
                    this.click();
                }
            });

            var text = "Are you sure you want to submit the page?";
            radconfirm(text, callBackFunction, 200, 200, null, "RadConfirm");
            //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
            args.set_cancel(true);
        }


    </script>
 

        
     <form id="form1" runat="server">
        <%-- <telerik:RadFormDecorator ID="Radformdecorator1" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
       


        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="900px" Height="680px" Style="margin: 10px;"> --%>
          <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
         <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server" Width="700px" HorizontalAlign="NotSet">
      
        <div class="row col-12" style="margin-top:10px; margin-left:50px; width:100%">
              
            <div class="col-md-12 d-flex align-items-stretch">
                <div class="row flex-grow">
                    <div class="col-12">
                        
                        <div class="card">
                            <div class="card-header bg-light">
                               <asp:Label ID="lblCentroActual" runat="server" Text="!!" />
                            </div>
                            <div class="card-body">

                            <div class="form-group row">
                                 <asp:Label ID="lblidcentro" runat="server" Text="!!" Visible="false"> </asp:Label>      <br />
                                          <label class="col-sm-3 col-form-label" for="lblc">Código Centro:</label>
                                   
                                    <div class="col-sm-5">
                                      <telerik:RadNumericTextBox ID="addidCentro" runat="server" Skin="MetroTouch"  MaxLength="9" Width="100%" EmptyMessage="Identificador" NumberFormat-GroupSeparator=""  NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>                               
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="addidCentro" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>
                                    </div>
                                  </div>

                                <div class="form-group row">                                  
                                   <label class="col-sm-3 col-form-label" for="lblc2">Centro De Trabajo - Instalacion:</label> 

                                        <div class="col-sm-9">
                                                                        
                                         <telerik:RadTextBox ID="addcentro" runat="server" Skin="MetroTouch" MaxLength="180" Width="100%" EmptyMessage="Centro de trabajo"></telerik:RadTextBox>                               
                                 <asp:RequiredFieldValidator ID="valReqaddcentro" ControlToValidate="addcentro" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>  
                                   
                                            </div>


                                    </div>

                                 <div class="col-sm-4 offset-3 text-center">
                                       <telerik:RadButton ID="cmdcentro" runat="server" Text="Agregar"  Skin="Material" RenderMode="Lightweight" Width="100%" OnClientClicking="RadConfirm" OnClick="cmdcentro_Click">
                             
                                </telerik:RadButton>  
                                     </div>

                                <br />

                                <div class="form-group row">   
                                  <div class="col-sm-12">
                                       <telerik:RadGrid ID="gridcccmex" runat="server" Width="100%"  Height="250px" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridcccmex_ItemCommand" OnNeedDataSource="gridcccmex_NeedDataSource" PageSize="20">
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
                                      </div>
                                    </div>
                                
                                <div class="col-sm-12">
                                    
                           <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" 
                 ShowSummary="true" DisplayMode="BulletList" CssClass="lert-dark"                                     
                 HeaderText="Datos requeridos favor de corregir" />
                                    </div>

                                </div>
 
                            </div>
                     

                        </div>
                    </div>

                </div>
            </div>

             </telerik:radajaxpanel>
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize">
    </telerik:RadWindowManager>

      

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
      <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="18px">Procesando ... </asp:Label>
     </telerik:RadAjaxLoadingPanel>
          
 </form>

    <!-- container-scroller -->
  <!-- plugins:js -->
  <script src="../vendors/js/vendor.bundle.base.js"></script>
  <script src="../vendors/js/vendor.bundle.addons.js"></script>
      <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
  <!-- endinject -->
  <!-- Plugin js for this page-->
  <!-- End plugin js for this page-->
  <!-- inject:js -->
  <script src="../js/off-canvas.js"></script>
  <script src="../js/misc.js"></script>
 
  <!-- endinject -->
  <!-- Custom js for this page-->
  <!-- End custom js for this page-->
</body>
</html>
