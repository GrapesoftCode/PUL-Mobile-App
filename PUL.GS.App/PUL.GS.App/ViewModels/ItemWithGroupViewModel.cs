using PUL.GS.App.Dependencies;
using PUL.GS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class ItemWithGroupViewModel : BaseViewModel, IGroupScrollItem
    {
        public ItemWithGroupViewModel()
        {
            this.Config = new ScrollToConfiguration();
        }

        public object GroupValue { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public ScrollToConfiguration Config { get; set; }
    }
}
