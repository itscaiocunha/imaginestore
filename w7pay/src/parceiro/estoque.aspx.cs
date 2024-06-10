using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src.parceiro
{
    public partial class estoque : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    hdfIdEmpresa.Value = Session["idempresa"].ToString();
                    
                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "select sum(sald) as saldo from estoque1 where manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasPagas.Text =  reader["saldo"].ToString();
                        }
                        else
                        {
                            lblTotalVendasPagas.Text = "0";
                        
                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "select sum(sald) as saldo from estoque where manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalVendasRegistradas.Text = reader["saldo"].ToString();
                        }
                        else
                        {
                            lblTotalVendasRegistradas.Text = "0";

                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "select count(distinct name_client) as qtdeloja from estoque1 where manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read())
                        {
                            lblTotalNaoPagas.Text = reader["qtdeloja"].ToString();
                        }
                        else
                            lblTotalNaoPagas.Text = "0";
                    }

                    lblTotalMensagens.Text = (Convert.ToInt16(lblTotalVendasPagas.Text) + Convert.ToInt16(lblTotalVendasRegistradas.Text)).ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }
            }

            //atualizacao.GETEstoque();
        }
    }
}