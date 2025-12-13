using ServiceReference1;
using Movil_Servicio_DB_Nido.Util;

namespace Movil_Servicio_DB_Nido;

public partial class HomePage : ContentPage
{
    private List<clsAlumno> _hijos = new();
    public HomePage()
	{
		InitializeComponent();
        // Mostrar nombre del apoderado (del login)
        lblApoderado.Text = LoginPage.NombreApoderadoGlobal;
        CargarHijos();
    }
    private async void CargarHijos()
    {
        try
        {
            var client = new Service1Client();
            var lista = await client.Mov_ListarHijosPorApoderadoAsync(LoginPage.IdUsuarioGlobal);

            _hijos = lista.ToList();

            pickerHijos.ItemsSource = _hijos;
            pickerHijos.ItemDisplayBinding = new Binding("NombreCompleto");

            // Opcional: auto-seleccionar si solo tiene 1 hijo
            if (_hijos.Count == 1)
                pickerHijos.SelectedIndex = 0;
        }
        catch
        {
            await DisplayAlert("Error", "No se pudieron cargar los hijos.", "OK");
        }
    }
    private async void pickerHijos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pickerHijos.SelectedItem is clsAlumno alumno)
        {
            AppState.IdAlumnoSeleccionado = alumno.Id;
            AppState.NombreAlumnoSeleccionado = alumno.NombreCompleto;

            try
            {
                var client = new Service1Client();
                var mat = await client.Mov_ObtenerMatriculaActualAsync(AppState.IdAlumnoSeleccionado);

                // Usa clsMatricula (o lo que devuelva tu WCF)
                AppState.IdMatriculaActual = (mat != null) ? mat.Id_Matricula : 0;

                if (AppState.IdMatriculaActual <= 0)
                {
                    await DisplayAlert("Aviso", "Este alumno no tiene matrícula activa.", "OK");
                }
            }
            catch
            {
                AppState.IdMatriculaActual = 0;
                await DisplayAlert("Error", "No se pudo obtener la matrícula actual.", "OK");
            }
        }
    }

    private async void GoComunicados(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//comunicados");
    }

    private async void GoCuotas(object sender, EventArgs e)
    {
        if (AppState.IdAlumnoSeleccionado <= 0)
        {
            await DisplayAlert("Aviso", "Seleccione un hijo primero.", "OK");
            return;
        }

        if (AppState.IdMatriculaActual <= 0)
        {
            await DisplayAlert("Aviso", "El hijo seleccionado no tiene matrícula activa.", "OK");
            return;
        }

        await Shell.Current.GoToAsync("//cuotas");
    }


}