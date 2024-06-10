using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src.parceiro
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
                             "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasPagas.Text = "R$ " + reader["ganho"].ToString();
                            lblTotalVendasRegistradas.Text = reader["qtde"].ToString();
                        }
                        else
                        {
                            lblTotalVendasPagas.Text = "R$ 0,00";
                            lblTotalVendasRegistradas.Text = "0";
                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalNaoPagas.Text = reader["qtdelojas"].ToString();
                        }
                        else
                            lblTotalNaoPagas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "select count(*) as qtde from vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and masked_card_number <> '' group by masked_card_number"))
                    {
                        if (reader.Read())
                        {
                            lblTotalMensagens.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalMensagens.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    lblMensagemBoasVindas.Text = ex.Message;
                }
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            string filtrodata = "";

            DateTime datainicio, datafim;
            if (DateTime.TryParseExact(txtDataInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datainicio) &&
                DateTime.TryParseExact(txtDataFim.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datafim))
            {
                filtrodata = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnecetionString").ExecuteReader(CommandType.Text,
                    "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' " + filtrodata + ""))
                {
                    if (reader.Read())
                    {
                        lblTotalVendasPagas.Text = "R$ " + reader["ganho"].ToString();
                        lblTotalVendasRegistradas.Text = reader["qtde"].ToString();
                    }
                    else
                    {
                        lblTotalVendasPagas.Text = "R$ 0,00";
                        lblTotalVendasRegistradas.Text = "0";
                    }
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "' " + filtrodata + ""))
                {
                    if (reader.Read())
                    {
                        lblTotalNaoPagas.Text = reader["qtdelojas"].ToString();
                    }
                    else
                        lblTotalNaoPagas.Text = "0";
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "select count(*) as qtde from vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "' " + filtrodata + " and masked_card_number <> '' group by masked_card_number"))
                {
                    if (reader.Read())
                    {
                        lblTotalMensagens.Text = reader["qtde"].ToString();
                    }
                    else
                        lblTotalMensagens.Text = "0";
                }
                //}

                sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + " group by v.client_id    having count(quantity) > 0    order by qtde desc";
                sdsDados.DataBind();

                SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + "    group by v.client_id    having count(quantity) > 0    order by fatura desc";
                SqlDataSource1.DataBind();

                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();

                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource4.DataBind();
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
                    lblTotalVendasPagas.Text = "R$ " + reader["ganho"].ToString();
                    lblTotalVendasRegistradas.Text = reader["qtde"].ToString();
                }
                else
                {
                    lblTotalVendasPagas.Text = "R$ 0,00";
                    lblTotalVendasRegistradas.Text = "0";
                }
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(distinct machine_id) as qtdelojas FROM vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
            {
                if (reader.Read())
                {
                    lblTotalNaoPagas.Text = reader["qtdelojas"].ToString();
                }
                else
                    lblTotalNaoPagas.Text = "0";
            }

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                      "select count(*) as qtde from vendas v where occurred_at > getDate() - 7 and v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and masked_card_number <> '' group by masked_card_number"))
            {
                if (reader.Read())
                {
                    lblTotalMensagens.Text = reader["qtde"].ToString();
                }
                else
                    lblTotalMensagens.Text = "0";
            }

            sdsDados.SelectCommand = "select " + ddlTopQtdeVendas.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7 group by v.client_id    having count(quantity) > 0    order by qtde desc";
            sdsDados.DataBind();

            SqlDataSource1.SelectCommand = "select " + ddlTopFaturamentoLoja.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7    group by v.client_id    having count(quantity) > 0    order by fatura desc";
            SqlDataSource1.DataBind();

            SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource2.DataBind();

            SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource4.DataBind();
        }

        protected void ddlTopQtdeVendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            sdsDados.SelectCommand = "select "+ddlTopQtdeVendas.SelectedValue+" count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock) where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7 group by v.client_id    having count(quantity) > 0    order by qtde desc";
            sdsDados.DataBind();
        }

        protected void ddlTopFaturamentoLoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "select "+ddlTopFaturamentoLoja.SelectedValue+" count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.client_name) as nomecliente from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7    group by v.client_id    having count(quantity) > 0    order by fatura desc";
            SqlDataSource1.DataBind();
        }

        protected void ddlTopQtdeVendaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource2.SelectCommand = "select "+ddlTopQtdeVendaProduto.SelectedValue+" count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource2.DataBind();
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            string id = e.PostBackValue.ToString();
            string filtrodata = "", filtro_name = "";

            DateTime datainicio, datafim;
            if (DateTime.TryParseExact(txtDataInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datainicio) &&
                DateTime.TryParseExact(txtDataFim.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datafim))
            {
                filtrodata = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";                
                filtro_name = " and client_name = '" + id + "'";

                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + filtro_name +"    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();

                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + filtro_name +"    group by v.good_id    having count(quantity) > 0    order by fatura desc";
                SqlDataSource4.DataBind();
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and occurred_at > getdate() - 7    group by v.good_id    having count(quantity) > 0    order by qtde desc";
            SqlDataSource4.DataBind();
        }

        protected void Chart2_Click(object sender, ImageMapEventArgs e)
        {
            string id = e.PostBackValue.ToString();
            string filtrodata = "", filtro_name = "";

            DateTime datainicio, datafim;
            if (DateTime.TryParseExact(txtDataInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datainicio) &&
                DateTime.TryParseExact(txtDataFim.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datafim))
            {
                filtrodata = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";                
                filtro_name = " and client_name = '" + id + "'";

                SqlDataSource2.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + filtro_name + "    group by v.good_id    having count(quantity) > 0    order by qtde desc";
                SqlDataSource2.DataBind();

                SqlDataSource4.SelectCommand = "select " + ddlTopQtdeVendaProduto.SelectedValue + " count(quantity) as qtde, cast(sum(value) as decimal(10,2)) as fatura, max(v.product_name) as nomeproduto from vendas v (nolock)    where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' and " + filtrodata + filtro_name + "    group by v.good_id    having count(quantity) > 0    order by fatura desc";
                SqlDataSource4.DataBind();
            }
        }
    }
}