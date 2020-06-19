﻿namespace PUL.GS.Models.Messages
{
    public class SimpleTextMessage : ChatMessage
    {
        public string Text { get; set; }
        public SimpleTextMessage()
        {

        }

        public SimpleTextMessage(string sender) : base(sender)
        {

        }
    }
}
