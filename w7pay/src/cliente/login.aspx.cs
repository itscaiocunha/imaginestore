using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Web;
using System.Web.UI;

namespace w7pay.src.cliente
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void lkbAcessar_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx", false);
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {

        }
    }
}