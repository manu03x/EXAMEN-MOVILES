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

			viewModel.welcomeMessage = Preferences.Get("nombre", string.Empty);;
			viewModel.imageUrl = Preferences.Get("img", string.Empty);
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


