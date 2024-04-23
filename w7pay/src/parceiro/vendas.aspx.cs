using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using RestSharp;
using Sdk.BankingApi;
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
    public partial class vendas2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }
            }
             
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();

            string dado = "{\"call\": \"ListarLocaisEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\": 1, \"nRegPorPagina\": 50}]}";
            var cliente = new RestClient("https://app.omie.com.br/api/v1/estoque/local/");
            var requere = new RestRequest(Method.POST);
            requere.AddParameter("application/json", dado, ParameterType.RequestBody);

            IRestResponse responses = cliente.Execute(requere);

            dynamic result = serialize.DeserializeObject(responses.Content);
            int qtde = 47;           

            try
            {
                for (int i = 0; i < qtde; i++)
                {
                    try
                    {
                        string codigo_local_estoque = result["locaisEncontrados"][i]["codigo_local_estoque"].ToString();
                        string descricao = result["locaisEncontrados"][i]["descricao"].ToString();

                        string dados = "{\"call\": \"ListarPosEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\": 3, \"nRegPorPagina\": 1000, \"dDataPosicao\": \"22/04/2024\", \"cExibeTodos\": \"S\", \"codigo_local_estoque\": \"" + codigo_local_estoque + "\"}]}";

                        var client = new RestClient($"https://app.omie.com.br/api/v1/estoque/consulta/");
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("application/json", dados, ParameterType.RequestBody);

                        IRestResponse response = client.Execute(request);

                        dynamic resultado = serialize.DeserializeObject(response.Content);

                        try
                        {
                            dynamic produtos = resultado["produtos"];

                            foreach (var produto in produtos)
                            {
                                string nSaldo = produto["nSaldo"].ToString();
                                string cCodigo = produto["cCodigo"].ToString();
                                string cDescricao = produto["cDescricao"].ToString();
                                {
                                    try
                                    {
                                        DbCommand command3 = db.GetSqlStringCommand(
                                        "INSERT INTO estoque (id, name, type, manufacturer_id, category_id, upc_code, sald, idclient, name_client) values (@id, @name, @type, @manufacturer_id, @category_id, @upc_code, @sald, @idclient, @name_client)");
                                        db.AddInParameter(command3, "@id", DbType.Int32, 0);
                                        db.AddInParameter(command3, "@name", DbType.String, cDescricao);
                                        db.AddInParameter(command3, "@type", DbType.String, "ProductCD");
                                        db.AddInParameter(command3, "@manufacturer_id", DbType.Int32, 0);
                                        db.AddInParameter(command3, "@category_id", DbType.Int32, 0);
                                        db.AddInParameter(command3, "@upc_code", DbType.String, cCodigo);
                                        db.AddInParameter(command3, "@sald", DbType.Int32, nSaldo);
                                        db.AddInParameter(command3, "@idclient", DbType.String, codigo_local_estoque);
                                        db.AddInParameter(command3, "@name_client", DbType.String, descricao);

                                        db.ExecuteNonQuery(command3);
                                        lblteste.Text = "ok, inserido";
                                    }
                                    catch (Exception ex)
                                    {
                                        lblteste.Text = "Erro Leitura: " + ex.Message;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblteste.Text = "Erro Leitura: " + ex.Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblteste.Text = "Erro: " + ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                lblteste.Text = "Erro: " + ex.Message;
            }
        }
    }
}