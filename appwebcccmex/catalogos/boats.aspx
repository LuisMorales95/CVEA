<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="boats.aspx.cs" Inherits="appwebcccmex.catalogos.boats" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
       <div class="col-md-12 grid-margin">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Barcos
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">
                            <div class="form-group row">
                                <%-- each item --%>
                                <div class="col-sm-12">
                                    <%-- labels --%>
                                    <label class="col-form-label col-sm-2" for="lblc">Código Barcos</label>

                                    <div class=" col-sm-4">
                                        <%-- inputs --%>
                                        <telerik:RadNumericTextBox ID="addidbarco" runat="server" Skin="MetroTouch" MaxLength="9" Width="100%" CssClass="form-control" EmptyMessage="Identificador" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="addidbarco" runat="server"
                                            ValidationGroup="get"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <%-- each item --%>
                                <div class="col-sm-12">
                                    <%-- labels --%>
                                    <label class="col-form-label col-sm-2" for="lblc2">Barcos</label>

                                    <div class=" col-sm-7">
                                        <%-- inputs --%>
                                        <telerik:RadTextBox ID="addbarco" runat="server" Skin="MetroTouch" MaxLength="190" Width="100%" CssClass="form-control" EmptyMessage="Barco"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="valReqaddcentro" ControlToValidate="addbarco" runat="server"
                                            ValidationGroup="get"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 text-center mb-4">
                                <telerik:RadButton ID="rbtBarco" runat="server" Width="100%" RenderMode="Lightweight" Skin="Material" Font-Size="18px" ToggleType="CheckBox" ButtonType="LinkButton">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Barco Importación"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Barco Nacional"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div class="col-sm-4 text-center">
                                <%-- buttons add Shin Material--%>
                                <telerik:RadButton ID="cmdEjecuta" runat="server" Text="Agregar" Font-Size="18px" Width="100%" Skin="Material" RenderMode="Lightweight" OnClientClicking="RadConfirm" OnClick="cmdEjecuta_Click">
                                </telerik:RadButton>
                            </div>
                            <div class="col-sm-4 text-center">
                                <%-- extra validations --%>
                                <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true"
                                    ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error"
                                    HeaderText="Datos requeridos, favor de corregir" />
                            </div>
                            <hr />

                            <div class="col-sm-12">
                                <%-- table grid with other skins --%>
                                <telerik:RadGrid ID="gridcccmex" runat="server" Skin="Material" RenderMode="Lightweight" Width="100%" AllowAutomaticDeletes="false"
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false"
                                    OnItemCommand="gridcccmex_ItemCommand" OnNeedDataSource="gridcccmex_NeedDataSource" PageSize="10">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView AllowAutomaticInserts="false" AutoGenerateColumns="False" Caption="LISTADO DE BARCOS" ClientDataKeyNames="IdBarco,Barco,Importacion"
                                        CommandItemDisplay="Top" DataKeyNames="IdBarco" Font-Size="14px">
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true" Skin="Material" RenderMode="Lightweight">
                                                <Items>
                                                    <telerik:RadToolBarButton CommandName="updGrid" ImageUrl="../Images/Update.png" Text="Actualizar">
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
                                            <telerik:GridBoundColumn DataField="IdBarco" DataType="System.Int64" HeaderStyle-Width="20%" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Barco" DataType="System.String" HeaderText="BARCO" UniqueName="Barco">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Importacion" DataType="System.Boolean" HeaderText="IMP." UniqueName="Importacion">
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

    
    <telerik:RadWindowManager ID="windowManager1" runat="server" Skin="Bootstrap"/>


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
                    <telerik:AjaxUpdatedControl ControlID="cmdEjecuta" />
                    <telerik:AjaxUpdatedControl ControlID="addidbarco" />
                    <telerik:AjaxUpdatedControl ControlID="addbarco" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cmdEjecuta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridcccmex"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="windowManager1" />
                    <telerik:AjaxUpdatedControl ControlID="addidbarco" />
                    <telerik:AjaxUpdatedControl ControlID="addbarco" />
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
