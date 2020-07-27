using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Infrastructure;
using PUL.GS.App.Pages;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Json;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class LoginViewModel : FreshBasePageModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBusy { get; set; }

        private readonly AccountData _accountAgent;
        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        public ICommand LoginCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Cargando");

                //await ChatService.InitAsync(UserName);

                User user = new User()
                { 
                    Username = UserName,
                    Password = Password
                };

                var token = _accountAgent.GetToken(user);

                if (token.Success)
                    await CoreMethods.PushPageModel<MainViewModel>(UserName);

                //var masterDetail = new FreshMasterDetailNavigationContainer();
                //masterDetail.AddPage<MainViewModel>("Inicio");
                //masterDetail.AddPage<ProfileViewModel>("Perfil", "");
                //masterDetail.AddPage<RoomsViewModel>("Salas", "");

                //masterDetail.Init("Menu", "logo.png");
                //Application.Current.MainPage = masterDetail;


                dialogs.HideLoading();

                IsBusy = false;
            }
        });

        public ICommand LoginFacebookCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                //dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPageModel<LoginFacebookViewModel>();

                //dialogs.HideLoading();

                IsBusy = false;
            }
        });


        //IChatService ChatService;
        IUserDialogs dialogs;

        public LoginViewModel(
            IUserDialogs _userDialogs
            //IChatService _chatService
            )
        {
            _accountAgent = new AccountData(appSettings);
            //ChatService = _chatService;
            dialogs = _userDialogs;
        }
    }
}
