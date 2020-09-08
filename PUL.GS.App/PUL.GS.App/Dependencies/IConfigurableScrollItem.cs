using PUL.GS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Dependencies
{
    public interface IConfigurableScrollItem
    {
        ScrollToConfiguration Config { get; set; }
    }
}
