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

                CargarNombreDocente();
                CargarGrupos();
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
        /// Carga los grupos anuales asignados al docente
        /// </summary>
        private void CargarGrupos()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                var grupos = servicio.ListarGruposPorDocente(idUsuario);

                if (grupos != null && grupos.Any())
                {
                    rptGrupos.DataSource = grupos;
                    rptGrupos.DataBind();
                    rptGrupos.Visible = true;
                    pnlSinGrupos.Visible = false;
                }
                else
                {
                    rptGrupos.Visible = false;
                    pnlSinGrupos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar grupos: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorGrupos", 
                    $"alert('Error al cargar grupos: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// WebMethod llamado desde JavaScript para obtener alumnos por grupo anual
        /// </summary>
        [WebMethod(EnableSession = true)]
        public static List<wcfNido.clsAlumno> ObtenerAlumnosPorGrupo(int idGrupoAnual)
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                var alumnos = servicio.ListarAlumnosPorGrupoAnual(idGrupoAnual);
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