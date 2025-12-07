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
    public partial class frm_Comunicado : System.Web.UI.Page
    {
        private int Id_Usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id_Usuario = Convert.ToInt32(Session["IdUsuario"]);

            if (!IsPostBack)
            {
                txt_Fecha_Creacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
                

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsRol> lista = xdb.GetRol().ToList();

                Ddl_Rol.DataSource = lista;
                Ddl_Rol.DataTextField = "NombreRol";
                Ddl_Rol.DataValueField = "Id";
                Ddl_Rol.DataBind();
                Ddl_Rol.Items.Insert(0, new ListItem("-- Seleccione a quien va dirigido --", ""));
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsComunicado xCom = new clsComunicado();

                xCom.Usuario = new clsUsuario();
                xCom.Rol = new clsRol();
                
                xCom.Usuario.Id = Id_Usuario;
                xCom.Rol.Id = int.Parse(Ddl_Rol.SelectedValue);
                xCom.Nombre = txt_Nombre.Text.Trim();
                xCom.Descripcion = txt_Descripcion.Text.Trim();
                xCom.FechaFinal = DateTime.TryParse(txt_Fecha_Final.Text.Trim(), out DateTime f) ? f : (DateTime?)null;

                xdb.InsComunicado(xCom);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "Alert",
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

                clsComunicado xCom = new clsComunicado();
                xCom.Usuario = new clsUsuario();
                xCom.Rol = new clsRol();

                xCom.Id = Convert.ToInt32(this.txt_IdComunicado.Text.Trim());
                xCom.Usuario.Id = Id_Usuario;
                xCom.Rol.Id = int.Parse(Ddl_Rol.SelectedValue);
                xCom.Nombre = txt_Nombre.Text.Trim();
                xCom.Descripcion = txt_Descripcion.Text.Trim();
                xCom.FechaFinal = DateTime.TryParse(txt_Fecha_Final.Text.Trim(), out DateTime f) ? f : (DateTime?)null;

                xdb.ModComunicado(xCom);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado modificado correctamente.');", true);
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

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsComunicado xCom = new clsComunicado
                {
                    Id = Convert.ToInt32(this.txt_IdComunicado.Text.Trim())
                };

                xdb.DelComunicado(xCom.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado eliminado correctamente.');", true);
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

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                //Id_Usuario = Convert.ToInt32(Session["IdUsuario"]);
                List<clsComunicado> lista = xdb.GetComunicado(Id_Usuario).ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.Nombre ?? "").ToLower().Contains(filtro) ||
                            (x.Rol.NombreRol ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvComunicados.DataSource = lista;
                gvComunicados.DataBind();
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
                    $"alert('Error al cargar lista de comunicados: {mensaje}');",
                    true
                );

            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            //Id_Usuario = Convert.ToInt32(Session["IdUsuario"]);
            var lista = xdb.GetComunicado(Id_Usuario).ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();

                lista = lista
                    .Where(x =>
                        (x.Nombre != null && x.Nombre.ToLower().Contains(filtro)) ||
                        (x.Rol.NombreRol != null && x.Rol.NombreRol.ToLower().Contains(filtro))
                    )
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvComunicados.DataSource = lista;
            gvComunicados.DataBind();
        }

        protected void gvComunicados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    //Id_Usuario = Convert.ToInt32(Session["IdUsuario"]);
                    var lista = xdb.GetComunicado(Id_Usuario); // 

                    // Buscar el usuario correspondiente al ID
                    var Comu = lista.FirstOrDefault(u => u.Id == id);

                    if (Comu != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdComunicado.Text = Comu.Id.ToString();
                        //Id_Usuario = Convert.ToInt32(Comu.Usuario.Id.ToString());
                        Ddl_Rol.SelectedValue = Comu.Rol.Id.ToString();
                        txt_Nombre.Text = Comu.Nombre;
                        txt_Descripcion.Text = Comu.Descripcion;
                        txt_Fecha_Creacion.Text = Comu.FechaCreacion.HasValue ? Comu.FechaCreacion.Value.ToString("yyyy-MM-dd") : string.Empty;
                        txt_Fecha_Final.Text = Comu.FechaFinal.HasValue ? Comu.FechaFinal.Value.ToString("yyyy-MM-dd") : string.Empty;

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
                    xdb.DelComunicado(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado eliminado correctamente.');", true);
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
        }

        protected void gvComunicados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvComunicados.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            clsValidacion.LimpiarControles(this);
            txt_Fecha_Creacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}