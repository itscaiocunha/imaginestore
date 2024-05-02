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
                //atualizacao.GETVendas();

                Database db = DatabaseFactory.CreateDatabase("ConnectionString");

                //for (int i = 0; i < 28; i++)
                //{
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/cashless_facts?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI&start_date=30/04/2024&end_date=02/05/2024&page=1&per_page=1000");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "application/json");

                IRestResponse response = client.Execute(request);

                dynamic resultado = serializer.DeserializeObject(response.Content);

                foreach (var info in resultado)
                {
                    string id = info["id"].ToString();
                    string occurred_at = info["occurred_at"].ToString().Replace("T", " ").Replace("Z", "");
                    string point_of_sale = info["point_of_sale"].ToString();
                    string equipment_id = info["equipment_id"].ToString();
                    string installation_id = info["installation_id"].ToString();
                    string planogram_item_id = info["planogram_item_id"].ToString();
                    string equipment_label_number = info["equipment_label_number"].ToString();
                    string equipment_serial_number = info["equipment_serial_number"].ToString();
                    string masked_card_number = "", number_of_payments = "";
                    try
                    {
                        masked_card_number = info["masked_card_number"].ToString();
                    }
                    catch
                    {
                        masked_card_number = "";
                    }
                    try
                    {
                        number_of_payments = info["number_of_payments"].ToString();
                    }
                    catch
                    {
                        number_of_payments = "";
                    }
                    string quantity = info["quantity"].ToString();
                    string value = info["value"].ToString();
                    string request_number = "", issuer_authorization_code = "";
                    try
                    {
                        request_number = info["request_number"].ToString();
                    }
                    catch
                    {
                        request_number = "";
                    }
                    try
                    {
                        issuer_authorization_code = info["issuer_authorization_code"].ToString();
                    }
                    catch
                    {
                        issuer_authorization_code = "";
                    }
                    var cliente = info["client"].ToString();
                    string client_id = info["client"]["id"].ToString();
                    string client_name = info["client"]["name"].ToString();
                    var location = info["location"].ToString();
                    string location_id = info["location"]["id"].ToString();
                    string location_name = info["location"]["name"].ToString();
                    var machine = info["machine"].ToString();
                    string machine_id = info["machine"]["id"].ToString();
                    var machine_model = info["machine_model"]["name"].ToString();
                    string machine_model_id = info["machine_model"]["id"].ToString();
                    string planogram_item = info["planogram_item"].ToString();
                    var good = info["good"].ToString();
                    string good_id = info["good"]["id"].ToString();
                    string type = info["good"]["type"].ToString();
                    string category_id = info["good"]["category_id"].ToString();
                    string manufacturer_id = "", upc_code = "", barcode = "";
                    try
                    {
                        manufacturer_id = info["good"]["manufacturer_id"].ToString();
                    }
                    catch
                    {
                        manufacturer_id = "";
                    }
                    string name = info["good"]["name"].ToString();
                    try
                    {
                        upc_code = info["good"]["upc_code"].ToString();
                    }
                    catch
                    {
                        upc_code = "";
                    }
                    try
                    {
                        barcode = info["good"]["barcode"].ToString();
                    }
                    catch
                    {
                        barcode = "";
                    }

                    string payment_authorizer = "", eft_provider_id = "", eft_authorizer_id = "", eft_card_brand_id = "", eft_card_type_id = "";
                    try
                    {
                        var eft_provider = info["eft_provider"].ToString();
                        eft_provider_id = info["eft_provider"]["name"].ToString();
                        var eft_authorizer = info["eft_authorizer"].ToString();
                        eft_authorizer_id = info["eft_authorizer"]["name"].ToString();
                        var eft_card_brand = info["eft_card_brand"].ToString();
                        eft_card_brand_id = info["eft_card_brand"]["name"].ToString();
                        var eft_card_type = info["eft_card_type"].ToString();
                        eft_card_type_id = info["eft_card_type"]["name"].ToString();
                    }
                    catch
                    {
                        eft_provider_id = "";
                        eft_authorizer_id = "";
                        eft_card_brand_id = "";
                        eft_card_type_id = "";
                    }
                    try
                    {
                        payment_authorizer = info["payment_authorizer"]["name"].ToString();
                    }
                    catch
                    {
                        payment_authorizer = "";
                    }
                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT * from vendas where id = '" + id + "'"))
                    {

                        if (!reader.Read())
                        {
                            //    lblteste.Text = "aqui";
                            DbCommand command3 = db.GetSqlStringCommand(
            "insert into vendas (id, occurred_at, client_id, location_id, machine_id, installation_id, planogram_item_id, good_id, coil, quantity, value, client_name, location_name, machine_model_name, type, " +
            "category_id, manufacturer_id, product_name, upc_code, barcode, point_of_sale, equipment_id, equipment_label_number, equipment_serial_number, masked_card_number, number_of_payments, request_number, " +
            "issuer_authorization_code, machine_model, planogram_item, eft_provider, eft_authorizer, eft_card_brand, eft_card_type, payment_authorizer, data_cricao) values " +
            "(@id, @occurred_at, @client_id, @location_id, @machine_id, @installation_id, @planogram_item_id, @good_id, @coil, @quantity, @value, @client_name, @location_name, @machine_model_name, @type, @" +
            "category_id, @manufacturer_id, @product_name, @upc_code, @barcode, @point_of_sale, @equipment_id, @equipment_label_number, @equipment_serial_number, @masked_card_number, @number_of_payments, @request_number, @" +
            "issuer_authorization_code, @machine_model, @planogram_item, @eft_provider, @eft_authorizer, @eft_card_brand, @eft_card_type, @payment_authorizer, GETDATE())");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@occurred_at", DbType.DateTime, Convert.ToDateTime(occurred_at));
                        db.AddInParameter(command3, "@client_id", DbType.Int32, Convert.ToInt32(client_id));
                        db.AddInParameter(command3, "@location_id", DbType.Int32, Convert.ToInt32(location_id));
                        db.AddInParameter(command3, "@machine_id", DbType.Int32, Convert.ToInt32(machine_id));
                        db.AddInParameter(command3, "@installation_id", DbType.Int32, Convert.ToInt32(installation_id));
                        db.AddInParameter(command3, "@planogram_item_id", DbType.Int32, Convert.ToInt32(planogram_item_id));
                        db.AddInParameter(command3, "@good_id", DbType.Int32, Convert.ToInt32(good_id));
                        db.AddInParameter(command3, "@coil", DbType.String, barcode);
                        db.AddInParameter(command3, "@quantity", DbType.Int16, Convert.ToInt16(quantity));
                        db.AddInParameter(command3, "@value", DbType.Double, Convert.ToDouble(value));
                        db.AddInParameter(command3, "@client_name", DbType.String, client_name);
                        db.AddInParameter(command3, "@location_name", DbType.String, location_name);
                        db.AddInParameter(command3, "@machine_model_name", DbType.String, machine_model_id);
                        db.AddInParameter(command3, "@type", DbType.String, type);
                        db.AddInParameter(command3, "@category_id", DbType.String, category_id);
                        db.AddInParameter(command3, "@manufacturer_id", DbType.String, manufacturer_id);
                        db.AddInParameter(command3, "@product_name", DbType.String, name);
                        db.AddInParameter(command3, "@upc_code", DbType.String, upc_code);
                        db.AddInParameter(command3, "@barcode", DbType.String, barcode);
                        db.AddInParameter(command3, "@point_of_sale", DbType.String, point_of_sale);
                        db.AddInParameter(command3, "@equipment_id", DbType.String, equipment_id);
                        db.AddInParameter(command3, "@equipment_label_number", DbType.String, equipment_label_number);
                        db.AddInParameter(command3, "@equipment_serial_number", DbType.String, equipment_serial_number);
                        db.AddInParameter(command3, "@masked_card_number", DbType.String, masked_card_number);
                        db.AddInParameter(command3, "@number_of_payments", DbType.String, number_of_payments);
                        db.AddInParameter(command3, "@request_number", DbType.String, request_number);
                        db.AddInParameter(command3, "@issuer_authorization_code", DbType.String, issuer_authorization_code);
                        db.AddInParameter(command3, "@machine_model", DbType.String, machine_model);
                        db.AddInParameter(command3, "@planogram_item", DbType.String, planogram_item);
                        db.AddInParameter(command3, "@eft_provider", DbType.String, eft_provider_id);
                        db.AddInParameter(command3, "@eft_authorizer", DbType.String, eft_authorizer_id);
                        db.AddInParameter(command3, "@eft_card_brand", DbType.String, eft_card_brand_id);
                        db.AddInParameter(command3, "@eft_card_type", DbType.String, eft_card_type_id);
                        db.AddInParameter(command3, "@payment_authorizer", DbType.String, payment_authorizer);


                        try
                        {
                            db.ExecuteNonQuery(command3);
                            try
                            {
                                DbCommand command = db.GetSqlStringCommand(
                    "update estoque1 set sald = sald - @sald where idclient = @idclient and idlocation = @idlocation and idmachine = @idmachine and id = @good_id and type = 'Product'");
                                db.AddInParameter(command, "@idclient", DbType.Int32, Convert.ToInt32(client_id));
                                db.AddInParameter(command, "@idlocation", DbType.Int32, Convert.ToInt32(location_id));
                                db.AddInParameter(command, "@idmachine", DbType.Int32, Convert.ToInt32(machine_id));
                                db.AddInParameter(command, "@good_id", DbType.Int32, Convert.ToInt32(good_id));
                                db.AddInParameter(command, "@sald", DbType.Int32, Convert.ToInt32(quantity));

                                db.ExecuteNonQuery(command);

                                lblteste.Text = "ok, inserido";
                            }
                            catch (Exception ex)
                            {
                                //aqui precisa inserir o erro em um arquivo
                                lblteste.Text = "erro" + ex;
                            }
                        }
                        catch (Exception ex)
                        {
                            //aqui precisa inserir o erro em um arquivo
                            lblteste.Text = "erro:" + ex;
                        }
                        }
                    }
                }
            }
        }
    }
}