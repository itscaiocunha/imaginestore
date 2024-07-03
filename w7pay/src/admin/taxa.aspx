<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="taxa.aspx.cs" Inherits="w7pay.src.taxa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <script src="../js/mascara.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdfId" runat="server" />
            <!-- Title and Top Buttons Start -->
            <div class="page-title-container">
                <div class="row g-0">
                    <!-- Title Start -->
                    <div class="col-auto mb-3 mb-md-0 me-auto">
                        <div class="w-auto sw-md-30">
                            <a href="#" class="muted-link pb-1 d-inline-block breadcrumb-back">
                                <i data-acorn-icon="chevron-left" data-acorn-size="13"></i>
                                <span class="text-small align-middle">Administrador</span>
                            </a>
                            <h1 class="mb-0 pb-0 display-4" id="title">Taxas Imagine Store</h1>
                        </div>
                    </div>
                    <!-- Title End -->

                    <!-- Top Buttons Start -->
                    <div class="w-100 d-md-none"></div>
                    <div class="col-12 col-sm-6 col-md-auto d-flex align-items-end justify-content-end mb-2 mb-sm-0 order-sm-3">
                        <asp:LinkButton ID="lkbAdicionarPerfil" runat="server" CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="LinkButton1_Click">
                         <i data-acorn-icon="plus"></i> Novo Cadastro</asp:LinkButton>
                    </div>
                    <!-- Top Buttons End -->
                </div>
            </div>
            <!-- Title and Top Buttons End -->

            <!-- Controls Start -->
            <div class="row mb-2">
                <!-- Search Start -->
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Filtrar"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbFiltro" runat="server" CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
                        <i data-acorn-icon="search"></i> 
                        Filtrar Cliente
                    </asp:LinkButton>
                </div>
                <!-- Search End -->
            <!-- Discount List Start -->
            <div class="row">
                <div class="col-12 mb-5">
                    <asp:GridView ID="gdvDados" runat="server" Width="100%" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="Não há dados para visualizar" DataSourceID="sdsDados" OnRowCommand="gdvDados_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button data-bs-offset="0,3" data-bs-toggle="modal" data-bs-target="#pnlModal" ID="btnEditar" CssClass="btn btn-icon btn-icon-end btn-primary" CommandArgument='<%# Eval("idfornecedor") %>' CommandName="Editar" runat="server" Text="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="idfornecedor" Visible="false" HeaderText="idfornecedor" SortExpression="idfornecedor" />
                            <asp:BoundField DataField="name" HeaderText="Fornecedor" SortExpression="name" />
                            <asp:BoundField DataField="taxa" HeaderText="Taxa(%)" SortExpression="taxa" />
                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />                            
                            <asp:BoundField DataField="dia_pagamento" HeaderText="Dia de Pagamento" SortExpression="dia_pagamento" />                                                        
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle Height="4em" BackColor="White" ForeColor="#a59e9e" CssClass="fix-margin" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsDados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand=
                        "select t.idfornecedor as idfornecedor, t.taxa, t.[status], t.data_cadastro, t.dia_pagamento, f.id, f.name from split t
                        join fornecedores f on f.id = t.idfornecedor"></asp:SqlDataSource>
                </div>
            </div>



            <!-- Discount Add Modal Start -->
            <asp:Panel ID="pnlModal" runat="server" CssClass="modal-right" Visible="false">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Adicionar Taxa de Fornecedor</h5>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3">
                                <label class="form-label">Fornecedor</label>
                                <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsFornecedor" DataTextField="nomecompleto" DataValueField="id">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select name as nomecompleto, id from fornecedores order by name"></asp:SqlDataSource>
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Taxa de Comissão(%)</label>
                                <asp:TextBox ID="txtTaxa" onkeyup="formataContabil(this,event);" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Dia de Pagamento</label>
                                <asp:TextBox ID="txtDiaPagamento" onkeyup="formataInteiro(this,event);" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3 w-100">
                                <label class="form-label">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control shadow dropdown-menu-end">
                                    <asp:ListItem Text="Ativo" CssClass="dropdown-item"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" CssClass="dropdown-item"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal-footer border-0">
                            <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Button ID="btnSalvar" CssClass="btn btn-icon btn-icon-end btn-success" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                            <asp:LinkButton ID="lkbFechar" runat="server" CssClass="btn btn-danger btn-icon btn-icon-start" OnClick="lkbFechar_Click">
                                <i data-acorn-icon="close"></i>
                                Fechar 
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- Discount Add Modal End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
