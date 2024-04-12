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
    public partial class busca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                  "select p.id, c.descricao, name, cost_price, image from produtos p join categorias c on c.id = p.category_id where image <> ''"))
                {
                    while (reader.Read())
                    {
                        ltrProdutos1.Text += "<div class='col-sm-6 col-md-4 col-lg-3 col-xl-3 col-xxl-2'><div class='product__item product__item-2 b-radius-2 mb-20'><div class='product__thumb fix'><div class='product-image w-img'><a href = 'produto-detalhes.aspx?id=" + reader["id"].ToString() + "'><img src = '" + reader["image"].ToString() + "' alt='product' width='20%' height='20%'></a>&nbsp;</div><div class='product__offer'></div><div class='product-action product-action-2'></div></div><div class='product__content product__content-2'><h6><a href = 'product-details.html'>" + reader["name"].ToString() + "</a></h6><div class='rating mb-5 mt-10'><span>" + reader["descricao"].ToString() + "</span></div><div class='price'><span>R$ " + reader["cost_price"].ToString() + "</span></div></div> </div></div>";
                    }
                }
            }
        }

        protected void ddlQtdePagina_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}