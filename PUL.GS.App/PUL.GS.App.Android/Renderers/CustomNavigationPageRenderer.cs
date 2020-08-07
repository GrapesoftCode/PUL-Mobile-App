using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Graphics.Drawable;
using Plugin.CurrentActivity;
using PUL.GS.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(PUL.GS.App.Pages.MainPage), typeof(CustomNavigationPageRenderer))]
namespace PUL.GS.App.Droid.Renderers
{
    public class CustomNavigationPageRenderer : MasterDetailPageRenderer
    {
        public CustomNavigationPageRenderer(Context context) : base(context) { }

        //private static Android.Support.V7.Widget.Toolbar GetToolbar() => (CrossCurrentActivity.Current?.Activity as MainActivity)?.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            for (var i = 0; i < toolbar.ChildCount; i++)
            {
                var imageButton = toolbar.GetChildAt(i) as Android.Widget.ImageButton;

                var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
                if (drawerArrow == null)
                    continue;

                imageButton.SetImageDrawable(Context.GetDrawable(Resource.Drawable.hamburguesa));
            }
        }
    }
}