﻿using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Controls;
using PUL.GS.App.Infrastructure;
using PUL.GS.App.Pages;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Json;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace PUL.GS.App.ViewModels
{
    public class LoginViewModel : FreshBasePageModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBusy { get; set; }

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

                dialogs.ShowLoading("Cargando");

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
                        CurrentUser = result.objectResult;

                    
                    //await CoreMethods.PushPageModel<MainViewModel>(CurrentUser);
                    var masterDetail = new FreshMasterDetailNavigationContainer();
                    //masterDetail.AddPage<ProfileViewModel>("Inicio", CurrentUser);
                    //masterDetail.AddPage<RoomsViewModel>("Chat", CurrentUser);
                    
                    masterDetail.Init("Menu", "hamburguesa.png");
                    masterDetail.Master = FreshPageModelResolver.ResolvePageModel<MasterViewModel>();

                    var tabbedNavigation = new CustomTabbedPage();
                    tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
                    tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
                    tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetElevation(12);
                    tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsLegacyColorModeEnabled(true);
                    var home = new NavigationPage(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser))
                    {
                        IconImageSource = "home.png"
                    };
                    var browser = new NavigationPage(FreshPageModelResolver.ResolvePageModel<BrowserViewModel>(CurrentUser))
                    {
                        IconImageSource = "browser.png"
                    };
                    var room = new NavigationPage(FreshPageModelResolver.ResolvePageModel<RoomsViewModel>(CurrentUser))
                    {
                        IconImageSource = "pulear.png"
                    };
                    tabbedNavigation.Children.Add(home);
                    tabbedNavigation.Children.Add(browser);
                    tabbedNavigation.Children.Add(room);

                    NavigationPage.SetHasNavigationBar(masterDetail, true);


                    //tabbedNavigation.Children.Add(FreshPageModelResolver.ResolvePageModel<HomeViewModel>(CurrentUser));
                    //tabbedNavigation.Children.Add(FreshPageModelResolver.ResolvePageModel<BrowserViewModel>(CurrentUser));
                    //tabbedNavigation.Children.Add(FreshPageModelResolver.ResolvePageModel<RoomsViewModel>(CurrentUser));
                    //tabbedNavigation.AddTab<BrowserViewModel>(null,"browser.png", CurrentUser);
                    //tabbedNavigation.AddTab<RoomsViewModel>(null, "pulear.png", CurrentUser);
                    //tabbedNavigation.AddTab<RoomsViewModel>(null, "message.png", CurrentUser);
                    masterDetail.Detail = tabbedNavigation;

                    Xamarin.Forms.Application.Current.MainPage = masterDetail;

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
