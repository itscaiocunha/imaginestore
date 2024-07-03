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
    public partial class taxa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try {
                    Session["idusuario"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }
            }
        }

        protected void lkbFechar_Click(object sender, EventArgs e)
        {
            pnlModal.Visible = false;
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            sdsDados.SelectCommand = "select * from split t join fornecedores f on f.id = t.idfornecedor where name like '%" + txtBuscar.Text + "%'";
            gdvDados.DataBind();
        }

        //protected void gdvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    hdfId.Value = e.CommandArgument.ToString();
        //    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
        //                  "SELECT * from split where idfornecedor = '" + hdfId.Value + "'"))
        //    {
        //        if (reader.Read())
        //        {
        //            ddlFornecedor.SelectedValue = reader["idfornecedor"].ToString();
        //            txtTaxa.Text = reader["taxa"].ToString();
        //            ddlStatus.SelectedValue = reader["status"].ToString();
        //            txtDiaPagamento.Text = reader["dia_pagamento"].ToString();
        //            pnlModal.Visible = true;
        //            lblMensagem.Text = "";
        //            txtTaxa.Focus();

        //        }
        //    }
        //}

        protected void gdvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                hdfId.Value = e.CommandArgument.ToString();
                string query = "SELECT * from split where idfornecedor = @idfornecedor";

                // Use a conexão com o banco de dados
                Database db = DatabaseFactory.CreateDatabase("ConnectionString");

                using (DbCommand cmd = db.GetSqlStringCommand(query))
                {
                    db.AddInParameter(cmd, "@idfornecedor", DbType.String, hdfId.Value);
                    using (IDataReader reader = db.ExecuteReader(cmd))
                    {
                        if (reader.Read())
                        {
                            ddlFornecedor.SelectedValue = reader["idfornecedor"].ToString();
                            txtTaxa.Text = reader["taxa"].ToString();

                            // Verifica se o valor existe no DropDownList antes de atribuir
                            string status = reader["status"].ToString();
                            if (ddlStatus.Items.FindByValue(status) != null)
                            {
                                ddlStatus.SelectedValue = status;
                            }
                            else
                            {
                                // Handle the case where the value is not found
                                lblMensagem.Text = "Status inválido.";
                            }

                            txtDiaPagamento.Text = reader["dia_pagamento"].ToString();
                            pnlModal.Visible = true;
                            lblMensagem.Text = "";
                            txtTaxa.Focus();
                        }
                    }
                }
            }
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            
            if (hdfId.Value == "")
            {
                DbCommand command = db.GetSqlStringCommand(
                "INSERT INTO split (idfornecedor, taxa, status, dia_pagamento, datacadastro) values (@idfornecedor, @taxa, @status, @dia_pagamento, getDate())");
                db.AddInParameter(command, "@idfornecedor", DbType.Int16, Convert.ToInt16(ddlFornecedor.SelectedValue));
                db.AddInParameter(command, "@taxa", DbType.Double, Convert.ToDouble(txtTaxa.Text));
                db.AddInParameter(command, "@status", DbType.String, ddlStatus.SelectedValue);
                db.AddInParameter(command, "@dia_pagamento", DbType.Int16, Convert.ToInt16(txtDiaPagamento.Text));
                
                try
                {
                    db.ExecuteNonQuery(command);
                    lblMensagem.Text = "Informação salva com sucesso!";
                    txtTaxa.Text = "";
                    txtDiaPagamento.Text = "";
                    gdvDados.DataBind();
                    pnlModal.Visible = false;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao tentar salvar informação. " + ex.Message;
                }
            }
            else
            {
                DbCommand command = db.GetSqlStringCommand(
               "UPDATE split SET taxa = @taxa, status = @status , dia_pagamento = @dia_pagamento where idfornecedor = @idfornecedor");
                db.AddInParameter(command, "@idfornecedor", DbType.Int16, Convert.ToInt16(ddlFornecedor.SelectedValue));
                db.AddInParameter(command, "@taxa", DbType.Double, Convert.ToDouble(txtTaxa.Text));
                db.AddInParameter(command, "@status", DbType.String, ddlStatus.SelectedValue);
                db.AddInParameter(command, "@dia_pagamento", DbType.Int16, Convert.ToInt16(txtDiaPagamento.Text));
                try
                {
                    db.ExecuteNonQuery(command);
                    lblMensagem.Text = "Informação atualizada com sucesso!";
                    txtTaxa.Text = "";
                    txtDiaPagamento.Text = "";
                    hdfId.Value = "";
                    gdvDados.DataBind();
                    pnlModal.Visible = false;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao tentar atualizada informação. " + ex.Message;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            pnlModal.Visible = true;
            txtTaxa.Text = "";
            txtDiaPagamento.Text = "";
            lblMensagem.Text = "";
            txtTaxa.Focus();
        }
    }
}