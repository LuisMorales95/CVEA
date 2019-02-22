<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cccmex_productos.aspx.cs" Inherits="appwebcccmex.catalogos.cccmex_productos" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

      <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
         <hgroup class="title">
        <h1>Productos</h1>       
    </hgroup>

            <fieldset>
                 <ol>
                        <li>
                               <asp:Label ID="lblid" runat="server" Text="Código Producto"> </asp:Label>
                            
                               <telerik:RadNumericTextBox ID="addidproducto" runat="server" Height="35px" MaxLength="9" Width="30%" EmptyMessage="Identificador" NumberFormat-GroupSeparator=""  NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>                               
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="addidproducto" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>  
                            </li> 
                            <li>
                               <asp:Label ID="lblnombre" runat="server" Text="Producto"> </asp:Label>
                               <telerik:RadTextBox ID="addproducto" runat="server" Height="35px" MaxLength="190" Width="60%" EmptyMessage="Producto"></telerik:RadTextBox>                               
                                 <asp:RequiredFieldValidator ID="valReqaddcentro" ControlToValidate="addproducto" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>  
                            </li> 
                     
                     <li>
                            <telerik:RadButton ID="cmdEjecuta" runat="server" Text="Agregar" Height="35" Width="20%" OnClientClicking="RadConfirm" OnClick="cmdEjecuta_Click">                             
                                </telerik:RadButton>  
                           
                      </li>                        
                      </ol>
                                           
                </fieldset>
          

         
                           <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" 
                 ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error"                                     
                 HeaderText="Datos requeridos, favor de corregir" />
          

                           <p>
                               <telerik:RadGrid ID="gridcccmex" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridcccmex_ItemCommand" OnNeedDataSource="gridcccmex_NeedDataSource" PageSize="10" Width="80%">
                                   <PagerStyle Mode="NumericPages" />
                                   <mastertableview allowautomaticinserts="false" autogeneratecolumns="False" caption="LISTADO DE PRODUCTOS" clientdatakeynames="IdProducto,Producto" commanditemdisplay="Top" datakeynames="IdProducto" font-size="14px">
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
                                           <telerik:GridBoundColumn DataField="IdProducto" DataType="System.Int64" HeaderStyle-Width="20%" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center">
                                               <ItemStyle HorizontalAlign="Center" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                           </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="Producto" DataType="System.String" HeaderText="PRODUCTO" UniqueName="Producto">
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
                              
           </p>
                               
      </telerik:RadAjaxPanel>

      <telerik:RadWindowManager ID="windowManager1" runat="server"/>
     

     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true"  OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>

              <telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>                    
                      <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" /> 
                          
                </UpdatedControls>
            </telerik:AjaxSetting> 

                <telerik:AjaxSetting AjaxControlID="gridcccmex" >
                    <UpdatedControls>                        
                           <telerik:AjaxUpdatedControl ControlID="windowManager1"/>
                           <telerik:AjaxUpdatedControl ControlID="gridcccmex"/>
                            <telerik:AjaxUpdatedControl ControlID="cmdEjecuta"/>
                             <telerik:AjaxUpdatedControl ControlID="addidproducto"/>
                              <telerik:AjaxUpdatedControl ControlID="addproducto"/>
                    </UpdatedControls>
               </telerik:AjaxSetting>

                 <telerik:AjaxSetting AjaxControlID="cmdEjecuta" >
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="gridcccmex"></telerik:AjaxUpdatedControl>
                           <telerik:AjaxUpdatedControl ControlID="windowManager1"/> 
                             <telerik:AjaxUpdatedControl ControlID="addidproducto"/>
                              <telerik:AjaxUpdatedControl ControlID="addproducto"/>
                    </UpdatedControls>
               </telerik:AjaxSetting>
                                
    
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridcccmex" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>                    
                </UpdatedControls>
            </telerik:AjaxSetting>     
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
      <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="18px">Procesando ... </asp:Label>
     </telerik:RadAjaxLoadingPanel>
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">


                    //RadConfirm
                    function RadConfirm(sender, args) {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                this.click();
                            }
                        });

                        var text = "Esta seguro?";
                        radconfirm(text, callBackFunction, 300, 200, null, "Confirmación");
                        args.set_cancel(true);
                    }
                    //End RadConfirm

                    function callConfirm() {
                        radconfirm('Estas seguro?', confirmCallBackFn, 300, 200, null, "Confirmación");
                    }
                    function confirmCallBackFn(arg) {
                        var ajaxManager = $find("<%=RadAjaxManager1.ClientID%>");
                        if (arg) {
                            ajaxManager.ajaxRequest('oka');
                        }
                        else {
                            ajaxManager.ajaxRequest('cancel');
                        }
                    }

        </script>
            </telerik:RadCodeBlock>
</asp:Content>
