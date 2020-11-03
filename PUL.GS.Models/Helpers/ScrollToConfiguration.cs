﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PUL.GS.Models.Helpers
{
    public class ScrollToConfiguration
    {
        public bool Animated { get; set; } = false;

        public ScrollToPosition ScrollToPosition { get; set; } = ScrollToPosition.Center;
    }
}
