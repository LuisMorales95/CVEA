<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cambiarpass.aspx.cs" Inherits="webAppTesoreria.Account.cambiarpass" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <telerik:radajaxpanel ID="RadAjaxPanel1" runat="server">  

      <div class="row">
        <div class="col-md-12">
            <section id="passwordForm">
                
                    <div class="form-horizontal">
                         <p style="margin-top:20px;">Inició sesión como <strong><asp:Label ID="lblid" runat="server"></asp:Label></strong>.</p>
                        <h4>Formulario para cambiar contraseña</h4>
                        <hr />
                        <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                        <div class="form-group">
                            <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword" CssClass="col-md-2 control-label">Contraseña actual</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="CurrentPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="text-danger" ErrorMessage="El campo de contraseña actual es obligatorio."
                                    ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword" CssClass="col-md-2 control-label">Nueva contraseña</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="NewPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                                    CssClass="text-danger" ErrorMessage="La contraseña nueva es obligatoria."
                                    ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword" CssClass="col-md-2 control-label">Confirmar la nueva contraseña</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="La confirmación de la nueva contraseña es obligatoria."
                                    ValidationGroup="ChangePassword" />
                                <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="La nueva contraseña y la contraseña de confirmación no coinciden."
                                    ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" Text="Cambiar Password" ValidationGroup="ChangePassword" OnClick="ChangePassword_Click" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>

                 </section>
            </div>
          </div>
          </telerik:radajaxpanel>
      <telerik:RadWindowManager ID="windowManager1" runat="server" Animation="Resize">         
         </telerik:RadWindowManager>
</asp:Content>
