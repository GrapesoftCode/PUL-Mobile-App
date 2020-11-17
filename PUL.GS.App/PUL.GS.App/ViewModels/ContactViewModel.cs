using Acr.UserDialogs;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        readonly IChatService ChatService;
        readonly IUserDialogs dialogs;

        public User CurrentUser { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }
        public Contact CurrentContact { get; set; }
        public ICommand EnterContactCommand { get; set; }

        public ContactViewModel(
            IUserDialogs _dialogs,
            IChatService _chatService)
        {
            ChatService = _chatService;
            dialogs = _dialogs;
            Contacts = new ObservableCollection<Contact>();
            CurrentContact = new Contact();
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            EnterContactCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentContact != null)
                    {
                        //Tuple<string, string> data =
                        //new Tuple<string, string>(CurrentUser.Username, CurrentContact.Username);
                        //await CoreMethods.PushPageModel<ChatViewModel>(data);

                        Greeting greeting = new Greeting()
                        {
                            Username = CurrentUser.Username,
                            FileName = null
                        };

                        await ChatService.InitAsync(CurrentContact.Username);

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

            dialogs.ShowLoading("Cargando...");

            var liscontacts = await contactAgent.GetAll(CurrentUser.Username);
            Contacts = new ObservableCollection<Contact>(liscontacts.objectResult);

            await ChatService.InitAsync(CurrentContact.Username);

            //Rooms = await ChatService.GetRooms();

            dialogs.HideLoading();
        }
    }
}
