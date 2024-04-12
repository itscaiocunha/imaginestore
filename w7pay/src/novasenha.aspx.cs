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
    public partial class novasenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" || txtEmail.Text != "" || txtNovaSenha.Text != "")
            {
                using (IDataReader reader1 = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                           "select * from imagine_segurancao where email = '" + txtEmail.Text + "' and token = '" + txtCodigo.Text + "' and data_limite > getdate()"))
                {
                    if (reader1.Read())
                    {
                        //atualiza a senha de acesso
                        Database db = DatabaseFactory.CreateDatabase("ConnectionString");
                        //criptografa a senha de aceso
                        string cript = Criptografia.Encrypt(txtNovaSenha.Text).Replace("+", "=");

                        DbCommand command = db.GetSqlStringCommand(
                        "UPDATE imagine_usuario set senha = @senha where idusuario = @idusuario");
                        db.AddInParameter(command, "@idusuario", DbType.Int16, Convert.ToInt16(reader1["idusuario"].ToString()));
                        db.AddInParameter(command, "@senha", DbType.String, cript);
                        db.ExecuteNonQuery(command);

                        lblMensagem.Text = "Senha atualizada com sucesso. Acesse o sistema para logar!";
                    }
                    else
                    {
                        lblMensagem.Text = "Dados incorretos ou tempo expirado. Tente novamente!";
                    }
                }
            }
            else
            {
                lblMensagem.Text = "Preencha todos os campos para avançar.";
            }
        }
    }
}