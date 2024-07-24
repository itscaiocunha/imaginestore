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
                string queryGanhoQtde = $@"
                    SELECT ISNULL(SUM(TRY_CAST(v.value AS DECIMAL(10, 2))), 0) AS ganho, 
                           COUNT(*) AS qtde 
                    FROM vendas v 
                    WHERE manufacturer_id = '{manufacturerId}' AND status = 'OK' {filtrodata}";

                string queryTicket = $@"
                    SELECT SUM(CAST(value AS DECIMAL(10, 2))) / COUNT(*) AS ticket 
                    FROM vendas 
                    WHERE manufacturer_id = '{manufacturerId}' AND status = 'OK'";

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

        protected void ddlCategoria_DataBound(object sender, EventArgs e)
        {
            ddlCategoria.Items.Insert(0, new ListItem("TODOS", "0"));
            ddlProduto.Items.Insert(0, new ListItem("TODOS", "0"));
        }
    }
}
