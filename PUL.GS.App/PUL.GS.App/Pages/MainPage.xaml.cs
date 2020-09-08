using FreshMvvm;
using PUL.GS.App.Controls;
using PUL.GS.App.Models;
using PUL.GS.App.ViewModels;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace PUL.GS.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public User CurrentUser { get; set; }
        public MainPage()
        {
            InitializeComponent();


            NavigationPage.SetHasNavigationBar(this, false);

            //var tabbedNavigation = new CustomTabbedPage();
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

            //var chat = new NavigationPage(FreshPageModelResolver.ResolvePageModel<RoomsViewModel>(CurrentUser))
            //{
            //    IconImageSource = "message.png"
            //};
            //tabbedNavigation.Children.Add(home);
            //tabbedNavigation.Children.Add(browser);
            //tabbedNavigation.Children.Add(room);
            //tabbedNavigation.Children.Add(chat);


            //var profile = new ToolbarItem {
            //    IconImageSource = "profile.png",
            //    Priority = 1,
            //    Order= ToolbarItemOrder.Primary
            //};
            //var location = new ToolbarItem
            //{
            //    IconImageSource = "location.png",
            //    Priority = 2,
            //    Order = ToolbarItemOrder.Primary,
            //    Text = "location"
            //};

            //Master = FreshPageModelResolver.ResolvePageModel<MasterViewModel>();
            //Detail = new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeTabbedViewModel>());

            //var masterDetail = new CustomTabbedPage()
            //{
            //    Title = "Hola morros",
            //    //BackgroundColor = Color.FromHex("#ffffff"),
            //    Master = FreshPageModelResolver.ResolvePageModel<MasterViewModel>(CurrentUser),
            //    Detail = new NavigationPage(tabbedNavigation),
            //};

            //Detail.MasterBehavior = MasterBehavior.Split;

            //masterDetail.BackgroundColor = Color.FromHex("#fffff");
            //masterDetail.ToolbarItems.Add(profile);
            //masterDetail.ToolbarItems.Add(location);
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (!(e.SelectedItem is MainPageMasterMenuItem item))
        //        return;

        //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    page.Title = item.Title;

        //    Detail = new NavigationPage(page);
        //    IsPresented = false;

        //    MasterPage.ListView.SelectedItem = null;
        //}
    }
}