<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="accumulatedbyorder.aspx.cs" Inherits="appwebcccmex.accumulatedbyorder" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Acumuldos Por Orden De Servicio
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">
                            <div class="form-group row">

                                <div class="col-sm-12 mb-4">
                                    <label class="col-form-label col-sm-2" for="lblc">Orden De Servicio</label>
                                    <div class=" col-sm-4">
                                        <telerik:RadComboBox ID="cmbordenserv" runat="server" Skin="Bootstrap" EmptyMessage="- Selecciona la orden -" Width="100%" MarkFirstMatch="true"></telerik:RadComboBox>
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <label class="col-form-label col-sm-2" for="lblc">Producto</label>
                                    <div class=" col-sm-4">
                                        <telerik:RadButton ID="rbproducto" runat="server" Skin="Bootstrap" Width="100%" ToggleType="CheckBox" ButtonType="LinkButton">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Propileno"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Turbosina"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <label class="col-form-label col-sm-2" for="lblc">Acumulado</label>
                                    <div class=" col-sm-4">
                                        <telerik:RadButton ID="rbacumulado" runat="server" Skin="Bootstrap" Width="100%" ToggleType="CheckBox" ButtonType="LinkButton">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Servicio"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Centro"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-4 text-center">
                                <%-- buttons add Shin Material--%>
                                <telerik:RadButton ID="cmdejecuta" runat="server" Skin="Material" RenderMode="Lightweight" Width="100%" Height="35px" Font-Bold="true" Font-Size="16px" Text="Realizar consulta" OnClick="cmdejecuta_Click" />
                            </div>

                            <hr />

                            <div class="col-sm-12">

                                <p>
                                    <telerik:RadGrid ID="gridcentro" runat="server" Skin="Material" RenderMode="Lightweight"
                                        AllowPaging="False" AllowAutomaticUpdates="false" AllowAutomaticInserts="false"
                                        AllowAutomaticDeletes="false" AllowSorting="false" AllowMultiRowSelection="false"
                                        Font-Size="12px"
                                        OnItemCommand="gridcentro_ItemCommand" OnExportCellFormatting="gridcentro_ExportCellFormatting">
                                        <ExportSettings HideStructureColumns="true" FileName="reporteByCentro"></ExportSettings>
                                        <ExportSettings HideStructureColumns="true"></ExportSettings>
                                        <MasterTableView AutoGenerateColumns="False" Caption="FILTRADO POR CENTRO"
                                            CommandItemDisplay="Top" DataKeyNames="Idinst" ClientDataKeyNames="Idinst" AllowAutomaticInserts="false">
                                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" />
                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn Created="True"
                                                FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="IdInst" DataType="System.Int64"
                                                    HeaderText="CODIGO" UniqueName="IdInst" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NombreCentro" DataType="System.String" HeaderText="INSTALACIÓN" UniqueName="NombreCentro"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IdServicio" DataType="System.String"
                                                    HeaderText="CODIGO" UniqueName="IdServicio" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NombreServicio" DataType="System.String" HeaderText="SERVICIO" UniqueName="NombreServicio"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cant_insp_mezcla" HeaderText="VOL. CERTIFICADO" DataType="System.Decimal" UniqueName="Cant_insp_mezcla" DataFormatString="{0:##0.000}">
                                                    <ItemStyle HorizontalAlign="right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Mes" DataType="System.Int16" HeaderText="MES" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Year" DataType="System.Int16" HeaderText="AÑO" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
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
                                            <Selecting AllowRowSelect="false"></Selecting>
                                        </ClientSettings>
                                        <FilterMenu EnableImageSprites="False">
                                        </FilterMenu>
                                    </telerik:RadGrid>

                                </p>
                                <p>
                                    <telerik:RadGrid ID="gridServicio" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="false" AllowSorting="false" Font-Size="12px" OnItemCommand="gridServicio_ItemCommand">
                                        <ExportSettings FileName="reporteByCentro" HideStructureColumns="true">
                                        </ExportSettings>
                                        <ExportSettings HideStructureColumns="true">
                                        </ExportSettings>
                                        <MasterTableView AllowAutomaticInserts="false" AutoGenerateColumns="False" Caption="FILTRADO POR SERVICIO" ClientDataKeyNames="IdServicio" CommandItemDisplay="Top" DataKeyNames="IdServicio">
                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowRefreshButton="false" />
                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="IdServicio" DataType="System.String" FooterStyle-Font-Bold="true" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center" UniqueName="IdServicio">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NombreServicio" DataType="System.String" HeaderText="SERVICIO" UniqueName="NombreServicio">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cant_insp_mezcla" DataFormatString="{0:### ##0.000}" DataType="System.Decimal" HeaderText="VOL. CERTIFICADO" UniqueName="Cant_insp_mezcla">
                                                    <ItemStyle HorizontalAlign="right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Mes" DataType="System.Int16" HeaderText="MES" ItemStyle-HorizontalAlign="Center">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Year" DataType="System.Int16" HeaderText="AÑO" ItemStyle-HorizontalAlign="Center">
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
                                            <Selecting AllowRowSelect="false" />
                                        </ClientSettings>
                                        <FilterMenu EnableImageSprites="False">
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </p>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <%-- RadWindowManager add skin-bootstrap --%>
    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
        EnableAJAX="true" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="cmdejecuta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridcentro" />
                    <telerik:AjaxUpdatedControl ControlID="gridServicio" />
                    <telerik:AjaxUpdatedControl ControlID="windowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <%--    <telerik:AjaxSetting AjaxControlID="gridServicio" >
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="gridServicio"></telerik:AjaxUpdatedControl>
                           <telerik:AjaxUpdatedControl ControlID="windowManager1"/>
                    </UpdatedControls>
               </telerik:AjaxSetting>--%>

            <telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
        <ClientEvents OnRequestStart="onRequestStart" />
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
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


            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
