using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_ReporteDocentes : System.Web.UI.Page
    {
        wcfNido.Service1Client xdb = new wcfNido.Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Nombres"] != null)
                {
                    lblNombreDocente.Text = Session["Nombres"].ToString();
                }

                CargarDocentesActivos();
            }
        }

        private void CargarDocentesActivos()
        {
            try
            {
                // Uso del nuevo metodo que filtra por SP
                var docentesActivos = xdb.ListarProfesoresActivos();
                
                gvDocentes.DataSource = docentesActivos;
                gvDocentes.DataBind();

                lblTotal.Text = docentesActivos.Length.ToString();
            }
            catch (Exception ex)
            {
                // Manejar error
                 Response.Write("<script>alert('Error al cargar docentes: " + ex.Message + "');</script>");
            }
        }
    }
}