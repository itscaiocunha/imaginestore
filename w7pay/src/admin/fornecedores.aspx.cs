using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class fornecedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try { 
                //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }

                //atualizacao.GETFornecedores();
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            sdsDados.SelectCommand = "select *  from base_fornecedor_omie o left join fornecedores f on f.name = o.[Razão Social / Nome Completo] where f.name is not null and [Razão Social / Nome Completo] like '%" + txtBuscar.Text + "%' order by f.name";
            gdvDados.DataBind();
        }
    }
}