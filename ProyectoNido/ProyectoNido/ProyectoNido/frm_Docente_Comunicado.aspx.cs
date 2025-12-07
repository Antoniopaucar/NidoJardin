using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace ProyectoNido
{
    public partial class frm_Docente_Comunicado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    CargarComunicados();
            //    CargarDatosDocente();
            //}
        }

        private void CargarDatosDocente()
        {
            //if (Session["IdUsuario"] != null)
            //{
            //    try
            //    {
            //        int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
            //        wcfNido.Service1Client servicio = new wcfNido.Service1Client();
            //        wcfNido.clsUsuario docente = servicio.ObtenerDatosDocente(idUsuario);

            //        if (docente != null)
            //        {
            //            lblNombreDocente.Text = $"{docente.Nombres} {docente.ApellidoPaterno} {docente.ApellidoMaterno}";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        string script = $"console.error('Error al cargar datos del docente: {ex.Message}');";
            //        ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            //    }
            //}
        }

        private void CargarComunicados()
        {
            //if (Session["IdUsuario"] != null)
            //{
            //    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
            //    wcfNido.Service1Client client = new wcfNido.Service1Client();
            //    var lista = client.GetComunicado(idUsuario);
            //    rptComunicados.DataSource = lista;
            //    rptComunicados.DataBind();
            //}
        }

        [WebMethod]
        public static void MarcarComoVisto(int idComunicado)
        {
            //if (HttpContext.Current.Session["IdUsuario"] != null)
            //{
            //    int idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            //    wcfNido.Service1Client client = new wcfNido.Service1Client();
            //    client.MarcarComunicadoVisto(idComunicado, idUsuario);
            //}
        }
    }
}