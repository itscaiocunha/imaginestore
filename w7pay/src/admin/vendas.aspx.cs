using Microsoft.Practices.EnterpriseLibrary.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace w7pay.src
{
    public partial class vendas2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }

                //atualizacao.GETVendas();
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {

            DateTime datainicio, datafim;
            if (DateTime.TryParseExact(txtDataInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datainicio) &&
                DateTime.TryParseExact(txtDataFim.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datafim))
            {

                sdsDados.SelectCommand =
                    "select f.name, l.name, c.name, m.asset_number, ct.descricao, p.name, coil, quantity, value, occurred_at from vendas v (nolock)" +
                    "join clientes c on c.id = v.client_id" +
                    "join locais l on l.id = v.location_id" +
                    "join maquinas m on m.id = v.machine_id" +
                    "join produtos p on p.id = v.good_id" +
                    "join fornecedores f on f.id = p.manufacturer_id" +
                    "join categorias ct on ct.id = p.category_id" +
                    "where occurred_at >= '" + datainicio + "' and occurred_at <= '" + datafim + "'" + " order by f.name, l.name, c.name, m.asset_number, ct.descricao, p.name";
                gdvDados.DataBind();
            }
        }
    }
}