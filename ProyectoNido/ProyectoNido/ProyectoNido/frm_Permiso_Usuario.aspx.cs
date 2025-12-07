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
    public partial class frm_Permiso_Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuario> listaUser = xdb.GetUsuario().ToList();

                Ddl_Usuario.DataSource = listaUser;
                Ddl_Usuario.DataTextField = "NombreUsuario";
                Ddl_Usuario.DataValueField = "Id";
                Ddl_Usuario.DataBind();
                Ddl_Usuario.Items.Insert(0, new ListItem("-- Seleccione un Usuario --", ""));

                List<clsPermiso> listaPermiso = xdb.GetPermiso().ToList();

                Ddl_Permiso.DataSource = listaPermiso;
                Ddl_Permiso.DataTextField = "NombrePermiso";
                Ddl_Permiso.DataValueField = "Id";
                Ddl_Permiso.DataBind();
                Ddl_Permiso.Items.Insert(0, new ListItem("-- Seleccione un Permiso --", ""));
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuarioPermiso xUsp = new clsUsuarioPermiso();

                xUsp.Usuario = new clsUsuario();
                xUsp.Permiso = new clsPermiso();

                xUsp.Usuario.Id = int.Parse(Ddl_Usuario.SelectedValue);
                xUsp.Permiso.Id = int.Parse(Ddl_Permiso.SelectedValue);
                xUsp.Tipo = this.txt_Tipo.Text.Trim();

                xdb.InsUsuarioPermiso(xUsp);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario Permiso agregado correctamente.');", true);
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

            wcfNido.Service1Client xdb = new wcfNido.Service1Client();
            var lista = xdb.GetUsuarioPermiso().ToList();

            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x =>
                        (x.Usuario.NombreUsuario ?? "").ToLower().Contains(filtro) ||
                        (x.Permiso.NombrePermiso ?? "").ToLower().Contains(filtro)
                    )
                    .ToList();
            }

            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            gvUsuarioPermiso.DataSource = lista;
            gvUsuarioPermiso.DataBind();
        }

        protected void gvUsuarioPermiso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarioPermiso.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvUsuarioPermiso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string[] argumentos = e.CommandArgument.ToString().Split('-');

                    //if (argumentos.Length < 3)
                    //  throw new Exception("El CommandArgument no contiene los valores esperados.");

                    // IDs de la clave compuesta
                    int idUsuario = Convert.ToInt32(argumentos[0]);
                    int idPermiso = Convert.ToInt32(argumentos[1]);

                    // Unir todo lo demás como texto (evita error con guiones)
                    string tipo = string.Join("-", argumentos.Skip(2));

                    wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                    xdb.DelUsuarioPermiso(idUsuario, idPermiso);

                    CargarGrid();

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "alert",
                        "alert('Usuario-Permiso eliminado correctamente.');",
                        true);
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
                    $"alert('Error: {mensaje}');",
                    true
                );
            }
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuarioPermiso> lista = xdb.GetUsuarioPermiso().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.Usuario.NombreUsuario ?? "").ToLower().Contains(filtro) ||
                            (x.Permiso.NombrePermiso ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvUsuarioPermiso.DataSource = lista;
                gvUsuarioPermiso.DataBind();
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

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;

            clsValidacion.LimpiarControles(this);
        }
    }
}