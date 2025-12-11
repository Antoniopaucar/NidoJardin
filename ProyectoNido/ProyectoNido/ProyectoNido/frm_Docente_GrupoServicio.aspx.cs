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
    public partial class frm_Docente_GrupoServicio : System.Web.UI.Page
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

                CargarNombreDocente();
                CargarGruposServicio();
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
                System.Diagnostics.Debug.WriteLine("Error al cargar nombre docente: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga los grupos de servicio asignados al docente
        /// </summary>
        private void CargarGruposServicio()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                var grupos = servicio.ListarGruposServicioPorDocente(idUsuario);

                if (grupos != null && grupos.Any())
                {
                    rptGruposServicio.DataSource = grupos;
                    rptGruposServicio.DataBind();
                    rptGruposServicio.Visible = true;
                    pnlSinGrupos.Visible = false;
                }
                else
                {
                    rptGruposServicio.Visible = false;
                    pnlSinGrupos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGruposServicio", 
                    $"alert('Error al cargar grupos de servicio: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// WebMethod llamado desde JavaScript para obtener alumnos por grupo de servicio
        /// </summary>
        [WebMethod(EnableSession = true)]
        public static List<wcfNido.clsAlumno> ObtenerAlumnosPorGrupoServicio(int idGrupoServicio)
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                var alumnos = servicio.ListarAlumnosPorGrupoServicio(idGrupoServicio);
                return alumnos != null ? alumnos.ToList() : new List<wcfNido.clsAlumno>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener alumnos: {ex.Message}");
                throw;
            }
        }
    }
}