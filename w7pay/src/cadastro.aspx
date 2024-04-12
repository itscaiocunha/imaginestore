<%@ Page Title="" Language="C#" MasterPageFile="~/src/geral.master" AutoEventWireup="true" CodeBehind="cadastro.aspx.cs" Inherits="w7pay.src.cadastro2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:HiddenField ID="hdfRevendedor" runat="server" />
            <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
            <asp:HiddenField ID="hdfToken" runat="server" />
            <script src="js/mascara.js"></script>
            <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="4000" OnTick="Timer1_Tick"></asp:Timer>
            <!-- Title and Top Buttons Start -->

            <div class="row mb-n5">
                <asp:Panel ID="pnlCadastro" runat="server">
                    <div class="col-xl-12">

                        <div class="mb-5">

                            <div class="card">
                                <div class="card-body">
                                    <div class="page-title-container">
                                        <div class="row g-0">
                                            <!-- Title Start -->
                                            <div class="col-auto mb-3 mb-md-0 me-auto">
                                                <div class="w-auto sw-md-30">
                                                    <img src="img/logo/logo.png" alt="Logo" width="100">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Title End -->

                                    <h1 class="">Cadastro na Plataforma</h1>
                                    <!-- Title and Top Buttons End -->
                                    <h2 class="small-title">Preencha os dados</h2>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">CNPJ/CPF</label>
                                        <asp:TextBox ID="txtCNPJCPF" onkeyup="formataCNPJ(this,event);" MaxLength="18" CssClass="form-control" runat="server" Required></asp:TextBox>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">E-mail</label>
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Required></asp:TextBox>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">Whatsapp</label>
                                        <asp:TextBox ID="txtTelefone" onkeyup="formataTelefone(this,event);" MaxLength="15" CssClass="form-control" runat="server" Required></asp:TextBox>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">Senha</label>
                                        <asp:TextBox ID="txtSenha" TextMode="Password" CssClass="form-control" runat="server" Required></asp:TextBox>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">Escolha seu plano</label>
                                        <asp:DropDownList runat="server" ID="ddlPlano" CssClass="form-control">
                                            <asp:ListItem Text="Fornecedor"></asp:ListItem>
                                            <asp:ListItem Text="Parceiro"></asp:ListItem>
                                            <asp:ListItem Text="Consumidor"></asp:ListItem>
                                        </asp:DropDownList>                                       
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">Ao continuar, você concorda com nosso <a href="#" target="_blank">Aviso de Privacidade</a>.</label>
                                    </div>
                                    <div class="mb-3 w-100" align="right">
                                        <a href="login.aspx" class="text-center">
                                            <span>Já tem conta?</span></a>
                                    </div>
                                    <div class="mb-3 w-100" align="left">
                                        <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                                    </div>
                                    <div class="w-100 mb-0">
                                        <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn btn-outline-primary btn-icon btn-icon-start" OnClick="btnCadastrar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlPagamento" Visible="false" runat="server">
                    <div class="col-xl-6">
                        <div class="mb-5">
                            <div class="card">
                                <div class="card-body">
                                    <div class="page-title-container">
                                        <div class="row g-0">
                                            <!-- Title Start -->
                                            <div class="col-auto mb-3 mb-md-0 me-auto">
                                                <div class="w-auto sw-md-30">
                                                    <img src="img/logo/w7paylogo.png" alt="Logo" width="100">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Title End -->

                                    <h1 class="">Checkout de Pagamento</h1>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">CNPJ/CPF</label>
                                        <asp:Label ID="lblCNPJ"  runat="server"></asp:Label>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">E-mail/Whatsapp</label>
                                        <asp:Label ID="lblEmail" runat="server" ></asp:Label> - <asp:Label ID="lblWhatsapp" runat="server"></asp:Label>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">Plano Selecionado</label>
                                        <asp:Label ID="lblPlano" runat="server"></asp:Label>
                                    </div>
                                    <!-- Title and Top Buttons End -->
                                    <h2 class="small-title">Faça a leitura do QRCode</h2>
                                    <div class="mb-3 w-100">
                                        <asp:Image ID="imgQRCode" ImageUrl="#" runat="server"></asp:Image>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <label class="form-label">Chave Copia e Cola</label>
                                        <asp:TextBox ID="lblChaveCopiaCola" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblTxid" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <div class="mb-3 w-100">
                                        <asp:Label ID="lblMensagemPix" Font-Size="2em" runat="server"></asp:Label>
                                    </div>
                                    <div class="w-100 mb-0">
                                        <asp:Button ID="btnLogin" runat="server" Text="Acessar o sistema" CssClass="btn btn-outline-primary btn-icon btn-icon-start" Visible="false" OnClick="btnLogin_Click" />
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
