using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_GrupoAnual : System.Web.UI.Page
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
                RefrescarTextosSeleccionados();
            }
        }
        // ========================= CRUD =========================

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                clsGrupoAnual obj = new clsGrupoAnual
                {
                    Id_Salon = int.Parse(hdnIdSalon.Value),
                    Id_Profesor = int.Parse(hdnIdProfesor.Value),
                    Id_Nivel = int.Parse(hdnIdNivel.Value),
                    Periodo = short.Parse(txt_Periodo.Text.Trim())
                };

                Service1Client xdb = new Service1Client();
                string msg = xdb.InsertarGrupoAnual(obj);

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
            if (string.IsNullOrWhiteSpace(txt_IdGrupoAnual.Text))
            {
                Alert("Debe consultar un registro primero.");
                return;
            }

            if (!Validar()) return;

            try
            {
                clsGrupoAnual obj = new clsGrupoAnual
                {
                    Id_GrupoAnual = int.Parse(txt_IdGrupoAnual.Text),
                    Id_Salon = int.Parse(hdnIdSalon.Value),
                    Id_Profesor = int.Parse(hdnIdProfesor.Value),
                    Id_Nivel = int.Parse(hdnIdNivel.Value),
                    Periodo = short.Parse(txt_Periodo.Text.Trim())
                };

                Service1Client xdb = new Service1Client();
                string msg = xdb.ModificarGrupoAnual(obj);

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
            if (string.IsNullOrWhiteSpace(txt_IdGrupoAnual.Text))
            {
                Alert("Debe consultar un registro primero.");
                return;
            }

            try
            {
                int id = int.Parse(txt_IdGrupoAnual.Text);

                Service1Client xdb = new Service1Client();
                string msg = xdb.EliminarGrupoAnual(id);

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

        // ========================= GRID =========================

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvGrupoAnual_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGrupoAnual.PageIndex = e.NewPageIndex;
            CargarGrid(txtBuscar.Text.Trim());
        }

        protected void gvGrupoAnual_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Service1Client xdb = new Service1Client();

                if (e.CommandName == "Consultar")
                {
                    var obj = xdb.GetGrupoAnual_V().FirstOrDefault(x => x.Id_GrupoAnual == id);
                    if (obj == null)
                    {
                        Alert("Registro no encontrado.");
                        return;
                    }

                    txt_IdGrupoAnual.Text = obj.Id_GrupoAnual.ToString();

                    hdnIdSalon.Value = obj.Id_Salon.ToString();
                    txt_SalonSeleccionado.Text = Server.HtmlDecode(obj.NombreSalon ?? "");

                    hdnIdProfesor.Value = obj.Id_Profesor.ToString();
                    txt_ProfesorSeleccionado.Text = Server.HtmlDecode(obj.NombreProfesor ?? "");

                    hdnIdNivel.Value = obj.Id_Nivel.ToString();
                    txt_NivelSeleccionado.Text = Server.HtmlDecode(obj.NombreNivel ?? "");

                    txt_Periodo.Text = obj.Periodo.ToString();

                    ModoEdicion();
                }

                if (e.CommandName == "Eliminar")
                {
                    string msg = xdb.EliminarGrupoAnual(id);
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

        private void CargarGrid(string filtro = "")
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.GetGrupoAnual_V().ToList();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    // si el filtro es año numérico, filtra por periodo
                    if (short.TryParse(filtro, out short anio))
                        lista = lista.Where(x => x.Periodo == anio).ToList();
                    else
                        lista = lista.Where(x =>
                            (x.NombreSalon ?? "").ToLower().Contains(filtro.ToLower()) ||
                            (x.NombreProfesor ?? "").ToLower().Contains(filtro.ToLower()) ||
                            (x.NombreNivel ?? "").ToLower().Contains(filtro.ToLower())
                        ).ToList();
                }

                lblMensaje.Text = (lista.Count == 0) ? "No se encontraron resultados." : "";

                gvGrupoAnual.DataSource = lista;
                gvGrupoAnual.DataBind();
            }
            catch (Exception ex)
            {
                Alert("Error al cargar Grupo Anual: " + ex.Message);
            }
        }

        // ========================= MODALES =========================

        // ---- SALON ----
        protected void btnBuscarSalonModal_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.BuscarSalon(txtBuscarSalon.Text.Trim()).ToList();

                gvSalon.DataSource = lista;
                gvSalon.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalSalon",
                    "var m = new bootstrap.Modal(document.getElementById('modalSalon')); m.show();", true);
            }
            catch (Exception ex)
            {
                Alert("Error al buscar salón: " + ex.Message);
            }
        }

        protected void gvSalon_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "seleccionar") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvSalon.DataKeys[index].Value);
            string nombre = Server.HtmlDecode(gvSalon.Rows[index].Cells[0].Text);

            hdnIdSalon.Value = id.ToString();
            txt_SalonSeleccionado.Text = nombre;

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "hideModalSalon", "bootstrap.Modal.getInstance(document.getElementById('modalSalon')).hide();", true);
        }

        // ---- PROFESOR ----
        protected void btnBuscarProfesorModal_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.buscarProfesor(txtBuscarProfesor.Text.Trim()).ToList();

                gvProfesor.DataSource = lista;
                gvProfesor.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalProfesor",
                    "var m = new bootstrap.Modal(document.getElementById('modalProfesor')); m.show();", true);
            }
            catch (Exception ex)
            {
                Alert("Error al buscar profesor: " + ex.Message);
            }
        }

        protected void gvProfesor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "seleccionar") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvProfesor.DataKeys[index].Value);
            string nombre = Server.HtmlDecode(gvProfesor.Rows[index].Cells[0].Text);

            hdnIdProfesor.Value = id.ToString();
            txt_ProfesorSeleccionado.Text = nombre;

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "hideModalProfesor", "bootstrap.Modal.getInstance(document.getElementById('modalProfesor')).hide();", true);
        }

        // ---- NIVEL ----
        protected void btnBuscarNivelModal_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.buscarNivel(txtBuscarNivel.Text.Trim()).ToList();

                gvNivel.DataSource = lista;
                gvNivel.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalNivel",
                    "var m = new bootstrap.Modal(document.getElementById('modalNivel')); m.show();", true);
            }
            catch (Exception ex)
            {
                Alert("Error al buscar nivel: " + ex.Message);
            }
        }

        protected void gvNivel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "seleccionar") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvNivel.DataKeys[index].Value);
            string nombre = Server.HtmlDecode(gvNivel.Rows[index].Cells[0].Text);

            hdnIdNivel.Value = id.ToString();
            txt_NivelSeleccionado.Text = nombre;

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "hideModalNivel", "bootstrap.Modal.getInstance(document.getElementById('modalNivel')).hide();", true);
        }

        // ========================= HELPERS =========================

        private void Limpiar()
        {
            txt_IdGrupoAnual.Text = "";
            txt_Periodo.Text = "";

            hdnIdSalon.Value = "";
            hdnIdProfesor.Value = "";
            hdnIdNivel.Value = "";

            txt_SalonSeleccionado.Text = "";
            txt_ProfesorSeleccionado.Text = "";
            txt_NivelSeleccionado.Text = "";

            ModoNuevo();
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

        private void Alert(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert", $"alert('{mensaje.Replace("'", "\\'")}');", true);
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(hdnIdSalon.Value))
            {
                Alert("Seleccione un Salón.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(hdnIdProfesor.Value))
            {
                Alert("Seleccione un Profesor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(hdnIdNivel.Value))
            {
                Alert("Seleccione un Nivel.");
                return false;
            }

            if (!short.TryParse(txt_Periodo.Text.Trim(), out short periodo))
            {
                Alert("Ingrese un año válido (Ej: 2024, 2025).");
                return false;
            }
            if (periodo < 2000 || periodo > 2100)
            {
                Alert("El año debe estar entre 2000 y 2100.");
                return false;
            }

            return true;
        }

        private void RefrescarTextosSeleccionados()
        {
            try
            {
                Service1Client xdb = new Service1Client();

                // SALON
                if (!string.IsNullOrWhiteSpace(hdnIdSalon.Value))
                {
                    int idSalon = int.Parse(hdnIdSalon.Value);
                    var salon = xdb.BuscarSalon("").FirstOrDefault(s => s.Id_Salon == idSalon);
                    if (salon != null) txt_SalonSeleccionado.Text = salon.Nombre;
                }

                // PROFESOR
                if (!string.IsNullOrWhiteSpace(hdnIdProfesor.Value))
                {
                    int idProfesor = int.Parse(hdnIdProfesor.Value);
                    var prof = xdb.buscarProfesor("").FirstOrDefault(p => p.Id_Profesor == idProfesor);
                    if (prof != null) txt_ProfesorSeleccionado.Text = prof.NombreCompleto;
                }

                // NIVEL
                if (!string.IsNullOrWhiteSpace(hdnIdNivel.Value))
                {
                    int idNivel = int.Parse(hdnIdNivel.Value);
                    var niv = xdb.buscarNivel("").FirstOrDefault(n => n.Id == idNivel);
                    if (niv != null) txt_NivelSeleccionado.Text = niv.Nombre;
                }
            }
            catch
            {
                // No alert aquí para que no moleste en cada postback
            }
        }
    }
}