using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_MatriculaDetalle : System.Web.UI.Page
    {
        private int IdMatricula
        {
            get { return int.Parse(txt_IdMatricula.Text); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idMatricula;
                string valor = Request.QueryString["IdMatricula"];

                if (!int.TryParse(valor, out idMatricula))
                {
                    lblMensaje.Text = "Id de matrícula no válido.";
                    return;
                }

                txt_IdMatricula.Text = idMatricula.ToString();   // solo para mostrar
                CargarGrid();                                    // carga solo las cuotas de esa matrícula
                btn_Modificar.Enabled = false;
            }
        }
        private void CargarGrid()
        {
            Service1Client xdb = new Service1Client();
            var lista = xdb.MatriculaDetalle_ListarPorMatricula(IdMatricula).ToList();

            gvDetalle.DataSource = lista;
            gvDetalle.DataBind();

            lblMensaje.Text = $"Se encontraron {lista.Count} cuotas para la matrícula {IdMatricula}.";
        }
        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                clsMatriculaDetalle det = LeerFormulario();
                det.Id_Matricula = IdMatricula;

                int idNuevo = xdb.MatriculaDetalle_Insertar(det);
                lblMensaje.Text = "Detalle registrado. Id = " + idNuevo;

                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar detalle: " + ex.Message;
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client xdb = new Service1Client();
                clsMatriculaDetalle det = LeerFormulario();
                det.Id_Matricula = IdMatricula;
                det.Id_MatriculaDetalle = int.Parse(txt_IdMatriculaDetalle.Text);

                xdb.MatriculaDetalle_Actualizar(det);
                lblMensaje.Text = "Detalle actualizado.";

                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al modificar detalle: " + ex.Message;
            }
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_IdMatriculaDetalle.Text))
                {
                    lblMensaje.Text = "Seleccione un detalle primero.";
                    return;
                }

                int idDet = int.Parse(txt_IdMatriculaDetalle.Text);
                Service1Client xdb = new Service1Client();
                xdb.MatriculaDetalle_Eliminar(idDet);

                lblMensaje.Text = "Detalle eliminado.";
                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar detalle: " + ex.Message;
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txt_IdMatriculaDetalle.Text = "";
            txt_NroCuota.Text = "";
            txt_NombreCuota.Text = "";
            txt_FechaVencimiento.Text = "";
            txt_Cantidad.Text = "";
            txt_Monto.Text = "";
            txt_Descuento.Text = "0";
            txt_Adicional.Text = "0";
            txt_FechaPago.Text = "";
            ddl_EstadoPago.SelectedValue = "P";
            txt_TotalLinea.Text = "";
            txt_Observacion.Text = "";

            btn_Modificar.Enabled = false;
        }

        private clsMatriculaDetalle LeerFormulario()
        {
            var det = new clsMatriculaDetalle();

            det.NroCuota = string.IsNullOrWhiteSpace(txt_NroCuota.Text)
                ? (int?)null
                : int.Parse(txt_NroCuota.Text);

            det.NombreCuota = txt_NombreCuota.Text.Trim();

            det.FechaVencimiento = string.IsNullOrWhiteSpace(txt_FechaVencimiento.Text)
                ? (DateTime?)null
                : DateTime.Parse(txt_FechaVencimiento.Text);

            det.Cantidad = string.IsNullOrWhiteSpace(txt_Cantidad.Text)
                ? 1
                : int.Parse(txt_Cantidad.Text);

            det.Monto = string.IsNullOrWhiteSpace(txt_Monto.Text)
                ? 0
                : decimal.Parse(txt_Monto.Text);

            det.Descuento = string.IsNullOrWhiteSpace(txt_Descuento.Text)
                ? 0
                : decimal.Parse(txt_Descuento.Text);

            det.Adicional = string.IsNullOrWhiteSpace(txt_Adicional.Text)
                ? 0
                : decimal.Parse(txt_Adicional.Text);

            det.FechaPago = string.IsNullOrWhiteSpace(txt_FechaPago.Text)
                ? (DateTime?)null
                : DateTime.Parse(txt_FechaPago.Text);

            det.EstadoPago = ddl_EstadoPago.SelectedValue;
            det.Observacion = txt_Observacion.Text.Trim();

            return det;
        }

        protected void gvDetalle_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int idDet = Convert.ToInt32(e.CommandArgument);
            Service1Client xdb = new Service1Client();

            if (e.CommandName == "Consultar")
            {
                var det = xdb.MatriculaDetalle_Obtener(idDet);
                if (det != null)
                {
                    txt_IdMatriculaDetalle.Text = det.Id_MatriculaDetalle.ToString();
                    txt_NroCuota.Text = det.NroCuota?.ToString();
                    txt_NombreCuota.Text = det.NombreCuota;
                    txt_FechaVencimiento.Text = det.FechaVencimiento?.ToString("yyyy-MM-dd");
                    txt_Cantidad.Text = det.Cantidad.ToString();
                    txt_Monto.Text = det.Monto.ToString("0.00");
                    txt_Descuento.Text = det.Descuento.ToString("0.00");
                    txt_Adicional.Text = det.Adicional.ToString("0.00");
                    txt_FechaPago.Text = det.FechaPago?.ToString("yyyy-MM-dd");
                    ddl_EstadoPago.SelectedValue = det.EstadoPago;
                    txt_TotalLinea.Text = det.TotalLinea.ToString("0.00");
                    txt_Observacion.Text = det.Observacion;

                    btn_Modificar.Enabled = true;
                }
            }
            else if (e.CommandName == "Anular")
            {
                xdb.MatriculaDetalle_Anular(idDet);
                CargarGrid();
            }
            else if (e.CommandName == "Reactivar")
            {
                xdb.MatriculaDetalle_Reactivar(idDet);
                CargarGrid();
            }
        }

        //private void Deshabilitar()
        //{
        //    pnlContenido.Enabled = false; // si quieres, crea un panel para todo
        //}

    }
}