using ClosedXML.Excel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sdk.BankingApi;
using Sdk.PixApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using w7pay.src.cliente;

namespace w7pay.src.parceiro
{
    public partial class fechamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdfId.Value =  Session["idempresa"].ToString();
                ddlAnoMes.DataBind();
                ddlMes.DataBind();
                string mesano = ddlMes.SelectedValue + "/" + ddlAnoMes.SelectedValue;
                try
                {
                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                      @"select  sum(f.qtde_venda) as qtde, max(taxa) as tx, cast(isnull(sum(faturamento),0) as decimal(10,2)) as valortotal, case when max(s.idfornecedor) is not null then cast((isnull(sum(faturamento),0) * max(s.taxa)) / 100  as Decimal(10,2)) else 0 end as taxa from fechamento f
                        left join split s on s.idfornecedor = f.idfornecedor
                        where f.idfornecedor = '" + hdfId.Value + "' and mesano = '" + mesano + "' group by f.idfornecedor"))
                    {
                        if (reader.Read())
                        {
                            gdvDetalhes.DataBind();
                            lblQtdeTotal.Text = reader["qtde"].ToString();
                            lblSaldoVendas.Text = "R$ " + Convert.ToDecimal(reader["valortotal"]).ToString("N2");
                            lblValorImagine.Text = "R$ " + Convert.ToDecimal(reader["taxa"]).ToString("N2");
                            lblTaxa.Text = Convert.ToDecimal(reader["tx"]).ToString("N2");
                            decimal receber = Convert.ToDecimal(reader["valortotal"]) - Convert.ToDecimal(reader["taxa"]);
                            lblValorAReceber.Text = "R$ " + receber.ToString("N2");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            string mesano = ddlMes.SelectedValue + "/" + ddlAnoMes.SelectedValue;
            try
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                      @"select  sum(f.qtde_venda) as qtde, max(taxa) as tx, cast(isnull(sum(faturamento),0) as decimal(10,2)) as valortotal, case when max(s.idfornecedor) is not null then cast((isnull(sum(faturamento),0) * max(s.taxa)) / 100  as Decimal(10,2)) else 0 end as taxa from fechamento f
                        left join split s on s.idfornecedor = f.idfornecedor
                        where f.idfornecedor = '" + hdfId.Value + "' and mesano = '" + mesano + "' group by f.idfornecedor"))
                {
                    if (reader.Read())
                    {
                        gdvDetalhes.DataBind();
                        lblQtdeTotal.Text = reader["qtde"].ToString();
                        lblSaldoVendas.Text = "R$ " + reader["valortotal"].ToString();
                        lblValorImagine.Text = "R$ " + reader["taxa"].ToString();
                        lblTaxa.Text = reader["tx"].ToString();
                        double receber = Convert.ToDouble(reader["valortotal"].ToString()) - Convert.ToDouble(reader["taxa"].ToString());
                        lblValorAReceber.Text = receber.ToString("c2");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        public class FechamentoModel
        {
            public string mesano { get; set; }
            public string fornecedor { get; set; }
            public string ean { get; set; }
            public string nomeproduto { get; set; }
            public string qtde_mes_anterior { get; set; }
            public string entrada { get; set; }
            public string valor { get; set; }
            public string qtde_venda { get; set; }
            public string faturamento { get; set; }
            public string qtde_dishonest { get; set; }
            public string valor_dishonest { get; set; }
            public string estoquecd { get; set; }
            public string estoqueloja { get; set; }
            public string saldo { get; set; }

        }

        static List<FechamentoModel> GenerateDataFromGridView(GridView gridView)
        {
            var data = new List<FechamentoModel>();

            foreach (GridViewRow row in gridView.Rows)
            {
                var item = new FechamentoModel
                {
                    mesano = row.Cells[0].Text,
                    fornecedor = row.Cells[1].Text,
                    ean = row.Cells[2].Text,
                    nomeproduto = row.Cells[3].Text,
                    qtde_mes_anterior = row.Cells[4].Text,
                    entrada = row.Cells[5].Text,
                    valor = row.Cells[6].Text,
                    qtde_venda = row.Cells[7].Text,
                    faturamento = row.Cells[8].Text,
                    qtde_dishonest = row.Cells[9].Text,
                    valor_dishonest = row.Cells[10].Text,
                    estoquecd = row.Cells[11].Text,
                    estoqueloja = row.Cells[12].Text,
                    saldo = row.Cells[13].Text
                };

                data.Add(item);
            }

            return data;
        }

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = GenerateDataFromGridView(gdvDetalhes);

                string fileName = "FechamentoFinanceiro.xlsx";

                string filePath = Path.Combine(Server.MapPath("~/assets"), fileName);

                GenerateFile("Fechamento Financeiro", filePath, lista);

                lblMensagem.Text = "Arquivo de Excel gerado com sucesso.";

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Ocorreu um erro ao gerar o arquivo Excel: " + ex.Message;
            }
        }

        static void GenerateFile(string tabName, string filePath, ICollection<FechamentoModel> lista)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var workbook = new XLWorkbook())
            {
                var planilha = workbook.Worksheets.Add(tabName);

                int line = 1;
                GenerateHeader(planilha);
                line++;

                foreach (var item in lista)
                {
                    planilha.Cell("C" + line).Value = item.ean;
                    planilha.Cell("D" + line).Value = item.nomeproduto;
                    planilha.Cell("E" + line).Value = item.qtde_mes_anterior;
                    planilha.Cell("F" + line).Value = item.entrada;
                    planilha.Cell("G" + line).Value = item.valor;
                    planilha.Cell("H" + line).Value = item.qtde_venda;
                    planilha.Cell("I" + line).Value = item.faturamento;
                    planilha.Cell("J" + line).Value = item.qtde_dishonest;
                    planilha.Cell("K" + line).Value = item.valor_dishonest;
                    planilha.Cell("L" + line).Value = item.estoquecd;
                    planilha.Cell("M" + line).Value = item.estoqueloja;
                    planilha.Cell("N" + line).Value = item.saldo;
                    line++;
                }

                workbook.SaveAs(filePath);
            }

        }


        static void GenerateHeader(IXLWorksheet planilha)
        {
            planilha.Cell("A1").Value = "Mês/Ano";
            planilha.Cell("B1").Value = "Fornecedor";
            planilha.Cell("C1").Value = "EAN";
            planilha.Cell("D1").Value = "Produto";
            planilha.Cell("E1").Value = "Quant. Mês Anterior";
            planilha.Cell("F1").Value = "Entrada";
            planilha.Cell("G1").Value = "Valor";
            planilha.Cell("H1").Value = "Quant. Venda";
            planilha.Cell("I1").Value = "Valor Venda";
            planilha.Cell("J1").Value = "Quant. Dishonest";
            planilha.Cell("K1").Value = "Valor Dishonest";
            planilha.Cell("L1").Value = "Estoque CD";
            planilha.Cell("M1").Value = "Estoque Loja";
            planilha.Cell("N1").Value = "Saldo";
        }
    }
}
