using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Contact
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Room { get; set; }
        public string Color { get; set; }
        public string Avatar { get; set; }


        public string FullName {
            get {
                return string.Format("{0} {1}", FirstName, SurName);
            }
        }
    }
}
