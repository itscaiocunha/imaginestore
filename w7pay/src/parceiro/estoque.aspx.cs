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
                        if (reader.Read() && reader["saldo"] != DBNull.Value)
                        {
                            lblEstoqueLoja.Text =  reader["saldo"].ToString();
                        }
                        else
                        {
                            lblEstoqueLoja.Text = "0";
                        
                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                             "select sum(sald) as saldo from estoque where manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read() && reader["saldo"] != DBNull.Value)
                        {
                            lblEstoqueCD.Text = reader["saldo"].ToString();
                        }
                        else
                        {
                            lblEstoqueCD.Text = "0";

                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "select count(distinct name_client) as qtdeloja from estoque1 where manufacturer_id = '" + hdfIdEmpresa.Value + "'"))
                    {
                        if (reader.Read() && reader["qtdeloja"] != DBNull.Value)
                        {
                            lblTotalNaoPagas.Text = reader["qtdeloja"].ToString();
                        }
                        else
                            lblTotalNaoPagas.Text = "0";
                    }

                    lblTotalMensagens.Text = (Convert.ToInt16(lblEstoqueLoja.Text) + Convert.ToInt16(lblEstoqueCD.Text)).ToString();
                }
                catch (Exception ex)
                {
                    //Response.Redirect("../sessao.aspx", false);
                    lblteste.Text = "Erro: " + ex;
                }
            }

            //atualizacao.GETEstoque();
        }
    }
}