using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class UserFacebook
    {
        public string Name { get; set; }
        public Picture Picture { get; set; }
        public string Id { get; set; }
        public string Birthday { get; set; }
    }
    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}
