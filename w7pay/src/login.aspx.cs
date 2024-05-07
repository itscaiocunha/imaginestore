using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using w7pay.Service;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace w7pay.src
{
    public partial class login2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                //limpa as sessões
                Session["idempresa"] = "";
                Session["idusuario"] = "";
                Session["nomeusuario"] = "";
                Session["token"] = "";
                Session.Clear();


                ////aqui envia o email ao cliente cadastrado
                //// corpo do e-mail
                //string strHtml = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>";
                //strHtml = strHtml + "<title>Imagine Store - Plataforma Digital</title></head><body><br>";
                //strHtml = strHtml + "<img src='https://imaginestore.azurewebsites.net/src/img/logo/logo.png' width='200' alt='Logo'>";
                //strHtml = strHtml + "<p><strong><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>Novo Cadastro<br>Imagine Store - Plataforma Digital</font></strong></p>";
                //strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p>Olá, tudo bem? Bem-vindo a plataforma digital Imagine Store.</p>";
                //strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p>Seu cadastro foi realizado com sucesso na plataforma e seus acessos são estes:</p>";
                //strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p><strong>E-mail:</strong>eduardo.marques@imagineyourstore.com.br</p>";
                //strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p><strong>Senha</strong><br>123456</p>";
                //strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p><a href='https://imaginestore.azurewebsites.net/'>Plataforma Imagine Store</a></p><br><br>";
                //strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p><strong>Neste momento vamos compartilhar as informações sobre os seus produtos. Estamos atualizando a plataforma constantemente, em breve com mais informações.</strong><br></p>";
                //strHtml = strHtml + "</font><img src=''></body></html>";

                ////base teste
                //Email.emailTxt("contato@w7agencia.com.br", "daniele@imaginestore.com.br", "", "", "Imagine Store - Plataforma Digital", strHtml, 1);
                ////base oficial
                ////Email.emailTxt("contato@w7agencia.com.br", txtEmail.Text, "", "", "Imagine Store - Novo Cadastro", strHtml, 1);

            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            //criptografa a senha de aceso
            string cript = Criptografia.Encrypt(txtSenha.Text).Replace("+", "=");
            //verifica se está com acesso ao sistema
            if (auth.Login(txtEmail.Text, cript))
            {
                using (IDataReader reader1 = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                       "select * from imagine_usuario u left join imagine_usuario_perfil up on up.idusuario = u.idusuario join imagine_usuario_loja ul on ul.idusuario = u.idusuario  join fornecedores f on f.id = ul.idloja where u.email = '" + txtEmail.Text + "'"))
                {
                    if (reader1.Read())
                    {
                        Session["idempresa"] = reader1["idloja"].ToString();
                        Session["idusuario"] = reader1["idusuario"].ToString();
                        Session["nomeusuario"] = reader1["name"].ToString();
                        //Session["token"] = reader1["token"].ToString();
                        if (reader1["perfil"].ToString() == "ADMIN")
                            Response.Redirect("admin/dashboard.aspx", false);
                        else if (reader1["perfil"].ToString() == "FORNECEDOR")
                            Response.Redirect("parceiro/dashboard.aspx", false);
                    }
                    else
                    {
                        lblMensagem.Text = "E-mail ou senha incorretos. Tente novamente!";
                    }
                }
            //}
            //else//verifica o cnpj se é de parceiro
            //{
            //    using (IDataReader reader2 = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
            //   "select * from base_fornecedor_omie bf join fornecedores f on f.name = bf.[Razão Social / Nome Completo] where [CNPJ / CPF] = '" + txtEmail.Text + "'"))
            //    {
            //        if (reader2.Read())
            //        {
            //            Session["idempresa"] = reader2["id"].ToString();
            //            Session["idusuario"] = reader2["id"].ToString();
            //            Session["nomeusuario"] = reader2["name"].ToString();
            //            //Session["token"] = reader1["token"].ToString();
            //            Response.Redirect("parceiro/dashboard.aspx", false);
            //        }
            //        else
            //        {
            //            lblMensagem.Text = "E-mail ou senha incorretos. Tente novamente!";
            //        }
            //    }
            }
        }
    }
}