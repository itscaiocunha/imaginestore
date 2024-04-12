using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("loja.aspx?busca=" + txtBuscaProduto.Text + "", false);
        }

        protected void gdvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Response.Redirect("loja.aspx?categoria=" + e.CommandArgument.ToString() + "", false);
        }
    }
}