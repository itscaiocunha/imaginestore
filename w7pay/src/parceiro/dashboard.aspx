<%@ Page Title="" Language="C#" MasterPageFile="~/src/parceiro/masterparceiro.master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="w7pay.src.parceiro.dashboard2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
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
            <%--<div class="col-auto d-flex align-items-end justify-content-end">
                <a href="pix.aspx" class="btn btn-primary btn-icon btn-icon-start" target="_blank">
                  <i data-acorn-icon="plus"></i>
                  <span>Novo Pagamento</span></a>
              </div>
              <!-- Title End -->
            </div>--%>
        </div>
        <!-- Title and Top Buttons End -->
    </div>
    <!-- Stats Start -->
    <div class="row">
        <div class="col-12">
            <div class="d-flex">
                <h2 class="small-title">Estatísticas em tempo real</h2>
            </div>
            <div class="mb-5">
                <div class="row g-2">
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="cart" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">VENDAS TOTAIS</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotalVendasRegistradas" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="dollar" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">VENDAS TOTAIS</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotalVendasPagas" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="tag" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">PRODUTOS</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotalNaoPagas" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="dollar" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">VENDAS 7 DIAS</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotalVendas7dias" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="dollar" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">VENDAS 30 DIAS</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotalVendas30dias" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6 col-md-4 col-lg-2">
                        <div class="card h-100 hover-scale-up cursor-pointer">
                            <div class="card-body d-flex flex-column align-items-center">
                                <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                    <i data-acorn-icon="arrow-top-left" class="text-primary"></i>
                                </div>
                                <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">DISHONEST</div>
                                <div class="text-primary cta-4">
                                    <asp:Label ID="lblTotalMensagens" runat="server"></asp:Label></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Stats End -->

            <div class="row">
        <!-- Recent Orders Start -->
        <div class="col-xl-4 mb-5">
            <h2 class="small-title">Quantidade de Vendas por Loja</h2>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <div class="h-100">
                        <div class="row">
                            <div class="col-12 mb-5" style="overflow: auto">
                                <asp:Chart ID="Chart1" runat="server" DataSourceID="sdsDados" Height="1000px" Palette="EarthTones">
                                    <Series>
                                        <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="nomecliente" YValueMembers="qtde" IsValueShownAsLabel="true"></asp:Series>
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
             select count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(c.name) as nomecliente from vendas v (nolock)
                join clientes c on c.id = v.client_id
                join locais l on l.id = v.location_id
                join maquinas m on m.id = v.machine_id
                join produtos p on p.id = v.good_id
                join fornecedores f on f.id = p.manufacturer_id
                join categorias ct on ct.id = p.category_id
                join estoque e on e.upc_code = v.upc_code
                where p.manufacturer_id = @id and ((e.name_client = 'CD' and v.occurred_at >= dateadd(day, -30, getdate())) or e.name_client != 'CD')
                group by c.id
                having count(quantity) &gt; 0
                order by qtde">
             <SelectParameters>
                 <asp:SessionParameter Name="id" SessionField="idempresa" />
             </SelectParameters>
                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Recent Orders End -->

        <!-- Performance Start -->
        <div class="col-xl-4 mb-5">
            <div class="d-flex">
                <h2 class="small-title">Faturamentopor por Loja</h2>
            </div>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <div class="h-100">
                        <div class="row">
                            <div class="col-12 mb-5" style="overflow: auto">
                                    <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSource1" Height="1000px" Palette="EarthTones">
    <Series>
        <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="nomecliente" YValueMembers="fatura" IsValueShownAsLabel="true"></asp:Series>
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
             select count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(c.name) as nomecliente from vendas v (nolock)
                join clientes c on c.id = v.client_id
                join locais l on l.id = v.location_id
                join maquinas m on m.id = v.machine_id
                join produtos p on p.id = v.good_id
                join fornecedores f on f.id = p.manufacturer_id
                join categorias ct on ct.id = p.category_id
                join estoque e on e.upc_code = v.upc_code
                             where p.manufacturer_id = @id and ((e.name_client = 'CD' and v.occurred_at >= dateadd(day, -30, getdate())) or e.name_client != 'CD')
                group by c.id
                having count(quantity) &gt; 0
                order by qtde  "> 
              <SelectParameters>
     <asp:SessionParameter Name="id" SessionField="idempresa" />
 </SelectParameters>
                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Performance Start -->
        <div class="col-xl-4 mb-5">
            <div class="d-flex">
                <h2 class="small-title">Quantidade de Venda por Produto</h2>
            </div>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <div class="h-100">
                        <div class="row">
                            <div class="col-12 mb-5" style="overflow: auto">
                                <asp:Chart ID="Chart3" runat="server" DataSourceID="SqlDataSource2" Height="1000px" Palette="EarthTones">
    <Series>
        <asp:Series Name="Series1" ChartType="Bar" YValuesPerPoint="4" XValueMember="nomeproduto" YValueMembers="fatura" IsValueShownAsLabel="true"></asp:Series>
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
             select count(quantity) as qtde, cast(sum(value) as decimal (10,2)) as fatura, max(p.name) as nomeproduto from vendas v (nolock)
                join clientes c on c.id = v.client_id
                join locais l on l.id = v.location_id
                join maquinas m on m.id = v.machine_id
                join produtos p on p.id = v.good_id
                join fornecedores f on f.id = p.manufacturer_id
                join categorias ct on ct.id = p.category_id
                join estoque e on e.upc_code = v.upc_code 
                             where p.manufacturer_id = @id and ((e.name_client = 'CD' and v.occurred_at >= dateadd(day, -30, getdate())) or e.name_client != 'CD')
                group by p.id
                having count(quantity) &gt; 0
                order by qtde">
              <SelectParameters>
     <asp:SessionParameter Name="id" SessionField="idempresa" /> 
 </SelectParameters>
                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Performance End -->

                                    <!-- Performance Start -->
        <div class="col-xl-12 mb-5">
            <div class="d-flex">
                <h2 class="small-title">Quantidade/Faturamento por mês</h2>
            </div>
            <div class="card h-xl-100-card">
                <div class="card-body h-100">
                    <div class="h-100">
                        <div class="row">
                            <div class="col-12 mb-5" style="overflow: auto">
                                <asp:Chart ID="Chart4" runat="server" Width="970px" DataSourceID="SqlDataSource3" Palette="EarthTones">
    <Series>
        <asp:Series Name="Series1" ChartType="Spline" YValuesPerPoint="4" XValueMember="mesano" YValueMembers="fatura" IsValueShownAsLabel="true"></asp:Series>        
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
         <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand=
             "select count(quantity) as qtde, cast(sum(value) as decimal (10,2)) as fatura, convert(varchar,month(v.occurred_at))+'/'+  convert(varchar,year(v.occurred_at)) as mesano from vendas v (nolock)
   join clientes c on c.id = v.client_id
   join locais l on l.id = v.location_id
   join maquinas m on m.id = v.machine_id
   join produtos p on p.id = v.good_id
   join fornecedores f on f.id = p.manufacturer_id
   join categorias ct on ct.id = p.category_id
   join estoque e on e.upc_code = v.upc_code 
                where p.manufacturer_id = @id and e.name_client = 'CD' and v.occurred_at >= DATEADD(day, -30, GETDATE())
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
            </div>
        </div>
        <!-- Performance End -->
    </div>
    

</asp:Content>
