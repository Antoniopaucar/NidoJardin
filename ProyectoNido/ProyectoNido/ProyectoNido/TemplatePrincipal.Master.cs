using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class TemplatePrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar sesión
            if (Session["Usuario"] == null)
            {
                Response.Redirect("frm_Login.aspx");
                return;
            }

            lbl_Usuario.Text = "USUARIO: " + Session["Usuario"].ToString();

            int[] roles = (int[])Session["Roles"];
            int[] permisos = (int[])Session["Permisos"];

            // Cargar roles en el ComboBox solo si no es PostBack
            if (!IsPostBack)
            {
                CargarRolesEnComboBox();
            }

            // Mostrar/ocultar grupos de menú según el rol
            MostrarOcultarGruposMenu();
        }

        // Nuevo método: Mostrar u ocultar grupos de menú según el rol
        private void MostrarOcultarGruposMenu()
        {
            try
            {
                string nombreRolActual = Session["NombreRolActual"]?.ToString() ?? "";
                
                // Verificar si el rol actual es Administrador o Secretario
                bool mostrarGrupos = EsAdministradorOSecretario(nombreRolActual);

                // Ocultar o mostrar todos los grupos
                grupo1.Visible = mostrarGrupos;
                grupo2.Visible = mostrarGrupos;
                grupo3.Visible = mostrarGrupos;
                grupo4.Visible = mostrarGrupos;
                grupo5.Visible = mostrarGrupos;
            }
            catch (Exception ex)
            {
                // En caso de error, ocultar los grupos por seguridad
                grupo1.Visible = false;
                grupo2.Visible = false;
                grupo3.Visible = false;
                grupo4.Visible = false;
                grupo5.Visible = false;
                System.Diagnostics.Debug.WriteLine("Error al mostrar/ocultar grupos: " + ex.Message);
            }
        }

        // Nuevo método: Verificar si el rol es Administrador o Secretario
        private bool EsAdministradorOSecretario(string nombreRol)
        {
            if (string.IsNullOrEmpty(nombreRol))
            {
                // Si no hay nombre de rol, intentar obtenerlo
                if (Session["RolActual"] != null)
                {
                    int rolActualId = Convert.ToInt32(Session["RolActual"]);
                    nombreRol = ObtenerNombreRol(rolActualId);
                }
            }

            string rolUpper = nombreRol.ToUpper();
            return rolUpper == "ADMINISTRADOR" || rolUpper == "SECRETARIO";
        }

        // Nuevo método: Cargar roles del usuario en el ComboBox
        private void CargarRolesEnComboBox()
        {
            try
            {
                int[] rolesIds = (int[])Session["Roles"];
                
                if (rolesIds == null || rolesIds.Length == 0)
                {
                    ddlRoles.Visible = false;
                    return;
                }

                // Si solo tiene un rol, ocultar el combo
                if (rolesIds.Length == 1)
                {
                    ddlRoles.Visible = false;
                    // Guardar el rol actual en Session
                    Session["RolActual"] = rolesIds[0];
                    Session["NombreRolActual"] = ObtenerNombreRol(rolesIds[0]);
                    return;
                }

                // Obtener los roles desde el servicio WCF
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                var listaRoles = servicio.ObtenerRolesPorIds(rolesIds);

                ddlRoles.DataSource = listaRoles;
                ddlRoles.DataTextField = "NombreRol";
                ddlRoles.DataValueField = "Id";
                ddlRoles.DataBind();

                // Seleccionar el rol actual si existe
                if (Session["RolActual"] != null)
                {
                    int rolActualId = Convert.ToInt32(Session["RolActual"]);
                    if (ddlRoles.Items.FindByValue(rolActualId.ToString()) != null)
                    {
                        ddlRoles.SelectedValue = rolActualId.ToString();
                    }
                    else
                    {
                        // Si no existe, seleccionar el primero
                        Session["RolActual"] = rolesIds[0];
                        Session["NombreRolActual"] = listaRoles.FirstOrDefault()?.NombreRol ?? "";
                    }
                }
                else
                {
                    // Primera vez, seleccionar el primero
                    Session["RolActual"] = rolesIds[0];
                    Session["NombreRolActual"] = listaRoles.FirstOrDefault()?.NombreRol ?? "";
                }

                ddlRoles.Visible = true;
            }
            catch (Exception ex)
            {
                // En caso de error, ocultar el combo
                ddlRoles.Visible = false;
                System.Diagnostics.Debug.WriteLine("Error al cargar roles: " + ex.Message);
            }
        }

        // Nuevo método: Obtener nombre de rol por ID
        private string ObtenerNombreRol(int idRol)
        {
            try
            {
                wcfNido.Service1Client servicio = new wcfNido.Service1Client();
                var listaRoles = servicio.ObtenerRolesPorIds(new int[] { idRol });
                return listaRoles.FirstOrDefault()?.NombreRol ?? "";
            }
            catch
            {
                return "";
            }
        }

        // Nuevo método: Obtener página de destino según rol
        private string ObtenerPaginaPorRol(string nombreRol)
        {
            switch (nombreRol.ToUpper())
            {
                case "ADMINISTRADOR":
                    return "frm_Inicio.aspx";
                case "SECRETARIO":
                    return "frm_Inicio.aspx";
                case "PROFESOR":
                    return "frm_Docente_Datos.aspx";
                case "APODERADO":
                    return "frm_Apoderado_Datos.aspx";
                default:
                    return "frm_Inicio.aspx";
            }
        }

        // Evento cuando se cambia el rol seleccionado
        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rolSeleccionadoId = Convert.ToInt32(ddlRoles.SelectedValue);
                string nombreRol = ddlRoles.SelectedItem.Text;

                // Guardar el rol seleccionado en Session
                Session["RolActual"] = rolSeleccionadoId;
                Session["NombreRolActual"] = nombreRol;

                // Redirigir a la página correspondiente al rol
                string paginaDestino = ObtenerPaginaPorRol(nombreRol);
                Response.Redirect(paginaDestino);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cambiar rol: " + ex.Message);
            }
        }
    }
}