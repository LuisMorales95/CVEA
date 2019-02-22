<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="diagrama.aspx.cs" Inherits="appwebcccmex.diagramas.diagrama" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <div>
        <canvas id="miCanvas" width="900" height="500">
        <p>Su navegador no soporta HTML5</p>
        </canvas>

    </div>

        hola
    </form>

</asp:Content>