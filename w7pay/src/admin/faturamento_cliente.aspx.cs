using Microsoft.Practices.EnterpriseLibrary.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class faturamento_cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }                
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            sdsDados.SelectCommand =
                "select cliente, sum(qtde) as qtde, sum(valor) as valor " +
                "from (select masked_card_number as cliente, count(*) as qtde, sum(value) as valor from vendas " +
                "where masked_card_number is not null and masked_card_number &lt;&gt; ''" +
                "group by masked_card_number" +
                "" +
                "union all" +
                "" +
                "select convert(varchar,id) as cliente, count(*) as qtde, sum(value) as valor from vendas " +
                "where masked_card_number is null and masked_card_number = ''" +
                "group by id) as tab" +
                "" +
                "where cliente like '%" + txtBuscar.Text + "%'" +
                "group by cliente" +
                "order by valor desc, qtde desc";
            gdvDados.DataBind();
        }
    }
}