namespace DataBinding
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Registra las rutas de navegación
            Routing.RegisterRoute(nameof(FormPage), typeof(FormPage));
        }
    }
}
