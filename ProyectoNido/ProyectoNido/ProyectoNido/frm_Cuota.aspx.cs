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
    public partial class frm_Cuota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTarifarios();
                CargarGrid();  // mostrará vacío si no hay tarifario seleccionado
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
            }
        }

        // CARGAR COMBO TARIFARIO

        private void CargarTarifarios()
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                var lista = xdb.GetTarifario_1().ToList();

                ddl_Tarifario.DataSource = lista;
                ddl_Tarifario.DataTextField = "Nombre";       // texto que se ve
                ddl_Tarifario.DataValueField = "Id_Tarifario"; // valor (PK)
                ddl_Tarifario.DataBind();

                ddl_Tarifario.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione un Tarifario --", ""));
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError("Error al cargar Tarifarios", fex);
            }
        }

        protected void ddl_Tarifario_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarGrid();
        }

        // AGREGAR
        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddl_Tarifario.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un tarifario.');", true);
                    return;
                }

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsCuota c = ObtenerCuotaDesdeFormulario();

                bool ok = xdb.InsertarCuota(c);

                if (ok)
                {
                    LimpiarCampos();
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cuota agregada correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudo agregar la cuota.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError("Error al agregar la cuota", fex);
            }
        }

        // MODIFICAR
        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_IdCuota.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione una cuota de la lista.');", true);
                    return;
                }

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsCuota c = ObtenerCuotaDesdeFormulario();
                c.Id_Cuota = Convert.ToInt32(txt_IdCuota.Text);

                bool ok = xdb.ActualizarCuota(c);

                if (ok)
                {
                    LimpiarCampos();
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cuota modificada correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudo modificar la cuota.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError("Error al modificar la cuota", fex);
            }
        }

        // ELIMINAR
        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_IdCuota.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione una cuota para eliminar.');", true);
                    return;
                }

                int idCuota = Convert.ToInt32(txt_IdCuota.Text);
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                bool ok = xdb.EliminarCuota(idCuota);

                if (ok)
                {
                    LimpiarCampos();
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cuota eliminada correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudo eliminar la cuota.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError("Error al eliminar la cuota", fex);
            }
        }

        // LIMPIAR
        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            txt_IdCuota.Text = "";
            txt_NroCuota.Text = "";
            txt_FechaPagoSugerido.Text = "";
            txt_Monto.Text = "";
            txt_Descuento.Text = "";
            txt_Adicional.Text = "";
            txt_NombreCuota.Text = "";

            if (ddl_Tarifario.Items.Count > 0 && ddl_Tarifario.SelectedIndex < 0)
                ddl_Tarifario.SelectedIndex = 0;
        }


        // ARMAR OBJETO DESDE FORMULARIO

        private clsCuota ObtenerCuotaDesdeFormulario()
        {
            clsCuota c = new clsCuota();

            c.Id_Tarifario = string.IsNullOrEmpty(ddl_Tarifario.SelectedValue)
                ? 0
                : Convert.ToInt32(ddl_Tarifario.SelectedValue);

            c.NroCuota = int.TryParse(txt_NroCuota.Text, out int nro) ? nro : 0;

            if (DateTime.TryParse(txt_FechaPagoSugerido.Text, out DateTime f))
                c.FechaPagoSugerido = f;
            else
                c.FechaPagoSugerido = DateTime.Today;

            c.Monto = decimal.TryParse(txt_Monto.Text, out decimal m) ? m : (decimal?)null;
            c.Descuento = decimal.TryParse(txt_Descuento.Text, out decimal d) ? d : (decimal?)null;
            c.Adicional = decimal.TryParse(txt_Adicional.Text, out decimal a) ? a : (decimal?)null;
            c.NombreCuota = txt_NombreCuota.Text.Trim();

            return c;
        }


        // CARGAR GRID

        private void CargarGrid(string filtro = "")
        {
            try
            {
                if (string.IsNullOrEmpty(ddl_Tarifario.SelectedValue))
                {
                    gvCuota.DataSource = null;
                    gvCuota.DataBind();
                    lblMensaje.Text = "Seleccione un tarifario para ver sus cuotas.";
                    return;
                }

                int idTarifario = Convert.ToInt32(ddl_Tarifario.SelectedValue);

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsCuota> lista = xdb.GetCuota(idTarifario).ToList(); // método WCF

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista.Where(x =>
                        (x.NombreCuota ?? "").ToLower().Contains(filtro) ||
                        x.NroCuota.ToString().Contains(filtro) ||
                        (x.Monto.HasValue && x.Monto.Value.ToString().Contains(filtro))
                    ).ToList();
                }

                lblMensaje.Text = (lista.Count == 0)
                    ? "No hay cuotas para el tarifario seleccionado."
                    : "";

                gvCuota.DataSource = lista;
                gvCuota.DataBind();
            }
            catch (System.ServiceModel.FaultException fex)
            {
                MostrarError("Error al cargar cuotas", fex);
            }
        }


        // FILTRAR

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrid(txtBuscar.Text.Trim());
        }

        // GRID COMMAND
        protected void gvCuota_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    if (string.IsNullOrEmpty(ddl_Tarifario.SelectedValue)) return;

                    int idTarifario = Convert.ToInt32(ddl_Tarifario.SelectedValue);
                    var lista = xdb.GetCuota(idTarifario).ToList();
                    var cuota = lista.FirstOrDefault(c => c.Id_Cuota == id);

                    if (cuota != null)
                    {
                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdCuota.Text = cuota.Id_Cuota.ToString();
                        ddl_Tarifario.SelectedValue = cuota.Id_Tarifario.ToString();
                        txt_NroCuota.Text = cuota.NroCuota.ToString();
                        txt_FechaPagoSugerido.Text = cuota.FechaPagoSugerido.ToString("yyyy-MM-dd");
                        txt_Monto.Text = cuota.Monto.HasValue ? cuota.Monto.Value.ToString("N2") : "";
                        txt_Descuento.Text = cuota.Descuento.HasValue ? cuota.Descuento.Value.ToString("N2") : "";
                        txt_Adicional.Text = cuota.Adicional.HasValue ? cuota.Adicional.Value.ToString("N2") : "";
                        txt_NombreCuota.Text = cuota.NombreCuota;
                    }
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    MostrarError("Error al consultar la cuota", fex);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.EliminarCuota(id);
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cuota eliminada correctamente.');", true);
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    MostrarError("Error al eliminar la cuota", fex);
                }
            }
        }

        // PAGINACIÓN
        protected void gvCuota_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvCuota.PageIndex = e.NewPageIndex;
            CargarGrid(txtBuscar.Text.Trim());
        }
        // MOSTRAR ERROR
        private void MostrarError(string descripcion, System.ServiceModel.FaultException fex)
        {
            string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alertError",
                $"alert('{descripcion}: {mensaje}');",
                true
            );
        }
    }
}