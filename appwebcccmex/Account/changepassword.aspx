<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/caminPage.Master" CodeBehind="changepassword.aspx.cs" Inherits="appwebcccmex.Account.changepassword" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="col-md-12 grid-margin">
            <div class="row ">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header text-github">
                            <p style="margin-top: 20px;">
                                Inició sesión como <strong>
                                    <asp:Label ID="lblid" runat="server"></asp:Label></strong>.
                            </p>
                            <h4>Formulario para cambiar contraseña</h4>
                        </div>
                        <div class="card-body badge-outline-light text-dark">
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group">
                                        <div>
                                            <label class="col-form-label" for="lblc">Contraseña actual</label>
                                            <div class="align-items-center">
                                                <asp:TextBox runat="server" ID="CurrentPassword" TextMode="Password" Width="80%" CssClass="form-control" BackColor="#e8f0fe" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                                                    CssClass="field-validation-error" ErrorMessage="El campo de contraseña actual es obligatorio."
                                                    ValidationGroup="ChangePassword" />
                                            </div>
                                        </div>
                                        <div>
                                            <label class="col-form-label" for="lblc">Nueva contraseña</label>
                                            <div class="align-items-center">
                                                <asp:TextBox runat="server" ID="NewPassword" TextMode="Password" Width="80%" CssClass="form-control" BackColor="#e8f0fe" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                                                    CssClass="field-validation-error" ErrorMessage="La contraseña nueva es obligatoria."
                                                    ValidationGroup="ChangePassword" />
                                            </div>
                                        </div>
                                        <div>
                                            <label class="col-form-label" for="lblc">Confirmar la nueva contraseña</label>
                                            <div class="align-items-center">
                                                <asp:TextBox runat="server" ID="ConfirmNewPassword" TextMode="Password" Width="80%" CssClass="form-control" BackColor="#e8f0fe" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La confirmación de la nueva contraseña es obligatoria."
                                                    ValidationGroup="ChangePassword" />
                                                <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La nueva contraseña y la contraseña de confirmación no coinciden."
                                                    ValidationGroup="ChangePassword" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-check mb-3">
                                        <div class="col-md-offset-2">
                                            <asp:CheckBox runat="server" ID="CheckBox1" CssClass="form-check-input" />
                                            <label class="form-check-label" for="exampleCheck1">¿Recordar cuenta?</label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div>
                                            <asp:Button runat="server" Text="Cambiar Password" ValidationGroup="ChangePassword" CssClass="btn btn-primary text-center p-2" Font-Size="18px" Width="80%" OnClick="ChangePassword_Click" />
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
    </telerik:RadAjaxPanel>

    <%-- RadWindowManager add skin-bootstrap --%>

    <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize" Skin="Bootstrap">
    </telerik:RadWindowManager>

</asp:Content>
