using Microsoft.Practices.EnterpriseLibrary.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class dashboard2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    hdfIdEmpresa.Value = Session["idempresa"].ToString();
                    lblMensagemBoasVindas.Text = "Bem-vindo, " + Session["nomeusuario"].ToString();

                    txtDataInicio.Text = DateTime.Now.Date.AddDays(-7).ToString(CultureInfo.CreateSpecificCulture("pt-BR")).Substring(0, 10);
                    txtDataFim.Text = DateTime.UtcNow.ToString(CultureInfo.CreateSpecificCulture("pt-BR")).Substring(0, 10);//DateTime.Now.Date.ToShortDateString();

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where occurred_at > getDate() - 7 and [status] = 'OK'"))
                    {
                        if (reader.Read())
                        {
                            lblFaturamento.Text = "R$ " + reader["ganho"].ToString();
                            lblVendas.Text = reader["qtde"].ToString();
                        }
                        else
                        {
                            lblFaturamento.Text = "R$ 0,00";
                            lblVendas.Text = "0";
                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(*) as qtde from produtos"))
                    {
                        if (reader.Read())
                        {
                            lblProdutos.Text = reader["qtde"].ToString();
                        }
                        else
                            lblProdutos.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT COUNT(*) as qtde FROM fornecedores"))
                    {
                        if (reader.Read())
                        {
                            lblFornecedores.Text = reader["qtde"].ToString();
                        }
                        else
                            lblFornecedores.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT COUNT(*) as qtde FROM clientes"))
                    {
                        if (reader.Read())
                        {
                            lblClientes.Text = reader["qtde"].ToString();
                        }
                        else
                            lblClientes.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT ROUND(SUM(value) / NULLIF(COUNT(*), 0), 2) AS ticket FROM vendas where occurred_at > getDate() - 7 and [status] = 'OK'"))
                    {
                        if (reader.Read())
                        {
                            lblTicket.Text = reader["ticket"].ToString();
                        }
                        else
                            lblTicket.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT ROUND(SUM(value) / NULLIF(COUNT(DISTINCT client_name), 0), 2) AS receita_media FROM vendas WHERE occurred_at > GETDATE() - 7 AND [status] = 'OK'"))
                    {
                        if (reader.Read())
                        {
                            lblReceita.Text = reader["receita_media"].ToString();
                        }
                        else
                            lblReceita.Text = "0";
                    }


                }
                catch (Exception ex)
                {
                    lblMensagemBoasVindas.Text = ex.Message;
                }
            }
        }

        protected void lkbLimpar_Click(object sender, EventArgs e)
        {
            txtDataInicio.Text = DateTime.Now.Date.AddDays(-7).ToString(CultureInfo.CreateSpecificCulture("pt-BR")).Substring(0, 10);
            txtDataFim.Text = DateTime.UtcNow.ToString(CultureInfo.CreateSpecificCulture("pt-BR")).Substring(0, 10);//DateTime.Now.Date.ToShortDateString();

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where occurred_at > getDate() - 7 and [status] = 'OK'"))
            {
                if (reader.Read())
                {
                    lblFaturamento.Text = "R$ " + reader["ganho"].ToString();
                    lblVendas.Text = reader["qtde"].ToString();
                }
                else
                {
                    lblFaturamento.Text = "R$ 0,00";
                    lblVendas.Text = "0";
                }
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where occurred_at > getDate() - 7 "))
            {
                if (reader.Read())
                {
                    lblProdutos.Text = reader["qtdelojas"].ToString();
                }
                else
                    lblProdutos.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT COUNT(*) as qtde FROM fornecedores"))
            {
                if (reader.Read())
                {
                    lblFornecedores.Text = reader["qtde"].ToString();
                }
                else
                    lblFornecedores.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT COUNT(*) as qtde FROM clientes"))
            {
                if (reader.Read())
                {
                    lblClientes.Text = reader["qtde"].ToString();
                }
                else
                    lblClientes.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas where occurred_at > getDate() - 7 " + "and [status] = 'OK'"))
            {
                if (reader.Read())
                {
                    lblTicket.Text = reader["ticket"].ToString();
                }
                else
                    lblTicket.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas WHERE occurred_at > GETDATE() - 7 AND [status] = 'OK' "))
            {
                if (reader.Read())
                {
                    lblReceita.Text = reader["receita_media"].ToString();
                }
                else
                    lblReceita.Text = "0";
            }

            sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7 group by v.client_id    having count(quantity) > 0    order by qtde desc";
            sdsDados.DataBind();

            SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.client_id    having count(quantity) > 0    order by fatura desc";
            SqlDataSource1.DataBind();

            SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource2.DataBind();

            SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource4.DataBind();
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";
            string whereClause = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue;
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue;
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue;
            }

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodatas = "occurred_at >= '" + datainicio.ToString("yyyy-MM-dd") + "' AND occurred_at <= '" + datafim.ToString("yyyy-MM-dd") + "'";

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(filtrofornecedor)) conditions.Add(filtrofornecedor);
            if (!string.IsNullOrEmpty(filtrocategoria)) conditions.Add(filtrocategoria);
            if (!string.IsNullOrEmpty(filtroloja)) conditions.Add(filtroloja);
            if (!string.IsNullOrEmpty(filtrodatas)) conditions.Add(filtrodatas);

            if (conditions.Count > 0)
            {
                whereClause = "WHERE " + string.Join(" AND ", conditions);
            }

            string query1 = "SELECT ISNULL(SUM(TRY_CAST(v.value AS DECIMAL(10,2))),0) AS ganho, COUNT(*) AS qtde FROM vendas v " + whereClause + " AND [status] = 'OK'";

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, query1))
            {
                if (reader.Read())
                {
                    lblFaturamento.Text = "R$ " + reader["ganho"].ToString();
                    lblVendas.Text = reader["qtde"].ToString();
                }
                else
                {
                    lblFaturamento.Text = "R$ 0,00";
                    lblVendas.Text = "0";
                }
            }

            string query2 = "SELECT COUNT(DISTINCT machine_id) AS qtdelojas FROM vendas v " + whereClause + " AND [status] = 'OK'";
            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, query2))
            {
                if (reader.Read())
                {
                    lblProdutos.Text = reader["qtdelojas"].ToString();
                }
                else
                {
                    lblProdutos.Text = "0";
                }
            }

            string query3 = "SELECT COUNT(*) AS qtde FROM fornecedores";
            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, query3))
            {
                if (reader.Read())
                {
                    lblFornecedores.Text = reader["qtde"].ToString();
                }
                else
                {
                    lblFornecedores.Text = "0";
                }
            }

            string query4 = "SELECT COUNT(*) AS qtde FROM clientes";
            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, query4))
            {
                if (reader.Read())
                {
                    lblClientes.Text = reader["qtde"].ToString();
                }
                else
                {
                    lblClientes.Text = "0";
                }
            }

            string query5 = "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas " + whereClause + " AND [status] = 'OK'";
            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, query5))
            {
                if (reader.Read())
                {
                    lblTicket.Text = reader["ticket"].ToString();
                }
                else
                {
                    lblTicket.Text = "0";
                }
            }

            string query6 = "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas " + whereClause + " AND [status] = 'OK'";
            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, query6))
            {
                if (reader.Read())
                {
                    lblReceita.Text = reader["receita_media"].ToString();
                }
                else
                {
                    lblReceita.Text = "0";
                }
            }

            sdsDados.SelectCommand = "SELECT " + ddlTopQtdeVendas.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.client_name) AS nomecliente FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.client_id HAVING COUNT(quantity) > 0 ORDER BY qtde DESC";
            sdsDados.DataBind();

            SqlDataSource1.SelectCommand = "SELECT " + ddlTopFaturamentoLoja.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.client_name) AS nomecliente FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.client_id HAVING COUNT(quantity) > 0 ORDER BY fatura DESC";
            SqlDataSource1.DataBind();

            SqlDataSource2.SelectCommand = "SELECT " + ddlTopQtdeVendaProduto.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.product_name) AS nomeproduto FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.good_id HAVING COUNT(quantity) > 0 ORDER BY qtde DESC";
            SqlDataSource2.DataBind();

            SqlDataSource4.SelectCommand = "SELECT " + ddlTopQtdeVendaProduto.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.product_name) AS nomeproduto FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.good_id HAVING COUNT(quantity) > 0 ORDER BY qtde DESC";
            SqlDataSource4.DataBind();
        }

        protected void ddlTopQtdeVendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";
            string whereClause = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue;
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue;
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue;
            }

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodatas = "occurred_at >= '" + datainicio.ToString("yyyy-MM-dd") + "' AND occurred_at <= '" + datafim.ToString("yyyy-MM-dd") + "'";

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(filtrofornecedor)) conditions.Add(filtrofornecedor);
            if (!string.IsNullOrEmpty(filtrocategoria)) conditions.Add(filtrocategoria);
            if (!string.IsNullOrEmpty(filtroloja)) conditions.Add(filtroloja);
            if (!string.IsNullOrEmpty(filtrodatas)) conditions.Add(filtrodatas);

            if (conditions.Count > 0)
            {
                whereClause = "WHERE " + string.Join(" AND ", conditions);
            }

            sdsDados.SelectCommand = "SELECT " + ddlTopQtdeVendas.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.client_name) AS nomecliente FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.client_id HAVING COUNT(quantity) > 0 ORDER BY qtde DESC";
            sdsDados.DataBind();
        }

        protected void ddlTopFaturamentoLoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";
            string whereClause = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue;
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue;
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue;
            }

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodatas = "occurred_at >= '" + datainicio.ToString("yyyy-MM-dd") + "' AND occurred_at <= '" + datafim.ToString("yyyy-MM-dd") + "'";

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(filtrofornecedor)) conditions.Add(filtrofornecedor);
            if (!string.IsNullOrEmpty(filtrocategoria)) conditions.Add(filtrocategoria);
            if (!string.IsNullOrEmpty(filtroloja)) conditions.Add(filtroloja);
            if (!string.IsNullOrEmpty(filtrodatas)) conditions.Add(filtrodatas);

            if (conditions.Count > 0)
            {
                whereClause = "WHERE " + string.Join(" AND ", conditions);
            }

            SqlDataSource1.SelectCommand = "SELECT " + ddlTopFaturamentoLoja.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.client_name) AS nomecliente FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.client_id HAVING COUNT(quantity) > 0 ORDER BY fatura DESC";
            SqlDataSource1.DataBind();
        }

        protected void ddlTopQtdeVendaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";
            string whereClause = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue;
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue;
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue;
            }

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodatas = "occurred_at >= '" + datainicio.ToString("yyyy-MM-dd") + "' AND occurred_at <= '" + datafim.ToString("yyyy-MM-dd") + "'";

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(filtrofornecedor)) conditions.Add(filtrofornecedor);
            if (!string.IsNullOrEmpty(filtrocategoria)) conditions.Add(filtrocategoria);
            if (!string.IsNullOrEmpty(filtroloja)) conditions.Add(filtroloja);
            if (!string.IsNullOrEmpty(filtrodatas)) conditions.Add(filtrodatas);

            if (conditions.Count > 0)
            {
                whereClause = "WHERE " + string.Join(" AND ", conditions);
            }

            SqlDataSource2.SelectCommand = "SELECT " + ddlTopQtdeVendaProduto.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.product_name) AS nomeproduto FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.good_id HAVING COUNT(quantity) > 0 ORDER BY qtde DESC";
            SqlDataSource2.DataBind();

            SqlDataSource4.SelectCommand = "SELECT " + ddlTopQtdeVendaProduto.SelectedValue + " COUNT(quantity) AS qtde, CAST(SUM(value) AS DECIMAL(10,2)) AS fatura, MAX(v.product_name) AS nomeproduto FROM vendas v " + whereClause + " AND [status] = 'OK' GROUP BY v.good_id HAVING COUNT(quantity) > 0 ORDER BY qtde DESC";
            SqlDataSource4.DataBind();
        }

        protected void ddlTopFaturamentoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue + "";
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue + "";
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue + "";
            }

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodatas = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";

            SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock) where " + filtrofornecedor + filtrodatas + "and [status] = 'OK' and " + filtrocategoria + "  and " + filtroloja + "  group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource4.DataBind();
        }

        protected void ddlTopProdutoVenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            string topValue = ddlTopProdutoVenda.SelectedValue;
            string topClause = string.IsNullOrEmpty(topValue) ? "" : $"TOP {topValue} ";

            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue;
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue;
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue;
            }

            DateTime datainicio = DateTime.ParseExact(txtDataInicio.Text, "dd/MM/yyyy", null);
            DateTime datafim = DateTime.ParseExact(txtDataFim.Text, "dd/MM/yyyy", null);

            filtrodatas = $"occurred_at >= '{datainicio:yyyy-MM-dd}' AND occurred_at <= '{datafim:yyyy-MM-dd}'";

            List<string> filtros = new List<string>();

            if (!string.IsNullOrEmpty(filtrofornecedor))
            {
                filtros.Add(filtrofornecedor);
            }
            if (!string.IsNullOrEmpty(filtrocategoria))
            {
                filtros.Add(filtrocategoria);
            }
            if (!string.IsNullOrEmpty(filtroloja))
            {
                filtros.Add(filtroloja);
            }

            string whereClause = string.Join(" AND ", filtros);
            if (!string.IsNullOrEmpty(whereClause))
            {
                whereClause = " AND " + whereClause;
            }

            SqlDataSource7.SelectCommand = $@"
                SELECT {topClause}count(v.category_id) AS qtde, max(c.descricao) AS descricao
                FROM vendas v
                INNER JOIN categorias c ON c.id = v.category_id
                WHERE {filtrodatas} AND [status] = 'OK' {whereClause}
                GROUP BY category_id
                ORDER BY qtde DESC";

            SqlDataSource7.DataBind();
        }

        protected void ddlTopVendasCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtrodatas = "";
            string filtrofornecedor = "";
            string filtrocategoria = "";
            string filtroloja = "";

            if (ddlFornecedores.SelectedValue != "0")
            {
                filtrofornecedor = "manufacturer_id = " + ddlFornecedores.SelectedValue + "";
            }

            if (ddlCategoria.SelectedValue != "0")
            {
                filtrocategoria = "category_id = " + ddlCategoria.SelectedValue + "";
            }

            if (ddlLojas.SelectedValue != "0")
            {
                filtroloja = "client_id = " + ddlLojas.SelectedValue + "";
            }

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodatas = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";

            SqlDataSource5.SelectCommand = "select " + ddlTopVendasCliente.SelectedValue + " client_name as client, count(*) as qtde from vendas where " + filtrofornecedor + filtrodatas + "and [status] = 'OK' and " + filtrocategoria + "  and " + filtroloja + " group by client_name order by qtde desc";
            SqlDataSource5.DataBind();
        }

        protected void ddlTotal_DataBound(object sender, EventArgs e)
        {
            ddlCategoria.Items.Insert(0, new ListItem("TODOS", "0"));
            ddlLojas.Items.Insert(0, new ListItem("TODOS", "0"));
            ddlFornecedores.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        //protected void rodarVendas(object sender, EventArgs e)
        //{
        //    atualizacao.GETVendas();
        //}
        //protected void rodarFornecedores(object sender, EventArgs e)
        //{
        //    atualizacao.GETFornecedores();
        //}
        //protected void rodarProdutos(object sender, EventArgs e)
        //{
        //    atualizacao.GETProdutos();
        //}
        //protected void rodarEstoque(object sender, EventArgs e)
        //{
        //   atualizacao.GETEstoque();
        //}
    }
}