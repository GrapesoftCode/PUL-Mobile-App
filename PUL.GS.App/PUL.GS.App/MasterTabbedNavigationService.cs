using FreshMvvm;
using PUL.GS.App.Controls;
using PUL.GS.App.Pages;
using PUL.GS.App.ViewModels;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PUL.GS.App
{
    public class MasterTabbedNavigationService : MasterDetailPage, IFreshNavigationService
    {
        CustomTabbedPage _tabbedNavigationPage;
        //MasterDetailPage _masterDetailPage;
        //MasterDetailPage masterDetailsMultiple = new MasterDetailPage();
        public User CurrentUser { get; set; }

        public MasterTabbedNavigationService(User CurrentUser)
        {
            this.CurrentUser = CurrentUser;
            //SetupTabbedPage();
            CreateMenuPage("Menu");
            RegisterNavigation();
            NavigationPage.SetHasNavigationBar(this, false);
            //NavigationPage.SetHasBackButton(masterDetailsMultiple, false);
        }

        //void SetupTabbedPage()
        //{
        //    _tabbedNavigationPage = new CustomTabbedPage();
        //    _tabbedNavigationPage.AddTab<HomeViewModel>(null, "home.png", CurrentUser);
        //    _tabbedNavigationPage.AddTab<BrowserViewModel>(null, "browser.png", CurrentUser);
        //    _tabbedNavigationPage.AddTab<RoomsViewModel>(null, "pulear.png", CurrentUser);
        //    _tabbedNavigationPage.AddTab<RoomsViewModel>(null, "message.png", CurrentUser);
        //    Detail = _tabbedNavigationPage;
        //}

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this);
        }

        protected void CreateMenuPage(string menuPageTitle)
        {
            var _menuPage = new MasterPage
            {
                Title = menuPageTitle
            };
            //var listView = new ListView();

            //listView.ItemsSource = new string[] { "Contacts", "Quotes", "Modal Demo" };

            //listView.ItemSelected += async (sender, args) =>
            //{

            //    switch ((string)args.SelectedItem)
            //    {
            //        //case "Contacts":
            //        //    _tabbedNavigationPage.CurrentPage = _contactsPage;
            //        //    break;
            //        //case "Quotes":
            //        //    _tabbedNavigationPage.CurrentPage = _quotesPage;
            //        //    break;
            //        case "Modal Demo":
            //            var modalPage = FreshPageModelResolver.ResolvePageModel<ProfileViewModel>(CurrentUser);
            //            await PushPage(modalPage, null, true);
            //            break;
            //        default:
            //            break;
            //    }

            //    IsPresented = false;
            //};

            ////_menuPage.Content = listView;

            //this.Master = new NavigationPage(_menuPage) { Title = "Menu" };

            //_masterDetailPage = new MasterDetailPage();

            _tabbedNavigationPage = new CustomTabbedPage();
            _tabbedNavigationPage.AddTab<HomeViewModel>(null, "home.png", CurrentUser);
            _tabbedNavigationPage.AddTab<BrowserViewModel>(null, "browser.png", CurrentUser);
            _tabbedNavigationPage.AddTab<RoomsViewModel>(null, "pulear.png", CurrentUser);
            _tabbedNavigationPage.AddTab<RoomsViewModel>(null, "message.png", CurrentUser);

            var masterPage = FreshPageModelResolver.ResolvePageModel<MasterViewModel>(CurrentUser);
            masterPage.Title = "Master View Model";
            //var masterPageArea = new FreshNavigationContainer(masterPage, "MasterPageArea");
            //masterPageArea.Title = "Menu";
            //this.Title = "Prueba";
            this.Master = new NavigationPage(_menuPage) { Title = "Menu", IconImageSource = "hamburgesa", BarTextColor = Color.FromHex("#0000") };
            this.Detail = _tabbedNavigationPage;
        }





        public string NavigationServiceName => throw new NotImplementedException();

        public void NotifyChildrenPageWasPopped()
        {
            throw new NotImplementedException();
        }

        public async Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                await Navigation.PopModalAsync();
            else
                await ((NavigationPage)_tabbedNavigationPage.CurrentPage).PopAsync();
        }

        public async Task PopToRoot(bool animate = true)
        {
            await((NavigationPage)_tabbedNavigationPage.CurrentPage).PopToRootAsync(animate);
        }

        public async Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                await Navigation.PushModalAsync(new NavigationPage(page), animate);
            else
                await ((NavigationPage)_tabbedNavigationPage.CurrentPage).PushAsync(page, animate);
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            throw new NotImplementedException();
        }
    }
}
