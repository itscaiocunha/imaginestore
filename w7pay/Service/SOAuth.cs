using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using InterAPI.Model;
using InterAPI.Utils;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace InterAPI.Service
{
    public class SOAuth
    {
        public static readonly string PathCert = ConfigurationManager.AppSettings["PathCert"];
        public static readonly string PassCert = ConfigurationManager.AppSettings["PassCert"];

        public static AccessToken GetToken(X509Certificate2 certSign)
        {
            string urlOAuth = "https://cdpj.partners.bancointer.com.br/oauth/v2/token";
            string clientId = "20e2637f-9063-462f-9c07-156794ea1432";
            string clientSecret = "dd1ea4b7-e4fd-4b74-a6c0-ab14285cfff2";
            string scope = "extrato.read boleto-cobranca.read boleto-cobranca.write pagamento-boleto.write pagamento-boleto.read pagamento-darf.write barrascob.write cob.read cob.write cobv.write cobv.read pix.write pix.read webhook.read webhook.write payloadlocation.write payloadlocation.read pagamento-pix.write pagamento-pix.read webhook-banking.write webhook-banking.read";
            string grantType = "client_credentials";

            try
            {
                try
                {
                    //Caminho onde o certificado será salvo localmente no container
                    //Console.WriteLine($"obtendo certificado");
                    string userProfileDirectory = @"c:\home\site\wwwroot\certificado";
                    string localCertPath = Path.Combine(userProfileDirectory, "Certificate.pfx");

                    var certPassword = "0525";
                    X509Certificate2 cert = new X509Certificate2(localCertPath, certPassword);

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    var myModel = new Dictionary<string, string> {
                        { "client_id",clientId },
                        { "client_secret", clientSecret },
                        { "scope", scope },
                        { "grant_type", grantType }
                    };

                    //System.IO.File.AppendAllText(@"c:\home\site\wwwroot\log.txt", $"certificado {cert}");

                    var content = new FormUrlEncodedContent(myModel);

                    var clientHandler = new HttpClientHandler
                    {
                        ClientCertificates = { cert },
                        ClientCertificateOptions = ClientCertificateOption.Manual
                        ,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert2, certChain, policyErrors) => true,
                        SslProtocols = (SslProtocols)(SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.SystemDefault)
                    };

                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        httpClient.DefaultRequestHeaders.ExpectContinue = true;
                        //System.IO.File.AppendAllText(@"c:\home\site\wwwroot\log.txt", $"hnd --");
                        var response = httpClient.PostAsync(urlOAuth, content).Result;

                        response.EnsureSuccessStatusCode();

                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        var token = JsonConvert.DeserializeObject<AccessToken>(jsonString);

                        token.Expire = DateTime.Now.AddSeconds(token.Expires_in);
                        //System.IO.File.AppendAllText(@"c:\home\site\wwwroot\log.txt", $"TK{token}");
                        return token;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro Certificado: {ex.Message}");
                    //System.IO.File.AppendAllText(@"c:\home\site\wwwroot\log.txt", $"API {ex.InnerException.ToString()}");
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                //System.IO.File.AppendAllText(@"c:\home\site\wwwroot\log.txt", $"GERAL {ex.InnerException.ToString()}");
            }

            return null;
        }


    //    public static AccessToken GetToken()
    //    {
    //        string urlOAuth = "https://cdpj.partners.bancointer.com.br/oauth/v2/token";
    //        string clientId = "20e2637f-9063-462f-9c07-156794ea1432";
    //        string clientSecret = "dd1ea4b7-e4fd-4b74-a6c0-ab14285cfff2";
    //        string scope = "extrato.read boleto-cobranca.read boleto-cobranca.write pagamento-boleto.write pagamento-boleto.read pagamento-darf.write barrascob.write cob.read cob.write cobv.write cobv.read pix.write pix.read webhook.read webhook.write payloadlocation.write payloadlocation.read pagamento-pix.write pagamento-pix.read webhook-banking.write webhook-banking.read";
    //        string grantType = "client_credentials";

    //        //BlobClient c = new BlobClient("DefaultEndpointsProtocol=https;AccountName=vendadireta;AccountKey=Ps6oqC2inWSphOWPi5Iog9PZi8yZXo1yPLVi0wkBywdEhkUMh+yMeyedNJ5Pyx/vgmU6GSUBfASHGpP5gCUM6A==;EndpointSuffix=core.windows.net", "vendadireta", "certificado");
    //        //var arq = DownloadBlobToFileAsync(c);

    //        try
    //        {
    //            // Substitua pelos caminhos reais dos seus certificados
    //            //string certPath = @"C:\Users\w7agencia\source\repos\w7pay\w7pay\src\Certificate.pfx";
    //            //string certPath = @"https:\\vendadireta.blob.core.windows.net\certificado\Certificate.pfx";                                
    //            //string certPath = "/src/Certificate.pfx";
    //            //string certPassword = "0525";

    //            //var cert = new X509Certificate2(certPath, certPassword);

    //            string keyVaultUrl = "https://w7payvault.vault.azure.net/";
                                
    //            var client = new SecretClient(new Uri(keyVaultUrl), new ClientSecretCredential("f97f5531-ee5d-432f-b69e-ef8d81a982e6", "2d30599d-6451-4540-b3b3-5c13af83157d", "L8j8Q~V0KSEqbYhMkQsPqf-lujvwiu1OwVyiTcVQ"));
                
    //            string certSecretName = "w7segredo";
    //            var certSecret = client.GetSecret(certSecretName);

    //            byte[] certBytes = Convert.FromBase64String(certSecret.Value.Value);
    //            X509Certificate2 cert;

    //            // PFX password - ajuste conforme necessário
    //            string pfxPassword = "0525";

    //            // Importando o certificado PFX com a senha
    //            using (MemoryStream pfxStream = new MemoryStream(certBytes))
    //            {
    //                cert = new X509Certificate2(pfxStream.ToString(), pfxPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
    //            }

    //            var myModel = new Dictionary<string, string>    {

    //  { "client_id",clientId },

    //  { "client_secret", clientSecret },

    //  { "scope", scope },

    //  { "grant_type", grantType }

    //};
    //            var content = new FormUrlEncodedContent(myModel);
    //            var _clientHandler = new HttpClientHandler();
    //            _clientHandler.ClientCertificates.Add(cert);
    //            _clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

    //            var _client = new HttpClient(new HttpClientHandler
    //            {
    //                ClientCertificateOptions = ClientCertificateOption.Manual,
    //                //SslProtocols = SslProtocols.,
    //                ClientCertificates = { cert }
    //            });

    //            using (HttpResponseMessage response = _client.PostAsync(urlOAuth, content).Result)
    //            {
    //                response.EnsureSuccessStatusCode();
    //                string jsonString = response.Content.ReadAsStringAsync().Result;
    //                var tk = jsonString[0];

    //                AccessToken token = JsonConvert.DeserializeObject<AccessToken>(jsonString);
    //                token.Expire = DateTime.Now.AddSeconds(token.Expires_in);
    //                ConstApi.Token = token;
    //                return token;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Erro: {ex.Message}");
    //        }
    //        return null;
    //    }

        /// <summary>
        ///  Funcao inicializar o certificado na memoria. Primeira funcao que deve ser
        ///  chamada sempre.
        /// </summary>
        /// <param name="certFile">Arquivo certificado</param>
        /// <param name="password">Senha certificado</param>
        /// <returns></returns>
        public static bool SetCertificate(byte[] certFile, string password)
        {
            var x =  new X509Certificate2(certFile, password);
            if(x != null)
            {
                ConstApi.certificate2 = x;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funcao para comecar a usar qualquer parte do projeto. 
        /// Chamar depois de SetCertificado
        /// </summary>
        /// <param name="clientId">cclientId forneceido pelo Inter</param>
        /// <param name="clientSecret">clienteSecret fornecido pelo Inter</param>
        /// <param name="scopes">acessos que o token tera separado por espaco
        /// extrato.read - Consulta de Extrato e Saldo
        /// boleto-cobranca.read - Consulta de boletos e exportação para PDF
        /// boleto-cobranca.write - Emissão e cancelamento de boletos
        /// pagamento-boleto.write - Pagamento de titulo com código de barras
        /// pagamento-boleto.read - Obter dados completos do titulo a partir do código de barras ou da linha digitável
        /// pagamento-darf.write - Pagamento de DARF sem código de barras
        /// cob.write - Alteração de cobranças imediatas
        /// cob.read - Consulta de cobranças imediatas
        /// pix.write - Alteração de pix
        /// pix.read - Consulta de pix
        /// webhook.read - Consulta do webhook
        /// webhook.write - Alteração do webhook
        /// payloadlocation.write - Alteração de payloads
        /// payloadlocation.read - Consulta de payloads
        /// 
        /// </param>
        /// <returns>Retorna true se token gerado com sucesso.</returns>
        /// <exception cref="Exception"></exception>
        public static bool SetToken(string clientId, string clientSecret, string scopes)
        {
            if (ConstApi.certificate2 == null)
                throw new Exception("CERTIFICADO NAO INICIALIZADO");

            var client = new RestClient("https://cdpj.partners.bancointer.com.br/oauth/v2/token");
            client.ClientCertificates = new X509CertificateCollection() { ConstApi.certificate2 };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter(
                "application/x-www-form-urlencoded",
                $"grant_type=client_credentials&" +
                $"client_id={clientId}&" +
                $"client_secret={clientSecret}&" +
                $"scope={scopes}"
                , ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"{response.StatusCode} - {response.ErrorException} - {response.ErrorMessage}");

            AccessToken token = JsonConvert.DeserializeObject<AccessToken>(response.Content);
            token.Expire = DateTime.Now.AddSeconds(token.Expires_in);
            ConstApi.Token = token;
            return true;
        }
        public class Parametros
        {
            public int tipoParceiro { get; set; }
        }

        public class OutputUpload
        {
            public string Message { get; set; }
            public List<OutputFileUpload> Arquivos { get; set; }
        }

        public class OutputFileUpload
        {
            public string Nome { get; set; }
            public string NovoNome { get; set; }
        }

        private CloudBlobClient getConnection(bool gerenteFacil = false)
        {
            CloudStorageAccount storageAccount = null;

            var conn = "";

            if (CloudStorageAccount.TryParse(conn, out storageAccount)
            )
            {
                return storageAccount.CreateCloudBlobClient();
            }
            else
            {
                throw new Exception("Não foi possível conectar no servidor de arquivos!");
            }
        }

        private readonly CloudStorageAccount _account;
        private readonly CloudBlobClient _blobClient;

        public string DownloadToText(string containerName, string blobName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=vendadireta;AccountKey=Ps6oqC2inWSphOWPi5Iog9PZi8yZXo1yPLVi0wkBywdEhkUMh+yMeyedNJ5Pyx/vgmU6GSUBfASHGpP5gCUM6A==;EndpointSuffix=core.windows.net");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            string text;

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                text = System.Text.Encoding.Default.GetString(memoryStream.ToArray());
            }

            return text;
        }

        //public static void UploadFromText(string text, string containerName, string blobName)
        //{
        //    CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
        //                CloudBlockBlob blockBob = container.GetBlockBlobReference(blobName); // Bloco bob

        //    blockBob.UploadText(text); //Se existir irá sobrescrevero existente. 
        //}

        public async void TesteUpload()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=vendadireta;AccountKey=Ps6oqC2inWSphOWPi5Iog9PZi8yZXo1yPLVi0wkBywdEhkUMh+yMeyedNJ5Pyx/vgmU6GSUBfASHGpP5gCUM6A==;EndpointSuffix=core.windows.net";
            string containerName = "w7pay";
            var serviceClient = new BlobServiceClient(connectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            var path2 = @"../src";
            var fileName2 = "logo1.jpg";
            var localFile = Path.Combine(path2, fileName2);
            var blobClient = containerClient.GetBlobClient("Pasta1/" + "logo1.jpg");    
            FileStream uploadFileStream = File.OpenRead(localFile);
            await blobClient.UploadAsync(uploadFileStream, true);   
            uploadFileStream.Close();
        }
    }
}
