using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_Apoderado_Comunicado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si hay sesión de usuario
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("frm_Login.aspx");
                    return;
                }

                CargarNombreApoderado();
                CargarHijos();
                CargarListaComunicados();
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
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener datos del apoderado
                var apoderado = servicio.ObtenerApoderadoPorId(idUsuario);
                
                if (apoderado != null)
                {
                    lblNombreDocente.Text = $"{apoderado.Nombres} {apoderado.ApellidoPaterno}";
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
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
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
                // No mostrar error al usuario, simplemente no mostrar hijos
            }
        }

        /// <summary>
        /// Carga la lista de comunicados dirigidos específicamente al rol APODERADO
        /// </summary>
        private void CargarListaComunicados()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Usar el método específico para apoderados que usa el SP listar_comunicados_por_rol_usuario_Apoderado
                var listaComunicados = servicio.GetComunicadoPorRolUsuarioApoderado(idUsuario);
                
                if (listaComunicados != null && listaComunicados.Any())
                {
                    rptComunicados.DataSource = listaComunicados;
                    rptComunicados.DataBind();
                }
                else
                {
                    // Mostrar mensaje cuando no hay comunicados
                    rptComunicados.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "sinComunicados", 
                        "alert('No hay comunicados disponibles.');", true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar comunicados: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorComunicados", 
                    $"alert('Error al cargar comunicados: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// WebMethod llamado desde JavaScript para marcar un comunicado como visto
        /// REUTILIZA el mismo método que usa el docente
        /// </summary>
        [WebMethod(EnableSession = true)]
        public static string MarcarComoVisto(int idComunicado)
        {
            try
            {
                if (HttpContext.Current.Session["IdUsuario"] != null)
                {
                    int idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
                    wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                    // REUTILIZA: Usar el mismo método WCF que usa el docente
                    servicio.MarcarComunicadoVisto(idComunicado, idUsuario);
                    return "OK";
                }
                return "SIN_SESION";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
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
                
                // Redirigir a la página de hijos para ver y editar los datos del hijo seleccionado
                Response.Redirect("frm_Apoderado_hijos.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al seleccionar hijo: {ex.Message}");
            }
        }
    }
}