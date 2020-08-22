using FreshMvvm;
using PUL.GS.App.Models;
using PUL.GS.App.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class MasterViewModel : FreshBasePageModel
    {
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; } = new ObservableCollection<MainPageMasterMenuItem>();

        public override void Init(object initData)
        {
            base.Init(initData);

            MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
            {
                    new MainPageMasterMenuItem { Id = 1, Title = "Inicio", Icon="home.png", TargetType= typeof(RoomsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "Perfil", Icon="profile.png",  TargetType = typeof(ProfilePage) },
                    new MainPageMasterMenuItem { Id = 1, Title = "Salas", Icon="home2.png", TargetType= typeof(RoomsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "Settings", Icon="setting.png",  TargetType = typeof(SettingsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "About", Icon="help.png",  TargetType = typeof(AboutPage) },
                });
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
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
