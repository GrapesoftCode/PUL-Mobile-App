using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models.Messages
{
    public class PhotoUrlMessage : ChatMessage
    {
        public string Url { get; set; }
        public PhotoUrlMessage()
        {

        }

        public PhotoUrlMessage(string sender) : base(sender)
        {

        }
    }
}
