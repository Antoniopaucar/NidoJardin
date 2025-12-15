using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_HistorialServicio_PorHijo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si hay sesión de usuario
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("frm_Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarNombreApoderado();
                CargarHijos();
                CargarHistorialServicios();
            }
        }

        /// <summary>
        /// Carga el nombre del apoderado en el panel lateral
        /// </summary>
        private void CargarNombreApoderado()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                Service1Client servicio = new Service1Client();

                var apoderado = servicio.ObtenerApoderadoPorId(idUsuario);

                if (apoderado != null)
                {
                    lblNombreApoderado.Text = $"{apoderado.Nombres} {apoderado.ApellidoPaterno}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar nombre apoderado: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga la lista de hijos (alumnos) del apoderado
        /// </summary>
        private void CargarHijos()
        {
            try
            {
                int idApoderado = Convert.ToInt32(Session["IdUsuario"]);
                Service1Client servicio = new Service1Client();

                var hijos = servicio.ListarAlumnosPorApoderado(idApoderado);

                if (hijos != null && hijos.Any())
                {
                    rptHijos.DataSource = hijos;
                    rptHijos.DataBind();
                }
                else
                {
                    rptHijos.DataSource = new List<wcfNido.clsAlumno>();
                    rptHijos.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar hijos: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga el historial de servicios adicionales del hijo seleccionado
        /// </summary>
        private void CargarHistorialServicios()
        {
            try
            {
                // Obtener ID del alumno desde QueryString o Session
                int idAlumno = 0;

                if (Request.QueryString["idAlumno"] != null)
                {
                    idAlumno = Convert.ToInt32(Request.QueryString["idAlumno"]);
                    Session["IdAlumnoSeleccionado"] = idAlumno;
                }
                else if (Session["IdAlumnoSeleccionado"] != null)
                {
                    idAlumno = Convert.ToInt32(Session["IdAlumnoSeleccionado"]);
                }

                if (idAlumno <= 0)
                {
                    pnlSinServicios.Visible = true;
                    pnlServicios.Visible = false;
                    return;
                }

                Service1Client servicio = new Service1Client();

                // Obtener nombre del hijo
                var hijos = servicio.ListarAlumnosPorApoderado(Convert.ToInt32(Session["IdUsuario"]));
                var hijo = hijos?.FirstOrDefault(h => h.Id == idAlumno);
                if (hijo != null)
                {
                    lblNombreHijo.Text = $"{hijo.Nombres} {hijo.ApellidoPaterno} {hijo.ApellidoMaterno}";
                }

                // Obtener servicios adicionales del alumno
                var listaServicios = servicio.ListarServicioAlumnoPorAlumno(idAlumno);

                if (listaServicios != null && listaServicios.Any())
                {
                    rptServicios.DataSource = listaServicios;
                    rptServicios.DataBind();
                    pnlServicios.Visible = true;
                    pnlSinServicios.Visible = false;
                }
                else
                {
                    pnlSinServicios.Visible = true;
                    pnlServicios.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar historial de servicios: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorHistorial",
                    $"alert('Error al cargar historial de servicios: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Maneja el evento click de los botones de hijos
        /// Redirige a frm_Apoderado_hijos.aspx para ver y editar los datos del hijo
        /// </summary>
        protected void btnHijo_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idAlumno = Convert.ToInt32(btn.CommandArgument);

                // Guardar el ID del alumno seleccionado en sesión
                Session["IdAlumnoSeleccionado"] = idAlumno;

                // Redirigir inmediatamente a la página de hijos para ver y editar los datos del hijo seleccionado
                // endResponse: false con CompleteRequest() asegura que no se ejecute código adicional después de la redirección
                Response.Redirect("frm_Apoderado_hijos.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al seleccionar hijo: {ex.Message}");
            }
        }
    }
}