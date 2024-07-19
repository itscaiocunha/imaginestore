using ClosedXML.Excel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using w7pay.src.cliente;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Text;using iTextSharp.text;
using iTextSharp.text.pdf;
using iText.StyledXmlParser.Jsoup.Nodes;
using Document = iTextSharp.text.Document;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

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

                    using (IDataReader reader1 = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                "select count(*) as qtde, sum(value) as valortotal from vendas where manufacturer_id = '" + hdfId.Value + "' and month(occurred_at) = '" + ddlMes.SelectedValue + "' and year(occurred_at) = '" + ddlAnoMes.SelectedValue + "'  and status = 'OK'"))
                    {
                        if (reader1.Read())
                        {
                            lblQtdeTotal.Text = reader1["qtde"].ToString();
                            lblSaldoVendas.Text = "R$ " + Convert.ToDecimal(reader1["valortotal"]).ToString("N2");
                        }
                        else
                        {
                            lblQtdeTotal.Text = "0";
                        }
                    }

                    using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text,
                                      @"select  count(f.qtde_venda) as qtde, max(taxa) as tx, cast(isnull(sum(faturamento),0) as decimal(10,2)) as valortotal, case when max(s.idfornecedor) is not null then cast((isnull(sum(faturamento),0) * max(s.taxa)) / 100  as Decimal(10,2)) else 0 end as taxa from fechamento f
                        left join split s on s.idfornecedor = f.idfornecedor
                        where f.idfornecedor = '" + hdfId.Value + "' and mesano = '" + mesano + "' group by f.idfornecedor"))
                    {
                        if (reader.Read())
                        {
                            gdvDetalhes.DataBind();
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
                        lblValorAReceber.Text = "R"+receber.ToString("c2");
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
        //        GenerateHeader(planilha, lblQtdeTotal.Text, lblSaldoVendas.Text, lblValorImagine.Text, lblValorAReceber.Text, Session["nomeusuario"].ToString(), ddlMes.SelectedValue+"/"+ddlAnoMes.SelectedValue);
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

        //protected void btnDownloadPDF_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var lista = GenerateDataFromGridView(gdvDetalhes);

        //        string fileName = "FechamentoFinanceiro.pdf";

        //        string filePath = Path.Combine(Server.MapPath("~/assets"), fileName);

        //        GeneratePDF(filePath, lista);

        //        lblMensagem.Text = "Arquivo PDF gerado com sucesso.";

        //        Response.Clear();
        //        Response.ContentType = "application/pdf";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //        Response.TransmitFile(filePath);
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensagem.Text = "Ocorreu um erro ao gerar o arquivo PDF: " + ex.Message;
        //    }
        //}

        //static void GeneratePDF(string filePath, ICollection<FechamentoModel> lista)
        //{
        //    iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
        //    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        //    document.Open();

        //    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Fechamento Financeiro");
        //    title.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
        //    document.Add(title);

        //    PdfPTable table = new PdfPTable(14);

        //    foreach (var property in typeof(FechamentoModel).GetProperties())
        //    {
        //        table.AddCell(new PdfPCell(new Phrase(property.Name)));
        //    }

        //    foreach (var item in lista)
        //    {
        //        foreach (var property in typeof(FechamentoModel).GetProperties())
        //        {
        //            table.AddCell(new PdfPCell(new Phrase(property.GetValue(item)?.ToString())));
        //        }
        //    }

        //    document.Add(table);
        //    document.Close();
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
