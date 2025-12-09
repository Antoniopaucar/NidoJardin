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
                CargarCombos();
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
                //gvProfesores.Visible = false;
            }

        }

        protected void btn_BuscarProfesor_Click(object sender, EventArgs e)
        {
            //string texto = txt_BuscarProfesor.Text.Trim();
            //Service1Client xdb = new Service1Client();
            //var lista = xdb.buscarProfesor(texto).ToList();

            //gvProfesores.DataSource = lista;
            //gvProfesores.DataBind();
            //gvProfesores.Visible = lista.Count > 0;
        }

        protected void gvProfesores_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int idProfesor = Convert.ToInt32(gvProfesores.SelectedDataKey.Value);
            //string nombre = gvProfesores.SelectedRow.Cells[0].Text;

            //hdnIdProfesor.Value = idProfesor.ToString();
            //txt_ProfesorSeleccionado.Text = nombre;

            //// Si quieres ocultar la lista después de seleccionar:
            //// gvProfesores.Visible = false;
        }

        private void CargarCombos()
        {
            //try
            //{
            //}
            //catch (Exception ex)
            //{
                
            //}
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                Service1Client xdb = new Service1Client();
                List<clsGrupoServicio> lista = xdb.GetGrupoServicio().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    if (byte.TryParse(filtro, out byte periodo))
                    {
                        lista = lista.Where(g => g.Periodo == periodo).ToList();
                    }
                }

                lblMensaje.Text = lista.Count == 0
                    ? "No se encontraron resultados."
                    : "";

                gvGrupoServicio.DataSource = lista;
                gvGrupoServicio.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar Grupo Servicio: {ex.Message}');", true);
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(hdnIdSalon.Value) ||
                    string.IsNullOrWhiteSpace(hdnIdProfesor.Value) ||
                    string.IsNullOrWhiteSpace(hdnIdServicio.Value) ||
                    string.IsNullOrWhiteSpace(txt_Periodo.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Complete todos los campos.');", true);
                    return;
                }

                // Crear entidad GrupoServicio
                clsGrupoServicio obj = new clsGrupoServicio()
                {
                    Id_Salon = int.Parse(hdnIdSalon.Value),
                    Id_Profesor = int.Parse(hdnIdProfesor.Value),
                    Id_ServicioAdicional = int.Parse(hdnIdServicio.Value),
                    Periodo = (byte)int.Parse(txt_Periodo.Text)
                };

                // Llamar al método WCF
                string mensaje = xdb.InsertarGrupoServicio(obj);

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

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_IdGrupoServicio.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Debe seleccionar un Grupo Servicio primero.');", true);
                    return;
                }

                clsGrupoServicio obj = new clsGrupoServicio()
                {
                    Id_GrupoServicio = int.Parse(txt_IdGrupoServicio.Text),
                    Id_Salon = int.Parse(hdnIdSalon.Value),
                    Id_Profesor = int.Parse(hdnIdProfesor.Value),
                    Id_ServicioAdicional = int.Parse(hdnIdServicio.Value),
                    Periodo = (byte)int.Parse(txt_Periodo.Text)
                };

                Service1Client xdb = new Service1Client();
                string mensaje = xdb.ModificarGrupoServicio(obj);

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
            txt_SalonSeleccionado.Text = "";
            txt_ProfesorSeleccionado.Text = "";
            txt_ServicioSeleccionado.Text = "";
            txt_Periodo.Text = "";

            hdnIdSalon.Value = "";
            hdnIdProfesor.Value = "";
            hdnIdServicio.Value = "";
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvGrupoServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int id = Convert.ToInt32(e.CommandArgument);
            //Service1Client xdb = new Service1Client();

            //if (e.CommandName == "Consultar")
            //{
            //    try
            //    {
            //        var lista = xdb.GetGrupoServicio();
            //        var grupo = lista.FirstOrDefault(g => g.Id_GrupoServicio == id);

            //        if (grupo != null)
            //        {
            //            this.btn_Agregar.Enabled = false;
            //            this.btn_Modificar.Enabled = true;
            //            this.btn_Eliminar.Enabled = true;

            //            txt_IdGrupoServicio.Text = grupo.Id_GrupoServicio.ToString();

            //            ddl_Salon.SelectedValue = grupo.Id_Salon.ToString();
            //            ddl_Profesor.SelectedValue = grupo.Id_Profesor.ToString();
            //            ddl_ServicioAdicional.SelectedValue = grupo.Id_ServicioAdicional.ToString();

            //            txt_Periodo.Text = grupo.Periodo.ToString();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al consultar: {ex.Message}');", true);
            //    }
            //}
            //else if (e.CommandName == "Eliminar")
            //{
            //    try
            //    {
            //        xdb.DelGrupoServicio(id);
            //        CargarGrid();
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Grupo de Servicio eliminado correctamente.');", true);
            //    }
            //    catch (Exception ex)
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
            //    }
            //}
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
            if (e.CommandName == "seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gvProfesor.DataKeys[index].Value);
                string nombre = gvProfesor.Rows[index].Cells[0].Text;

                hdnIdProfesor.Value = id.ToString();
                txt_ProfesorSeleccionado.Text = nombre;
            }
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


    }

}