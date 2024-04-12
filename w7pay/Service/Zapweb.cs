using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using w7pay.Model.FPIX;

namespace w7pay.Service
{
    public class Zapweb
    {
        public static dynamic GerarToken(Whatsapp dados)
        {
            string BASEURL = "https://www.zapweb.app.br/usuario/entrar";
            var client = new RestClient($"{BASEURL}");

            var request = new RestRequest(Method.POST);
            string env = dados.toCreateToken();
            request.AddParameter(
                "application/json",
                env,
                ParameterType.RequestBody);

            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            //if (response.StatusCode != HttpStatusCode.Created)
                //throw new Exception($"{response.StatusCode} - {response.StatusDescription}");

            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public static dynamic EnviarMensagem(Whatsapp dados)
        {
            dynamic token = Zapweb.GerarToken(dados);
            var status = token["status"];
            if (status == "sucesso")
            {
                var responsavelId = token["data"]["autorizacao"]["responsavelId"];
                var tk = token["data"]["autorizacao"]["token"];

                string BASEURL = "https://www.zapweb.app.br/mensagem/texto";
                var client = new RestClient($"{BASEURL}");
                var request = new RestRequest(Method.POST);
                string env = dados.toCreateMensagem();
                request.AddParameter(
                    "application/json",
                    env,
                    ParameterType.RequestBody);

                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                //if (response.StatusCode != HttpStatusCode.Created)
                //throw new Exception($"{response.StatusCode} - {response.StatusDescription}");

                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            else
            {
                return JsonConvert.DeserializeObject<dynamic>("Erro");
            }
        }
    }
}