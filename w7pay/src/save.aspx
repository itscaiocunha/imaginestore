<%@ Page Title="" Language="C#" MasterPageFile="~/src/geral.master" Async="true" AutoEventWireup="true" CodeBehind="save.aspx.cs" Inherits="w7pay.src.save" %>

  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Title and Top Buttons Start -->
      <script src="js/mascara.js"></script>
    <div class="row mb-n5">
      <div class="col-xl-12">

        <div class="mb-5">

          <div class="card">
            <div class="card-body">
              <div class="page-title-container">
                <div class="row g-0">
                  <!-- Title Start -->
                  <div class="col-auto mb-3 mb-md-0 me-auto">
                    <div class="w-auto sw-md-30">
                      <img src="img/logo/logo.png" alt="Logo" width="200">
                    </div>
                  </div>
                  <!-- Title End -->

                  <!-- Top Buttons Start -->
                  <div class="w-100 d-md-none"></div>

                  <!-- Top Buttons End -->
                </div>
              </div>
              <!-- Title and Top Buttons End -->
              <div class="mb-3 w-100">
                <label class="form-label">CNPJ</label>
                <asp:TextBox id="txtCNPJ" onkeyup="formataCNPJ(this,event);" CssClass="form-control" runat="server" Required></asp:TextBox>
              </div>
              <div class="mb-3 w-100">
                <label class="form-label">E-mail</label>
                <asp:TextBox id="txtEmail" CssClass="form-control" runat="server" Required>
                </asp:TextBox>
              </div>
              <div class="w-100 mb-0" align="left">
                <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
              </div>

              <div class="w-100 mb-0">
                <asp:Button ID="Button1" runat="server" Text="Salvar"
                  CssClass="btn btn-outline-primary btn-icon btn-icon-start" OnClick="Button1_Click" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </asp:Content>