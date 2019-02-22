<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="Zone.aspx.cs" Inherits="appwebcccmex.Zone" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Gestión de Zonas
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">


                            <div class="col-sm-12">
                                <%-- table grid with other skins --%>

                                <telerik:RadGrid ID="gridAlerta" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" BackColor="White"
                                    AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false"
                                    OnItemCommand="gridAlerta_ItemCommand" OnNeedDataSource="gridAlerta_NeedDataSource" PageSize="10" Skin="Material" RenderMode="Lightweight" ShowFooter="True">

                                    <MasterTableView AllowAutomaticInserts="false" NoDetailRecordsText="No se encontraron registros ..." AutoGenerateColumns="False"
                                        Caption="Zonas" ClientDataKeyNames="IdZona"
                                        CommandItemDisplay="Top" DataKeyNames="IdZona" Font-Size="12px">
                                        <NoRecordsTemplate>
                                            <table style="width: 100%; height: 100%; border: 0; padding: 20px; border-spacing: 20px;">
                                                <tr>
                                                    <td style="text-align: center; vertical-align: central">
                                                        <h3 style="color: Black">Registros no encontrado, Favor de volver a realizar la consulta</h3>
                                                    </td>
                                                </tr>
                                            </table>
                                        </NoRecordsTemplate>

                                        <PagerStyle Mode="NumericPages" />
                                        <HeaderStyle Font-Bold="True" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true" EnableShadows="True" RenderMode="Lightweight" SingleClick="None" Skin="Material">
                                                <Items>
                                                    <telerik:RadToolBarButton runat="server" CommandName="btnAgregar" ImageUrl="~/Images/mas3.png" Text="Nuevo" ToolTip="Generar una nueva zona.">
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton runat="server" CommandName="btnActualizar" ImageUrl="~/Images/Update.png" Text="Actualizar" ToolTip="Actualizar una zona existente.">
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton runat="server" CommandName="btnEliminar" ImageUrl="~/Images/delete.png" Text="Eliminar" ToolTip="Eliminar una zona existente.">
                                                    </telerik:RadToolBarButton>
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>

                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />

                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="False">
                                        </RowIndicatorColumn>

                                        <ExpandCollapseColumn Created="True" FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                        </ExpandCollapseColumn>

                                        <RowIndicatorColumn Visible="False">
                                        </RowIndicatorColumn>

                                        <ExpandCollapseColumn Created="True">
                                        </ExpandCollapseColumn>

                                        <Columns>
                                            <telerik:GridBoundColumn DataField="IdZona" DataType="System.Int64" HeaderText="ZONA ID" Visible="true" />
                                            <telerik:GridBoundColumn DataField="Zona" DataType="System.String" HeaderText="ZONA" Visible="true" />
                                            <telerik:GridBoundColumn DataField="Descripcion" DataType="System.String" HeaderText="DESCRIPCION" Visible="true" />
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True" Position="Bottom" />
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <ClientEvents OnRowDblClick="gridAlerta_OnRowDblClick" />
                                        <Scrolling AllowScroll="True" FrozenColumnsCount="2" SaveScrollPosition="true" UseStaticHeaders="True" />
                                    </ClientSettings>

                                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                                </telerik:RadGrid>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <%-- RadWindowManager add skin-bootstrap --%>


    <telerik:RadWindowManager ID="ManejadorRadWindow" runat="server" Animation="Resize" Skin="Bootstrap">
        <Windows>
            <telerik:RadWindow ID="VentanaRad" runat="server" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false"
                NavigateUrl="~/modal_cccmex_zonas.aspx" Title="Gestiòn de Zonas">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxManager ID="ManejadorRadAjax" runat="server" EnableAJAX="true" OnAjaxRequest="ManejadorRadAjax_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridAlerta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridAlerta" />
                    <telerik:AjaxUpdatedControl ControlID="ManejadorRadWindow" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ManejadorRadAjax">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridAlerta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxPanelCargando" runat="server" Transparency="50">
    </telerik:RadAjaxLoadingPanel>



</asp:Content>


<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
        }

        .auto-style2 {
            width: 100px;
        }
    </style>

    <script type="text/javascript" id="telerikClientEvents1">

        var isDoubleClick = false;
        var clickHandler = null;
        var ClickedIndex = null;

        function gridAlerta_OnRowDblClick(sender, args) {
            ClickedIndex = args._itemIndexHierarchical;
            isDoubleClick = true;
            if (clickHandler) {
                window.clearTimeout(clickHandler);
                clickHandler = null;
            }
            clickHandler = window.setTimeout(ActualClick, 200);
        }

        function AbrirRadWindow() {
            var radwindow = $find('<%=VentanaRad.ClientID %>');
            radwindow.show();
        }

        function ActualClick() {
            if (isDoubleClick) {
                var grid = $find("<%=gridAlerta.ClientID %>");
                if (grid)
                    var MasterTable = grid.get_masterTableView();
                var Rows = MasterTable.get_dataItems();
                if (Rows.length > 0) {
                    var row = Rows[0];
                    if (ClickedIndex != null && ClickedIndex != undefined) {

                        MasterTable.fireCommand("DobleClick", ClickedIndex);
                    }
                }
            }
        }
        function refreshGrid(arg) {

            $find("<%= ManejadorRadAjax.ClientID %>").ajaxRequest("Rebind");

        }
        function confirmCallBackFn(arg) {

            if (arg == true) {
                $find("<%= ManejadorRadAjax.ClientID %>").ajaxRequest("Eliminar");
            }

        }


    </script>

</asp:Content>

