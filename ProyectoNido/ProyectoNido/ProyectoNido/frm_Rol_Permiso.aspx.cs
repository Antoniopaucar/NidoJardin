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
    public partial class frm_Rol_Permiso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsRol> listaRol = xdb.GetRol().ToList();

                Ddl_Rol.DataSource = listaRol;
                Ddl_Rol.DataTextField = "NombreRol";
                Ddl_Rol.DataValueField = "Id";
                Ddl_Rol.DataBind();
                Ddl_Rol.Items.Insert(0, new ListItem("-- Seleccione un Rol --", ""));

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

                clsRolPermiso xRpe = new clsRolPermiso();

                xRpe.Permiso = new clsPermiso();
                xRpe.Rol = new clsRol();

                xRpe.Rol.Id = int.Parse(Ddl_Rol.SelectedValue);
                xRpe.Permiso.Id = int.Parse(Ddl_Permiso.SelectedValue);
                xRpe.Tipo = this.txt_Tipo.Text.Trim();

                xdb.InsRolPermiso(xRpe);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('ROl Permiso agregado correctamente.');", true);
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
            var lista = xdb.GetRolPermiso().ToList();

            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x =>
                        (x.Permiso.NombrePermiso ?? "").ToLower().Contains(filtro) ||
                        (x.Rol.NombreRol ?? "").ToLower().Contains(filtro)
                    )
                    .ToList();
            }

            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            gvRolPermiso.DataSource = lista;
            gvRolPermiso.DataBind();
        }

        protected void gvRolPermiso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRolPermiso.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvRolPermiso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string[] argumentos = e.CommandArgument.ToString().Split('-');

                    //if (argumentos.Length < 3)
                      //  throw new Exception("El CommandArgument no contiene los valores esperados.");

                    // IDs de la clave compuesta
                    int idRol = Convert.ToInt32(argumentos[0]);
                    int idPermiso = Convert.ToInt32(argumentos[1]);

                    // Unir todo lo demás como texto (evita error con guiones)
                    string tipo = string.Join("-", argumentos.Skip(2));

                    wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                    xdb.DelRolPermiso(idRol, idPermiso);

                    CargarGrid();

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "alert",
                        "alert('Rol-Permiso eliminado correctamente.');",
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
                List<clsRolPermiso> lista = xdb.GetRolPermiso().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            ((x.Permiso?.NombrePermiso) ?? "").ToLower().Contains(filtro) ||
                            ((x.Rol?.NombreRol) ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvRolPermiso.DataSource = lista;
                gvRolPermiso.DataBind();
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