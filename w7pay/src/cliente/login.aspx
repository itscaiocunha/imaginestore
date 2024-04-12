﻿<%@ Page Title="" Language="C#" MasterPageFile="~/src/cliente/master.master" AutoEventWireup="true" Async="true" CodeBehind="login.aspx.cs" Inherits="w7pay.src.cliente.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <!-- page-banner-area-start -->
    <div class="page-banner-area page-banner-height-2" data-background="assets/img/banner/page-banner-4.png">
        <div class="container">
            <div class="row">
                <div class="col-xl-12">
                    <div class="page-banner-content text-center">
                        <h4 class="breadcrumb-title">My account</h4>
                        <div class="breadcrumb-two">
                            <nav>
                                <nav class="breadcrumb-trail breadcrumbs">
                                    <ul class="breadcrumb-menu">
                                        <li class="breadcrumb-trail">
                                            <a href="dashboard.aspx"><span>Página Inicial</span></a>
                                        </li>
                                        <li class="trail-item">
                                            <span>Minha Conta</span>
                                        </li>
                                    </ul>
                                </nav>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- page-banner-area-end -->

    <!-- account-area-start -->
    <div class="account-area mt-70 mb-70">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="basic-login mb-50">
                        <h5>Login</h5>

                        <label for="name">E-mail de acesso  <span>*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <label for="pass">Senha <span>*</span></label>
                        <asp:TextBox ID="txtSenha" TextMode="Password" runat="server"></asp:TextBox>
                        <div class="login-action mb-10 fix">
                            <%--<span class="log-rem f-left">
                                    <input id="remember" type="checkbox">
                                    <label for="remember">Remember me</label>
                                 </span>--%>
                            <span class="forgot-login f-right">
                                <a href="#">Esqueceu a senha?</a>
                            </span>
                        </div>
                        <asp:LinkButton ID="lkbAcessar" CssClass="tp-in-btn w-100" runat="server" OnClick="lkbAcessar_Click">Acessar</asp:LinkButton>

                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="basic-login">
                        <h5>Cadastrar
                        </h5>
                        <div class="mb-3 w-100">
                            <label class="form-label">CPF</label>
                            <asp:TextBox ID="txtCNPJCPF" onkeyup="formataCNPJ(this,event);" MaxLength="18" CssClass="form-control" runat="server" Required></asp:TextBox>
                        </div>
                        <div class="mb-3 w-100">
                            <label class="form-label">E-mail</label>
                            <asp:TextBox ID="txtEmailNovo" CssClass="form-control" runat="server" Required></asp:TextBox>
                        </div>
                        <div class="mb-3 w-100">
                            <label class="form-label">Whatsapp</label>
                            <asp:TextBox ID="txtTelefone" onkeyup="formataTelefone(this,event);" MaxLength="15" CssClass="form-control" runat="server" Required></asp:TextBox>
                        </div>
                        <div class="mb-3 w-100">
                            <label class="form-label">Senha</label>
                            <asp:TextBox ID="txtSenhaNovo" TextMode="Password" CssClass="form-control" runat="server" Required></asp:TextBox>
                        </div>
                        <div class="mb-3 w-100">
                            <label class="form-label">Ao continuar, você concorda com nosso <a href="#" target="_blank">Aviso de Privacidade</a>.</label>
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
    </div>
    <!-- account-area-end -->

    <!-- cta-area-start -->
    <section class="cta-area d-ldark-bg pt-55 pb-10">
        <div class="container">
            <div class="row">
                <div class="col-lg-4 col-md-6">
                    <div class="cta-item cta-item-d mb-30">
                        <h5 class="cta-title">Follow Us</h5>
                        <p>We make consolidating, marketing and tracking your social media website easy.</p>
                        <div class="cta-social">
                            <div class="social-icon">
                                <a href="#" class="facebook"><i class="fab fa-facebook-f"></i></a>
                                <a href="#" class="twitter"><i class="fab fa-twitter"></i></a>
                                <a href="#" class="youtube"><i class="fab fa-youtube"></i></a>
                                <a href="#" class="linkedin"><i class="fab fa-linkedin-in"></i></a>
                                <a href="#" class="rss"><i class="fas fa-rss"></i></a>
                                <a href="#" class="dribbble"><i class="fab fa-dribbble"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="cta-item mb-30">
                        <h5 class="cta-title">Sign Up To Newsletter</h5>
                        <p>Join 60.000+ subscribers and get a new discount coupon  on every Saturday.</p>
                        <div class="subscribe__form">

                            <input type="email" placeholder="Enter your email here...">
                            <button>subscribe</button>

                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="cta-item mb-30">
                        <h5 class="cta-title">Download App</h5>
                        <p>DukaMarket App is now available on App Store & Google Play. Get it now.</p>
                        <div class="cta-apps">
                            <div class="apps-store">
                                <a href="#">
                                    <img src="assets/img/brand/app_ios.png" alt=""></a>
                                <a href="#">
                                    <img src="assets/img/brand/app_android.png" alt=""></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>
    <!-- cta-area-end -->
</asp:Content>
