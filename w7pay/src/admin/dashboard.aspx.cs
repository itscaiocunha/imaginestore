using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
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

                    //using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                    //          "SELECT count(*) as qtde from produtos where manufacturer_id  = '" + hdfIdEmpresa.Value + "'"))
                    //{
                    //    if (reader.Read())
                    //    {
                    //        lblTotalNaoPagas.Text = reader["qtde"].ToString();
                    //    }
                    //    else
                    //        lblTotalNaoPagas.Text = "0";
                    //}

                    //using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                    //          "select count(*) as qtde from base_dishonest d join produtos p on p.upc_code = d.[CÓDIGO EM BARRAS] where manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    //{
                    //    if (reader.Read())
                    //    {
                    //        lblTotalMensagens.Text = reader["qtde"].ToString();
                    //    }
                    //    else
                    //        lblTotalMensagens.Text = "0";
                    //}
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

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where " + filtrodata + ""))
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
        }

        protected void rodarVendas(object sender, EventArgs e)
        {
            atualizacao.GETVendas();
        }
        protected void rodarFornecedores(object sender, EventArgs e)
        {
            atualizacao.GETFornecedores();
        }

        protected void rodarProdutos(object sender, EventArgs e)
        {
            atualizacao.GETProdutos();
        }
    }
}