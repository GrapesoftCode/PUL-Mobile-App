using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Dependencies
{
    public interface INativeServices
    {
        void OnThemeChanged(ThemeManager.Themes theme);
    }
}
