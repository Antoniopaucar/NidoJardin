namespace Movil_Servicio_DB_Nido;
using Microsoft.Maui.Controls;
using Movil_Servicio_DB_Nido.Util;
using ServiceReference1;

public partial class CuotasPage : ContentPage
{
	public CuotasPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarCuotas();
    }

    private async Task CargarCuotas()
    {
        lblAlumno.Text = $"Alumno: {AppState.NombreAlumnoSeleccionado}";

        if (AppState.IdMatriculaActual <= 0)
        {
            await DisplayAlert("Aviso", "No hay matrícula activa.", "OK");
            return;
        }

        try
        {
            var client = new Service1Client();

            // 1) Resumen
            var resumen = await client.Mov_ResumenCuotasAsync(AppState.IdMatriculaActual);
            lblTotal.Text = $"S/ {resumen.Total:F2}";
            lblPagado.Text = $"S/ {resumen.Pagado:F2}";
            lblPendiente.Text = $"S/ {resumen.Pendiente:F2}";

            // 2) Lista de cuotas
            var cuotas = await client.Mov_ListarCuotasAsync(AppState.IdMatriculaActual);
            cvCuotas.ItemsSource = cuotas.ToList();
        }
        catch
        {
            await DisplayAlert("Error", "No se pudieron cargar las cuotas.", "OK");
        }
    }
}