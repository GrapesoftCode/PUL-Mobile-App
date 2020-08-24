using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Establishment
    {
        public string id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Rfc { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Cover { get; set; }
        public string MusicalGenre { get; set; }
        public string CostLevel { get; set; }
        public string Type { get; set; }
        public Address Address { get; set; }
        public Address FiscalAddress { get; set; }
        public Logo Logo { get; set; }
        //public List<Image> Inside { get; set; }
        //public List<Image> Outside { get; set; }
        //public List<Document> Documents { get; set; }
    }
}
