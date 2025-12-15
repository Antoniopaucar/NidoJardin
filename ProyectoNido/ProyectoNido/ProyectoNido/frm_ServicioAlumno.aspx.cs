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
                CargarGrid();
                ModoNuevo();
            }
            else
            {
                RestaurarSeleccionVisual();
            }
        }
        // ========================= BOTONES CRUD =========================

        // ===================== CRUD =====================

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                clsServicioAlumno_v obj = ObtenerDatosFormulario(false);

                Service1Client xdb = new Service1Client();
                string msg = xdb.InsertarServicioAlumno(obj);

                Alert(msg);
                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                Alert("Error al agregar: " + ex.Message);
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_IdServicioAlumno.Text))
            {
                Alert("Debe consultar un registro primero.");
                return;
            }

            if (!Validar()) return;

            try
            {
                clsServicioAlumno_v obj = ObtenerDatosFormulario(true);

                Service1Client xdb = new Service1Client();
                string msg = xdb.ModificarServicioAlumno(obj);

                Alert(msg);
                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                Alert("Error al modificar: " + ex.Message);
            }
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_IdServicioAlumno.Text))
                {
                    Alert("Debe consultar un registro primero.");
                    return;
                }

                int id = int.Parse(txt_IdServicioAlumno.Text);

                Service1Client xdb = new Service1Client();
                string msg = xdb.EliminarServicioAlumno(id);

                Alert(msg);
                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                Alert("Error al eliminar: " + ex.Message);
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        // ===================== GRID =====================

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrid(txtBuscar.Text.Trim());
        }

        protected void gvServicioAlumno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvServicioAlumno.PageIndex = e.NewPageIndex;
            CargarGrid(txtBuscar.Text.Trim());
        }

        protected void gvServicioAlumno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Consultar")
                {
                    Service1Client xdb = new Service1Client();
                    var obj = xdb.GetServicioAlumno().FirstOrDefault(x => x.Id_ServicioAlumno == id);

                    if (obj == null)
                    {
                        Alert("Registro no encontrado.");
                        return;
                    }

                    // cargar a formulario
                    txt_IdServicioAlumno.Text = obj.Id_ServicioAlumno.ToString();

                    hdnIdGrupoServicio.Value = obj.Id_GrupoServicio.ToString();
                    // mostramos algo amigable (salón + servicio + periodo)
                    txt_GrupoServicioSeleccionado.Text = (obj.NombreSalon ?? "") + " | " + (obj.NombreServicio ?? "") + " | " + obj.Periodo.ToString();

                    hdnIdAlumno.Value = obj.Id_Alumno.ToString();
                    txt_AlumnoSeleccionado.Text = obj.NombreAlumno;

                    txt_FechaInicio.Text = obj.FechaInicio.ToString("yyyy-MM-dd");
                    txt_FechaFinal.Text = obj.FechaFinal.HasValue ? obj.FechaFinal.Value.ToString("yyyy-MM-dd") : "";
                    txt_FechaPago.Text = obj.FechaPago.HasValue ? obj.FechaPago.Value.ToString("yyyy-MM-dd") : "";

                    txt_HoraInicio.Text = obj.HoraInicio.HasValue ? obj.HoraInicio.Value.ToString(@"hh\:mm") : "";
                    txt_HoraFinal.Text = obj.HoraFinal.HasValue ? obj.HoraFinal.Value.ToString(@"hh\:mm") : "";

                    txt_Monto.Text = obj.Monto.ToString("0.00");

                    ModoEdicion();
                }
                else if (e.CommandName == "Eliminar")
                {
                    Service1Client xdb = new Service1Client();
                    string msg = xdb.EliminarServicioAlumno(id);

                    Alert(msg);
                    Limpiar();
                    CargarGrid(txtBuscar.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Alert("Error en la grilla: " + ex.Message);
            }
        }

        // ===================== MODALES =====================

        // ---- Buscar Alumno (dentro modal) ----
        protected void btnBuscarAlumnoModal_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.buscarAlumno(txtBuscarAlumno.Text.Trim()).ToList();

                gvAlumno.DataSource = lista;
                gvAlumno.DataBind();

                // reabrir modal (postback)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalAlumno",
                    "var m = new bootstrap.Modal(document.getElementById('modalAlumno')); m.show();", true);
            }
            catch (Exception ex)
            {
                Alert("Error al buscar alumno: " + ex.Message);
            }
        }

        protected void gvAlumno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "seleccionar") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvAlumno.DataKeys[index].Value);

            string nombre = gvAlumno.Rows[index].Cells[0].Text;

            hdnIdAlumno.Value = id.ToString();
            txt_AlumnoSeleccionado.Text = nombre;

            // cerrar modal
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "hideModalAlumno", "bootstrap.Modal.getInstance(document.getElementById('modalAlumno')).hide();", true);
        }

        // ---- Buscar Grupo Servicio (dentro modal) ----
        protected void btnBuscarGrupoServicioModal_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.buscarGrupoServicio(txtBuscarGrupoServicio.Text.Trim()).ToList();

                gvGrupoServicioModal.DataSource = lista;
                gvGrupoServicioModal.DataBind();

                // reabrir modal (postback)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalGrupoServicio",
                    "var m = new bootstrap.Modal(document.getElementById('modalGrupoServicio')); m.show();", true);
            }
            catch (Exception ex)
            {
                Alert("Error al buscar Grupo Servicio: " + ex.Message);
            }
        }

        protected void gvGrupoServicioModal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "seleccionar") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvGrupoServicioModal.DataKeys[index].Value);

            // ojo: el orden de celdas depende de tus BoundField
            string salon = gvGrupoServicioModal.Rows[index].Cells[0].Text;
            string profesor = gvGrupoServicioModal.Rows[index].Cells[1].Text;
            string servicio = gvGrupoServicioModal.Rows[index].Cells[2].Text;
            string periodo = gvGrupoServicioModal.Rows[index].Cells[3].Text;

            hdnIdGrupoServicio.Value = id.ToString();
            txt_GrupoServicioSeleccionado.Text = salon + " | " + servicio + " | " + periodo;

            // cerrar modal
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "hideModalGrupoServicio", "bootstrap.Modal.getInstance(document.getElementById('modalGrupoServicio')).hide();", true);
        }

        // ===================== UTILITARIOS =====================

        private void Alert(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert", "alert('" + mensaje.Replace("'", "\\'") + "');", true);
        }

        private void ModoNuevo()
        {
            btn_Agregar.Enabled = true;
            btn_Modificar.Enabled = false;
            btn_Eliminar.Enabled = false;
        }

        private void ModoEdicion()
        {
            btn_Agregar.Enabled = false;
            btn_Modificar.Enabled = true;
            btn_Eliminar.Enabled = true;
        }

        private void Limpiar()
        {
            txt_IdServicioAlumno.Text = "";

            hdnIdGrupoServicio.Value = "";
            txt_GrupoServicioSeleccionado.Text = "";

            hdnIdAlumno.Value = "";
            txt_AlumnoSeleccionado.Text = "";

            txt_FechaInicio.Text = "";
            txt_FechaFinal.Text = "";
            txt_FechaPago.Text = "";

            txt_HoraInicio.Text = "";
            txt_HoraFinal.Text = "";

            txt_Monto.Text = "";

            ModoNuevo();
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.GetServicioAlumno().ToList();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    string f = filtro.Trim().ToLower();

                    lista = lista.Where(x =>
                        ((x.NombreAlumno ?? "").ToLower().Contains(f)) ||
                        ((x.NombreSalon ?? "").ToLower().Contains(f)) ||
                        ((x.NombreServicio ?? "").ToLower().Contains(f)) ||
                        (x.Periodo.ToString().Contains(f))
                    ).ToList();
                }

                lblMensaje.Text = (lista.Count == 0) ? "No se encontraron resultados." : "";

                gvServicioAlumno.DataSource = lista;
                gvServicioAlumno.DataBind();
            }
            catch (Exception ex)
            {
                Alert("Error al cargar Servicio Alumno: " + ex.Message);
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(hdnIdGrupoServicio.Value))
            {
                Alert("Seleccione un Grupo Servicio.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(hdnIdAlumno.Value))
            {
                Alert("Seleccione un Alumno.");
                return false;
            }

            DateTime fi;
            if (!DateTime.TryParse(txt_FechaInicio.Text.Trim(), out fi))
            {
                Alert("Ingrese una Fecha Inicio válida.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txt_FechaFinal.Text))
            {
                DateTime ff;
                if (!DateTime.TryParse(txt_FechaFinal.Text.Trim(), out ff))
                {
                    Alert("Ingrese una Fecha Final válida o deje vacío.");
                    return false;
                }
                if (ff.Date < fi.Date)
                {
                    Alert("La Fecha Final no puede ser menor que la Fecha Inicio.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txt_FechaPago.Text))
            {
                DateTime fp;
                if (!DateTime.TryParse(txt_FechaPago.Text.Trim(), out fp))
                {
                    Alert("Ingrese una Fecha Pago válida o deje vacío.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txt_HoraInicio.Text))
            {
                TimeSpan hi;
                if (!TimeSpan.TryParse(txt_HoraInicio.Text.Trim(), out hi))
                {
                    Alert("Hora Inicio inválida. Use HH:mm o deje vacío.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txt_HoraFinal.Text))
            {
                TimeSpan hf;
                if (!TimeSpan.TryParse(txt_HoraFinal.Text.Trim(), out hf))
                {
                    Alert("Hora Final inválida. Use HH:mm o deje vacío.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txt_HoraInicio.Text) && !string.IsNullOrWhiteSpace(txt_HoraFinal.Text))
            {
                TimeSpan hi = TimeSpan.Parse(txt_HoraInicio.Text.Trim());
                TimeSpan hf = TimeSpan.Parse(txt_HoraFinal.Text.Trim());
                if (hf <= hi)
                {
                    Alert("Hora Final debe ser mayor que Hora Inicio.");
                    return false;
                }
            }

            decimal monto;
            if (!decimal.TryParse(txt_Monto.Text.Trim(), out monto))
            {
                Alert("Ingrese un monto válido.");
                return false;
            }
            if (monto < 0)
            {
                Alert("El monto no puede ser negativo.");
                return false;
            }

            return true;
        }

        private clsServicioAlumno_v ObtenerDatosFormulario(bool incluirId)
        {
            clsServicioAlumno_v obj = new clsServicioAlumno_v();

            if (incluirId)
                obj.Id_ServicioAlumno = int.Parse(txt_IdServicioAlumno.Text);

            obj.Id_GrupoServicio = int.Parse(hdnIdGrupoServicio.Value);
            obj.Id_Alumno = int.Parse(hdnIdAlumno.Value);

            obj.FechaInicio = DateTime.Parse(txt_FechaInicio.Text.Trim()).Date;

            if (!string.IsNullOrWhiteSpace(txt_FechaFinal.Text))
                obj.FechaFinal = DateTime.Parse(txt_FechaFinal.Text.Trim()).Date;

            if (!string.IsNullOrWhiteSpace(txt_FechaPago.Text))
                obj.FechaPago = DateTime.Parse(txt_FechaPago.Text.Trim()).Date;

            if (!string.IsNullOrWhiteSpace(txt_HoraInicio.Text))
                obj.HoraInicio = TimeSpan.Parse(txt_HoraInicio.Text.Trim());

            if (!string.IsNullOrWhiteSpace(txt_HoraFinal.Text))
                obj.HoraFinal = TimeSpan.Parse(txt_HoraFinal.Text.Trim());

            obj.Monto = decimal.Parse(txt_Monto.Text.Trim());

            return obj;
        }

        private void RestaurarSeleccionVisual()
        {
            try
            {
                Service1Client xdb = new Service1Client();

                // Alumno
                if (!string.IsNullOrWhiteSpace(hdnIdAlumno.Value) &&
                    string.IsNullOrWhiteSpace(txt_AlumnoSeleccionado.Text))
                {
                    int idAlumno = int.Parse(hdnIdAlumno.Value);
                    var al = xdb.buscarAlumno("").FirstOrDefault(a => a.Id_Alumno == idAlumno);
                    if (al != null)
                        txt_AlumnoSeleccionado.Text = al.NombreCompleto;
                }

                // GrupoServicio (texto armado salón|servicio|periodo)
                if (!string.IsNullOrWhiteSpace(hdnIdGrupoServicio.Value) &&
                    string.IsNullOrWhiteSpace(txt_GrupoServicioSeleccionado.Text))
                {
                    int idGs = int.Parse(hdnIdGrupoServicio.Value);
                    var gs = xdb.buscarGrupoServicio("").FirstOrDefault(g => g.Id_GrupoServicio == idGs);
                    if (gs != null)
                        txt_GrupoServicioSeleccionado.Text = gs.NombreSalon + " | " + gs.NombreServicio + " | " + gs.Periodo;
                }
            }
            catch
            {
                // No hacemos alert aquí para no molestar; solo intentamos restaurar.
            }
        }
    }
}