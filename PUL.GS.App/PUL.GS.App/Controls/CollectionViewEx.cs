using PUL.GS.App.Dependencies;
using PUL.GS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.Controls
{
    public class CollectionViewEx : CollectionView
    {
        public static BindableProperty ScrollToItemWithConfigProperty = BindableProperty.Create(nameof(ScrollToItemWithConfig), typeof(IConfigurableScrollItem), typeof(CollectionViewEx), default(IConfigurableScrollItem), BindingMode.Default, propertyChanged: OnScrollToItemWithConfigPropertyChanged);
        public IConfigurableScrollItem ScrollToItemWithConfig
        {
            get => (IConfigurableScrollItem)GetValue(ScrollToItemWithConfigProperty);
            set => SetValue(ScrollToItemWithConfigProperty, value);
        }
        private static void OnScrollToItemWithConfigPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            if (bindable is CollectionViewEx current)
            {
                if (newValue is IGroupScrollItem scrollToItemWithGroup)
                {
                    if (scrollToItemWithGroup.Config == null)
                        scrollToItemWithGroup.Config = new ScrollToConfiguration();
                    current.ScrollTo(scrollToItemWithGroup, scrollToItemWithGroup.GroupValue, scrollToItemWithGroup.Config.ScrollToPosition, scrollToItemWithGroup.Config.Animated);
                }
            }
        }
    }
}
