<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="dishonest.aspx.cs" Inherits="w7pay.src.dishonest" %>

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
                    <h1 class="mb-0 pb-0 display-6" id="title">Dishonest</h1>
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
    <!-- Search End -->
    <!-- Order List Start -->
          <div class="row">
            <div class="col-12 mb-5">              
              <asp:GridView ID="gdvDados" Width="100%" runat="server" CellPadding="4" EmptyDataText="Não há dados para visualizar" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataSourceID="sdsDados">
                  <AlternatingRowStyle />
                  <Columns>
                      <asp:BoundField DataField="REPOSITOR" HeaderText="REPOSITOR" Visible="false" SortExpression="REPOSITOR" />
                      <asp:BoundField DataField="DATA" HeaderText="Data" SortExpression="DATA" />
                      <asp:BoundField DataField="LOJA" HeaderText="Loja" SortExpression="LOJA" />
                      <asp:BoundField DataField="CÓDIGO EM BARRAS" HeaderText="CÓDIGO EM BARRAS" SortExpression="CÓDIGO EM BARRAS" />
                      <asp:BoundField DataField="QTD SISTEMA" HeaderText="QTD SISTEMA" SortExpression="QTD SISTEMA" />
                      <asp:BoundField DataField="QTD FÍSICA" HeaderText="QTD FÍSICA" SortExpression="QTD FÍSICA" />
                      <asp:BoundField DataField="DIVERGÊNCIA" HeaderText="DIVERGÊNCIA" SortExpression="DIVERGÊNCIA" />
                      <asp:BoundField DataField="Descrição produto" HeaderText="Descrição produto" SortExpression="Descrição produto" />                      
                      <asp:BoundField DataField="Preço de venda" HeaderText="Preço de venda" SortExpression="Preço de venda" />
                      <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                      <asp:BoundField DataField="Fornecedor" HeaderText="Fornecedor" SortExpression="Fornecedor" />
                      <asp:BoundField DataField="Custo unitário" HeaderText="Custo unitário" SortExpression="Custo unitário" />
                      <asp:BoundField DataField="Dishonest" HeaderText="Dishonest" SortExpression="Dishonest" />
                      <asp:BoundField DataField="Dishonest venda" HeaderText="Dishonest venda" SortExpression="Dishonest venda" />
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
         <asp:SqlDataSource ID="sdsDados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select * from base_dishonest where data > getdate() - 30">         
                </asp:SqlDataSource>
            </div>
          </div>
          <!-- Order List End -->
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
