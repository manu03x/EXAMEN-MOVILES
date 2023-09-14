using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace Examen.Pages
{
    public partial class Config : ContentPage
    {
        private MainPageViewModel viewModel;

        public Config (MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }


        private void GuardarCambios_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("nombre", viewModel.WelcomeMessage);
            Preferences.Set("img", viewModel.ImageUrl);
            // Obtén los nuevos valores ingresados por el usuario desde las entradas
            string nuevoMensaje = viewModel.WelcomeMessage;
            string nuevaImageUrl = viewModel.ImageUrl;

            // Actualiza las propiedades en el ViewModel
            viewModel.WelcomeMessage = nuevoMensaje;
            viewModel.ImageUrl = nuevaImageUrl;

            // Puedes guardar los cambios en algún almacén de datos si es necesario

            // Luego, puedes navegar de regreso a la página principal o a donde desees
            Navigation.PopAsync();
        }
    }
}
