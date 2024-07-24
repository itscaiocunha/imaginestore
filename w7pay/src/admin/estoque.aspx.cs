using Microsoft.Practices.EnterpriseLibrary.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace w7pay.src
{
    public partial class estoque : System.Web.UI.Page
    {
        public class inventories
        {
            /// <summary>
            /// Nome do campo.
            /// </summary>
            public string distribution_center_id { get; set; }

            public string total_quantity { get; set; }
            public string committed_quantity { get; set; }
        }

        public class itemestoque
        {
            /// <summary>
            /// Nome do campo.
            /// </summary>
            public int id { get; set; }

            public int type { get; set; }
            public int manufacturer_id { get; set; }
            public int category_id { get; set; }
            public int name { get; set; }
            public int upc_code { get; set; }
            public int barcode { get; set; }
            public int external_id { get; set; }
            public int url { get; set; }
            public List<inventories> inventoriess { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //sdsDados.SelectCommand = "SELECT e.id, MAX(f.name) AS fornecedor, MAX(ct.descricao) AS descricao, MAX(e.name) AS produto, MAX(p.image) AS image, MAX(e.upc_code) AS upc_code, MAX(e.sald) AS sald from estoque1 e (nolock) join fornecedores f on f.id = e.manufacturer_id join categorias ct on ct.id = e.category_id join produtos p on p.id = e.id where f.id = '" + ddlFornecedores.SelectedValue + "'GROUP BY e.id ORDER BY MAX(f.name), MAX(ct.descricao), MAX(e.name); ";
                //gdvDados.DataBind();

                //try
                //{
                //    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                //             "select sum(sald) as saldo from estoque1 where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                //    {
                //        if (reader.Read())
                //        {
                //            lblEstoque.Text = reader["saldo"].ToString();
                //        }
                //        else
                //        {
                //            lblEstoque.Text = "0";

                //        }
                //    }

                //    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                //             "select sum(sald) as saldo from estoque where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                //    {
                //        if (reader.Read())
                //        {
                //            lblEstoqueCD.Text = reader["saldo"].ToString();
                //        }
                //        else
                //        {
                //            lblEstoqueCD.Text = "0";

                //        }
                //    }

                //    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                //              "select count(distinct name_client) as qtdeloja from estoque1 where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                //    {
                //        if (reader.Read())
                //        {
                //            lblLojas.Text = reader["qtdeloja"].ToString();
                //        }
                //        else
                //            lblLojas.Text = "0";
                //    }

                //    lblTotal.Text = (Convert.ToInt16(lblEstoque.Text) + Convert.ToInt16(lblEstoqueCD.Text)).ToString();
                //}
                //    catch (Exception ex)
                //    {
                //        lblTotal.Text = ex.Message;
                //    }

            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                sdsDados.SelectCommand = "SELECT e.id, MAX(f.name) AS fornecedor, MAX(ct.descricao) AS descricao, MAX(e.name) AS produto, MAX(p.image) AS image, MAX(e.upc_code) AS upc_code, MAX(e.sald) AS sald from estoque1 e (nolock) join fornecedores f on f.id = e.manufacturer_id join categorias ct on ct.id = e.category_id join produtos p on p.id = e.id where f.id = '" + ddlFornecedores.SelectedValue + "'GROUP BY e.id ORDER BY MAX(f.name), MAX(ct.descricao), MAX(e.name); ";
                gdvDados.DataBind();
            }
            catch
            {
                lblDados.Text = "Não há dados";
            }

            try
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                         "select sum(sald) as saldo from estoque1 where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                {
                    if (reader.Read())
                    {
                        lblEstoque.Text = reader["saldo"].ToString();
                    }
                    else
                    {
                        lblEstoque.Text = "0";

                    }
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                         "select sum(sald) as saldo from estoque where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                {
                    if (reader.Read())
                    {
                        lblEstoqueCD.Text = reader["saldo"].ToString();
                    }
                    else
                    {
                        lblEstoqueCD.Text = "0";

                    }
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "select count(distinct name_client) as qtdeloja from estoque1 where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                {
                    if (reader.Read())
                    {
                        lblLojas.Text = reader["qtdeloja"].ToString();
                    }
                    else
                        lblLojas.Text = "0";
                }

                lblTotal.Text = (Convert.ToInt16(lblEstoque.Text) + Convert.ToInt16(lblEstoqueCD.Text)).ToString();
            }
            catch (Exception ex)
            {
                lblErros.Text = ex.Message;

                lblEstoque.Text = "0";
                lblLojas.Text = "0";
                lblEstoqueCD.Text = "0";
                lblTotal.Text = "0";
            }
        }

        protected void lkbLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                sdsDados.SelectCommand = "SELECT e.id, MAX(f.name) AS fornecedor, MAX(ct.descricao) AS descricao, MAX(e.name) AS produto, MAX(p.image) AS image, MAX(e.upc_code) AS upc_code, MAX(e.sald) AS sald from estoque1 e (nolock) join fornecedores f on f.id = e.manufacturer_id join categorias ct on ct.id = e.category_id join produtos p on p.id = e.id where f.id = '" + ddlFornecedores.SelectedValue + "'GROUP BY e.id ORDER BY MAX(f.name), MAX(ct.descricao), MAX(e.name); ";
                gdvDados.DataBind();
            }
            catch
            {
                lblDados.Text = "Não há dados";
            }

            try
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                         "select sum(sald) as saldo from estoque1 where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                {
                    if (reader.Read())
                    {
                        lblEstoque.Text = reader["saldo"].ToString();
                    }
                    else
                    {
                        lblEstoque.Text = "0";

                    }
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                         "select sum(sald) as saldo from estoque where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                {
                    if (reader.Read())
                    {
                        lblEstoqueCD.Text = reader["saldo"].ToString();
                    }
                    else
                    {
                        lblEstoqueCD.Text = "0";

                    }
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "select count(distinct name_client) as qtdeloja from estoque1 where manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
                {
                    if (reader.Read())
                    {
                        lblLojas.Text = reader["qtdeloja"].ToString();
                    }
                    else
                        lblLojas.Text = "0";
                }

                lblTotal.Text = (Convert.ToInt16(lblEstoque.Text) + Convert.ToInt16(lblEstoqueCD.Text)).ToString();
            }
            catch (Exception ex)
            {
                lblErros.Text = ex.Message;

                lblEstoque.Text = "0";
                lblLojas.Text = "0";
                lblEstoqueCD.Text = "0";
                lblTotal.Text = "0";
            }
        }
    }
}