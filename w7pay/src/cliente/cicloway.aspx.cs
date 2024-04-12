using InterAPI.Model;
using InterAPI.Service;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace w7pay.src.cliente
{
    public partial class cicloway : System.Web.UI.Page
    {
        public static readonly string PathCert = ConfigurationManager.AppSettings["PathCert"];
        public static readonly string PassCert = ConfigurationManager.AppSettings["PassCert"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AccessToken tk = new AccessToken();
                tk = SOAuth.GetToken(null);
                var token = tk.Access_token;
                hdfToken.Value = token;
            }
        }

        protected void gdvCarrinhoFinal_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

        }

        protected void btnCupom_Click(object sender, EventArgs e)
        {

        }

        protected void btnAtualizarCarrinho_Click(object sender, EventArgs e)
        {

        }

        protected void lkbCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx", false);
        }

        protected void btn5minutos_Click(object sender, EventArgs e)
        {
            InterAPI.Model.Pix dadospix = new InterAPI.Model.Pix();
            dadospix.chave = "19881867000121";
            dadospix.solicitacaoPagador = "Pagamento compra realizado Imagine Store";

            InterAPI.Model.PixValor pixvalor = new InterAPI.Model.PixValor();
            pixvalor.modalidadeAlteracao = 1;
            pixvalor.original = Convert.ToDecimal(1).ToString();

            InterAPI.Model.PixDevedor pixdev = new InterAPI.Model.PixDevedor();

            pixdev.cpf = "37035929827";//cpf do administrador            
            pixdev.nome = "Cliente Consumidor";

            InterAPI.Model.PixCalendario pixcalend = new InterAPI.Model.PixCalendario();
            pixcalend.criacao = DateTime.Now;
            pixcalend.expiracao = 3600;

            InterAPI.Model.PixInfoAdicional pixinfo = new InterAPI.Model.PixInfoAdicional();
            pixinfo.nome = "Pagamento de compra realizada Imagine Store";
            pixinfo.valor = "1";

            InterAPI.Model.Pixloc pixloc = new InterAPI.Model.Pixloc();
            pixloc.tipoCob = "cob";

            dadospix.devedor = pixdev;
            dadospix.loc = pixloc;
            //dadospix.infoAdicionais[0] = pixinfo;
            dadospix.valor = pixvalor;
            dadospix.calendario = pixcalend;

            //dadospix.infoAdicionais[0].nome = "Pagamento do serviço via plataforma W7 Pay";
            //dadospix.infoAdicionais[0].valor = "1";

            Pix retornopix = new Pix();
            try
            {
                //var certificates = new X509Certificate2();
                //X509KeyStorageFlags fleg = new X509KeyStorageFlags();
                //certificates.Import(PathCert, "0525", fleg);

                retornopix = SPixImediato.Post(dadospix, "71824669", hdfToken.Value, "cpf");
                var status = retornopix.status;
                var txid = retornopix.txid;
                var copiacola = retornopix.pixCopiaECola;
                lblChaveCopiaCola.Text = copiacola;
                lblTxid.Text = txid;
                imgQRCode.Visible = true;
                imgQRCode.ImageUrl = "https://gerarqrcodepix.com.br/api/v1?brcode=" + lblChaveCopiaCola.Text + "&tamanho=256";

            }
            catch (Exception ex)
            {
                lblMensagemPix.Text = ex.Message;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}