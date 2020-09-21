using FreshMvvm;
using PUL.GS.App.Models;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; } = new User();
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; } = new ObservableCollection<MainPageMasterMenuItem>();
        public MainPageMasterMenuItem CurrentItem { get; set; } = new MainPageMasterMenuItem();
        public ICommand ItemCommand { get; set; }
        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            ItemCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentItem != null)
                    {
                        switch (CurrentItem.Title)
                        {
                            case "Perfil":
                                //var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
                                await CoreMethods.PushPageModel<ProfileViewModel>(CurrentUser);
                                break;
                            case "Settings":
                                //var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
                                //await PushPage(modalPage, null, true);
                                break;
                            case "About":
                                //var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
                                //await PushPage(modalPage, null, true);
                                break;
                            default:
                                break;
                        }

                        //IsPresented = false;

                        CurrentItem = null;
                    }

                    IsBusy = false;
                }
            });

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
            {
                    //new MainPageMasterMenuItem { Id = 1, Title = "Inicio", Icon="home.png", TargetType =  new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser)) },
                    new MainPageMasterMenuItem { Id = 2, Title = "Perfil", Icon="profile.png",  TargetType = typeof(ProfilePage) },
                    //new MainPageMasterMenuItem { Id = 3, Title = "Salas", Icon="home2.png", TargetType =  new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser)) },
                    //new MainPageMasterMenuItem { Id = 4, Title = "Settings", Icon="setting.png",  TargetType =  new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser)) },
                    //new MainPageMasterMenuItem { Id = 5, Title = "About", Icon="help.png",  TargetType = new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser)) },
                });
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }
        //public MasterPageViewModel()
        //{
        //    //MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
        //    //{
        //    //        new MainPageMasterMenuItem { Id = 1, Title = "Inicio", Icon="home.png", TargetType= typeof(RoomsPage) },
        //    //        new MainPageMasterMenuItem { Id = 0, Title = "Perfil", Icon="profile.png",  TargetType = typeof(ProfilePage) },
        //    //        new MainPageMasterMenuItem { Id = 1, Title = "Salas", Icon="home2.png", TargetType= typeof(RoomsPage) },
        //    //        new MainPageMasterMenuItem { Id = 0, Title = "Settings", Icon="setting.png",  TargetType = typeof(SettingsPage) },
        //    //        new MainPageMasterMenuItem { Id = 0, Title = "About", Icon="help.png",  TargetType = typeof(AboutPage) },
        //    //    });
        //}

        #region INotifyPropertyChanged Implementation
        //public event PropertyChangedEventHandler PropertyChanged;
        //void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    if (PropertyChanged == null)
        //        return;

        //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        #endregion
    }
}
