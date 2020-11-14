using System;

using Android.App;
using Newtonsoft.Json;
using PUL.GS.App.Configuration;
using PUL.GS.App.Droid.Renderers;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginFacebookPage), typeof(LoginRenderer))]

namespace PUL.GS.App.Droid.Renderers
{
    public class LoginRenderer : PageRenderer
    {
        [Obsolete]
        public LoginRenderer() { }

        bool showLogin = true;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = this.Context as Android.App.Activity;

            if (showLogin && FacebookAuth.CurrentUser == null)
            {
                showLogin = false;
                var auth = FacebookAuth.FacebookAuthByClientId();

                auth.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)
                    {
                        var request = new OAuth2Request("GET", new Uri("https://" +
                            "graph.facebook.com/me?fields=name,picture,cover," +
                            "birthday"),
                            null, eventArgs.Account);

                        var fbResponse = await request.GetResponseAsync();
                        FacebookAuth.CurrentUser = JsonConvert.DeserializeObject<UserFacebook>
                        (fbResponse.GetResponseText());
                        FacebookAuth.SuccessfullLoginAction.Invoke();
                    }
                    else
                    {

                    }
                };
                activity.StartActivity(auth.GetUI(activity));
            }
        }
    }
}