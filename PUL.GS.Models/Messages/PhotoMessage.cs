using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models.Messages
{
    public class PhotoMessage : ChatMessage
    {
        public string Base64Photo { get; set; }
        public string FileEnding { get; set; }
        public PhotoMessage()
        {

        }

        public PhotoMessage(string sender) : base(sender)
        {

        }
    }
}
