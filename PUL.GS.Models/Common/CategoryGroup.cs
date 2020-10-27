using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models.Common
{
    public class CategoryGroup : List<Category>
    {
        public string Category { get; set; }
        public CategoryGroup(string category, List<Category> customers)
            : base(customers)
        {
            Category = category;
        }
    }
}
