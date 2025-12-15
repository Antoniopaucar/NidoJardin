using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_ReporteAlumnos : System.Web.UI.Page
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

                CargarAlumnosActivos();
            }
        }

        private void CargarAlumnosActivos()
        {
            try
            {
                // Uso del nuevo metodo que utiliza el SP especifico
                var alumnosActivos = xdb.ListarAlumnosActivos();
                
                gvAlumnos.DataSource = alumnosActivos;
                gvAlumnos.DataBind();

                lblTotal.Text = alumnosActivos.Length.ToString();

                if (alumnosActivos.Length == 0)
                {
                    // pnlSinResultados.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Manejar error si es necesario
                Response.Write("<script>alert('Error al cargar alumnos: " + ex.Message + "');</script>");
            }
        }
    }
}