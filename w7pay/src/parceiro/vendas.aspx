<%@ Page Title="" Language="C#" MasterPageFile="~/src/parceiro/masterparceiro.master" AutoEventWireup="true" CodeBehind="vendas.aspx.cs" Inherits="w7pay.src.parceiro.vendas2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
    <script src="../js/mascara.js"></script>
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
                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <div class="d-inline-block float-md-start me-1 mb-1 search-input-container w-100 shadow bg-foreground">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Filtrar"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <div class="d-inline-block float-md-start me-1 mb-1 search-input-container w-100 shadow bg-foreground">
                        <asp:TextBox ID="txtDataInicio" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <div class="d-inline-block float-md-start me-1 mb-1 search-input-container w-100 shadow bg-foreground">
                        <asp:TextBox ID="txtDataFim" runat="server" MaxLength="10"  onkeyup="formataData(this,event);" CssClass="form-control" placeholder="__/__/____" Required></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbFiltro" runat="server"
                        CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbFiltro_Click">
   <i data-acorn-icon="search"></i> Atualizar
                    </asp:LinkButton>
                </div>
                <div class="col-sm-12 col-md-5 col-lg-3 col-xxl-2 mb-1">
                    <asp:LinkButton ID="lkbLimpar" runat="server"
                        CssClass="btn btn-outline-primary btn-icon btn-icon-start ms-0 ms-sm-1 w-100 w-md-auto" OnClick="lkbLimpar_Click">
<i data-acorn-icon="close"></i> Limpar
                    </asp:LinkButton>
                </div>
                <!-- Search End -->

                <div class="col-sm-12 col-md-7 col-lg-9 col-xxl-10 text-end mb-1">
                    <div class="d-inline-block">
                        <!-- Print Button Start -->
                        <%--<asp:LinkButton ID="btnImprimir" runat="server" CssClass="btn btn-icon btn-icon-only btn-foreground-alternate shadow"><i data-acorn-icon="print"></i></asp:LinkButton>
            <!-- Print Button End -->--%>

                        <!-- Export Dropdown Start -->
                        <%--<div class="d-inline-block">
                <button class="btn p-0" data-bs-toggle="dropdown" type="button" data-bs-offset="0,3">
                    <span
                        class="btn btn-icon btn-icon-only btn-foreground-alternate shadow dropdown"
                        data-bs-delay="0"
                        data-bs-placement="top"
                        data-bs-toggle="tooltip"
                        title="Export">
                        <i data-acorn-icon="download"></i>
                    </span>
                </button>
                <div class="dropdown-menu shadow dropdown-menu-end">
                    <asp:LinkButton ID="btnDownloadExcel" runat="server" CssClass="dropdown-item export-excel">Excel</asp:LinkButton>
                    <asp:LinkButton ID="btnDownloadPDf" runat="server" CssClass="dropdown-item export-pdf">Pdf</asp:LinkButton>
                    <asp:LinkButton ID="btnDownloadCSV" runat="server" CssClass="dropdown-item export-cvs">Csv</asp:LinkButton>
                </div>

            </div>--%>
                        <!-- Export Dropdown End -->

                        <!-- Length Start -->
                        <%--<div class="dropdown-as-select d-inline-block" data-childselector="span">
                <button class="btn p-0 shadow" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false" data-bs-offset="0,3">
                    <span
                        class="btn btn-foreground-alternate dropdown-toggle"
                        data-bs-toggle="tooltip"
                        data-bs-placement="top"
                        data-bs-delay="0"
                        title="Item Count">10 Items
</span>
                </button>
                <div class="dropdown-menu shadow dropdown-menu-end">
                    <asp:LinkButton ID="btnView5" runat="server" CssClass="dropdown-item">5 Itens</asp:LinkButton>
                    <asp:LinkButton ID="btnView20" runat="server" CssClass="dropdown-item active">20 Itens</asp:LinkButton>
                    <asp:LinkButton ID="btnView50" runat="server" CssClass="dropdown-item">50 Itens</asp:LinkButton>
                </div>
            </div>--%>
                        <!-- Length End -->
                    </div>
                </div>
            </div>
            <!-- Order List Start -->
            <div class="row">
                <div class="mb-5">
                    <div class="row g-2">
                        <div class="col-6 col-md-4 col-lg-2">
                            <div class="card h-100 hover-scale-up cursor-pointer">
                                <div class="card-body d-flex flex-column align-items-center">
                                    <div class="sw-6 sh-6 rounded-xl d-flex justify-content-center align-items-center border border-primary mb-4">
                                        <i data-acorn-icon="cart" class="text-primary"></i>
                                    </div>
                                    <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">VENDAS REALIZADAS</div>
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
                                    <div class="mb-1 d-flex align-items-center text-alternate text-small lh-1-25">FATURAMENTO</div>
                                    <div class="text-primary cta-4">
                                        <asp:Label ID="lblTotalVendasPagas" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                <div class="col-12 mb-5">
                    <asp:GridView ID="gdvDados" Width="100%" runat="server" CellPadding="4" EmptyDataText="Não há dados para visualizar" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataSourceID="sdsDados">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="#Venda" SortExpression="id" />
                            <asp:BoundField DataField="location_name" HeaderText="Local" SortExpression="location_name" />
                            <asp:BoundField DataField="client_name" HeaderText="Cliente" SortExpression="client_name" />
                            <asp:BoundField DataField="machine_model_name" HeaderText="Máquina" SortExpression="machine_model_name" />
                            <asp:BoundField DataField="descricao" HeaderText="Categoria" SortExpression="descricao" />
                            <asp:BoundField DataField="product_name" HeaderText="Produto" SortExpression="product_name" />
                            <asp:BoundField DataField="occurred_at" HeaderText="Data da Venda" SortExpression="occurred_at" />
                            <asp:BoundField DataField="quantity" HeaderText="Quant." SortExpression="quantity" />
                            <asp:BoundField DataField="value" HeaderText="Valor" DataFormatString="R{0:c2}" SortExpression="value" />
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
                    <asp:Label ID="lblteste" runat="server"></asp:Label>
                    <asp:SqlDataSource ID="sdsDados" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select top 50 v.id, (f.name) as name, (v.location_name) as location_name, (v.client_name) as client_name, (v.machine_model_name) as machine_model_name ,  (ct.descricao) as descricao, (v.product_name) as product_name, (coil) as coil, (quantity) as quantity, (value) as value , convert(varchar,DATEPART(day,(occurred_at)))+'/'+convert(varchar,DATEPART(month,(occurred_at)))+'/'+convert(varchar,DATEPART(year,(occurred_at)))+ ' '+ convert(varchar,(occurred_at),108) as occurred_at from vendas v (nolock) join fornecedores f on f.id = v.manufacturer_id join categorias ct on ct.id = v.category_id where v.manufacturer_id = @id and v.occurred_at > getdate() - 7 order by occurred_at desc">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="idempresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
            <!-- Order List End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
