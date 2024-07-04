<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="w7pay.src.dashboard2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <script src="../js/mascara.js"></script>
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
    <!-- Title and Top Buttons Start -->
    <div class="page-title-container">
        <div class="row">
            <!-- Title Start -->
            <div class="col-12 col-md-10">
                <a class="muted-link pb-2 d-inline-block hidden" href="#">
                    <span class="align-middle lh-1 text-small">&nbsp;</span>
                </a>
                <h1 class="mb-0 pb-0 display-4" id="title">
                    <asp:Label ID="lblMensagemBoasVindas" runat="server"></asp:Label>
                </h1>
            </div>

            <%-- Botão de Pagamento --%>
                <%--<div class="col-auto d-flex align-items-end justify-content-end">
                    <a href="pix.aspx" class="btn btn-primary btn-icon btn-icon-start" target="_blank">
                    <i data-acorn-icon="plus"></i>
                    <span>Novo Pagamento</span></a>
                </div>
            </div>--%>
        </div>
    </div>


    <%-- Filtro de Fornecedores e Data --%>
    <div class="row">
        <div class="col-12">
            <div class="d-flex">
                <h2 class="small-title">Estatísticas em tempo real</h2>
            </div>
            <div class="row mb-2">
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <asp:DropDownList ID="ddlFornecedores" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsFornecedores" DataTextField="name" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsFornecedores" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select * from fornecedores order by name"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <asp:TextBox ID="txtDataInicio" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <asp:TextBox ID="txtDataFim" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-3 col-lg-2 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbFiltro" runat="server"
                        CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
                        <i data-acorn-icon="search"></i> Atualizar
                    </asp:LinkButton>
                </div>
                <div class="col-sm-12 col-md-4 col-lg-2 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbLimpar" runat="server"
                        CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbLimpar_Click">
                        <i data-acorn-icon="close"></i> Limpar
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
                

    <%-- Cards --%>
    <div class="mb-5">
        <div class="row g-2">
            <div class="col-6 col-md-4 col-lg-2">
                <div class="card h-100 hover-scale-up cursor-pointer">
                    <div class="card-body d-flex flex-column align-items-center">
                        <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                            <i data-acorn-icon="cart" class="text-primary"></i>
                        </div>
                        <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">
                            VENDAS TOTAIS
                        </div>
                        <div class="text-primary cta-4">
                            <asp:Label ID="lblTotalVendasRegistradas" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-6 col-md-4 col-lg-2">
                <div class="card h-100 hover-scale-up cursor-pointer">
                    <div class="card-body d-flex flex-column align-items-center">
                        <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                            <i data-acorn-icon="dollar" class="text-primary"></i>
                        </div>
                        <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">
                            VENDAS TOTAIS
                        </div>
                        <div class="text-primary cta-4">
                            <asp:Label ID="lblTotalVendasPagas" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-6 col-md-4 col-lg-2">
                <div class="card h-100 hover-scale-up cursor-pointer">
                    <div class="card-body d-flex flex-column align-items-center">
                        <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                            <i data-acorn-icon="tag" class="text-primary"></i>
                        </div>
                        <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">
                            LOJAS COM VENDAS
                        </div>
                        <div class="text-primary cta-4">
                            <asp:Label ID="lblTotalNaoPagas" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-6 col-md-4 col-lg-2">
                <div class="card h-100 hover-scale-up cursor-pointer">
                    <div class="card-body d-flex flex-column align-items-center">
                        <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                            <i data-acorn-icon="arrow-top-left" class="text-primary"></i>
                        </div>
                        <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">
                            Nº DE COMPRAS/CLIENTE
                        </div>
                        <div class="text-primary cta-4">
                            <asp:Label ID="lblTotalMensagens" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    
    <%-- Gráficos --%>
    <div class="row">
        <div class="col-xl-3 mb-5">
            <h2 class="small-title">Quantidade de Vendas por Loja</h2>
            <asp:DropDownList ID="ddlTopQtdeVendas" runat="server" CssClass="form-control shadow dropdown-menu-end" AutoPostBack="true" OnSelectedIndexChanged="ddlTopQtdeVendas_SelectedIndexChanged">
                <asp:ListItem Text="10 melhores lojas" Value="top 10"></asp:ListItem>
                <asp:ListItem Text="30 melhores lojas" Value="top 30"></asp:ListItem>
                <asp:ListItem Text="Todas as Lojas" Value=""></asp:ListItem>
            </asp:DropDownList>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <asp:Chart ID="Chart1" runat="server" DataSourceID="sdsDados" Height="400px" Palette="EarthTones">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Bar" PostBackValue="#VALX" Palette="BrightPastel" YValuesPerPoint="4" XValueMember="nomecliente" YValueMembers="qtde" IsValueShownAsLabel="true" BackImageAlignment="BottomLeft"> </asp:Series>
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
                    <asp:SqlDataSource ID="sdsDados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="
                        select top 10 count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente, v.client_id from vendas v (nolock) 
                        where v.manufacturer_id = @id and occurred_at > getdate() - 7
                        group by v.client_id
                        having count(quantity) > 0
                        order by qtde desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="idempresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>

        <div class="col-xl-3 mb-5">
            <h2 class="small-title">Faturamento/Loja</h2>
            <asp:DropDownList ID="ddlTopFaturamentoLoja" runat="server" CssClass="form-control shadow dropdown-menu-end" AutoPostBack="true" OnSelectedIndexChanged="ddlTopFaturamentoLoja_SelectedIndexChanged">
                <asp:ListItem Text="10 melhores lojas" Value="top 10"></asp:ListItem>
                <asp:ListItem Text="30 melhores lojas" Value="top 30"></asp:ListItem>
                <asp:ListItem Text="Todas as Lojas" Value=""></asp:ListItem>
            </asp:DropDownList>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSource1" Height="400px" Palette="EarthTones">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Bar" PostBackValue="#VALX" LabelFormat="{0:c2}" YValuesPerPoint="4" XValueMember="nomecliente" YValueMembers="fatura" IsValueShownAsLabel="true"></asp:Series>
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
                        select top 10 count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)
                        where v.manufacturer_id = @id and occurred_at > getdate() - 7
                        group by v.client_id
                        having count(quantity) > 0
                        order by fatura desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="idempresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>

        <div class="col-xl-3 mb-5">
            <h2 class="small-title">Quantidade de Venda/Produto</h2>
            <asp:DropDownList ID="ddlTopQtdeVendaProduto" runat="server" CssClass="form-control shadow dropdown-menu-end" AutoPostBack="true" OnSelectedIndexChanged="ddlTopQtdeVendaProduto_SelectedIndexChanged">
                <asp:ListItem Text="10 melhores produtos" Value="top 10"></asp:ListItem>
                <asp:ListItem Text="30 melhores produtos" Value="top 30"></asp:ListItem>
            </asp:DropDownList>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <asp:Chart ID="Chart3" runat="server" DataSourceID="SqlDataSource2" Height="400px" Palette="EarthTones">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="nomeproduto" YValueMembers="qtde" IsValueShownAsLabel="true"></asp:Series>
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
                        select top 10 count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)
                        where v.manufacturer_id = @id and occurred_at > getdate() - 7
                        group by v.good_id
                        having count(quantity) > 0
                        order by qtde desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="idempresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>

        <div class="col-xl-3 mb-5">
            <h2 class="small-title">Faturamento de Venda/Produto</h2>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control shadow dropdown-menu-end" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                <asp:ListItem Text="10 melhores produtos" Value="top 10"></asp:ListItem>
                <asp:ListItem Text="30 melhores produtos" Value="top 30"></asp:ListItem>
            </asp:DropDownList>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <asp:Chart ID="Chart5" runat="server" DataSourceID="SqlDataSource4" Height="400px" Palette="EarthTones">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="nomeproduto" LabelFormat="{0:c2}" YValueMembers="fatura" IsValueShownAsLabel="true"></asp:Series>
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
                        select top 10 count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)
                        where v.manufacturer_id = @id and occurred_at > getdate() - 7
                        group by v.good_id
                        having count(quantity) > 0
                        order by fatura desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="idempresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xl-10 mb-5">
            <br /><br />
            <h2 class="small-title">Quantidade/Faturamento por mês</h2>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <asp:Chart ID="Chart4" runat="server" Width="970px" DataSourceID="SqlDataSource3" Palette="EarthTones">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Spline" LabelFormat="R{0:c2}" YValuesPerPoint="4" XValueMember="mesano" YValueMembers="fatura" IsValueShownAsLabel="true"></asp:Series>
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
                        select count(quantity) as qtde, cast(sum(value) as decimal (10,2)) as fatura, convert(varchar,month(v.occurred_at))+'/'+  convert(varchar,year(v.occurred_at)) as mesano from vendas v (nolock)
                        where v.manufacturer_id = @id
                        group by month(v.occurred_at), year(v.occurred_at)
                        having count(quantity) > 0
                        order by year(v.occurred_at), month(v.occurred_at)">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="idempresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>



    <%-- Botões de Rotina --%>
    <br />
    <div class="row gx-2">
        <div class="col-sm-12 col-md-4 col-lg-2 col-xxl-2 mb-1">
            <asp:LinkButton ID="rotinaVendas" runat="server"
                CssClass="btn btn-outline-primary btn-icon btn-icon-start w-100 w-md-auto"
                OnClick="rodarVendas">
                <i data-acorn-icon="sales"></i> Vendas
            </asp:LinkButton>
        </div>

        <div class="col-sm-12 col-md-4 col-lg-2 col-xxl-2 mb-1">
            <asp:LinkButton ID="rotinaFornecedores" runat="server"
                CssClass="btn btn-outline-primary btn-icon btn-icon-start w-100 w-md-auto"
                OnClick="rodarFornecedores">
                <i data-acorn-icon="supplier"></i> Fornecedores
            </asp:LinkButton>
        </div>

        <div class="col-sm-12 col-md-4 col-lg-2 col-xxl-2 mb-1">
            <asp:LinkButton ID="rotinaProdutos" runat="server"
                CssClass="btn btn-outline-primary btn-icon btn-icon-start w-100 w-md-auto"
                OnClick="rodarProdutos">
                <i data-acorn-icon="product"></i> Produtos
            </asp:LinkButton>
        </div>
    </div>
    <style>
        .btn-icon {
            margin-right: 10px; /* Adjust the margin as needed */
        }
        .row {
            justify-content: center; /* Center the buttons horizontally */
        }
    </style>




</asp:Content>
