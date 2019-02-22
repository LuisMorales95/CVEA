<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="appwebcccmex.Account.Login" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

 

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <section id="loginForm">
         <img src="Images/CCCLogo3.png" />
        <asp:Login ID="login1"  runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
            <LayoutTemplate>
                <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                <fieldset>
                    <legend>Formulario de inicio de sesión</legend>

                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="UserName">Nombre de usuario</asp:Label>
                            <asp:TextBox runat="server" ID="UserName" BackColor="White" /><br />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="get"  ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="El campo de nombre de usuario es obligatorio." />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Password">Contraseña</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" BackColor="White" /><br />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="get"  ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="El campo de contraseña es obligatorio." />
                        </li>
                        <li>
                            <asp:CheckBox runat="server" ID="RememberMe" />
                            <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">¿Recordar cuenta?</asp:Label>
                        </li>
                    </ol>
                      <telerik:RadButton ID="cmdSesion" runat="server" Width="58%" Skin="Silk" Height="40px" Text="Iniciar sesión" OnClick="cmdSesion_Click">                             
                                </telerik:RadButton>
                   <%-- <asp:Button runat="server" CommandName="Login" Text="Iniciar sesión"  Visible="false"/>--%>
                </fieldset>
            </LayoutTemplate>
        </asp:Login>

         <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" ShowSummary="true" DisplayMode="BulletList"/>
        <%--<p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Registrarse</asp:HyperLink>
            si no tiene una cuenta.
        </p>--%>
    </section>

    <section id="socialLoginForm">
        <img src="Images/CCCLogo2.png" />
        <%--<h2>colocar logo</h2>--%>
       <%-- <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />--%>
    </section>

    
      <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">
      </telerik:RadWindowManager>

</asp:Content>
