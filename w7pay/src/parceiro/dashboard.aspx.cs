using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
                try { 
                hdfIdEmpresa.Value = Session["idempresa"].ToString();
                lblMensagemBoasVindas.Text = "Bem-vindo, " + Session["nomeusuario"].ToString();

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "SELECT (SELECT COUNT(*) FROM vendas) + (SELECT COUNT(*) FROM VendasMLabs) AS qtde"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasRegistradas.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalVendasRegistradas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT SUM(try_cast(v.value as decimal(10,2))) as ganho, COUNT(*) as qtde FROM vendas v JOIN produtos p ON p.id = v.good_id where occurred_at > getDate() - 7 and p.manufacturer_id = '" + hdfIdEmpresa.Value+"'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendas7dias.Text = reader["qtde"].ToString() + " | R$ " + reader["ganho"].ToString();
                        }
                        else
                            lblTotalVendasRegistradas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT SUM(try_cast(v.value as decimal(10,2))) as ganho, COUNT(*) as qtde FROM vendas v JOIN produtos p ON p.id = v.good_id where occurred_at > getDate() - 30 and  p.manufacturer_id = '" + hdfIdEmpresa.Value+"'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendas30dias.Text = reader["qtde"].ToString() + " | R$ " + reader["ganho"].ToString();
                        }
                        else
                            lblTotalVendas30dias.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "select SUM(try_cast(v.value as decimal(10, 2))) + SUM(try_cast(m.valor as decimal(10, 2))) as ganho from vendas v JOIN VendasMLabs m ON v.barcode = m.Cód_produto and v.manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasPagas.Text = "R$ " + reader["ganho"].ToString();
                        }
                        else
                            lblTotalVendasPagas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT count(*) as qtde from produtos where manufacturer_id  = '"+hdfIdEmpresa.Value+"'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalNaoPagas.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalNaoPagas.Text = "0";
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "select count(*) as qtde from base_dishonest d join produtos p on p.upc_code = d.[CÓDIGO EM BARRAS] where manufacturer_id = '"+hdfIdEmpresa.Value+"'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalMensagens.Text = reader["qtde"].ToString();
                        }
                        else
                            lblTotalMensagens.Text = "0";
                    }
                }
                catch(Exception ex)
                {
                    lblMensagemBoasVindas.Text = ex.Message;
                }
            }
        }
    }
}