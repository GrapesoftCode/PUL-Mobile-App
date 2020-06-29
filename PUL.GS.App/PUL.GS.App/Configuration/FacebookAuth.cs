using FreshMvvm;
using PUL.GS.App.Pages;
using PUL.GS.App.ViewModels;
using PUL.GS.Models;
using System;
using Xamarin.Auth;
using Xamarin.Forms;

namespace PUL.GS.App.Configuration
{
    public static class FacebookAuth
    {
        public static UserFacebook CurrentUser;

        private static string ClientId = "737022690378640";
        public static Action SuccessfullLoginAction
        {
            get {
                return new Action(() => {

                    var profilePage =
                        FreshPageModelResolver
                            .ResolvePageModel<ProfileViewModel>(CurrentUser);

                    var navPage =
                        new FreshNavigationContainer(profilePage);

                    Application.Current.MainPage = navPage;
                });
            }
        }

        public static OAuth2Authenticator FacebookAuthByClientId()
        {
            return new OAuth2Authenticator(
              clientId: ClientId,
              scope: "",
              authorizeUrl : new Uri("https://www.facebook.com/v7.0/dialog/oauth/"),
              redirectUrl: new Uri("http://www.facebook.com/connect/" +
              "login_success.html"));
        }
    }
}
