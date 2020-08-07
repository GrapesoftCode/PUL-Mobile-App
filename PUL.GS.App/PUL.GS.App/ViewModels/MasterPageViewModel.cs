using FreshMvvm;
using PUL.GS.App.Models;
using PUL.GS.App.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class MasterPageViewModel : FreshBasePageModel
    {
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

        public MasterPageViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
            {
                    new MainPageMasterMenuItem { Id = 1, Title = "Inicio", Icon="home.png", TargetType= typeof(RoomsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "Perfil", Icon="profile.png",  TargetType = typeof(ProfilePage) },
                    new MainPageMasterMenuItem { Id = 1, Title = "Salas", Icon="home2.png", TargetType= typeof(RoomsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "Settings", Icon="setting.png",  TargetType = typeof(SettingsPage) },
                    new MainPageMasterMenuItem { Id = 0, Title = "About", Icon="help.png",  TargetType = typeof(AboutPage) },
                });
        }

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
