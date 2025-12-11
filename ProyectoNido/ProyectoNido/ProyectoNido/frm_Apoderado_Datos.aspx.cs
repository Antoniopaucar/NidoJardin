using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
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
        /// Redirige a frm_Apoderado_hijos.aspx para ver y editar los datos del hijo
        /// </summary>
        protected void btnHijo_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idAlumno = Convert.ToInt32(btn.CommandArgument);
                
                // Guardar el ID del alumno seleccionado en sesión
                Session["IdAlumnoSeleccionado"] = idAlumno;
                
                // Redirigir a la página de hijos para ver y editar los datos del hijo seleccionado
                Response.Redirect("frm_Apoderado_hijos.aspx");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al seleccionar hijo: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorSeleccion", 
                    $"alert('Error al seleccionar hijo: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// WebMethod para seleccionar un hijo usando AJAX (sin recargar página)
        /// </summary>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SeleccionarHijo(int idAlumno)
        {
            try
            {
                if (HttpContext.Current.Session["IdUsuario"] == null)
                {
                    return "ERROR:SIN_SESION";
                }

                // Guardar el ID del alumno seleccionado en sesión
                HttpContext.Current.Session["IdAlumnoSeleccionado"] = idAlumno;
                
                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERROR:{ex.Message}";
            }
        }

        /// <summary>
        /// WebMethod para deseleccionar hijo (Ver Todos)
        /// </summary>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string DeseleccionarHijo()
        {
            try
            {
                if (HttpContext.Current.Session["IdUsuario"] == null)
                {
                    return "ERROR:SIN_SESION";
                }

                // Limpiar la selección
                HttpContext.Current.Session["IdAlumnoSeleccionado"] = null;
                
                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERROR:{ex.Message}";
            }
        }

        /// <summary>
        /// WebMethod para obtener información completa del hijo seleccionado
        /// </summary>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string ObtenerInfoHijo(int idAlumno)
        {
            try
            {
                if (HttpContext.Current.Session["IdUsuario"] == null)
                {
                    return "ERROR:SIN_SESION";
                }

                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener todos los hijos del apoderado
                int idApoderado = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
                var hijos = servicio.ListarAlumnosPorApoderado(idApoderado);
                
                // Buscar el hijo específico
                var hijo = hijos?.FirstOrDefault(h => h.Id == idAlumno);
                
                if (hijo == null)
                {
                    return "ERROR:HIJO_NO_ENCONTRADO";
                }

                // Calcular edad
                string edad = "N/A";
                if (hijo.FechaNacimiento.HasValue)
                {
                    int años = DateTime.Now.Year - hijo.FechaNacimiento.Value.Year;
                    if (DateTime.Now.DayOfYear < hijo.FechaNacimiento.Value.DayOfYear)
                        años--;
                    edad = $"{años} años";
                }

                // Construir JSON con la información
                System.Text.StringBuilder json = new System.Text.StringBuilder();
                json.Append("{");
                json.Append($"\"Id\":{hijo.Id},");
                json.Append($"\"Nombres\":\"{hijo.Nombres}\",");
                json.Append($"\"ApellidoPaterno\":\"{hijo.ApellidoPaterno ?? ""}\",");
                json.Append($"\"ApellidoMaterno\":\"{hijo.ApellidoMaterno ?? ""}\",");
                json.Append($"\"Documento\":\"{hijo.Documento ?? ""}\",");
                json.Append($"\"FechaNacimiento\":\"{(hijo.FechaNacimiento.HasValue ? hijo.FechaNacimiento.Value.ToString("dd/MM/yyyy") : "N/A")}\",");
                json.Append($"\"Edad\":\"{edad}\",");
                json.Append($"\"Sexo\":\"{(hijo.Sexo == "M" ? "Masculino" : hijo.Sexo == "F" ? "Femenino" : "N/A")}\"");
                json.Append("}");

                return json.ToString();
            }
            catch (Exception ex)
            {
                return $"ERROR:{ex.Message}";
            }
        }

        /// <summary>
        /// Maneja la modificación de datos del apoderado
        /// </summary>
        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                wcfNido.clsApoderado xApo = new wcfNido.clsApoderado();
                
                // Inicializar ArchivoBase
                xApo.ArchivoBase = new wcfNido.clsArchivoBase();

                wcfNido.clsUsuario xusuario = new wcfNido.clsUsuario();
                xusuario.Distrito = new wcfNido.clsDistrito();
                xusuario.TipoDocumento = new wcfNido.clsTipoDocumento();

                // IDs
                xApo.Id = Convert.ToInt32(this.txt_IdApoderado.Text.Trim());
                xusuario.Id = Convert.ToInt32(this.txt_IdApoderado.Text.Trim());
                
                // Validar TipoDocumento igual que en docente
                if (!string.IsNullOrEmpty(Ddl_Tipo_Documento.SelectedValue) && Ddl_Tipo_Documento.SelectedValue != "0")
                {
                    xusuario.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                }
                else
                {
                    throw new ArgumentException("Debe seleccionar un Tipo de Documento.");
                }
                
                xusuario.NombreUsuario = txt_Usuario.Text.Trim();
                xusuario.Nombres = txt_Nombres.Text.Trim();
                xusuario.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xusuario.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xusuario.Documento = txt_Documento.Text.Trim();
                xusuario.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                xusuario.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue)
                    ? null
                    : Ddl_Sexo.SelectedValue;
                if (string.IsNullOrEmpty(Ddl_Distrito.SelectedValue) || Ddl_Distrito.SelectedValue == "0")
                {
                    xusuario.Distrito = null;
                }
                else
                {
                    xusuario.Distrito.Id = Convert.ToInt32(Ddl_Distrito.SelectedValue);
                }
                xusuario.Direccion = txt_Direccion.Text.Trim();
                xusuario.Telefono = txt_Telefono.Text.Trim();
                xusuario.Email = txt_Email.Text.Trim();
                xusuario.Activo = Activo; // Mantener el estado activo original

                // NO establecer Clave ni ClaveII - igual que en frm_Docente_Datos
                // Esto permite que el método modificar_usuario en BL no valide la contraseña
                // y el SP recibirá un hash de string vacío, que debe manejar correctamente

                // Procesar archivo usando el mismo método que en docente
                if (!ProcesarArchivo(fup_Copia_Dni, xApo.ArchivoBase, "COPIA DNI"))
                    return;

                // Usar los métodos originales igual que en docente
                xdb.ModUsuario(xusuario);
                xdb.ModApoderado(xApo);

                // Recargar datos
                CargarDatosApoderado();

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alert",
                    "alert('Sus datos han sido modificados correctamente.');",
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
            catch (ArgumentException aex)
            {
                string mensaje = aex.Message
                    .Replace("'", "\\'")
                    .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertArgumentError",
                    $"alert('Error de validación: {mensaje}');",
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
            else
            {
                // Si no hay archivo nuevo, asegurar que ArchivoBase no se envíe con datos vacíos
                // Esto es crucial para que el SP no intente actualizar el archivo con un valor vacío/nulo
                // y mantenga el existente.
                archivoBase.NombreArchivo = null;
                archivoBase.TamanioBytes = 0;
                archivoBase.Archivo = null;
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
