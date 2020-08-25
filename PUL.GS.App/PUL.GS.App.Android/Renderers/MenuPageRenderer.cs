using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using PUL.GS.App.Droid.Renderers;
using PUL.GS.App.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MenuPage), typeof(MenuPageRenderer))]
namespace PUL.GS.App.Droid.Renderers
{
    class MenuPageRenderer : TabbedPageRenderer
    {
        public MenuPageRenderer(Context context) : base(context)
        {

        }
        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child is TabLayout tabLayout)
            {
                tabLayout.TabMode = TabLayout.ModeScrollable;
            }
        }
    }
}