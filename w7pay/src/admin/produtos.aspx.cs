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

namespace w7pay.src
{
    public partial class produtos : System.Web.UI.Page
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
                atualizacao.GETProdutos();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if(txtBuscar.Text.Length > 3)
            {
                sdsDados.SelectCommand = "select top 100 p.image, p.id, p.type, p.name, c.descricao, p.barcode, p.cost_price, f.name as nameF from produtos p left join categorias c on c.id = p.category_idjoin fornecedores f on f.id = p.manufacturer_id where p.cost_price > 0 and p.name like '%"+txtBuscar.Text+"%' order by p.name";
                gdvDados.DataBind();
            }
        }
    }
}