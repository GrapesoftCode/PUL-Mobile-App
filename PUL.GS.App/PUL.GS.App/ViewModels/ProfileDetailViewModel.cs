using Acr.UserDialogs;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class ProfileDetailViewModel : BaseViewModel
    {
        readonly IChatService ChatService;
        readonly IUserDialogs dialogs;
        public Contact CurrentContact { get; set; }
        public ICommand EnterContactCommand { get; set; }
        public Image Image { get; set; }

        public ProfileDetailViewModel(
            IUserDialogs _dialogs,
            IChatService _chatService)
        {
            ChatService = _chatService;
            dialogs = _dialogs;
        }


        //public ICommand LoginCommand => new Command(async () =>
        //{
        //    if (!IsBusy)
        //    {
        //        IsBusy = true;

        //        dialogs.ShowLoading("Cargando", MaskType.Gradient);


        //        dialogs.HideLoading();

        //        IsBusy = false;
        //    }
        //});

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentContact = initData as Contact;

            EnterContactCommand = new Command(async (image) =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentContact != null)
                    {
                        string fileName = (string)image;

                        await CoreMethods.PopPopupPageModel();
                        await CoreMethods.PopToRoot(false);

                        Greeting greeting = new Greeting()
                        {
                            Username = CurrentContact.Username,
                            FileName = fileName
                        };

                        Tuple<Greeting, string> data =
                        new Tuple<Greeting, string>(greeting, CurrentContact.Username);
                        await CoreMethods.PushPageModel<ChatViewModel>(data);
                        CurrentContact = null;
                    }

                    IsBusy = false;
                }
            });
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);


            await ChatService.InitAsync(CurrentContact.Username);
        }
    }
}
