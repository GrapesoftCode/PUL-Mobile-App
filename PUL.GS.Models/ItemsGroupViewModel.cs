using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class ItemsGroupViewModel : List<Menu>
    {
        public string GroupName { get; private set; }

        public ItemsGroupViewModel(string name, List<Menu> group) : base(group)
        {
            this.GroupName = name;
            foreach (var item in group)
            {
                item.GroupValue = this.GroupName;
            }
        }
    }
}
