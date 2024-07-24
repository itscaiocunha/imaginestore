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
using System.Data.Common;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace w7pay.src
{
    public partial class save : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            if (!auth.VerificaCnpj(txtCNPJ.Text) && !auth.VerificaEmail(txtEmail.Text))
            {
                try
                {
                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                   "select f.id from base_fornecedor_omie o inner join fornecedores f on f.name = o.[Razão Social / Nome Completo] where o.[CNPJ / CPF] =  '" + txtCNPJ.Text + "'"))
                    {
                        if (reader.Read())
                        {
                            try
                            {
                                string pw = auth.RandomNumero(6);
                                string cript = Criptografia.Encrypt(pw).Replace("+", "=");

                                DbCommand command = db.GetSqlStringCommand(
                                    "INSERT INTO imagine_usuario (NOMECOMPLETO, NOMEUSUARIO, EMAIL, IDEMPRESA, SENHA, DATA_CADASTRO, STATUS) values (@NOMECOMPLETO, @NOMEUSUARIO, @EMAIL, @IDEMPRESA, @SENHA, GETDATE(), 'ATIVO')");
                                db.AddInParameter(command, "@NOMECOMPLETO", DbType.String, "");
                                db.AddInParameter(command, "@NOMEUSUARIO", DbType.String, txtCNPJ.Text);
                                db.AddInParameter(command, "@EMAIL", DbType.String, txtEmail.Text);
                                db.AddInParameter(command, "@SENHA", DbType.String, cript);
                                db.AddInParameter(command, "@IDEMPRESA", DbType.Int32, Convert.ToInt32(reader["id"].ToString()));

                                db.ExecuteNonQuery(command);

                                using (IDataReader reader2 = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                    "select idusuario from imagine_usuario where email = '" + txtEmail.Text + "'"))
                                {
                                    if (reader2.Read())
                                    {
                                        try {
                                            //Insere Loja
                                            DbCommand command2 = db.GetSqlStringCommand(
                                                "INSERT INTO imagine_usuario_loja (idusuario, idloja, status) values (@usuario, @loja, 'ATIVO')");
                                            db.AddInParameter(command2, "@usuario", DbType.Int32, Convert.ToInt32(reader2["idusuario"].ToString()));
                                            db.AddInParameter(command2, "@loja", DbType.Int32, Convert.ToInt32(reader["id"].ToString()));

                                            db.ExecuteNonQuery(command2);

                                            try {
                                                //Insere Perfil
                                                DbCommand command3 = db.GetSqlStringCommand(
                                                    "INSERT INTO imagine_usuario_perfil (idusuario, perfil) values (@usuario, 'FORNECEDOR')");
                                                db.AddInParameter(command3, "@usuario", DbType.Int32, Convert.ToInt32(reader2["idusuario"].ToString()));

                                                db.ExecuteNonQuery(command3);

                                                //aqui envia o email ao cliente cadastrado
                                                // corpo do e-mail
                                                string strhtml = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='content-type' content='text/html; charset=iso-8859-1'>";
                                                strhtml = strhtml + "</head><body><br>";
                                                strhtml = strhtml + "<img src='https://imaginestore.azurewebsites.net/src/img/logo/imaginelogo.png' width='200' alt='logo'><br><br>";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 16px'>Seja bem-vindo à Imagine Store!</p><br />";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 14px'>Para acessar a sua plataforma exclusiva, preencha o campo do usuário com o e-mail cadastrado e configure a sua senha. Ao acessar, você poderá ver estatísticas em tempo real, alterar informações do seu perfil, visualizar os produtos cadastrados, vendas por período, fechamento geral e por período, vendas por loja e/ou por produto e as informações de estoque em loja e no Centro de Distribuição (CD). Essa é a primeira versão, alguns dados poderão apresentar certa imprecisão porque alguns processos estão em integração. Novos gráficos e funções serão atualizadas nos próximos dias. Informaremos a vocês todas as etapas de evolução da plataforma.</p><br />";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 14px'>Se surgirem dúvidas durante o processo, estamos à disposição para ajudar. Para esclarecimentos adicionais, fale com: Fabio Passos (11) 94712-9181</p><br />";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 14px'><strong>E-mail: </strong>" + txtEmail.Text + "</p>";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 14px'><strong>Senha: </strong>" + pw + "</p><br />";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 14px'><a href='https://imaginestore.azurewebsites.net/'>Clique aqui para acessar</a></p><br>";
                                                strhtml = strhtml + "<font size='2' face='verdana, arial, helvetica, sans-serif'><p style='font-size: 14px'>Atenciosamente, Imagine Store.</p><br />";
                                                strhtml = strhtml + "</font><img src=''></body></html>";

                                                //base teste
                                                Email.emailTxt("contato@w7agencia.com.br", "contato@w7agencia.com.br", "", "", "Imagine Store - Plataforma Digital", strhtml, 1);
                                                //base oficial
                                                Email.emailTxt("contato@w7agencia.com.br", txtEmail.Text, "", "", "Imagine Store - Plataforma Digital", strhtml, 1);
                                                
                                                lblMensagem.Text = "Dados cadastrados com sucesso! Você receberá um e-mail com mais informações!";
                                            }
                                            catch (Exception ex)
                                            {
                                                lblMensagem.Text = "Erro ao tentar salvar informações loja. Tente novamente! " + ex.Message;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            lblMensagem.Text = "Erro ao tentar salvar informações perfil. Tente novamente! " + ex.Message;
                                        }
                                    }
                                    else
                                    {
                                        lblMensagem.Text = "Usuário não encontrado";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMensagem.Text = "Erro ao tentar salvar informações usuario. Tente novamente! " + ex.Message;
                            }
                        }
                        else
                        {
                            lblMensagem.Text = "CNPJ não encontrado! Tente novamente! ";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "CNPJ ou E-mail estão escritos incorretamente. Tente novamente! " + ex.Message;
                }
            }
        }
    }
}