<%@ Page Title="" Language="C#" MasterPageFile="~/src/cliente/master.master" AutoEventWireup="true" Async="true" CodeBehind="cicloway.aspx.cs" Inherits="w7pay.src.cliente.cicloway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdfTxid" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdfToken" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdfTokenEmpresa" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdfIdEmpresa" runat="server" />
    <asp:HiddenField ID="hdfTelefoneProprietario" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="4000" OnTick="Timer1_Tick"></asp:Timer>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <script src="js/mascara.js" type="text/javascript"></script>
            <!-- page-banner-area-start -->
            <div class="page-banner-area page-banner-height-2" data-background="assets/img/banner/page-banner-4.png">
                <div class="container">
                    <div class="row">
                        <div class="col-xl-12">
                            <div class="page-banner-content text-center">
                                <h4 class="breadcrumb-title">Seu Carrinho</h4>
                                <div class="breadcrumb-two">
                                    <nav>
                                        <nav class="breadcrumb-trail breadcrumbs">
                                            <ul class="breadcrumb-menu">
                                                <li class="breadcrumb-trail">
                                                    <a href="dashboard.aspx"><span>Página Inicial</span></a>
                                                </li>
                                                <li class="trail-item">
                                                    <span>Carrinho
                                                    </span>
                                                </li>
                                            </ul>
                                        </nav>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- page-banner-area-end -->

            <!-- cart-area-start -->
            <section class="cart-area pt-120 pb-120">
                <div class="container">
                    <div class="row">
                        <div class="col-12">

                            <div class="table-content table-responsive">
                                <h4>Selecione o tempo de uso</h4>
                                <div class="col-md-3">
                                    <asp:Button ID="btn5minutos" runat="server" Text="5 minutos" CssClass="btn btn-gradient-primary" OnClick="btn5minutos_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button1" runat="server" Text="15 minutos" CssClass="btn btn-gradient-primary" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button2" runat="server" Text="30 minutos" CssClass="btn btn-gradient-primary" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button3" runat="server" Text="60 minutos" CssClass="btn btn-gradient-primary" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="coupon-all">
                                        <div class="coupon">
                                            <asp:TextBox ID="txtCupom" runat="server" CssClass="input-text"></asp:TextBox>
                                            <asp:Button ID="btnCupom" runat="server" Text="Aplicar Cupom" CssClass="tp-btn-h1" OnClick="btnCupom_Click" />
                                        </div>
                                        <div class="coupon2">
                                            <asp:Image ID="imgQRCode" runat="server"></asp:Image>
                                            <asp:Label ID="lblChaveCopiaCola" runat="server" CssClass="form-control"></asp:Label>
                                            <asp:Label ID="lblTxid" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblMensagemPix" Font-Size="2em" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-end">
                                <div class="col-md-5">
                                    <div class="cart-page-total">
                                        <h2>Informações</h2>
                                        <ul class="mb-20">
                                            <li>Subtotal R$ <span>
                                                <asp:Label ID="lblSubtotal" runat="server" Text="0,00"></asp:Label></span></li>
                                            <li>Total R$ <span>
                                                <asp:Label ID="lblTotal" runat="server" Text="0,00"></asp:Label></span></li>
                                        </ul>
                                        <asp:Button ID="btnAtualizarCarrinho" runat="server" Text="Finalizar" CssClass="tp-btn-h1" OnClick="btnAtualizarCarrinho_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- cart-area-end -->

            <!-- cta-area-start -->
            <%-- <section class="cta-area d-ldark-bg pt-55 pb-10">
         <div class="container">
             <div class="row">
                 <div class="col-lg-4 col-md-6">
                     <div class="cta-item cta-item-d mb-30">
                         <h5 class="cta-title">Follow Us</h5>
                         <p>We make consolidating, marketing and tracking your social media website easy.</p>
                         <div class="cta-social">
                             <div class="social-icon">
                                 <a href="#" class="facebook"><i class="fab fa-facebook-f"></i></a>
                                 <a href="#" class="twitter"><i class="fab fa-twitter"></i></a>
                                 <a href="#" class="youtube"><i class="fab fa-youtube"></i></a>
                                 <a href="#" class="linkedin"><i class="fab fa-linkedin-in"></i></a>
                                 <a href="#" class="rss"><i class="fas fa-rss"></i></a>
                                 <a href="#" class="dribbble"><i class="fab fa-dribbble"></i></a>
                             </div>
                         </div>
                     </div>
                 </div>
                 <div class="col-lg-4 col-md-6">
                     <div class="cta-item mb-30">
                         <h5 class="cta-title">Sign Up To Newsletter</h5>
                         <p>Join 60.000+ subscribers and get a new discount coupon  on every Saturday.</p>
                         <div class="subscribe__form">
                            
                                 <input type="email" placeholder="Enter your email here...">
                                 <button>subscribe</button>
                            
                         </div>
                     </div>
                 </div>
                 <div class="col-lg-4 col-md-6">
                     <div class="cta-item mb-30">
                         <h5 class="cta-title">Download App</h5>
                         <p>DukaMarket App is now available on App Store & Google Play. Get it now.</p>
                         <div class="cta-apps">
                             <div class="apps-store">
                                 <a href="#"><img src="assets/img/brand/app_ios.png" alt=""></a>
                                 <a href="#"><img src="assets/img/brand/app_android.png" alt=""></a>
                             </div>
                         </div>
                     </div>
                 </div>
             </div>
         </div>

     </section>--%>
            <!-- cta-area-end -->

            <!-- shop modal start -->
            <div class="modal fade" id="productModalId" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered product__modal" role="document">
                    <div class="modal-content">
                        <div class="product__modal-wrapper p-relative">
                            <div class="product__modal-close p-absolute">
                                <button data-bs-dismiss="modal"><i class="fal fa-times"></i></button>
                            </div>
                            <div class="product__modal-inner">
                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12">
                                        <div class="product__modal-box">
                                            <div class="tab-content" id="modalTabContent">
                                                <div class="tab-pane fade show active" id="nav1" role="tabpanel" aria-labelledby="nav1-tab">
                                                    <div class="product__modal-img w-img">
                                                        <img src="assets/img/quick-view/quick-view-1.jpg" alt="">
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="nav2" role="tabpanel" aria-labelledby="nav2-tab">
                                                    <div class="product__modal-img w-img">
                                                        <img src="assets/img/quick-view/quick-view-2.jpg" alt="">
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="nav3" role="tabpanel" aria-labelledby="nav3-tab">
                                                    <div class="product__modal-img w-img">
                                                        <img src="assets/img/quick-view/quick-view-3.jpg" alt="">
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="nav4" role="tabpanel" aria-labelledby="nav4-tab">
                                                    <div class="product__modal-img w-img">
                                                        <img src="assets/img/quick-view/quick-view-4.jpg" alt="">
                                                    </div>
                                                </div>
                                            </div>
                                            <ul class="nav nav-tabs" id="modalTab" role="tablist">
                                                <li class="nav-item" role="presentation">
                                                    <button class="nav-link active" id="nav1-tab" data-bs-toggle="tab" data-bs-target="#nav1" type="button" role="tab" aria-controls="nav1" aria-selected="true">
                                                        <img src="assets/img/quick-view/quick-nav-1.jpg" alt="">
                                                    </button>
                                                </li>
                                                <li class="nav-item" role="presentation">
                                                    <button class="nav-link" id="nav2-tab" data-bs-toggle="tab" data-bs-target="#nav2" type="button" role="tab" aria-controls="nav2" aria-selected="false">
                                                        <img src="assets/img/quick-view/quick-nav-2.jpg" alt="">
                                                    </button>
                                                </li>
                                                <li class="nav-item" role="presentation">
                                                    <button class="nav-link" id="nav3-tab" data-bs-toggle="tab" data-bs-target="#nav3" type="button" role="tab" aria-controls="nav3" aria-selected="false">
                                                        <img src="assets/img/quick-view/quick-nav-3.jpg" alt="">
                                                    </button>
                                                </li>
                                                <li class="nav-item" role="presentation">
                                                    <button class="nav-link" id="nav4-tab" data-bs-toggle="tab" data-bs-target="#nav4" type="button" role="tab" aria-controls="nav4" aria-selected="false">
                                                        <img src="assets/img/quick-view/quick-nav-4.jpg" alt="">
                                                    </button>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12">
                                        <div class="product__modal-content">
                                            <h4><a href="product-details.html">Samsung C49J89: £875, Debenhams Plus</a></h4>
                                            <div class="product__review d-sm-flex">
                                                <div class="rating rating__shop mb-10 mr-30">
                                                    <ul>
                                                        <li><a href="#"><i class="fal fa-star"></i></a></li>
                                                        <li><a href="#"><i class="fal fa-star"></i></a></li>
                                                        <li><a href="#"><i class="fal fa-star"></i></a></li>
                                                        <li><a href="#"><i class="fal fa-star"></i></a></li>
                                                        <li><a href="#"><i class="fal fa-star"></i></a></li>
                                                    </ul>
                                                </div>
                                                <div class="product__add-review mb-15">
                                                    <span>01 review</span>
                                                </div>
                                            </div>
                                            <div class="product__price">
                                                <span>$109.00 – $307.00</span>
                                            </div>
                                            <div class="product__modal-des mt-20 mb-15">
                                                <ul>
                                                    <li><a href="#"><i class="fas fa-circle"></i>Bass and Stereo Sound.</a></li>
                                                    <li><a href="#"><i class="fas fa-circle"></i>Display with 3088 x 1440 pixels resolution.</a></li>
                                                    <li><a href="#"><i class="fas fa-circle"></i>Memory, Storage & SIM: 12GB RAM, 256GB.</a></li>
                                                    <li><a href="#"><i class="fas fa-circle"></i>Androi v10.0 Operating system.</a></li>
                                                </ul>
                                            </div>
                                            <div class="product__stock mb-20">
                                                <span class="mr-10">Availability :</span>
                                                <span>1795 in stock</span>
                                            </div>
                                            <div class="product__modal-form">

                                                <div class="pro-quan-area d-lg-flex align-items-center">
                                                    <div class="product-quantity mr-20 mb-25">
                                                        <div class="Carrinho de Compras-plus-minus p-relative">
                                                            <input type="text" value="1" />
                                                        </div>
                                                    </div>
                                                    <div class="pro-Carrinho de Compras-btn mb-25">
                                                        <button class="Carrinho de Compras-btn" type="submit">Add to Carrinho de Compras</button>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="product__stock mb-30">
                                                <ul>
                                                    <li><a href="#">
                                                        <span class="sku mr-10">SKU:</span>
                                                        <span>Samsung C49J89: £875, Debenhams Plus</span></a>
                                                    </li>
                                                    <li><a href="#">
                                                        <span class="cat mr-10">Categories:</span>
                                                        <span>iPhone, Tablets</span></a>
                                                    </li>
                                                    <li><a href="#">
                                                        <span class="tag mr-10">Tags:</span>
                                                        <span>Smartphone, Tablets</span></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- shop modal end -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
