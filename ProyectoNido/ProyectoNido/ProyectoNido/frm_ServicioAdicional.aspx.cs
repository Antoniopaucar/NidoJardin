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
    public partial class frm_ServicioAdicional : System.Web.UI.Page
    {
        // Plan (pseudocódigo detallado):
        // 1. En los handlers de btn_Agregar_Click y btn_Modificar_Click:
        //    - Leer el texto de txt_Tipo y normalizar a mayúsculas y trim.
        //    - Validar que tenga exactamente 1 carácter.
        //    - Validar que el carácter sea 'M', 'D' o 'H' (según restricción SQL Server).
        //    - Si no cumple, lanzar excepción con mensaje claro.
        //    - Asignar el primer carácter a xServ.Tipo.
        // 2. Mantener las validaciones existentes de costo y campos.
        // 3. No cambiar otra lógica de manejo de errores ni la UI.
        // 4. Aplicar la misma validación en agregar y modificar para consistencia.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Limpiar.Enabled = false;
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsServicioAdicional xServ = new clsServicioAdicional
                {
                    Nombre = txt_Nombre.Text.Trim(),
                    Descripcion = txt_Descripcion.Text.Trim()
                };

                // Tipo: CHAR(1) - validar que sea 'M', 'D' o 'H'
                string tipoTexto = txt_Tipo.Text.Trim().ToUpper();
                if (string.IsNullOrEmpty(tipoTexto) || tipoTexto.Length != 1)
                {
                    throw new Exception("Debe ingresar un tipo válido: 'M', 'D' o 'H'.");
                }
                if (!"MDH".Contains(tipoTexto))
                {
                    throw new Exception("Tipo inválido. Los valores permitidos son: 'M', 'D' o 'H'.");
                }
                xServ.Tipo = tipoTexto[0];

                // Costo: decimal
                if (!decimal.TryParse(txt_Costo.Text.Trim(), out decimal costo))
                {
                    throw new Exception("Debe ingresar un costo válido.");
                }
                xServ.Costo = costo;

                xdb.InsServicioAdicional(xServ);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Servicio adicional agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsServicioAdicional xServ = new clsServicioAdicional
                {
                    Id_ServicioAdicional = Convert.ToInt32(this.txt_IdServicioAdicional.Text.Trim()),
                    Nombre = txt_Nombre.Text.Trim(),
                    Descripcion = txt_Descripcion.Text.Trim()
                };

                // Tipo: CHAR(1) - validar que sea 'M', 'D' o 'H'
                string tipoTexto = txt_Tipo.Text.Trim().ToUpper();
                if (string.IsNullOrEmpty(tipoTexto) || tipoTexto.Length != 1)
                {
                    throw new Exception("Debe ingresar un tipo válido: 'M', 'D' o 'H'.");
                }
                if (!"MDH".Contains(tipoTexto))
                {
                    throw new Exception("Tipo inválido. Los valores permitidos son: 'M', 'D' o 'H'.");
                }
                xServ.Tipo = tipoTexto[0];

                if (!decimal.TryParse(txt_Costo.Text.Trim(), out decimal costo))
                {
                    throw new Exception("Debe ingresar un costo válido.");
                }
                xServ.Costo = costo;

                xdb.ModServicioAdicional(xServ);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Servicio adicional modificado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                int id = Convert.ToInt32(this.txt_IdServicioAdicional.Text.Trim());

                xdb.DelServicioAdicional(id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Servicio adicional eliminado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            var lista = xdb.GetServicioAdicional().ToList();

            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x => x.Nombre != null && x.Nombre.ToLower().Contains(filtro))
                    .ToList();
            }

            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            gvServicioAdicional.DataSource = lista;
            gvServicioAdicional.DataBind();
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsServicioAdicional> lista = xdb.GetServicioAdicional().ToList();

                gvServicioAdicional.DataSource = lista;
                gvServicioAdicional.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar la lista de servicios adicionales: {ex.Message}');", true);
            }
        }

        protected void gvServicioAdicional_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    var lista = xdb.GetServicioAdicional();
                    var serv = lista.FirstOrDefault(u => u.Id_ServicioAdicional == id);

                    if (serv != null)
                    {
                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdServicioAdicional.Text = serv.Id_ServicioAdicional.ToString();
                        txt_Nombre.Text = serv.Nombre;
                        txt_Descripcion.Text = serv.Descripcion;
                        txt_Tipo.Text = serv.Tipo.ToString();
                        txt_Costo.Text = serv.Costo.ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al consultar: {ex.Message}');", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.DelServicioAdicional(id);
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Servicio adicional eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvServicioAdicional_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvServicioAdicional.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            clsValidacion.LimpiarControles(this);
        }
    }
}