<%@ Page Title="" Language="C#" MasterPageFile="~/src/cliente/master.master" AutoEventWireup="true" Async="true" CodeBehind="carrinho.aspx.cs" Inherits="w7pay.src.cliente.carrinho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">     
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
                                             <asp:GridView ID="gdvCarrinhoFinal" EmptyDataText="Não há produtos no carrinho." ShowHeader="false" runat="server" AutoGenerateColumns="False" DataSourceID="sdsCarrinhoFinal" OnRowCommand="gdvCarrinhoFinal_RowCommand">
                                                 <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
   <td class="product-thumbnail"><a href="shop-details.html"><img src='<%# Eval("image") %>' alt=""></a></td>
   <td class="product-name"><a href="shop-details.html"><%# Eval("name") %></a></td>
   <td class="product-price"><span class="amount"><%# Eval("subtotal") %></span></td>
   <td class="product-quantity">
         <div class="cart-plus-minus">
<asp:TextBox ID="txtQtdeItem" runat="server" Text='<%# Eval("qtde") %>'></asp:TextBox><div class="dec qtybutton"><asp:LinkButton ID="lkbRetiraQtde" CommandName="Retira" CommandArgument='<%# Eval("id") %>' runat="server">-</asp:LinkButton></div><div class="inc qtybutton"><asp:LinkButton ID="lkbAddItem" CommandName="Adiciona" CommandArgument='<%# Eval("id") %>' runat="server">+</asp:LinkButton></div></div>
   </td>
   <td class="product-subtotal"><span class="amount"><%# Eval("total") %></span></td>
   <td class="product-remove"><a href="#">
        <asp:LinkButton ID="lkbRemoverItem" CommandName="Remover" CommandArgument='<%# Eval("id") %>' runat="server"><i class="fa fa-times"></i></asp:LinkButton></a></td>
</tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 </Columns>
                                             </asp:GridView>
                                             <asp:SqlDataSource ID="sdsCarrinhoFinal" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT c.id, p.image, c.client_id, c.product_id, p.name, c.subtotal, c.qtde, c.total from carrinho c
join produtos p on p.id = c.product_id"></asp:SqlDataSource>                       
                        </div>
                        <div class="row">
                           <div class="col-12">
                                 <div class="coupon-all">
                                    <div class="coupon">                                       
    <asp:TextBox ID="txtCupom" runat="server" CssClass="input-text"></asp:TextBox>
    <asp:Button ID="btnCupom" runat="server" Text="Aplicar Cupom" CssClass="tp-btn-h1" OnClick="btnCupom_Click" />
                                    </div>
                                    <div class="coupon2">
        <asp:Button ID="btnAtualizarCarrinho" runat="server" Text="Atualizar Carrinho" CssClass="tp-btn-h1" OnClick="btnAtualizarCarrinho_Click" />
                                    </div>
                                 </div>
                           </div>
                        </div>
                        <div class="row justify-content-end">
                           <div class="col-md-5">
                                 <div class="cart-page-total">
                                    <h2>Informações do Carrinho</h2>
                                    <ul class="mb-20">
                                       <li>Subtotal R$ <span>
            <asp:Label ID="lblSubtotal" runat="server" Text="0,00"></asp:Label></span></li>
                                       <li>Total R$ <span>
                <asp:Label ID="lblTotal" runat="server" Text="0,00"></asp:Label></span></li>
                                    </ul>
                                    <asp:LinkButton ID="lkbCheckout" runat="server" OnClick="lkbCheckout_Click">Ir para Checkout</asp:LinkButton>
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
</asp:Content>
