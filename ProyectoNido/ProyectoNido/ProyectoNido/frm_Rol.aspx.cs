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
    public partial class frm_Rol : System.Web.UI.Page
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

                clsRol xRol = new clsRol();

                xRol.NombreRol = txt_Nombre.Text.Trim();
                xRol.Descripcion = txt_Descripcion.Text.Trim();

                xdb.InsRol(xRol);
            
                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Rol agregado correctamente.');", true);
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

                clsRol xRol = new clsRol();

                xRol.Id = Convert.ToInt32(this.txt_IdRol.Text.Trim());
                xRol.NombreRol = txt_Nombre.Text.Trim();
                xRol.Descripcion = txt_Descripcion.Text.Trim();

                xdb.ModRol(xRol);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Rol modificado correctamente.');", true);
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

                clsRol xRol = new clsRol
                {
                    Id = Convert.ToInt32(this.txt_IdRol.Text.Trim())
                };

                xdb.DelRol(xRol.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Rol eliminado correctamente.');", true);
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
            var lista = xdb.GetRol().ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x => x.NombreRol != null && x.NombreRol.ToLower().Contains(filtro))
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvRol.DataSource = lista;
            gvRol.DataBind();
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsRol> lista = xdb.GetRol().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.NombreRol ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvRol.DataSource = lista;
                gvRol.DataBind();
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
                    $"alert('Error al cargar lista de roles: {mensaje}');",
                    true
                );
            }
        }

        protected void gvRol_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetRol(); // 

                    // Buscar el usuario correspondiente al ID
                    var Rol = lista.FirstOrDefault(u => u.Id == id);

                    if (Rol != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdRol.Text = Rol.Id.ToString();
                        txt_Nombre.Text = Rol.NombreRol;
                        txt_Descripcion.Text = Rol.Descripcion;

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
                    xdb.DelRol(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Rol eliminado correctamente.');", true);
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

        protected void gvRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRol.PageIndex = e.NewPageIndex;
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