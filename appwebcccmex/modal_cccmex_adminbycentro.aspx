<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modal_cccmex_adminbycentro.aspx.cs" Inherits="appwebcccmex.modal_cccmex_adminbycentro" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System.Globalization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Administración de inspecciones</title>
    <link href="style/modalpopup.css" rel="stylesheet" />

</head>
<body onload="AdjustRadWidow();">
    <form id="form1" runat="server" class="contact_form" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" />
        <style type="text/css">
            .scrollable {
                height: 550px;
                overflow-y: scroll;
            }
        </style>
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function AdjustRadWidow() {
                var oWindow = GetRadWindow();
                setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 650);
            }

            //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
            function ChromeSafariFix(oWindow) {
                var iframe = oWindow.get_contentFrame();
                var body = iframe.contentWindow.document.body;

                setTimeout(function () {
                    var height = body.scrollHeight;
                    var width = body.scrollWidth;

                    var iframeBounds = $telerik.getBounds(iframe);
                    var heightDelta = height - iframeBounds.height;
                    var widthDelta = width - iframeBounds.width;

                    if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
                    if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
                    oWindow.center();

                }, 310);
            }

            //RadConfirm
            function RadConfirm(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                var text = "Esta seguro de actualizar?";
                radconfirm(text, callBackFunction, 300, 160, null, "Confirmación");
                args.set_cancel(true);
            }

            function RadConfirmdel(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                var text = "Esta seguro de eliminar?";
                radconfirm(text, callBackFunction, 300, 160, null, "Confirmación");
                args.set_cancel(true);
            }

            //Regresar a parent ...
            //GetRadWindow().BrowserWindow.refreshGrid(args); || GetRadWindow().close(); 
            function CloseAndRebind(args) { GetRadWindow().BrowserWindow.refreshGrid(args); GetRadWindow().close(); }



        </script>

        <%--  <div id="body" style="width:600px;">--%>

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="800px" Height="670px" HorizontalAlign="NotSet">

            <div class="scrollable">
                <ul>
                    <li>
                        <h2>
                            <asp:Label ID="lblencabezado" runat="server" Font-Bold="true" Font-Size="16px" Text="EDICIÓN  CCC-MEX ADMIN"></asp:Label>
                        </h2>
                        <hr />
                    </li>
                    <li>
                        <table>
                            <tr>
                                <td>
                                    <label for="ctrlreg">Control de registro General:</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblctrlreg" runat="server" Font-Bold="true" Font-Size="16px" Text="?"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="ctrlreg">Control de registro Por Producto:</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblctrlregProd" runat="server" Font-Bold="true" Font-Size="16px" Text="?"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="rev">Instalación:</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbinstalacion" runat="server" Skin="Bootstrap" AutoPostBack="true" EmptyMessage="- Seleccione la instalación -" Width="300px" MarkFirstMatch="true" LoadingMessage="Cargando..." DropDownWidth="300px"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cmbinstalacion" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="ordensurt">Orden de surtimiento:</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="addordenservicio" runat="server" Skin="Bootstrap" Height="35px" MaxLength="20" Width="30%" EmptyMessage="Orden servicio"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="addordenservicio" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="rev">Fecha:</label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="rdpFecha" runat="server" Skin="Bootstrap" AutoPostBack="true"
                                        DateInput-EmptyMessage="Fecha Isnpección" MaxDate="01/01/3000"
                                        MinDate="01/01/1000" Width="30%" Height="35px">
                                        <Calendar ID="Calendar3" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <telerik:RadTextBox ID="addanio" runat="server" Skin="Bootstrap" Height="35px" MaxLength="5" Width="10%" EmptyMessage="AÑO" ReadOnly="true" />
                                    <telerik:RadTextBox ID="addmes" runat="server" Skin="Bootstrap" Height="35px" MaxLength="2" Width="10%" EnabledStyle-HorizontalAlign="Center" EmptyMessage="MES" ReadOnly="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdpFecha" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cantidadinsp">Cantidad inspeccionada:</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="addcantidadinsp" runat="server" Skin="Bootstrap" EmptyMessage="0.00" MinValue="0" NumberFormat-DecimalDigits="3" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator="," Height="35px" Width="30%">
                                        <ReadOnlyStyle HorizontalAlign="Right" />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="addcantidadinsp" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="propileno">Propileno:</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="addpropileno" runat="server" Skin="Bootstrap" EmptyMessage="0.00" MinValue="0" NumberFormat-DecimalDigits="3" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator="," Height="35px" Width="30%">
                                        <ReadOnlyStyle HorizontalAlign="Right" />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="addpropileno" runat="server" ValidationGroup="get"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </li>

                    <li>
                        <div style="text-align: right; margin-right: 35px;">
                            <telerik:RadButton ID="cmdEjecuta" runat="server" Skin="Bootstrap" Width="35%" Height="35px" Text="Actualizar Registro" OnClientClicking="RadConfirm" OnClick="cmdEjecuta_Click">
                            </telerik:RadButton>
                            <telerik:RadButton ID="btndel" runat="server" Skin="Bootstrap" Width="35%" Height="35px" Text="Eliminar Registro" OnClientClicking="RadConfirmdel" OnClick="btndel_Click">
                            </telerik:RadButton>
                        </div>
                    </li>
                    <li>
                        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" ExpandMode="MultipleExpandedItems" Width="96%">
                            <Items>
                                <telerik:RadPanelItem Text="Información general de la inspección" Expanded="false" ForeColor="Silver" Font-Bold="true" Font-Size="14px">
                                    <Items>
                                        <telerik:RadPanelItem Value="info" runat="server"  Skin="Bootstrap">
                                            <ItemTemplate>
                                                <table style="border-spacing: 5px; border-collapse: separate; text-align: left; margin: auto; font-size: 14px; width: 95%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl1" runat="server" Text="No. Orden Servicio:"></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblorderservicio" runat="server" Text="##"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Producto:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblidproducto" runat="server" Text="##"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblproducto" runat="server" Text="!!!"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Centro:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblidcentro" runat="server" Text="##"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblcentro" runat="server" Text="!!!"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Servicio:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblidservicio" runat="server" Text="##"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblservicio" runat="server" Text="!!!"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Vol. Certificado:" /><br />
                                                            <asp:Label ID="lblmezcla" runat="server" Text="##" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblpropileno_turbosina" runat="server" Text="nombre:" /><br />
                                                            <asp:Label ID="lblreg_propileno_turbosina" runat="server" Text="##" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Año/Mes:" /><br />
                                                            <asp:Label ID="lblanio_mes" runat="server" Text="##" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Fecha:" /><br />
                                                            <asp:Label ID="lblfecha" runat="server" Text="##/##/##" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Fol. Cer. Cantidad/Archivo:" /><br />
                                                            <asp:Label ID="lblfolcertcantidad_file" runat="server" Text="##" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Fol. Cer. Calidad/Archivo:" /><br />
                                                            <asp:Label ID="lblfolcertcalidad_file" runat="server" Text="##" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelItem>
                            </Items>

                        </telerik:RadPanelBar>
                    </li>

                    <%-- <li>  
                   <telerik:RadPanelBar ID="RadPanelBar2" runat="server" ExpandMode="MultipleExpandedItems" Width="96%">
                             <Items>
                                 <telerik:RadPanelItem Text="Detalle de la inspección" Expanded="false" ForeColor="Silver" Font-Bold="true" Font-Size="14px">
                                     <Items>
                                         <telerik:RadPanelItem Value="info" runat="server">
                                     <ItemTemplate>
                                         <table style="border-spacing:5px; border-collapse:separate; text-align:left; margin:auto; font-size:16px;">
                                              <tr>
                                                 <td>
                                                     <telerik:RadGrid ID="gridCapturas" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="false">
                                   <PagerStyle Mode="NumericPages" />
                                   <mastertableview allowautomaticinserts="false" NoDetailRecordsText="No se encontraron registros ..." autogeneratecolumns="False" caption="CAPTURAS" clientdatakeynames="IdReg" commanditemdisplay="Top" datakeynames="IdReg" font-size="12px">
                                      <NoRecordsTemplate>
                                            <table style="width:100%; border:0; padding:20px; border-spacing:20px;">
                                                <tr>
                                                    <td style="text-align:center;">
                                                        <h2 style="color:Black">Registros no encontrados, Favor de volver a realizar la consultar</h2>
                                                    </td>
                                                </tr>
                                            </table>
                                         </NoRecordsTemplate>
                                     
                                       <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                       <rowindicatorcolumn filtercontrolalttext="Filter RowIndicator column">
                                       </rowindicatorcolumn>
                                       <expandcollapsecolumn created="True" filtercontrolalttext="Filter ExpandColumn column" visible="True">
                                       </expandcollapsecolumn>
                                       <Columns>

                                     <telerik:GridBoundColumn DataField="IdReg" DataType="System.Int64" HeaderText="IDREG" Visible="false" />
                                     <telerik:GridBoundColumn DataField="IdProducto" DataType="System.Int64" HeaderText="IDPROD" Visible="false" />
                                     <telerik:GridBoundColumn DataField="IdCentro" DataType="System.Int64" HeaderText="IDCENTRO" Visible="false" /> 
                                     <telerik:GridBoundColumn DataField="IdInst" DataType="System.Int64" HeaderText="IDINST" Visible="false" /> 
                                     <telerik:GridBoundColumn DataField="IdServicio" DataType="System.String" HeaderText="IDSERV" Visible="false" /> 
                                     <telerik:GridBoundColumn DataField="IdBarco" DataType="System.Int64" HeaderText="IDBARCO" Visible="false" />  
                                     <telerik:GridBoundColumn DataField="Referencia_folio" DataType="System.Int64" HeaderText="REF_FOLIO" Visible="false" />   

                                            <telerik:GridBoundColumn DataField="Estatus_revisado" DataType="System.String" HeaderText="E. Rev." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65px" >
                                     <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Estatus_pagado" DataType="System.String" HeaderText="E. Pag." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65px">
                                     <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Orden_servicio" DataType="System.String" HeaderText="No. Orden" ItemStyle-HorizontalAlign="Center" >
                                     <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="NombreCentro" DataType="System.String" HeaderText="Centro" UniqueName="NombreCentro"/>
                                    <telerik:GridBoundColumn DataField="NombreServicio" DataType="System.String" HeaderText="Servicio" UniqueName="NombreServicio"/>
                                    <telerik:GridBoundColumn DataField="NombreProducto" DataType="System.String" HeaderText="Producto" UniqueName="NombreProducto"/>

                                    <telerik:GridBoundColumn DataField="Cant_insp_mezcla" HeaderText="Cantidad Insp." DataType="System.Decimal" UniqueName="Cant_insp_mezcla" DataFormatString="{0:### ##0.000}">
                                        <ItemStyle HorizontalAlign="right"/>
                                      </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Fecha" DataType="System.DateTime" HeaderText="Fecha"   HeaderStyle-Width="10%"
                                    DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center">
                                     <ItemStyle HorizontalAlign="Left"/>
                                    </telerik:GridBoundColumn> 

                                           <telerik:GridBoundColumn DataField="Folio_cert_calidad" DataType="System.String" HeaderText="F. Calidad" UniqueName="Folio_cert_calidad"/>
                                    <telerik:GridBoundColumn DataField="Folio_cert_cant" DataType="System.String" HeaderText="F. Cantidad" UniqueName="Folio_cert_cant"/>

                                       </Columns>
                                       <editformsettings>
                                           <editcolumn filtercontrolalttext="Filter EditCommandColumn column">
                                           </editcolumn>
                                       </editformsettings>
                                       <PagerStyle PageSizeControlType="RadComboBox" />
                                   </mastertableview>
                                   <clientsettings>
                                       <Selecting AllowRowSelect="true" />
                                       <Scrolling AllowScroll="True" FrozenColumnsCount="2" SaveScrollPosition="true" UseStaticHeaders="True" />                                                                            
                                   </clientsettings>
                                   <filtermenu enableimagesprites="False">
                                   </filtermenu>
                               </telerik:RadGrid>
                                                 </td>                                                
                                             </tr>                                           
                                         </table>
                                     </ItemTemplate>                                             
                                         </telerik:RadPanelItem>
                                     </Items>
                                 </telerik:RadPanelItem>
                             </Items>

                         </telerik:RadPanelBar>
                    
                </li>--%>
                </ul>
            </div>

            <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" Visible="false"
                ShowSummary="true" DisplayMode="BulletList" CssClass="field-validation-error" />


        </telerik:RadAjaxPanel>


        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
        </telerik:RadWindowManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxManager1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" EnableEmbeddedSkins="false">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                var $ = $telerik.$;
                function validationFailed(radAsyncUpload, args) {
                    var $row = $(args.get_row());
                    var erorMessage = getErrorMessage(radAsyncUpload, args);
                    var span = createError(erorMessage);
                    $row.addClass("ruError");
                    $row.append(span);
                }
                function getErrorMessage(sender, args) {
                    var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                    if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                        if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                            return ("Archivo no soportado.");
                        }
                        else {
                            return ("This file exceeds the maximum allowed size of 500 KB.");
                        }
                    }
                    else {
                        return ("not correct extension.");
                    }
                }
                function createError(erorMessage) {
                    var input = '<span class="ruErrorMessage">' + erorMessage + ' </span>';
                    return input;
                }
            </script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
