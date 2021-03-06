﻿using PUL.GS.App.Models;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PUL.GS.App.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }
        public MainPageMasterMenuItem CurrentItem { get; set; }
        public ICommand ItemCommand { get; set; }
        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;

            //ItemCommand = new Command(async () =>
            //{
            //    if (!IsBusy)
            //    {
            //        IsBusy = true;

            //        if (CurrentItem != null)
            //        {
            //            switch (CurrentItem.Title)
            //            {
            //                case "Perfil":
            //                    //var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
            //                    await CoreMethods.PushPageModel<ProfileViewModel>(CurrentUser);
            //                    break;
            //                case "Settings":
            //                    //var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
            //                    //await PushPage(modalPage, null, true);
            //                    break;
            //                case "About":
            //                    //var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
            //                    //await PushPage(modalPage, null, true);
            //                    break;
            //                default:
            //                    break;
            //            }

            //            //IsPresented = false;

            //            CurrentItem = null;
            //        }

            //        IsBusy = false;
            //    }
            //});

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            //MenuItems = new ObservableCollection<MainPageMasterMenuItem>();

            //MenuItems.Add(new MainPageMasterMenuItem { Id = 1, Title = "AJUSTES", Icon = "profile.png" });
            //MenuItems.Add(new MainPageMasterMenuItem { Id = 2, Title = "PAGO", Icon = "profile.png" });
            //MenuItems.Add(new MainPageMasterMenuItem { Id = 3, Title = "CUPONES", Icon = "profile.png" });
            //MenuItems.Add(new MainPageMasterMenuItem { Id = 4, Title = "CENTRO DE AYUDA", Icon = "profile.png" });
            //MenuItems.Add(new MainPageMasterMenuItem { Id = 5, Title = "NOTIFICACIONES", Icon = "profile.png" });
            //MenuItems.Add(new MainPageMasterMenuItem { Id = 6, Title = "HISTORIAL", Icon = "profile.png" });
            //MenuItems.Add(new MainPageMasterMenuItem { Id = 7, Title = "TÉRMINOS Y CONDICIONES", Icon = "profile.png" });


            MenuItems = new ObservableCollection<MainPageMasterMenuItem>()
            {
                new MainPageMasterMenuItem { Id = 1, Title = "AJUSTES", Icon = "profile.png" },
                new MainPageMasterMenuItem { Id = 2, Title = "PAGO", Icon = "profile.png" },
                new MainPageMasterMenuItem { Id = 3, Title = "CUPONES", Icon = "profile.png" },
                new MainPageMasterMenuItem { Id = 4, Title = "CENTRO DE AYUDA", Icon = "profile.png" },
                new MainPageMasterMenuItem { Id = 5, Title = "NOTIFICACIONES", Icon = "profile.png" },
                new MainPageMasterMenuItem { Id = 6, Title = "HISTORIAL", Icon = "profile.png" },
                new MainPageMasterMenuItem { Id = 7, Title = "TÉRMINOS Y CONDICIONES", Icon = "profile.png" },
            };

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
