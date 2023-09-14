
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Examen

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Configurar la página de inicio de la aplicación como SplashScreenPage
            MainPage = new NavigationPage(new SplashScreenPage());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            // Agregar la lógica de la SplashScreen aquí
            await ShowSplashScreen();

            // Navegar a la página principal una vez que se haya completado la SplashScreen
            MainPage = new NavigationPage(new MainPage());
        }

        private async Task ShowSplashScreen()
        {
            // Realizar la lógica de SplashScreen, por ejemplo, mostrar durante un tiempo específico
            await Task.Delay(30000); // Espera 3 segundos

            // O realiza cualquier otra lógica necesaria para la SplashScreen
        }
    }
}