<%@ Page Title="" Language="C#" MasterPageFile="~/caminPage.Master" AutoEventWireup="true" CodeBehind="centrospag.aspx.cs" Inherits="appwebcccmex.catalogos.centrospag" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">

        <div class="col-md-12 d-flex grid-margin">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Centros de trabajo
                        </div>
                        <div class="card-body badge-outline-secondary text-dark ">
                            <div class="form-group row">

                                <div class="col-sm-12">
                                    <label class="col-form-label col-sm-2" for="lblc">Código Centro:</label>
                                    <div class="col-sm-4">
                                        <telerik:RadNumericTextBox ID="addidCentro" Skin="MetroTouch" EnabledStyle-HorizontalAlign="Center" CssClass="form-control" runat="server" ReadOnly="true" MaxLength="9" Width="100%" EmptyMessage="Identificador" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <label class="col-form-label col-sm-2" for="lblc2">Centro De Trabajo:</label>
                                    <div class="col-sm-12">
                                        <telerik:RadTextBox ID="addcentro" runat="server" Skin="MetroTouch" CssClass="form-control" MaxLength="180" Width="100%" EmptyMessage="Centro de trabajo"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="valReqaddcentro" ControlToValidate="addcentro" runat="server"
                                            ValidationGroup="get"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="col-sm-4 text-center">
                                <telerik:RadButton ID="cmdcentro" runat="server" Font-Size="18px" Skin="Material" RenderMode="Lightweight" Text="Agregar" Width="100%" OnClientClicking="RadConfirm" OnClick="cmdcentro_Click">
                                </telerik:RadButton>
                            </div>

                            <div class="col-sm-4 text-center">
                                <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true"
                                    ShowSummary="true" DisplayMode="BulletList" CssClass="alert-danger"
                                    HeaderText="Datos requeridos" />
                            </div>

                            <hr />

                            <div class="col-sm-12">
                                <telerik:RadGrid ID="gridcccmex" runat="server" Skin="Material" RenderMode="Lightweight" Width="100%" AllowAutomaticDeletes="false" 
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" 
                                    OnItemCommand="gridcccmex_ItemCommand" OnNeedDataSource="gridcccmex_NeedDataSource" PageSize="50">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView AllowAutomaticInserts="false" AutoGenerateColumns="False" Caption="LISTADO DE CENTROS" ClientDataKeyNames="IdCentro,Centro"
                                        CommandItemDisplay="Top" DataKeyNames="IdCentro" Font-Size="14px">
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true" Skin="Material" RenderMode="Lightweight">
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
                                        </CommandItemTemplate>
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="IdCentro" HeaderStyle-Width="20%" DataType="System.Int64" HeaderText="CODIGO" 
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Centro" DataType="System.String" HeaderText="CENTRO" UniqueName="Centro">
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

    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move,Resize" Modal="true" VisibleStatusbar="false"
                NavigateUrl="centrosmodalpag.aspx" Title="Agregando instalaciones al centro">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridcccmex">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="windowManager1" />
                    <telerik:AjaxUpdatedControl ControlID="gridcccmex" />
                    <telerik:AjaxUpdatedControl ControlID="cmdcentro" />
                    <telerik:AjaxUpdatedControl ControlID="addidCentro" />
                    <telerik:AjaxUpdatedControl ControlID="addcentro" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmdcentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridcccmex"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="windowManager1" />
                    <telerik:AjaxUpdatedControl ControlID="addidCentro" />
                    <telerik:AjaxUpdatedControl ControlID="addcentro" />
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
