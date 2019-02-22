<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cccmex_acumuladosByOrden.aspx.cs" Inherits="appwebcccmex.cccmex_acumuladosByOrden" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <hgroup class="title">
            <h1>Acumuldos Por Orden De Servicio</h1>
        </hgroup>

        <fieldset>
            <ol>
                <li>
                    <asp:Label ID="lblCentro" runat="server" Font-Bold="true" Font-Size="14px" Text="Orden De Servicio: "></asp:Label><br />
                    <telerik:RadComboBox ID="cmbordenserv" runat="server" EmptyMessage="- Selecciona la orden -" Width="250px" MarkFirstMatch="true"></telerik:RadComboBox>
                </li>

                <li>
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="14px" Text="Producto: "></asp:Label><br />
                    <telerik:RadButton ID="rbproducto" runat="server" Width="25%" ToggleType="CheckBox" ButtonType="LinkButton">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Propileno"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Turbosina"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </li>

                <li>
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="14px" Text="Acumulado: "></asp:Label><br />
                    <telerik:RadButton ID="rbacumulado" runat="server" Width="25%" ToggleType="CheckBox" ButtonType="LinkButton">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Servicio"></telerik:RadButtonToggleState>
                            <telerik:RadButtonToggleState Text="Centro"></telerik:RadButtonToggleState>
                        </ToggleStates>
                    </telerik:RadButton>
                </li>
                <li>

                    <telerik:RadButton ID="cmdejecuta" runat="server" Width="33%" Height="35px" Font-Bold="true" Font-Size="16px" Text="Realizar consulta" OnClick="cmdejecuta_Click" />
                </li>
            </ol>

        </fieldset>

        <p>
            <telerik:RadGrid ID="gridcentro" runat="server"
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

    </telerik:RadAjaxPanel>

    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">
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

