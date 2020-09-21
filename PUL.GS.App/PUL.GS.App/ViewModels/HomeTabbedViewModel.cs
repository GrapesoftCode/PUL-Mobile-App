using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Models;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class HomeTabbedViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }
        public override void Init(object initData)
        {
            base.Init(initData);

            CurrentUser = initData as User;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            //dialogs.ShowLoading("Cargando");
            MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
           {
                    new MainPageMasterMenuItem { Id = 1, Title = "Home", Icon="home.png", TargetType = typeof(HomePage) },
                    new MainPageMasterMenuItem { Id = 2, Title = "Browser", Icon="browser.png",  TargetType = typeof(BrowserPage) },
                    new MainPageMasterMenuItem { Id = 3, Title = "Pulear", Icon="pulear.png", TargetType = typeof(RoomsPage)},
                    new MainPageMasterMenuItem { Id = 4, Title = "Chat", Icon="message.png",  TargetType = typeof(RoomsPage) },
                });
            //dialogs.HideLoading();
        }
       
    }
}
