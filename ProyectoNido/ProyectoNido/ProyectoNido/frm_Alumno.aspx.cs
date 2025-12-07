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
    public partial class frm_Alumno : System.Web.UI.Page
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

                List<clsApoderado> listaapo = xdb.GetApoderado().ToList();

                Ddl_Apoderado.DataSource = listaapo;
                Ddl_Apoderado.DataTextField = "NombreCompleto";
                Ddl_Apoderado.DataValueField = "Id";
                Ddl_Apoderado.DataBind();
                Ddl_Apoderado.Items.Insert(0, new ListItem("-- Seleccione un Apoderado --", ""));

                List<clsTipoDocumento> listatd = xdb.GetTipoDocumento().ToList();

                Ddl_Tipo_Documento.DataSource = listatd;
                Ddl_Tipo_Documento.DataTextField = "Nombre";
                Ddl_Tipo_Documento.DataValueField = "Id";
                Ddl_Tipo_Documento.DataBind();
                Ddl_Tipo_Documento.Items.Insert(0, new ListItem("-- Seleccione un Documento --", ""));

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

                clsAlumno xAlu = new clsAlumno();
                xAlu.Fotos = new clsArchivoBase();
                xAlu.CopiaDni = new clsArchivoBase();
                xAlu.PermisoPublicidad = new clsArchivoBase();
                xAlu.CarnetSeguro = new clsArchivoBase();

                xAlu.Apoderado = new clsApoderado();
                xAlu.TipoDocumento = new clsTipoDocumento();

                xAlu.Apoderado.Id = Convert.ToInt32(Ddl_Apoderado.SelectedValue);
                xAlu.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                xAlu.Nombres = txt_Nombres.Text.Trim();
                xAlu.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xAlu.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xAlu.Documento = txt_Documento.Text.Trim();
                xAlu.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                xAlu.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue)
                    ? null
                    : Ddl_Sexo.SelectedValue;

                xAlu.Activo = this.chb_Activo.Checked;

                if (fup_Fotos.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Fotos.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xAlu.Fotos.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.Fotos.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.Fotos.Archivo = fup_Copia_Dni.FileBytes;
                }

                if (fup_Copia_Dni.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Copia_Dni.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xAlu.CopiaDni.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.CopiaDni.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.CopiaDni.Archivo = fup_Copia_Dni.FileBytes;
                }

                if (fup_Permiso_Publicidad.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Permiso_Publicidad.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xAlu.PermisoPublicidad.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.PermisoPublicidad.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.PermisoPublicidad.Archivo = fup_Copia_Dni.FileBytes;
                }

                if (fup_Carnet_Seguro.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Carnet_Seguro.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xAlu.CarnetSeguro.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.CarnetSeguro.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.CarnetSeguro.Archivo = fup_Copia_Dni.FileBytes;
                }

                xdb.InsAlumno(xAlu);

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
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsAlumno xAlu = new clsAlumno();
                xAlu.Fotos = new clsArchivoBase();
                xAlu.CopiaDni = new clsArchivoBase();
                xAlu.PermisoPublicidad = new clsArchivoBase();
                xAlu.CarnetSeguro = new clsArchivoBase();

                xAlu.Apoderado = new clsApoderado();
                xAlu.TipoDocumento = new clsTipoDocumento();

                xAlu.Id = Convert.ToInt32(this.txt_IdAlumno.Text.Trim());
                xAlu.Apoderado.Id = Convert.ToInt32(Ddl_Apoderado.SelectedValue);
                xAlu.TipoDocumento.Id = Convert.ToInt32(Ddl_Tipo_Documento.SelectedValue);
                xAlu.Nombres = txt_Nombres.Text.Trim();
                xAlu.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xAlu.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xAlu.Documento = txt_Documento.Text.Trim();
                xAlu.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                xAlu.Sexo = string.IsNullOrEmpty(Ddl_Sexo.SelectedValue)
                    ? null
                    : Ddl_Sexo.SelectedValue;

                xAlu.Activo = this.chb_Activo.Checked;

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

                    xAlu.Fotos.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.Fotos.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.Fotos.Archivo = fup_Copia_Dni.FileBytes;
                }

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

                    xAlu.CopiaDni.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.CopiaDni.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.CopiaDni.Archivo = fup_Copia_Dni.FileBytes;
                }

                if (fup_Permiso_Publicidad.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Permiso_Publicidad.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (PERMISO PUBLICIDAD) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xAlu.PermisoPublicidad.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.PermisoPublicidad.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.PermisoPublicidad.Archivo = fup_Copia_Dni.FileBytes;
                }

                if (fup_Carnet_Seguro.HasFile)
                {
                    int maxBytes = 5 * 1024 * 1024; // 5 MB

                    if (fup_Carnet_Seguro.PostedFile.ContentLength > maxBytes)
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "alertSize",
                            "alert('El archivo (CARNET SEGURO) supera el límite máximo permitido de 5 MB.');",
                            true
                        );
                        return; // Detiene el proceso para no guardar
                    }

                    xAlu.CarnetSeguro.NombreArchivo = fup_Copia_Dni.FileName;
                    xAlu.CarnetSeguro.TamanioBytes = fup_Copia_Dni.FileBytes.Length;
                    xAlu.CarnetSeguro.Archivo = fup_Copia_Dni.FileBytes;
                }

                xdb.ModAlumno(xAlu);

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
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsAlumno xalu = new clsAlumno
                {
                    Id = Convert.ToInt32(this.txt_IdAlumno.Text.Trim())
                };

                xdb.DelAlumno(xalu.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Alumno eliminado correctamente.');", true);
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            var lista = xdb.GetAlumno().ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();

                lista = lista
                    .Where(x =>
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
            gvAlumnos.DataSource = lista;
            gvAlumnos.DataBind();
        }

        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetAlumno(); // 

                    // Buscar el usuario correspondiente al ID
                    var alum = lista.FirstOrDefault(u => u.Id == id);

                    if (alum != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdAlumno.Text = alum.Id.ToString();
                        Ddl_Tipo_Documento.SelectedValue = alum.TipoDocumento.Id.ToString();
                        Ddl_Apoderado.SelectedValue = alum.Apoderado.Id.ToString();
                        txt_Nombres.Text = alum.Nombres;
                        txt_ApellidoPaterno.Text = alum.ApellidoPaterno;
                        txt_ApellidoMaterno.Text = alum.ApellidoMaterno;
                        txt_Documento.Text = alum.Documento;
                        txt_Fecha_Nacimiento.Text = alum.FechaNacimiento.HasValue ? alum.FechaNacimiento.Value.ToString("yyyy-MM-dd") : string.Empty;
                        Ddl_Sexo.SelectedValue = alum.Sexo;
                        chb_Activo.Checked = alum.Activo;

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
                    xdb.DelAlumno(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Alumno eliminado correctamente.');", true);
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
            else
            {
                try
                {
                    // Enviar el archivo al navegador
                    clsArchivoBase xAb = xdb.RetArchivoAlumno(id, e.CommandName.ToString());

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

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsAlumno> lista = xdb.GetAlumno().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.Nombres ?? "").ToLower().Contains(filtro) ||
                            (x.ApellidoPaterno ?? "").ToLower().Contains(filtro) ||
                            (x.ApellidoMaterno ?? "").ToLower().Contains(filtro) ||
                            (x.Documento ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvAlumnos.DataSource = lista;
                gvAlumnos.DataBind();
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
                    $"alert('Error al cargar lista de Alumnos: {mensaje}');",
                    true
                );
            }
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
            clsValidacion.AplicarMaxLengthPorTipoDocumento(Ddl_Tipo_Documento, txt_Documento);
        }
    }
}