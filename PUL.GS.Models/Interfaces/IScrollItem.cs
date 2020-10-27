using PUL.GS.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models.Interfaces
{
    public interface IConfigurableScrollItem
    {
        ScrollToConfiguration Config { get; set; }
    }

    public interface IGroupScrollItem : IConfigurableScrollItem
    {
        object GroupValue { get; set; }
    }
}
