using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace DataBinding;

public partial class FormPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();

    public FormPage()
    {
        InitializeComponent();
    }

    private async void Button_Guardar_Clickeded(object sender, EventArgs e)
    {
        // Obtener los datos del formulario
        var persona = new PersonaModel
        {
            nombre = nombre.Text,
            apellido = apellido.Text,
            sexo = GetSelectedSexo(),
            fh_nac = fechaNacimiento.Date.ToString("yyyy-MM-dd"),
            id_rol = rolPicker.SelectedIndex + 1
        };

        // Serializar la persona a JSON
        var jsonContent = JsonSerializer.Serialize(persona);

        // Crear el contenido de la solicitud
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Enviar los datos a la API con una solicitud POST
        var response = await _httpClient.PostAsync("https://fi.jcaguilar.dev/v1/escuela/persona", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Persona guardada correctamente", "OK");
            await Navigation.PushAsync(new MainPage());  // Regresar a la página principal
        }
        else
        {
            await DisplayAlert("Error", "No se pudo guardar la persona", "OK");
        }
    }

    // Evento para el botón de Regresar
    private async void Button_Regresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();  // Regresar a la página principal
    }

    private string? GetSelectedSexo()
    {
        if (sexoMasculino.IsChecked)
            return "h";  // Masculino
        else if (sexoFemenino.IsChecked)
            return "m";  // Femenino
        else if (sexoOtro.IsChecked)
            return "o";  // Otro
        else
            return null;
    }
}