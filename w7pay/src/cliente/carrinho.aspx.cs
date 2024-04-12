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
    public partial class carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gdvCarrinhoFinal_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

        }

        protected void btnCupom_Click(object sender, EventArgs e)
        {

        }

        protected void btnAtualizarCarrinho_Click(object sender, EventArgs e)
        {

        }

        protected void lkbCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx", false);
        }
    }
}