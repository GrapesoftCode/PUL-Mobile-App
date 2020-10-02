using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PUL.GS.Models
{
    public class Book
    {
        public string id { get; set; }
        public string Token { get; set; }
        public string Qr { get; set; }
        public Table Table { get; set; }
        public int Persons { get; set; }
        public string Hour { get; set; }
        public string Date { get; set; }
        public List<Menu> Menus { get; set; }
        public double TotalMinimumConsumption { get; set; }
        public int TipPercent { get; set; }
        public double SubTotal { get; set; }
        public double Commission { get; set; }
        public double PerPerson { get; set; }
        public double Tip { get; set; }
        public double Total { get; set; }
        public string PlayerId { get; set; }
        public string userId { get; set; }
        public string establishmentId { get; set; }

        public Establishment Establishment { get; set; }
        public User User { get; set; }
        public string FullName 
        { 
            get {

                return string.Format("{0} {1} {2}", User.FirstName, User.SurName, User.LastName);
            }
        }
    }
}
