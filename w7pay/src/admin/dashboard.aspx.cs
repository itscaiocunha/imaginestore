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
                             "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and [status] = 'OK'"))
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
                              "SELECT count(*) as qtde from produtos where manufacturer_id  = '" + ddlFornecedores.SelectedValue + "'"))
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
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas where manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getDate() - 7 " + "and [status] = 'OK'"))
                    {
                        if (reader.Read())
                        {
                            lblTicket.Text = reader["ticket"].ToString();
                        }
                        else
                            lblTicket.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas WHERE occurred_at > GETDATE() - 7 AND [status] = 'OK' AND manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
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
                             "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
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
                              "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
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
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas where manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getDate() - 7 " + "and [status] = 'OK'"))
            {
                if (reader.Read())
                {
                    lblTicket.Text = reader["ticket"].ToString();
                }
                else
                    lblTicket.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas WHERE occurred_at > GETDATE() - 7 AND [status] = 'OK' AND manufacturer_id = '" + ddlFornecedores.SelectedValue + "'"))
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

            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));

            filtrodata = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + ""))
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
                      "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + ""))
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
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / NULLIF(COUNT(*), 0) AS ticket FROM vendas where manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + "and [status] = 'OK'"))
            {
                if (reader.Read())
                {
                    lblTicket.Text = reader["ticket"].ToString();
                }
                else
                    lblTicket.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "SELECT SUM(CAST(value AS DECIMAL(10,2))) / COUNT(DISTINCT client_name) AS receita_media FROM vendas WHERE [status] = 'OK' AND manufacturer_id = '" + ddlFornecedores.SelectedValue + "'" + filtrodata ))
            {
                if (reader.Read())
                {
                    lblReceita.Text = reader["receita_media"].ToString();
                }
                else
                    lblReceita.Text = "0";
            }


            sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + " group by v.client_id    having count(quantity) > 0    order by qtde desc";
            sdsDados.DataBind();

            SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + "    group by v.client_id    having count(quantity) > 0    order by fatura desc";
            SqlDataSource1.DataBind();

            SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource2.DataBind();

            SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' " + filtrodata + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource4.DataBind();
        }

        protected void ddlTopQtdeVendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7 group by v.client_id    having count(quantity) > 0    order by qtde desc";
            sdsDados.DataBind();
        }

        protected void ddlTopFaturamentoLoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.client_id    having count(quantity) > 0    order by fatura desc";
            SqlDataSource1.DataBind();
        }

        protected void ddlTopQtdeVendaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource2.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + ddlFornecedores.SelectedValue + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource4.DataBind();
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
            SqlDataSource5.SelectCommand = "select " + ddlTopVendasCliente.SelectedValue + " client_name as client, count(*) as qtde from vendas where manufacturer_id = '" + ddlFornecedores.SelectedValue + "' group by client_name order by qtde desc";
            SqlDataSource5.DataBind();
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

        protected void rodarEstoque(object sender, EventArgs e)
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
            JavaScriptSerializer serialize2 = new System.Web.Script.Serialization.JavaScriptSerializer();

            string dado = "{\"call\": \"ListarLocaisEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\": 1, \"nRegPorPagina\": 50}]}";
            var cliente = new RestClient("https://app.omie.com.br/api/v1/estoque/local/");
            var requere = new RestRequest(Method.POST);
            requere.AddParameter("application/json", dado, ParameterType.RequestBody);

            IRestResponse responses = cliente.Execute(requere);

            dynamic result = serialize2.DeserializeObject(responses.Content);
            //int totalPag = Convert.ToInt16(result["nTotPaginas"].ToString());

            for (int i = 1; i <= 60; i++)
            {
                try
                {
                    //string codigo_local_estoque = result["locaisEncontrados"]["codigo_local_estoque"].ToString();
                    //string descricao = result["locaisEncontrados"]["descricao"].ToString();

                    string dados = "{\"call\": \"ListarPosEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\":" + i + ", \"nRegPorPagina\": 1000, \"dDataPosicao\": \"" + DateTime.Now.ToShortDateString() + "\", \"cExibeTodos\": \"S\", \"codigo_local_estoque\": \"0\"}]}";

                    var client = new RestClient($"https://app.omie.com.br/api/v1/estoque/consulta/");
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("application/json", dados, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);


                    try
                    {
                        dynamic resultado = serialize.DeserializeObject(response.Content);


                        try
                        {
                            // 6879291650, 6902157478, 6900741284, 6910062042, 6903646981, 6900561856

                            dynamic produtos = resultado["produtos"];

                            foreach (var produto in produtos)
                            {
                                try
                                {
                                    string nSaldo = produto["nSaldo"].ToString();
                                    string cCodigo = produto["cCodigo"].ToString();
                                    string cDescricao = produto["cDescricao"].ToString();

                                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                      "SELECT id, convert(varchar, data_criacao, 103) as data, sald FROM estoque where id = '" + cCodigo + "'"))
                                    {
                                        if (reader.Read())
                                        {
                                            if (reader["data"].ToString() == DateTime.Now.Date.ToString())
                                            {
                                                try
                                                {
                                                    int soma = Convert.ToInt16(nSaldo) + Convert.ToInt16(reader["sald"].ToString());
                                                    DbCommand command3 = db.GetSqlStringCommand(
                                                        "UPDATE estoque SET sald = @sald where id = @id");
                                                    db.AddInParameter(command3, "@id", DbType.Int32, cCodigo);
                                                    db.AddInParameter(command3, "@sald", DbType.Int32, soma);

                                                    db.ExecuteNonQuery(command3);
                                                    lblErros.Text = "Rodou 1";
                                                }
                                                catch (Exception ex)
                                                {
                                                    lblErros.Text = "erro 1: " + ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    DbCommand command4 = db.GetSqlStringCommand(
                                                        "UPDATE estoque SET sald = @sald where id = @id");
                                                    db.AddInParameter(command4, "@id", DbType.Int32, cCodigo);
                                                    db.AddInParameter(command4, "@sald", DbType.Int32, nSaldo);

                                                    db.ExecuteNonQuery(command4);
                                                    lblErros.Text = "Rodou 2";
                                                }
                                                catch (Exception ex)
                                                {
                                                    lblErros.Text = "erro 2: " + ex.Message;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                DbCommand command5 = db.GetSqlStringCommand(
                                                    "INSERT INTO estoque (id, name, type, manufacturer_id, category_id, upc_code, sald, idclient, name_client, data_criacao) VALUES (@id, @name, @type, @manufacturer_id, @category_id, @upc_code, @sald, @idclient, @name_client, getdate())");
                                                db.AddInParameter(command5, "@id", DbType.Int32, 0);
                                                db.AddInParameter(command5, "@name", DbType.String, cDescricao);
                                                db.AddInParameter(command5, "@type", DbType.String, "ProductCD");
                                                db.AddInParameter(command5, "@manufacturer_id", DbType.Int32, 0);
                                                db.AddInParameter(command5, "@category_id", DbType.Int32, 0);
                                                db.AddInParameter(command5, "@upc_code", DbType.String, cCodigo);
                                                db.AddInParameter(command5, "@sald", DbType.Int32, nSaldo);
                                                db.AddInParameter(command5, "@idclient", DbType.Int32, 0);
                                                db.AddInParameter(command5, "@name_client", DbType.Int32, 0);

                                                db.ExecuteNonQuery(command5);
                                                lblErros.Text = "Rodou 3";
                                            }
                                            catch (Exception ex)
                                            {
                                                lblErros.Text = "erro 3: " + ex.Message;
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblErros.Text = "erro 8: " + ex.Message;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblErros.Text = "erro 4: " + ex + " /// Resposta JSON: " + response.Content;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblErros.Text = "erro 6: " + ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    lblErros.Text = "erro 5: " + ex.Message;
                }
            }
        }

        protected void ddlCategoria_DataBound(object sender, EventArgs e)
        {
            ddlCategoria.Items.Insert(0, new ListItem("TODOS", "0"));
            ddlProduto.Items.Insert(0, new ListItem("TODOS", "0"));
        }
    }
}