using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Controls;
using PUL.GS.App.Infrastructure;
using PUL.GS.App.Pages;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Json;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace PUL.GS.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public User CurrentUser { get; set; }

        private readonly AccountData _accountAgent;
        readonly AppSettings appSettings = new AppSettings()
        {
            baseUrl = "http://grapesoft-001-site13.ctempurl.com/api/",
            timeZoneKey = "Central Standard Time (Mexico)"
        };

        public ICommand LoginCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                dialogs.ShowLoading("Cargando", MaskType.Gradient);

                //await ChatService.InitAsync(UserName);

                User user = new User()
                { 
                    Username = UserName,
                    Password = Password
                };

                var token = _accountAgent.GetToken(user);

                if (token.Success)
                {
                    var result = _accountAgent.GetUserByCredentials(UserName, Password, token.objectResult);
                    if (result.Success)
                    {
                        CurrentUser = result.objectResult;
                        //await CoreMethods.PushPageModel<MainViewModel>(CurrentUser);


                        var tabbedNavigation = new CustomTabbedPage();
                        
                        var home = new NavigationPage();
                        await home.PushAsync(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser));
                        home.BarTextColor = Color.Purple;
                        home.IconImageSource = "home.png";

                        var browser = new NavigationPage(FreshPageModelResolver.ResolvePageModel<BrowserViewModel>(CurrentUser))
                        {
                            IconImageSource = "browser.png",
                            BarBackgroundColor = Color.Blue,
                            BarTextColor = Color.Purple
                        };
                        var room = new NavigationPage(FreshPageModelResolver.ResolvePageModel<RoomsViewModel>(CurrentUser))
                        {
                            IconImageSource = "pulear.png",
                            BarBackgroundColor = Color.Blue,
                            BarTextColor = Color.Purple
                        };

                        var chat = new NavigationPage(FreshPageModelResolver.ResolvePageModel<RoomsViewModel>(CurrentUser))
                        {
                            IconImageSource = "message.png",
                            BarBackgroundColor = Color.Blue,
                            BarTextColor = Color.Purple
                        };
                        tabbedNavigation.Children.Add(home);
                        tabbedNavigation.Children.Add(browser);
                        tabbedNavigation.Children.Add(room);
                        tabbedNavigation.Children.Add(chat);
                        tabbedNavigation.BarTextColor = Color.Red;


                        var profile = new ToolbarItem
                        {
                            IconImageSource = "profile.png",
                            Priority = 1,
                            Order = ToolbarItemOrder.Primary
                        };
                        var location = new ToolbarItem
                        {
                            IconImageSource = "location.png",
                            Priority = 2,
                            Order = ToolbarItemOrder.Primary,
                            Text = "location"
                        };


                        var masterDetail = new MasterDetailPage()
                        {
                            Master = FreshPageModelResolver.ResolvePageModel<MasterViewModel>(CurrentUser),
                            Detail = new NavigationPage(tabbedNavigation),
                        };

                        //masterDetail.MasterBehavior = MasterBehavior.Split;
                        //masterDetail.Master.BackgroundColor = Color.Red;
                        //masterDetail.Detail.BackgroundColor = Color.Blue;
                        //masterDetail.BackgroundColor = Color.Yellow;
                        masterDetail.ToolbarItems.Add(profile);
                        masterDetail.ToolbarItems.Add(location);

                        NavigationPage.SetHasNavigationBar(masterDetail, false);

                        //ThemeManager.LoadTheme();

                        //if (IsInitialized)
                        //{
                            await App.Current.MainPage.Navigation.PushAsync(masterDetail);
                        //    IsInitialized = false;
                        //}
                    }

                }


                dialogs.HideLoading();

                IsBusy = false;
            }
        });

        public ICommand LoginFacebookCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;

                //dialogs.ShowLoading("Conectando");

                await CoreMethods.PushPageModel<LoginFacebookViewModel>();

                //dialogs.HideLoading();

                IsBusy = false;
            }
        });

        public ICommand RegisterCommand => new Command(async () => {
            if (!IsBusy)
            {
                IsBusy = true;
                dialogs.ShowLoading("Conectando");
                await CoreMethods.PushPageModel<RegisterViewModel>();
                dialogs.HideLoading();
                IsBusy = false;
            }
        });

        //IChatService ChatService;
        readonly IUserDialogs dialogs;

        public LoginViewModel(
            IUserDialogs _userDialogs
            //IChatService _chatService
            )
        {
            _accountAgent = new AccountData(appSettings);
            //ChatService = _chatService;
            dialogs = _userDialogs;
        }
    }
}
