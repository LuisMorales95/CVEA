<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cccmex_situacionoperativa.aspx.cs" Inherits="appwebcccmex.cccmex_situacionoperativa" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <hgroup class="title">
            <h1>Situación Operativa de -
                <asp:Label ID="nameCentro" runat="server" Text="..." Font-Bold="true"></asp:Label></h1>
        </hgroup>

        <fieldset>
            <ol>

                <li>
                    <telerik:RadButton ID="rbtTodas" runat="server" Width="20%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton" AutoPostBack="true" OnClick="rbtTodas_Click">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Todos los registros - OK"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Filtrar Todas"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                    <br />
                    <telerik:RadButton ID="rbtHoy" runat="server" Width="20%" ToggleType="Radio" ButtonType="LinkButton" GroupName="StandardButton" AutoPostBack="true" OnClick="rbtHoy_Click">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Registros del día de hoy - OK"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Hoy"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </li>
                <li>
                    <telerik:RadDatePicker ID="rdpFechaIni" runat="server" AutoPostBack="true" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                        DateInput-EmptyMessage="Fecha Captura" MaxDate="01/01/3000"
                        MinDate="01/01/1000" Width="22%" OnSelectedDateChanged="rdpFechaIni_SelectedDateChanged">
                        <Calendar ID="Calendar3" runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </telerik:RadDatePicker>
                </li>
            </ol>

        </fieldset>

        <p>
            <telerik:RadGrid ID="gridCapturas" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false" OnItemCommand="gridCapturas_ItemCommand" OnNeedDataSource="gridCapturas_NeedDataSource" PageSize="10">
                <PagerStyle Mode="NumericPages" />
                <MasterTableView AllowAutomaticInserts="false" NoDetailRecordsText="No se encontraron registros ..." AutoGenerateColumns="False" Caption="SITUACIÓN OPERATIVA" DataKeyNames="IdOperatividad" ClientDataKeyNames="IdOperatividad,IdCentro" CommandItemDisplay="Top" Font-Size="10px">
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
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true">
                            <Items>
                                <telerik:RadToolBarButton Text="CAPTURA SITUACIÓN OPERATIVA" CommandName="addGrid" ImageUrl="images/mas2.png"
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

                        <telerik:GridBoundColumn DataField="Fecha" DataType="System.DateTime" HeaderText="Fecha" HeaderStyle-Width="10%"
                            DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Cantidad_dia_anterior" UniqueName="Cantidad_dia_anterior" DataType="System.String" HeaderText="C. INSP." />
                        <telerik:GridBoundColumn DataField="Unidad_inspeccionada" UniqueName="Unidad_inspeccionada" DataType="System.String" HeaderText="U. INSP." />
                        <telerik:GridBoundColumn DataField="Equipos_Rechazados" UniqueName="Equipos_Rechazados" DataType="System.String" HeaderText="E. RECHAZADOS." />
                        <telerik:GridBoundColumn DataField="Unidad_pendiente" UniqueName="Unidad_pendiente" DataType="System.String" HeaderText="U. PEND." />
                        <telerik:GridBoundColumn DataField="Unidad_inspeccionada_hora" UniqueName="Unidad_inspeccionada_hora" DataType="System.String" HeaderText="U. INSP. HRS." />
                        <telerik:GridBoundColumn DataField="Unidad_pendiente_hora" UniqueName="Unidad_pendiente_hora" DataType="System.String" HeaderText="U. PEND. HRS" />
                        <telerik:GridBoundColumn DataField="Tanque_servicio" UniqueName="Tanque_servicio" DataType="System.String" HeaderText="T. EN SERV." />
                        <telerik:GridBoundColumn DataField="Problema_inspeccion" UniqueName="Problema_inspeccion" DataType="System.String" HeaderText="PROBLEMAS" />
                        <telerik:GridBoundColumn DataField="Otras_observaciones" UniqueName="Otras_observaciones" DataType="System.String" HeaderText="OBSERVACIONES" />
                        <telerik:GridBoundColumn DataField="Cantidad_facturada" UniqueName="Cantidad_facturada" DataType="System.String" HeaderText="CANTIDAD F." />
                        <telerik:GridBoundColumn DataField="Cantidad_facturada2" UniqueName="Cantidad_facturada2" DataType="System.String" HeaderText="CANTIDAD F2." />
                        <telerik:GridBoundColumn DataField="Centro_centro" UniqueName="Centro_centro" DataType="System.String" HeaderText="CENTRO" Visible="false" />

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

            <p>
            </p>

        </p>

    </telerik:RadAjaxPanel>

    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false"
                NavigateUrl="modal_cccmex_situacionoperativa.aspx" Title="Captura Situación operativa">
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

            <telerik:AjaxSetting AjaxControlID="rdpFechaIni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCapturas" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>

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
