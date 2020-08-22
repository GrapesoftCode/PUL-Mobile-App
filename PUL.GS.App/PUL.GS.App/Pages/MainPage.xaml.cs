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

            this.Master = FreshPageModelResolver.ResolvePageModel<MasterViewModel>();
            MasterBehavior = MasterBehavior.Popover;

            ////Create Detail
            CustomTabbedPage tabbedPage = new CustomTabbedPage();
            tabbedPage.BarTextColor = Color.FromHex("#fffff");
            tabbedPage.BarTextColor = Color.Black;
            var home = new NavigationPage(new HomePage())
            {
                IconImageSource = "home.png"
            };
            var browser = new NavigationPage(new BrowserPage())
            {
                IconImageSource = "browser.png"
            };
            var room = new NavigationPage(new RoomsPage())
            {
                IconImageSource = "pulear.png"
            };
            tabbedPage.Children.Add(home);
            tabbedPage.Children.Add(browser);
            tabbedPage.Children.Add(room);
            this.Detail = tabbedPage;

            //BindingContext = new MainViewModel();
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