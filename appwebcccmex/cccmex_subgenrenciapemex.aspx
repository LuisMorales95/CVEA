<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cccmex_subgenrenciapemex.aspx.cs" Inherits="appwebcccmex.cccmex_subgenrenciapemex" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
     <hgroup class="title">
        <h1>SUBGERENCIA PEMEX</h1>       
    </hgroup>

            <fieldset>
                 <ol>
                     <li>
                         <telerik:RadPanelBar ID="RadPanelBar1" runat="server" ExpandMode="MultipleExpandedItems" Width="100%">
                             <Items>
                                 <telerik:RadPanelItem Text="Datos generales de consulta" Expanded="false" ForeColor="Silver" Font-Bold="true" Font-Size="14px">
                                     <Items>
                                         <telerik:RadPanelItem Value="info" runat="server">
                                     <ItemTemplate>
                                         <table style="border-spacing:5px; border-collapse:separate; text-align:left; margin:auto; font-size:16px; width:90%;">
                                              <tr>
                                                 <td>
                                                    <asp:Label ID="lbl1" runat="server" Text="Centro:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadComboBox ID="cmbcentro" runat="server" AutoPostBack="true" EmptyMessage="- Seleccione el centro -" Width="410px" MarkFirstMatch="true" ></telerik:RadComboBox>
                                                 </td>
                                             </tr>
                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Producto:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadComboBox ID="cmbproducto" runat="server" AutoPostBack="true" EmptyMessage="- Seleccione el producto -" Width="410px" MarkFirstMatch="true" ></telerik:RadComboBox>
                                                 </td>
                                             </tr>
                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Servicio:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadComboBox ID="cmbservicio" runat="server" AutoPostBack="true" EmptyMessage="- Seleccione el servicio -" Width="410px" MarkFirstMatch="true" ></telerik:RadComboBox>
                                                 </td>
                                             </tr>
                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Estatus Rev.:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadButton ID="rbtDefault" runat="server" Width="60%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Por Default [N] - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar por default"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>

                                                      <telerik:RadButton ID="rbtRevisado" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="En Revisado [R] - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar los revisador"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                    <br />
                                                      <telerik:RadButton ID="rbtTramite" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="En Trámite [T] - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar en trámite"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>

                                                      <telerik:RadButton ID="rbtCancel" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Cancelados [C] - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar los cancelados"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                 </td>
                                             </tr>
                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Estatus Pago:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadButton ID="rbtaceptadopag" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton02">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Pago Aceptado [A] - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar por pago aceptado"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>

                                                      <telerik:RadButton ID="rbtarechazadopag" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton02">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Pago Pendiente [P] - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar por pago pendiente"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                     </td>
                                                   </tr>

                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Barco Importación:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadButton ID="rbtnBarco" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton02">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Barco Importación - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar por Barco Importación"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                   
                                                     </td>
                                                   </tr>

                                              <tr>
                                                 <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Fecha:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadDatePicker ID="rdpFechaIni" runat="server" AutoPostBack="true" 
                                                                DateInput-EmptyMessage="Fecha Inicial" MaxDate="01/01/3000"  
                                                                MinDate="01/01/1000" Width="45%" Height="35px">
                                                                <Calendar ID="Calendar3" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                                        </telerik:RadCalendarDay>
                                                                    </SpecialDays>
                                                                </Calendar>
                                                          <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" />
                                                     </telerik:RadDatePicker>  

                                                      <telerik:RadDatePicker ID="rdpFechaFin" runat="server" AutoPostBack="true"
                                                                DateInput-EmptyMessage="Fecha Final" MaxDate="01/01/3000"  
                                                                MinDate="01/01/1000" Width="45%" Height="35px">
                                                                <Calendar ID="Calendar1" runat="server">
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
                                                 <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Año/Mes:"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <telerik:RadButton ID="rbtAnio" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton03">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Año Fecha Inicial - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar por año fecha inical"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>

                                                      <telerik:RadButton ID="rbtAniomes" runat="server" Width="45%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton03">
                                                        <ToggleStates>
                                                            <telerik:RadButtonToggleState Text="Mes/Año Fecha Inicial - OK"></telerik:RadButtonToggleState>
                                                            <telerik:RadButtonToggleState Text="Filtrar por mes/año fecha inicial"></telerik:RadButtonToggleState>
                                                        </ToggleStates>
                                                    </telerik:RadButton>
                                                     </td>
                                                   </tr>
                                              <tr>                                                 
                                                 <td colspan="2" style="text-align:right;">
                                                      <telerik:RadButton ID="cmdreset" runat="server" Width="45%"  Height="35px" Font-Bold="true" Font-Size="16px"  Text="Resetear parametros" OnClientClicking="RadConfirm" OnClick="cmdreset_Click"/>                             
                                                      <telerik:RadButton ID="cmdejecuta" runat="server" Width="45%"  Height="35px" Font-Bold="true" Font-Size="16px" Text="Realizar consulta" OnClick="cmdejecuta_Click"/>                             
                                                     
                                                 </td>
                                             </tr>
                                         </table>
                                     </ItemTemplate>                                             
                                         </telerik:RadPanelItem>
                                     </Items>
                                 </telerik:RadPanelItem>
                             </Items>

                         </telerik:RadPanelBar>
                     </li>
                   
                      </ol>
                                           
                </fieldset>

           <p>
               <telerik:RadGrid ID="gridCapturas" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridCapturas_ItemCommand" OnNeedDataSource="gridCapturas_NeedDataSource" PageSize="10">
                                   <PagerStyle Mode="NumericPages" />
                                   <mastertableview allowautomaticinserts="false" NoDetailRecordsText="No se encontraron registros ..." autogeneratecolumns="False" caption="CAPTURAS" clientdatakeynames="IdReg,IdProducto,IdCentro,IdInst,IdServicio,IdBarco,Folio_cert_calidad,Folio_cert_cant,Referencia_folio" commanditemdisplay="Top" datakeynames="IdReg" font-size="11px">
                                      <NoRecordsTemplate>
                                            <table style="width:100%; border:0; padding:20px; border-spacing:20px;">
                                                <tr>
                                                    <td style="text-align:center;">
                                                        <h2 style="color:Black">Registros no encontrados, Favor de volver a realizar la consultar</h2>
                                                    </td>
                                                </tr>
                                            </table>
                                         </NoRecordsTemplate>
                                       <commanditemtemplate>                                           
                                             <asp:ImageButton ID="filegridaccess" ImageUrl="Images/Freeform-Access-32.png" Width="32px" Height="32px" runat="server"
                                                CommandName="fileGrid" ToolTip="Exportar archivo a excel"/>
                                            <asp:ImageButton ID="filegridzip" ImageUrl="Images/Zip-32.png" Width="32px" Height="32px" runat="server"
                                                CommandName="filezipGrid" ToolTip="Bajar archivos pdf en ZIP"/> 
                                       </commanditemtemplate>
                                       <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                       <rowindicatorcolumn filtercontrolalttext="Filter RowIndicator column">
                                       </rowindicatorcolumn>
                                       <expandcollapsecolumn created="True" filtercontrolalttext="Filter ExpandColumn column" visible="True">
                                       </expandcollapsecolumn>
                                       <Columns>

                                     <telerik:GridBoundColumn DataField="IdReg" DataType="System.Int64" HeaderText="IDREG" Visible="false" />
                                     <telerik:GridBoundColumn DataField="IdProducto" DataType="System.Int64" HeaderText="IDPROD" Visible="false" />
                                     <telerik:GridBoundColumn DataField="IdCentro" DataType="System.Int64" HeaderText="IDCENTRO" Visible="false" /> 
                                     <telerik:GridBoundColumn DataField="IdInst" DataType="System.Int64" HeaderText="IDINST" Visible="false" /> 
                                     <telerik:GridBoundColumn DataField="IdServicio" DataType="System.String" HeaderText="IDSERV" Visible="false" /> 
                                     <telerik:GridBoundColumn DataField="IdBarco" DataType="System.Int64" HeaderText="IDBARCO" Visible="false" />  
                                       
                                     <telerik:GridBoundColumn DataField="Referencia_folio" DataType="System.Int64" HeaderText="REF_FOLIO" Visible="false" />   

                                            <telerik:GridBoundColumn DataField="Estatus_revisado" DataType="System.String" HeaderText="E. Rev." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65px" >
                                     <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Estatus_pagado" DataType="System.String" HeaderText="E. Pag." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65px">
                                     <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Orden_servicio" DataType="System.String" HeaderText="No. Orden" ItemStyle-HorizontalAlign="Center" >
                                     <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="NombreCentro" DataType="System.String" HeaderText="Centro" UniqueName="NombreCentro"/>
                                    <telerik:GridBoundColumn DataField="NombreServicio" DataType="System.String" HeaderText="Servicio" UniqueName="NombreServicio"/>
                                    <telerik:GridBoundColumn DataField="NombreProducto" DataType="System.String" HeaderText="Producto" UniqueName="NombreProducto"/>
                                           <telerik:GridBoundColumn DataField="NombreBarco" DataType="System.String" HeaderText="Barco" Visible="true" />

                                    <telerik:GridBoundColumn DataField="Cant_insp_mezcla" HeaderText="Vol. Certificado" DataType="System.Decimal" UniqueName="Cant_insp_mezcla" DataFormatString="{0:### ##0.000}">
                                        <ItemStyle HorizontalAlign="right"/>
                                      </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Fecha" DataType="System.DateTime" HeaderText="Fecha"   HeaderStyle-Width="10%"
                                    DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center">
                                     <ItemStyle HorizontalAlign="Left"/>
                                    </telerik:GridBoundColumn> 

                                    <telerik:GridBoundColumn DataField="Folio_cert_calidad" DataType="System.String" HeaderText="F. Calidad" UniqueName="Folio_cert_calidad" Visible="false"/>
                                    <telerik:GridBoundColumn DataField="Folio_cert_cant" DataType="System.String" HeaderText="F. Cantidad" UniqueName="Folio_cert_cant" Visible="false"/>

                                            <telerik:GridTemplateColumn HeaderText="Archivos" DataType="System.String" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" >
                                    <ItemTemplate>
                                    <asp:ImageButton ID="filecertificacion" ImageUrl="images/Pdf-48.png" Width="25px" Height="25px" runat="server"
                                     CommandName="pdfArchivos" ToolTip="Descarga certificado de calidad  y cantidad en pdf"/>                                                                                                        
                                    </ItemTemplate>
                  </telerik:GridTemplateColumn>
                                                            

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
                                        <ClientEvents OnRowDblClick="RowDblClick"/>                                       
                                   </clientsettings>
                                   <filtermenu enableimagesprites="False">
                                   </filtermenu>
                               </telerik:RadGrid>
         
               <p>
               </p>
         
        </p>

 </telerik:RadAjaxPanel>

  <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">
      <Windows>
     <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false"
                NavigateUrl="modal_cccmex_subgerenciapemex.aspx" Title="Capturas">
       </telerik:RadWindow>           
      </Windows>
      
      </telerik:RadWindowManager>

       <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
          <AjaxSettings>
               <telerik:AjaxSetting AjaxControlID="gridCapturas" >
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="gridCapturas"></telerik:AjaxUpdatedControl>
                           <telerik:AjaxUpdatedControl ControlID="windowManager1"/>
                    </UpdatedControls>
               </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="filegridaccess" >
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="gridCapturas" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>                         
                    </UpdatedControls>
               </telerik:AjaxSetting>


               <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCapturas" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>           
          </AjaxSettings>
            <ClientEvents OnRequestStart="onRequestStart" />
     </telerik:RadAjaxManager>

     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" EnableEmbeddedSkins="false">
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

         function refreshGrid(arg) {
             if (!arg) {
                 $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
             }
             else {
                 $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
             }
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
         
         function onRequestStart(sender, args) {
             if (args.get_eventTarget().indexOf("filegridaccess") >= 0) {
                 args.set_enableAjax(false);
             }
             if (args.get_eventTarget().indexOf("filegridzip") >= 0) {
                 args.set_enableAjax(false);
             }
             if (args.get_eventTarget().indexOf("pdfArchivos") >= 0) {
                 args.set_enableAjax(false);
             }
          
         }

         var isDoubleClick = false;
         var clickHandler = null;
         var ClickedIndex = null;

         function RowDblClick(sender, args) {
             ClickedIndex = args._itemIndexHierarchical;
             isDoubleClick = true;
             if (clickHandler) {
                 window.clearTimeout(clickHandler);
                 clickHandler = null;
             }
             clickHandler = window.setTimeout(ActualClick, 200);
         }

         function ActualClick() {
             if (isDoubleClick) {
                 var grid = $find("<%=gridCapturas.ClientID %>");
                 if (grid) {
                     var MasterTable = grid.get_masterTableView();
                     var Rows = MasterTable.get_dataItems();
                     if (Rows.length > 0) {
                         var row = Rows[0];
                         if (ClickedIndex != null && ClickedIndex != undefined) {
                             MasterTable.fireCommand("RowDoubleClick", ClickedIndex);
                         }
                     }
                 }
             }
         }
        </script>
        </telerik:RadCodeBlock>
</asp:Content>
