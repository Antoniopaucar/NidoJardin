using ProyectoNido.Auxiliar;
using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
                this.chb_Activo.Checked = true;

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                List<clsTipoDocumento> listatd = xdb.GetTipoDocumento().ToList();

                Ddl_Tipo_Documento.DataSource = listatd;
                Ddl_Tipo_Documento.DataTextField = "Nombre";
                Ddl_Tipo_Documento.DataValueField = "Id";
                Ddl_Tipo_Documento.DataBind();
                Ddl_Tipo_Documento.Items.Insert(0, new ListItem("-- Seleccione un Documento --", ""));

                List<clsDistrito> lista = xdb.GetDistrito().ToList();

                Ddl_Distrito.DataSource = lista;
                Ddl_Distrito.DataTextField = "Nombre";
                Ddl_Distrito.DataValueField = "Id";
                Ddl_Distrito.DataBind();
                Ddl_Distrito.Items.Insert(0, new ListItem("-- Seleccione un Distrito --", ""));

                // Cargar sexos (sin clase ni BD)
                Ddl_Sexo.Items.Clear();
                Ddl_Sexo.Items.Add(new ListItem("-- Seleccione un Sexo --", ""));
                Ddl_Sexo.Items.Add(new ListItem("Masculino", "M"));
                Ddl_Sexo.Items.Add(new ListItem("Femenino", "F"));
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsUsuario xusuario = new clsUsuario();
                xusuario.Distrito = new clsDistrito();
                xusuario.TipoDocumento = new clsTipoDocumento();

                xusuario.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                xusuario.NombreUsuario = txt_NombreUsuario.Text.Trim();
                xusuario.Clave = txt_Contrasenia.Text.Trim();
                xusuario.ClaveII = txt_Repetir_Contrasenia.Text.Trim();
                xusuario.Nombres = txt_Nombres.Text.Trim();
                xusuario.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xusuario.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xusuario.Documento = txt_Documento.Text.Trim();
                xusuario.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                xusuario.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue)
                ? null
                : Ddl_Sexo.SelectedValue;
                if (string.IsNullOrEmpty(Ddl_Distrito.SelectedValue))
                {
                    xusuario.Distrito = null;   // ← distrito NO seleccionado
                }
                else
                {
                    xusuario.Distrito.Id = Convert.ToInt32(Ddl_Distrito.SelectedValue);
                }

                xusuario.Direccion = txt_Direccion.Text.Trim();
                xusuario.Telefono = txt_Telefono.Text.Trim();
                xusuario.Email = txt_Email.Text.Trim();
                
                xdb.InsUsuarios(xusuario);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
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

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuario xusuario = new clsUsuario();
                xusuario.Distrito = new clsDistrito();
                xusuario.TipoDocumento = new clsTipoDocumento();

                xusuario.Id = Convert.ToInt32(this.txt_IdUsuario.Text.Trim());
                xusuario.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                xusuario.NombreUsuario = txt_NombreUsuario.Text.Trim();

                if (!string.IsNullOrEmpty(xusuario.Clave))
                {
                    xusuario.Clave = txt_Contrasenia.Text.Trim();
                    xusuario.ClaveII = txt_Repetir_Contrasenia.Text.Trim();
                }
                
                xusuario.Nombres = txt_Nombres.Text.Trim();
                xusuario.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xusuario.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xusuario.Documento = txt_Documento.Text.Trim();
                xusuario.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                xusuario.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue)
                    ? null
                    : Ddl_Sexo.SelectedValue;
                if (string.IsNullOrEmpty(Ddl_Distrito.SelectedValue))
                {
                    xusuario.Distrito = null;   // ← distrito NO seleccionado
                }
                else
                {
                    xusuario.Distrito.Id = Convert.ToInt32(Ddl_Distrito.SelectedValue);
                }
                xusuario.Direccion = txt_Direccion.Text.Trim();
                xusuario.Telefono = txt_Telefono.Text.Trim();
                xusuario.Email = txt_Email.Text.Trim();
                xusuario.Activo = this.chb_Activo.Checked;

                xdb.ModUsuario(xusuario);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario modificado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
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

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuario xusuario = new clsUsuario
                {
                    Id = Convert.ToInt32(this.txt_IdUsuario.Text.Trim())
                };

                xdb.DelUsuarios(xusuario.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario eliminado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
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

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuario> lista = xdb.GetUsuario().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.NombreUsuario ?? "").ToLower().Contains(filtro) ||
                            (x.Nombres ?? "").ToLower().Contains(filtro) ||
                            (x.ApellidoPaterno ?? "").ToLower().Contains(filtro) ||
                            (x.ApellidoMaterno ?? "").ToLower().Contains(filtro) ||
                            (x.Documento ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvUsuarios.DataSource = lista;
                gvUsuarios.DataBind();
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertError",
                    $"alert('Error al cargar lista de Usuario: {mensaje}');",
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
            var lista = xdb.GetUsuario().ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();

                lista = lista
                    .Where(x =>
                        (x.NombreUsuario != null && x.NombreUsuario.ToLower().Contains(filtro)) ||
                        (x.Nombres != null && x.Nombres.ToLower().Contains(filtro)) ||
                        (x.ApellidoPaterno != null && x.ApellidoPaterno.ToLower().Contains(filtro)) ||
                        (x.ApellidoMaterno != null && x.ApellidoMaterno.ToLower().Contains(filtro)) ||
                        (x.Documento != null && x.Documento.ToLower().Contains(filtro))
                    )
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetUsuario(); // o GetUsuarios() si ese es el nombre correcto

                    // Buscar el usuario correspondiente al ID
                    var user = lista.FirstOrDefault(u => u.Id == id);

                    if (user != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdUsuario.Text = user.Id.ToString();
                        Ddl_Tipo_Documento.SelectedValue = user.TipoDocumento.Id.ToString();
                        txt_NombreUsuario.Text = user.NombreUsuario;
                        txt_Nombres.Text = user.Nombres;
                        txt_ApellidoPaterno.Text = user.ApellidoPaterno;
                        txt_ApellidoMaterno.Text = user.ApellidoMaterno;
                        txt_Contrasenia.Text = user.Clave;
                        txt_Repetir_Contrasenia.Text = user.ClaveII;
                        txt_Documento.Text = user.Documento;
                        txt_Fecha_Nacimiento.Text = user.FechaNacimiento.HasValue ? user.FechaNacimiento.Value.ToString("yyyy-MM-dd") : string.Empty;
                        Ddl_Sexo.SelectedValue = user.Sexo;

                        if (user.Distrito.Id.ToString() != "0")
                        {
                            Ddl_Distrito.SelectedValue = user.Distrito.Id.ToString();
                        }
                        else
                        {
                            Ddl_Distrito.SelectedIndex = -1;
                        }
                        txt_Direccion.Text = user.Direccion;
                        txt_Telefono.Text = user.Telefono;
                        txt_Email.Text = user.Email;
                        chb_Activo.Checked =  user.Activo;
                        
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
                        "alertError",
                        $"alert('Error al consultar: {mensaje}');",
                        true
                    );
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.DelUsuarios(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario eliminado correctamente.');", true);
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    string mensaje = fex.Message
                    .Replace("'", "\\'")
                    .Replace(Environment.NewLine, " ");

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "alertError",
                        $"alert('Error al eliminar: {mensaje}');",
                        true
                    );
                }
            }
        }

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            clsValidacion.LimpiarControles(this);
            this.chb_Activo.Checked = true;
        }

        protected void Ddl_Tipo_Documento_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsValidacion.AplicarMaxLengthPorTipoDocumento(Ddl_Tipo_Documento,txt_Documento);
        }
    }
}