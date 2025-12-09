using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_ServicioAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarGrid();
                this.btn_Agregar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
            }
        }

        private void CargarCombos()
        {
            try
            {
                Service1Client xdb = new Service1Client();

                // ==== Grupo Servicio ====
                var listaGrupo = xdb.GetGrupoServicio().ToList();
                this.ddl_GrupoServicio.DataSource = listaGrupo;
                // Texto que muestra el combo (ajusta si tu entidad tiene otra propiedad descriptiva)
                this.ddl_GrupoServicio.DataTextField = "Periodo";           // o "NombreGrupo" si lo tuvieras
                this.ddl_GrupoServicio.DataValueField = "Id_GrupoServicio";
                this.ddl_GrupoServicio.DataBind();
                this.ddl_GrupoServicio.Items.Insert(0, new ListItem("--Seleccione--", "0"));

                // ==== Alumno ====
                var listaAlumno = xdb.GetAlumno().ToList();
                this.ddl_Alumno.DataSource = listaAlumno;
                this.ddl_Alumno.DataTextField = "NombreCompleto";           // propiedad típica para mostrar nombre
                this.ddl_Alumno.DataValueField = "Id_Alumno";
                this.ddl_Alumno.DataBind();
                this.ddl_Alumno.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error al cargar combos: {ex.Message}');", true);
            }
        }

        private void CargarGrid()
        {
            try
            {
                Service1Client xdb = new Service1Client();
                List<clsServicioAlumno> lista = xdb.GetServicioAlumno().ToList();

                gvServicioAlumno.DataSource = lista;
                gvServicioAlumno.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error al cargar Servicio Alumno: {ex.Message}');", true);
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_GrupoServicio.SelectedValue == "0" ||
                    ddl_Alumno.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe seleccionar Grupo de Servicio y Alumno.');", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_FechaInicio.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe ingresar la Fecha de Inicio.');", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Monto.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe ingresar el Monto.');", true);
                    return;
                }

                // ==== Parseo de datos ====
                int idGrupo = int.Parse(ddl_GrupoServicio.SelectedValue);
                int idAlumno = int.Parse(ddl_Alumno.SelectedValue);

                DateTime fechaInicio;
                if (!DateTime.TryParse(txt_FechaInicio.Text.Trim(), out fechaInicio))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Fecha Inicio no válida (use formato dd/mm/yyyy).');", true);
                    return;
                }

                DateTime? fechaFinal = null;
                if (!string.IsNullOrWhiteSpace(txt_FechaFinal.Text))
                {
                    DateTime temp;
                    if (!DateTime.TryParse(txt_FechaFinal.Text.Trim(), out temp))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Fecha Final no válida.');", true);
                        return;
                    }
                    fechaFinal = temp;
                }

                DateTime? fechaPago = null;
                if (!string.IsNullOrWhiteSpace(txt_FechaPago.Text))
                {
                    DateTime temp;
                    if (!DateTime.TryParse(txt_FechaPago.Text.Trim(), out temp))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Fecha de Pago no válida.');", true);
                        return;
                    }
                    fechaPago = temp;
                }

                TimeSpan? horaInicio = null;
                if (!string.IsNullOrWhiteSpace(txt_HoraInicio.Text))
                {
                    TimeSpan temp;
                    if (!TimeSpan.TryParse(txt_HoraInicio.Text.Trim(), out temp))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Hora Inicio no válida (use formato HH:mm).');", true);
                        return;
                    }
                    horaInicio = temp;
                }

                TimeSpan? horaFinal = null;
                if (!string.IsNullOrWhiteSpace(txt_HoraFinal.Text))
                {
                    TimeSpan temp;
                    if (!TimeSpan.TryParse(txt_HoraFinal.Text.Trim(), out temp))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Hora Final no válida (use formato HH:mm).');", true);
                        return;
                    }
                    horaFinal = temp;
                }

                decimal monto;
                if (!decimal.TryParse(txt_Monto.Text.Trim(), out monto))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Monto no válido.');", true);
                    return;
                }

                // ==== Llamar al servicio ====
                Service1Client xdb = new Service1Client();
                clsServicioAlumno ser = new clsServicioAlumno();

                ser.Id_GrupoServicio = idGrupo;
                ser.Id_Alumno = idAlumno;
                ser.FechaInicio = fechaInicio;
                ser.FechaFinal = fechaFinal;
                ser.FechaPago = fechaPago;
                ser.HoraInicio = horaInicio;
                ser.HoraFinal = horaFinal;
                ser.Monto = monto;

                xdb.InsServicioAlumno(ser);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Servicio Alumno agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_IdServicioAlumno.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe seleccionar un registro para modificar.');", true);
                    return;
                }

                if (ddl_GrupoServicio.SelectedValue == "0" ||
                    ddl_Alumno.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe seleccionar Grupo de Servicio y Alumno.');", true);
                    return;
                }

                Service1Client xdb = new Service1Client();
                clsServicioAlumno ser = new clsServicioAlumno();

                ser.Id_ServicioAlumno = int.Parse(txt_IdServicioAlumno.Text.Trim());
                ser.Id_GrupoServicio = int.Parse(ddl_GrupoServicio.SelectedValue);
                ser.Id_Alumno = int.Parse(ddl_Alumno.SelectedValue);

                DateTime fechaInicio;
                if (!DateTime.TryParse(txt_FechaInicio.Text.Trim(), out fechaInicio))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Fecha Inicio no válida.');", true);
                    return;
                }
                ser.FechaInicio = fechaInicio;

                DateTime tempDate;
                if (!string.IsNullOrWhiteSpace(txt_FechaFinal.Text) &&
                    DateTime.TryParse(txt_FechaFinal.Text.Trim(), out tempDate))
                    ser.FechaFinal = tempDate;
                else
                    ser.FechaFinal = null;

                if (!string.IsNullOrWhiteSpace(txt_FechaPago.Text) &&
                    DateTime.TryParse(txt_FechaPago.Text.Trim(), out tempDate))
                    ser.FechaPago = tempDate;
                else
                    ser.FechaPago = null;

                TimeSpan tempTime;
                if (!string.IsNullOrWhiteSpace(txt_HoraInicio.Text) &&
                    TimeSpan.TryParse(txt_HoraInicio.Text.Trim(), out tempTime))
                    ser.HoraInicio = tempTime;
                else
                    ser.HoraInicio = null;

                if (!string.IsNullOrWhiteSpace(txt_HoraFinal.Text) &&
                    TimeSpan.TryParse(txt_HoraFinal.Text.Trim(), out tempTime))
                    ser.HoraFinal = tempTime;
                else
                    ser.HoraFinal = null;

                decimal monto;
                if (!decimal.TryParse(txt_Monto.Text.Trim(), out monto))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Monto no válido.');", true);
                    return;
                }
                ser.Monto = monto;

                xdb.ModServicioAlumno(ser);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Servicio Alumno modificado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_IdServicioAlumno.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe seleccionar un registro para eliminar.');", true);
                    return;
                }

                int id = int.Parse(txt_IdServicioAlumno.Text.Trim());
                Service1Client xdb = new Service1Client();
                xdb.DelServicioAlumno(id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Servicio Alumno eliminado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            txt_IdServicioAlumno.Text = string.Empty;
            ddl_GrupoServicio.SelectedIndex = 0;
            ddl_Alumno.SelectedIndex = 0;

            txt_FechaInicio.Text = string.Empty;
            txt_FechaFinal.Text = string.Empty;
            txt_FechaPago.Text = string.Empty;
            txt_HoraInicio.Text = string.Empty;
            txt_HoraFinal.Text = string.Empty;
            txt_Monto.Text = string.Empty;
        }

        protected void gvServicioAlumno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Service1Client xdb = new Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    var lista = xdb.GetServicioAlumno().ToList();
                    var ser = lista.FirstOrDefault(s => s.Id_ServicioAlumno == id);

                    if (ser != null)
                    {
                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdServicioAlumno.Text = ser.Id_ServicioAlumno.ToString();
                        ddl_GrupoServicio.SelectedValue = ser.Id_GrupoServicio.ToString();
                        ddl_Alumno.SelectedValue = ser.Id_Alumno.ToString();

                        // Formatear fechas solo si tienen valor (evita llamar ToString(string) en Nullable<T>)
                        txt_FechaInicio.Text = (ser.FechaInicio != null && ser.FechaInicio.HasValue)
                            ? ser.FechaInicio.Value.ToString("dd/MM/yyyy")
                            : "";
                        txt_FechaFinal.Text = ser.FechaFinal.HasValue ? ser.FechaFinal.Value.ToString("dd/MM/yyyy") : "";
                        txt_FechaPago.Text = ser.FechaPago.HasValue ? ser.FechaPago.Value.ToString("dd/MM/yyyy") : "";

                        // Horas (TimeSpan?) también formateadas si tienen valor
                        txt_HoraInicio.Text = ser.HoraInicio.HasValue ? ser.HoraInicio.Value.ToString(@"hh\:mm") : "";
                        txt_HoraFinal.Text = ser.HoraFinal.HasValue ? ser.HoraFinal.Value.ToString(@"hh\:mm") : "";

                        // Monto puede ser nullable; formatear solo si tiene valor
                        txt_Monto.Text = ser.Monto != null && ser.Monto.HasValue
                            ? ser.Monto.Value.ToString("0.00")
                            : "";
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Error al consultar: {ex.Message}');", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.DelServicioAlumno(id);
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Servicio Alumno eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvServicioAlumno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvServicioAlumno.PageIndex = e.NewPageIndex;
            CargarGrid();
        }
    }
}