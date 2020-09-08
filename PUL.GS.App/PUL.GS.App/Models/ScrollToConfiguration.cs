using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.App.Models
{
    public class ScrollToConfiguration
    {
        public bool Animated { get; set; } = true;
        public ScrollToPosition ScrollToPosition { get; set; } = ScrollToPosition.Center;
    }
}
