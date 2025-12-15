using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_CronogramaPago_PorHijo : System.Web.UI.Page
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
                CargarCronograma();
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
        /// Carga el cronograma de pagos del hijo seleccionado
        /// </summary>
        private void CargarCronograma()
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
                    pnlSinMatricula.Visible = true;
                    pnlCronograma.Visible = false;
                    pnlSinCuotas.Visible = false;
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

                // Obtener matrícula actual del alumno
                var matricula = servicio.ObtenerMatriculaActualPorAlumno(idAlumno);

                if (matricula == null || matricula.Id_Matricula <= 0)
                {
                    pnlSinMatricula.Visible = true;
                    pnlCronograma.Visible = false;
                    pnlSinCuotas.Visible = false;
                    return;
                }

                // Obtener resumen de cuotas
                var resumen = servicio.ResumenCuotasPorMatricula(matricula.Id_Matricula);
                if (resumen != null)
                {
                    lblTotal.Text = $"S/ {resumen.Total:N2}";
                    lblPagado.Text = $"S/ {resumen.Pagado:N2}";
                    lblPendiente.Text = $"S/ {resumen.Pendiente:N2}";
                }

                // Obtener detalle de cuotas
                var listaCuotas = servicio.ListarCuotasPorMatricula(matricula.Id_Matricula);

                if (listaCuotas != null && listaCuotas.Any())
                {
                    rptCronograma.DataSource = listaCuotas;
                    rptCronograma.DataBind();
                    pnlCronograma.Visible = true;
                    pnlSinCuotas.Visible = false;
                    pnlSinMatricula.Visible = false;
                }
                else
                {
                    pnlSinCuotas.Visible = true;
                    pnlCronograma.Visible = false;
                    pnlSinMatricula.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar cronograma: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCronograma",
                    $"alert('Error al cargar cronograma: {ex.Message.Replace("'", "\\'")}');", true);
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
                // endResponse: true asegura que no se ejecute código adicional después de la redirección
                Response.Redirect("frm_Apoderado_hijos.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al seleccionar hijo: {ex.Message}");
            }
        }

        /// <summary>
        /// Maneja el click en "Cronograma de Pagos"
        /// </summary>
        protected void lnkCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                int idAlumno = Convert.ToInt32(lnk.CommandArgument);

                Session["IdAlumnoSeleccionado"] = idAlumno;
                Response.Redirect($"frm_CronogramaPago_PorHijo.aspx?idAlumno={idAlumno}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al abrir cronograma: {ex.Message}");
            }
        }

        /// <summary>
        /// Maneja el click en "Ver Historial de Servicio"
        /// </summary>
        protected void lnkHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                int idAlumno = Convert.ToInt32(lnk.CommandArgument);

                // Guardar el ID del alumno seleccionado en sesión
                Session["IdAlumnoSeleccionado"] = idAlumno;

                // Redirigir a la página de historial de servicios
                Response.Redirect($"frm_HistorialServicio_PorHijo.aspx?idAlumno={idAlumno}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al abrir historial: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorHistorial",
                    $"alert('Error al abrir historial: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Obtiene la clase CSS para la fila según el estado de pago
        /// </summary>
        protected string GetClaseFilaEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
                return "fila-pendiente";

            switch (estado.ToUpper())
            {
                case "C": // Cancelado (Pagado)
                    return "fila-pagada";
                case "X": // Exonerado
                    return "fila-exonerada";
                case "P": // Pendiente
                default:
                    return "fila-pendiente";
            }
        }

        /// <summary>
        /// Obtiene la clase CSS para el badge según el estado de pago
        /// </summary>
        protected string GetClaseBadgeEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
                return "badge-pendiente";

            switch (estado.ToUpper())
            {
                case "C": // Cancelado (Pagado)
                    return "badge-pagado";
                case "X": // Exonerado
                    return "badge-exonerado";
                case "P": // Pendiente
                default:
                    return "badge-pendiente";
            }
        }

        /// <summary>
        /// Obtiene el texto del estado según el estado de pago
        /// </summary>
        protected string GetTextoEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
                return "Pendiente";

            switch (estado.ToUpper())
            {
                case "C": // Cancelado (Pagado)
                    return "Pagado";
                case "X": // Exonerado
                    return "Exonerado";
                case "P": // Pendiente
                default:
                    return "Pendiente";
            }
        }
    }
}