using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoNido.wcfNido;

namespace ProyectoNido
{
    public partial class frm_Apoderado_hijos : System.Web.UI.Page
    {
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
                
                // Verificar si hay un hijo seleccionado
                if (Session["IdAlumnoSeleccionado"] != null)
                {
                    int idAlumno = Convert.ToInt32(Session["IdAlumnoSeleccionado"]);
                    CargarDatosHijo(idAlumno);
                    
                    // Script para expandir las opciones después de cargar la página
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "expandirHijoInicial", 
                        "setTimeout(function() { verificarHijoSeleccionado(); }, 200);", true);
                }
                else
                {
                    // Mostrar mensaje de selección
                    pnlSinHijoSeleccionado.Visible = true;
                    pnlFormularioHijo.Visible = false;
                }
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
            }
        }

        /// <summary>
        /// Carga los dropdowns (TipoDocumento, Sexo)
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar dropdowns: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga los datos del hijo seleccionado en el formulario
        /// </summary>
        private void CargarDatosHijo(int idAlumno)
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                // Obtener todos los hijos del apoderado
                int idApoderado = Convert.ToInt32(Session["IdUsuario"]);
                var hijos = servicio.ListarAlumnosPorApoderado(idApoderado);
                
                // Buscar el hijo específico
                var hijo = hijos?.FirstOrDefault(h => h.Id == idAlumno);
                
                if (hijo != null)
                {
                    txt_IdAlumno.Text = hijo.Id.ToString();
                    txt_Nombres.Text = hijo.Nombres;
                    txt_ApellidoPaterno.Text = hijo.ApellidoPaterno;
                    txt_ApellidoMaterno.Text = hijo.ApellidoMaterno;
                    
                    if (hijo.TipoDocumento != null && hijo.TipoDocumento.Id > 0)
                    {
                        Ddl_Tipo_Documento.SelectedValue = hijo.TipoDocumento.Id.ToString();
                    }
                    
                    txt_Documento.Text = hijo.Documento;
                    
                    if (hijo.FechaNacimiento.HasValue)
                    {
                        txt_Fecha_Nacimiento.Text = hijo.FechaNacimiento.Value.ToString("yyyy-MM-dd");
                    }
                    
                    if (!string.IsNullOrEmpty(hijo.Sexo))
                    {
                        Ddl_Sexo.SelectedValue = hijo.Sexo;
                    }
                    
                    chb_Activo.Checked = hijo.Activo;
                    
                    // Actualizar estado de documentos (mostrar si existen o mensaje de carga)
                    ActualizarEstadoDocumentos(idAlumno);
                    
                    // Mostrar formulario y ocultar mensaje
                    pnlSinHijoSeleccionado.Visible = false;
                    pnlFormularioHijo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar datos del hijo: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorDatos", 
                    $"alert('Error al cargar datos: {ex.Message.Replace("'", "\\'")}');", true);
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
                
                // Recargar los hijos para actualizar el estado activo
                CargarHijos();
                
                // Cargar datos del hijo
                CargarDatosHijo(idAlumno);
                
                // Script para expandir las opciones después del postback
                ScriptManager.RegisterStartupScript(this, this.GetType(), "expandirHijo", 
                    "setTimeout(function() { verificarHijoSeleccionado(); }, 100);", true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al seleccionar hijo: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorSeleccion", 
                    $"alert('Error al seleccionar hijo: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Maneja el click en "Cronograma de Pagos"
        /// </summary>
        protected void lnkCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                int idAlumno = Convert.ToInt32(lnk.CommandArgument);
                
                // Guardar el ID del alumno seleccionado en sesión
                Session["IdAlumnoSeleccionado"] = idAlumno;
                
                // Redirigir a la página de cronograma de pagos
                Response.Redirect($"frm_CronogramaPago_PorHijo.aspx?idAlumno={idAlumno}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al abrir cronograma: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorCronograma",
                    $"alert('Error al abrir cronograma: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Maneja el click en "Ver Historial de Servicio"
        /// </summary>
        protected void lnkHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                int idAlumno = Convert.ToInt32(lnk.CommandArgument);
                
                // Guardar el ID del alumno seleccionado en sesión
                Session["IdAlumnoSeleccionado"] = idAlumno;
                
                // Redirigir a la página de historial de servicios
                Response.Redirect($"frm_HistorialServicio_PorHijo.aspx?idAlumno={idAlumno}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al abrir historial: {ex.Message}");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorHistorial",
                    $"alert('Error al abrir historial: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Maneja la modificación de datos del hijo
        /// </summary>
        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["IdAlumnoSeleccionado"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "sinSeleccion", 
                        "alert('Debe seleccionar un hijo primero.');", true);
                    return;
                }

                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                
                clsAlumno alumno = new clsAlumno();
                alumno.Fotos = new clsArchivoBase();
                alumno.CopiaDni = new clsArchivoBase();
                alumno.PermisoPublicidad = new clsArchivoBase();
                alumno.CarnetSeguro = new clsArchivoBase();
                alumno.Apoderado = new clsApoderado();
                alumno.TipoDocumento = new clsTipoDocumento();

                // Obtener el apoderado actual (el que está logueado)
                int idApoderado = Convert.ToInt32(Session["IdUsuario"]);
                
                alumno.Id = Convert.ToInt32(txt_IdAlumno.Text.Trim());
                alumno.Apoderado.Id = idApoderado; // Siempre el apoderado logueado
                alumno.TipoDocumento.Id = int.TryParse(Ddl_Tipo_Documento.SelectedValue, out int idTipoDoc) && idTipoDoc > 0 ? idTipoDoc : 0;
                alumno.Nombres = txt_Nombres.Text.Trim();
                alumno.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                alumno.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                alumno.Documento = txt_Documento.Text.Trim();
                alumno.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text, out DateTime fechaNac) ? fechaNac : (DateTime?)null;
                alumno.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue) ? null : Ddl_Sexo.SelectedValue;
                alumno.Activo = chb_Activo.Checked;

                // Procesar archivos
                if (!ProcesarArchivo(fup_Fotos, alumno.Fotos, "FOTOS"))
                    return;
                if (!ProcesarArchivo(fup_Copia_Dni, alumno.CopiaDni, "COPIA DNI"))
                    return;
                if (!ProcesarArchivo(fup_Permiso_Publicidad, alumno.PermisoPublicidad, "PERMISO PUBLICIDAD"))
                    return;
                if (!ProcesarArchivo(fup_Carnet_Seguro, alumno.CarnetSeguro, "CARNET SEGURO"))
                    return;

                // Modificar alumno
                servicio.ModAlumno(alumno);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alert",
                    "alert('Datos del estudiante modificados correctamente.');",
                    true
                );

                // Recargar datos (incluye actualizar estado de documentos)
                CargarDatosHijo(alumno.Id);
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
        /// Procesa un archivo subido: valida tamaño y carga en el objeto ArchivoBase
        /// </summary>
        private bool ProcesarArchivo(FileUpload fileUpload, clsArchivoBase archivoBase, string nombreMostrar)
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
        /// Maneja el cambio de tipo de documento (para validaciones futuras)
        /// </summary>
        protected void Ddl_Tipo_Documento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí se pueden agregar validaciones según el tipo de documento seleccionado
        }

        /// <summary>
        /// Verifica si un documento existe en la BD consultando directamente el archivo
        /// </summary>
        private bool VerificarDocumentoExisteEnBD(int idAlumno, string tipoArchivo)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsArchivoBase archivo = xdb.RetArchivoAlumno(idAlumno, tipoArchivo);
                
                // El documento existe si tiene tamaño mayor a 0
                return archivo != null && archivo.TamanioBytes > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Actualiza la visibilidad de los controles de documentos según si existen en la BD
        /// Muestra "Descargar" si existe, "Cargar documento" si no existe
        /// </summary>
        private void ActualizarEstadoDocumentos(int idAlumno)
        {
            // Fotos
            bool existeFotos = VerificarDocumentoExisteEnBD(idAlumno, "Fotos");
            lnk_Fotos.Visible = existeFotos;
            lnk_Fotos.Text = "Descargar";
            lbl_Fotos_Msg.Visible = !existeFotos;
            lbl_Fotos_Msg.Text = "Cargar documento";

            // Copia DNI
            bool existeCopiaDni = VerificarDocumentoExisteEnBD(idAlumno, "CopiaDni");
            lnk_Copia_Dni.Visible = existeCopiaDni;
            lnk_Copia_Dni.Text = "Descargar";
            lbl_Copia_Dni_Msg.Visible = !existeCopiaDni;
            lbl_Copia_Dni_Msg.Text = "Cargar documento";

            // Permiso Publicidad
            bool existePermiso = VerificarDocumentoExisteEnBD(idAlumno, "Permiso");
            lnk_Permiso_Publicidad.Visible = existePermiso;
            lnk_Permiso_Publicidad.Text = "Descargar";
            lbl_Permiso_Publicidad_Msg.Visible = !existePermiso;
            lbl_Permiso_Publicidad_Msg.Text = "Cargar documento";

            // Carnet Seguro
            bool existeCarnet = VerificarDocumentoExisteEnBD(idAlumno, "Carnet");
            lnk_Carnet_Seguro.Visible = existeCarnet;
            lnk_Carnet_Seguro.Text = "Descargar";
            lbl_Carnet_Seguro_Msg.Visible = !existeCarnet;
            lbl_Carnet_Seguro_Msg.Text = "Cargar documento";
        }

        /// <summary>
        /// Maneja la descarga de archivos del alumno
        /// </summary>
        public void DescargarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string tipoArchivo = btn.CommandArgument;
            
            if (Session["IdAlumnoSeleccionado"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "sinSeleccion", 
                    "alert('Debe seleccionar un hijo primero.');", true);
                return;
            }

            int idAlumno = Convert.ToInt32(Session["IdAlumnoSeleccionado"]);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            try
            {
                // Enviar el archivo al navegador
                clsArchivoBase xAb = xdb.RetArchivoAlumno(idAlumno, tipoArchivo);

                if (xAb != null && xAb.TamanioBytes > 0)
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream"; // Default
                    
                    // Ajustar ContentType según la extensión
                    string extension = System.IO.Path.GetExtension(xAb.NombreArchivo).ToLower();
                    if (extension == ".pdf") Response.ContentType = "application/pdf";
                    else if (extension == ".jpg" || extension == ".jpeg") Response.ContentType = "image/jpeg";
                    else if (extension == ".png") Response.ContentType = "image/png";
                    else if (extension == ".doc" || extension == ".docx") Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                    string headerFileName = HttpUtility.UrlPathEncode(xAb.NombreArchivo);
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + headerFileName);
                    Response.AddHeader("Content-Length", xAb.Archivo.Length.ToString());
                    Response.BinaryWrite(xAb.Archivo);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El archivo no se encontró o está vacío.');", true);
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
    }
}
