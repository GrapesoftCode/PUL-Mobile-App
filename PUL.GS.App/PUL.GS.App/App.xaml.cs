using Acr.UserDialogs;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using FreshMvvm;
using PUL.GS.App.ViewModels;
using PUL.GS.Core.Services;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PUL.GS.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConfigureContainer();

            MainPage = new SplashPage();

            //Remove this method to stop OneSignal Debugging  
            OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.NONE);

            OneSignal.Current.StartInit("58997a76-3b19-4c01-9909-85d729b19784")
            .Settings(new Dictionary<string, bool>() {
            { IOSSettings.kOSSettingsKeyAutoPrompt, false },
            { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
                    .InFocusDisplaying(OSInFocusDisplayOption.Notification)
                    .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();

            
            //MainPage = new MainPage();
        }

        private void ConfigureContainer()
        {
            FreshIOC.Container.Register<IChatService, ChatService>();
            FreshIOC.Container.Register<IUserDialogs>(UserDialogs.Instance);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
