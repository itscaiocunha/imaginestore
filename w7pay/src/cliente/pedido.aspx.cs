using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web;
using System.Web.UI;

namespace w7pay.src.cliente
{
    public partial class pedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrProdutos1.Text = "<div class=\"product__item product__item-2 b-radius-2 mb-20\">                                         <div class=\"product__thumb fix\">                                             <div class=\"product-image w-img\">                                                 <a href = \"produto-detalhes.aspx?id=<%# Eval(\"id\") %>\">                                                     <img src = '<%# Eval(\"image\") %>' alt=\"product\">                                                 </a>                                                 &nbsp;                                             </div>                                             <div class=\"product__offer\">                                                 <%--<span class=\"discount\">-15%</span>--%>                                             </div>                                             <div class=\"product-action product-action-2\">                                                 <a href = \"#\" class=\"icon-box icon-box-1\" data-bs-toggle=\"modal\" data-bs-target=\"#productModalId\">                                                     <i class=\"fal fa-eye\"></i>                                                     <i class=\"fal fa-eye\"></i>                                                 </a>                                                 <a href = \"#\" class=\"icon-box icon-box-1\">                                                     <i class=\"fal fa-heart\"></i>                                                     <i class=\"fal fa-heart\"></i>                                                 </a>                                                 <a href = \"#\" class=\"icon-box icon-box-1\">                                                     <i class=\"fal fa-layer-group\"></i>                                                     <i class=\"fal fa-layer-group\"></i>                                                 </a>                                             </div>                                         </div>                                         <div class=\"product__content product__content-2\">                                             <h6><a href = \"product-details.html\" ><%# Eval(\"name\") %></a></h6>                                             < div class=\"rating mb-5 mt-10\">                                                 <ul>                                                     <li><a href = \"#\" >< i class=\"fal fa-star\"></i></a></li>                                                     <li><a href = \"#\" >< i class=\"fal fa-star\"></i></a></li>                                                     <li><a href = \"#\" >< i class=\"fal fa-star\"></i></a></li>                                                     <li><a href = \"#\" >< i class=\"fal fa-star\"></i></a></li>                                                     <li><a href = \"#\" >< i class=\"fal fa-star\"></i></a></li>                                                 </ul>                                                 <span>(01 review)</span>                                             </div>                                             <div class=\"price\">                                                 <span><%# Eval(\"cost_price\") %></span>                                             </div>                                         </div>                                              </div>";
            }
        }
    }
}