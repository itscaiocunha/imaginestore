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
                       "select * from imagine_usuario u left join imagine_usuario_perfil up on up.idusuario = u.idusuario join imagine_usuario_loja ul on ul.idusuario = u.idusuario where u.email = '" + txtEmail.Text + "'"))
                {
                    if (reader1.Read())
                    {
                        Session["idempresa"] = reader1["idloja"].ToString();
                        Session["idusuario"] = reader1["idusuario"].ToString();
                        Session["nomeusuario"] = reader1["nomeusuario"].ToString();
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
            }
            else//verifica o cnpj se é de parceiro
            {
                using (IDataReader reader2 = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
               "select * from base_fornecedor_omie bf join fornecedores f on f.name = bf.[Razão Social / Nome Completo] where [CNPJ / CPF] = '" + txtEmail.Text + "'"))
                {
                    if (reader2.Read())
                    {
                        Session["idempresa"] = reader2["id"].ToString();
                        Session["idusuario"] = reader2["id"].ToString();
                        Session["nomeusuario"] = reader2["name"].ToString();
                        //Session["token"] = reader1["token"].ToString();
                        Response.Redirect("parceiro/dashboard.aspx", false);
                    }
                    else
                    {
                        lblMensagem.Text = "E-mail ou senha incorretos. Tente novamente!";
                    }
                }
            }
        }
    }
}