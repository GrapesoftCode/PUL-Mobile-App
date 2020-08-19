using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Municipality { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string OutdoorNumber { get; set; }
        public string InteriorNumber { get; set; }
    }
}
