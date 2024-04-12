<%@ Page Title="" Language="C#" MasterPageFile="~/src/cliente/master.master" AutoEventWireup="true" Async="true" CodeBehind="produto-detalhes.aspx.cs" Inherits="w7pay.src.cliente.produtodetalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <!-- breadcrumb__area-start -->
    <section class="breadcrumb__area box-plr-75">
        <div class="container">
            <div class="row">
                <div class="col-xxl-12">
                    <div class="breadcrumb__wrapper">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Página Inicial</a></li>
                                <li class="breadcrumb-item active" aria-current="page">Loja</li>
                            </ol>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- breadcrumb__area-end -->

    <!-- product-details-start -->
    <div class="product-details">
        <div class="container">
            <div class="row">
                <div class="col-xl-6">
                    <div class="product__details-nav d-sm-flex align-items-start">
                        <%-- <ul class="nav nav-tabs flex-sm-column justify-content-between" id="productThumbTab" role="tablist">
                                <li class="nav-item" role="presentation">
                                  <button class="nav-link active" id="thumbOne-tab" data-bs-toggle="tab" data-bs-target="#thumbOne" type="button" role="tab" aria-controls="thumbOne" aria-selected="true">
                                      <img src="assets/img/product/nav/product-nav-1.jpg" alt="">
                                  </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                  <button class="nav-link" id="thumbTwo-tab" data-bs-toggle="tab" data-bs-target="#thumbTwo" type="button" role="tab" aria-controls="thumbTwo" aria-selected="false">
                                    <img src="assets/img/product/nav/product-nav-2.jpg" alt="">
                                  </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                  <button class="nav-link" id="thumbThree-tab" data-bs-toggle="tab" data-bs-target="#thumbThree" type="button" role="tab" aria-controls="thumbThree" aria-selected="false">
                                    <img src="assets/img/product/nav/product-nav-3.jpg" alt="">
                                  </button>
                                </li>
                            </ul>--%>
                        <div class="product__details-thumb">
                            <div class="tab-content" id="productThumbContent">
                                <div class="tab-pane fade show active" id="thumbOne" role="tabpanel" aria-labelledby="thumbOne-tab">
                                    <div class="product__details-nav-thumb w-img">
                                        <asp:Image ID="imgProduto" runat="server" />
                                    </div>
                                </div>
                                <%-- <div class="tab-pane fade" id="thumbTwo" role="tabpanel" aria-labelledby="thumbTwo-tab">
                                        <div class="product__details-nav-thumb w-img">
                                            <img src="assets/img/product/nav/product-nav-big-3.jpg" alt="">
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="thumbThree" role="tabpanel" aria-labelledby="thumbThree-tab">
                                        <div class="product__details-nav-thumb w-img">
                                            <img src="assets/img/product/nav/product-nav-big-2.jpg" alt="">
                                        </div>
                                    </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6">
                    <div class="product__details-content">
                        <h6>
                            <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h6>
                        <%--<div class="pd-rating mb-10">
                                <ul class="rating">
                                    <li><a href="#"><i class="fal fa-star"></i></a></li>
                                    <li><a href="#"><i class="fal fa-star"></i></a></li>
                                    <li><a href="#"><i class="fal fa-star"></i></a></li>
                                    <li><a href="#"><i class="fal fa-star"></i></a></li>
                                    <li><a href="#"><i class="fal fa-star"></i></a></li>
                                </ul>
                                <span>(01 review)</span>
                                <span><a href="#">Add your review</a></span>
                            </div>--%>
                        <div class="price mb-10">
                            <span>
                                <asp:Label ID="lblPreco" runat="server" Text=""></asp:Label></span>
                        </div>
                        <%--<div class="features-des mb-20 mt-10">
                                <ul>
                                    <li><a href="product-details.html"><i class="fas fa-circle"></i> Bass and Stereo Sound.</a></li>
                                    <li><a href="product-details.html"><i class="fas fa-circle"></i> Display with 3088 x 1440 pixels resolution.</a></li>
                                    <li><a href="product-details.html"><i class="fas fa-circle"></i> Memory, Storage &amp; SIM: 12GB RAM, 256GB.</a></li>
                                    <li><a href="product-details.html"><i class="fas fa-circle"></i> Androi v10.0 Operating system.</a></li>
                                </ul>
                            </div>--%>
                        <div class="product-stock mb-20">
                            <h5>Disponibiliade: <span>
                                <asp:Label ID="lblQtdeEstoque" runat="server" Text=""></asp:Label>
                                em estoque</span></h5>
                        </div>
                        <div class="cart-option mb-15">
                            <div class="product-quantity mr-20">
                                <div class="cart-plus-minus p-relative">
                                    <asp:TextBox ID="txtQtde" runat="server" Text="1" OnTextChanged="txtQtde_TextChanged"></asp:TextBox>
                                    <div class="dec qtybutton">
                                        <asp:LinkButton ID="lkbRetirar" runat="server">-</asp:LinkButton>
                                    </div>
                                    <div class="inc qtybutton">
                                        <asp:LinkButton ID="lkbAdicionar" runat="server" OnClick="lkbAdicionar_Click">+</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <asp:LinkButton ID="lkbAddCarrinho" CssClass="cart-btn" runat="server" OnClick="lkbAddCarrinho_Click">Adicionar ao carrinho</asp:LinkButton>
                        </div>
                        <div class="details-meta">
                            <div class="d-meta-left">
                                <div class="dm-item mr-20">
                                    <asp:LinkButton ID="lkbAddFavoritos" runat="server" OnClick="lkbAddFavoritos_Click"><i class="fal fa-heart"></i>Favoritar</asp:LinkButton>
                                    <a href="#"></a>
                                </div>
                            </div>
                            <div class="d-meta-left">
                                <div class="dm-item">
                                    <a href="#"><i class="fal fa-share-alt"></i>Compartilhar</a>
                                </div>
                            </div>
                        </div>
                        <div class="product-tag-area mt-15">
                            <div class="product_info">
                                <span class="sku_wrapper">
                                    <span class="title">SKU:</span>
                                    <span class="sku">
                                        <asp:Label ID="lblSku" runat="server" Text=""></asp:Label></span>
                                </span>
                                <span class="posted_in">
                                    <span class="title">Categoria</span>
                                    <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
                                </span>
                                <%-- <span class="tagged_as">
                                        <span class="title">Tags:</span>
                                        <a href="#">Smartphone</a>, 
                                        <a href="#">Tablets</a>
                                    </span>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- product-details-end -->

    <!-- product-details-des-start -->
    <div class="product-details-des mt-40 mb-60">
        <div class="container">
            <%-- <div class="row">
                <div class="col-xl-12">
                    <div class="product__details-des-tab">
                        <ul class="nav nav-tabs" id="productDesTab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="des-tab" data-bs-toggle="tab" data-bs-target="#des" type="button" role="tab" aria-controls="des" aria-selected="true">Descrição </button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="aditional-tab" data-bs-toggle="tab" data-bs-target="#aditional" type="button" role="tab" aria-controls="aditional" aria-selected="false">Informação Adicional</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="review-tab" data-bs-toggle="tab" data-bs-target="#review" type="button" role="tab" aria-controls="review" aria-selected="false">Avaliações </button>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
           <div class="tab-content" id="prodductDesTaContent">
                <div class="tab-pane fade active show" id="des" role="tabpanel" aria-labelledby="des-tab">
                    <div class="product__details-des-wrapper">
                        <asp:Label ID="lblDescricao" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="tab-pane fade" id="aditional" role="tabpanel" aria-labelledby="aditional-tab">
                    <div class="product__desc-info">
                        <asp:Label ID="lblInfoAdicional" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="tab-pane fade" id="review" role="tabpanel" aria-labelledby="review-tab">
                    <div class="product__details-review">
                        <div class="row">
                            <div class="col-xl-4">
                                <div class="review-rate">
                                    <h5>
                                        <asp:Label ID="lblScore" runat="server" Text=""></asp:Label></h5>
                                    <div class="review-star">
                                        <a href="#"><i class="fas fa-star"></i></a>
                                        <a href="#"><i class="fas fa-star"></i></a>
                                        <a href="#"><i class="fas fa-star"></i></a>
                                        <a href="#"><i class="fas fa-star"></i></a>
                                        <a href="#"><i class="fas fa-star"></i></a>
                                    </div>
                                    <span class="review-count">
                                        <asp:Label ID="lblQtdeAvaliacao" runat="server" Text=""></asp:Label> avalições</span>
                                </div>
                            </div>
                            <div class="col-xl-8">
                                <div class="review-des-infod">
                                    <h6>1 review for "<span>Wireless Bluetooth Over-Ear Headphones</span>"</h6>
                                    <div class="review-details-des">
                                        <div class="author-image mr-15">
                                            <a href="#">
                                                <img src="assets/img/author/author-sm-1.jpeg" alt=""></a>
                                        </div>
                                        <div class="review-details-content">
                                            <div class="str-info">
                                                <div class="review-star mr-15">
                                                    <a href="#"><i class="fas fa-star"></i></a>
                                                    <a href="#"><i class="fas fa-star"></i></a>
                                                    <a href="#"><i class="fas fa-star"></i></a>
                                                    <a href="#"><i class="fas fa-star"></i></a>
                                                    <a href="#"><i class="fas fa-star"></i></a>
                                                </div>
                                                <div class="add-review-option">
                                                    <a href="#">Add Review</a>
                                                </div>
                                            </div>
                                            <div class="name-date mb-30">
                                                <h6>admin – <span>May 27, 2021</span></h6>
                                            </div>
                                            <p>A light chair, easy to move around the dining table and about the room. Duis aute irure dolor in reprehenderit in
                                                <br>
                                                voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-12">
                                <div class="product__details-comment ">
                                    <div class="comment-title mb-20">
                                        <h3>Add a review</h3>
                                        <p>Your email address will not be published. Required fields are marked *</p>
                                    </div>
                                    <div class="comment-rating mb-20">
                                        <span>Overall ratings</span>
                                        <ul>
                                            <li><a href="#"><i class="fas fa-star"></i></a></li>
                                            <li><a href="#"><i class="fas fa-star"></i></a></li>
                                            <li><a href="#"><i class="fas fa-star"></i></a></li>
                                            <li><a href="#"><i class="fas fa-star"></i></a></li>
                                            <li><a href="#"><i class="fas fa-star"></i></a></li>
                                        </ul>
                                    </div>
                                    <div class="comment-input-box">                                        
                                            <div class="row">
                                                <div class="col-xxl-6 col-xl-6">
                                                    <div class="comment-input">
                                                        <input type="text" placeholder="Your Name">
                                                    </div>
                                                </div>
                                                <div class="col-xxl-6 col-xl-6">
                                                    <div class="comment-input">
                                                        <input type="email" placeholder="Your Email">
                                                    </div>
                                                </div>
                                                <div class="col-xxl-12">
                                                    <textarea placeholder="Your review" class="comment-input comment-textarea"></textarea>
                                                </div>
                                                <div class="col-xxl-12">
                                                    <div class="comment-agree d-flex align-items-center mb-25">
                                                        <div class="form-check">
                                                            <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
                                                            <label class="form-check-label" for="flexCheckDefault">
                                                                Save my name, email, and website in this browser for the next time I comment.
                                                         
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-12">
                                                    <div class="comment-submit">
                                                        <button type="submit" class="cart-btn">Submit</button>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
        </div>
    </div>
    <!-- product-details-des-end -->

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
                                                    <div class="cart-plus-minus p-relative">
                                                        <input type="text" value="1" />
                                                    </div>
                                                </div>
                                                <div class="pro-cart-btn mb-25">
                                                    <button class="cart-btn" type="submit">Add to cart</button>
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
</asp:Content>
