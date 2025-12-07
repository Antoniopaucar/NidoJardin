using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            var login = new wcfNido.clsLogin
            {
                Usuario = txtUsuario.Text.Trim(),
                clave = txtClave.Text.Trim()
            };

            var resultado = xdb.ValidarUsuario(login);

            if (resultado != null && resultado.Length > 0)
            {
                var primero = resultado[0];

                if (!primero.Exito)
                {
                    string script = $"alert('{primero.Mensaje}');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }

                //Usuario válido
                Session["Usuario"] = primero.Usuario;
                Session["IdUsuario"] = primero.Id_Usuario;

                //Guardar los arrays con roles y permisos
                var rolesArray = resultado
                    .Where(r => r.Id_Rol.HasValue)
                    .Select(r => r.Id_Rol.Value)
                    .Distinct()
                    .ToArray();

                var permisosArray = resultado
                    .Where(r => r.Id_Permiso.HasValue)
                    .Select(r => r.Id_Permiso.Value)
                    .Distinct()
                    .ToArray();

                Session["Roles"] = rolesArray;
                Session["Permisos"] = permisosArray;

                //Guardar todos los valores devueltos (el resultado completo)
                Session["ResultadoLogin"] = resultado;

                // Redirigir a la siguiente página
                Response.Redirect("frm_Inicio.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error en el usuario o clave.');", true);
            }

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            string script = "window.location.href = 'https://www.google.com';";
            ClientScript.RegisterStartupScript(this.GetType(), "IrAGoogle", script, true);
        }


    }
}