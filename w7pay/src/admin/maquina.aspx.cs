using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src
{
    public partial class maquina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try { 
                //hdfIdEmpresa.Value = Session["idempresa"].ToString();
                }
                catch
                {
                    Response.Redirect("../sessao.aspx", false);
                }

                //atualizacao.GETMaquinas();
            }
        }

        protected void lkbFiltro_Click(object sender, EventArgs e)
        {
            sdsDados.SelectCommand = "select m.id, asset_number, l.name as nomelocal, cash_mode from maquinas m left join locais l on l.id = m.location_id where asset_number like '%" + txtBuscar.Text + "%' order by l.name, m.asset_number";
            gdvDados.DataBind();
        }
    }
}