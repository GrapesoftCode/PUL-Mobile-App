using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class CustomerGroup : List<Menu>
    {
        public string Category { get; set; }
        public CustomerGroup(string category, List<Menu> customers) 
            : base(customers)
        {
            Category = category;
        }
    }
}
