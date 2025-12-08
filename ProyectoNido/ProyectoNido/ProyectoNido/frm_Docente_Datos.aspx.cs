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
    public partial class frm_Docente_Datos : System.Web.UI.Page
    {
        // Propiedad para mantener el estado Activo del usuario
        bool Activo
        {
            get => (bool?)ViewState["Activo"] ?? false;
            set => ViewState["Activo"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si hay sesión de usuario
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("frm_Login.aspx");
                    return;
                }

                CargarCombos();
                CargarDatosDocente();
            }
        }

        private void CargarCombos()
        {
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Cargar Tipo Documento
            List<clsTipoDocumento> listatd = xdb.GetTipoDocumento().ToList();
            Ddl_Tipo_Documento.DataSource = listatd;
            Ddl_Tipo_Documento.DataTextField = "Nombre";
            Ddl_Tipo_Documento.DataValueField = "Id";
            Ddl_Tipo_Documento.DataBind();
            Ddl_Tipo_Documento.Items.Insert(0, new ListItem("-- Seleccione un Documento --", ""));

            // Cargar Distritos
            List<clsDistrito> lista = xdb.GetDistrito().ToList();
            Ddl_Distrito.DataSource = lista;
            Ddl_Distrito.DataTextField = "Nombre";
            Ddl_Distrito.DataValueField = "Id";
            Ddl_Distrito.DataBind();
            Ddl_Distrito.Items.Insert(0, new ListItem("-- Seleccione un Distrito --", ""));

            // Cargar Sexo
            Ddl_Sexo.Items.Clear();
            Ddl_Sexo.Items.Add(new ListItem("-- Seleccione un Sexo --", ""));
            Ddl_Sexo.Items.Add(new ListItem("Masculino", "M"));
            Ddl_Sexo.Items.Add(new ListItem("Femenino", "F"));
        }

        private void CargarDatosDocente()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                
                // Obtenemos la lista completa y filtramos (o usamos un método específico si existiera)
                var lista = xdb.GetProfesor(); 
                var Profe = lista.FirstOrDefault(u => u.Id == idUsuario);

                if (Profe != null)
                {
                    // Llenar campos
                    txt_IdProfesor.Text = Profe.Id.ToString();
                    txt_Usuario.Text = Profe.NombreUsuario; // Usuario suele ser read-only
                    
                    txt_Nombres.Text = Profe.Nombres;
                    txt_ApellidoPaterno.Text = Profe.ApellidoPaterno;
                    txt_ApellidoMaterno.Text = Profe.ApellidoMaterno;
                    
                    if (Profe.TipoDocumento != null)
                        Ddl_Tipo_Documento.SelectedValue = Profe.TipoDocumento.Id.ToString();
                    
                    txt_Documento.Text = Profe.Documento;
                    
                    txt_Fecha_Nacimiento.Text = Profe.FechaNacimiento.HasValue ? Profe.FechaNacimiento.Value.ToString("yyyy-MM-dd") : string.Empty;
                    
                    Ddl_Sexo.SelectedValue = Profe.Sexo;
                    
                    if (Profe.Distrito != null && Profe.Distrito.Id.ToString() != "0")
                    {
                        Ddl_Distrito.SelectedValue = Profe.Distrito.Id.ToString();
                    }
                    else
                    {
                        Ddl_Distrito.SelectedIndex = 0;
                    }

                    txt_Direccion.Text = Profe.Direccion;
                    txt_Telefono.Text = Profe.Telefono;
                    txt_Email.Text = Profe.Email;
                    
                    txt_Fecha_Ingreso.Text = Profe.FechaIngreso.HasValue ? Profe.FechaIngreso.Value.ToString("yyyy-MM-dd") : string.Empty;
                    // Fecha ingreso normalmente no editable por el propio docente, pero se muestra

                    // Guardar estado activo
                    Activo = Profe.Activo;
                    
                    // Actualizar Label del nombre en la barra lateral si existe
                    if (lblNombreDocente != null)
                        lblNombreDocente.Text = $"{Profe.Nombres} {Profe.ApellidoPaterno}";

                    // Controlar visibilidad de botones de descarga vs mensaje de carga
                    // Verificar directamente si cada documento existe en la BD
                    ActualizarEstadoDocumentos(idUsuario);
                }
            }
            catch (Exception ex)
            {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alertErrorCarga", $"alert('Error al cargar datos: {ex.Message}');", true);
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsProfesor xProfe = new clsProfesor();
                
                // Inicializar objetos de archivos
                xProfe.TituloProfesional = new clsArchivoBase();
                xProfe.Cv = new clsArchivoBase();
                xProfe.EvaluacionPsicologica = new clsArchivoBase();
                xProfe.Fotos = new clsArchivoBase();
                xProfe.VerificacionDomiciliaria = new clsArchivoBase();

                clsUsuario xusuario = new clsUsuario();
                xusuario.Distrito = new clsDistrito();
                xusuario.TipoDocumento = new clsTipoDocumento();

                // IDs
                xProfe.Id = Convert.ToInt32(this.txt_IdProfesor.Text.Trim());
                xusuario.Id = Convert.ToInt32(this.txt_IdProfesor.Text.Trim());
                
                // Datos
                xusuario.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                xusuario.NombreUsuario = txt_Usuario.Text.Trim();
                xusuario.Nombres = txt_Nombres.Text.Trim();
                xusuario.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xusuario.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xusuario.Documento = txt_Documento.Text.Trim();
                
                xusuario.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                
                xusuario.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue) ? null : Ddl_Sexo.SelectedValue;
                
                if (string.IsNullOrEmpty(Ddl_Distrito.SelectedValue))
                    xusuario.Distrito = null; 
                else
                    xusuario.Distrito.Id = Convert.ToInt32(Ddl_Distrito.SelectedValue);

                xusuario.Direccion = txt_Direccion.Text.Trim();
                xusuario.Telefono = txt_Telefono.Text.Trim();
                xusuario.Email = txt_Email.Text.Trim();
                xusuario.Activo = Activo; // Mantener el estado activo original
                
                // Fecha Ingreso (se mantiene la del formulario o null)
                xProfe.FechaIngreso = DateTime.TryParse(txt_Fecha_Ingreso.Text.Trim(), out DateTime fi) ? fi : (DateTime?)null;

                // VALIDACIÓN Y CARGA DE ARCHIVOS
                if (!ProcesarArchivo(fup_Titulo_Profesional, xProfe.TituloProfesional, "TITULO PROFESIONAL")) return;
                if (!ProcesarArchivo(fup_Cv, xProfe.Cv, "CURRICULUM VITAE")) return;
                if (!ProcesarArchivo(fup_Evaluacion_Psicologica, xProfe.EvaluacionPsicologica, "EVALUACION PSICOLOGICA")) return;
                if (!ProcesarArchivo(fup_Fotos, xProfe.Fotos, "FOTOS")) return;
                if (!ProcesarArchivo(fup_Verificacion_Domiciliaria, xProfe.VerificacionDomiciliaria, "VERIFICACION DOMICILIARIA")) return;

                // Guardar cambios
                xdb.ModUsuario(xusuario);
                xdb.ModProfesor(xProfe);

                // Recargar para ver cambios
                CargarDatosDocente();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sus datos han sido modificados correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message.Replace("'", "\\'").Replace(Environment.NewLine, " ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", $"alert('Error: {mensaje}');", true);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'").Replace(Environment.NewLine, " ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertErrorGeneral", $"alert('Error inesperado: {mensaje}');", true);
            }
        }

        private bool ProcesarArchivo(FileUpload fileUpload, clsArchivoBase archivoBase, string nombreMostrar)
        {
            if (fileUpload.HasFile)
            {
                int maxBytes = 5 * 1024 * 1024; // 5 MB
                if (fileUpload.PostedFile.ContentLength > maxBytes)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSize", $"alert('El archivo ({nombreMostrar}) supera el límite máximo permitido de 5 MB.');", true);
                    return false;
                }

                archivoBase.NombreArchivo = fileUpload.FileName;
                archivoBase.TamanioBytes = fileUpload.FileBytes.Length;
                archivoBase.Archivo = fileUpload.FileBytes;
            }
            return true;
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            // En este formulario, "Limpiar" podría significar "Restablecer a valores guardados"
            CargarDatosDocente();
        }

        protected void Ddl_Tipo_Documento_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsValidacion.AplicarMaxLengthPorTipoDocumento(Ddl_Tipo_Documento, txt_Documento);
        }

        public void DescargarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string tipoArchivo = btn.CommandArgument;
            int id = Convert.ToInt32(Session["IdUsuario"]);

            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            try
            {
                // Enviar el archivo al navegador
                clsArchivoBase xAb = xdb.RetArchivoProfesor(id, tipoArchivo);

                if (xAb.TamanioBytes > 0)
                {
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; // Default, o detectar por extensión
                    
                    // Ajustar ContentType según la extensión si es posible, o dejar genérico
                    // Si el nombre tiene extensión conocida, mejor
                    string extension = System.IO.Path.GetExtension(xAb.NombreArchivo).ToLower();
                    if (extension == ".pdf") Response.ContentType = "application/pdf";
                    else if (extension == ".jpg" || extension == ".jpeg") Response.ContentType = "image/jpeg";
                    else if (extension == ".png") Response.ContentType = "image/png";

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", $"alert('Error al descargar: {mensaje}');", true);
            }
            catch (Exception ex)
            {
                // Response.End() lanza ThreadAbortException, es normal
                if (!(ex is System.Threading.ThreadAbortException))
                {
                    string mensaje = ex.Message.Replace("'", "\\'").Replace(Environment.NewLine, " ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertErrorDescarga", $"alert('Error inesperado: {mensaje}');", true);
                }
            }
        }

        /// <summary>
        /// Verifica si un documento existe en la BD consultando directamente el archivo
        /// </summary>
        private bool VerificarDocumentoExisteEnBD(int idProfesor, string tipoArchivo)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                clsArchivoBase archivo = xdb.RetArchivoProfesor(idProfesor, tipoArchivo);
                
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
        private void ActualizarEstadoDocumentos(int idProfesor)
        {
            // Título Profesional
            bool existeTitulo = VerificarDocumentoExisteEnBD(idProfesor, "TituloProfesional");
            lnk_Titulo_Profesional.Visible = existeTitulo;
            lnk_Titulo_Profesional.Text = "Descargar";
            lbl_Titulo_Profesional_Msg.Visible = !existeTitulo;
            lbl_Titulo_Profesional_Msg.Text = "Cargar documento";

            // Curriculum Vitae
            bool existeCv = VerificarDocumentoExisteEnBD(idProfesor, "Cv");
            lnk_Cv.Visible = existeCv;
            lnk_Cv.Text = "Descargar";
            lbl_Cv_Msg.Visible = !existeCv;
            lbl_Cv_Msg.Text = "Cargar documento";

            // Evaluación Psicológica
            bool existeEval = VerificarDocumentoExisteEnBD(idProfesor, "EvaluacionPsicologica");
            lnk_Evaluacion_Psicologica.Visible = existeEval;
            lnk_Evaluacion_Psicologica.Text = "Descargar";
            lbl_Evaluacion_Psicologica_Msg.Visible = !existeEval;
            lbl_Evaluacion_Psicologica_Msg.Text = "Cargar documento";

            // Fotos
            bool existeFotos = VerificarDocumentoExisteEnBD(idProfesor, "Fotos");
            lnk_Fotos.Visible = existeFotos;
            lnk_Fotos.Text = "Descargar";
            lbl_Fotos_Msg.Visible = !existeFotos;
            lbl_Fotos_Msg.Text = "Cargar documento";

            // Verificación Domiciliaria
            bool existeVerif = VerificarDocumentoExisteEnBD(idProfesor, "VerificacionDomiciliaria");
            lnk_Verificacion_Domiciliaria.Visible = existeVerif;
            lnk_Verificacion_Domiciliaria.Text = "Descargar";
            lbl_Verificacion_Domiciliaria_Msg.Visible = !existeVerif;
            lbl_Verificacion_Domiciliaria_Msg.Text = "Cargar documento";
        }
    }
}