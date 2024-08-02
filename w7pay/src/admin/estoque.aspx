<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="estoque.aspx.cs" Inherits="w7pay.src.estoque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
     <script src="js/mascara.js"></script>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title-container">
                <div class="row">
                    <%-- Título da Página --%>
                    <div class="col-auto mb-3 mb-md-0 me-auto">
                        <div class="w-auto sw-md-30">
                            <a href="dashboard.aspx" class="muted-link pb-1 d-inline-block breadcrumb-back">
                                <i data-acorn-icon="chevron-left" data-acorn-size="13"></i>
                                <span class="text-small align-middle">Página Inicial</span>
                            </a>
                            <h1 class="mb-0 pb-0 display-6" id="title">Estoque de Produtos</h1>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Filtros --%>
            <div class="row mb-2">
                <asp:Label ID="lblErros" runat="server"></asp:Label>
                <%-- Filtro de Fornecedores --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlForncedores" class="form-label">Fornecedor:</label>
                        <asp:DropDownList ID="ddlFornecedores" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsFornecedores" DataTextField="name" DataValueField="id" OnDataBound="ddlTotal_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsFornecedores" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand=
                            "select * from fornecedores order by name"></asp:SqlDataSource>
                    </div>
                </div>

                <%-- Filtro de Categoria --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlCategoria" class="form-label">Categoria:</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsCategoria" DataTextField="descricao" DataValueField="id" OnDataBound="ddlTotal_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand=
                            "select id, descricao from categorias order by descricao"></asp:SqlDataSource>
                    </div>
                </div>

                <%-- Filtro de Produto --%>
                <%--<div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlProduto" class="form-label">Produto:</label>
                        <asp:DropDownList ID="ddlProduto" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsProduto" DataTextField="name" DataValueField="id" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsProduto" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select id, name from produtos order by name"></asp:SqlDataSource>
                    </div>
                </div>--%>

                <%-- Filtro de Lojas --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlLojas" class="form-label">Loja:</label>
                        <asp:DropDownList ID="ddlLojas" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsLojas" DataTextField="name" DataValueField="id" OnDataBound="ddlTotal_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsLojas" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select * from clientes"></asp:SqlDataSource>
                    </div>
                </div>
            </div>

               <%-- Botão de Filtrar --%>
            <div class="row mb-2">
                <div class="col-sm-12 col-md-3 col-lg-2 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbFiltro" runat="server"
                        CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
                        <i data-acorn-icon="search"></i> Atualizar
                    </asp:LinkButton>
                </div>

                <%-- Botão de Limpar Filtro --%>
                <div class="col-sm-12 col-md-4 col-lg-2 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbLimpar" runat="server"
                        CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbLimpar_Click">
                        <i data-acorn-icon="close"></i> Limpar
                    </asp:LinkButton>
                </div>
            </div>
            
            <%-- Cards --%>
            <div class="mb-5">
                <div class="row g-2">

                    <%-- Estoque Geral--%>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="cart" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">ESTOQUE LOJAS</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblEstoque" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- Estoque CD --%>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="dollar" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">ESTQOUE CD</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblEstoqueCD" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- Lojas com Estoque --%>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="tag" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">QTDE LOJAS C/ ESTOQUE</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblLojas" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- Estoque Total --%>
                    <%--<div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="arrow-top-left" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">ESTOQUE LOJA+CD</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
            
            <%-- Gráficos --%>
            <div class="row">
                <%-- Estoque CD por Produto --%>
                <%--<div class="col-xl-3 mb-5">
                    <h2 class="small-title">ESTOQUE DE PRODUTO EM CD</h2>           
                    <div class="card h-xl-100-card">
                        <div class="card-body h-100">
                            <div class="h-100">
                                <div class="col-12 mb-5" style="overflow-y:scroll; height:400px">
                                    <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1" Height="400px" Palette="EarthTones">
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Bar" PostBackValue="#VALX" Palette="BrightPastel" YValuesPerPoint="4" XValueMember="produto" YValueMembers="saldocd" IsValueShownAsLabel="true"> </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY IntervalType="Number" LineDashStyle="NotSet">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="false" />
                                                </AxisY>
                                                <AxisX IntervalType="Days" LineDashStyle="NotSet" Interval="1">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="true" />
                                                </AxisX>
                                                <AxisX2>
                                                    <MajorTickMark Enabled="False" />
                                                </AxisX2>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="
                                        select top(10) max(e.name) as produto, isnull(max(e.sald), '0') as saldocd from estoque e (nolock) 
                                        join fornecedores f on f.id = e.manufacturer_id
                                        join categorias ct on ct.id = e.category_id
                                        join produtos p on p.id = e.id
                                        where e.sald > 0
                                        group by e.id
                                        order by saldocd">
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>

                <%-- Estoque Loja por Produto --%>
                <div class="col-xl-3 mb-5">
                    <h2 class="small-title">ESTOQUE DE PRODUTO EM LOJA</h2>          
                    <div class="card h-xl-100-card">
                        <div class="card-body h-100">
                            <div class="h-100">
                                <div class="col-12 mb-5" style="overflow-y:scroll; height:400px">
                                    <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSource2" Height="400px" Palette="EarthTones">
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Bar" PostBackValue="#VALX" YValuesPerPoint="4" XValueMember="produto" YValueMembers="saldocd" IsValueShownAsLabel="true"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY IntervalType="Number" LineDashStyle="NotSet">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="false" />
                                                </AxisY>
                                                <AxisX IntervalType="Days" LineDashStyle="NotSet" Interval="1">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="true" />
                                                </AxisX>
                                                <AxisX2>
                                                    <MajorTickMark Enabled="False" />
                                                </AxisX2>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="
                                        select top(10) max(e.name) as produto, isnull(max(e.sald), '0') as saldocd from estoque1 e (nolock)
                                        join fornecedores f on f.id = e.manufacturer_id
                                        join categorias ct on ct.id = e.category_id
                                        join produtos p on p.id = e.id
                                        where e.sald > 0
                                        group by e.id
                                        order by saldocd">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlFornecedores" Name="fornecedor" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- Estoque por Loja --%>
                <div class="col-xl-3 mb-5">
                    <h2 class="small-title">QUANTIDADE TOTAL DE ESTOQUE POR LOJA</h2>
                    <div class="card h-xl-100-card">
                        <div class="card-body h-100">
                            <div class="h-100">
                                <div class="col-12 mb-5" style="overflow-y:scroll; height:400px">
                                    <asp:Chart ID="Chart3" runat="server" DataSourceID="SqlDataSource3" Height="400px" Palette="EarthTones">
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="client" YValueMembers="saldo" IsValueShownAsLabel="true"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY IntervalType="Number" LineDashStyle="NotSet">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="false" />
                                                </AxisY>
                                                <AxisX IntervalType="Days" LineDashStyle="NotSet" Interval="1">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="true" />
                                                </AxisX>
                                                <AxisX2>
                                                    <MajorTickMark Enabled="False" />
                                                </AxisX2>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="
                                        select top(10) max(e.name_client) as client, isnull(max(e.sald), '0') as saldo from estoque1 e (nolock)
                                        join fornecedores f on f.id = e.manufacturer_id
                                        join categorias ct on ct.id = e.category_id
                                        join produtos p on p.id = e.id
                                        where e.sald > 0
                                        group by e.idclient
                                        order by saldo">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlFornecedores" Name="fornecedor" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- Total Itens por Categoria --%>
                <div class="col-xl-3 mb-5">
                    <h2 class="small-title">ESTOQUE POR CATEGORIA</h2>
                    <div class="card h-xl-100-card">
                        <div class="card-body h-100">
                            <div class="h-100">
                                <div class="col-12 mb-5" style="overflow-y:scroll; height:400px">
                                    <asp:Chart ID="Chart4" runat="server" DataSourceID="SqlDataSource4" Height="400px" Palette="EarthTones">
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="client" YValueMembers="saldo" IsValueShownAsLabel="true"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY IntervalType="Number" LineDashStyle="NotSet">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="false" />
                                                </AxisY>
                                                <AxisX IntervalType="Days" LineDashStyle="NotSet" Interval="1">
                                                    <MajorGrid Enabled="False" />
                                                    <MajorTickMark Enabled="False" />
                                                    <LabelStyle Enabled="true" />
                                                </AxisX>
                                                <AxisX2>
                                                    <MajorTickMark Enabled="False" />
                                                </AxisX2>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="
                                        select top(10) max(c.descricao) as client, isnull(max(e.sald), '0') as saldo from estoque1 e (nolock)
                                        join categorias c on c.id = e.category_id
                                        join categorias ct on ct.id = e.category_id
                                        join produtos p on p.id = e.id
                                        where e.sald > 0
                                        group by e.idclient
                                        order by saldo">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlFornecedores" Name="fornecedor" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Grid Produtos -->
            <div class="row">
                <div class="col-12 mb-5">
                    <asp:Label ID="lblDados" runat="server"></asp:Label>
                    <asp:GridView ID="gdvDados" Width="100%" runat="server" CellPadding="4" EmptyDataText="Não há dados para visualizar" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataSourceID="sdsDados">
                      <AlternatingRowStyle />
                      <Columns>
                          <asp:BoundField DataField="id" HeaderText="#Cod" SortExpression="id" />
                          <asp:BoundField DataField="fornecedor" HeaderText="Fornecedor" SortExpression="fornecedor" />
                          <asp:BoundField DataField="descricao" HeaderText="Categoria" SortExpression="descricao" />                      
                          <asp:BoundField DataField="produto" HeaderText="Produto" SortExpression="produto" />                     
                          <asp:BoundField DataField="sald" HeaderText="Saldo" SortExpression="sald" />
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
                         "SELECT e.id, MAX(f.name) AS fornecedor, MAX(ct.descricao) AS descricao, MAX(e.name) AS produto, MAX(p.image) AS imagem, MAX(e.upc_code) AS upc_code, MAX(e.sald) AS sald 
                         FROM estoque1 e (nolock) 
                         JOIN fornecedores f ON f.id = e.manufacturer_id JOIN categorias ct ON ct.id = e.category_id 
                         JOIN produtos p ON p.id = e.id
                         GROUP BY e.id 
                         ORDER BY MAX(f.name), MAX(ct.descricao), MAX(e.name);">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlFornecedores" Name="fornecedor" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
              </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
