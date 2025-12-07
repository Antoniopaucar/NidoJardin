using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Docente_GrupoAnual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar sesión
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("frm_Login.aspx");
                    return;
                }

                CargarDatosDocente();
                CargarGrupos();
            }
        }

        private void CargarDatosDocente()
        {
            //try
            //{
            //    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
            //    wcfNido.Service1Client servicio = new wcfNido.Service1Client();
            //    wcfNido.clsUsuario docente = servicio.ObtenerDatosDocente(idUsuario);

            //    if (docente != null)
            //    {
            //        lblNombreDocente.Text = $"{docente.Nombres} {docente.ApellidoPaterno} {docente.ApellidoMaterno}";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Manejo silencioso o log
            //    System.Diagnostics.Debug.WriteLine("Error al cargar docente: " + ex.Message);
            //}
        }

        private void CargarGrupos()
        {
            //try
            //{
            //    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
            //    wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
            //    var grupos = servicio.ListarGruposPorDocente(idUsuario);

            //    if (grupos != null && grupos.Count() > 0)
            //    {
            //        rptGrupos.DataSource = grupos;
            //        rptGrupos.DataBind();
            //        pnlSinGrupos.Visible = false;
            //    }
            //    else
            //    {
            //        rptGrupos.Visible = false;
            //        pnlSinGrupos.Visible = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string script = $"console.error('Error al cargar grupos: {ex.Message}');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            //}
        }
    }
}