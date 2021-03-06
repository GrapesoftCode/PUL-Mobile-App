﻿using System;

using Newtonsoft.Json;
using PUL.GS.App.Configuration;
using PUL.GS.App.iOS;
using PUL.GS.App.Pages;
using PUL.GS.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginFacebookPage), typeof(LoginRenderer))]

namespace PUL.GS.App.iOS
{
    public class LoginRenderer : PageRenderer
    {
        [Obsolete]
        public LoginRenderer() { }

        bool showLogin = true;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            //var activity = this.Context as Activity;            //var activity = this.Context as Activity;

            if (showLogin && FacebookAuth.User == null)
            {
                showLogin = false;
                var auth = FacebookAuth.FacebookAuthByClientId();

                auth.Completed += async (sender, eventArgs) =>
                {
                    DismissViewController(true, null);
                    if (eventArgs.IsAuthenticated)
                    {
                        var request = new OAuth2Request("GET", new Uri("https://" +
                            "graph.facebook.com/me?fields=name,picture,cover," +
                            "birthday"),
                            null, eventArgs.Account);

                        var fbResponse = await request.GetResponseAsync();
                        FacebookAuth.User = JsonConvert.DeserializeObject<UserFacebook>
                        (fbResponse.GetResponseText());
                        FacebookAuth.SuccessfullLoginAction.Invoke();
                    }
                    else
                    {

                    }
                };
                PresentViewController(auth.GetUI(), true, null);
            }
        }
    }
}