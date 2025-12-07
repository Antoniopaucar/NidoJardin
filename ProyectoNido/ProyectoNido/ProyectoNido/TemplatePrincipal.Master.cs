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
            //lbl_Usuario.Text = "USUARIO: " + Session["Usuario"].ToString();

            lbl_Usuario.Text = "USUARIO: " + Session["Usuario"].ToString();

            int[] roles = (int[])Session["Roles"];
            int[] permisos = (int[])Session["Permisos"];

            // ejemplo: mostrar cuántos roles y permisos tiene
            //lbl_Usuario.Text += $" | Roles: {roles.Length} | Permisos: {permisos.Length}";
        }
    }
}