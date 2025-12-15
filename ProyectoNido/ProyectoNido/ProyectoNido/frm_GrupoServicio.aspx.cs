using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_GrupoServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
            }

        }

        protected void btn_BuscarProfesor_Click(object sender, EventArgs e)
        {

        }

        protected void gvProfesores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (!short.TryParse(txt_Periodo.Text.Trim(), out short periodo))
            {
                Alert("Ingrese un año válido (Ej: 2025).");
                return;
            }
            if (!Validar()) return;

            try
            {
                clsGrupoServicio obj = new clsGrupoServicio
                {
                    Id_Salon = int.Parse(hdnIdSalon.Value),
                    Id_Profesor = int.Parse(hdnIdProfesor.Value),
                    Id_ServicioAdicional = int.Parse(hdnIdServicio.Value),
                    Periodo = short.Parse(txt_Periodo.Text.Trim())
                };

                Service1Client xdb = new Service1Client();
                string msg = xdb.InsertarGrupoServicio(obj);

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
            if (string.IsNullOrWhiteSpace(txt_IdGrupoServicio.Text))
            {
                Alert("Debe consultar un registro primero.");
                return;
            }
            if (!short.TryParse(txt_Periodo.Text.Trim(), out short periodo))
            {
                Alert("Ingrese un año válido (Ej: 2025).");
                return;
            }

            if (!Validar()) return;

            try
            {
                clsGrupoServicio obj = new clsGrupoServicio
                {
                    Id_GrupoServicio = int.Parse(txt_IdGrupoServicio.Text),
                    Id_Salon = int.Parse(hdnIdSalon.Value),
                    Id_Profesor = int.Parse(hdnIdProfesor.Value),
                    Id_ServicioAdicional = int.Parse(hdnIdServicio.Value),
                    Periodo = short.Parse(txt_Periodo.Text.Trim())
                };

                Service1Client xdb = new Service1Client();
                string msg = xdb.ModificarGrupoServicio(obj);

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
                if (string.IsNullOrEmpty(txt_IdGrupoServicio.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Debe seleccionar un registro.');", true);
                    return;
                }

                int id = int.Parse(txt_IdGrupoServicio.Text);

                Service1Client xdb = new Service1Client();
                string mensaje = xdb.EliminarGrupoServicio(id);

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "ok", $"alert('{mensaje}');", true);

                Limpiar();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "err", $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txt_IdGrupoServicio.Text = "";
            txt_Periodo.Text = "";

            hdnIdSalon.Value = "";
            hdnIdProfesor.Value = "";
            hdnIdServicio.Value = "";

            txt_SalonSeleccionado.Text = "";
            txt_ProfesorSeleccionado.Text = "";
            txt_ServicioSeleccionado.Text = "";

            ModoNuevo();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvGrupoServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Consultar") return;

            int id = Convert.ToInt32(e.CommandArgument);

            Service1Client xdb = new Service1Client();
            var obj = xdb.GetGrupoServicio()
                         .FirstOrDefault(x => x.Id_GrupoServicio == id);

            if (obj == null)
            {
                Alert("Registro no encontrado.");
                return;
            }

            txt_IdGrupoServicio.Text = obj.Id_GrupoServicio.ToString();
            hdnIdSalon.Value = obj.Id_Salon.ToString();
            hdnIdProfesor.Value = obj.Id_Profesor.ToString();
            hdnIdServicio.Value = obj.Id_ServicioAdicional.ToString();
            txt_Periodo.Text = obj.Periodo.ToString();

            // Opcional: mostrar nombres (si tu SP los retorna)
            txt_SalonSeleccionado.Text = obj.NombreSalon;
            txt_ProfesorSeleccionado.Text = obj.NombreProfesor;
            txt_ServicioSeleccionado.Text = obj.NombreServicio;

            ModoEdicion();
        }

        protected void gvGrupoServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGrupoServicio.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        // PROFESOR
        protected void btnBuscarProfesorModal_Click(object sender, EventArgs e)
        {
            Service1Client xdb = new Service1Client();
            var lista = xdb.buscarProfesor(txtBuscarProfesor.Text.Trim()).ToList();

            gvProfesor.DataSource = lista;
            gvProfesor.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalProfesor",
                "var m = new bootstrap.Modal(document.getElementById('modalProfesor')); m.show();", true);
        }

        protected void gvProfesor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "seleccionar") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvProfesor.DataKeys[index].Value);
            string nombre = gvProfesor.Rows[index].Cells[0].Text;

            hdnIdProfesor.Value = id.ToString();
            txt_ProfesorSeleccionado.Text = nombre;

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "hideModal", "bootstrap.Modal.getInstance(document.getElementById('modalProfesor')).hide();", true);

        }

        // SALÓN
        protected void btnBuscarSalonModal_Click(object sender, EventArgs e)
        {
            Service1Client xdb = new Service1Client();
            var lista = xdb.BuscarSalon(txtBuscarSalon.Text.Trim()).ToList();

            gvSalon.DataSource = lista;
            gvSalon.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalSalon",
                "var m = new bootstrap.Modal(document.getElementById('modalSalon')); m.show();", true);
        }

        protected void gvSalon_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gvSalon.DataKeys[index].Value);
                string nombre = gvSalon.Rows[index].Cells[0].Text;

                hdnIdSalon.Value = id.ToString();
                txt_SalonSeleccionado.Text = nombre;
            }
        }

        // SERVICIO ADICIONAL
        protected void btnBuscarServicioModal_Click(object sender, EventArgs e)
        {
            Service1Client xdb = new Service1Client();
            var lista = xdb.BuscarServicioAdicional(txtBuscarServicio.Text.Trim()).ToList();

            gvServicio.DataSource = lista;
            gvServicio.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalServicio",
                "var m = new bootstrap.Modal(document.getElementById('modalServicio')); m.show();", true);
        }

        protected void gvServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gvServicio.DataKeys[index].Value);
                string nombre = gvServicio.Rows[index].Cells[0].Text;

                hdnIdServicio.Value = id.ToString();
                txt_ServicioSeleccionado.Text = nombre;
            }
        }

        //----------------------------------------------------------------------------------------------------------------
        private void Alert(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert", $"alert('{mensaje.Replace("'", "\\'")}');", true);
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

        private void CargarGrid(string filtro = "")
        {
            try
            {
                Service1Client xdb = new Service1Client();
                var lista = xdb.GetGrupoServicio().ToList();

                if (!string.IsNullOrWhiteSpace(filtro) &&
                    byte.TryParse(filtro, out byte periodo))
                {
                    lista = lista.Where(g => g.Periodo == periodo).ToList();
                }

                lblMensaje.Text = lista.Count == 0
                    ? "No se encontraron resultados."
                    : "";

                gvGrupoServicio.DataSource = lista;
                gvGrupoServicio.DataBind();
            }
            catch (Exception ex)
            {
                Alert("Error al cargar Grupo Servicio: " + ex.Message);
            }
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
            if (string.IsNullOrWhiteSpace(hdnIdServicio.Value))
            {
                Alert("Seleccione un Servicio Adicional.");
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

    }

}