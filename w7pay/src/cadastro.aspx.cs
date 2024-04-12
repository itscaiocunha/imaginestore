using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using w7pay.Model.FPIX;
using w7pay.Service;

namespace w7pay.src
{
    public partial class cadastro2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {                
                if (Request.QueryString.Count > 0)
                {                    
                    hdfRevendedor.Value = Request.QueryString["tk"].ToString();
                }
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            //cria database
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            //verificar se o cnpj é valido
            if (!auth.VerificaCnpj(txtCNPJCPF.Text.Replace(".", "").Replace("/","").Replace("-","")))
            {
                //verificar se o email é valido 
                if (!auth.VerificaEmail(txtEmail.Text))
                {
                    try
                    {
                        string tkempresa = Criptografia.Encrypt(auth.RandomString(6)).Replace("+","=");

                        //atualizado os dado da empresa
                        DbCommand command1 = db.GetSqlStringCommand(
                        "INSERT INTO imagine_empresa (CNPJ, EMAIL, TELEFONE, STATUS, DATA_CADASTRO, IDREVENDEDOR, TOKEN, IDPLANO) VALUES (@CNPJ, EMAIL, TELEFONE, STATUS, GETDATE(), IDREVENDEDOR, TOKEN, IDPLANO)");
                        db.AddInParameter(command1, "@CNPJ", DbType.String, txtCNPJCPF.Text.Replace(".", "").Replace("/", "").Replace("-", ""));
                        db.AddInParameter(command1, "@EMAIL", DbType.String, txtEmail.Text);
                        db.AddInParameter(command1, "@TELEFONE", DbType.String, txtTelefone.Text.Replace(".", "").Replace("/", "").Replace("-", ""));
                        db.AddInParameter(command1, "@STATUS", DbType.String, "AGUARDANDO PAGAMENTO");
                        db.AddInParameter(command1, "@IDREVENDEDOR", DbType.String, hdfRevendedor.Value);
                        db.AddInParameter(command1, "@TOKEN", DbType.String, tkempresa);
                        db.AddInParameter(command1, "@IDPLANO", DbType.Int16, ddlPlano.SelectedValue);

                        db.ExecuteNonQuery(command1);

                        using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                               "select idempresa from imagine_empresa where cnpj = '" + txtCNPJCPF.Text.Replace(".", "").Replace("/", "").Replace("-", "") + "'"))
                        {
                            if (reader.Read())
                            {
                                try
                                {
                                    hdfIdEmpresa.Value = reader["idempresa"].ToString();
                                    //criptografa a senha de acesso
                                    string cript = Criptografia.Encrypt(txtSenha.Text).Replace("+", "=");
                                    //atualiza os dados de usuario
                                    DbCommand command = db.GetSqlStringCommand(
                            "INSERT INTO imagine_usuario (NOMECOMPLETO, NOMEUSUARIO, EMAIL, IDEMPRESA, SENHA, DATA_CADASTRO, STATUS) values (@NOMECOMPLETO, NOMEUSUARIO, EMAIL, IDEMPRESA, SENHA, GETDATE(), 'AGUARDANDO PAGAMENTO')");
                                    db.AddInParameter(command, "@NOMECOMPLETO", DbType.String, "");
                                    db.AddInParameter(command, "@NOMEUSUARIO", DbType.String, "");
                                    db.AddInParameter(command, "@EMAIL", DbType.String, txtEmail.Text);
                                    db.AddInParameter(command, "@SENHA", DbType.String, cript);
                                    db.AddInParameter(command, "@IDEMPRESA", DbType.Int16, Convert.ToInt16(reader["idempresa"].ToString()));

                                    db.ExecuteNonQuery(command);

                                    //insere dados de configuração de empresa
                                    DbCommand command2 = db.GetSqlStringCommand(
                                    "INSERT INTO imagine_config (IDEMPRESA, IDTIPOCHAVE, DATA_CADASTRO) VALUES (@IDEMPRESA, IDTIPOCHAVE, GETDATE())");
                                    db.AddInParameter(command2, "@IDEMPRESA", DbType.Int16, Convert.ToInt16(reader["idempresa"].ToString()));
                                    db.AddInParameter(command2, "@IDTIPOCHAVE", DbType.Int16, 1);

                                    db.ExecuteNonQuery(command2);                                   
                                }
                                catch( Exception ex)
                                {
                                    lblMensagem.Text = "Erro ao tentar salvar informações. Tenta novamente!" + ex.Message;
                                }                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Erro ao tentar salvar informações. Tenta novamente!" + ex.Message;
                    }
                }
                else
                {
                    lblMensagem.Text = "E-mail já cadastrado em nosso sistema.";
                }
            }
            else
            {
                lblMensagem.Text = "CNPJ já cadastrado em nosso sistema.";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Whatsapp dados = new Whatsapp();
            dados.email = "contato@w7startup.com.br";
            dados.senha = "Sqlw7@23w7";
            dados.numero = "55" + txtTelefone.Text.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            dados.mensagem = "Olá, tudo bem? Estamos muito felizes pelo seu cadastro. Deu tudo certo, tá! Sei que é muito bom vender no pix sem taxa e com segurança. Agora, poderá utilizar tecnologia em seu negócio à vontade. Ah, não se esqueça de ao acessar a plataforma, preenchar suas configurações. Boas vendas!";
            dados.clientId = "2b083oMVRtgOUdBG9EAu00YeSQF1zsegthyIsFhF1JaErRTytpZqULO";

            Zapweb.EnviarMensagem(dados);

            //desativa o timer
            Timer1.Enabled = false;
            btnLogin.Visible = true;
            //apos o primeiro registro, apaga as chaves
            lblChaveCopiaCola.Text = "";
            lblTxid.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx", false);
        }
    }
}