using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class esqueceu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (auth.VerificaEmail(txtEmail.Text))
            {
                //verifica se o cnpj está correto 
                if (auth.VerificaUltimosDigitosCnpj(txtCodigo.Text))
                {
                    //atualiza a senha de acesso
                    Database db = DatabaseFactory.CreateDatabase("ConnectionString");
                    //envia o email para o usuario
                    try
                    {
                        string codigo = auth.RandomString(6);
                        DbCommand command = db.GetSqlStringCommand(
                                "INSERT INTO imagine_seguranca (idusuario, email, token, data_cadastro, data_limite, status) values (@idusuario, email, token, getdate(), data_limite, status)");
                        db.AddInParameter(command, "@idusuario", DbType.Int16, Convert.ToInt16(auth.RetornaIdUsuario(txtEmail.Text)));
                        db.AddInParameter(command, "@email", DbType.String, txtEmail.Text);
                        db.AddInParameter(command, "@token", DbType.String, Criptografia.Encrypt(codigo));
                        db.AddInParameter(command, "@data_limite", DbType.String, DateTime.Now.AddDays(2));
                        db.AddInParameter(command, "@status", DbType.String, "ATIVO");
                        db.ExecuteNonQuery(command);

                        // corpo do e-mail
                        string strHtml = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>";
                        strHtml = strHtml + "<title>W7 Pay</title></head><body><br>";
                        strHtml = strHtml + "<img src=''>";
                        strHtml = strHtml + "<p><strong><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>Solicitação de troca senha<br>W7 Pay</font></strong></p>";
                        strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p>Olá, tudo bem?</p>";
                        strHtml = strHtml + "<font size='5' face='Verdana, Arial, Helvetica, sans-serif'><p>Código de segurança: "+codigo+"</p>";
                        strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p><strong>Sua solicitação de troca de senha foi realizada. Acesse este link e insira o código de confirmação.</strong></p>";
                        strHtml = strHtml + "<font size='2' face='Verdana, Arial, Helvetica, sans-serif'><p>Plataforma <a href='#'>W7 Pay - Troca de Senha</a></p>";
                        strHtml = strHtml + "</font><img src=''></body></html>";
                               
                        //envia o email
                        Email.emailTxt("ola@w7pay.com.br", txtEmail.Text, "", "", "W7 Pay - Troca de Senha", "teste", 0);

                        txtCodigo.Text = "";
                        txtEmail.Text = "";
                        lblMensagem.Text = "Dados confirmados com sucesso. Você receberá um e-mail para troca de senha.";
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Erro ao tentar salvar informações." + ex.Message;
                    }
                }
                else
                {
                    lblMensagem.Text = "Dados de segurança incorretos. Verifique o cnpj.";
                }
            }
            else
            {
                lblMensagem.Text = "E-mail não encontrado em nosso sistema!";
            }
        }
    }
}