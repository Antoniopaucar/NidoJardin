using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsArchivoBase
    {
        public string NombreArchivo { get; set; }
        public int TamanioBytes { get; set; }
        public byte[] Archivo { get; set; }
    }
}
