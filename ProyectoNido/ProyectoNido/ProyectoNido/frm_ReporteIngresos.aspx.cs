using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_ReporteIngresos : System.Web.UI.Page
    {
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
                CargarFiltros();
                pnlResultados.Visible = false;
                pnlSinIngresos.Visible = false;
            }
        }

        /// <summary>
        /// Carga el nombre del docente en el panel lateral.
        /// No modifica ninguna clase ni método existente: reutiliza el servicio actual.
        /// </summary>
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

        /// <summary>
        /// Carga los combos de salón y distrito reutilizando los servicios existentes.
        /// </summary>
        private void CargarFiltros()
        {
            try
            {
                Service1Client servicio = new Service1Client();

                // Salones
                var salones = servicio.GetSalon();
                ddlSalon.DataSource = salones ?? new clsSalon[0];
                ddlSalon.DataTextField = "Nombre";
                ddlSalon.DataValueField = "Id";
                ddlSalon.DataBind();
                ddlSalon.Items.Insert(0, new ListItem("Todos los salones", ""));

                // Distritos
                var distritos = servicio.GetDistrito();
                ddlDistrito.DataSource = distritos ?? new clsDistrito[0];
                ddlDistrito.DataTextField = "Nombre";
                ddlDistrito.DataValueField = "Id";
                ddlDistrito.DataBind();
                ddlDistrito.Items.Insert(0, new ListItem("Todos los distritos", ""));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar filtros de reporte de ingresos: {ex.Message}");
            }
        }

        /// <summary>
        /// Maneja el click del botón de búsqueda.
        /// Aplica validaciones básicas y muestra mensaje cuando no hay resultados.
        /// </summary>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idSalon = string.IsNullOrWhiteSpace(ddlSalon.SelectedValue)
                    ? (int?)null
                    : Convert.ToInt32(ddlSalon.SelectedValue);

                int? idDistrito = string.IsNullOrWhiteSpace(ddlDistrito.SelectedValue)
                    ? (int?)null
                    : Convert.ToInt32(ddlDistrito.SelectedValue);

                DateTime? fechaInicio = string.IsNullOrWhiteSpace(txtFechaInicio.Text)
                    ? (DateTime?)null
                    : DateTime.Parse(txtFechaInicio.Text);

                DateTime? fechaFin = string.IsNullOrWhiteSpace(txtFechaFin.Text)
                    ? (DateTime?)null
                    : DateTime.Parse(txtFechaFin.Text);

                if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "rangoFechasInvalido",
                        "alert('La fecha de inicio no puede ser mayor que la fecha fin.');", true);
                    pnlResultados.Visible = false;
                    pnlSinIngresos.Visible = false;
                    return;
                }

                var ingresos = ObtenerIngresos(idSalon, idDistrito, fechaInicio, fechaFin);

                if (ingresos != null && ingresos.Any())
                {
                    // Filter just in case the SP or service didn't filter
                    ingresos = ingresos.Where(x => x.EstadoPago == "C" || x.EstadoDescripcion == "Cancelado").ToList();
                }

                if (ingresos != null && ingresos.Any())
                {
                    decimal total = ingresos.Sum(x => x.Monto);
                    lblTotal.Text = "S/ " + total.ToString("N2");

                    gvIngresos.DataSource = ingresos;
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
                System.Diagnostics.Debug.WriteLine($"Error al buscar ingresos: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorIngresos",
                    $"alert('Error al obtener el reporte de ingresos: {ex.Message.Replace("'", "\\'")}');", true);
                pnlResultados.Visible = false;
                pnlSinIngresos.Visible = false;
                lblTotal.Text = "S/ 0.00";
            }
        }

        /// <summary>
        /// Obtiene la lista de ingresos para el reporte llamando al servicio WCF.
        /// </summary>
        private List<IngresoReporteDto> ObtenerIngresos(int? idSalon, int? idDistrito, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<IngresoReporteDto> resultado = new List<IngresoReporteDto>();

            try
            {
                Service1Client servicio = new Service1Client();
                // Llamamos al nuevo método agregado al servicio.
                var lista = servicio.ListarReporteIngresos(idSalon, idDistrito, fechaInicio, fechaFin);

                if (lista != null)
                {
                    foreach (var item in lista)
                    {
                        resultado.Add(new IngresoReporteDto
                        {
                            FechaPago = item.FechaPago,
                            Salon = item.Salon,
                            Distrito = item.Distrito,
                            Alumno = item.Alumno,
                            Concepto = item.Concepto,
                            Monto = item.Monto,
                            EstadoPago = item.EstadoPago,
                            EstadoDescripcion = item.EstadoDescripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al llamar al servicio de reporte: {ex.Message}");
                throw; // Re-lanzamos para que lo capture el btnBuscar_Click
            }

            return resultado;
        }

        /// <summary>
        /// DTO interno para mostrar el resultado del reporte de ingresos.
        /// </summary>
        private class IngresoReporteDto
        {
            public DateTime? FechaPago { get; set; }
            public string Salon { get; set; }
            public string Distrito { get; set; }
            public string Alumno { get; set; }
            public string Concepto { get; set; }
            public decimal Monto { get; set; }
            public string EstadoPago { get; set; }
            public string EstadoDescripcion { get; set; }
        }
    }
}