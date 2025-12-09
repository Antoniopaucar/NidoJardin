using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ProyectoNido
{
    public partial class frm_Matricula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarGrilla();
                btn_Modificar.Enabled = false;
                btn_Anular.Enabled = false;
            }
        }

        private void CargarCombos()
        {
            Service1Client xdb = new Service1Client();

            // Alumnos
            var alumnos = xdb.listarAlumnos_Combo().ToList();

            ddl_Alumno.DataSource = alumnos;
            ddl_Alumno.DataTextField = "NombreCompleto";
            ddl_Alumno.DataValueField = "Id";
            ddl_Alumno.DataBind();

            ddl_Alumno.Items.Insert(0, new ListItem("-- Seleccione Alumno --", ""));

            // Grupo Anual
            var grupos = xdb.listarGrupoAnual_Combo().ToList();
            ddl_GrupoAnual.DataSource = grupos;
            ddl_GrupoAnual.DataTextField = "NombreGrupo";
            ddl_GrupoAnual.DataValueField = "Id_GrupoAnual";
            ddl_GrupoAnual.DataBind();
            ddl_GrupoAnual.Items.Insert(0, new ListItem("-- Seleccione Grupo --", ""));

            // Tarifario
            var tarifarios = xdb.listar_tarifario_combo().ToList();
            ddl_Tarifario.DataSource = tarifarios;
            ddl_Tarifario.DataTextField = "Nombre";
            ddl_Tarifario.DataValueField = "Id_Tarifario";
            ddl_Tarifario.DataBind();
            ddl_Tarifario.Items.Insert(0, new ListItem("-- Seleccione Tarifario --", ""));
        }

        // =============== ESTADOS DE BOTONES ===============
        private void ModoInicial()
        {
            txt_IdMatricula.Text = "";
            txt_Codigo.Text = "";
            ddl_Alumno.SelectedIndex = 0;
            ddl_GrupoAnual.SelectedIndex = 0;
            ddl_Tarifario.SelectedIndex = 0;
            txt_FechaMatricula.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ddl_Estado.SelectedValue = "A";
            txt_SubTotal.Text = "0.00";
            txt_DescuentoTotal.Text = "0.00";
            txt_Total.Text = "0.00";
            txt_Observacion.Text = "";

            btn_Guardar.Enabled = true;
            btn_Modificar.Enabled = false;
            btn_Anular.Enabled = false;
        }

        // =============== CARGAR GRILLA ===============
        private void CargarGrilla()
        {
            Service1Client xdb = new Service1Client();

            string estadoFiltro = ddlEstadoFiltro.SelectedValue; // "", "A", "I", "X"
            var lista = xdb.Nido_Matricula_Listar(estadoFiltro).ToList();

            // Filtro por texto (código o nombre alumno)
            string texto = txtBuscar.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(texto))
            {
                lista = lista.Where(m =>
                        (m.Codigo != null && m.Codigo.ToUpper().Contains(texto)) ||
                        (m.AlumnoNombre != null && m.AlumnoNombre.ToUpper().Contains(texto))
                    ).ToList();
            }

            gvMatriculas.DataSource = lista;
            gvMatriculas.DataBind();

            lblMensaje.Text = $"Se encontraron {lista.Count} matrículas.";
        }

        // =============== EVENTOS BOTONES CABECERA ===============

        protected void btn_Nuevo_Click(object sender, EventArgs e)
        {
            ModoInicial();
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                clsMatricula mat = LeerFormulario();

                // Insertar
                int idNuevo = xdb.Matricula_Insertar(mat);
                txt_IdMatricula.Text = idNuevo.ToString();

                lblMensaje.Text = "Matrícula registrada correctamente. Id = " + idNuevo;

                CargarGrilla();
                btn_Modificar.Enabled = true;
                btn_Anular.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar matrícula: " + ex.Message;
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_IdMatricula.Text))
            {
                lblMensaje.Text = "Seleccione una matrícula primero.";
                return;
            }

            try
            {
                Service1Client xdb = new Service1Client();
                clsMatricula mat = LeerFormulario();
                mat.Id_Matricula = int.Parse(txt_IdMatricula.Text);

                bool ok = xdb.Matricula_Actualizar(mat);
                if (ok)
                    lblMensaje.Text = "Matrícula actualizada correctamente.";
                else
                    lblMensaje.Text = "No se pudo actualizar la matrícula.";

                CargarGrilla();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al modificar matrícula: " + ex.Message;
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            ModoInicial();
        }

        protected void btn_Anular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_IdMatricula.Text))
            {
                lblMensaje.Text = "Seleccione una matrícula primero.";
                return;
            }

            try
            {
                int id = int.Parse(txt_IdMatricula.Text);
                Service1Client xdb = new Service1Client();

                // Cambiar a estado Anulado 'X'
                bool ok = xdb.Matricula_CambiarEstado(id, "X");
                if (ok)
                {
                    lblMensaje.Text = "Matrícula anulada correctamente.";
                    ddl_Estado.SelectedValue = "X";
                }
                else
                {
                    lblMensaje.Text = "No se pudo cambiar el estado de la matrícula.";
                }

                CargarGrilla();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cambiar estado de matrícula: " + ex.Message;
            }
        }

        // =============== FILTRO GRILLA ===============
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void gvMatriculas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMatriculas.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        // =============== ROWCOMMAND GRILLA ===============

        protected void gvMatriculas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Service1Client xdb = new Service1Client();

            if (e.CommandName == "Consultar")
            {
                var mat = xdb.Matricula_Obtener(id);
                if (mat != null)
                {
                    txt_IdMatricula.Text = mat.Id_Matricula.ToString();
                    txt_Codigo.Text = mat.Codigo;

                    ddl_Alumno.SelectedValue = mat.Id_Alumno.ToString();
                    ddl_GrupoAnual.SelectedValue = mat.Id_GrupoAnual.ToString();
                    ddl_Tarifario.SelectedValue = mat.Id_Tarifario.ToString();

                    txt_FechaMatricula.Text = mat.FechaMatricula != default(DateTime)
                        ? mat.FechaMatricula.ToString("yyyy-MM-dd")
                        : DateTime.Now.ToString("yyyy-MM-dd");

                    txt_SubTotal.Text = mat.SubTotal.ToString("0.00");
                    txt_DescuentoTotal.Text = mat.DescuentoTotal.ToString("0.00");
                    txt_Total.Text = mat.Total.ToString("0.00");
                    ddl_Estado.SelectedValue = string.IsNullOrEmpty(mat.Estado) ? "A" : mat.Estado;
                    txt_Observacion.Text = mat.Observacion;

                    btn_Guardar.Enabled = false;
                    btn_Modificar.Enabled = true;
                    btn_Anular.Enabled = true;
                }
            }
            else if (e.CommandName == "VerCuotas")
            {
                // Ir al detalle de cuotas
                Response.Redirect("frm_MatriculaDetalle.aspx?IdMatricula=" + id);
            }
            else if (e.CommandName == "CambiarEstado")
            {
                // Toggle rápido de estado: si está A -> X, si no -> A (ejemplo)
                var mat = xdb.Matricula_Obtener(id);
                if (mat != null)
                {
                    string nuevoEstado = mat.Estado == "A" ? "X" : "A";
                    bool ok = xdb.Matricula_CambiarEstado(id, nuevoEstado);
                    if (ok)
                        lblMensaje.Text = $"Estado de matrícula cambiado a {nuevoEstado}.";
                    else
                        lblMensaje.Text = "No se pudo cambiar el estado.";

                    CargarGrilla();
                }
            }
        }

        // =============== LECTURA DEL FORMULARIO ===============
        private clsMatricula LeerFormulario()
        {
            var mat = new clsMatricula();

            // Alumno
            if (!string.IsNullOrEmpty(ddl_Alumno.SelectedValue))
                mat.Id_Alumno = int.Parse(ddl_Alumno.SelectedValue);
            else
                throw new Exception("Debe seleccionar un alumno.");

            // Grupo
            if (!string.IsNullOrEmpty(ddl_GrupoAnual.SelectedValue))
                mat.Id_GrupoAnual = int.Parse(ddl_GrupoAnual.SelectedValue);
            else
                throw new Exception("Debe seleccionar un grupo anual.");

            // Tarifario
            if (!string.IsNullOrEmpty(ddl_Tarifario.SelectedValue))
                mat.Id_Tarifario = int.Parse(ddl_Tarifario.SelectedValue);
            else
                throw new Exception("Debe seleccionar un tarifario.");

            mat.Codigo = txt_Codigo.Text.Trim();

            if (!string.IsNullOrEmpty(txt_FechaMatricula.Text))
                mat.FechaMatricula = DateTime.Parse(txt_FechaMatricula.Text);
            else
                mat.FechaMatricula = DateTime.Now;

            // Totales: al crear puedes mandar 0 o NULL, serán recalculados luego
            decimal sub, desc, tot;
            mat.SubTotal = decimal.TryParse(txt_SubTotal.Text, out sub) ? sub : 0;
            mat.DescuentoTotal = decimal.TryParse(txt_DescuentoTotal.Text, out desc) ? desc : 0;
            mat.Total = decimal.TryParse(txt_Total.Text, out tot) ? tot : 0;

            mat.Estado = ddl_Estado.SelectedValue;
            mat.Observacion = txt_Observacion.Text.Trim();

            return mat;
        }
    }
}