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
                try
                {
                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "select sum(sald) as saldo from estoque1"))
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
                             "select sum(sald) as saldo from estoque"))
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
                              "select count(distinct name_client) as qtdeloja from estoque1 "))
                    {
                        if (reader.Read())
                        {
                            lblLojas.Text = reader["qtdeloja"].ToString();
                        }
                        else
                            lblLojas.Text = "0";
                    }

                    //lblTotal.Text = (Convert.ToInt16(lblEstoque.Text) + Convert.ToInt16(lblEstoqueCD.Text)).ToString();
                }
                catch (Exception ex)
                {
                    /*lblTotal.Text = ex.Message*/;
                }

            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";
            string whereClause = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "e.manufacturer_id = " + ddlFornecedores.SelectedValue;
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "e.category_id = " + ddlCategoria.SelectedValue;
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "e.idclient = " + ddlLojas.SelectedValue;
            }

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(filtrofornecedor)) conditions.Add(filtrofornecedor);
            if (!string.IsNullOrEmpty(filtrocategoria)) conditions.Add(filtrocategoria);
            if (!string.IsNullOrEmpty(filtroloja)) conditions.Add(filtroloja);

            if (conditions.Count > 0)
            {
                whereClause = "WHERE " + string.Join(" AND ", conditions);
            }

            try
            {
                // Atualiza o SqlDataSource principal
                sdsDados.SelectCommand = "SELECT e.id, MAX(f.name) AS fornecedor, MAX(ct.descricao) AS descricao, MAX(e.name) AS produto, " +
                                         "MAX(p.image) AS image, MAX(e.upc_code) AS upc_code, MAX(e.sald) AS sald " +
                                         "FROM estoque1 e (NOLOCK) " +
                                         "JOIN fornecedores f ON f.id = e.manufacturer_id " +
                                         "JOIN categorias ct ON ct.id = e.category_id " +
                                         "JOIN produtos p ON p.id = e.id " +
                                         whereClause +
                                         " GROUP BY e.id " +
                                         "ORDER BY MAX(f.name), MAX(ct.descricao), MAX(e.name);";
                gdvDados.DataBind();
            }
            catch
            {
                lblDados.Text = "Não há dados";
            }

            try
            {
                // Atualiza os totais
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                         "SELECT ISNULL(SUM(e.sald), 0) AS saldo FROM estoque1 e " + whereClause))
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
                         "SELECT ISNULL(SUM(sald), 0) AS saldo FROM estoque"))
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
                          "SELECT ISNULL(COUNT(DISTINCT e.name_client), 0) AS qtdeloja FROM estoque1 e " + whereClause))
                {
                    if (reader.Read())
                    {
                        lblLojas.Text = reader["qtdeloja"].ToString();
                    }
                    else
                    {
                        lblLojas.Text = "0";
                    }
                }

                // Atualiza os gráficos
                SqlDataSource2.SelectCommand = "SELECT TOP(10) MAX(e.name) AS produto, ISNULL(MAX(e.sald), 0) AS saldocd " +
                                               "FROM estoque1 e (NOLOCK) " +
                                               "JOIN fornecedores f ON f.id = e.manufacturer_id " +
                                               "JOIN categorias ct ON ct.id = e.category_id " +
                                               "JOIN produtos p ON p.id = e.id " +
                                               whereClause +
                                               " AND e.sald > 0 " +
                                               "GROUP BY e.id " +
                                               "ORDER BY saldocd DESC";
                Chart2.DataBind();

                SqlDataSource3.SelectCommand = "SELECT TOP(10) MAX(e.name_client) AS client, ISNULL(MAX(e.sald), 0) AS saldo " +
                                               "FROM estoque1 e (NOLOCK) " +
                                               "JOIN fornecedores f ON f.id = e.manufacturer_id " +
                                               "JOIN categorias ct ON ct.id = e.category_id " +
                                               "JOIN produtos p ON p.id = e.id " +
                                               whereClause +
                                               " AND e.sald > 0 " +
                                               "GROUP BY e.idclient " +
                                               "ORDER BY saldo DESC";
                Chart3.DataBind();

                SqlDataSource4.SelectCommand = "SELECT TOP(10) MAX(c.descricao) AS client, ISNULL(MAX(e.sald), 0) AS saldo " +
                                               "FROM estoque1 e (NOLOCK) " +
                                               "JOIN categorias c ON c.id = e.category_id " +
                                               "JOIN produtos p ON p.id = e.id " +
                                               whereClause +
                                               " AND e.sald > 0 " +
                                               "GROUP BY c.descricao " +
                                               "ORDER BY saldo DESC";
                Chart4.DataBind();
            }
            catch (Exception ex)
            {
                lblErros.Text = ex.Message;

                lblEstoque.Text = "0";
                lblLojas.Text = "0";
                lblEstoqueCD.Text = "0";
            }
        }



        protected void ddlTotal_DataBound(object sender, EventArgs e)
        {
            ddlCategoria.Items.Insert(0, new ListItem("TODOS", "0"));
            ddlLojas.Items.Insert(0, new ListItem("TODOS", "0"));
            ddlFornecedores.Items.Insert(0, new ListItem("TODOS", "0"));
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
                         "select sum(sald) as saldo from estoque1"))
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
                         "select sum(sald) as saldo from estoque"))
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
                          "select count(distinct name_client) as qtdeloja from estoque1"))
                {
                    if (reader.Read())
                    {
                        lblLojas.Text = reader["qtdeloja"].ToString();
                    }
                    else
                        lblLojas.Text = "0";
                }

                //lblTotal.Text = (Convert.ToInt16(lblEstoque.Text) + Convert.ToInt16(lblEstoqueCD.Text)).ToString();
            }
            catch (Exception ex)
            {
                lblErros.Text = ex.Message;

                lblEstoque.Text = "0";
                lblLojas.Text = "0";
                lblEstoqueCD.Text = "0";
                //lblTotal.Text = "0";
            }
        }
    }
}