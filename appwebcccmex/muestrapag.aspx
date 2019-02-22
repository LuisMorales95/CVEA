<%@ Page Title="" Language="C#" MasterPageFile="~/caminPage.Master" AutoEventWireup="true" CodeBehind="muestrapag.aspx.cs" Inherits="appwebcccmex.muestrapag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <div class="row" style="margin-top:10px;">
              
            <div class="col-md-12 d-flex align-items-stretch grid-margin">
                <div class="row flex-grow">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header bg-light">
                               Centros de trabajo
                            </div>
                            <div class="card-body">


                                <div class="form-group row">
                                    
                                   <div class="col-sm-12">
                                          <label class="col-form-label col-sm-2" for="lblc">Contribuyente:</label>
                                  
                                        <asp:Label ID="lblcontribuyente" CssClass="col-form-label col-sm-10" runat="server" Font-Bold="true" Text="!!!" />
                                    
                                        </div>
                                         

                                        <div class="col-sm-12">
                                              <label class="col-form-label col-sm-2" for="lblc2">Clave Catastral:</label>                                   
                                        <asp:Label ID="lblclave" class="ccol-form-label col-sm-10" runat="server" Font-Bold="true" Text="!!!" />
                                   
                                            </div>

                                      <div class="col-sm-12">
                                           <label class="col-form-label col-sm-2" for="lblc3">Monto a pagar:</label>
                                    
                                        <asp:Label ID="lblmonto" runat="server" class="col-form-label col-sm-10" Font-Bold="true" Text="!!!" />
                                    
                                          </div>

                                     <div class="col-sm-10 offset-2">
                                           <asp:Label ID="lblletra" runat="server" Font-Bold="true" Font-Italic="true" ForeColor="Red" Text="!!!" />
                                         </div>
                                    </div>

                                 <div class="col-sm-3 offset-2 text-center">
                                      <asp:ImageButton ID="btnBancomer" runat="server" Width="250%" ImageUrl="~/images/boton_bancomer2.png" AlternateText="Pagar" />
                                     </div>

                                </div>
 
                            </div>

                        </div>
                    </div>

                </div>

            </div>

</asp:Content>
