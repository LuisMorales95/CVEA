<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="usermanagement.aspx.cs" Inherits="appwebcccmex.catalogos.usermanagement" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Servicio
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">
                            <div class="form-group row">

                                <div class="col-sm-12">
                                    <label class="col-form-label col-sm-2" for="lblc">Centro</label>
                                    <div class=" col-sm-4">
                                        <telerik:RadComboBox ID="cmbCentro" runat="server" Skin="Bootstrap" Height="200" Width="100%"
                                            DropDownWidth="600" EmptyMessage="Seleccione el centro de trabajo" HighlightTemplatedItems="true"
                                            EnableLoadOnDemand="true" Filter="StartsWith"
                                            OnItemsRequested="cmbCentro_ItemsRequested"
                                            OnSelectedIndexChanged="cmbCentro_SelectedIndexChanged" AutoPostBack="true">
                                            <HeaderTemplate>
                                                <table style="width: 500px; border-spacing: 0; border-collapse: collapse; padding: 0;">
                                                    <tr>
                                                        <td style="width: 350px;">Centro de trabajo
                                                        </td>
                                                        <td style="width: 200px;">Código
                                                        </td>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 500px; border-spacing: 0; border-collapse: collapse; padding: 0;">
                                                    <tr>
                                                        <td style="width: 350px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['Nombre']")%>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['Codigo']")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                            </div>

                            <hr />

                            <div class="col-sm-12">
                                <telerik:RadGrid ID="gridUsuarios" runat="server" Skin="Material" RenderMode="Lightweight" 
                                    AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                    AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridUsuarios_ItemCommand" 
                                    OnNeedDataSource="gridUsuarios_NeedDataSource" PageSize="10" Width="100%">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView AllowAutomaticInserts="false" NoDetailRecordsText="No se encontraron registros ..." AutoGenerateColumns="False" Caption="LISTADO DE USUARIOS" ClientDataKeyNames="IappId,IdCentro,Nombre_Centro" CommandItemDisplay="Top" DataKeyNames="IappId" Font-Size="14px">
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
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" Skin="Material" RenderMode="Lightweight" Width="100%" AutoPostBack="true">
                                                <Items>
                                                    <telerik:RadToolBarButton CommandName="updGrid" ImageUrl="../Images/Update.png" Text="Actualizar">
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton Text="Agregar Usuario" CommandName="addGrid" ImageUrl="../images/mas2.png"
                                                        Visible='<%# !gridUsuarios.MasterTableView.IsItemInserted %>'>
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton CommandName="editGrid" ImageUrl="../Images/edit-29.png" Text="Editar">
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton CommandName="delGrid" ImageUrl="../Images/delete.png" Text="Eliminar">
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
                                            <telerik:GridBoundColumn DataField="IappId" DataType="System.Int64"
                                                HeaderText="ID" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IdCentro" DataType="System.Int64"
                                                HeaderText="ID_CENTRO" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IappLogin" DataType="System.String" HeaderStyle-Width="10%"
                                                HeaderText="Usuario" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IappNombre_Completo" DataType="System.String"
                                                HeaderText="NOMBRE" UniqueName="IappNombre_Completo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nombre_Centro" DataType="System.String"
                                                HeaderText="CENTRO" UniqueName="Nombre_Centro">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="IappActivo" HeaderStyle-Width="10%"
                                                DataImageUrlFormatString="../images/change_{0}.png" ImageAlign="Middle" ImageHeight="32px" ImageWidth="32px" HeaderText="CENTRO">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridImageColumn>
                                            <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="IappPemex" HeaderStyle-Width="10%"
                                                DataImageUrlFormatString="../images/admin_{0}.png" ImageAlign="Middle" ImageHeight="32px" ImageWidth="32px" HeaderText="PEMEX">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridImageColumn>
                                            <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="IappAdmin" HeaderStyle-Width="10%"
                                                DataImageUrlFormatString="../images/admin_{0}.png" ImageAlign="Middle" ImageHeight="32px" ImageWidth="32px" HeaderText="ADMIN">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridImageColumn>
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
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move,Resize" Modal="true" VisibleStatusbar="false"
                NavigateUrl="cccmex_modalUsuarios.aspx" Title="Usuarios del portal">
            </telerik:RadWindow>
        </Windows>

    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
        EnableAJAX="true" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridUsuarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridUsuarios"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="windowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridUsuarios" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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
