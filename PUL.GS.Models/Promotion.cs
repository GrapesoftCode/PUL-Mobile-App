using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Promotion
    {
        public string id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; } = "promotion2.jfif";
        public string establishmentId { get; set; }
        public string userId { get; set; }
        //public Image Image { get; set; }
        public DateTime DeadDate { get; set; }
    }
}
