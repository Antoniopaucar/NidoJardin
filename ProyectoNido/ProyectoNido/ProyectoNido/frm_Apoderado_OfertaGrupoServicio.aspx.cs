using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_Apoderado_OfertaGrupoServicio : System.Web.UI.Page
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
                CargarOfertas();
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
        /// Carga las ofertas de grupos de servicio del periodo actual
        /// </summary>
        private void CargarOfertas()
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener todas las ofertas del periodo actual
                var ofertas = servicio.ListarOfertasGrupoServicio();
                
                System.Diagnostics.Debug.WriteLine($"Ofertas obtenidas: {ofertas?.Count() ?? 0}");
                
                if (ofertas != null && ofertas.Any())
                {
                    rptOfertas.DataSource = ofertas;
                    rptOfertas.DataBind();
                    rptOfertas.Visible = true;
                    pnlSinOfertas.Visible = false;
                }
                else
                {
                    rptOfertas.Visible = false;
                    pnlSinOfertas.Visible = true;
                    System.Diagnostics.Debug.WriteLine("No se encontraron ofertas. Posibles causas:");
                    System.Diagnostics.Debug.WriteLine("1. No hay grupos de servicio creados para el periodo actual");
                    System.Diagnostics.Debug.WriteLine("2. El stored procedure sp_ListarOfertasGrupoServicio no está ejecutado");
                    System.Diagnostics.Debug.WriteLine("3. No hay servicios adicionales con grupos asociados");
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                System.Diagnostics.Debug.WriteLine($"FaultException al cargar ofertas: {fex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {fex.StackTrace}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorOfertas", 
                    $"alert('Error al cargar ofertas: {fex.Message.Replace("'", "\\'")}');", true);
                rptOfertas.Visible = false;
                pnlSinOfertas.Visible = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar ofertas: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorOfertas", 
                    $"alert('Error al cargar ofertas: {ex.Message.Replace("'", "\\'")}');", true);
                rptOfertas.Visible = false;
                pnlSinOfertas.Visible = true;
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