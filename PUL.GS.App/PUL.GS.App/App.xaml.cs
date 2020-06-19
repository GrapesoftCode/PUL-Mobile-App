using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.ViewModels;
using PUL.GS.Core.Services;
using Xamarin.Forms;

namespace PUL.GS.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConfigureContainer();

            var loginPage =
                FreshPageModelResolver
                .ResolvePageModel<LoginViewModel>();

            var navPage =
                new FreshNavigationContainer(loginPage);

            MainPage = navPage;

        }

        private void ConfigureContainer()
        {
            FreshIOC.Container.Register<IChatService, ChatService>();
            FreshIOC.Container.Register<IUserDialogs>(UserDialogs.Instance);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
