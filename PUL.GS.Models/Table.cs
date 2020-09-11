using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Table
    {
        public string id { get; set; }
        public int Number { get; set; }
        public int Quantity { get; set; }
        public string SelectType { get; set; }
        public string Location { get; set; }
        public bool SmokingArea { get; set; }
        public double MinimumConsumption { get; set; }
        public bool Vacant { get; set; }
        public int Order { get; set; }
        public string Logo { get; set; }
        public string establishmentId { get; set; }
        public string userId { get; set; }
        public int Count { get; set; }
    }
}
