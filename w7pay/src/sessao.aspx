<%@ Page Title="" Language="C#" MasterPageFile="~/src/geral.master" AutoEventWireup="true" CodeBehind="sessao.aspx.cs" Inherits="w7pay.src.sessao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <!-- Title and Top Buttons Start -->
          
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
                  <img src="img/logo/imaginelogo.png" alt="Logo" width="100">
                </div>
              </div>
              <!-- Title End -->

              <!-- Top Buttons Start -->
              <div class="w-100 d-md-none"></div>
              
              <!-- Top Buttons End -->
            </div>
          </div>
          <!-- Title and Top Buttons End -->
           <h1>Sessão expirada</h1>
                      <div class="mb-3 w-100">
                        <label class="form-label">Clique para realizar o acesso novamente.</label>
                        
                      </div>
                      <div class="w-100 mb-0">
         <asp:Button ID="Button1" runat="server" Text="Ir para Login" CssClass="btn btn-outline-primary btn-icon btn-icon-start" OnClick="Button1_Click" />              
              </div>
                  </div>
                </div>
              </div>
            </div>
          </div> 


</asp:Content>
