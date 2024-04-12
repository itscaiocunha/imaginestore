using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace w7pay
{
    public class Email
    {
        //Adicionar a classe abaixo, senão o AutoDiscoverUrl retorna erro
        private static bool ValidateRedirectionUrlCallback(string url)
        {
            if (url.Substring(0, 8) == "https://")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        const string BASEURRL = @"https://www.zapweb.app.br/usuario/entrar";

        public static void MensagemWhatsapp(string mensagem, int idvenda, int idempresa)
        {
            //envia a mensagem ao cliente ou ao proprietário 
            //aqui vai conectar com a empresa zapweb via api
            //"autorizacao": {
            //"responsavelId": "91ea6097-83a6-4dca-b5a2-158f08e62f25",
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjkxZWE2MDk3LTgzYTYtNGRjYS1iNWEyLTE1OGYwOGU2MmYyNSIsImlhdCI6MTcwMTI4MzU0OSwiZXhwIjoxNzAxMzY5OTQ5fQ.SbDlUZ4cpZE2PiYuj-YMMqwMEVOsXf1OM3iyXDnKztQ";
            var dados = "{ 'email': 'contato@w7startup.com.br', 'senha': 'Sqlw7@23w7'";

            var client = new RestClient($"{BASEURRL}");
            var request = new RestRequest(Method.POST);
            string env = JsonConvert.SerializeObject(dados);
            request.AddParameter(
                "application/json",
                env,
                ParameterType.RequestBody);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"{response.StatusCode} - {response.StatusDescription}");

            var ret = JsonConvert.DeserializeObject<Email>(response.Content);

            //registra a mensagem na tabela de notificacoes 
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            DbCommand command = db.GetSqlStringCommand(
            "INSERT INTO imagine_mensagem (idempresa, mensagem, data_cadastro, idvenda) Values (@idempresa, mensagem, getDate(), idvenda)");
            db.AddInParameter(command, "@idempresa", DbType.String, idempresa);
            db.AddInParameter(command, "@mensagem", DbType.String, mensagem);
            db.AddInParameter(command, "@idvenda", DbType.String, idvenda);
            //db.ExecuteNonQuery(command);
        }

        public static void emailTxt(string de, string para, string cc, string cco, string assunto, string mensagem, int prioridade)
        {
            //Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            //DbCommand command = db.GetSqlStringCommand(
            //"INSERT INTO Email_Disparo (Email_de,Email_Para,Email_Assunto,Email_Corpo,Email_data_gravacao,Email_data_envio,Email_Tentativa_Envio,Email_Enviado) " +
            //        " Values (@Email_de,@Email_Para,@Email_Assunto,@Email_Corpo,@Email_data_gravacao,@Email_data_envio,1,0)");
            //db.AddInParameter(command, "@Email_Para", DbType.String, para);
            //db.AddInParameter(command, "@Email_De", DbType.String, "w7naoresponda@gmail.com");
            //db.AddInParameter(command, "@Email_Assunto", DbType.String, assunto);
            //db.AddInParameter(command, "@Email_Corpo", DbType.String, mensagem);
            //db.AddInParameter(command, "@Email_data_gravacao", DbType.DateTime, DateTime.Now);
            //db.AddInParameter(command, "@Email_data_envio", DbType.DateTime, DateTime.Now);
            //db.ExecuteNonQuery(command);

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("w7naoresponda@gmail.com", "Sqlw7@20w7");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("w7naoresponda@gmail.com", "W7 Pay");
            mail.From = new MailAddress("w7naoresponda@gmail.com", "W7 Pay");
            mail.To.Add(new MailAddress(para, para));
            mail.Subject = assunto;
            mail.Body = mensagem;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
                //trata erro
            }
            finally
            {
                mail = null;
            }
        }
    }
}