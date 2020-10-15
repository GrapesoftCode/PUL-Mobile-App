using Acr.UserDialogs;
using PUL.GS.App.Pages;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class PulerViewModel : BaseViewModel
    {

        readonly IChatService ChatService;
        readonly IUserDialogs dialogs;
        private ObservableCollection<Contact> contacts;
        private bool isRefreshing;
        private bool pulers;
        private bool activity;
        private bool isToggled;
        private string phantom;

        public string Phantom
        {
            get => phantom; set
            {
                phantom = value;
                OnPropertyChanged();
            }
        }

        public bool IsToggled
        {
            get => isToggled; set
            {
                isToggled = value;
                ChangeCollection(IsToggled);
                OnPropertyChanged();
            }
        }

        public bool Pulers
        {
            get => pulers; set
            {
                pulers = value;
                OnPropertyChanged();
            }
        }

        public bool Activity
        {
            get => activity; set
            {
                activity = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get => isRefreshing; set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand { get; set; }
        public User CurrentUser { get; set; }
        public ObservableCollection<Contact> Contacts
        {
            get => contacts; set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }
        public Contact CurrentContact { get; set; }
        public ICommand EnterContactCommand { get; set; }

        public ICommand PhantomCommand => new Command(async (color) =>
        {
            if ((string)color == "greenphantom.png")

                Phantom = "redphantom.png";
            else
                Phantom = "greenphantom.png";
        });

        public ICommand TappedActivityCommand => new Command(() => {

            isToggled = false;
            ChangeCollection(IsToggled);
        });

        public ICommand TappedPulersCommand => new Command(() => {

            isToggled = true;
            ChangeCollection(IsToggled);
        });

        public PulerViewModel(
            IUserDialogs _dialogs,
            IChatService _chatService)
        {
            ChatService = _chatService;
            dialogs = _dialogs;

            EnterContactCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentContact != null)
                    {
                        await CoreMethods.PushPopupPageModel<ProfileDetailViewModel>(CurrentContact);
                        CurrentContact = null;
                    }

                    IsBusy = false;
                }
            });

            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await Task.Delay(3000);
                RefreshItems(0);
                IsRefreshing = false;
            });

            ChangeCollection(IsToggled);
            Phantom = "greenphantom.png";
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando");

            //await ChatService.InitAsync(CurrentUser.Username);
            //Rooms = await ChatService.GetRooms();

            dialogs.HideLoading();
        }


        private void RefreshItems(int collection)
        {
            var list = contactAgent.GetAll(CurrentUser.Username).objectResult;
            if (collection == 0)
                Contacts = new ObservableCollection<Contact>(list);
            else
                Contacts = new ObservableCollection<Contact>(list);
        }

        private void ChangeCollection(bool toggle)
        {
            if (!toggle)
            {
                Activity = true;
                Pulers = false;
            }
            else
            {
                Pulers = true;
                Activity = false;
            }
        }
    }
}
