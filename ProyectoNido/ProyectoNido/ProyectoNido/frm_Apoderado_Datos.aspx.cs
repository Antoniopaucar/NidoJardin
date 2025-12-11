using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_Apoderado_Datos : System.Web.UI.Page
    {
        // Propiedad para mantener el estado Activo del apoderado
        bool Activo
        {
            get => (bool?)ViewState["Activo"] ?? false;
            set => ViewState["Activo"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar sesión
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("frm_Login.aspx");
                    return;
                }

                CargarNombreApoderado();
                CargarHijos();
                CargarDropdowns();
                CargarDatosApoderado();
            }
        }

        /// <summary>
        /// Carga el nombre del apoderado en el panel lateral
        /// </summary>
        private void CargarNombreApoderado()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener datos del apoderado
                var apoderado = servicio.ObtenerApoderadoPorId(idUsuario);
                
                if (apoderado != null)
                {
                    lblNombreDocente.Text = $"{apoderado.Nombres} {apoderado.ApellidoPaterno}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar nombre apoderado: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga la lista de hijos (alumnos) del apoderado
        /// </summary>
        private void CargarHijos()
        {
            try
            {
                int idApoderado = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                var hijos = servicio.ListarAlumnosPorApoderado(idApoderado);
                
                if (hijos != null && hijos.Any())
                {
                    rptHijos.DataSource = hijos;
                    rptHijos.DataBind();
                }
                else
                {
                    rptHijos.DataSource = new List<wcfNido.clsAlumno>();
                    rptHijos.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar hijos: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorHijos", 
                    $"alert('Error al cargar hijos: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Carga los datos del apoderado en el formulario
        /// </summary>
        private void CargarDatosApoderado()
        {
            try
            {
                int idApoderado = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                var apoderado = servicio.ObtenerApoderadoPorId(idApoderado);
                
                if (apoderado != null)
                {
                    txt_IdApoderado.Text = apoderado.Id.ToString();
                    txt_Usuario.Text = apoderado.NombreUsuario;
                    txt_Nombres.Text = apoderado.Nombres;
                    txt_ApellidoPaterno.Text = apoderado.ApellidoPaterno;
                    txt_ApellidoMaterno.Text = apoderado.ApellidoMaterno;
                    
                    if (apoderado.TipoDocumento != null && apoderado.TipoDocumento.Id > 0)
                    {
                        Ddl_Tipo_Documento.SelectedValue = apoderado.TipoDocumento.Id.ToString();
                    }
                    
                    txt_Documento.Text = apoderado.Documento;
                    
                    if (apoderado.FechaNacimiento.HasValue)
                    {
                        txt_Fecha_Nacimiento.Text = apoderado.FechaNacimiento.Value.ToString("yyyy-MM-dd");
                    }
                    
                    if (!string.IsNullOrEmpty(apoderado.Sexo))
                    {
                        Ddl_Sexo.SelectedValue = apoderado.Sexo;
                    }
                    
                    if (apoderado.Distrito != null && apoderado.Distrito.Id > 0)
                    {
                        Ddl_Distrito.SelectedValue = apoderado.Distrito.Id.ToString();
                    }
                    
                    txt_Direccion.Text = apoderado.Direccion;
                    txt_Telefono.Text = apoderado.Telefono;
                    txt_Email.Text = apoderado.Email;
                    
                    // Guardar estado activo
                    Activo = apoderado.Activo;
                    
                    // Actualizar estado de documentos
                    ActualizarEstadoDocumentos(idApoderado);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar datos apoderado: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorDatos", 
                    $"alert('Error al cargar datos: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Carga los dropdowns (TipoDocumento, Sexo, Distrito)
        /// </summary>
        private void CargarDropdowns()
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Cargar Tipo Documento
                var tiposDocumento = servicio.GetTipoDocumento();
                Ddl_Tipo_Documento.DataSource = tiposDocumento;
                Ddl_Tipo_Documento.DataTextField = "Nombre";
                Ddl_Tipo_Documento.DataValueField = "Id";
                Ddl_Tipo_Documento.DataBind();
                Ddl_Tipo_Documento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                
                // Cargar Sexo
                Ddl_Sexo.Items.Clear();
                Ddl_Sexo.Items.Add(new ListItem("-- Seleccione --", ""));
                Ddl_Sexo.Items.Add(new ListItem("Masculino", "M"));
                Ddl_Sexo.Items.Add(new ListItem("Femenino", "F"));
                
                // Cargar Distrito
                var distritos = servicio.GetDistrito();
                Ddl_Distrito.DataSource = distritos;
                Ddl_Distrito.DataTextField = "Nombre";
                Ddl_Distrito.DataValueField = "Id";
                Ddl_Distrito.DataBind();
                Ddl_Distrito.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar dropdowns: {ex.Message}");
            }
        }

        /// <summary>
        /// Maneja el evento click de los botones de hijos
        /// </summary>
        protected void btnHijo_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idAlumno = Convert.ToInt32(btn.CommandArgument);
                
                // Guardar el ID del alumno seleccionado en sesión
                Session["IdAlumnoSeleccionado"] = idAlumno;
                
                // Recargar la página para actualizar el estado activo de los botones
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al seleccionar hijo: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorSeleccion", 
                    $"alert('Error al seleccionar hijo: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Maneja la modificación de datos del apoderado
        /// </summary>
        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener el apoderado actual
                int idApoderado = Convert.ToInt32(Session["IdUsuario"]);
                var apoderado = servicio.ObtenerApoderadoPorId(idApoderado);
                
                if (apoderado == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorNoEncontrado", 
                        "alert('No se encontraron los datos del apoderado.');", true);
                    return;
                }
                
                // Crear objeto Usuario para actualizar datos comunes
                wcfNido.clsUsuario usuario = new wcfNido.clsUsuario();
                usuario.Distrito = new wcfNido.clsDistrito();
                usuario.TipoDocumento = new wcfNido.clsTipoDocumento();
                
                // IDs
                usuario.Id = idApoderado;
                
                // Actualizar datos del usuario (datos comunes)
                usuario.TipoDocumento.Id = int.TryParse(Ddl_Tipo_Documento.SelectedValue, out int idTipoDoc) && idTipoDoc > 0 ? idTipoDoc : 0;
                usuario.NombreUsuario = apoderado.NombreUsuario; // Mantener el nombre de usuario
                usuario.Nombres = txt_Nombres.Text.Trim();
                usuario.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                usuario.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                usuario.Documento = txt_Documento.Text.Trim();
                usuario.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text, out DateTime fechaNac) ? fechaNac : (DateTime?)null;
                usuario.Sexo = Ddl_Sexo.SelectedValue;
                usuario.Distrito.Id = int.TryParse(Ddl_Distrito.SelectedValue, out int idDistrito) && idDistrito > 0 ? idDistrito : 0;
                usuario.Direccion = txt_Direccion.Text.Trim();
                usuario.Telefono = txt_Telefono.Text.Trim();
                usuario.Email = txt_Email.Text.Trim();
                usuario.Activo = apoderado.Activo; // Mantener el estado activo original
                
                // Inicializar ArchivoBase si no existe
                if (apoderado.ArchivoBase == null)
                    apoderado.ArchivoBase = new wcfNido.clsArchivoBase();
                
                // Procesar archivo Copia DNI
                if (!ProcesarArchivo(fup_Copia_Dni, apoderado.ArchivoBase, "COPIA DNI"))
                    return;
                
                // Modificar usuario y apoderado (similar a como se hace con profesor)
                servicio.ModUsuario(usuario);
                servicio.ModApoderado(apoderado);
                
                // Mostrar mensaje de éxito
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alert",
                    "alert('Apoderado modificado correctamente.');",
                    true
                );
                
                // Recargar datos
                CargarDatosApoderado();
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

        /// <summary>
        /// Limpia los campos del formulario
        /// </summary>
        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recargar los datos originales del apoderado
                CargarDatosApoderado();
                
                // Limpiar el FileUpload
                fup_Copia_Dni.Attributes.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al limpiar campos: {ex.Message}");
            }
        }

        /// <summary>
        /// Maneja el cambio de tipo de documento (para validaciones futuras)
        /// </summary>
        protected void Ddl_Tipo_Documento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí se pueden agregar validaciones según el tipo de documento seleccionado
            // Por ejemplo, validar formato de DNI, RUC, etc.
        }

        /// <summary>
        /// Procesa un archivo subido: valida tamaño y carga en el objeto ArchivoBase
        /// </summary>
        private bool ProcesarArchivo(FileUpload fileUpload, wcfNido.clsArchivoBase archivoBase, string nombreMostrar)
        {
            if (fileUpload.HasFile)
            {
                int maxBytes = 5 * 1024 * 1024; // 5 MB
                if (fileUpload.PostedFile.ContentLength > maxBytes)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSize", 
                        $"alert('El archivo ({nombreMostrar}) supera el límite máximo permitido de 5 MB.');", true);
                    return false;
                }

                archivoBase.NombreArchivo = fileUpload.FileName;
                archivoBase.TamanioBytes = fileUpload.FileBytes.Length;
                archivoBase.Archivo = fileUpload.FileBytes;
            }
            return true;
        }

        /// <summary>
        /// Maneja la descarga de archivos del apoderado
        /// </summary>
        public void DescargarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(Session["IdUsuario"]);

            wcfNido.Service1Client servicio = new wcfNido.Service1Client();

            try
            {
                // Obtener el archivo Copia DNI del apoderado
                wcfNido.clsArchivoBase archivo = servicio.RetArchivoApoderado(id);

                if (archivo != null && archivo.TamanioBytes > 0)
                {
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; // Default
                    
                    // Ajustar ContentType según la extensión
                    string extension = System.IO.Path.GetExtension(archivo.NombreArchivo).ToLower();
                    if (extension == ".pdf") 
                        Response.ContentType = "application/pdf";
                    else if (extension == ".jpg" || extension == ".jpeg") 
                        Response.ContentType = "image/jpeg";
                    else if (extension == ".png") 
                        Response.ContentType = "image/png";
                    else if (extension == ".doc") 
                        Response.ContentType = "application/msword";
                    else if (extension == ".docx") 
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                    string headerFileName = HttpUtility.UrlPathEncode(archivo.NombreArchivo);
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + headerFileName);
                    Response.AddHeader("Content-Length", archivo.Archivo.Length.ToString());
                    Response.BinaryWrite(archivo.Archivo);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNoArchivo", 
                        "alert('El archivo no se encontró o está vacío.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message.Replace("'", "\\'").Replace(Environment.NewLine, " ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", 
                    $"alert('Error al descargar: {mensaje}');", true);
            }
            catch (Exception ex)
            {
                // Response.End() lanza ThreadAbortException, es normal
                if (!(ex is System.Threading.ThreadAbortException))
                {
                    string mensaje = ex.Message.Replace("'", "\\'").Replace(Environment.NewLine, " ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertErrorDescarga", 
                        $"alert('Error inesperado: {mensaje}');", true);
                }
            }
        }

        /// <summary>
        /// Verifica si el documento Copia DNI existe en la BD
        /// </summary>
        private bool VerificarDocumentoExisteEnBD(int idApoderado)
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                wcfNido.clsArchivoBase archivo = servicio.RetArchivoApoderado(idApoderado);
                
                // El documento existe si tiene tamaño mayor a 0
                return archivo != null && archivo.TamanioBytes > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Actualiza la visibilidad de los controles de documento según si existe en la BD
        /// Muestra "Descargar" si existe, "Cargar documento" si no existe
        /// </summary>
        private void ActualizarEstadoDocumentos(int idApoderado)
        {
            // Copia DNI
            bool existeCopiaDni = VerificarDocumentoExisteEnBD(idApoderado);
            lnk_Copia_Dni.Visible = existeCopiaDni;
            lnk_Copia_Dni.Text = "Descargar";
            lbl_Copia_Dni_Msg.Visible = !existeCopiaDni;
            lbl_Copia_Dni_Msg.Text = "Cargar documento";
        }
    }
}
