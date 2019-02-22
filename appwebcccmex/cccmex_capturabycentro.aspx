<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cccmex_capturabycentro.aspx.cs" Inherits="appwebcccmex.cccmex_capturabycentro" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <hgroup class="title">
            <h1>Capturas -
                <asp:Label ID="nameCentro" runat="server" Text="..." Font-Bold="true"></asp:Label></h1>
        </hgroup>

        <fieldset>
            <ol>
                <li>
                    <asp:Label ID="lblCentro" runat="server" Font-Bold="true" Font-Size="16px" Text="Instalaciones: "></asp:Label><br />
                    <telerik:RadComboBox ID="cmbInstalacion" runat="server" AutoPostBack="true" EmptyMessage="- Selecciona la instalación -" Width="410px" MarkFirstMatch="true" OnSelectedIndexChanged="cmbInstalacion_SelectedIndexChanged"></telerik:RadComboBox>
                </li>
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
                <MasterTableView AllowAutomaticInserts="false" NoDetailRecordsText="No se encontraron registros ..." AutoGenerateColumns="False" Caption="CAPTURAS" ClientDataKeyNames="IdReg,IdProducto,IdCentro,IdInst,IdServicio,IdBarco,Folio_cert_calidad,Folio_cert_cant,Referencia_folio" CommandItemDisplay="Top" DataKeyNames="IdReg" Font-Size="10px">
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
                        <asp:ImageButton ID="filegridzip" ImageUrl="Images/Zip-32.png" Width="32px" Height="32px" runat="server"
                            CommandName="filezipGrid" ToolTip="Bajar archivos pdf en ZIP" />
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true">
                            <Items>
                                <telerik:RadToolBarButton Text="CAPTURA INSPECCIÓN" CommandName="addGrid" ImageUrl="images/mas2.png"
                                    Visible='<%# !gridCapturas.MasterTableView.IsItemInserted %>'>
                                </telerik:RadToolBarButton>


                                <telerik:RadToolBarButton CommandName="editGrid" ImageUrl="Images/edit-29.png" Text="EDITAR ARCHIVO">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton CommandName="printGrid" ImageUrl="Images/Print_32.png" Text="IMPRIMIR" Enabled="false">
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

                        <telerik:GridBoundColumn DataField="IdReg" DataType="System.Int64" HeaderText="IDREG" Visible="false" />
                        <telerik:GridBoundColumn DataField="IdProducto" DataType="System.Int64" HeaderText="IDPROD" Visible="false" />
                        <telerik:GridBoundColumn DataField="IdCentro" DataType="System.Int64" HeaderText="IDCENTRO" Visible="false" />
                        <telerik:GridBoundColumn DataField="IdInst" DataType="System.Int64" HeaderText="IDINST" Visible="false" />
                        <telerik:GridBoundColumn DataField="IdServicio" DataType="System.String" HeaderText="IDSERV" Visible="false" />
                        <telerik:GridBoundColumn DataField="IdBarco" DataType="System.Int64" HeaderText="IDBARCO" Visible="false" />
                        <telerik:GridBoundColumn DataField="Referencia_folio" DataType="System.Int64" HeaderText="REF_FOLIO" Visible="false" />

                        <telerik:GridBoundColumn DataField="Orden_servicio" DataType="System.String" HeaderText="No. Orden" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Fecha" DataType="System.DateTime" HeaderText="Fecha" HeaderStyle-Width="10%"
                            DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="NombreCentro" DataType="System.String" HeaderText="Centro" UniqueName="NombreCentro" />
                        <telerik:GridBoundColumn DataField="NombreServicio" DataType="System.String" HeaderText="Servicio" UniqueName="NombreServicio" />
                        <telerik:GridBoundColumn DataField="NombreProducto" DataType="System.String" HeaderText="Producto" UniqueName="NombreProducto" />

                        <telerik:GridBoundColumn DataField="Cant_insp_mezcla" HeaderText="Cantidad Insp." DataType="System.Decimal" UniqueName="Cant_insp_mezcla" DataFormatString="{0:#,##0.#00}">
                            <ItemStyle HorizontalAlign="right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Propileno" HeaderText="Propileno" DataType="System.Decimal" UniqueName="Propileno" DataFormatString="{0:#,##0.#00}">
                            <ItemStyle HorizontalAlign="right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn HeaderText="Archivos" UniqueName="Folio_cert_calidad" DataType="System.String" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <asp:ImageButton ID="filecertificacion" ImageUrl="images/Pdf-48.png" Width="25px" Height="25px" runat="server"
                                    CommandName="pdfArchivos" ToolTip="Descarga certificado de calidad  y cantidad en pdf" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>



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
                NavigateUrl="modal_cccmex_capturabycentro.aspx" Title="Capturas">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close,Move,Resize" Modal="true" VisibleStatusbar="false"
                NavigateUrl="modal_cccmex_editbycentro.aspx" Title="Edición">
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

            function openRadWindow2() {
                var radwindow = $find('<%=RadWindow2.ClientID %>');
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
                if (args.get_eventTarget().indexOf("filecertificacion") >= 0) {
                    args.set_enableAjax(false);
                }
                if (args.get_eventTarget().indexOf("filegridzip") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
