<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="appwebcccmex.error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Pagina de error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">       
        <asp:ImageButton id="imgerror" runat="server"  ImageUrl="~/Images/error404.png" Height="420px" ImageAlign="Middle" OnClick="imgerror_Click" Width="630px"/>
    </div>
    </form>
</body>
</html>
