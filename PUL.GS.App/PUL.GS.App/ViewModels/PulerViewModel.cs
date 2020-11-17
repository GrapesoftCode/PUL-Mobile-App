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
        readonly IUserDialogs dialogs;
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

        public ObservableCollection<Activity> Activities { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }

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
        public User CurrentUser { get; set; }
        public Contact CurrentContact { get; set; }
        public Activity CurrentActivity { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand EnterContactCommand { get; set; }
        public ICommand ActivityCommand { get; set; }
        public ICommand PhantomCommand => new Command(async (color) =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPopupPageModel<PhantomViewModel>();

                dialogs.HideLoading();

                IsBusy = false;
            }
            //if ((string)color == "greenphantom.png")

            //    Phantom = "redphantom.png";
            //else
            //    Phantom = "greenphantom.png";
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
            IUserDialogs _dialogs)
        {
            dialogs = _dialogs;

            ActivityCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentActivity != null)
                    {
                        Contact contact = new Contact()
                        {
                            FirstName = CurrentActivity.User.FirstName,
                            LastName = CurrentActivity.User.LastName,
                            SurName = CurrentActivity.User.SurName,
                            Username = CurrentActivity.User.Username,
                        };

                        await CoreMethods.PushPopupPageModel<ProfileDetailViewModel>(contact);
                        CurrentActivity = null;
                    }

                    IsBusy = false;
                }
            });

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
                await RefreshItems(0);
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

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData != null)
            {
                var returnData = returnedData as StatusIndicator;


                Phantom = returnData.Image;
            }
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            dialogs.ShowLoading("Cargando...");

            await RefreshItems(0);
            //await ChatService.InitAsync(CurrentUser.Username);
            //Rooms = await ChatService.GetRooms();

            dialogs.HideLoading();
        }


        private async Task RefreshItems(int collection)
        {
            var activities = await activityAgent.GetAll(CurrentUser.Username);
            if (activities.Success) {
                Activities = new ObservableCollection<Activity>(activities.objectResult);
            }
            var contacts = await contactAgent.GetAll(CurrentUser.Username);
            if (contacts.Success)
            {
                Contacts = new ObservableCollection<Contact>(contacts.objectResult);
            }
            //if (collection == 0)
            //    Contacts = new ObservableCollection<Contact>(list.objectResult);
            //else
            //    Contacts = new ObservableCollection<Contact>(list.objectResult);
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
