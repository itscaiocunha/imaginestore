<%@ Page Title="" Language="C#" MasterPageFile="~/src/admin/principal.Master" AutoEventWireup="true" CodeBehind="vendas.aspx.cs" Inherits="w7pay.src.vendas2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
    <script src="../js/mascara.js"></script>
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
                            <h1 class="mb-0 pb-0 display-6" id="title">Relatório de Vendas</h1>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Filtros --%>
            <div class="row mb-2">   
                <div class="d-flex">
                    <h2 class="small-title">Estatísticas em tempo real</h2>
                </div>
                <%-- Filtro de Data (Inicio) --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <asp:TextBox ID="txtDataInicio" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
                    </div>
                </div>

                <%-- Filtro de Data (Fim) --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <asp:TextBox ID="txtDataFim" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
                    </div>
                </div>
            </div>
            
            <%-- Filtro de Fornecedores --%>
            <div class="row mb-2">
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlForncedores" class="form-label">Fornecedor:</label>
                        <asp:DropDownList ID="ddlFornecedores" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsFornecedores" DataTextField="name" DataValueField="id"></asp:DropDownList>
                        <asp:SqlDataSource ID="sdsFornecedores" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select * from fornecedores order by name"></asp:SqlDataSource>
                    </div>
                </div>

                <%-- Filtro de Produto --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlProduto" class="form-label">Produto:</label>
                        <asp:DropDownList ID="ddlProduto" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsProduto" DataTextField="name" DataValueField="id" OnDataBound="ddlCategoria_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsProduto" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select id, name from produtos order by name"></asp:SqlDataSource>
                    </div>
                </div>

                <%-- Filtro de Categoria --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlCategoria" class="form-label">Categoria:</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsCategoria" DataTextField="descricao" DataValueField="id" OnDataBound="ddlCategoria_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select id, descricao from categorias order by descricao"></asp:SqlDataSource>
                    </div>
                </div>

                <%-- Filtro de Canal --%>
                <div class="col-sm-12 col-md-5 col-lg-4 col-xxl-2 mb-1">
                    <div class="">
                        <label for="ddlCanal" class="form-label">Canal:</label>
                        <asp:DropDownList ID="ddlCanal" runat="server" CssClass="form-control shadow dropdown-menu-end" DataSourceID="sdsCanal" DataTextField="canal" DataValueField="canal">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCanal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="select canal from lojas group by canal"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        

           <div class="row mb-2">
                <%-- Botão de Filtrar --%>
                <div class="col-sm-12 col-md-3 col-lg-2 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbFiltro" runat="server" CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
                        <i data-acorn-icon="search"></i> Atualizar
                    </asp:LinkButton>
                </div>

                <%-- Botão de Limpar Filtro --%>
                <div class="col-sm-12 col-md-4 col-lg-2 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbLimpar" runat="server" CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbLimpar_Click">
                        <i data-acorn-icon="close"></i> Limpar
                    </asp:LinkButton>
                </div>

               <asp:Label ID="lblErro" runat="server"></asp:Label>
            </div>
        </div>
            
            <%-- Cards --%>
            <div class="row">
                <div class="mb-5">
                    <div class="row g-2">

                        <%--  --%>
                        <div class="col-6 col-md-4 col-lg-2">
                            <div class="card h-100 hover-scale-up cursor-pointer">
                                <div class="card-body d-flex flex-column align-items-center">
                                    <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                        <i data-acorn-icon="cart" class="text-primary"></i>
                                    </div>
                                    <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">FATURAMENTO TOTAL</div>
                                    <div class="text-primary cta-4">
                                        <asp:Label ID="lblVendas" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Produtos Vendidos --%>
                        <div class="col-6 col-md-4 col-lg-2">
                            <div class="card h-100 hover-scale-up cursor-pointer">
                                <div class="card-body d-flex flex-column align-items-center">
                                    <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                        <i data-acorn-icon="dollar" class="text-primary"></i>
                                    </div>
                                    <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">QUANTIDADE DE PRODUTOS VENDIDOS</div>
                                    <div class="text-primary cta-4">
                                        <asp:Label ID="lblQtde" runat="server"></asp:Label>
                                    </div>
                                 </div>
                             </div>
                         </div>

                        <%-- Ticket Médio por Compra --%>
                        <div class="col-6 col-md-4 col-lg-2">
                            <div class="card h-100 hover-scale-up cursor-pointer">
                                <div class="card-body d-flex flex-column align-items-center">
                                    <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                        <i data-acorn-icon="cart" class="text-primary"></i>
                                    </div>
                                    <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">TICKET MÉDIO POR COMPRA</div>
                                    <div class="text-primary cta-4">
                                        <asp:Label ID="lblTicket" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                     </div>
                 </div>
            </div>

            <%-- Tela de Carregamento --%>
            <asp:UpdateProgress ID="LoaderBar" runat="server" DisplayAfter="300" DynamicLayout="true">
                <ProgressTemplate>
                    <style type="text/css">
                        .updateprogress-overlay {
                            position: fixed;
                            top: 0;
                            left: 0;
                            width: 100%;
                            height: 100%;
                            background-color: rgba(0, 0, 0, 0.5);
                            z-index: 1000; 
                        }

                        .updateprogress-centered {
                            position: absolute;
                            top: 50%;
                            left: 50%;
                            transform: translate(-50%, -50%);
                            z-index: 1001; 
                        }

                        h1 {
                            font-size: 20px;
                            color: white;
                        }
                    </style>
                    <div class="updateprogress-centered">
                        <h1>Salvando... Por favor aguarde!</h1>
                    </div>
                    <div class="updateprogress-overlay"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <%-- Grid --%>
            <%--<div class="row">
                <div class="col-12 mb-5">              
                <asp:GridView ID="gdvDados" Width="100%" runat="server" CellPadding="4" EmptyDataText="Não há dados para visualizar" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataSourceID="sdsDados">
                <AlternatingRowStyle/>
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
                    <asp:SqlDataSource ID="sdsDados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand= "
                        select f.name, l.name, c.name, m.asset_number, ct.descricao, p.name, coil, quantity, value, occurred_at from vendas v (nolock)
                        join clientes c on c.id = v.client_id
                        join locais l on l.id = v.location_id
                        join maquinas m on m.id = v.machine_id
                        join produtos p on p.id = v.good_id
                        join fornecedores f on f.id = p.manufacturer_id 
                        join categorias ct on ct.id = p.category_id
                        where occurred_at > getDate() - 7
                        order by f.name, l.name, c.name, m.asset_number, ct.descricao, p.name">
                    </asp:SqlDataSource>
                </div>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
