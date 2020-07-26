using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Icon { get; set; }
        public List<Submodules> Submodules { get; set; }
        public int Order { get; set; }
        //public Status Status { get; set; }
        //public CollectionType CollectionType { get; set; }
    }

    public class Submodules
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Order { get; set; }
    }
}
