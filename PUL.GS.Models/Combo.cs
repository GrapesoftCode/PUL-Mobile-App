using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Combo
    {
        public string id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Logo Logo { get; set; }
        public string establishmentId { get; set; }
        public string userId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
