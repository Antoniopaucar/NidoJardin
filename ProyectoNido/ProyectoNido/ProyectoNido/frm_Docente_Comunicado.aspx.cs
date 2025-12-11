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
    public partial class frm_Docente_Comunicado : System.Web.UI.Page
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

                CargarNombreDocente();
                CargarListaComunicados();
            }
        }

        /// <summary>
        /// Carga el nombre del docente en el panel lateral
        /// </summary>
        private void CargarNombreDocente()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener datos del profesor
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

        /// <summary>
        /// Carga la lista de comunicados dirigidos a los roles del usuario
        /// </summary>
        private void CargarListaComunicados()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Usar el nuevo método que filtra por rol del usuario
                var listaComunicados = servicio.GetComunicadoPorRolUsuario(idUsuario);
                
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
    }
}