using Microsoft.Practices.EnterpriseLibrary.Data;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Reflection.PortableExecutable;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using DocumentFormat.OpenXml.Drawing.Charts;
using w7pay.src;
using iTextSharp.text.pdf;

namespace w7pay
{
    internal class atualizacao
    {
        public static void GETCategoria()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/categories?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            dynamic resultado = serializer.DeserializeObject(response.Content);

            foreach (var info in resultado)
            {
                string id = info["id"].ToString();
                string descricao = info["name"].ToString();

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT * from categorias where id = '" + id + "'"))
                {
                    if (reader.Read())
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "UPDATE categorias SET descricao = @descricao where id = @id");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@descricao", DbType.String, descricao);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                    else
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "insert into categorias (id, descricao) values (@id, @descricao)");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@descricao", DbType.String, descricao);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                }
            }
        }

        public static void GETLocais()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/locations?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            dynamic resultado = serializer.DeserializeObject(response.Content);

            foreach (var info in resultado)
            {
                string id = info["id"].ToString();
                string client_id = info["client_id"].ToString();
                string name = info["name"].ToString();
                string phone = info["phone"].ToString();
                string street = info["street"].ToString();
                string number = info["number"].ToString();
                string complement = info["complement"].ToString();
                string neighborhood = info["neighborhood"].ToString();
                string city = info["city"].ToString();
                string country = info["country"].ToString();
                string state = info["state"].ToString();
                string zip_code = info["zip_code"].ToString();

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT * from locais where id = '" + id + "'"))
                {
                    if (reader.Read())
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "UPDATE locais SET client_id = @client_id, name = @name, phone = @phone, street = @street, number = @number, complement = @complement, neighborhood = @neighborhood, city = @city, country = @country, state = @state,  zip_code = @zip_code where id = @id");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@client_id", DbType.String, client_id);
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        db.AddInParameter(command3, "@phone", DbType.String, phone);
                        db.AddInParameter(command3, "@street", DbType.String, street);
                        db.AddInParameter(command3, "@number", DbType.String, number);
                        db.AddInParameter(command3, "@complement", DbType.String, complement);
                        db.AddInParameter(command3, "@neighborhood", DbType.String, neighborhood);
                        db.AddInParameter(command3, "@city", DbType.String, city);
                        db.AddInParameter(command3, "@country", DbType.String, country);
                        db.AddInParameter(command3, "@state", DbType.String, state);
                        db.AddInParameter(command3, "@zip_code", DbType.String, zip_code);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                    else
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "insert into locais (id, client_id, name, phone, street, number, complement, neighborhood, city, country, state, zip_code) values (@id, @client_id, @name, @phone, @street, @number, @complement, @neighborhood, @city, @country, @state, @zip_code)");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@client_id", DbType.String, client_id);
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        db.AddInParameter(command3, "@phone", DbType.String, phone);
                        db.AddInParameter(command3, "@street", DbType.String, street);
                        db.AddInParameter(command3, "@number", DbType.String, number);
                        db.AddInParameter(command3, "@complement", DbType.String, complement);
                        db.AddInParameter(command3, "@neighborhood", DbType.String, neighborhood);
                        db.AddInParameter(command3, "@city", DbType.String, city);
                        db.AddInParameter(command3, "@country", DbType.String, country);
                        db.AddInParameter(command3, "@state", DbType.String, state);
                        db.AddInParameter(command3, "@zip_code", DbType.String, zip_code);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                }
            }
        }

        public static void GETMaquinas()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/machines?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            dynamic resultado = serializer.DeserializeObject(response.Content);

            foreach (var info in resultado)
            {
                string id = info["id"].ToString();
                string asset_number = info["asset_number"].ToString();
                string distribution_center_id = info["distribution_center_id"].ToString();
                string machine_model_id = info["machine_model_id"].ToString();

                //string location_id = info["installation"]["location_id"].ToString();
                //string installation_id = info["installation"]["id"].ToString();
                //string machine_id = info["installation"]["machine_id"].ToString();
                //string equipment_id = info["installation"]["equipment_id"].ToString();
                //string place = info["installation"]["place"].ToString();
                //string cash_mode = info["installation"]["cash_mode"].ToString();
                //string restock_mode = info["installation"]["restock_mode"].ToString();
                //string restock_strategy = info["installation"]["restock_strategy"].ToString();

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT * from maquinas where id = '" + id + "'"))
                {
                    if (reader.Read())
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "UPDATE maquinas SET asset_number = @asset_number, distribution_center_id = @distribution_center_id, machine_model_id = @machine_model_id where id = @id");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@asset_number", DbType.String, asset_number);
                        db.AddInParameter(command3, "@distribution_center_id", DbType.Int32, distribution_center_id);
                        db.AddInParameter(command3, "@machine_model_id", DbType.Int32, machine_model_id);
                        //db.AddInParameter(command3, "@installation_id", DbType.Int32, installation_id);
                        //db.AddInParameter(command3, "@location_id", DbType.Int32, location_id);
                        //db.AddInParameter(command3, "@machine_id", DbType.Int32, machine_id);
                        //db.AddInParameter(command3, "@equipment_id", DbType.Int32, equipment_id);
                        //db.AddInParameter(command3, "@place", DbType.String, place);
                        //db.AddInParameter(command3, "@cash_mode", DbType.String, cash_mode);
                        //db.AddInParameter(command3, "@restock_mode", DbType.String, restock_mode);
                        //db.AddInParameter(command3, "@restock_strategy", DbType.String, restock_strategy);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                    else
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "insert into maquinas (id, asset_number, distribution_center_id, machine_model_id) values (@id, @asset_number, @distribution_center_id, @machine_model_id)");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@asset_number", DbType.String, asset_number);
                        db.AddInParameter(command3, "@distribution_center_id", DbType.Int32, distribution_center_id);
                        db.AddInParameter(command3, "@machine_model_id", DbType.Int32, machine_model_id);
                        //db.AddInParameter(command3, "@installation_id", DbType.Int32, installation_id);
                        //db.AddInParameter(command3, "@location_id", DbType.Int32, location_id);
                        //db.AddInParameter(command3, "@machine_id", DbType.Int32, machine_id);
                        //db.AddInParameter(command3, "@equipment_id", DbType.Int32, equipment_id);
                        //db.AddInParameter(command3, "@place", DbType.String, place);
                        //db.AddInParameter(command3, "@cash_mode", DbType.String, cash_mode);
                        //db.AddInParameter(command3, "@restock_mode", DbType.String, restock_mode);
                        //db.AddInParameter(command3, "@restock_strategy", DbType.String, restock_strategy);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                }
            }
        }

        public static void GETClientes()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/clients?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            dynamic resultado = serializer.DeserializeObject(response.Content);

            foreach (var info in resultado)
            {
                string id = info["id"].ToString();
                string name = info["name"].ToString();
                string corporate_name = info["corporate_name"].ToString();
                string cpf = "";
                string cnpj = info["cnpj"].ToString();
                string nif = "";
                string contact_name = info["contact_name"].ToString();
                string contact_phone = info["contact_phone"].ToString();
                string contact_email = info["contact_email"].ToString();
                string notes = info["notes"].ToString();
                string legal_type = info["legal_type"].ToString();
                string main_location_id = info["main_location_id"].ToString();

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT * from clientes where id = '" + id + "'"))
                {
                    if (reader.Read())
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "UPDATE clientes SET name = @name, corporate_name = @corporate_name, cpf = @cpf, cnpj = @cnpj, nif = @nif, contact_name = @contact_name, contact_phone = @contact_phone, contact_email = @contact_email, notes = @notes, legal_type = @legal_type, main_location_id = @main_location_id  where id = @id");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        db.AddInParameter(command3, "@corporate_name", DbType.String, corporate_name);
                        db.AddInParameter(command3, "@cpf", DbType.String, cpf);
                        db.AddInParameter(command3, "@cnpj", DbType.String, cnpj);
                        db.AddInParameter(command3, "@nif", DbType.String, nif);
                        db.AddInParameter(command3, "@contact_name", DbType.String, contact_name);
                        db.AddInParameter(command3, "@contact_phone", DbType.String, contact_phone);
                        db.AddInParameter(command3, "@contact_email", DbType.String, contact_email);
                        db.AddInParameter(command3, "@notes", DbType.String, notes);
                        db.AddInParameter(command3, "@legal_type", DbType.String, legal_type);
                        db.AddInParameter(command3, "@main_location_id", DbType.String, main_location_id);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                    else
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "insert into clientes (id, name, corporate_name, cpf, cnpj, nif, contact_name, contact_phone, contact_email, notes, legal_type, main_location_id) values (@id, @name, @corporate_name, @cpf, @cnpj, @nif, @contact_name, @contact_phone, @contact_email, @notes, @legal_type, @main_location_id)");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        db.AddInParameter(command3, "@corporate_name", DbType.String, corporate_name);
                        db.AddInParameter(command3, "@cpf", DbType.String, cpf);
                        db.AddInParameter(command3, "@cnpj", DbType.String, cnpj);
                        db.AddInParameter(command3, "@nif", DbType.String, nif);
                        db.AddInParameter(command3, "@contact_name", DbType.String, contact_name);
                        db.AddInParameter(command3, "@contact_phone", DbType.String, contact_phone);
                        db.AddInParameter(command3, "@contact_email", DbType.String, contact_email);
                        db.AddInParameter(command3, "@notes", DbType.String, notes);
                        db.AddInParameter(command3, "@legal_type", DbType.String, legal_type);
                        db.AddInParameter(command3, "@main_location_id", DbType.String, main_location_id);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                }
            }
        }

        public static void GETFornecedores()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/manufacturers?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            dynamic resultado = serializer.DeserializeObject(response.Content);

            foreach (var info in resultado)
            {
                string id = info["id"].ToString();
                string name = info["name"].ToString();

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT * from fornecedores where id = '" + id + "'"))
                {
                    if (reader.Read())
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "UPDATE fornecedores SET name = @name where id = @id");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                    else
                    {
                        DbCommand command3 = db.GetSqlStringCommand(
                        "insert into fornecedores (id, name) values (@id, @name)");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                }
            }
        }
            
        public static void GETVendas()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            //for (int i = 0; i < 28; i++)
            //{
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/cashless_facts?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI&start_date=2024-06-29&end_date=2024-07-03&page=1&per_page=1000");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);

            dynamic resultado = serializer.DeserializeObject(response.Content);

            foreach (var info in resultado)
            {
                string id = info["id"].ToString();
                string status = info["status"].ToString();
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
                        DbCommand command3 = db.GetSqlStringCommand(
        "insert into vendas (id, occurred_at, client_id, location_id, machine_id, installation_id, planogram_item_id, good_id, coil, quantity, value, client_name, location_name, machine_model_name, type, " +
        "category_id, manufacturer_id, product_name, upc_code, barcode, point_of_sale, equipment_id, equipment_label_number, equipment_serial_number, masked_card_number, number_of_payments, request_number, " +
        "issuer_authorization_code, machine_model, planogram_item, eft_provider, eft_authorizer, eft_card_brand, eft_card_type, payment_authorizer, status, data_criacao) values " +
        "(@id, @occurred_at, @client_id, @location_id, @machine_id, @installation_id, @planogram_item_id, @good_id, @coil, @quantity, @value, @client_name, @location_name, @machine_model_name, @type, @" +
        "category_id, @manufacturer_id, @product_name, @upc_code, @barcode, @point_of_sale, @equipment_id, @equipment_label_number, @equipment_serial_number, @masked_card_number, @number_of_payments, @request_number, @" +
        "issuer_authorization_code, @machine_model, @planogram_item, @eft_provider, @eft_authorizer, @eft_card_brand, @eft_card_type, @payment_authorizer, @status, GETDATE())");
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
                        db.AddInParameter(command3, "@status", DbType.String, status);


                        try
                        {
                            db.ExecuteNonQuery(command3);
                            if (status != "CANCEL") {
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

                                }
                                catch (Exception ex)
                                {
                                    //aqui precisa inserir o erro em um arquivo
                                }
                            } 
                        }
                        catch (Exception ex)
                        {
                            //aqui precisa inserir o erro em um arquivo
                        }
                    }
                }
            }
        }

        public class ListaSaldo
        {
            public string distribution_center_id { get; set; }
            public string total_quantity { get; set; }
            public string committed_quantity { get; set; }
        }

        public static void GETEstoque()
        {
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
                                }
                                catch (Exception ex)
                                {
                                    string erro = ex.Message;
                                }
                            }
                            catch (Exception ex)
                            {
                                string erro = ex.Message;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string erro = ex.Message;
                    }
                }
            }
        }

        public static void GETEstoquesCD()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();

            string dado = "{\"call\": \"ListarLocaisEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\": 1, \"nRegPorPagina\": 50}]}";
            var cliente = new RestClient("https://app.omie.com.br/api/v1/estoque/local/");
            var requere = new RestRequest(Method.POST);
            requere.AddParameter("application/json", dado, ParameterType.RequestBody);

            IRestResponse responses = cliente.Execute(requere);

            dynamic result = serialize.DeserializeObject(responses.Content);

            int ntotalpaginas = Convert.ToInt16(result["nTotPaginas"].ToString());

            for (int i = 1; i <= ntotalpaginas; i++)
            {
                try
                {
                    string codigo_local_estoque = result["locaisEncontrados"]["codigo_local_estoque"].ToString();
                    string descricao = result["locaisEncontrados"]["descricao"].ToString();

                    string dados = "{\"call\": \"ListarPosEstoque\", \"app_key\": \"2985236014761\", \"app_secret\": \"fae7916a76427bddc6488208cf7f45d4\", \"param\": [{\"nPagina\":" + i + ", \"nRegPorPagina\": 1000, \"dDataPosicao\": \"" + DateTime.Now.Date + "\", \"cExibeTodos\": \"S\", \"codigo_local_estoque\": \"0\"}]}";

                    var client = new RestClient($"https://app.omie.com.br/api/v1/estoque/consulta/");
                    var request = new RestRequest(Method.POST);
                    request.AddParameter("application/json", dados, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    dynamic resultado = serialize.DeserializeObject(response.Content);


                    try
                    {
                        // 6879291650, 6902157478, 6900741284, 6910062042, 6903646981, 6900561856

                        dynamic produtos = resultado["produtos"];

                        foreach (var produto in produtos)
                        {
                            string nSaldo = produto["nSaldo"].ToString();
                            string cCodigo = produto["cCodigo"].ToString();
                            string cDescricao = produto["cDescricao"].ToString();

                            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                              "SELECT id, convert(varchar, data_criacao, 103) as data, sald FROM estoque where id = '" + cCodigo + "'"))
                            {
                                if (reader.Read())
                                {
                                    if (reader["data"].ToString() == DateTime.Now.Date.ToString())
                                    {
                                        try
                                        {
                                            int soma = Convert.ToInt16(nSaldo) + Convert.ToInt16(reader["sald"].ToString());
                                            DbCommand command3 = db.GetSqlStringCommand(
                                                "UPDATE estoque SET sald = @sald where id = @id");
                                            db.AddInParameter(command3, "@id", DbType.Int32, cCodigo);
                                            db.AddInParameter(command3, "@sald", DbType.Int32, soma);

                                            db.ExecuteNonQuery(command3);
                                        }
                                        catch (Exception ex)
                                        {
                                            string erro = ex.Message;
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            DbCommand command4 = db.GetSqlStringCommand(
                                                "UPDATE estoque SET sald = @sald where id = @id");
                                            db.AddInParameter(command4, "@id", DbType.Int32, cCodigo);
                                            db.AddInParameter(command4, "@sald", DbType.Int32, nSaldo);

                                            db.ExecuteNonQuery(command4);
                                        }
                                        catch (Exception ex)
                                        {
                                            string erro = ex.Message;
                                        }

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        DbCommand command5 = db.GetSqlStringCommand(
                                            "INSERT INTO estoque (id, name, type, manufacturer_id, category_id, upc_code, sald, data_criacao) VALUES (@id, @name, @type, @manufacturer_id, @category_id, @upc_code, @sald, getdate())");
                                        db.AddInParameter(command5, "@id", DbType.Int32, 0);
                                        db.AddInParameter(command5, "@name", DbType.String, cDescricao);
                                        db.AddInParameter(command5, "@type", DbType.String, "ProductCD");
                                        db.AddInParameter(command5, "@manufacturer_id", DbType.Int32, 0);
                                        db.AddInParameter(command5, "@category_id", DbType.Int32, 0);
                                        db.AddInParameter(command5, "@upc_code", DbType.String, cCodigo);
                                        db.AddInParameter(command5, "@sald", DbType.Int32, nSaldo);

                                        db.ExecuteNonQuery(command5);
                                    }
                                    catch (Exception ex)
                                    {
                                        string erro = ex.Message;
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string erro = ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                }
            }
        }

        public static void GETProdutos()
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                          "SELECT distinct v.good_id, coil from  vendas v left join produtos p on p.id = v.good_id where p.id is null"))
            {
                if (reader.Read())
                {
                    var client = new RestClient($"https://vmpay.vertitecnologia.com.br/api/v1/products?access_token=04PJ5nF3VnLIfNLJRbqmZkEMhU2VNCClOjPoTPCI&barcode=" + reader["coil"].ToString() + "");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Accept", "application/json");

                    IRestResponse response = client.Execute(request);

                    dynamic resultado = serializer.DeserializeObject(response.Content);

                    foreach (var info in resultado)
                    {
                        string id = info["id"].ToString();
                        string name = info["name"].ToString();
                        string tipe = info["type"].ToString();
                        string manufacturer_id = info["manufacturer_id"].ToString();
                        string category_id = info["category_id"].ToString();
                        string upc_code = info["upc_code"].ToString();
                        string barcode = "";
                        try
                        {
                            barcode = info["barcode"].ToString();
                        }
                        catch
                        {
                            barcode = "";
                        }
                        string cost_price = "0";
                        try
                        {
                            cost_price = info["default_price"].ToString();
                        }
                        catch
                        {
                            cost_price = "0";
                        }
                        string image = "";
                        try
                        {
                            image = info["image"].ToString();
                        }
                        catch
                        {
                            image = "";
                        }

                        //    DbCommand command3 = db.GetSqlStringCommand(
                        //    "UPDATE produtos SET name = @name, type = @type, manufacturer_id = @manufacturer_id, category_id = @category_id, upc_code = @upc_code, barcode = @barcode, default_price = @default_price, image = @image, update_date = getdate()  where id = @id");
                        //    db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        //    db.AddInParameter(command3, "@name", DbType.String, name);
                        //    db.AddInParameter(command3, "@type", DbType.String, tipe);
                        //    db.AddInParameter(command3, "@manufacturer_id", DbType.Int32, Convert.ToInt32(manufacturer_id));
                        //    db.AddInParameter(command3, "@category_id", DbType.Int32, Convert.ToInt32(category_id));
                        //    db.AddInParameter(command3, "@upc_code", DbType.String, upc_code);
                        //    db.AddInParameter(command3, "@barcode", DbType.String, barcode);
                        //    db.AddInParameter(command3, "@default_price", DbType.Double, Convert.ToDouble(cost_price));
                        //    db.AddInParameter(command3, "@image", DbType.String, image);
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

                        DbCommand command3 = db.GetSqlStringCommand(
                        "insert into produtos (id, name, type, manufacturer_id, category_id, upc_code, barcode, default_price, image, create_date) values (@id, @name, @type, @manufacturer_id, @category_id, @upc_code, @barcode, @default_price, @image, getdate())");
                        db.AddInParameter(command3, "@id", DbType.Int32, Convert.ToInt32(id));
                        db.AddInParameter(command3, "@name", DbType.String, name);
                        db.AddInParameter(command3, "@type", DbType.String, tipe);
                        db.AddInParameter(command3, "@manufacturer_id", DbType.Int32, Convert.ToInt32(manufacturer_id));
                        db.AddInParameter(command3, "@category_id", DbType.Int32, Convert.ToInt32(category_id));
                        db.AddInParameter(command3, "@upc_code", DbType.String, upc_code);
                        db.AddInParameter(command3, "@barcode", DbType.String, barcode);
                        db.AddInParameter(command3, "@default_price", DbType.Double, Convert.ToDouble(cost_price));
                        db.AddInParameter(command3, "@image", DbType.String, image);
                        try
                        {
                            db.ExecuteNonQuery(command3);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;
                        }
                    }
                }
            }
        }
    }
}