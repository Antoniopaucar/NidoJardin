using ProyectoNido.Auxiliar;
using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Tarifario : System.Web.UI.Page
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
        // ============================
        // AGREGAR
        // ============================
        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsTarifario xt = new clsTarifario();

                xt.Tipo = ddl_Tipo.SelectedValue;
                xt.Nombre = txt_Nombre.Text.Trim();
                xt.Descripcion = txt_Descripcion.Text.Trim();
                xt.Periodo = int.TryParse(txt_Periodo.Text, out int p) ? p : 0;
                xt.Valor = decimal.TryParse(txt_Valor.Text, out decimal v) ? v : 0;

                bool ok = xdb.InsertarTarifario(xt);

                if (ok)
                {
                    LimpiarCampos();
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tarifario agregado correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudo agregar el tarifario.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError(fex);
            }
        }

        // ============================
        // MODIFICAR
        // ============================
        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsTarifario xt = new clsTarifario();

                xt.Id_Tarifario = Convert.ToInt32(txt_IdTarifario.Text);
                xt.Tipo = ddl_Tipo.SelectedValue;
                xt.Nombre = txt_Nombre.Text.Trim();
                xt.Descripcion = txt_Descripcion.Text.Trim();
                xt.Periodo = int.TryParse(txt_Periodo.Text, out int p) ? p : 0;
                xt.Valor = decimal.TryParse(txt_Valor.Text, out decimal v) ? v : 0;

                bool ok = xdb.ActualizarTarifario(xt);

                if (ok)
                {
                    LimpiarCampos();
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tarifario modificado correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudo modificar el tarifario.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError(fex);
            }
        }

        // ============================
        // ELIMINAR
        // ============================
        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                int id = Convert.ToInt32(txt_IdTarifario.Text);
                bool ok = xdb.EliminarTarifario(id);

                if (ok)
                {
                    LimpiarCampos();
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tarifario eliminado correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudo eliminar el tarifario.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError(fex);
            }
        }

        // ============================
        // LIMPIAR
        // ============================
        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            clsValidacion.LimpiarControles(this);

            ddl_Tipo.SelectedIndex = 0;
        }

        // ============================
        // CARGAR GRID
        // ============================
        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsTarifario> lista = xdb.GetTarifario_1().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();
                    lista = lista.Where(t =>
                        (t.Nombre ?? "").ToLower().Contains(filtro) ||
                        (t.Tipo ?? "").ToLower().Contains(filtro) ||
                        t.Periodo.ToString().Contains(filtro)
                    ).ToList();
                }

                gvTarifario.DataSource = lista;
                gvTarifario.DataBind();
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError(fex);
            }
        }

        // ============================
        // FILTRAR
        // ============================
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrid(txtBuscar.Text.Trim());
        }

        // ============================
        // GRID COMMAND
        // ============================
        protected void gvTarifario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    var lista = xdb.GetTarifario_1().ToList();
                    var tar = lista.FirstOrDefault(t => t.Id_Tarifario == id);

                    if (tar != null)
                    {
                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdTarifario.Text = tar.Id_Tarifario.ToString();
                        ddl_Tipo.SelectedValue = tar.Tipo ?? "";
                        txt_Nombre.Text = tar.Nombre;
                        txt_Descripcion.Text = tar.Descripcion;
                        txt_Periodo.Text = tar.Periodo.ToString();
                        txt_Valor.Text = tar.Valor.ToString("N2");
                    }
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    MostrarError(fex);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.EliminarTarifario(id);
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tarifario eliminado correctamente.');", true);
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    MostrarError(fex);
                }
            }
        }

        // ============================
        // PAGINACION
        // ============================
        protected void gvTarifario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTarifario.PageIndex = e.NewPageIndex;
            CargarGrid(txtBuscar.Text.Trim());
        }

        // ============================
        // MOSTRAR ERROR WCF
        // ============================
        private void MostrarError(System.ServiceModel.FaultException fex)
        {
            string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alertError",
                $"alert('Error: {mensaje}');",
                true
            );
        }
    }
}