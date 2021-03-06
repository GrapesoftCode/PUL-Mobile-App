﻿using PUL.GS.Models.Messages;

namespace PUL.GS.Core.EventHandlers
{
    public class MessageEventArgs
    {
        public ChatMessage Message { get; set; }

        public MessageEventArgs(ChatMessage message)
        {
            Message = message;
        }
    }
}
