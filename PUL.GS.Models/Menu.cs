using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Menu
    {
        public string id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Logo Logo { get; set; }
        public string UserId { get; set; }
        public string EstablishmentId { get; set; }
        //public Menu(string name)
        //{
        //    this.Name = name;
        //}

        //public string Id { get; set; }
        //public string Name { get; set; }

        //public override string ToString()
        //{
        //    return Name;
        //}
    }
}
