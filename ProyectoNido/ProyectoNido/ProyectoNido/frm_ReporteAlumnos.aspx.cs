using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_ReporteAlumnos : System.Web.UI.Page
    {
        wcfNido.Service1Client xdb = new wcfNido.Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("frm_Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarNombreDocente();
                CargarAlumnosActivos();
            }
        }

        private void CargarNombreDocente()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                Service1Client servicio = new Service1Client();

                var listaProfesores = servicio.GetProfesor();
                var profesor = listaProfesores.FirstOrDefault(p => p.Id == idUsuario);

                if (profesor != null)
                {
                    lblNombreDocente.Text = $"{profesor.Nombres} {profesor.ApellidoPaterno}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar nombre docente: {ex.Message}");
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