using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Pages;
using PUL.GS.Core.Services;
using System.Windows.Input;
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

                await ChatService.InitAsync(UserName);

                await CoreMethods.PushPageModel<RoomsViewModel>(UserName);

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
