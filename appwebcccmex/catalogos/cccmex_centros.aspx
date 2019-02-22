<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cccmex_centros.aspx.cs" Inherits="appwebcccmex.catalogos.cccmex_centros" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

      <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
         <hgroup class="title">
        <h1>Centros de trabajo</h1>       
    </hgroup>

            <fieldset>
                 <ol>
                        <li>
                               <asp:Label ID="Label1" runat="server" Text="Código Centro"> </asp:Label>
                            
                               <telerik:RadNumericTextBox ID="addidCentro" runat="server" Height="35px" ReadOnly="true" MaxLength="9" Width="30%" EmptyMessage="Identificador" NumberFormat-GroupSeparator=""  NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>                               
                                
                            </li> 
                            <li>
                               <asp:Label ID="lblcentro" runat="server" Text="Centro De Trabajo"> </asp:Label>
                               <telerik:RadTextBox ID="addcentro" runat="server" Height="35px" MaxLength="180" Width="60%" EmptyMessage="Centro de trabajo"></telerik:RadTextBox>                               
                                 <asp:RequiredFieldValidator ID="valReqaddcentro" ControlToValidate="addcentro" runat="server" 
                       ValidationGroup="get"></asp:RequiredFieldValidator>  
                            </li> 
                     
                     <li>
                            <telerik:RadButton ID="cmdcentro" runat="server" Text="Agregar" Height="35" Width="20%" OnClientClicking="RadConfirm" OnClick="cmdcentro_Click">
                             
                                </telerik:RadButton>  
                           
                      </li>                        
                      </ol>
                                           
                </fieldset>
          

         
                           <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" 
                 ShowSummary="true" DisplayMode="BulletList" CssClass="text-danger"                                     
                 HeaderText="Datos requeridos" />
                         
                               <p>
                                   <telerik:RadGrid ID="gridcccmex" runat="server" Width="80%" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridcccmex_ItemCommand" OnNeedDataSource="gridcccmex_NeedDataSource" PageSize="10">
                                       <PagerStyle Mode="NumericPages" />
                                       <mastertableview allowautomaticinserts="false" autogeneratecolumns="False" caption="LISTADO DE CENTROS" clientdatakeynames="IdCentro,Centro" commanditemdisplay="Top" datakeynames="IdCentro" font-size="14px">
                                           <commanditemtemplate>
                                               <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true">
                                                   <Items>
                                                       <telerik:RadToolBarButton CommandName="updGrid" ImageUrl="../Images/Update.png" Text="Actualizar">
                                                       </telerik:RadToolBarButton>
                                                       <telerik:RadToolBarButton CommandName="editGrid" ImageUrl="../Images/edit-29.png" Text="Editar">
                                                       </telerik:RadToolBarButton>
                                                       <telerik:RadToolBarButton CommandName="delGrid" ImageUrl="../Images/delete.png" Text="Eliminar">
                                                       </telerik:RadToolBarButton>
                                                         <telerik:RadToolBarButton CommandName="addGrid" ImageUrl="../Images/mas3.png" Text="Agregar Instalaciones">
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
                                               <telerik:GridBoundColumn DataField="IdCentro" HeaderStyle-Width="20%" DataType="System.Int64" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center">
                                                   <ItemStyle HorizontalAlign="Center"/>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                               </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn DataField="Centro" DataType="System.String" HeaderText="CENTRO" UniqueName="Centro">
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

     <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">
      <Windows>
     <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move,Resize" Modal="true" VisibleStatusbar="false"
                NavigateUrl="cccmex_modalcentros.aspx" Title="Agregando instalaciones al centro">
       </telerik:RadWindow>           
      </Windows>
      
      </telerik:RadWindowManager>
     

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
                            <telerik:AjaxUpdatedControl ControlID="cmdcentro"/>
                             <telerik:AjaxUpdatedControl ControlID="addidCentro"/>
                              <telerik:AjaxUpdatedControl ControlID="addcentro"/>
                    </UpdatedControls>
               </telerik:AjaxSetting>

                 <telerik:AjaxSetting AjaxControlID="cmdcentro" >
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="gridcccmex"></telerik:AjaxUpdatedControl>
                           <telerik:AjaxUpdatedControl ControlID="windowManager1"/> 
                             <telerik:AjaxUpdatedControl ControlID="addidCentro"/>
                              <telerik:AjaxUpdatedControl ControlID="addcentro"/>
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


                    //Grid Empresas - Operaciones
                    function openRadWindow() {
                        var radwindow = $find('<%=RadWindow1.ClientID %>');
             radwindow.show();
                    }

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
