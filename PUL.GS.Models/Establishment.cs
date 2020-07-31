using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Establishment
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Rfc { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Cover { get; set; }
        public string Logo { get; set; }
        //public Address Address { get; set; }
        //public Address FiscalAddress { get; set; }
        //public List<Image> Images { get; set; }
        //public List<Document> Documents { get; set; }
        public string userId { get; set; }
    }
}
