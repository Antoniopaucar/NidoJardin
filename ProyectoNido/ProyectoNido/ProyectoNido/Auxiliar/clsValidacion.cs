using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido.Auxiliar
{
    public static class clsValidacion
    {
        public static void AplicarMaxLengthPorTipoDocumento(
            DropDownList ddl,
            TextBox txt)
        {
            // Si no hay selección válida
            if (string.IsNullOrEmpty(ddl.SelectedValue) || ddl.SelectedValue == "0")
            {
                txt.Text = "";
                txt.MaxLength = 20;  // Valor por defecto
                return;
            }

            wcfNido.Service1Client xdb = new wcfNido.Service1Client();
            var lista = xdb.GetTipoDocumento().ToList();

            if (lista == null || lista.Count == 0)
            {
                txt.MaxLength = 20;
                return;
            }

            int idSeleccionado = Convert.ToInt32(ddl.SelectedValue);

            var tipo = lista.FirstOrDefault(t => t.Id == idSeleccionado);

            if (tipo == null)
            {
                txt.MaxLength = 20;
                return;
            }

            txt.Text = "";
            txt.MaxLength = tipo.CantidadCaracteres;
        }

        public static void LimpiarControles(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Text = string.Empty;
                }
                else if (c is DropDownList ddl)
                {
                    ddl.SelectedIndex = 0; // o -1 si quieres sin selección
                }
                else if (c is CheckBox cb)
                {
                    cb.Checked = false;
                }
                else if (c is RadioButton rb)
                {
                    rb.Checked = false;
                }
                else if (c is RadioButtonList rbl)
                {
                    rbl.ClearSelection();
                }
                else if (c is ListBox lb)
                {
                    lb.ClearSelection();
                }
                else if (c is FileUpload fu)
                {
                    // No hay método directo para limpiar FileUpload, se puede reemplazar por uno nuevo
                    // Nota: esto solo funciona si no hay postback aún
                }

                // Recursivamente limpiar controles hijos
                if (c.HasControls())
                {
                    LimpiarControles(c);
                }
            }
        }
    }
}