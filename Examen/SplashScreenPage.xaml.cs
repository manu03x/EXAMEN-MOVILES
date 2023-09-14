using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Examen
{
    public partial class SplashScreenPage : ContentPage
    {
			private MainPageViewModel viewModel;
        public SplashScreenPage()
        {
            InitializeComponent();
			viewModel = new MainPageViewModel();

			viewModel.welcomeMessage = Preferences.Get("nombre", string.Empty);;
			viewModel.imageUrl = Preferences.Get("img", string.Empty);
			BindingContext = viewModel;
            // Agrega tu lógica aquí, por ejemplo, una llamada a un servicio web.
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            // Simula la carga de datos durante unos segundos
            await Task.Delay(5000);

            // Navega a la página principal de la aplicación
            await Navigation.PushAsync(new MainPage());
        }
    }
}
