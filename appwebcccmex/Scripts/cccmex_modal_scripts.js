function ObtenerRadWindow() {
    var oWindow = null;
    if (window.radWindow) oWindow = window.radWindow;
    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
    return oWindow;
}

function AjustarRadWindow() {
    var oWindow = ObtenerRadWindow();
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

function ConfirmarUptRadWindow(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            this.click();
        }
    });

    var text = "Esta seguro de actualizar?";
    radconfirm(text, callBackFunction, 300, 160, null, "Confirmación");
    args.set_cancel(true);
}

function ConfirmarDelRadWindow(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            this.click();
        }
    });

    var text = "Esta seguro de eliminar?";
    radconfirm(text, callBackFunction, 300, 160, null, "Confirmación");
    args.set_cancel(true);
}

function CloseAndRebind(args) {
  
    ObtenerRadWindow().BrowserWindow.refreshGrid(args);
    ObtenerRadWindow().close();

}

function Close(args) {

    ObtenerRadWindow().close();

}

