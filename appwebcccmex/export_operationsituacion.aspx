<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="export_operationsituacion.aspx.cs" Inherits="appwebcccmex.export_operationsituacion" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Situación Operativa de por centros de trabajo
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">
                            <div class="form-group row">
                                <%-- each item --%>
                                <div class="col-sm-12 mb-4">
                                    <div class=" col-sm-4">
                                        <telerik:RadButton ID="rbtHoy" runat="server" Skin="Bootstrap" Width="100%" 
                                            ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton" 
                                            AutoPostBack="true" OnClick="rbtHoy_Click">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="Registros del día de hoy - OK"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="Hoy"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class=" col-sm-4">
                                        <telerik:RadDatePicker ID="rdpFechaIni" runat="server" Skin="Bootstrap" AutoPostBack="true" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            DateInput-EmptyMessage="Fecha Captura" MaxDate="01/01/3000"
                                            MinDate="01/01/1000" Width="100%" OnSelectedDateChanged="rdpFechaIni_SelectedDateChanged">
                                            <Calendar ID="Calendar3" runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>

                            </div>

                            <hr />

                            <div class="col-sm-12">
                                <%-- table grid with other skins --%>
                                <p>
                                    <telerik:RadGrid ID="gridCapturas" runat="server" Skin="Material" RenderMode="Lightweight"
                                        AllowPaging="false" AllowAutomaticUpdates="false" AllowAutomaticInserts="false"
                                        AllowAutomaticDeletes="false" AllowSorting="false" AllowMultiRowSelection="false"
                                        Font-Size="12px"
                                        OnItemCommand="gridCapturas_ItemCommand" OnExportCellFormatting="gridCapturas_ExportCellFormatting">
                                        <ExportSettings HideStructureColumns="true" FileName="reporteByCentro"></ExportSettings>
                                        <ExportSettings HideStructureColumns="true">
                                            <Excel Format="Html" />
                                        </ExportSettings>
                                        <MasterTableView AutoGenerateColumns="False" Caption="SITUACIÓN OPERATIVA POR CENTRO DE TRABAJO"
                                            CommandItemDisplay="Top" DataKeyNames="IdOperatividad" ClientDataKeyNames="IdOperatividad,IdCentro" AllowAutomaticInserts="false">
                                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" />
                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn Created="True"
                                                FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Fecha" DataType="System.DateTime" HeaderText="Fecha" HeaderStyle-Width="10%"
                                                    DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Centro_centro" UniqueName="Centro_centro" DataType="System.String" HeaderText="CENTRO" />
                                                <telerik:GridBoundColumn DataField="Cantidad_dia_anterior" UniqueName="Cantidad_dia_anterior" DataType="System.String" HeaderText="C. INSP." />
                                                <telerik:GridBoundColumn DataField="Equipos_Rechazados" UniqueName="Equipos_Rechazados" DataType="System.String" HeaderText="E. RECHAZADOS." />
                                                <telerik:GridBoundColumn DataField="Unidad_inspeccionada" UniqueName="Unidad_inspeccionada" DataType="System.String" HeaderText="U. INSP." />
                                                <telerik:GridBoundColumn DataField="Unidad_pendiente" UniqueName="Unidad_pendiente" DataType="System.String" HeaderText="U. PEND." />
                                                <telerik:GridBoundColumn DataField="Unidad_inspeccionada_hora" UniqueName="Unidad_inspeccionada_hora" DataType="System.String" HeaderText="U. INSP. HRS." />
                                                <telerik:GridBoundColumn DataField="Unidad_pendiente_hora" UniqueName="Unidad_pendiente_hora" DataType="System.String" HeaderText="U. PEND. HRS" />
                                                <telerik:GridBoundColumn DataField="Tanque_servicio" UniqueName="Tanque_servicio" DataType="System.String" HeaderText="TANQUE EN SERVICIO Y MUESTREO" />
                                                <telerik:GridBoundColumn DataField="Problema_inspeccion" UniqueName="Problema_inspeccion" DataType="System.String" HeaderText="PROBLEMAS EN LA INSPECCIÓN" />
                                                <telerik:GridBoundColumn DataField="Otras_observaciones" UniqueName="Otras_observaciones" DataType="System.String" HeaderText="OTRAS OBSERVACIONES" />
                                                <telerik:GridBoundColumn DataField="Cantidad_facturada" UniqueName="Cantidad_facturada" DataType="System.String" HeaderText="CANTIDAD FACTURADA AYER" />
                                                <telerik:GridBoundColumn DataField="Cantidad_facturada2" UniqueName="Cantidad_facturada2" DataType="System.String" HeaderText="CANTIDAD FACTURADA HOY" />
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

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
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
            <telerik:AjaxSetting AjaxControlID="rdpFechaIni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCapturas" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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
