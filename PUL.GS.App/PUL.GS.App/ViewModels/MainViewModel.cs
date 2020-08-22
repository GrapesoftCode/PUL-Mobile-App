using FreshMvvm;
using PUL.GS.App.Controls;
using PUL.GS.App.Infrastructure;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        public User CurrentUser { get; set; } = new User();
        //public MainPage MainPage { get; set; } = new MainPage();
        //public MasterViewModel masterViewModel { get; set; }
        //public HomeTabbedViewModel homeTabbedViewModel { get; set; }

        public MainViewModel()
        {
            //NavigationPage.SetHasNavigationBar(MainPage, false);
            //MainPage.Master = FreshPageModelResolver.ResolvePageModel<MasterViewModel>();
            //MainPage.MasterBehavior = MasterBehavior.Popover;
            //// Create Detail
            //CustomTabbedPage tabbedPage = new CustomTabbedPage();
            //tabbedPage.BarTextColor = Color.FromHex("#fffff");
            //tabbedPage.BarTextColor = Color.Black;
            //var home = new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser))
            //{
            //    IconImageSource = "home.png"
            //};
            //var browser = new NavigationPage(FreshPageModelResolver.ResolvePageModel<BrowserViewModel>(CurrentUser))
            //{
            //    IconImageSource = "browser.png"
            //};
            //var room = new NavigationPage(FreshPageModelResolver.ResolvePageModel<RoomsViewModel>(CurrentUser))
            //{
            //    IconImageSource = "pulear.png"
            //};

            //tabbedPage.Children.Add(home);
            //tabbedPage.Children.Add(browser);
            //tabbedPage.Children.Add(room);
            //MainPage.Detail = tabbedPage;
        }



        public override void Init(object initData)
        {
            CurrentUser = initData as User;

        }
    }
}
