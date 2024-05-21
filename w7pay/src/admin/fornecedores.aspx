<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="fornecedores.aspx.cs" Inherits="w7pay.src.fornecedores" %>

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
                    <h1 class="mb-0 pb-0 display-6" id="title">Fornecedores</h1>
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

    <%--<div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
        <div class="">
            <asp:DropDownList ID="ddlFornecedores" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsFornecedores" DataTextField="name" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsFornecedores" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select * from fornecedores order by name"></asp:SqlDataSource>
        </div>
    </div>--%>

    <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
        <asp:LinkButton ID="lkbFiltro" runat="server" CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
           <i data-acorn-icon="send"></i>
           Atualizar Dados
        </asp:LinkButton>
    </div>
</div>
    <!-- Order List Start -->
          <div class="row">
            <div class="col-12 mb-5">              
              <asp:GridView ID="gdvDados" Width="100%" runat="server" CellPadding="4" EmptyDataText="Não há dados para visualizar" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataSourceID="sdsDados">
                  <AlternatingRowStyle />
                  <Columns>
                      <asp:BoundField DataField="Situação" HeaderText="Situação" SortExpression="Situação" />
                      <asp:BoundField DataField="Tags" HeaderText="Tipo" SortExpression="Tags" />
                      <asp:BoundField DataField="CNPJ / CPF" HeaderText="CNPJ / CPF" SortExpression="CNPJ / CPF" />
                      <asp:BoundField DataField="name" HeaderText="Nome na VMPay" SortExpression="name" />
                      <asp:BoundField DataField="Razão Social / Nome Completo" HeaderText="Razão Social / Nome Completo" SortExpression="Razão Social / Nome Completo" />
                      <asp:BoundField DataField="Nome Fantasia / Nome Abreviado" HeaderText="Nome Fantasia / Nome Abreviado" SortExpression="Nome Fantasia / Nome Abreviado" />
                      <asp:BoundField DataField="Telefone" HeaderText="Telefone" SortExpression="Telefone" />
                      <asp:BoundField DataField="Contato" HeaderText="Contato" SortExpression="Contato" />
                      <asp:BoundField DataField="E-mail" HeaderText="E-mail" Visible="false" SortExpression="E-mail" />                      
                      <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
                      <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                      <asp:BoundField DataField="Endereço" HeaderText="Endereço" Visible="false" SortExpression="Endereço" />
                      <asp:BoundField DataField="Bairro" HeaderText="Bairro" Visible="false" SortExpression="Bairro" />
                      <asp:BoundField DataField="CEP" HeaderText="CEP" Visible="false" SortExpression="CEP" />
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
         <asp:SqlDataSource ID="sdsDados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select *  from base_fornecedor_omie o
left join fornecedores f on f.name = o.[Razão Social / Nome Completo]
where f.name is not null
order by f.name">
                </asp:SqlDataSource>
            </div>
          </div>
          <!-- Order List End -->
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
