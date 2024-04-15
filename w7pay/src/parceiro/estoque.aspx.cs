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
                    //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }
            }

            //atualizacao.GETEstoques();
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                     "select distinct id from produtos"))
            {
                while (reader.Read())
                {
                    try
                    {
                        var url = string.Format("https://vmpay.vertitecnologia.com.br/api/v1/installation_stock_balances?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI&good_id=" + reader["id"].ToString());
                        var client = new RestClient(url);
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("Accept", "application/json");

                        IRestResponse response = client.Execute(request);
                        dynamic resultado = serializer.DeserializeObject(response.Content);
                        int qtde = resultado.Length;

                        for (int i = 0; i < qtde; i++)
                        {
                            lblteste.Text = "Entrou";
                            try
                            {
                                string id = resultado[i]["good"]["id"].ToString();
                                string name = resultado[i]["good"]["name"].ToString();
                                string barcode = resultado[i]["good"]["barcode"].ToString();
                                string inventory_balance = resultado[i]["inventory_balance"].ToString();
                                string idclient = resultado[i]["client"]["id"].ToString();
                                string name_client = resultado[i]["client"]["name"].ToString();
                                string idlocation = resultado[i]["location"]["id"].ToString();
                                string name_location = resultado[i]["location"]["name"].ToString();
                                string idmachine = resultado[i]["machine"]["id"].ToString();
                                string name_machine = resultado[i]["machine"]["asset_number"].ToString();

                                try
                                {
                                    DbCommand command3 = db.GetSqlStringCommand(
                                "INSERT INTO estoque (id, name, type, manufacturer_id, category_id, upc_code,  sald, idclient, name_client, idlocation, name_location, idmachine, name_machine) values (@id, @name, @type, @manufacturer_id, @category_id, @upc_code, @sald, @idclient, @name_client, @idlocation, @name_location, @idmachine, @name_machine)");
                                    db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                                    db.AddInParameter(command3, "@name", DbType.String, name);
                                    db.AddInParameter(command3, "@type", DbType.String, "Product");
                                    db.AddInParameter(command3, "@manufacturer_id", DbType.Int32, 0);
                                    db.AddInParameter(command3, "@category_id", DbType.Int32, 0);
                                    db.AddInParameter(command3, "@upc_code", DbType.String, barcode);
                                    db.AddInParameter(command3, "@sald", DbType.Int32, inventory_balance);
                                    db.AddInParameter(command3, "@idclient", DbType.Int32, Convert.ToInt32(idclient));
                                    db.AddInParameter(command3, "@name_client", DbType.String, name_client);
                                    db.AddInParameter(command3, "@idlocation", DbType.Int32, Convert.ToInt32(idlocation));
                                    db.AddInParameter(command3, "@name_location", DbType.String, name_location);
                                    db.AddInParameter(command3, "@idmachine", DbType.Int32, Convert.ToInt32(idmachine));
                                    db.AddInParameter(command3, "@name_machine", DbType.String, name_machine);


                                    db.ExecuteNonQuery(command3);
                                    lblteste.Text = "ok inserido";
                                }
                                catch (Exception ex)
                                {
                                    lblteste.Text = "Erro 1: " + ex.Message;
                                }
                            }
                            catch (Exception ex)
                            {
                                lblteste.Text = "Erro Leitura: " + ex.Message;
                            }

                            ////if (reader["id"].ToString() != "" )
                            //{
                            //    DbCommand command3 = db.GetSqlStringCommand(
                            //"UPDATE estoque SET name = @name, type = @type, manufacturer_id = @manufacturer_id, category_id = @category_id, upc_code = @barcode, sald = @inventory_balance, idclient = @idclient, name_client = @name_client, idlocation = @idlocation, name_location = @name_location, idmachine = @idmachine, name_machine = @name_machine where id = @id");
                            //    db.AddInParameter(command3, "@id", DbType.Int64, Convert.ToInt64(id));
                            //    db.AddInParameter(command3, "@name", DbType.String, name);
                            //    db.AddInParameter(command3, "@type", DbType.String, "Product");
                            //    db.AddInParameter(command3, "@manufacturer_id", DbType.Int64, Convert.ToInt64(reader["manufacturer_id"].ToString()));
                            //    db.AddInParameter(command3, "@category_id", DbType.Int64, Convert.ToInt64(reader["category_id"].ToString()));
                            //    db.AddInParameter(command3, "@barcode", DbType.Int32, Convert.ToInt32(barcode));
                            //    db.AddInParameter(command3, "@inventory_balance", DbType.Int32, Convert.ToInt32(inventory_balance));
                            //    db.AddInParameter(command3, "@idclient", DbType.Int32, Convert.ToInt32(idclient));
                            //    db.AddInParameter(command3, "@name_client", DbType.Int32, Convert.ToInt32(name_client));
                            //    db.AddInParameter(command3, "@idlocation", DbType.Int32, Convert.ToInt32(idlocation));
                            //    db.AddInParameter(command3, "@name_location", DbType.Int32, Convert.ToInt32(name_location));
                            //    db.AddInParameter(command3, "@idmachine", DbType.Int32, Convert.ToInt32(idmachine));
                            //    db.AddInParameter(command3, "@name_machine", DbType.Int32, Convert.ToInt32(name_machine));
                            //    try
                            //    {
                            //        db.ExecuteNonQuery(command3);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        string erro = ex.Message;
                            //    }
                            //}
                            //else
                            //{


                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        lblteste.Text = ex.Message;
                    }
                }
            }

            //JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //string dados = "{'call': 'ListarPosEstoque', 'app_key': '2985236014761', 'app_secret': 'fae7916a76427bddc6488208cf7f45d4', 'param': [{'nPagina': 1, 'nRegPorPagina': 50, 'dDataPosicao': '11/04/2024', 'cExibeTodos': 'S', 'codigo_local_estoque': 0}]}";
            //var client = new RestClient($"https://app.omie.com.br/api/v1/estoque/consulta/");
            //var request = new RestRequest(Method.POST);
            //string env = JsonConvert.SerializeObject(dados);
            //request.AddParameter(
            //    "application/json",
            //    env,
            //    ParameterType.RequestBody);

            //IRestResponse response = client.Execute(request);
            //dynamic resultado = serializer.DeserializeObject(response.Content);
            //lblteste.Text = resultado.ToString();
        }
    }
}