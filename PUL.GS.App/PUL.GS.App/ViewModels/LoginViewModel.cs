using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Pages;
using PUL.GS.Core.Services;
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
        public bool IsBusy { get; set; }        

        public ICommand ConnectCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                //await ChatService.InitAsync(UserName);

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

        public ICommand LoginCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPageModel<LoginFacebookViewModel>();

                dialogs.HideLoading();

                IsBusy = false;
            }
        });


        IChatService ChatService;
        IUserDialogs dialogs;

        public LoginViewModel(IChatService _chatService, IUserDialogs _userDialogs)
        {
            ChatService = _chatService;
            dialogs = _userDialogs;
        }
    }
}
