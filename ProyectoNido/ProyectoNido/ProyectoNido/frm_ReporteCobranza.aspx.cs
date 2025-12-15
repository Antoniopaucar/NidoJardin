using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_ReporteCobranza : System.Web.UI.Page
    {
        Service1Client xdb = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("frm_Login.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                CargarNombreDocente();
                CargarCombos();
            }
        }

        /// <summary>
        /// Carga el nombre del docente en el panel lateral.
        /// </summary>
        private void CargarNombreDocente()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                // Usamos el cliente ya existente o instanciamos uno nuevo si preferimos aislamiento, 
                // pero para seguir el ejemplo exacto de ReporteIngresos:
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

        private void CargarCombos()
        {
            try
            {
                // Cargar Distritos
                var distritos = xdb.GetDistrito().ToList();
                ddlDistrito.DataSource = distritos;
                ddlDistrito.DataTextField = "Nombre";
                ddlDistrito.DataValueField = "Id";
                ddlDistrito.DataBind();
                ddlDistrito.Items.Insert(0, new ListItem("-- Todos los Distritos --", "0"));

                // Cargar Salones
                var salones = xdb.GetSalon().ToList();
                ddlSalon.DataSource = salones;
                ddlSalon.DataTextField = "Nombre";
                ddlSalon.DataValueField = "Id";
                ddlSalon.DataBind();
                ddlSalon.Items.Insert(0, new ListItem("-- Todos los Salones --", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCombos", 
                    $"alert('Error al cargar filtros: {ex.Message}');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idSalon = ddlSalon.SelectedValue == "0" ? (int?)null : int.Parse(ddlSalon.SelectedValue);
                int? idDistrito = ddlDistrito.SelectedValue == "0" ? (int?)null : int.Parse(ddlDistrito.SelectedValue);
                
                DateTime? fechaInicio = null;
                DateTime? fechaFin = null;

                if (!string.IsNullOrEmpty(txtFechaInicio.Text))
                    fechaInicio = DateTime.Parse(txtFechaInicio.Text);

                if (!string.IsNullOrEmpty(txtFechaFin.Text))
                    fechaFin = DateTime.Parse(txtFechaFin.Text);

                var cobranzas = ObtenerCobranzas(idSalon, idDistrito, fechaInicio, fechaFin);

                // Filtrar solo pendientes (aunque el SP ya lo hace, doble seguridad)
                if (cobranzas != null && cobranzas.Any())
                {
                    cobranzas = cobranzas
                        .Where(x => x.EstadoPago == "P" || x.EstadoDescripcion == "Pendiente" || x.EstadoPago == null && x.FechaPago == null)
                        .ToList();
                }

                if (cobranzas != null && cobranzas.Any())
                {
                    decimal total = cobranzas.Sum(x => x.Monto);
                    lblTotal.Text = "S/ " + total.ToString("N2");

                    gvIngresos.DataSource = cobranzas;
                    gvIngresos.DataBind();
                    pnlResultados.Visible = true;
                    pnlSinIngresos.Visible = false;
                }
                else
                {
                    lblTotal.Text = "S/ 0.00";
                    gvIngresos.DataSource = null;
                    gvIngresos.DataBind();
                    pnlResultados.Visible = false;
                    pnlSinIngresos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al buscar cobranzas: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCobranzas", 
                    $"alert('Error al obtener el reporte de cobranzas: {ex.Message.Replace("'", "\'")}');", true);
            }
        }

        private List<clsReporteIngreso> ObtenerCobranzas(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
             // Llamar al nuevo método ListarReporteCobranzas
             // Nota: Si el SP usa fechaInicio/Fin para filtrar FechaVencimiento, está bien pasarlas aquí.
             return xdb.ListarReporteCobranzas(idSalon, idDistrito, fechaInicio, fechaFin).ToList();
        }
    }
}