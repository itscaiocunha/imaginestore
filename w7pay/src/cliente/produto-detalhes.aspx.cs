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
    public partial class produtodetalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                  "select p.id, descricao, name, default_price, image from produtos p join categorias c on c.id = p.category_id where p.id = '" + Request.QueryString["id"].ToString() +"'"))
                {
                    if (reader.Read())
                    {
                        imgProduto.ImageUrl = reader["image"].ToString();
                        lblTitulo.Text = reader["name"].ToString();
                        lblPreco.Text = reader["default_price"].ToString();
                        lblSku.Text = reader["id"].ToString();
                        lblCategoria.Text = reader["descricao"].ToString();
                    }
                }
            }
        }

        protected void lkbAdicionar_Click(object sender, EventArgs e)
        {

        }

        protected void txtQtde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lkbAddCarrinho_Click(object sender, EventArgs e)
        {

        }

        protected void lkbAddFavoritos_Click(object sender, EventArgs e)
        {

        }
    }
}