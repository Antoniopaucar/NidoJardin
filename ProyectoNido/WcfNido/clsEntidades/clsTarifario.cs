using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsTarifario
    {
        public int Id_Tarifario { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Periodo { get; set; }
        public decimal Valor { get; set; }

        public clsTarifario() { }

        public clsTarifario(int id, string tipo, string nombre, string descripcion,
                            int periodo, decimal valor)
        {
            Id_Tarifario = id;
            Tipo = tipo;
            Nombre = nombre;
            Descripcion = descripcion;
            Periodo = periodo;
            Valor = valor;
        }
    }
}
