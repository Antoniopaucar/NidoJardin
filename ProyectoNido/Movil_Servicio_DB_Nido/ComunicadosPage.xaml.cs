namespace Movil_Servicio_DB_Nido;
using ServiceReference1;
using System.Collections.ObjectModel;
using System.Linq;

public partial class ComunicadosPage : ContentPage
{

	public ComunicadosPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarComunicados();
    }

    private async Task CargarComunicados()
    {
        try
        {
            var client = new Service1Client();
            int idUsuario = LoginPage.IdUsuarioGlobal;

            // Puede venir como List o como Array según tu referencia WCF
            var data = await client.mov_Comunicado_Listar_Por_UsuarioAsync(idUsuario);

            // Si data ya es List<E_Comunicado>, esto funciona igual
            var lista = data?.ToList() ?? new List<E_Comunicado>();

            if (lista.Count == 0)
            {
                lvComunicados.ItemsSource = null; // EmptyView mostrará el mensaje
                return;
            }

            lvComunicados.ItemsSource = lista;
        }
        catch
        {
            await DisplayAlert("Error", "No se pudieron cargar los comunicados.", "OK");
        }
    }
}