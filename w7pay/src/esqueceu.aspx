<%@ Page Title="" Language="C#" MasterPageFile="~/src/geral.master" AutoEventWireup="true" CodeBehind="esqueceu.aspx.cs" Inherits="w7pay.src.esqueceu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
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
                                        <img src="img/logo/w7paylogo.png" alt="Logo" width="100">
                                    </div>
                                </div>
                                <!-- Title End -->

                                <!-- Top Buttons Start -->
                                <div class="w-100 d-md-none"></div>

                                <!-- Top Buttons End -->
                            </div>
                        </div>
                        <!-- Title and Top Buttons End -->
                        <h2 class="small-title">Esqueceu a senha</h2>
                        <div class="mb-3 w-100">
                            <label class="form-label">E-mail</label>
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3 w-100">
                            <label class="form-label">Últimos 4 digitos do CNPJ</label>
                            <asp:TextBox ID="txtCodigo" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="w-100 mb-0" align="left">
                            <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="mb-3 w-100" align="right">
                            <a href="login.aspx" class="text-center">
                                <span>Ir para Login</span></a>
                            <a href="cadastro.aspx" class="text-center">
                                <span>Ainda não tem conta?</span></a>
                        </div>
                        <div class="w-100 mb-0">
                            <asp:Button ID="Button1" runat="server" Text="Acessar" CssClass="btn btn-outline-primary btn-icon btn-icon-start" OnClick="Button1_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
