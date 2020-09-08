using FreshMvvm;
using PUL.GS.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PUL.GS.App
{
    public class SplashPage : ContentPage
    {
        readonly Image splashImage;
        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logo.png",
                WidthRequest = 100,
                HeightRequest = 100,
                //Rotation = 90 
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#ffffff"); //429de3
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //await splashImage.RotateTo(360, 0, Easing.Linear);
            await splashImage.ScaleTo(150, 1000, Easing.Linear);
            await splashImage.ScaleTo(0.9, 1300, Easing.Linear);
            await splashImage.ScaleTo(1, 1800); //Time consuming processes such as initialization

            var loginPage =
                FreshPageModelResolver
                .ResolvePageModel<LoginViewModel>();

            var navPage =
                new FreshNavigationContainer(loginPage);

            //Para ocultar la barra de navegación no se puede ir a la siguiente página usando FreshMvvm y el ViewModel de la página, se debe navegar directamente a la página.
            //Application.Current.MainPage = new NavigationPage(loginPage);
            //Al resolver la página por medio de FreshMvvm tambien se pudo mostrar sin barra de navegación
            Application.Current.MainPage = navPage; 

        }
    }
}