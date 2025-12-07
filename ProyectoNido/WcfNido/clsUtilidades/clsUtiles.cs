using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace clsUtilidades
{
    public static class clsUtiles
    {
        public static string ObtenerSha256(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input.Trim()));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
        public static string ValidarUser(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return "El Usuario no debe estar vacío.";

            if (Regex.IsMatch(nombreUsuario, @"\s"))
                return "El Usuario No debe contener espacios en blanco.";

            if (nombreUsuario.Length < 8 || nombreUsuario.Length > 15)
                return "El Usuario debe tener un mínimo de 8 caracteres y un máximo de 15 caracteres.";

            if (Regex.IsMatch(nombreUsuario, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "El Usuario NO debe tener el formato de una dirección de correo electrónico.";

            return null;
        }

        public static string ValidarContrasenia(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                return "la contraseña no debe estar vacía.";

            if (contrasena.Length < 8 || contrasena.Length > 15)
                return "La contraseña debe tener un mínimo de 8 caracteres y un máximo de 15 caracteres.";

            if (!Regex.IsMatch(contrasena, @"\d"))
                return "La contraseña debe contener al menos un número.";

            if (!Regex.IsMatch(contrasena, @"[A-Z]"))
                return "La contraseña debe contener al menos una letra mayúscula.";

            if (!Regex.IsMatch(contrasena, @"[!@#$%^&*()_+{}\[\]:;'<>,.?\\|~`-]"))
                return "La contraseña debe contener al menos un carácter especial.";

            if (Regex.IsMatch(contrasena, @"\s"))
                return "La contraseña No debe contener espacios en blanco.";

            return null;
        }
    }
}
