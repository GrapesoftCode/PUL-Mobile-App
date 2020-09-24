using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class NotificationData
    {
        private readonly AppSettings settings;

        public NotificationData(AppSettings configuration)
        {
            settings = configuration;
        }
    }
}
