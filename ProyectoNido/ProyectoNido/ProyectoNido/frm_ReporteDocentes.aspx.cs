using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_ReporteDocentes : System.Web.UI.Page
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
                CargarDocentesActivos();
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