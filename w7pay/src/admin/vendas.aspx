<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="vendas.aspx.cs" Inherits="w7pay.src.vendas2" %>

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
                    <h1 class="mb-0 pb-0 display-6" id="title">Relatório de Vendas</h1>
                </div>
            </div>
            <!-- Title End -->
        </div>
    </div>
    <!-- Title and Top Buttons End -->

    <!-- Controls Start -->
    <div class="row mb-2">
    <!-- Search Start -->

        <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
            <div class="">
                <label class="form-label">Data Início</label>
                <asp:TextBox ID="txtDataInicio" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
            <div class="">
                <label class="form-label">Data Final</label>
                <asp:TextBox ID="txtDataFim" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-3 col-lg-2 col-xxl-2 mb-1">
            <asp:LinkButton ID="lkbFiltro" runat="server"
                CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
                <i data-acorn-icon="search"></i> Atualizar
            </asp:LinkButton>
        </div>

    <asp:Label ID="lblteste" runat="server"></asp:Label>  
</div>
            <asp:Label ID="lblErro" runat="server" Text=""></asp:Label>
    <!-- Order List Start -->
          <div class="row">
            <div class="col-12 mb-5">              
              <asp:GridView ID="gdvDados" Width="100%" runat="server" CellPadding="4" EmptyDataText="Não há dados para visualizar" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataSourceID="sdsDados">
                  <AlternatingRowStyle />
                  <Columns>
                      <asp:BoundField DataField="name" HeaderText="Fornecedor" SortExpression="name" />
                      <asp:BoundField DataField="name1" HeaderText="Local" SortExpression="name1" />
                      <asp:BoundField DataField="name2" HeaderText="Cliente" SortExpression="name2" />
                      <asp:BoundField DataField="asset_number" HeaderText="Máquina" SortExpression="asset_number" />
                      <asp:BoundField DataField="descricao" HeaderText="Categoria" SortExpression="descricao" />
                      <asp:BoundField DataField="name3" HeaderText="Produto" SortExpression="name3" />
                      <asp:BoundField DataField="occurred_at" HeaderText="Data da Venda" SortExpression="occurred_at" />
                      <asp:BoundField DataField="quantity" HeaderText="Quant." SortExpression="quantity" />                      
                      <asp:BoundField DataField="value" HeaderText="Valor" DataFormatString="{0:c2}" SortExpression="value" />
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
             "select f.name, l.name, c.name, m.asset_number, ct.descricao, p.name, coil, quantity, value, occurred_at from vendas v (nolock)
                join clientes c on c.id = v.client_id
                join locais l on l.id = v.location_id
                join maquinas m on m.id = v.machine_id
                join produtos p on p.id = v.good_id
                join fornecedores f on f.id = p.manufacturer_id
                join categorias ct on ct.id = p.category_id
                where occurred_at > getDate() - 7
                order by f.name, l.name, c.name, m.asset_number, ct.descricao, p.name">
                </asp:SqlDataSource>
            </div>
          </div>
          <!-- Order List End -->
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
