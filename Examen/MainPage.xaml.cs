using System;
using Examen.Pages;
namespace Examen;
public partial class MainPage : ContentPage
{
    private MainPageViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();
        viewModel = new MainPageViewModel();

        viewModel.welcomeMessage = "Venta de casas";
        viewModel.imageUrl = "https://images.vexels.com/media/users/3/128197/isolated/preview/f7bab71f1e4284e313bd7ddb4fa085e9-icono-de-lupa-inmobiliaria-casa.png";
       	BindingContext = viewModel;
    }

    private async void GoToClientes(object sender, EventArgs e)
    {
  		var clientesPage = new Clientes(); // Crear una instancia de la página
		await Navigation.PushAsync(clientesPage); // Agregar la página a la pila de navegación
    }

    private async void GoToPropiedades(object sender, EventArgs e)
    {
        // Implementa la navegación a la página de Propiedades
		var propiedadesPage = new Propiedades(); // Crear una instancia de la página
		await Navigation.PushAsync(propiedadesPage); // Agregar la página a la pila de navegación
    }

    private async void OpenConfig(object sender, EventArgs e)
    {
  		var configPage = new Config(viewModel); // Crear una instancia de la página
		await Navigation.PushAsync(configPage); // Agregar la página a la pila de navegación
    }
}


