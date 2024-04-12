using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
                //try { 
                //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                //lblMensagemBoasVindas.Text = "Bem-vindo, " + Session["nomeusuario"].ToString();

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(*) as qtde from vendas"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasRegistradas.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalVendasRegistradas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT cast(sum(value) as decimal(10,2)) as ganho, count(*) as qtde from vendas where occurred_at > getDate() - 2"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendas7dias.Text = reader["qtde"].ToString() + " | R$ " + reader["ganho"].ToString();
                        }
                        else
                            lblTotalVendasRegistradas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT cast(sum(value) as decimal(10,2)) as ganho, count(*) as qtde from vendas where occurred_at > getDate() - 30"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendas30dias.Text = reader["qtde"].ToString() + " | R$ " + reader["ganho"].ToString();
                        }
                        else
                            lblTotalVendas30dias.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT sum(value) as ganho, count(*) as qtde from vendas"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasPagas.Text = "R$ " + reader["ganho"].ToString();
                        }
                        else
                            lblTotalVendasPagas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(*) as qtde from produtos"))
                    {
                        if (reader.Read())
                        {
                            lblTotalNaoPagas.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalNaoPagas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(*) as qtde from fornecedores"))
                    {
                        if (reader.Read())
                        {
                            lblTotalMensagens.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalMensagens.Text = "0";
                    }
                //}
                //catch
                //{
                //    Response.Redirect("../sessao.aspx", false);
                //}
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }
    }
}