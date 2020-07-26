using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class User
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public List<Module> Modules { get; set; }
        public string ModulesString { get; set; }
        public string Token { get; set; }
        public Guid SecurityStamp { get; set; }
        public string Room { get; set; }
        public string Color { get; set; }
        public string Avatar { get; set; }

    }
}
