<%@ Page Title="" Language="C#" MasterPageFile="~/src/lead/masterlead.master" AutoEventWireup="true" Async="true" CodeBehind="profile.aspx.cs" Inherits="w7pay.src.lead.profile2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
    <asp:HiddenField ID="hdfIdUsuario" runat="server" />
    <script src="js/mascara.js"></script>
    <!-- Title and Top Buttons Start -->
          <div class="page-title-container">
            <div class="row g-0">
              <!-- Title Start -->
              <div class="col-auto mb-3 mb-md-0 me-auto">
                <div class="w-auto sw-md-30">
                  <a href="#" class="muted-link pb-1 d-inline-block breadcrumb-back">
                    <i data-acorn-icon="chevron-left" data-acorn-size="13"></i>
                    <span class="text-small align-middle">Página Inicial</span>
                  </a>
                  <h1 class="mb-0 pb-0 display-4" id="title">Meu perfil</h1>
                </div>
              </div>
              <!-- Title End -->

              <!-- Top Buttons Start -->
              <div class="w-100 d-md-none"></div>
              <div class="col-auto d-flex align-items-end justify-content-end">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-outline-primary btn-icon btn-icon-start" OnClick="btnSalvar_Click" />        
                  <br />
                  <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
              </div>
              <!-- Top Buttons End -->
            </div>
          </div>
          <!-- Title and Top Buttons End -->

          <div class="row mb-n5">
            <div class="col-xl-4">
              <div class="mb-5">
                <h2 class="small-title">Dados do Usuário</h2>
                <div class="card">
                  <div class="card-body">
                      <div class="mb-3">
                        <label class="form-label">Nome Completo</label>
                        <asp:TextBox id="txtNomeCompleto" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Como gostaria de ser chamado</label>
                        <asp:TextBox id="txtNomeUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">E-mail</label>
                        <asp:TextBox id="txtEmailUsuario" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">É revendedor? Link de Venda</label>
                        <asp:TextBox id="txtLinkRevenda" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                  </div>
                </div>
              </div>
            
        <%--  <div class="row">
                
              <div class="mb-5">
                <h2 class="small-title">Dados do Plano</h2>
                <div class="card">
                  <div class="card-body">
                      <div class="mb-3 w-100">
                        <label class="form-label">Plano atual</label>
                        <%--<asp:DropDownList runat="server" id="ddlPlano" CssClass="form-control" DataSourceID="sdsPlanos" DataTextField="nomeplano" DataValueField="idplano">                            
                        </asp:DropDownList>
            <asp:SqlDataSource ID="sdsPlanos" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select idplano, nomeplano from imagine_planos"></asp:SqlDataSource>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Status</label><br />
                        <asp:Label id="lblStatusPlano" runat="server"></asp:Label>
                       
                      </div>
                         <div class="mb-3">
                             <a href="faturas.aspx">Ver minhas faturas</a>
                            </div>
                  </div>
                </div>
                    </div>
              </div>--%>
        </div>
            <div class="col-xl-8">
              <div class="mb-5">
                <h2 class="small-title">Dados da Empresa</h2>
                <div class="card">
                  <div class="card-body">
                            <div class="mb-3">
                        <label class="form-label">Logo (50x50px/.jpg ou .png)</label>
                        <asp:FileUpload id="fluLogo" runat="server" CssClass="form-control"></asp:FileUpload>
                            <asp:Image ID="imgLogo" runat="server" Width="100px" />
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Nome da Empresa(Fantasia)</label>
                        <asp:TextBox id="txtNomeEmpresa" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                            <div class="mb-3">
                        <label class="form-label">Razão Social</label>
                        <asp:TextBox id="txtRazaoSocial" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                            <div class="mb-3">
                        <label class="form-label">CNPJ</label>
                        <asp:TextBox id="txtCNPJ" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Telefone</label>
                        <asp:TextBox id="txtTelefone" onkeyup="formataTelefone(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">E-mail</label>
                        <asp:TextBox id="txtEmailEmpresa" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">CEP</label>
                        <asp:TextBox id="txtCEP" CssClass="form-control" onkeyup="formataCEP(this,event);" runat="server" AutoPostBack="True" OnTextChanged="txtCEP_TextChanged"></asp:TextBox>
                      </div>
                      <div class="mb-3 w-100">
                        <label class="form-label">UF</label>
                            <asp:DropDownList runat="server" id="ddlUF" CssClass="form-control">
                        <asp:ListItem Text="Acre - AC" Value="AC"/>
  <asp:ListItem Text="Alagoas - AL" Value="AL"/>
  <asp:ListItem Text="Amapá - AP" Value="AP"/>
  <asp:ListItem Text="Amazonas - AM" Value="AM"/>
  <asp:ListItem Text="Bahia - BA" Value="BA"/>
                            <asp:ListItem Text="Ceará - CE" Value="CE"/>
                            <asp:ListItem Text="Espiríto Santo - ES" Value="ES"/>
                            <asp:ListItem Text="Goiás - GO" Value="GO"/>
                            <asp:ListItem Text="Maranhão - MA" Value="MA"/>
                            <asp:ListItem Text="Mato Grosso - MT" Value="MT"/>
                            <asp:ListItem Text="Mato Grosso do Sul - MS" Value="MS"/>
                            <asp:ListItem Text="Minas Gerais - MG" Value="MG"/>
                            <asp:ListItem Text="Pará - PA" Value="PA"/>
                            <asp:ListItem Text="Paraíba - PB" Value="PB"/>
                            <asp:ListItem Text="Paraná - PR" Value="PR"/>
                            <asp:ListItem Text="Pernambuco - PE" Value="PE"/>
                            <asp:ListItem Text="Piauí - PI" Value="PI"/>
                            <asp:ListItem Text="Rio de Janeiro - RJ" Value="RJ"/>
                            <asp:ListItem Text="Rio Grande do Norte - RN" Value="RN"/>
                            <asp:ListItem Text="Rio Grande do Sul - RS" Value="RS"/>
                            <asp:ListItem Text="Rondônia - RO" Value="RO"/>
                            <asp:ListItem Text="Roraima - RR" Value="RR"/>
                            <asp:ListItem Text="Santa Catarina - SC" Value="SC"/>
                            <asp:ListItem Text="São Paulo - SP" Value="SP"/>
                            <asp:ListItem Text="Sergipe - SE" Value="SE"/>
                            <asp:ListItem Text="Tocantins - TO" Value="TO"/>
                            <asp:ListItem Text="Distrito Federal - DF" Value="DF"/>
                            </asp:DropDownList>
                      </div>
                      <div class="mb-3 w-100">
                        <label class="form-label">Cidade</label>
                        <asp:TextBox id="txtCidade" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Endereço</label>
                        <asp:TextBox id="txtEndereco" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Número</label>
                        <asp:TextBox id="txtNum" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="mb-3">
                        <label class="form-label">Bairro</label>
                        <asp:TextBox id="txtBairro" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                  </div>
                </div>
              </div>

            
            </div>
          </div>    
</asp:Content>
