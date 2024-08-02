using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class vendas2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitialDates();
                LoadData();
            }
        }

        private void SetInitialDates()
        {
            txtDataInicio.Text = DateTime.Now.Date.AddDays(-7).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR"));
            txtDataFim.Text = DateTime.UtcNow.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        private void LoadData(string filtrodata = "")
        {
            try
            {
                string manufacturerId = ddlFornecedores.SelectedValue;

                // Ajuste as consultas SQL para incluir o filtro corretamente
                string queryGanhoQtde = $@"
        SELECT ISNULL(SUM(TRY_CAST(value AS DECIMAL(10, 2))), 0) AS ganho, 
               COUNT(*) AS qtde 
        FROM vendas 
        WHERE status = 'OK' {filtrodata}";

                string queryTicket = $@"
            SELECT SUM(CAST(value AS DECIMAL(10, 2))) / COUNT(*) AS ticket 
            FROM vendas 
            WHERE status = 'OK'";

                string queryVendasDia = $@"
            SELECT
                CAST(occurred_at AS DATE) AS Data,
                COUNT(*) AS Quantidade_Vendas
            FROM    
                vendas
            WHERE 
                occurred_at >= DATEADD(DAY, -7, GETDATE())
            GROUP BY 
                CAST(occurred_at AS DATE)
            ORDER BY 
                Data;";

                string queryItensDia = $@"
            SELECT top(10)
                CAST(occurred_at AS DATE) AS Data,
                product_name,
                COUNT(*) AS Quantidade_Itens_Vendidos
            FROM   
                vendas
            WHERE 
                occurred_at >= DATEADD(DAY, -1, GETDATE())
            GROUP BY 
                CAST(occurred_at AS DATE),
                product_name
            ORDER BY   
                Data,
                Quantidade_Itens_Vendidos desc";

                string queryTicketMedio = $@"
            SELECT 
                CAST(occurred_at AS DATE) AS Data,
                SUM(value) / COUNT(*) AS Ticket_Medio
            FROM 
                vendas      
            WHERE 
                occurred_at >= DATEADD(DAY, -7, GETDATE())
            GROUP BY 
                CAST(occurred_at AS DATE)
            ORDER BY 
                Data;";

                string queryLojaDia = $@"
            SELECT top(10)
                CAST(occurred_at AS DATE) AS Data,
                client_name,
                SUM(value) AS faturamento
            FROM 
                vendas
            WHERE 
                occurred_at >= DATEADD(DAY, -1, GETDATE())
            GROUP BY 
                CAST(occurred_at AS DATE),
                client_name
            ORDER BY 
                Data,
                faturamento DESC;";

                string queryCompraClienteDia = $@"
            SELECT top(10)
                CAST(occurred_at AS DATE) AS Data,
                client_name,
                COUNT(*) AS compras
            FROM 
                vendas
            WHERE 
                occurred_at >= DATEADD(DAY, -1, GETDATE())
            GROUP BY 
                CAST(occurred_at AS DATE),
                client_name
            ORDER BY 
                Data,
                compras DESC;";

                string queryComprasCliente = $@"
            SELECT top(10)
                client_name,
                COUNT(*) AS Numero_Compras
            FROM 
                vendas
            GROUP BY 
                client_name    
            ORDER BY 
                Numero_Compras DESC;";

                // Executar e atualizar os labels
                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, queryGanhoQtde))
                {
                    if (reader.Read())
                    {
                        lblVendas.Text = "R$ " + reader["ganho"].ToString();
                        lblQtde.Text = reader["qtde"].ToString();
                    }
                    else
                    {
                        lblVendas.Text = "R$ 0,00";
                        lblQtde.Text = "0";
                    }
                }

                using (IDataReader reader = DatabaseFactory.CreateDatabase("ConnectionString").ExecuteReader(CommandType.Text, queryTicket))
                {
                    if (reader.Read())
                    {
                        lblTicket.Text = "R$ " + reader["ticket"].ToString();
                    }
                    else
                    {
                        lblTicket.Text = "0";
                    }
                }

                // Atualize os DataSources e gráficos
                sdsDados.SelectCommand = queryVendasDia;
                sdsDados.DataBind();

                //SqlDataSource1.SelectCommand = queryItensDia;
                //SqlDataSource1.DataBind();

                SqlDataSource2.SelectCommand = queryTicketMedio;
                SqlDataSource2.DataBind();

                //SqlDataSource3.SelectCommand = queryLojaDia;
                //SqlDataSource3.DataBind();

                //SqlDataSource4.SelectCommand = queryCompraClienteDia;
                //SqlDataSource4.DataBind();

                SqlDataSource5.SelectCommand = queryComprasCliente;
                SqlDataSource5.DataBind();

            }
            catch (Exception ex)
            {
                lblErro.Text = "Erro: " + ex.Message;
            }
        }


        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            try
            {
                DateTime datainicio = DateTime.ParseExact(txtDataInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime datafim = DateTime.ParseExact(txtDataFim.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string filtrodata = $" AND occurred_at >= '{datainicio:yyyy-MM-dd}' AND occurred_at <= '{datafim:yyyy-MM-dd}' ";
                LoadData(filtrodata);
            }
            catch (Exception ex)
            {
                lblErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void lkbLimpar_Click(object sender, EventArgs e)
        {
            SetInitialDates();
            LoadData();
        }

        //protected void ddlCategoria_DataBound(object sender, EventArgs e)
        //{
        //    ddlCategoria.Items.Insert(0, new ListItem("TODOS", "0"));        
        //}
    }
}
