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

            string dados = "{\"call\": \"ListarPosEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\": 3, \"nRegPorPagina\": 1000, \"dDataPosicao\": \"15/04/2024\", \"cExibeTodos\": \"S\", \"codigo_local_estoque\": 0}]}";
            var client = new RestClient($"https://app.omie.com.br/api/v1/estoque/consulta/");
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", dados, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            dynamic resultado = serialize.DeserializeObject(response.Content);
            //int qtde = resultado.Count;

            try
            {
                //for (int i = 0; i < qtde; i++)
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
                            db.AddInParameter(command3, "@type", DbType.String, "Product");
                            db.AddInParameter(command3, "@manufacturer_id", DbType.Int32, 0);
                            db.AddInParameter(command3, "@category_id", DbType.Int32, 0);
                            db.AddInParameter(command3, "@upc_code", DbType.String, cCodigo);
                            db.AddInParameter(command3, "@sald", DbType.Int32, nSaldo);
                            db.AddInParameter(command3, "@idclient", DbType.Int32, 99999);
                            db.AddInParameter(command3, "@name_client", DbType.String, "CD");

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
    }
}