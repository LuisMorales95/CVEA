<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="capturebylaboratory.aspx.cs" Inherits="appwebcccmex.capturebylaboratory" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            Capturas - <asp:Label ID="nameCentro" runat="server" Text="..." Font-Bold="true"></asp:Label>
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">
                            <div class="form-group row">
                                <div class="col-sm-12">

                                    <label class="col-form-label col-sm-2" for="lblc">Instalaciones</label>

                                    <div class=" col-sm-12">
                                        <telerik:RadComboBox ID="cmbInstalacion" runat="server" Skin="Bootstrap" AutoPostBack="true" EmptyMessage="- Selecciona la instalación -" Width="410px" MarkFirstMatch="true" OnSelectedIndexChanged="cmbInstalacion_SelectedIndexChanged"></telerik:RadComboBox>
                                    </div>

                                </div>
                            </div>
                            <hr />
                            <p>

                                <telerik:RadGrid ID="gridCapturas" runat="server" Skin="Material" RenderMode="Lightweight" Width="100%" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                                    AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True"
                                    AllowSorting="false" OnItemCommand="gridCapturas_ItemCommand" OnNeedDataSource="gridCapturas_NeedDataSource" PageSize="10">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView AllowAutomaticInserts="false"
                                        NoDetailRecordsText="No se encontraron registros ..."
                                        AutoGenerateColumns="False" Caption="LISTA DE INSTRUMENTOS"
                                        ClientDataKeyNames="Idlaboratorio,Idinst,Idprueba"
                                        CommandItemDisplay="Top" DataKeyNames="Idlaboratorio" Font-Size="10px">
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
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="true" Skin="Material" RenderMode="Lightweight">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="CAPTURA DE INSTRUMENTO" CommandName="addGrid" ImageUrl="images/mas2.png"
                                                        Visible='<%# !gridCapturas.MasterTableView.IsItemInserted %>'>
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton CommandName="delGrid" ImageUrl="Images/delete.png" Text="ELIMINAR INSTRUMENTO" Enabled="true">
                                                    </telerik:RadToolBarButton>
                                                    <telerik:RadToolBarButton CommandName="editGrid" ImageUrl="Images/edit-29.png" Text="EDITAR INSTRUMENTO" Enabled="true">
                                                    </telerik:RadToolBarButton>
                                                </Items>
                                            </telerik:RadToolBar>
                                            <asp:ImageButton ID="fileLab" ImageUrl="images/Pdf-48.png" Width="30px" Height="30px" runat="server"
                                                CommandName="pdfArchivos" ToolTip="Descarga el instrumento pdf" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Idlaboratorio" DataType="System.Int64" HeaderText="IDLAB" Visible="false" />
                                            <telerik:GridBoundColumn DataField="Idinst" DataType="System.Int64" HeaderText="IDSINST" Visible="false" />
                                            <telerik:GridBoundColumn DataField="Idprueba" DataType="System.Int64" HeaderText="IDPRUEBA" Visible="false" />
                                            <telerik:GridBoundColumn DataField="Pruebas" DataType="System.String" HeaderText="Pruebas" />
                                            <telerik:GridBoundColumn DataField="Metodo_astm" DataType="System.String" HeaderText="Metodo ASTM" />
                                            <telerik:GridBoundColumn DataField="Dispositivo_temp" DataType="System.String" HeaderText="Disp. Temp." />
                                            <telerik:GridBoundColumn DataField="No_inf_calibr_temp" DataType="System.String" HeaderText="No. Inf. Calibr." />
                                            <telerik:GridBoundColumn DataField="Hidrometro" DataType="System.String" HeaderText="Didrometro" />
                                            <telerik:GridBoundColumn DataField="No_inf_calibr_hid" DataType="System.String" HeaderText="No. Inf. Calibr." />
                                            <telerik:GridBoundColumn DataField="Probeta" DataType="System.String" HeaderText="Probeta" />
                                            <telerik:GridBoundColumn DataField="No_inf_calibr_prob" DataType="System.String" HeaderText="No. Inf. Calibr." />
                                            <telerik:GridBoundColumn DataField="Equipo_analisis" DataType="System.String" HeaderText="Equipo de Analisis" />
                                            <telerik:GridBoundColumn DataField="Modelo_marca" DataType="System.String" HeaderText="Modelo/Marca" />
                                            <telerik:GridBoundColumn DataField="Fecha_calibr_mantto" DataType="System.String" HeaderText="Fecha Mantto."
                                                UniqueName="Fecha_calibr_mantto" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="No_inf_calibr_equipo" UniqueName="No_inf_calibr_equipo" DataType="System.String" HeaderText="No. Inf. Calibr." />
                                            <telerik:GridBoundColumn DataField="Estandar_verif_util" UniqueName="Estandar_verif_util" DataType="System.String" HeaderText="Estandar Verif." />
                                            <telerik:GridBoundColumn DataField="Fecha_vig_estandar" DataType="System.String" HeaderText="Fecha Vig. Estandar"
                                                UniqueName="Fecha_vig_estandar" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Medidor_poro_memb" UniqueName="Medidor_poro_memb" DataType="System.String" HeaderText="Medidor de Poro" />
                                            <telerik:GridBoundColumn DataField="Inf_calibr_bal_analitica" UniqueName="Inf_calibr_bal_analitica" DataType="System.String" HeaderText="Inf. Calibr. Analitica" />
                                            <telerik:GridBoundColumn DataField="Inf_calibr_tubo_cann" UniqueName="Inf_calibr_tubo_cann" DataType="System.String" HeaderText="Inf. Calibr. Tubo C." />
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

                            </p>
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
                NavigateUrl="modal_cccmex_capturabylaboratorio.aspx" Title="Capturas laboratorio">
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
                if (args.get_eventTarget().indexOf("fileLab") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

