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
                
                // TODO: Implementar redirección o modal para cronograma de pagos
                ScriptManager.RegisterStartupScript(this, this.GetType(), "cronograma", 
                    $"alert('Cronograma de Pagos para el alumno ID: {idAlumno} - Funcionalidad pendiente de implementar');", true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al abrir cronograma: {ex.Message}");
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
                
                // TODO: Implementar redirección o modal para historial de servicio
                ScriptManager.RegisterStartupScript(this, this.GetType(), "historial", 
                    $"alert('Historial de Servicio para el alumno ID: {idAlumno} - Funcionalidad pendiente de implementar');", true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al abrir historial: {ex.Message}");
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

                // Recargar datos
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
    }
}
