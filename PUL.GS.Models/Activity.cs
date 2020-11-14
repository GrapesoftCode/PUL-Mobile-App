using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Activity
    {
        public string id { get; set; }
        public string Activities { get; set; }
        public string Category { get; set; }
        public User User { get; set; }
        public Establishment Establishment { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
