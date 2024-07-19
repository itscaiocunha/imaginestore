using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web.UI.WebControls;
using w7pay.src.cliente;
using w7pay.src.parceiro;


namespace w7pay.src
{
    public partial class fechamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Database db = DatabaseFactory.CreateDatabase("ConnectionString");

                //try
                //{
                //    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                //                                                  @"select max(f.name) as fornecedor, manufacturer_id, tab.id, max(codigobarras) as codigobarras, max(nomeproduto) as nomeproduto, max(custo) as custo, max(valor) as valor, max(qtde) as qtde, max(total) as total, sum(qtdediso) as qtdediso, sum(valordiso) as valordiso, sum(qtdecd) as qtdecd, sum(qtdeloja) as qtdeloja, sum(qtdecd) + sum(qtdeloja) as saldo  from (
                //                    select e.manufacturer_id, e.id, max(e.upc_code) as codigobarras, max(e.name) as nomeproduto, 0 as custo, 0 as valor, 0 as qtde, 0 as total, 0 as qtdediso, 0 as valordiso, isnull(sum(e.sald),0) as qtdecd, 0 as qtdeloja from estoque e
                //                    where distribution_center_id = '99999'
                //                    group by e.manufacturer_id, e.id
                //                    union all
                //                    select e.manufacturer_id, e.id, max(e.upc_code) as codigobarras, max(e.name) as nomeproduto, 0 as custo, 0 as valor, 0 as qtde, 0 as total, 0 as qtdediso, 0 as valordiso, 0 as qtdecd, isnull(sum(e.sald),0) as qtdeloja from estoque e
                //                    where distribution_center_id <> '99999'
                //                    group by e.manufacturer_id, e.id
                //                    union all
                //                    select p.manufacturer_id, v.good_id as id, max(p.upc_code) as codigobarras, max(p.name) as nomeproduto, 0 as custo, max(v.value) as valor, isnull(sum(v.quantity),0) as qtde, isnull(sum(v.value),0) as total, 0 as qtdediso, 0 as valordiso, 0 as qtdecd, 0 as qtdeloja from vendas v
                //                    left join produtos p on p.id = convert(varchar,v.good_id)
                //                    where year(v.occurred_at) = '2024' and month(v.occurred_at) = '3' and v.[status] = 'OK'
                //                    group by p.manufacturer_id, v.good_id) as tab
                //                    join fornecedores f on f.id = tab.manufacturer_id
                //                    group by manufacturer_id, tab.id
                //                    having manufacturer_id is not null
                //                    order by total desc"))
                //    {
                //        while (reader.Read())
                //        {
                //            DbCommand command = db.GetSqlStringCommand(
                //                "INSERT INTO fechamento (mesano, idfornecedor, fornecedor, product_id, ean, nomeproduto, qtde_mes_anterior, entrada, custo, valor, qtde_venda, faturamento, qtde_dishonest, valor_dishonest, qtde_combo, devolucao, perdas, estoquecd, estoqueloja, saldo, data_criacao) values (@mesano, @idfornecedor, @fornecedor, @product_id, @ean, @nomeproduto, @qtde_mes_anterior, @entrada, @custo, @valor, @qtde_venda, @faturamento, @qtde_dishonest, @valor_dishonest, @qtde_combo, @devolucao, @perdas, @estoquecd, @estoqueloja, @saldo, getDate())");
                //            db.AddInParameter(command, "@mesano", DbType.String, "5/2024");
                //            db.AddInParameter(command, "@idfornecedor", DbType.Int32, Convert.ToInt32(reader["manufacturer_id"].ToString()));
                //            db.AddInParameter(command, "@fornecedor", DbType.String, reader["fornecedor"].ToString());
                //            db.AddInParameter(command, "@product_id", DbType.Int32, Convert.ToInt32(reader["id"].ToString()));
                //            db.AddInParameter(command, "@ean", DbType.String, reader["codigobarras"].ToString());
                //            db.AddInParameter(command, "@nomeproduto", DbType.String, reader["nomeproduto"].ToString());
                //            db.AddInParameter(command, "@qtde_mes_anterior", DbType.Int16, 0);
                //            db.AddInParameter(command, "@entrada", DbType.Int16, 0);
                //            db.AddInParameter(command, "@custo", DbType.Double, Convert.ToDouble(reader["custo"].ToString()));
                //            db.AddInParameter(command, "@valor", DbType.Double, Convert.ToDouble(reader["valor"].ToString()));
                //            db.AddInParameter(command, "@qtde_venda", DbType.Int16, Convert.ToInt16(reader["qtde"].ToString()));
                //            db.AddInParameter(command, "@faturamento", DbType.Double, Convert.ToDouble(reader["total"].ToString()));
                //            db.AddInParameter(command, "@qtde_dishonest", DbType.Int16, 0);
                //            db.AddInParameter(command, "@valor_dishonest", DbType.Double, 0);
                //            db.AddInParameter(command, "@qtde_combo", DbType.Int16, 0);
                //            db.AddInParameter(command, "@devolucao", DbType.Int16, 0);
                //            db.AddInParameter(command, "@perdas", DbType.Int16, 0);
                //            db.AddInParameter(command, "@estoquecd", DbType.Int16, Convert.ToInt16(reader["qtdecd"].ToString()));
                //            db.AddInParameter(command, "@estoqueloja", DbType.Int16, Convert.ToInt16(reader["qtdeloja"].ToString()));
                //            db.AddInParameter(command, "@saldo", DbType.Int16, Convert.ToInt16(reader["saldo"].ToString()));

                //            try
                //            {
                //                db.ExecuteNonQuery(command);
                //                lblMensagem.Text = "Informações atualizadas com sucesso!";
                //            }
                //            catch (Exception ex)
                //            {
                //                lblMensagem.Text = "Erro ao tentar gerar informação. " + ex.Message;
                //            }
                //            gdvDetalhes.DataBind();
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMensagem.Text = ex.Message;
                //}
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            string mesano = ddlMes.SelectedValue + "/" + ddlAnoMes.SelectedValue;

            try
            {
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                  @"select sum(qtde_venda) as qtde, max(taxa) as tx,  convert(varchar,cast(isnull(sum(faturamento),0) as decimal(10,2))) as valortotal, case when max(s.idfornecedor) is not null then (isnull(sum(faturamento),0) * 26.5) / 100 else 0 end as taxa from fechamento f
            left join split s on s.idfornecedor = f.idfornecedor
            where f.idfornecedor = '" + ddlFornecedor.SelectedValue + "' and mesano = '" + mesano + "' group by f.idfornecedor"))
                {
                    if (reader.Read())
                    {
                        gdvDetalhes.DataBind();

                        
                        lblValorImagine.Text = "R$ " + Convert.ToDecimal(reader["taxa"]).ToString("N2");
                        lblTaxa.Text = Convert.ToDecimal(reader["tx"]).ToString("N2");
                        decimal receber = Convert.ToDecimal(reader["valortotal"]) - Convert.ToDecimal(reader["taxa"]);
                        lblValorAReceber.Text = "R$ " + receber.ToString("N2");
                        lblQtdeTotal.Text = reader["qtde"].ToString();
                        lblSaldoVendas.Text = "R$ " + Convert.ToDecimal(reader["valortotal"]).ToString("N2");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }


        //public class FechamentoModel
        //{
        //    public string mesano { get; set; }
        //    public string fornecedor { get; set; }
        //    public string ean { get; set; }
        //    public string nomeproduto { get; set; }
        //    public string qtde_mes_anterior { get; set; }
        //    public string entrada { get; set; }
        //    public string valor { get; set; }
        //    public string qtde_venda { get; set; }
        //    public string faturamento { get; set; }
        //    public string qtde_dishonest { get; set; }
        //    public string valor_dishonest { get; set; }
        //    public string estoquecd { get; set; }
        //    public string estoqueloja { get; set; }
        //    public string saldo { get; set; }

        //}

        //static List<FechamentoModel> GenerateDataFromGridView(GridView gridView)
        //{
        //    var data = new List<FechamentoModel>();

        //    foreach (GridViewRow row in gridView.Rows)
        //    {
        //        var item = new FechamentoModel
        //        {
        //            mesano = row.Cells[0].Text,
        //            fornecedor = row.Cells[1].Text,
        //            ean = row.Cells[2].Text,
        //            nomeproduto = row.Cells[3].Text,
        //            qtde_mes_anterior = row.Cells[4].Text,
        //            entrada = row.Cells[5].Text,
        //            valor = row.Cells[6].Text,
        //            qtde_venda = row.Cells[7].Text,
        //            faturamento = row.Cells[8].Text,
        //            qtde_dishonest = row.Cells[9].Text,
        //            valor_dishonest = row.Cells[10].Text,
        //            estoquecd = row.Cells[11].Text,
        //            estoqueloja = row.Cells[12].Text,
        //            saldo = row.Cells[13].Text
        //        };

        //        data.Add(item);
        //    }

        //    return data;
        //}

        //protected void btnDownloadExcel_Click(object sender, EventArgs e)
        //{
        //    System.Threading.Thread.Sleep(200);

        //    try
        //    {
        //        var lista = GenerateDataFromGridView(gdvDetalhes);

        //        string fileName = "FechamentoFinanceiro.xlsx";

        //        string filePath = Path.Combine(Server.MapPath("~/assets"), fileName);

        //        GenerateExcel("Fechamento Financeiro", filePath, lista);

        //        lblMensagem.Text = "Arquivo de Excel gerado com sucesso.";

        //        Response.Clear();
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //        Response.TransmitFile(filePath);

        //        Response.End();

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensagem.Text = "Ocorreu um erro ao gerar o arquivo Excel: " + ex.Message;
        //    }
        //}

        //public void GenerateExcel(string tabName, string filePath, ICollection<FechamentoModel> lista)
        //{
        //    if (File.Exists(filePath))
        //        File.Delete(filePath);

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var planilha = workbook.Worksheets.Add(tabName);

        //        int line = 12;
        //        GenerateHeader(planilha, lblQtdeTotal.Text, lblSaldoVendas.Text, lblValorImagine.Text, lblValorAReceber.Text, Session["nomeusuario"].ToString(), ddlMes.SelectedValue + "/" + ddlAnoMes.SelectedValue);
        //        line++;

        //        foreach (var item in lista)
        //        {
        //            planilha.Cell("A" + line).Value = item.ean;
        //            planilha.Cell("B" + line).Value = item.nomeproduto.Replace("&amp;", "&");
        //            planilha.Cell("C" + line).Value = item.qtde_mes_anterior.Replace("&nbsp;", "-");
        //            planilha.Cell("D" + line).Value = item.entrada.Replace("&nbsp;", "-");
        //            planilha.Cell("E" + line).Value = item.valor.Replace("&nbsp;", "-");
        //            planilha.Cell("F" + line).Value = item.qtde_venda.Replace("&nbsp;", "-");
        //            planilha.Cell("G" + line).Value = item.faturamento.Replace("&nbsp;", "-");
        //            planilha.Cell("H" + line).Value = item.qtde_dishonest.Replace("&nbsp;", "-");
        //            planilha.Cell("I" + line).Value = item.valor_dishonest.Replace("&nbsp;", "-");
        //            planilha.Cell("J" + line).Value = item.estoquecd.Replace("&nbsp;", "-");
        //            planilha.Cell("K" + line).Value = item.estoqueloja.Replace("&nbsp;", "-");
        //            planilha.Cell("L" + line).Value = item.saldo.Replace("&nbsp;", "-");
        //            line++;
        //        }

        //        workbook.SaveAs(filePath);
        //    }

        //}

        //static void GenerateHeader(IXLWorksheet planilha, string qtdetotal, string saldovendas, string valorimagine, string areceber, string fornecedor, string mesano)
        //{
        //    planilha.Cell("A2").Value = "FECHAMENTO FINANCEIRO";
        //    planilha.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //    planilha.Cell("A2").Style.Font.Bold = true;
        //    planilha.Cell("A2").Style.Font.FontSize = 20;
        //    planilha.Range("A2:E2").Merge();

        //    planilha.Cell("A3").Value = "Fornecedor: ";
        //    planilha.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //    planilha.Cell("A3").Style.Font.Bold = true;
        //    planilha.Cell("A3").Style.Font.FontSize = 14;

        //    planilha.Cell("B3").Value = fornecedor;
        //    planilha.Cell("B3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //    planilha.Cell("B3").Style.Font.Bold = true;
        //    planilha.Cell("B3").Style.Font.FontSize = 14;

        //    planilha.Cell("A4").Value = "Mês de fechamento: ";
        //    planilha.Cell("A4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //    planilha.Cell("A4").Style.Font.Bold = true;
        //    planilha.Cell("A4").Style.Font.FontSize = 14;

        //    planilha.Cell("B4").Value = mesano;
        //    planilha.Cell("B4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //    planilha.Cell("B4").Style.Font.Bold = true;
        //    planilha.Cell("B4").Style.Font.FontSize = 14;

        //    planilha.Cell("A6").Value = "Total de Vendas: ";
        //    planilha.Cell("B6").Value = qtdetotal;
        //    planilha.Cell("A7").Value = "Saldo Vendas: ";
        //    planilha.Cell("B7").Value = saldovendas;
        //    planilha.Cell("A8").Value = "Valor Imagine(26,5%): ";
        //    planilha.Cell("B8").Value = valorimagine;
        //    planilha.Cell("A9").Value = "Valor a Receber: ";
        //    planilha.Cell("B9").Value = areceber;

        //    /* ADICIONANDO IMAGEM */
        //    planilha.Range("I2:L2").Merge();

        //    //string imagePath = @"https://imaginestore.azurewebsites.net/src/img/logo/imaginelogo.png";

        //    ////Adding image to the worksheet and moving it to the cell
        //    //var image = planilha.AddPicture(new Bitmap(imagePath)).MoveTo(planilha.Cell("I2"));
        //    //image.Name = "Logo";
        //    ////Scaling down image as per our cell size
        //    //image.Scale(.5);

        //    planilha.Cell("A12").Value = "EAN";
        //    planilha.Cell("A12").Style.Font.Bold = true;
        //    planilha.Cell("A12").Style.Font.FontSize = 14;
        //    planilha.Cell("B12").Value = "Produto";
        //    planilha.Cell("B12").Style.Font.Bold = true;
        //    planilha.Cell("B12").Style.Font.FontSize = 14;
        //    planilha.Cell("C12").Value = "Quant. Mês Anterior";
        //    planilha.Cell("C12").Style.Font.Bold = true;
        //    planilha.Cell("C12").Style.Font.FontSize = 14;
        //    planilha.Cell("D12").Value = "Entrada";
        //    planilha.Cell("D12").Style.Font.Bold = true;
        //    planilha.Cell("D12").Style.Font.FontSize = 14;
        //    planilha.Cell("E12").Value = "Valor";
        //    planilha.Cell("E12").Style.Font.Bold = true;
        //    planilha.Cell("E12").Style.Font.FontSize = 14;
        //    planilha.Cell("F12").Value = "Quant. Venda";
        //    planilha.Cell("F12").Style.Font.Bold = true;
        //    planilha.Cell("F12").Style.Font.FontSize = 14;
        //    planilha.Cell("G12").Value = "Valor Venda";
        //    planilha.Cell("G12").Style.Font.Bold = true;
        //    planilha.Cell("G12").Style.Font.FontSize = 14;
        //    planilha.Cell("H12").Value = "Quant. Dishonest";
        //    planilha.Cell("H12").Style.Font.Bold = true;
        //    planilha.Cell("H12").Style.Font.FontSize = 14;
        //    planilha.Cell("I12").Value = "Valor Dishonest";
        //    planilha.Cell("I12").Style.Font.Bold = true;
        //    planilha.Cell("I12").Style.Font.FontSize = 14;
        //    planilha.Cell("J12").Value = "Estoque CD";
        //    planilha.Cell("J12").Style.Font.Bold = true;
        //    planilha.Cell("J12").Style.Font.FontSize = 14;
        //    planilha.Cell("K12").Value = "Estoque Loja";
        //    planilha.Cell("K12").Style.Font.Bold = true;
        //    planilha.Cell("K12").Style.Font.FontSize = 14;
        //    planilha.Cell("L12").Value = "Saldo";
        //    planilha.Cell("L12").Style.Font.Bold = true;
        //    planilha.Cell("L12").Style.Font.FontSize = 14;

        //    //ajusta o tamanho da coluna com o conteudo
        //    planilha.Columns("A", "L").AdjustToContents();
        //}

        //protected void btnDownloadPDf_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnDownloadCSV_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var lista = GenerateDataFromGridView(gdvDetalhes);

        //        string fileName = "FechamentoFinanceiro.csv";

        //        string filePath = Path.Combine(Server.MapPath("~/assets"), fileName);

        //        GenerateCSV(filePath, lista);

        //        lblMensagem.Text = "Arquivo CSV gerado com sucesso.";

        //        Response.Clear();
        //        Response.ContentType = "text/csv";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //        Response.TransmitFile(filePath);
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensagem.Text = "Ocorreu um erro ao gerar o arquivo CSV: " + ex.Message;
        //    }
        //}

        //static void GenerateCSV(string filePath, ICollection<FechamentoModel> lista)
        //{
        //    if (File.Exists(filePath))
        //        File.Delete(filePath);

        //    using (StreamWriter writer = new StreamWriter(filePath))
        //    {

        //        writer.WriteLine("mesano,fornecedor,ean,nomeproduto,qtde_mes_anterior,entrada,valor,qtde_venda,faturamento,qtde_dishonest,valor_dishonest,estoquecd,estoqueloja,saldo");

        //        foreach (var item in lista)
        //        {
        //            writer.WriteLine($"{item.mesano},{item.fornecedor},{item.ean},{item.nomeproduto},{item.qtde_mes_anterior},{item.entrada},{item.valor},{item.qtde_venda},{item.faturamento},{item.qtde_dishonest},{item.valor_dishonest},{item.estoquecd},{item.estoqueloja},{item.saldo}");
        //        }
        //    }
        //}
    }
}