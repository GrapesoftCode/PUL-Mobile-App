using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Dependencies
{
    public interface IGroupScrollItem : IConfigurableScrollItem
    {
        object GroupValue { get; set; }
    }
}
