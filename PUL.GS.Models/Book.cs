using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Book
    {
        public string id { get; set; }
        public User User { get; set; }
        public Establishment Establishment { get; set; }
        public Table Table { get; set; }
        public int Persons { get; set; }
        public string Token { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
