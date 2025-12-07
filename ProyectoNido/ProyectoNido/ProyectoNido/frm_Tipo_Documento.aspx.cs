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
    public partial class frm_Tipo_Documento : System.Web.UI.Page
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

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsTipoDocumento xTd = new clsTipoDocumento();

                xTd.Nombre = txt_Nombre.Text.Trim();
                xTd.Abreviatura = txt_Abreviatura.Text.Trim();
                xTd.CantidadCaracteres = Convert.ToInt32(txt_Cantidad_Caracteres.Text.Trim());

                xdb.InsTipoDocumento(xTd);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tipo de Documento agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "Alerta",
                    $"alert('Error: {mensaje}');",
                    true
                );
            }

        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsTipoDocumento xTd = new clsTipoDocumento();

                xTd.Id = Convert.ToInt32(this.txt_IdTipoDocumento.Text.Trim());
                xTd.Nombre = txt_Nombre.Text.Trim();
                xTd.Abreviatura = txt_Abreviatura.Text.Trim();
                xTd.CantidadCaracteres = Convert.ToInt32(txt_Cantidad_Caracteres.Text.Trim());

                xdb.ModTipoDocumento(xTd);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tipo de Documento modificado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "Alerta",
                    $"alert('Error: {mensaje}');",
                    true
                );
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

                clsTipoDocumento xTd = new clsTipoDocumento
                {
                    Id = Convert.ToInt32(this.txt_IdTipoDocumento.Text.Trim())
                };

                xdb.DelTipoDocumento(xTd.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tipo de Documento eliminado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "Alerta",
                    $"alert('Error: {mensaje}');",
                    true
                );
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            var lista = xdb.GetTipoDocumento().ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();

                lista = lista
                    .Where(x =>
                        (x.Nombre != null && x.Nombre.ToLower().Contains(filtro)) ||
                        (x.Abreviatura != null && x.Abreviatura.ToLower().Contains(filtro))
                    )
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvTipoDocumentos.DataSource = lista;
            gvTipoDocumentos.DataBind();
        }

        protected void gvTipoDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTipoDocumentos.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvTipoDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetTipoDocumento(); // 

                    // Buscar el usuario correspondiente al ID
                    var xtipd = lista.FirstOrDefault(u => u.Id == id);

                    if (xtipd != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdTipoDocumento.Text = xtipd.Id.ToString();
                        txt_Nombre.Text = xtipd.Nombre;
                        txt_Abreviatura.Text = xtipd.Abreviatura;
                        txt_Cantidad_Caracteres.Text = xtipd.CantidadCaracteres.ToString();

                    }

                }
                catch (System.ServiceModel.FaultException fex)
                {
                    string mensaje = fex.Message
                    .Replace("'", "\\'")
                    .Replace(Environment.NewLine, " ");

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Alerta",
                        $"alert('Error al consultar: {mensaje}');",
                        true
                    );
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.DelTipoDocumento(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tipo de documento eliminado correctamente.');", true);
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    string mensaje = fex.Message
                    .Replace("'", "\\'")
                    .Replace(Environment.NewLine, " ");

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Alerta",
                        $"alert('Error al eliminar: {mensaje}');",
                        true
                    );
                }
            }
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsTipoDocumento> lista = xdb.GetTipoDocumento().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.Nombre ?? "").ToLower().Contains(filtro) ||
                            (x.Abreviatura ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvTipoDocumentos.DataSource = lista;
                gvTipoDocumentos.DataBind();
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "Alerta",
                    $"alert('Error al cargar lista de distritos: {mensaje}');",
                    true
                );
            }
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