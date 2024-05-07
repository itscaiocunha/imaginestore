using iTextSharp.text.pdf;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using RestSharp;
using Sdk.BankingApi;
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

namespace w7pay.src.parceiro
{
    public partial class vendas2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    hdfIdEmpresa.Value = Session["idempresa"].ToString();

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
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }

                //atualizacao.GETVendas();
            }        
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            string filtrodata = "", filtroid = "";
            DateTime datainicio = Convert.ToDateTime(txtDataInicio.Text.Substring(3, 2) + "/" + txtDataInicio.Text.Substring(0, 2) + "/" + txtDataInicio.Text.Substring(6, 4));
            DateTime datafim = Convert.ToDateTime(txtDataFim.Text.Substring(3, 2) + "/" + txtDataFim.Text.Substring(0, 2) + "/" + txtDataFim.Text.Substring(6, 4));
            filtrodata = " and occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "' ";

            if (txtBuscar.Text != "")
                filtroid = " and v.id = '"+txtBuscar.Text+"'";

            sdsDados.SelectCommand = "select v.id, (f.name) as name, (v.location_name) as location_name, (v.client_name) as client_name, (v.machine_model_name) as machine_model_name ,  (ct.descricao) as descricao, (v.product_name) as product_name, (coil) as coil, (quantity) as quantity, (value) as value , convert(varchar,DATEPART(day,(occurred_at)))+'/'+convert(varchar,DATEPART(month,(occurred_at)))+'/'+convert(varchar,DATEPART(year,(occurred_at)))+ ' '+ convert(varchar,(occurred_at),108) as occurred_at from vendas v (nolock) join fornecedores f on f.id = v.manufacturer_id join categorias ct on ct.id = v.category_id where v.manufacturer_id = @id "+filtrodata+" "+filtroid+" order by occurred_at desc";
            sdsDados.DataBind();

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT isnull(SUM(try_cast(v.value as decimal(10,2))),0) as ganho, COUNT(*) as qtde FROM vendas v where v.manufacturer_id = '" + hdfIdEmpresa.Value + "' "+filtrodata+" "+filtroid+""))
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

        protected void lkbLimpar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

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

            sdsDados.SelectCommand = "select v.id, (f.name) as name, (v.location_name) as location_name, (v.client_name) as client_name, (v.machine_model_name) as machine_model_name ,  (ct.descricao) as descricao, (v.product_name) as product_name, (coil) as coil, (quantity) as quantity, (value) as value , convert(varchar,DATEPART(day,(occurred_at)))+'/'+convert(varchar,DATEPART(month,(occurred_at)))+'/'+convert(varchar,DATEPART(year,(occurred_at)))+ ' '+ convert(varchar,(occurred_at),108) as occurred_at from vendas v (nolock) join fornecedores f on f.id = v.manufacturer_id join categorias ct on ct.id = v.category_id where v.manufacturer_id = @id order by occurred_at desc";
            sdsDados.DataBind();
        }
    }
}