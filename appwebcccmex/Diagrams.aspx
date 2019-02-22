<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="Diagrams.aspx.cs" Inherits="appwebcccmex.Diagrams" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-1.8.2.js" type="text/javascript"></script>


    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            CENTRO - <asp:Label ID="nameCentro" runat="server" Text="..." Font-Bold="true"></asp:Label>
                        </div>
                        <div class="card-body badge-outline-secondary text-dark">
                            <div class="form-group row">
                                <%-- each item --%>
                                <div class="col-sm-12">

                                    <label class="col-form-label col-sm-2" for="lblc">Centro</label>

                                    <div class=" col-sm-4">
                                        <telerik:RadComboBox ID="cmbcentro" runat="server" Skin="Bootstrap" OnSelectedIndexChanged="Cmbcentro_SelectedIndexChanged"
                                            AutoPostBack="true" EmptyMessage="- Seleccione el centro -" Width="410px" MarkFirstMatch="true">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                               
                                <div class="col-sm-12">

                                    <label class="col-form-label col-sm-2" for="lblc">Instalaciones</label>

                                    <div class=" col-sm-4">
                                        <telerik:RadComboBox ID="cmbInstalacion" runat="server" Skin="Bootstrap" AutoPostBack="true"
                                            EmptyMessage="- Selecciona la instalación -" Width="410px" MarkFirstMatch="true"
                                            OnSelectedIndexChanged="cmbInstalacion_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                            </div>

                            <hr />

                            <div class="col-sm-12">
                                <%-- table grid with other skins --%>
                                <canvas id="eventosCanvas">
                                    <p>Tu navegador no soporta canvas: Aquí deberías de ver una imagen :</p>
                                </canvas>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <%-- RadWindowManager add skin-bootstrap --%>
    
        <telerik:RadWindowManager ID="ManejadorRadWindow" runat="server" Skin="Bootstrap">
            <Windows>
                <telerik:RadWindow ID="VentanaRad" runat="server" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false"
                    Title="Gestiòn de Eventos">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                //objetos js globales
                var objetos;
                var canvas;
                var contexto;
                var imagen;


                //Falta implementacion de atributos.
                function Objeto(x, y, width, height, idevento, color, tipo) {
                    this.x = x;
                    this.y = y;
                    this.w = width;
                    this.h = height;
                    this.color = color;
                    this.idevento = idevento;
                    this.tipo = tipo;
                    //alert(this.idevento);
                }

                function dibujarTipos() {
                    contexto.strokeStyle = "yellow";

                    //contexto.globalAlpha = 0.85;

                    contexto.lineWidth = 4;
                    contexto.stroke();
                    //contexto.fillRect(200, 5, 20, 20);
                    contexto.strokeRect(10, 20, 10, 10); // rectángulo amarillo
                    contexto.strokeStyle = "cyan";
                    contexto.strokeRect(10, 5, 10, 10);
                    contexto.font = '10pt Arial';
                    contexto.fillStyle = 'black';
                    contexto.fillText('Mantenimiento', 25, 15);
                    contexto.fillText('Calibración', 25, 30);
                }

                function dibujarAlarma(x, y, w, h, color, tipo) {
                    if (tipo == 'Mantenimiento')
                        contexto.strokeStyle = "cyan";
                    else
                        contexto.strokeStyle = "yellow";
                    contexto.globalAlpha = 0.85;

                    contexto.lineWidth = 5;
                    contexto.stroke();
                    contexto.fillStyle = color;
                    contexto.fillRect(x, y, 20, 20);
                    contexto.strokeRect(x, y, 20, 20); // rectángulo amarillo

                }
                //Dibuja los objetos cargados
                function DibujarObjetos() {

                    for (var i = 0; i < objetos.length; i++) {
                        dibujarAlarma(objetos[i].x, objetos[i].y, objetos[i].w, objetos[i].h, objetos[i].color, objetos[i].tipo); // rectángulo amarillo
                    }

                }

                function CargarDiagrama(diagrama) {

                    canvas = null;
                    canvas = document.getElementById('eventosCanvas');
                    contexto = canvas.getContext('2d');
                    imagen = new Image();
                    imagen.src = "diagramas/" + diagrama;
                    //alert(imagen.src);
                    imagen.onload = function () {
                        canvas.width = this.width;
                        canvas.height = this.height;
                        //contexto.drawImage(imagen, 0, 0);  // imagen completa en la posición (0,0) 
                        contexto.drawImage(imagen, 0, 0, 800, 700);

                        //contexto.drawImage(imagen,130,85,80,60,285,205,150,107); // escalado de una porción
                        dibujarTipos();
                        DibujarObjetos();
                        canvas.addEventListener('mousemove', function (evt) {
                            var mousePos = getMousePos(canvas, evt);
                            var message = 'Mouse position: ' + mousePos.x + ',' + mousePos.y;

                            miAlerta(mousePos);
                        }, false);
                    }

                }

                function myFunction(num) {
                    //some code here
                    alert('Function called successfully! ' + num);
                }
                function miAlerta(mouse) {

                    for (var i = 0; i < objetos.length; i++) {
                        var x2 = objetos[i].x + objetos[i].w;
                        var y2 = objetos[i].y + objetos[i].h;
                        if ((mouse.x > objetos[i].x && mouse.x < x2) && (mouse.y > objetos[i].y && mouse.y < y2)) {
                            //alert(objetos[i].idevento);
                            mouse.x = 0;
                            mouse.y = 0;
                            AbrirRadWindow(objetos[i].idevento);
                            break;
                        }
                    }
                }

                function writeMessage(canvas, message) {
                    var context = canvas.getContext('2d');
                    //context.clearRect(0, 0, canvas.width, canvas.height);
                    context.font = '18pt Calibri';
                    context.fillStyle = 'black';
                    context.fillText(message, 10, 25);
                }
                function getMousePos(canvas, evt) {
                    var rect = canvas.getBoundingClientRect();
                    return {
                        x: Math.round(evt.clientX - rect.left),
                        y: Math.round(evt.clientY - rect.top)
                    };
                }
                function AbrirRadWindow(idevento) {
                    var radwindow = $find('<%=VentanaRad.ClientID %>');
                    radwindow.setUrl('modal_cccmex_infoevento.aspx?EventoID=' + idevento);
                    //window.radopen("modal_cccmex_infoevento.aspx?EmployeeID=2", "Información de Eventos");
                    radwindow.show();

                }
            </script>
        </telerik:RadCodeBlock>

</asp:Content>


<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
        }
    </style>
</asp:Content>