<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="capturaeventos.aspx.cs" Inherits="appwebcccmex.capturaeventos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var arrastrar = false;
        var delta = new Object();
        var L = 5;
        var paso = 2;
        var R = 100;
        var X; 
        var Y; 
        var objx = 100;
        var objy = 100;
        var inc = 20;
        //function Objeto(x, y, width, height, idevento, color) {
        //    this.x = x;
        //    this.y = y;
        //    this.w = width;
        //    this.h = height;
        //    this.color = color;
        //    this.idevento = idevento;
        //    //alert(this.idevento);
        //}
        //function DibujarObjetos() {

        //    for (var i = 0; i < 1; i++) {
        //        dibujarAlarma(objetos[i].x, objetos[i].y, objetos[i].w, objetos[i].h, objetos[i].color); // rectángulo amarillo
        //    }

        //}
        function dibujarObjeto(x, y, color) {
            contexto.drawImage(imagen, 0, 0, 800, 700);
            contexto.strokeStyle = color;
            contexto.globalAlpha = 0.85;

            contexto.lineWidth = 5;
            contexto.stroke();
            contexto.fillRect(x, y, 20, 20);
            contexto.strokeRect(x, y, 20, 20); // rectángulo amarillo

            objx = x;
            objy = y;
            //contexto.beginPath();
            //for (var i = 0; i < 4; i++) {
            //    a = x + 20 * i;
            //    b = y + 20 * i;
            //    contexto.lineTo(a, b);
            //}
            //contexto.fillStyle = color;
            //contexto.lineTo(x, y);
            //contexto.lineTo(x + 5, y);
            //contexto.lineTo(x, y + 5);
            //contexto.lineTo(x, y + 15);
            //contexto.closePath();

        }
        function CargarDiagrama(diagrama) {
             
            canvas = null;
            canvas = document.getElementById('eventosCanvas');
             X = canvas.width / 2;
             Y = canvas.height / 2;
            contexto = canvas.getContext('2d');
            imagen = new Image();
            imagen.src = "diagramas/" + diagrama;
            //alert(imagen.src);
            imagen.onload = function () {
                canvas.width = this.width;
                canvas.height = this.height;
                //contexto.drawImage(imagen, 0, 0);  // imagen completa en la posición (0,0) 
                 // escalado
                //contexto.drawImage(imagen,130,85,80,60,285,205,150,107); // escalado de una porción
                
                dibujarObjeto(X, Y, 'BLUE');
                //canvas.addEventListener('mousemove', function (evt) {
                //    var mousePos = getMousePos(canvas, evt);
                //    var message = 'Mouse position: ' + mousePos.x + ',' + mousePos.y;
                     
                //    miAlerta(mousePos);
                //}, false);
                function oMousePos(canvas, evt) {
                    var rect = canvas.getBoundingClientRect();
                    return {// devuelve un objeto
                        X: Math.round(evt.clientX - rect.left),
                        Y: Math.round(evt.clientY - rect.top)
                    };
                }
                canvas.addEventListener("mousedown", function (evt) {
                    var mousePos = oMousePos(canvas, evt);
                    dibujarObjeto(X, Y, 'BLUE');
                    //alert('X:' + objx + ' Y:' + objy);
                    //alert('X:' + objx + ' Y:' + objy + 'X1:' + mouse.X + ' Y1:' + mouse.Y);
                    //if (contexto.isPointInPath(mousePos.X, mousePos.Y)) {
                    //if ((mouse.X > objx ) && (mouse.Y > objy)) {
                    if ((mousePos.X > X && mousePos.X < X + 25) && (mousePos.Y > Y && mousePos.Y < Y + 25)) {
                        arrastrar = true;
                        
                        delta.X = X - mousePos.X;
                        delta.Y = Y - mousePos.Y;
                    }
                }, false);
                canvas.addEventListener("mousemove", function (evt) {
                    var mousePos = oMousePos(canvas, evt);
                    if (arrastrar) {
                        contexto.clearRect(0, 0, canvas.width, canvas.height);
                        X = mousePos.Y + delta.X, Y = mousePos.Y + delta.Y
                        //dibujarObjeto(X, Y, 'BLUE');
                    }
                }, false);
                canvas.addEventListener("mouseup", function (evt) {
                    var mousePos = oMousePos(canvas, evt);
                    dibujarObjeto(mousePos.X, mousePos.Y, 'BLUE');
                    //alert('X:' + objx + ' Y:' + objy);
                    arrastrar = false;
                    document.getElementById("coordenadas").innerHTML = 'X:' + objx + ' Y:' + objy;
                }, false);
            }
             
        }
        </script>
</head>
<body onload="CargarDiagrama('DIAGRAMA682.JPG');">
    <form id="form1" runat="server">
        <br />
        <p>Diagrama</p>
        <asp:Label runat="server" ID="coordenadas" Text="coordenadas" Font-Bold="True"></asp:Label><br />
<div style="width:800">
<canvas id="eventosCanvas" width="800">
 <p>Tu navegador no soporta canvas: Aquí deberías de ver una imagen :</p>
</canvas>
  </div>
    </form>
</body>
</html>
