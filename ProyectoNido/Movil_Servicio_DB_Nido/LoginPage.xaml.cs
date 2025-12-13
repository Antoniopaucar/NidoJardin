using Movil_Servicio_DB_Nido.Util;
using ServiceReference1;
namespace Movil_Servicio_DB_Nido;

public partial class LoginPage : ContentPage
{
    // Variables globales simples (luego las puedes mover a AppState)
    public static int IdUsuarioGlobal = 0;
    public static string NombreApoderadoGlobal = string.Empty;
    public LoginPage()
	{
		InitializeComponent();
	}

    private async void BtnIngresar_Clicked(object sender, EventArgs e)
    {
        lblError.IsVisible = false;

        string usuario = txtUser.Text?.Trim() ?? string.Empty;
        string clave = txtClave.Text?.Trim() ?? string.Empty;

        // Validación básica
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
        {
            lblError.Text = "Ingrese usuario y clave.";
            lblError.IsVisible = true;
            return;
        }

        try
        {
            // Cliente WCF
            var client = new Service1Client();

            string claveHash = HashHelper.Sha256(clave);

            var resp = await client.Mov_LoginApoderadoAsync(usuario, claveHash);

            // Validación de respuesta
            if (resp == null || resp.Id_Usuario <= 0)
            {
                lblError.Text = "Usuario o clave incorrectos.";
                lblError.IsVisible = true;
                return;
            }

            // Guardamos datos globales
            IdUsuarioGlobal = resp.Id_Usuario;
            NombreApoderadoGlobal = resp.NombreCompleto;

            // Navegación a selección de hijos
            await Shell.Current.GoToAsync("//home");
        }
        catch (Exception ex)
        {
            lblError.Text = "Error de conexión con el servidor.";
            lblError.IsVisible = true;

            // Para debug (opcional)
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}