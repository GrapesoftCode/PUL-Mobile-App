using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Logo
    {
        public string Name { get; set; }
        public byte[] ByteArray { get; set; }
        public string MimeType { get; set; }
        public Uri Uri { get; set; }
    }
}
