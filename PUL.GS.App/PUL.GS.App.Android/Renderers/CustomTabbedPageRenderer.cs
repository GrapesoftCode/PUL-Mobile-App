using System;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using PUL.GS.App.Controls;
using PUL.GS.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace PUL.GS.App.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        //private CustomTabbedPage _page;
        public CustomTabbedPageRenderer(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && e.NewElement != null)
            {
                for (int i = 0; i <= this.ViewGroup.ChildCount - 1; i++)
                {
                    var childView = this.ViewGroup.GetChildAt(i);
                    if (childView is ViewGroup viewGroup)
                    {
                        for (int j = 0; j <= viewGroup.ChildCount - 1; j++)
                        {
                            var childRelativeLayoutView = viewGroup.GetChildAt(j);
                            if (childRelativeLayoutView is BottomNavigationView view)
                            {
                                view.ItemIconTintList = null;
                            }
                        }
                    }
                }
            }
        }

        //async void TabLayout.IOnTabSelectedListener.OnTabReselected(TabLayout.Tab tab)
        //{
        //    await _page.CurrentPage.Navigation.PopToRootAsync();
        //}
    }
}
