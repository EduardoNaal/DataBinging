using System.Collections.ObjectModel;
using System.Text.Json;

namespace DataBinding
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private ObservableCollection<PersonaModel> _personas;
        public MainPage()
        {
            InitializeComponent();
            GetData();
            BindingContext = this;
        }

        public ObservableCollection<PersonaModel> Personas
        {
            get => _personas;
            set
            {
                _personas = value;
                OnPropertyChanged();
            }
        }
        public async void GetData()
        {
            var response = await _httpClient.GetStringAsync("https://fi.jcaguilar.dev/v1/escuela/persona");
            var personas = JsonSerializer.Deserialize<ObservableCollection<PersonaModel>>(response);

            Personas = personas;
        }


        private async void Button_Cliked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormPage());
        }
    }

}
