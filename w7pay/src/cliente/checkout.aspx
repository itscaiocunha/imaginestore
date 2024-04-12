<%@ Page Title="" Language="C#" MasterPageFile="~/src/cliente/master.master" AutoEventWireup="true" Async="true" CodeBehind="checkout.aspx.cs" Inherits="w7pay.src.cliente.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <!-- page-banner-area-start -->
    <div class="page-banner-area page-banner-height-2" data-background="assets/img/banner/page-banner-4.png">
        <div class="container">
            <div class="row">
                <div class="col-xl-12">
                    <div class="page-banner-content text-center">
                        <h4 class="breadcrumb-title">Checkout</h4>
                        <div class="breadcrumb-two">
                            <nav>
                                <nav class="breadcrumb-trail breadcrumbs">
                                    <ul class="breadcrumb-menu">
                                        <li class="breadcrumb-trail">
                                            <a href="dashboard.aspx"><span>Página Inicial</span></a>
                                        </li>
                                        <li class="trail-item">
                                            <span>Checkout</span>
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

    <!-- coupon-area-start -->
    <section class="coupon-area pt-120 pb-30">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="coupon-accordion">
                        <!-- ACCORDION START -->
                        <h3>Já é nosso cliente? <span id="showlogin">Clique aqui para o login</span></h3>
                        <div id="checkout-login" class="coupon-content">
                            <div class="coupon-info">
                                <p class="coupon-text">Insira seus dados de acesso.</p>

                                <p class="form-row-first">
                                    <label for="name">E-mail de acesso  <span>*</span></label>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                </p>
                                <p class="form-row-last">
                                    <label for="pass">Senha <span>*</span></label>
                                    <asp:TextBox ID="txtSenha" TextMode="Password" runat="server"></asp:TextBox>
                                </p>
                                <p class="form-row">
                                    <asp:LinkButton ID="lkbAcessar" CssClass="tp-in-btn w-100" runat="server">Acessar</asp:LinkButton>
                                    <%-- <label>
                                    <input type="checkbox">
                                    Remember me
                                </label>
                                </p>
                                <p class="lost-password">
                                <a href="#">Lost your password?</a>
                                </p>--%>
                            </div>
                        </div>
                        <!-- ACCORDION END -->
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="coupon-accordion">
                        <!-- ACCORDION START -->
                        <h3>Tem um cupom? <span id="showcoupon">Clique aqui para inserir o código</span></h3>
                        <div id="checkout_coupon" class="coupon-checkout-content">
                            <div class="coupon-info">

                                <p class="checkout-coupon">
                                    <asp:TextBox ID="txtCupom" runat="server" CssClass="input-text"></asp:TextBox>
                                    <asp:Button ID="btnCupom" runat="server" Text="Aplicar Cupom" CssClass="tp-btn-h1"  />
                                </p>

                            </div>
                        </div>
                        <!-- ACCORDION END -->
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- coupon-area-end -->

    <!-- checkout-area-start -->
    <section class="checkout-area pb-85">
        <div class="container">

            <div class="row">
                <div class="col-lg-6">
                    <div class="checkbox-form">
                        <h3>Detalhes do Pagamento</h3>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="checkout-form-list">
                                    <label>Primeiro Nome <span class="required">*</span></label>
                                    <asp:TextBox ID="txtPrimeiroNome" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="checkout-form-list">
                                    <label>Último Nome <span class="required">*</span></label>
                                    <asp:TextBox ID="txtUltimoNome" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="checkout-form-list">
                                    <label>CEP <span class="required">*</span></label>
                                    <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="checkout-form-list">
                                    <label>Endereço <span class="required">*</span></label>
                                    <asp:TextBox ID="txtEndereco" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="checkout-form-list">
                                    <label>Cidade <span class="required">*</span></label>
                                    <asp:TextBox ID="txtCidade" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="checkout-form-list">
                                    <label>Estado <span class="required">*</span></label>

                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="checkout-form-list">
                                    <label>E-mail <span class="required">*</span></label>
                                    <asp:TextBox ID="txtEmailFinal" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="checkout-form-list">
                                    <label>Telefone <span class="required">*</span></label>
                                    <asp:TextBox ID="txtTelefoneFinal" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="checkout-form-list create-acc">
                                    <input id="cbox" type="checkbox">
                                    <label>Criar uma conta?</label>
                                </div>
                                <div id="cbox_info" class="checkout-form-list create-account">
                                    <p>Ao realizar o cadastro, aceito os termos de uso da plataforma.</p>
                                    <label>Senha <span class="required">*</span></label>
                                    <asp:TextBox ID="txtSenhaFinal" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="different-address">
                            <div class="ship-different-title">
                                <h3>
                                    <label>Selecionar a loja para retirar</label>
                                    <input id="ship-box" type="checkbox">
                                </h3>
                            </div>
                            <div id="ship-box-info">
                                <div class="row">
                                   <div class="col-md-6">
    <div class="checkout-form-list">
        <label>Loja Imagine Store <span class="required">*</span></label>

    </div>
</div> 
                                </div>
                            </div>
                            <div class="order-notes">
                                <div class="checkout-form-list">
                                    <label>Observações</label>
                                    <textarea id="checkout-mess" cols="30" rows="10" placeholder="Digite aqui suas observações..."></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="your-order mb-30 ">
                        <h3>Sua Compra</h3>
                        <div class="your-order-table table-responsive">
                            <table>                              
    <tbody>
                            <asp:GridView ID="gdvCheckout" runat="server" EmptyDataText="Não há produtos no carrinho." ShowHeader="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr class="cart_item">
    <td class="product-name"><%# Eval("name") %> <strong class="product-quantity">× <%# Eval("qtde") %> </strong>
    </td>
    <td class="product-total">
        <span class="amount"><%# Eval("total") %> </span>
    </td>
</tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsCheckout" runat="server"></asp:SqlDataSource>
                            
                                    
                                </tbody>
                              </table>
                              <table>
                                    <tr class="cart-subtotal">
                                        <th>Carrinho Sub-total</th>
                                        <td><span class="amount">
                                            <asp:Label ID="lblSubtotal" runat="server" Text="0,00"></asp:Label></span></td>
                                    </tr>
                                    <tr class="shipping">
                                        <th>Envio</th>
                                        <td>
                                            <ul>
                                                <li>
                                                    <input type="radio" name="shipping">
                                                    <label>
                                                        Desconto: <span class="amount">
                                                            <asp:Label ID="lblDEsconto" runat="server" Text="0,00"></asp:Label></span>
                                                    </label>
                                                </li>
                                                <li>
                                                    <input type="radio" name="shipping">
                                                    <label>Envio Grátis:</label>
                                                </li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr class="order-total">
                                        <th>Valor Total</th>
                                        <td><strong><span class="amount">
                                            <asp:Label ID="lblValorTotal" runat="server" Text="0,00"></asp:Label></span></strong>
                                        </td>
                                    </tr>
                            </table>
                        </div>

                        <div class="payment-method">
                            <div class="accordion" id="checkoutAccordion">                                
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="paymentTwo">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#payment" aria-expanded="false" aria-controls="payment">
                                           Pix
                                       
                                        </button>
                                    </h2>
                                    <div id="payment" class="accordion-collapse collapse" aria-labelledby="paymentTwo" data-bs-parent="#checkoutAccordion">
                                        <div class="accordion-body">
                                            <p>Vamos gerar a chave pix.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="paypalThree">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#paypal" aria-expanded="false" aria-controls="paypal">
                                            Cartão de Crédito
                                       
                                        </button>
                                    </h2>
                                    <div id="paypal" class="accordion-collapse collapse" aria-labelledby="paypalThree" data-bs-parent="#checkoutAccordion">
                                        <div class="accordion-body">
                                            <p>
                                                Pague via PayPal; você pode pagar com seu cartão de crédito se não tiver um Conta Paypal.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="order-button-payment mt-20">
                                <button type="submit" class="tp-btn-h1">Finalizar Compra</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>
    <!-- checkout-area-end -->

    <!-- cta-area-start -->
    <section class="cta-area d-ldark-bg pt-55 pb-10">
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
                                <a href="#">
                                    <img src="assets/img/brand/app_ios.png" alt=""></a>
                                <a href="#">
                                    <img src="assets/img/brand/app_android.png" alt=""></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>
    <!-- cta-area-end -->
</asp:Content>
