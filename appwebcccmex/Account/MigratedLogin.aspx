<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="MigratedLogin.aspx.cs" Inherits="appwebcccmex.Account.MigratedLogin" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <section id="loginForm">
        <asp:Login ID="login1" runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
            <LayoutTemplate>
                <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                <fieldset>

                    <div class="col-md-12 d-flex align-items-stretch grid-margin">
                        <div class="row flex-grow">
                            <div class="col-12 ">
                                <div class="card">
                                    <div class="card-header text-github">
                                        Formulario de inicio de sesión
                                    </div>
                                    <div class="card-body badge-outline-light text-dark">
                                        <div class="row">
                                            <div class="col-6" >
                                                <div class="form-group">
                                                    <div>
                                                        <label class="col-form-label" for="lblc">Nombre de usuario</label>
                                                        <div class="align-items-center">
                                                            <asp:TextBox runat="server" Width="80%" ID="UserName" CssClass="form-control" BackColor="White" /><br />
                                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="get" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="El campo de nombre de usuario es obligatorio." />
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <label class="col-form-label" for="lblc2">Contraseña</label>
                                                        <div class="align-items-center">
                                                            <asp:TextBox runat="server" ID="Password" Width="80%" TextMode="Password" CssClass="form-control" BackColor="White" /><br />
                                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="get" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="El campo de contraseña es obligatorio." />
                                                        </div>
                                                    </div>
                                                </div>  
                                                <div class="form-group">
                                                    <div class="form-check mb-3">

                                                        <asp:CheckBox runat="server" ID="CheckBox1" CssClass="form-check-input" />
                                                        <label class="form-check-label" for="exampleCheck1">¿Recordar cuenta?</label>

                                                    </div>
                                                    <div>
                                                        <asp:Button ID="Button1" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary text-center p-2" Font-Size="18px" Width="80%" OnClick="cmdSesion_Click" />
                                                        <%--<telerik:RadButton ID="cmdSesion" runat="server" Width="100%"  CssClass="btn btn-google" Font-Size="18px" RenderMode="Lightweight" Text="Iniciar sesión" OnClick="cmdSesion_Click">--%>
                                                        <%--</telerik:RadButton>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">

                                                <section id="socialLoginForm">
                                                    <img src="Images/test.png" class="rounded img-fluid img-thumbnail" />
                                                    <%--<h2>colocar logo</h2>--%>
                                                    <%--<uc:openauthproviders runat="server" id="OpenAuthLogin" />--%>
                                                </section>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </LayoutTemplate>
        </asp:Login>
        <div class="col-sm-4 text-center">
            <%-- extra validations --%>
            <asp:ValidationSummary ID="valResumen" runat="server" ShowMessageBox="true" ShowSummary="true" DisplayMode="BulletList" />
        </div>
    </section>


    <%--    <section id="socialLoginForm">
        <img src="Images/CCCLogo2.png" />
        <h2>colocar logo</h2>
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>--%>


    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
    </telerik:RadWindowManager>

</asp:Content>

