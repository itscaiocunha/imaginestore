﻿using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web;

namespace w7pay.src.parceiro
{
    public partial class profile2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                hdfIdUsuario.Value = Session["idusuario"].ToString();

                //informações do usuario que está logado
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                       "select email, nomecompleto, nomeusuario from imagine_usuario where idusuario = '" + hdfIdUsuario.Value + "'"))
                {
                    if (reader.Read())
                    {
                        txtNomeCompleto.Text = reader["nomecompleto"].ToString();
                        txtNomeUsuario.Text = reader["nomeusuario"].ToString();
                        txtEmailUsuario.Text = reader["email"].ToString();
                    }
                }

                hdfIdEmpresa.Value = Session["idempresa"].ToString();

                //informações da empresa que está logada
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                       "select e.[Razão Social / Nome Completo] as razao_social, e.[Nome Fantasia / Nome Abreviado] as nome_empresa, e.cep, e.telefone, e.[CNPJ / CPF] as cnpj, e.Email as email, e.estado as uf, e.cidade, e.bairro, e.[Endereço] as endereco from fornecedores f join base_fornecedor_omie e on e.[Razão Social / Nome Completo] = f.name where f.id = '" + hdfIdEmpresa.Value + "'"))
                {
                    if (reader.Read())
                    {
                        txtRazaoSocial.Text = reader["razao_social"].ToString();
                        txtNomeEmpresa.Text = reader["nome_empresa"].ToString();
                        txtCNPJ.Text = reader["cnpj"].ToString();
                        txtEmailEmpresa.Text = reader["email"].ToString();
                        ddlUF.SelectedValue = reader["uf"].ToString();
                        txtCidade.Text = reader["cidade"].ToString();
                        txtBairro.Text = reader["bairro"].ToString();
                        txtEndereco.Text = reader["endereco"].ToString();
                        //txtNum.Text = reader["num"].ToString();
                        txtCEP.Text = reader["cep"].ToString();
                        txtTelefone.Text = reader["telefone"].ToString();                        
                    }
                }
            }
            catch
            {
                Response.Redirect("../sessao.aspx", false);
            }
        }
       
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            //cria database
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            try
            {
                //atualiza os dados de usuario
                DbCommand command = db.GetSqlStringCommand(
                "UPDATE imagine_usuario set NOMECOMPLETO = @NOMECOMPLETO, NOMEUSUARIO = @NOMEUSUARIO WHERE IDUSUARIO = @IDUSUARIO");
                db.AddInParameter(command, "@IDUSUARIO", DbType.Int16, hdfIdUsuario.Value);
                db.AddInParameter(command, "@NOMECOMPLETO", DbType.String, txtNomeCompleto.Text);
                db.AddInParameter(command, "@NOMEUSUARIO", DbType.String, txtNomeUsuario.Text);
                Session["nomeusuario"] = txtNomeUsuario.Text;

                db.ExecuteNonQuery(command);

                //atualizado os dado da empresa
                DbCommand command1 = db.GetSqlStringCommand(
                "UPDATE imagine_empresa set RAZAO_SOCIAL = @RAZAO_SOCIAL, NOME_EMPRESA = @NOME_EMPRESA, EMAIL = @EMAIL, UF = @UF, CIDADE = @CIDADE, ENDERECO = @ENDERECO, NUM = @NUM, BAIRRO = @BAIRRO, CEP = @CEP, TELEFONE = @TELEFONE WHERE IDEMPRESA = @IDEMPRESA");
                db.AddInParameter(command1, "@IDEMPRESA", DbType.Int16, hdfIdEmpresa.Value);
                db.AddInParameter(command1, "@RAZAO_SOCIAL", DbType.String, txtRazaoSocial.Text);
                db.AddInParameter(command1, "@NOME_EMPRESA", DbType.String, txtNomeEmpresa.Text);
                db.AddInParameter(command1, "@EMAIL", DbType.String, txtEmailEmpresa.Text);
                db.AddInParameter(command1, "@UF", DbType.String, ddlUF.SelectedValue);
                db.AddInParameter(command1, "@CIDADE", DbType.String, txtCidade.Text);
                db.AddInParameter(command1, "@ENDERECO", DbType.String, txtEndereco.Text);
                db.AddInParameter(command1, "@NUM", DbType.String, txtNum.Text);
                db.AddInParameter(command1, "@BAIRRO", DbType.String, txtBairro.Text);
                db.AddInParameter(command1, "@CEP", DbType.String, txtCEP.Text);
                db.AddInParameter(command1, "@TELEFONE", DbType.String, txtTelefone.Text);

                db.ExecuteNonQuery(command1);

                ////se tiver logo nova, insere
                //if (fluLogo.FileName != "")
                //{
                //    try
                //    {
                //        string filepath = Server.MapPath("\\src");

                //        HttpFileCollection uploadedFiles = Request.Files;
                //        for (int i = 0; i < uploadedFiles.Count; i++)
                //        {
                //            HttpPostedFile userPostedFile = uploadedFiles[i];
                //            try
                //            {
                //                userPostedFile.SaveAs(filepath + "\\" + Path.GetFileName(userPostedFile.FileName));
                //            }
                //            catch (Exception Ex)
                //            {
                //                lblMensagem.Text=  Ex.Message;
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("The process failed: ", ex.ToString());
                //    }

                //    //atualiza a logo da empresa
                //    DbCommand command2 = db.GetSqlStringCommand(
                //    "UPDATE imagine_empresa set LOGO = @LOGO WHERE IDEMPRESA = @IDEMPRESA");
                //    db.AddInParameter(command2, "@IDEMPRESA", DbType.String, hdfIdUsuario.Value);
                //    db.AddInParameter(command2, "@LOGO", DbType.String, "https://w7pay.azurewebsites.net/src/"+ fluLogo.FileName + "");//AQUI A URL GERADO PELO AZURE

                //    db.ExecuteNonQuery(command2);
                //}

                lblMensagem.Text = "Informações atualizadas com sucesso!";
            }
            catch(Exception ex)
            {
                lblMensagem.Text = "Erro ao tentar salvar informações." + ex;
            }
        }
               
        protected void txtCEP_TextChanged(object sender, EventArgs e)
        {
            if (txtCEP.Text.Length > 8)
            {
                string cepnovo = txtCEP.Text.Replace("-", "");
                var info = cep.HttpPost("http://viacep.com.br/ws/" + cepnovo + "/json/");
                dynamic dados = JsonConvert.DeserializeObject<dynamic>(info);
                var end = dados["logradouro"];
                txtEndereco.Text = end.ToString();
                var bairro = dados["bairro"];
                txtBairro.Text = bairro.ToString();
                var cidade = dados["localidade"];
                txtCidade.Text = cidade.ToString();
                var uf = dados["uf"];
                ddlUF.SelectedValue = uf.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //atualiza a senha de acesso
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            //criptografa a senha de aceso
            string cript = Criptografia.Encrypt(txtNovaSenha.Text).Replace("+", "=");

            DbCommand command = db.GetSqlStringCommand(
            "UPDATE imagine_usuario set senha = @senha where idusuario = @idusuario");
            db.AddInParameter(command, "@idusuario", DbType.Int16, Convert.ToInt16(hdfIdUsuario.Value));
            db.AddInParameter(command, "@senha", DbType.String, cript);
            try
            {
                db.ExecuteNonQuery(command);
                lblMensagem.Text = "Senha atualizada com sucesso!";
            }
            catch(Exception ex)
            {
                lblMensagem.Text = "Erro ao tentar atualizar";
            }

        }
    }
}