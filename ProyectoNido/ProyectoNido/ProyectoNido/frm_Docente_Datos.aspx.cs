using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.ServiceModel;
using System.Configuration;

namespace ProyectoNido
{
    public partial class frm_Docente_Datos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    // Verificar que el usuario esté logueado
            //    if (Session["IdUsuario"] == null)
            //    {
            //        Response.Redirect("frm_Login.aspx");
            //        return;
            //    }

            //    // Cargar datos del docente
            //    CargarDatosDocente();
            //}
        }

        private void CargarDatosDocente()
        {
            //try
            //{
            //    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                
            //    wcfNido.Service1Client servicio = new wcfNido.Service1Client();
            //    wcfNido.clsUsuario docente = servicio.ObtenerDatosDocente(idUsuario);

            //    if (docente != null)
            //    {
            //        // Cargar datos personales
            //        txtNombres.Text = docente.Nombres ?? string.Empty;
            //        txtApellidoPaterno.Text = docente.ApellidoPaterno ?? string.Empty;
            //        txtApellidoMaterno.Text = docente.ApellidoMaterno ?? string.Empty;
            //        txtDNI.Text = docente.Dni ?? string.Empty;
            //        txtDireccion.Text = docente.Direccion ?? string.Empty;
            //        txtEmail.Text = docente.Email ?? string.Empty;

            //        // Cargar fecha de nacimiento
            //        if (docente.FechaNacimiento.HasValue)
            //        {
            //            txtFechaNacimiento.Text = docente.FechaNacimiento.Value.ToString("yyyy-MM-dd");
            //        }

            //        // Cargar sexo
            //        if (!string.IsNullOrEmpty(docente.Sexo))
            //        {
            //            ddlSexo.SelectedValue = docente.Sexo;
            //        }

            //        // Cargar fecha de ingreso
            //        if (docente.FechaIngreso.HasValue)
            //        {
            //            txt_FechaIngreso.Text = docente.FechaIngreso.Value.ToString("yyyy-MM-dd");
            //        }

            //        // Verificación domiciliaria ahora es un archivo, se maneja en MostrarArchivoGuardado

            //        // Mostrar archivos guardados
            //        MostrarArchivoGuardado(docente.TituloProfesional, lnkTituloProfesional, lblTituloProfesional, "Título Profesional");
            //        MostrarArchivoGuardado(docente.Cv, lnkCV, lblCV, "CV");
            //        MostrarArchivoGuardado(docente.EvaluacionPsicologica, lnkEvaluacionPsicologica, lblEvaluacionPsicologica, "Evaluación Psicológica");
            //        MostrarArchivoGuardado(docente.Fotos, lnkFoto, lblFoto, "Foto");
            //        MostrarArchivoGuardado(docente.VerificacionDomiciliaria, lnkVerificacionDomiciliaria, lblVerificacionDomiciliaria, "Verificación Domiciliaria");

            //        // Actualizar nombre del docente en el panel izquierdo
            //        lblNombreDocente.Text = $"{docente.Nombres} {docente.ApellidoPaterno} {docente.ApellidoMaterno}";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Si hay error al cargar, mostrar mensaje pero no bloquear el formulario
            //    string script = $"console.error('Error al cargar datos del docente: {ex.Message}');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            //}
        }

        protected void btnGuardarDocente_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // Verificar que el usuario esté logueado
            //    if (Session["IdUsuario"] == null)
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe iniciar sesión para continuar.');", true);
            //        Response.Redirect("frm_Login.aspx");
            //        return;
            //    }

            //    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);

            //    // Obtener datos actuales primero (para DNI y Fecha de ingreso que no se pueden modificar)
            //    wcfNido.Service1Client servicio = new wcfNido.Service1Client();
            //    wcfNido.clsUsuario datosActuales = servicio.ObtenerDatosDocente(idUsuario);

            //    if (datosActuales == null)
            //    {
            //        string script = "alert('No se pudieron obtener los datos del docente.');";
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            //        return;
            //    }

            //    // Obtener valores del formulario
            //    string nombres = txtNombres.Text.Trim();
            //    string apPaterno = txtApellidoPaterno.Text.Trim();
            //    string apMaterno = txtApellidoMaterno.Text.Trim();
            //    // DNI no se puede modificar, usar el valor de la base de datos
            //    string dni = datosActuales.Dni ?? string.Empty;
            //    string direccion = txtDireccion.Text.Trim();
            //    string email = txtEmail.Text.Trim();
            //    string sexo = ddlSexo.SelectedValue;

            //    // Validar que el sexo sea válido (M o F)
            //    if (string.IsNullOrEmpty(sexo) || (sexo != "M" && sexo != "F"))
            //    {
            //        string script = "alert('Por favor seleccione un sexo válido (Masculino o Femenino).');";
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            //        return;
            //    }

            //    // Convertir fechas
            //    DateTime? fechaNacimiento = null;
            //    if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
            //    {
            //        if (DateTime.TryParse(txtFechaNacimiento.Text, out DateTime fechaNac))
            //        {
            //            fechaNacimiento = fechaNac;
            //        }
            //    }

            //    // Fecha de ingreso no se puede modificar, usar el valor de la base de datos
            //    DateTime? fechaIngreso = datosActuales.FechaIngreso;

            //    // Manejar archivos subidos
            //    // Si hay archivo nuevo, guardarlo y eliminar el anterior si existe
            //    // Si no hay archivo nuevo, pasar null para que el procedimiento almacenado mantenga el valor existente
            //    string tituloProfesional = null;
            //    if (fuTituloProfesional.HasFile && fuTituloProfesional.PostedFile != null && fuTituloProfesional.PostedFile.ContentLength > 0)
            //    {
            //        tituloProfesional = GuardarArchivo(fuTituloProfesional, "TituloProfesional", idUsuario, datosActuales?.TituloProfesional);
            //    }
            //    // Si no hay archivo nuevo, dejar null para que el procedimiento almacenado mantenga el valor existente

            //    string cv = null;
            //    if (fuCV.HasFile && fuCV.PostedFile != null && fuCV.PostedFile.ContentLength > 0)
            //    {
            //        cv = GuardarArchivo(fuCV, "CV", idUsuario, datosActuales?.Cv);
            //    }
            //    // Si no hay archivo nuevo, dejar null para que el procedimiento almacenado mantenga el valor existente

            //    string evaluacionPsicologica = null;
            //    if (fuEvaluacionPsicologica.HasFile && fuEvaluacionPsicologica.PostedFile != null && fuEvaluacionPsicologica.PostedFile.ContentLength > 0)
            //    {
            //        evaluacionPsicologica = GuardarArchivo(fuEvaluacionPsicologica, "EvaluacionPsicologica", idUsuario, datosActuales?.EvaluacionPsicologica);
            //    }
            //    // Si no hay archivo nuevo, dejar null para que el procedimiento almacenado mantenga el valor existente

            //    string fotos = null;
            //    if (fuFoto.HasFile && fuFoto.PostedFile != null && fuFoto.PostedFile.ContentLength > 0)
            //    {
            //        fotos = GuardarArchivo(fuFoto, "Foto", idUsuario, datosActuales?.Fotos);
            //    }
            //    // Si no hay archivo nuevo, dejar null para que el procedimiento almacenado mantenga el valor existente

            //    string verificacionDomiciliaria = null;
            //    if (fuVerificacionDomiciliaria.HasFile && fuVerificacionDomiciliaria.PostedFile != null && fuVerificacionDomiciliaria.PostedFile.ContentLength > 0)
            //    {
            //        verificacionDomiciliaria = GuardarArchivo(fuVerificacionDomiciliaria, "VerificacionDomiciliaria", idUsuario, datosActuales?.VerificacionDomiciliaria);
            //    }
            //    // Si no hay archivo nuevo, dejar null para que el procedimiento almacenado mantenga el valor existente

            //    // Llamar al servicio WCF
            //    servicio.ActualizarDatosDocente(
            //        idUsuario,
            //        nombres,
            //        apPaterno,
            //        apMaterno,
            //        dni,
            //        fechaNacimiento,
            //        sexo,
            //        direccion,
            //        email,
            //        fechaIngreso,
            //        tituloProfesional,
            //        cv,
            //        evaluacionPsicologica,
            //        fotos,
            //        verificacionDomiciliaria
            //    );

            //    // Mostrar mensaje de éxito
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Datos del docente actualizados correctamente.');", true);

            //    // Actualizar el nombre del docente en el panel izquierdo
            //    lblNombreDocente.Text = $"{nombres} {apPaterno} {apMaterno}";

            //    // Recargar los archivos guardados para mostrar los nuevos
            //    RecargarArchivosGuardados();
            //}
            //catch (FaultException fex)
            //{
            //    // Capturar errores específicos del servicio WCF
            //    string mensajeError = "Error al guardar los datos del docente.";
                
            //    if (fex.Message.Contains("CHECK constraint") && fex.Message.Contains("Sexo"))
            //    {
            //        mensajeError = "Error: El valor del campo Sexo no es válido. Por favor seleccione Masculino o Femenino.";
            //    }
            //    else if (fex.Message.Contains("CHECK constraint"))
            //    {
            //        mensajeError = "Error: Uno de los valores ingresados no cumple con las restricciones de la base de datos. Verifique los datos e intente nuevamente.";
            //    }
            //    else if (fex.Message.Contains("FOREIGN KEY"))
            //    {
            //        mensajeError = "Error: Los datos ingresados hacen referencia a información que no existe en el sistema.";
            //    }
            //    else if (fex.Message.Contains("UNIQUE"))
            //    {
            //        mensajeError = "Error: El DNI ingresado ya existe en el sistema. Por favor verifique el número de documento.";
            //    }
            //    else
            //    {
            //        mensajeError = $"Error al guardar los datos: {fex.Message}";
            //    }
                
            //    string scriptError = $"alert('{mensajeError.Replace("'", "\\'")}');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptError, true);
            //}
            //catch (Exception ex)
            //{
            //    string mensajeError = "Error inesperado al guardar los datos del docente.";
                
            //    if (ex.Message.Contains("CHECK constraint") && ex.Message.Contains("Sexo"))
            //    {
            //        mensajeError = "Error: El valor del campo Sexo no es válido. Por favor seleccione Masculino o Femenino.";
            //    }
            //    else if (ex.InnerException != null && ex.InnerException.Message.Contains("CHECK constraint"))
            //    {
            //        mensajeError = "Error: Uno de los valores ingresados no cumple con las restricciones de la base de datos. Verifique los datos e intente nuevamente.";
            //    }
            //    else
            //    {
            //        mensajeError = $"Error: {ex.Message}";
            //    }
                
            //    string scriptError = $"alert('{mensajeError.Replace("'", "\\'")}');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptError, true);
            //}
        }

        //private string GuardarArchivo(FileUpload fileUpload, string tipoArchivo, int idUsuario, string archivoAnterior = null)
        //{
            // Si no hay archivo seleccionado, retornar null para mantener el valor existente
            //if (!fileUpload.HasFile)
            //{
            //    return null; // El procedimiento almacenado no actualizará este campo si es null
            //}

            //// Validar que el archivo no esté vacío
            //if (fileUpload.PostedFile == null || fileUpload.PostedFile.ContentLength == 0)
            //{
            //    return null; // Archivo vacío, mantener el existente
            //}

            //try
            //{
            //    // Validar tamaño del archivo
            //    int tamañoMaximoMB = 10; // Por defecto 10MB
            //    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TamañoMaximoArchivoMB"]))
            //    {
            //        tamañoMaximoMB = Convert.ToInt32(ConfigurationManager.AppSettings["TamañoMaximoArchivoMB"]);
            //    }

            //    long tamañoMaximoBytes = tamañoMaximoMB * 1024 * 1024; // Convertir a bytes
            //    if (fileUpload.PostedFile.ContentLength > tamañoMaximoBytes)
            //    {
            //        throw new Exception($"El archivo excede el tamaño máximo permitido de {tamañoMaximoMB} MB.");
            //    }

            //    // Validar extensión del archivo
            //    string extension = Path.GetExtension(fileUpload.FileName).ToLower();
            //    string extensionesPermitidas = ConfigurationManager.AppSettings["ExtensionesPermitidas"] ?? ".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif";
            //    string[] extensionesArray = extensionesPermitidas.Split(',');

            //    if (!extensionesArray.Contains(extension))
            //    {
            //        throw new Exception($"Tipo de archivo no permitido. Extensiones permitidas: {extensionesPermitidas}");
            //    }

            //    // Obtener ruta de documentos desde configuración
            //    string rutaRelativa = ConfigurationManager.AppSettings["RutaDocumentos"] ?? "~/Documentos";
            //    string carpetaDocumentos = Server.MapPath(rutaRelativa);

            //    // Crear carpeta base si no existe
            //    if (!Directory.Exists(carpetaDocumentos))
            //    {
            //        Directory.CreateDirectory(carpetaDocumentos);
            //    }

            //    // Crear carpeta por tipo de documento
            //    string carpetaTipo = Path.Combine(carpetaDocumentos, tipoArchivo);
            //    if (!Directory.Exists(carpetaTipo))
            //    {
            //        Directory.CreateDirectory(carpetaTipo);
            //    }

            //    // Crear carpeta por usuario para evitar colisiones manteniendo el nombre original
            //    string carpetaUsuario = Path.Combine(carpetaTipo, $"Usuario_{idUsuario}");
            //    if (!Directory.Exists(carpetaUsuario))
            //    {
            //        Directory.CreateDirectory(carpetaUsuario);
            //    }

            //    // Eliminar archivo anterior si existe
            //    if (!string.IsNullOrEmpty(archivoAnterior))
            //    {
            //        string rutaArchivoAnterior = Path.Combine(carpetaDocumentos, archivoAnterior.Replace("/", "\\"));
            //        if (File.Exists(rutaArchivoAnterior))
            //        {
            //            try
            //            {
            //                File.Delete(rutaArchivoAnterior);
            //            }
            //            catch
            //            {
            //                // Si no se puede eliminar, continuar de todas formas (puede estar en uso)
            //            }
            //        }
            //    }

            //    // Mantener el nombre original del archivo
            //    string nombreArchivo = Path.GetFileName(fileUpload.FileName);
            //    if (string.IsNullOrEmpty(nombreArchivo))
            //    {
            //        nombreArchivo = $"{tipoArchivo}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
            //    }
                
            //    // Ruta completa del archivo
            //    string rutaCompleta = Path.Combine(carpetaUsuario, nombreArchivo);
                
            //    // Si existe un archivo previo con el mismo nombre, eliminarlo para reemplazarlo
            //    if (File.Exists(rutaCompleta))
            //    {
            //        File.Delete(rutaCompleta);
            //    }
                
            //    // Guardar archivo
            //    fileUpload.SaveAs(rutaCompleta);
                
            //    // Retornar la ruta relativa dentro de Documentos (ej. "CV/Usuario_1/miArchivo.pdf")
            //    string rutaRelativaArchivo = Path.Combine(tipoArchivo, $"Usuario_{idUsuario}", nombreArchivo).Replace("\\", "/");
            //    return rutaRelativaArchivo;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"Error al guardar el archivo {tipoArchivo}: {ex.Message}");
            //}
        //}

        //private void MostrarArchivoGuardado(string nombreArchivo, HyperLink lnkArchivo, Label lblArchivo, string tipoArchivo)
        //{
        //    // Siempre ocultar ambos inicialmente
        //    lnkArchivo.Visible = false;
        //    lblArchivo.Visible = false;

        //    if (!string.IsNullOrEmpty(nombreArchivo))
        //    {
        //        // Verificar que el archivo existe físicamente
        //        string rutaRelativa = ConfigurationManager.AppSettings["RutaDocumentos"] ?? "~/Documentos";
        //        string segmentoRelativo = nombreArchivo.Replace("\\", "/");
        //        string rutaBase = rutaRelativa.EndsWith("/") ? rutaRelativa : rutaRelativa + "/";
        //        string rutaVirtual = VirtualPathUtility.Combine(rutaBase, segmentoRelativo);
        //        string rutaCompleta = Server.MapPath(rutaVirtual);

        //        if (File.Exists(rutaCompleta))
        //        {
        //            // Mostrar enlace para descargar
        //            lnkArchivo.Visible = true;
        //            string nombreParaMostrar = Path.GetFileName(segmentoRelativo);
        //            lnkArchivo.Text = $"📄 {nombreParaMostrar}";
        //            lnkArchivo.NavigateUrl = ResolveUrl(rutaVirtual);
        //            lnkArchivo.ToolTip = $"Hacer clic para descargar {tipoArchivo}";
        //        }
        //        else
        //        {
        //            // Archivo en BD pero no existe físicamente
        //            lblArchivo.Visible = true;
        //            lblArchivo.Text = $"⚠ Archivo registrado pero no encontrado: {Path.GetFileName(segmentoRelativo)}";
        //        }
        //    }
        //    else
        //    {
        //        // No hay archivo guardado - mostrar mensaje
        //        lblArchivo.Visible = true;
        //        lblArchivo.Text = "Sin archivo guardado";
        //    }
        //}

        //private void RecargarArchivosGuardados()
        //{
        //    try
        //    {
        //        int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
        //        wcfNido.Service1Client servicio = new wcfNido.Service1Client();
        //        wcfNido.clsUsuario docente = servicio.ObtenerDatosDocente(idUsuario);

        //        if (docente != null)
        //        {
        //            MostrarArchivoGuardado(docente.TituloProfesional, lnkTituloProfesional, lblTituloProfesional, "Título Profesional");
        //            MostrarArchivoGuardado(docente.Cv, lnkCV, lblCV, "CV");
        //            MostrarArchivoGuardado(docente.EvaluacionPsicologica, lnkEvaluacionPsicologica, lblEvaluacionPsicologica, "Evaluación Psicológica");
        //            MostrarArchivoGuardado(docente.Fotos, lnkFoto, lblFoto, "Foto");
        //            MostrarArchivoGuardado(docente.VerificacionDomiciliaria, lnkVerificacionDomiciliaria, lblVerificacionDomiciliaria, "Verificación Domiciliaria");
        //        }
        //    }
        //    catch
        //    {
        //        // Silenciar errores al recargar
        //    }
        //}
    }
}