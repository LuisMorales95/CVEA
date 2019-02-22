<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="OrderServices.aspx.cs" Inherits="appwebcccmex.catalogos.OrderServices" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
         <div class="col-md-12 grid-margin">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Orden de Servicio
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">

                            <div class="col-sm-12">
                                <%-- table grid with other skins --%>
                                <telerik:RadGrid ID="gridCapturas" runat="server" Skin="Material" RenderMode="Lightweight" Width="100%" AllowAutomaticDeletes="false"
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowSorting="false"
                                    OnItemCommand="gridCapturas_ItemCommand">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView AllowAutomaticInserts="false" NoDetailRecordsText="No se encontraron registros ..." AutoGenerateColumns="False" Caption="ORDEN DE SERVICIO" DataKeyNames="Idorden" ClientDataKeyNames="Idorden" CommandItemDisplay="Top" Font-Size="14px">
                                        <NoRecordsTemplate>
                                            <table style="width: 100%; border: 0; padding: 20px; border-spacing: 20px;">
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <h2 style="color: Black">Registros no encontrados, Favor de volver a realizar la consultar</h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </NoRecordsTemplate>
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" Skin="Material" RenderMode="Lightweight" AutoPostBack="true">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="CAPTURA ORDEN DE SERVICIO" CommandName="addGrid" ImageUrl="images/mas2.png"
                                                        Visible='<%# !gridCapturas.MasterTableView.IsItemInserted %>'>
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton CommandName="editGrid" ImageUrl="Images/edit-29.png" Text="EDITAR">
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton CommandName="deleteGrid" ImageUrl="Images/delete.png" Text="ELIMINAR">
                                                    </telerik:RadToolBarButton>
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Orden_servicio" UniqueName="orden_servicio" DataType="System.String" HeaderText="ORDEN DE SERVICIO" />
                                            <telerik:GridBoundColumn DataField="Volumen" HeaderText="VOLUMEN" DataType="System.Decimal" UniqueName="Volumen" DataFormatString="{0:#,##0.#00}">
                                                <ItemStyle HorizontalAlign="right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Mes" UniqueName="Mes" DataType="System.String" HeaderText="MES" />
                                            <telerik:GridBoundColumn DataField="Anio" UniqueName="Anio" DataType="System.String" HeaderText="AÑO" />
                                            <telerik:GridBoundColumn DataField="Fecha" DataType="System.DateTime" HeaderText="Fecha Captura"
                                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle PageSizeControlType="RadComboBox" />
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" FrozenColumnsCount="2" SaveScrollPosition="true" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <%-- RadWindowManager add skin-bootstrap --%>

    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false"
                NavigateUrl="cccmex_modalOrdenServicio.aspx" Title="Captura Acumulado Por Orden">
            </telerik:RadWindow>
        </Windows>

    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridCapturas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCapturas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="windowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCapturas" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>

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
            
        </script>
    </telerik:RadCodeBlock>

</asp:Content>

