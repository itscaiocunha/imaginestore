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
            string filtrodata = "";
            string filtrodatas = "";

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodata = " occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";
            filtrodatas = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";

            if (ddlFornecedores.SelectedValue == "0" ^ ddlCategoria.SelectedValue == "0" ^ ddlLojas.SelectedValue == "0")
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where " + filtrodata + " and [status] = 'OK'"))
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
                          "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where " + filtrodata + " "))
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
                          "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas where " + filtrodata + " and [status] = 'OK'"))
                {
                    if (reader.Read())
                    {
                        lblTicket.Text = reader["ticket"].ToString();
                    }
                    else
                        lblTicket.Text = "0";
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas WHERE " + filtrodata + " and [status] = 'OK' "))
                {
                    if (reader.Read())
                    {
                        lblReceita.Text = reader["receita_media"].ToString();
                    }
                    else
                        lblReceita.Text = "0";
                }


                sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where " + filtrodata + " group by v.client_id having count(quantity) > 0    order by qtde desc";
                sdsDados.DataBind();

                SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where " + filtrodata + "    group by v.client_id    having count(quantity) > 0    order by fatura desc";
                SqlDataSource1.DataBind();

                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock) where" + filtrodata + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();

                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock) where " + filtrodata + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource4.DataBind();
            }
            else
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v  where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + "  and [status] = 'OK' and client_id = " + ddlLojas.SelectedValue + " AND category_id = " + ddlCategoria.SelectedValue + ""))
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
                          "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + " and client_id = " + ddlLojas.SelectedValue + " and category_id = " + ddlCategoria.SelectedValue + ""))
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
                          "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas where manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + "and [status] = 'OK' and client_id = " + ddlLojas.SelectedValue + "  and category_id = " + ddlCategoria.SelectedValue + ""))
                {
                    if (reader.Read())
                    {
                        lblTicket.Text = reader["ticket"].ToString();
                    }
                    else
                        lblTicket.Text = "0";
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas WHERE [status] = 'OK' AND manufacturer_id = '" + ddlFornecedores.SelectedValue + "'" + filtrodatas + "and client_id = " + ddlLojas.SelectedValue + "  and category_id = " + ddlCategoria.SelectedValue + ""))
                {
                    if (reader.Read())
                    {
                        lblReceita.Text = reader["receita_media"].ToString();
                    }
                    else
                        lblReceita.Text = "0";
                }


                sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + " and client_id = " + ddlLojas.SelectedValue + "  and category_id = " + ddlCategoria.SelectedValue + " group by v.client_id    having count(quantity) > 0    order by qtde desc";
                sdsDados.DataBind();

                SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + " and client_id = " + ddlLojas.SelectedValue + "  and category_id = " + ddlCategoria.SelectedValue + " group by v.client_id having count(quantity) > 0 order by fatura desc";
                SqlDataSource1.DataBind();

                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + " and client_id = " + ddlLojas.SelectedValue + "  and category_id = " + ddlCategoria.SelectedValue + " group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();

                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodatas + " and client_id = " + ddlLojas.SelectedValue + "  and category_id = " + ddlCategoria.SelectedValue + " group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource4.DataBind();
            }
            
        }

        protected void ddlTopQtdeVendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFornecedores.SelectedValue == "0")
            {
                sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where occurred_at > getdate() - 7 group by v.client_id having count(quantity) > 0 order by qtde desc";
                sdsDados.DataBind();
            }
            else
            {
                sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7 group by v.client_id    having count(quantity) > 0    order by qtde desc";
                sdsDados.DataBind();
            }
        }

        protected void ddlTopFaturamentoLoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFornecedores.SelectedValue == "0")
            {
                SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where occurred_at > getdate() - 7    group by v.client_id    having count(quantity) > 0    order by fatura desc";
                SqlDataSource1.DataBind();
            }
            else
            {
                SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.client_id    having count(quantity) > 0    order by fatura desc";
                SqlDataSource1.DataBind();
            }
        }

        protected void ddlTopQtdeVendaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFornecedores.SelectedValue == "0")
            {
                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock) where occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();
            }
            else
            {
                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();
            }
        }

        protected void ddlTopFaturamentoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFornecedores.SelectedValue == "0")
            {
                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock) where occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource4.DataBind();
            }
            else
            {
                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource4.DataBind();
            }
        }

        protected void ddlTopProdutoVenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            string topValue = ddlTopProdutoVenda.SelectedValue;
            string topClause = string.IsNullOrEmpty(topValue) ? "" : $"{topValue} ";

            SqlDataSource7.SelectCommand = $@"
        select {topClause}count(v.category_id) as qtde, max(c.descricao) as descricao 
        from vendas v 
        inner join categorias c on c.id = v.category_id 
        group by category_id 
        order by qtde desc";
            SqlDataSource7.DataBind();
        }

        protected void ddlTopVendasCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFornecedores.SelectedValue == "0")
            {
                SqlDataSource5.SelectCommand = "select " + ddlTopVendasCliente.SelectedValue + " client_name as client, count(*) as qtde from vendas group by client_name order by qtde desc";
                SqlDataSource5.DataBind();
            }
            else
            {
                SqlDataSource5.SelectCommand = "select " + ddlTopVendasCliente.SelectedValue + " client_name as client, count(*) as qtde from vendas where manufacturer_id = '" + ddlFornecedores.SelectedValue + "' group by client_name order by qtde desc";
                SqlDataSource5.DataBind();
            }
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