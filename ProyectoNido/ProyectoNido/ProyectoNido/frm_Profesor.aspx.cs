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
    public partial class frm_Profesor : System.Web.UI.Page
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

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsProfesor xProfe = new clsProfesor();
                xProfe.TituloProfesional = new clsArchivoBase();
                xProfe.Cv = new clsArchivoBase();
                xProfe.EvaluacionPsicologica = new clsArchivoBase();
                xProfe.Fotos = new clsArchivoBase();
                xProfe.VerificacionDomiciliaria = new clsArchivoBase();

                clsUsuario xusuario = new clsUsuario();
                xusuario.Distrito = new clsDistrito();
                xusuario.TipoDocumento = new clsTipoDocumento();

                xProfe.Id = Convert.ToInt32(this.txt_IdProfesor.Text.Trim());
                xProfe.FechaIngreso= DateTime.TryParse(txt_Fecha_Ingreso.Text.Trim(), out DateTime fi) ? fi : (DateTime?)null;

                xusuario.Id = Convert.ToInt32(this.txt_IdProfesor.Text.Trim());
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

                if (fup_Titulo_Profesional.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Titulo_Profesional.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (TITULO PROFESIONAL) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xProfe.TituloProfesional.NombreArchivo = fup_Titulo_Profesional.FileName;
                    xProfe.TituloProfesional.TamanioBytes = fup_Titulo_Profesional.FileBytes.Length;
                    xProfe.TituloProfesional.Archivo = fup_Titulo_Profesional.FileBytes;
                }

                if (fup_Cv.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Cv.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (CURRICULUM VITAE) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xProfe.Cv.NombreArchivo = fup_Cv.FileName;
                    xProfe.Cv.TamanioBytes = fup_Cv.FileBytes.Length;
                    xProfe.Cv.Archivo = fup_Cv.FileBytes;
                }

                if (fup_Evaluacion_Psicologica.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Evaluacion_Psicologica.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (EVALUACION PSICOLOGICA) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xProfe.EvaluacionPsicologica.NombreArchivo = fup_Evaluacion_Psicologica.FileName;
                    xProfe.EvaluacionPsicologica.TamanioBytes = fup_Evaluacion_Psicologica.FileBytes.Length;
                    xProfe.EvaluacionPsicologica.Archivo = fup_Evaluacion_Psicologica.FileBytes;
                }

                if (fup_Fotos.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Fotos.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (FOTOS) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xProfe.Fotos.NombreArchivo = fup_Fotos.FileName;
                    xProfe.Fotos.TamanioBytes = fup_Fotos.FileBytes.Length;
                    xProfe.Fotos.Archivo = fup_Fotos.FileBytes;
                }

                if (fup_Verificacion_Domiciliaria.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Verificacion_Domiciliaria.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (VERIFICACION DOMICILIARIA) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xProfe.VerificacionDomiciliaria.NombreArchivo = fup_Verificacion_Domiciliaria.FileName;
                    xProfe.VerificacionDomiciliaria.TamanioBytes = fup_Verificacion_Domiciliaria.FileBytes.Length;
                    xProfe.VerificacionDomiciliaria.Archivo = fup_Verificacion_Domiciliaria.FileBytes;
                }

                xdb.ModUsuario(xusuario);
                xdb.ModProfesor(xProfe);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Profesor modificado correctamente.');",
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

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            //Id_Usuario = Convert.ToInt32(Session["IdUsuario"]);
            var lista = xdb.GetProfesor().ToList();

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
            gvProfesor.DataSource = lista;
            gvProfesor.DataBind();
        }

        protected void gvProfesor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    var lista = xdb.GetProfesor(); // 

                    // Buscar el usuario correspondiente al ID
                    var Profe = lista.FirstOrDefault(u => u.Id == id);

                    if (Profe != null)
                    {
                        // Control de botones

                        this.btn_Modificar.Enabled = true;
                        //this.btn_Eliminar.Enabled = true;

                        txt_IdProfesor.Text = Profe.Id.ToString();
                        this.txt_Fecha_Ingreso.Text = Profe.FechaIngreso.HasValue ? Profe.FechaIngreso.Value.ToString("yyyy-MM-dd") : string.Empty;
                        this.txt_Usuario.Text = Profe.NombreUsuario;
                        this.txt_Nombres.Text = Profe.Nombres;
                        this.txt_ApellidoPaterno.Text = Profe.ApellidoPaterno;
                        this.txt_ApellidoMaterno.Text = Profe.ApellidoMaterno;
                        Ddl_Tipo_Documento.SelectedValue = Profe.TipoDocumento.Id.ToString();
                        this.txt_Documento.Text = Profe.Documento;
                        txt_Fecha_Nacimiento.Text = Profe.FechaNacimiento.HasValue ? Profe.FechaNacimiento.Value.ToString("yyyy-MM-dd") : string.Empty;
                        Ddl_Sexo.SelectedValue = Profe.Sexo;

                        if (Profe.Distrito.Id.ToString() != "0")
                        {
                            Ddl_Distrito.SelectedValue = Profe.Distrito.Id.ToString();
                        }
                        else
                        {
                            Ddl_Distrito.SelectedIndex = -1;
                        }
                        txt_Direccion.Text = Profe.Direccion;
                        txt_Telefono.Text = Profe.Telefono;
                        txt_Email.Text = Profe.Email;
                        Activo=Profe.Activo;
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
            else
            {
                try
                {
                    // Enviar el archivo al navegador
                    clsArchivoBase xAb = xdb.RetArchivoProfesor(id, e.CommandName.ToString());

                    if (xAb.TamanioBytes > 0)
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

        protected void gvProfesor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProfesor.PageIndex = e.NewPageIndex;
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

                List<clsProfesor> lista = xdb.GetProfesor().ToList();

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

                gvProfesor.DataSource = lista;
                gvProfesor.DataBind();
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