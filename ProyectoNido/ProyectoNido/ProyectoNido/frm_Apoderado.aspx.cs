using ProyectoNido.Auxiliar;
using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoNido
{
    public partial class frm_Apoderado : System.Web.UI.Page
    {
        bool Activo
        {
            get => (bool?)ViewState["Activo"] ?? false;
            set => ViewState["Activo"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CargarGrid();
                this.btn_Modificar.Enabled = false;

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

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsApoderado xApo = new clsApoderado();
                xApo.ArchivoBase = new clsArchivoBase();
                clsUsuario xusuario = new clsUsuario();
                xusuario.Distrito = new clsDistrito();
                xusuario.TipoDocumento = new clsTipoDocumento();

                xApo.Id = Convert.ToInt32(this.txt_IdApoderado.Text.Trim());

                xusuario.Id = Convert.ToInt32(this.txt_IdApoderado.Text.Trim());
                xusuario.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                xusuario.NombreUsuario = txt_Usuario.Text.Trim();
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
                xusuario.Activo = Activo;

                if (fup_Copia_Dni.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Copia_Dni.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (COPIA DNI) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xApo.ArchivoBase.NombreArchivo = fup_Copia_Dni.FileName;
                    xApo.ArchivoBase.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xApo.ArchivoBase.Archivo = fup_Copia_Dni.FileBytes;
                }

                xdb.ModUsuario(xusuario);
                xdb.ModApoderado(xApo);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Apoderado modificado correctamente.');",
                    true
                );
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
            catch (Exception ex)
            {
                string mensaje = ex.Message
                    .Replace("'", "\\'")
                    .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertErrorGeneral",
                    $"alert('Error inesperado: {mensaje}');",
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
            //Id_Usuario = Convert.ToInt32(Session["IdUsuario"]);
            var lista = xdb.GetApoderado().ToList();

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
            gvApoderados.DataSource = lista;
            gvApoderados.DataBind();
        }

        protected void gvApoderados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    var lista = xdb.GetApoderado(); // 

                    // Buscar el usuario correspondiente al ID
                    var Apod = lista.FirstOrDefault(u => u.Id == id);

                    if (Apod != null)
                    {
                        // Control de botones

                        this.btn_Modificar.Enabled = true;
                        //this.btn_Eliminar.Enabled = true;

                        txt_IdApoderado.Text = Apod.Id.ToString();
                        this.txt_Usuario.Text = Apod.NombreUsuario;
                        this.txt_Nombres.Text = Apod.Nombres;
                        this.txt_ApellidoPaterno.Text = Apod.ApellidoPaterno;
                        this.txt_ApellidoMaterno.Text = Apod.ApellidoMaterno;
                        Ddl_Tipo_Documento.SelectedValue =Apod.TipoDocumento.Id.ToString();
                        this.txt_Documento.Text = Apod.Documento;
                        txt_Fecha_Nacimiento.Text = Apod.FechaNacimiento.HasValue ? Apod.FechaNacimiento.Value.ToString("yyyy-MM-dd") : string.Empty;
                        Ddl_Sexo.SelectedValue = Apod.Sexo;

                        if (Apod.Distrito.Id.ToString() != "0")
                        {
                            Ddl_Distrito.SelectedValue = Apod.Distrito.Id.ToString();
                        }
                        else
                        {
                            Ddl_Distrito.SelectedIndex = -1;
                        }
                        txt_Direccion.Text = Apod.Direccion;
                        txt_Telefono.Text = Apod.Telefono;
                        txt_Email.Text = Apod.Email;
                        Activo = Apod.Activo;
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
            else if (e.CommandName == "VerArchivo")
            {
                try
                {
                    // Enviar el archivo al navegador
                    clsArchivoBase xAb = xdb.RetArchivoApoderado(id);

                    if (xAb.TamanioBytes>0)
                    {
                        Response.Clear();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";//"application/pdf"

                        // Codificar el nombre por si tiene espacios o caracteres especiales

                        string headerFileName = HttpUtility.UrlPathEncode(xAb.NombreArchivo);

                        Response.AddHeader("Content-Disposition", "attachment; filename=" + headerFileName);

                        Response.AddHeader("Content-Length", xAb.Archivo.Length.ToString());

                        Response.BinaryWrite(xAb.Archivo);

                        Response.Flush();

                        Response.End(); // Termina la respuesta
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sin archivo.');", true);
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
        }

        protected void gvApoderados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApoderados.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void Ddl_Tipo_Documento_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsValidacion.AplicarMaxLengthPorTipoDocumento(Ddl_Tipo_Documento, txt_Documento);
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                List<clsApoderado> lista = xdb.GetApoderado().ToList();

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

                gvApoderados.DataSource = lista;
                gvApoderados.DataBind();
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
                    $"alert('Error al cargar lista de comunicados: {mensaje}');",
                    true
                );

            }
        }

        private void LimpiarCampos()
        {
            this.btn_Modificar.Enabled = false;
            //this.btn_Eliminar.Enabled = false;

            clsValidacion.LimpiarControles(this);
        }
    }
}