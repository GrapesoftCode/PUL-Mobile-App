using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.ViewModels
{
    public class ItemsGroupViewModel : List<ItemWithGroupViewModel>
    {
        public string GroupName { get; private set; }

        public ItemsGroupViewModel(string name, List<ItemWithGroupViewModel> group) : base(group)
        {
            this.GroupName = name;
            foreach (var item in group)
            {
                item.GroupValue = this.GroupName;
            }
        }
    }
}
