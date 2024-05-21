<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="vendas_produto.aspx.cs" Inherits="w7pay.src.vendas_produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
    <script src="js/mascara.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Title and Top Buttons Start -->
            <div class="page-title-container">
                <div class="row">
                    <!-- Title Start -->
                    <div class="col-auto mb-3 mb-md-0 me-auto">
                        <div class="w-auto sw-md-30">
                            <a href="dashboard.aspx" class="muted-link pb-1 d-inline-block breadcrumb-back">
                                <i data-acorn-icon="chevron-left" data-acorn-size="13"></i>
                                <span class="text-small align-middle">Página Inicial</span>
                            </a>
                            <h1 class="mb-0 pb-0 display-6" id="title">Relatório de Vendas/Produto</h1>
                        </div>
                    </div>
                    <!-- Title End -->
                </div>
            </div>
            <!-- Title and Top Buttons End -->

            <!-- Controls Start -->
            <div class="row mb-2">
                <!-- Search Start -->
                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <div class="d-inline-block float-md-start me-1 mb-1 search-input-container w-100 shadow bg-foreground">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Filtrar"></asp:TextBox>
                        <span class="search-magnifier-icon">
                            <i data-acorn-icon="search"></i>
                        </span>
                        <span class="search-delete-icon d-none">
                            <i data-acorn-icon="close"></i>
                        </span>
                    </div>
                </div>

                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbFiltro" runat="server" CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
                        <i data-acorn-icon="send"></i>
                        Atualizar Dados
                    </asp:LinkButton>
                </div>
            </div>
            <asp:Label ID="lblErro" runat="server" Text=""></asp:Label>
            <!-- Order List Start -->
            <div class="row">
                <div class="row g-2">
                    <asp:Literal ID="ltrVendas" runat="server"></asp:Literal>
                </div>
            </div>
            <!-- Order List End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
