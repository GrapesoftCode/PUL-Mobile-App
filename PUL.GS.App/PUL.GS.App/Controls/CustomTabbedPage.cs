﻿using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace PUL.GS.App.Controls
{
    public class CustomTabbedPage : FreshTabbedNavigationContainer
    {
        public CustomTabbedPage()
        {
            this.SetPlatformSpecifics();
        }

        /// <summary>
        /// Sets properties/behaviours based on the currently active <see cref="IConfigPlatform"/>
        /// </summary>
        private void SetPlatformSpecifics()
        {
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            this.BarTextColor = Color.FromHex("#fffff");
        }
    }
}
